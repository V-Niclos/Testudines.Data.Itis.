using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("kingdoms")]
[Index("KingdomId", "KingdomName", Name = "kingdoms_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Kingdom
{
    [Key]
    [Column("kingdom_id", TypeName = "int(11)")]
    public int KingdomId { get; set; }

    [Column("kingdom_name")]
    [StringLength(10)]
    public string KingdomName { get; set; } = null!;

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
