using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("TaxonAuthorId", "KingdomId")]
[Table("taxon_authors_lkp")]
[Index("TaxonAuthorId", "TaxonAuthor", "KingdomId", Name = "taxon_authors_id_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TaxonAuthorsLkp
{
    [Key]
    [Column("taxon_author_id", TypeName = "int(11)")]
    public int TaxonAuthorId { get; set; }

    [Column("taxon_author")]
    [StringLength(100)]
    public string TaxonAuthor { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }

    [Key]
    [Column("kingdom_id", TypeName = "smallint(6)")]
    public short KingdomId { get; set; }

    [Column("short_author", TypeName = "text")]
    public string? ShortAuthor { get; set; }
}
