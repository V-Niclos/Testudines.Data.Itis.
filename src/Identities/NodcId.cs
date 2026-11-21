using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("NodcId1", "Tsn")]
[Table("nodc_ids")]
[Index("NodcId1", "Tsn", Name = "nodc_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class NodcId
{
    [Key]
    [Column("nodc_id")]
    [StringLength(12)]
    public string NodcId1 { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }

    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }
}
