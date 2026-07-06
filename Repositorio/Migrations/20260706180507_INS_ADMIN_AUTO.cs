using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    /// <inheritdoc />
    public partial class INS_ADMIN_AUTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
        IF NOT EXISTS (
            SELECT 1
            FROM USUARIOS
            WHERE NomeUsuario = 'admin'
        )
        BEGIN

            INSERT INTO PESSOAS
            (
                NomeCompleto,
                DataNascimento,
                Genero,
                DataCriacao,
                DataAlteracao,
                DataDeletado,
                UsuarioCriacaoId,
                UsuarioAlteracaoId
            )
            VALUES
            (
                'Administrador',
                '1950-01-01',
                'M',
                GETDATE(),
                NULL,
                NULL,
                0,
                0
            );

            DECLARE @PessoaId INT = SCOPE_IDENTITY();

            INSERT INTO USUARIOS
            (
                PessoaId,
                NomeUsuario,
                Email,
                SenhaHash,
                UltimoAcesso,
                Perfil
            )
            VALUES
            (
                @PessoaId,
                'admin',
                'admin@mail.com',
                '$2a$11$6VRK1jdlovVHL6iwLlKgPe8DavMgtVlI1yq1IcC5DJjCeAnV4fIyu',
                GETDATE(),
                1
            );

        END
        """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
