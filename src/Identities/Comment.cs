using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("comments")]
[Index("CommentId", Name = "comments_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Comment
{
    [Key]
    [Column("comment_id", TypeName = "int(11)")]
    public int CommentId { get; set; }

    [Column("commentator")]
    [StringLength(100)]
    public string? Commentator { get; set; }

    [Column("comment_detail", TypeName = "text")]
    public string CommentDetail { get; set; } = null!;

    [Column("comment_time_stamp", TypeName = "datetime")]
    public DateTime CommentTimeStamp { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
