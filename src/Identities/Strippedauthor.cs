using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("strippedauthor")]
[Index("TaxonAuthorId", "Shortauthor", Name = "taxon_author_id")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Strippedauthor
{
    [Key]
    [Column("taxon_author_id", TypeName = "int(11)")]
    public int TaxonAuthorId { get; set; }

    [Column("shortauthor")]
    [StringLength(100)]
    public string Shortauthor { get; set; } = null!;
}
