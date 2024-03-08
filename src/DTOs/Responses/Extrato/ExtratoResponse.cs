namespace RinhAPI.DTOs.Responses.Extrato;

public class ExtratoResponse
{
    public required ExtratoSaldoResponse Saldo { get; set; }
    public required List<ExtratoTransacaoResponse> Ultimas_transacoes { get; set; }
}
