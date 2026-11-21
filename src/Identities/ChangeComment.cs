using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("ChangeTrackId", "ChgCmtId")]
[Table("change_comments")]
[Index("ChangeTrackId", "ChgCmtId", Name = "change_comments_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class ChangeComment
{
    [Key]
    [Column("change_track_id", TypeName = "int(11)")]
    public int ChangeTrackId { get; set; }

    [Key]
    [Column("chg_cmt_id", TypeName = "int(11)")]
    public int ChgCmtId { get; set; }

    [Column("change_detail")]
    [StringLength(250)]
    public string ChangeDetail { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
