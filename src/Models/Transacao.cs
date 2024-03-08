using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RinhAPI.Models;

[Table("transacao")]
public class Transacao
{
    [Column("id")]
    public long Id { get; set; }

    [Column("valor")]
    public required int Valor { get; set; }

    [Column("tipo")]
    public required string Tipo { get; set; }

    [Column("descricao")]
    public required string Descricao { get; set; }

    [Column("data")]
    public DateTime Data { get; set; }

    [Column("idCliente")]
    public required int IdCliente { get; set; }
}
