namespace RinhAPI.DTOs.Responses.Extrato;

public class ExtratoTransacaoResponse
{
    public required int Valor { get; set; }
    public required string Tipo { get; set; }
    public required string Descricao { get; set; }
    public required DateTime Realizada_em { get; set; }
}
