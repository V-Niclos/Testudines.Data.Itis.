using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("taxonomic_units")]
[Index("Tsn", "ParentTsn", Name = "taxon_unit_index1")]
[Index("Tsn", "UnitName1", "NameUsage", Name = "taxon_unit_index2")]
[Index("KingdomId", "RankId", Name = "taxon_unit_index3")]
[Index("Tsn", "TaxonAuthorId", Name = "taxon_unit_index4")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TaxonomicUnit
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Column("unit_ind1")]
    [StringLength(1)]
    public string? UnitInd1 { get; set; }

    [Column("unit_name1")]
    [StringLength(35)]
    public string UnitName1 { get; set; } = null!;

    [Column("unit_ind2")]
    [StringLength(20)]
    public string? UnitInd2 { get; set; }

    [Column("unit_name2")]
    [StringLength(35)]
    public string? UnitName2 { get; set; }

    [Column("unit_ind3")]
    [StringLength(20)]
    public string? UnitInd3 { get; set; }

    [Column("unit_name3")]
    [StringLength(35)]
    public string? UnitName3 { get; set; }

    [Column("unit_ind4")]
    [StringLength(7)]
    public string? UnitInd4 { get; set; }

    [Column("unit_name4")]
    [StringLength(35)]
    public string? UnitName4 { get; set; }

    [Column("unnamed_taxon_ind")]
    [StringLength(1)]
    public string? UnnamedTaxonInd { get; set; }

    [Column("name_usage")]
    [StringLength(12)]
    public string NameUsage { get; set; } = null!;

    [Column("unaccept_reason")]
    [StringLength(50)]
    public string? UnacceptReason { get; set; }

    [Column("credibility_rtng")]
    [StringLength(40)]
    public string CredibilityRtng { get; set; } = null!;

    [Column("completeness_rtng")]
    [StringLength(10)]
    public string? CompletenessRtng { get; set; }

    [Column("currency_rating")]
    [StringLength(7)]
    public string? CurrencyRating { get; set; }

    [Column("phylo_sort_seq", TypeName = "smallint(6)")]
    public short? PhyloSortSeq { get; set; }

    [Column("initial_time_stamp", TypeName = "datetime")]
    public DateTime InitialTimeStamp { get; set; }

    [Column("parent_tsn", TypeName = "int(11)")]
    public int? ParentTsn { get; set; }

    [Column("taxon_author_id", TypeName = "int(11)")]
    public int? TaxonAuthorId { get; set; }

    [Column("hybrid_author_id", TypeName = "int(11)")]
    public int? HybridAuthorId { get; set; }

    [Column("kingdom_id", TypeName = "smallint(6)")]
    public short KingdomId { get; set; }

    [Column("rank_id", TypeName = "smallint(6)")]
    public short RankId { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }

    [Column("uncertain_prnt_ind")]
    [StringLength(3)]
    public string? UncertainPrntInd { get; set; }

    [Column("n_usage", TypeName = "text")]
    public string? NUsage { get; set; }

    [Column("complete_name", TypeName = "tinytext")]
    public string? CompleteName { get; set; }
}
