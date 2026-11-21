using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "DocIdPrefix", "DocumentationId")]
[Table("reference_links")]
[Index("Tsn", "DocIdPrefix", "DocumentationId", Name = "reference_links_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class ReferenceLink
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("doc_id_prefix")]
    [StringLength(3)]
    public string DocIdPrefix { get; set; } = null!;

    [Key]
    [Column("documentation_id", TypeName = "int(11)")]
    public int DocumentationId { get; set; }

    [Column("original_desc_ind")]
    [StringLength(1)]
    public string? OriginalDescInd { get; set; }

    [Column("init_itis_desc_ind")]
    [StringLength(1)]
    public string? InitItisDescInd { get; set; }

    [Column("change_track_id", TypeName = "int(11)")]
    public int? ChangeTrackId { get; set; }

    [Column("vernacular_name")]
    [StringLength(80)]
    public string? VernacularName { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
