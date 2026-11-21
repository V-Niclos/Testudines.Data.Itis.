using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("ExpertIdPrefix", "ExpertId")]
[Table("experts")]
[Index("ExpertIdPrefix", "ExpertId", Name = "experts_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Expert
{
    [Key]
    [Column("expert_id_prefix")]
    [StringLength(3)]
    public string ExpertIdPrefix { get; set; } = null!;

    [Key]
    [Column("expert_id", TypeName = "int(11)")]
    public int ExpertId { get; set; }

    [Column("expert")]
    [StringLength(100)]
    public string Expert1 { get; set; } = null!;

    [Column("exp_comment")]
    [StringLength(500)]
    public string? ExpComment { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
