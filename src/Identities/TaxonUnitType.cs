using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("KingdomId", "RankId")]
[Table("taxon_unit_types")]
[Index("KingdomId", "RankId", Name = "taxon_ut_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TaxonUnitType
{
    [Key]
    [Column("kingdom_id", TypeName = "int(11)")]
    public int KingdomId { get; set; }

    [Key]
    [Column("rank_id", TypeName = "smallint(6)")]
    public short RankId { get; set; }

    [Column("rank_name")]
    [StringLength(15)]
    public string RankName { get; set; } = null!;

    [Column("dir_parent_rank_id", TypeName = "smallint(6)")]
    public short DirParentRankId { get; set; }

    [Column("req_parent_rank_id", TypeName = "smallint(6)")]
    public short ReqParentRankId { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
