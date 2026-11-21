using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "DocIdPrefix", "DocumentationId", "VernId")]
[Table("vern_ref_links")]
[Index("Tsn", "DocIdPrefix", "DocumentationId", Name = "vern_rl_index1")]
[Index("Tsn", "VernId", Name = "vern_rl_index2")]
[MySqlCollation("latin1_swedish_ci")]
public partial class VernRefLink
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

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }

    [Key]
    [Column("vern_id", TypeName = "int(11)")]
    public int VernId { get; set; }
}
