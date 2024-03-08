using RinhAPI.DTOs.Responses.Transacao;

namespace RinhAPI.Repositories;

public interface ITransacaoRepository
{
    Task<TransacaoRealizadaResponse> RealizarTransacao(int idCliente, string tipo, string descricao, int valor);
}