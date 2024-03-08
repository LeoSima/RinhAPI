namespace RinhAPI.DTOs.Responses.Extrato;

public class ExtratoSaldoResponse
{
    public required int Total { get; set; }
    public required DateTime Data_extrato { get; set; }
    public required int Limite { get; set; }
}
