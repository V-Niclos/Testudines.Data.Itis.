using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("chg_operation_lkp")]
[Index("ChgOpId", Name = "chg_operation_lkp_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class ChgOperationLkp
{
    [Key]
    [Column("chg_op_id", TypeName = "int(11)")]
    public int ChgOpId { get; set; }

    [Column("change_operation")]
    [StringLength(25)]
    public string ChangeOperation { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
