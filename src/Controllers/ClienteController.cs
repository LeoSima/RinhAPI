using Microsoft.AspNetCore.Mvc;
using RinhAPI.DTOs.Requests;
using RinhAPI.DTOs.Responses.Extrato;
using RinhAPI.DTOs.Responses.Transacao;
using RinhAPI.Exceptions;
using RinhAPI.Interfaces;
using RinhAPI.Models;

namespace RinhAPI.Controllers;

[Route("clientes")]
[ApiController]
public class ClienteController(ICLienteService service) : ControllerBase
{
    private readonly ICLienteService _clienteService = service;

    [HttpPost("{id}/transacoes")]
    public async Task<IActionResult> RealizarTransacao([FromRoute]int id, [FromBody]TransacaoRequest request)
    {
        try
        {
            Cliente? cliente = await _clienteService.RecuperarClientePorId(id);
            if (cliente is null)
                return NotFound("Usuário não encontrado");

            TransacaoRealizadaResponse transacaoResponse = await _clienteService.RealizarTransacao(cliente, request);

            return Ok(transacaoResponse);
        }
        catch (LimiteInsuficienteException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        catch (OperacaoInvalidaException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}/extrato")]
    public async Task<IActionResult> RecuperarExtrato([FromRoute]int id)
    {
        if (!await _clienteService.VerificarCliente(id))
            return NotFound("Cliente não encontrado");

        ExtratoResponse extratoResponse = await _clienteService.RecuperarExtratoClientePorId(id);
        return Ok(extratoResponse);
    }
}
