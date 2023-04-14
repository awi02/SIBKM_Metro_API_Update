using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models;
[Table("tb_tr_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("major", TypeName = "varchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "varchar(10)")]
    public string Degree { get; set; }
    [Column("gpa", TypeName = "varchar(5)")]
    public string Gpa { get; set; }
    [Column("university_id")]
    public int? University_id { get; set; }
    // Cardinality
    public University University { get; set; }
    public Profiling Profiling { get; set; }
}
