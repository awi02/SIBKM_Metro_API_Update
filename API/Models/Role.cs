using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_tr_role")]
public class Role
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("name", TypeName = "varchar(50)")]
    public string Name { get; set; }
    // Cardinality
    [JsonIgnore]
    public ICollection<AccountRole>? AccountRole { get; set; }
}
