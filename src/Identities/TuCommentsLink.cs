using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "CommentId")]
[Table("tu_comments_links")]
[Index("Tsn", "CommentId", Name = "tu_comments_links_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TuCommentsLink
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("comment_id", TypeName = "int(11)")]
    public int CommentId { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
