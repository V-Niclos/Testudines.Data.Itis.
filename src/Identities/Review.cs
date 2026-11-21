using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "ReviewStartDate")]
[Table("reviews")]
[Index("Tsn", "ReviewStartDate", Name = "reviews_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Review
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("review_start_date")]
    public DateOnly ReviewStartDate { get; set; }

    [Column("review_end_date")]
    public DateOnly? ReviewEndDate { get; set; }

    [Column("review_reason")]
    [StringLength(25)]
    public string ReviewReason { get; set; } = null!;

    [Column("reviewer")]
    [StringLength(100)]
    public string Reviewer { get; set; } = null!;

    [Column("review_comment")]
    [StringLength(255)]
    public string? ReviewComment { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
