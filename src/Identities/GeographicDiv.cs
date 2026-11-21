using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "GeographicValue")]
[Table("geographic_div")]
[Index("Tsn", "GeographicValue", Name = "geographic_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class GeographicDiv
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("geographic_value")]
    [StringLength(45)]
    public string GeographicValue { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
