using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Servicos;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ILogDeErroServicos log)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro",
                Detail = ex.Message,
                Status = 500
            };

            TratarErroSql(ex, problem);

            _logger.LogError(ex,
                "Erro não tratado. URL: {Url}",
                context.Request.Path);

            context.Response.Clear();
            context.Response.StatusCode = problem.Status ?? 500;

            await log.LogarErro(ex, context.Request.Path);
            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    private static bool TratarErroSql(
    Exception ex,
    ProblemDetails problem)
    {
        var sqlEx = ObterSqlException(ex);

        if (sqlEx == null)
            return false;

        switch (sqlEx.Number)
        {
            case 547:
                problem.Detail = "Não é possível excluir o registro porque existem vínculos associados.";
                return true;

            case 2601:
            case 2627:
                problem.Detail = "Já existe um registro com essas informações.";
                return true;

            default:
                return false;
        }
    }

    private static SqlException? ObterSqlException(Exception ex)
    {
        while (ex != null)
        {
            if (ex is SqlException sqlEx)
                return sqlEx;

            ex = ex.InnerException!;
        }

        return null;
    }
}