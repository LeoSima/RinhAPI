using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RinhAPI.Models;

[Table("cliente")]
public class Cliente
{
    [Column("id")]
    public int Id { get; set; }
    [Column("nome")]
    public required string Nome { get; set; }
    [Column("limite")]
    public required int Limite { get; set; }
    [Column("saldo")]
    public int Saldo { get; set; }
}
