using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("SourceIdPrefix", "SourceId")]
[Table("other_sources")]
[Index("SourceIdPrefix", "SourceId", Name = "other_sources_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class OtherSource
{
    [Key]
    [Column("source_id_prefix")]
    [StringLength(3)]
    public string SourceIdPrefix { get; set; } = null!;

    [Key]
    [Column("source_id", TypeName = "int(11)")]
    public int SourceId { get; set; }

    [Column("source_type")]
    [StringLength(10)]
    public string SourceType { get; set; } = null!;

    [Column("source")]
    [StringLength(64)]
    public string Source { get; set; } = null!;

    [Column("version")]
    [StringLength(10)]
    public string Version { get; set; } = null!;

    [Column("acquisition_date")]
    public DateOnly AcquisitionDate { get; set; }

    [Column("source_comment")]
    [StringLength(500)]
    public string? SourceComment { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
