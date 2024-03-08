namespace RinhAPI.DTOs.Requests;

public class TransacaoRequest
{
    public required int Valor { get; set; }
    public required string Tipo { get; set; }
    public required string Descricao { get; set; }
}
