using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "JurisdictionValue")]
[Table("jurisdiction")]
[Index("Tsn", "JurisdictionValue", Name = "jurisdiction_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Jurisdiction
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("jurisdiction_value")]
    [StringLength(30)]
    public string JurisdictionValue { get; set; } = null!;

    [Column("origin")]
    [StringLength(19)]
    public string Origin { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
