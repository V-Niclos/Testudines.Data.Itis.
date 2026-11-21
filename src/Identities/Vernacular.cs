using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "VernId")]
[Table("vernaculars")]
[Index("Tsn", "VernacularName", "Language", Name = "vernaculars_index1")]
[Index("Tsn", "VernId", Name = "vernaculars_index2")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Vernacular
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Column("vernacular_name")]
    [StringLength(80)]
    public string VernacularName { get; set; } = null!;

    [Column("language")]
    [StringLength(15)]
    public string Language { get; set; } = null!;

    [Column("approved_ind")]
    [StringLength(1)]
    public string? ApprovedInd { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }

    [Key]
    [Column("vern_id", TypeName = "int(11)")]
    public int VernId { get; set; }
}
