using RinhAPI.DTOs.Requests;
using RinhAPI.DTOs.Responses.Extrato;
using RinhAPI.DTOs.Responses.Transacao;
using RinhAPI.Models;

namespace RinhAPI.Interfaces;

public interface ICLienteService
{
    Task<bool> VerificarCliente(int idCliente);
    Task<Cliente?> RecuperarClientePorId (int idCLiente);
    Task <TransacaoRealizadaResponse> RealizarTransacao(Cliente cliente, TransacaoRequest request);
    Task <ExtratoResponse> RecuperarExtratoClientePorId(int idCliente);
}
