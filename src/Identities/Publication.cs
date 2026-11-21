using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("PubIdPrefix", "PublicationId")]
[Table("publications")]
[Index("PubIdPrefix", "PublicationId", Name = "publications_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Publication
{
    [Key]
    [Column("pub_id_prefix")]
    [StringLength(3)]
    public string PubIdPrefix { get; set; } = null!;

    [Key]
    [Column("publication_id", TypeName = "int(11)")]
    public int PublicationId { get; set; }

    [Column("reference_author")]
    [StringLength(100)]
    public string ReferenceAuthor { get; set; } = null!;

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("publication_name")]
    [StringLength(255)]
    public string PublicationName { get; set; } = null!;

    [Column("listed_pub_date")]
    public DateOnly? ListedPubDate { get; set; }

    [Column("actual_pub_date")]
    public DateOnly ActualPubDate { get; set; }

    [Column("publisher")]
    [StringLength(80)]
    public string? Publisher { get; set; }

    [Column("pub_place")]
    [StringLength(40)]
    public string? PubPlace { get; set; }

    [Column("isbn")]
    [StringLength(16)]
    public string? Isbn { get; set; }

    [Column("issn")]
    [StringLength(16)]
    public string? Issn { get; set; }

    [Column("pages")]
    [StringLength(15)]
    public string? Pages { get; set; }

    [Column("pub_comment")]
    [StringLength(500)]
    public string? PubComment { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
