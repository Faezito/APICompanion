public class LogDeErro
{
    public int Id { get; set; }
    public string Mensagem { get; set; } = null!;
    public string StackTrace { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTime Data { get; set; }
}