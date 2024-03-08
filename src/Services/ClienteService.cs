using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RinhAPI.Db;
using RinhAPI.DTOs.Requests;
using RinhAPI.DTOs.Responses.Extrato;
using RinhAPI.DTOs.Responses.Transacao;
using RinhAPI.Exceptions;
using RinhAPI.Interfaces;
using RinhAPI.Models;
using RinhAPI.Repositories;

namespace RinhAPI.Services;

public class ClienteService(ITransacaoRepository repository, RinhaDbContext context, IMemoryCache cache) : ICLienteService
{
    private readonly ITransacaoRepository _repository = repository;
    private readonly RinhaDbContext _context = context;
    private readonly IMemoryCache _cache = cache;
    private readonly MemoryCacheEntryOptions opcoesCache = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
    };

    public async Task<bool> VerificarCliente(int idCliente)
    {
        Cliente? cliente = await RecuperarClientePorId(idCliente);
        return cliente is not null;
    }

    public async Task<Cliente?> RecuperarClientePorId(int idCLiente)
    {
        if (_cache.TryGetValue(idCLiente, out Cliente? cliente))
            return cliente;

        cliente = await _context.Cliente.FindAsync(idCLiente);

        if (cliente is not null)
            _cache.Set(idCLiente, cliente, opcoesCache);

        return cliente;
    }

    public async Task<TransacaoRealizadaResponse> RealizarTransacao(Cliente cliente, TransacaoRequest request)
    {
        if (request.Tipo == "c")
        {
            TransacaoRealizadaResponse response = await _repository.RealizarTransacao(cliente.Id, request.Tipo, request.Descricao, request.Valor);
            return response;
        } 
        else if (request.Tipo == "d")
        {
            if (!ValidaLimite(cliente, request.Valor))
                throw new LimiteInsuficienteException("Não há limite disponível para realizar a transação");

            TransacaoRealizadaResponse response = await _repository.RealizarTransacao(cliente.Id, request.Tipo, request.Descricao, request.Valor);
            return response;
        }
        else
        {
            throw new OperacaoInvalidaException("Operação informada inválida. Use \"c\" para crédioto e \"d\" para débito.");
        }
    }

    private bool ValidaLimite(Cliente cliente, int valorDebito)
    {
        int saldoFinal = cliente.Saldo - valorDebito;
        return cliente.Limite + saldoFinal >= 0;
    }

    public async Task<ExtratoResponse> RecuperarExtratoClientePorId(int idCliente)
    {
        Cliente cliente = (await RecuperarClientePorId(idCliente))!;
        ExtratoSaldoResponse clienteSaldo = new()
        {
            Total = cliente.Saldo,
            Data_extrato = DateTime.Now,
            Limite = cliente.Limite
        };

        ExtratoResponse response = new()
        {
            Saldo = clienteSaldo,
            Ultimas_transacoes = []
        };

        response.Ultimas_transacoes = await _context.Transacao
            .Where(transacao => transacao.IdCliente == idCliente)
            .OrderByDescending(transacao => transacao.Data)
            .Take(10)
            .Select(transacao => new ExtratoTransacaoResponse
            {
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                Descricao = transacao.Descricao,
                Realizada_em = transacao.Data.ToUniversalTime()
            })
            .ToListAsync();

        return response;
    }
}
