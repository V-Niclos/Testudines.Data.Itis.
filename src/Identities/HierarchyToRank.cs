using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Keyless]
[Table("HierarchyToRank")]
public partial class HierarchyToRank
{
    [Column("kingdom_tsn", TypeName = "int(11)")]
    public int? KingdomTsn { get; set; }

    [Column("level", TypeName = "int(11)")]
    public int? Level { get; set; }

    [Column("rank_id", TypeName = "smallint(6)")]
    public short? RankId { get; set; }

    [Column("rank_name")]
    [StringLength(15)]
    public string? RankName { get; set; }

    [Column("tsn", TypeName = "int(11)")]
    public int? Tsn { get; set; }

    [Column("parent_tsn", TypeName = "int(11)")]
    public int? ParentTsn { get; set; }

    [Column("scientific_name")]
    [StringLength(163)]
    public string? ScientificName { get; set; }

    [Column("taxon_author")]
    [StringLength(100)]
    public string? TaxonAuthor { get; set; }

    [Column("credibility_rtng")]
    [StringLength(40)]
    public string? CredibilityRtng { get; set; }

    [Column("sort")]
    [StringLength(400)]
    public string? Sort { get; set; }

    [Column(TypeName = "int(11)")]
    public int DirectChildrenCount { get; set; }

    [Column("synonyms")]
    [StringLength(1000)]
    public string? Synonyms { get; set; }
}
