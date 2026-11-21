using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("longnames")]
[Index("Tsn", "Completename", Name = "tsn")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Longname
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Column("completename")]
    [StringLength(164)]
    public string Completename { get; set; } = null!;
}
