using Npgsql;
using RinhAPI.DTOs.Responses.Transacao;
using RinhAPI.Exceptions;

namespace RinhAPI.Repositories;

public class TransacaoRepository(IDatabaseConnection databaseConnectionFactory) : ITransacaoRepository
{
    public readonly string PROCEDURE_REALIZA_TRANSACAO = "SELECT * FROM realizar_transacao($1, $2, $3, $4);";
    private readonly IDatabaseConnection _databaseConnectionFactory = databaseConnectionFactory;

    public async Task<TransacaoRealizadaResponse> RealizarTransacao(int idCliente, string tipo, string descricao, int valor)
    {
        try 
        {
            using var connection = _databaseConnectionFactory.GetConnection();
            using var command = new NpgsqlCommand(PROCEDURE_REALIZA_TRANSACAO, connection)
            {
                Parameters = 
                {
                    new() { Value = idCliente },
                    new() { Value = valor },
                    new() { Value = tipo },
                    new() { Value = descricao }
            }};

            await command.PrepareAsync();
            var reader = await command.ExecuteReaderAsync();
            await reader.ReadAsync();

            TransacaoRealizadaResponse transacao = new()
            {
                Saldo = reader.GetInt32(0),
                Limite = reader.GetInt32(1)
            };
            return transacao;
        } catch (PostgresException ex)
        {
            if (ex.MessageText.StartsWith("RN02"))
                throw new LimiteInsuficienteException(ex.Message);
            else
                throw;
        }
    }
}
