using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("ChangeTrackId", "ChgOpId")]
[Table("change_operations")]
[Index("ChangeTrackId", "ChgOpId", Name = "change_operations_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class ChangeOperation
{
    [Key]
    [Column("change_track_id", TypeName = "int(11)")]
    public int ChangeTrackId { get; set; }

    [Key]
    [Column("chg_op_id", TypeName = "int(11)")]
    public int ChgOpId { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
