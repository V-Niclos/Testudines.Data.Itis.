using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("hierarchy")]
[Index("HierarchyString", Name = "hierarchy_string")]
[MySqlCollation("latin1_swedish_ci")]
public partial class Hierarchy
{
    [Key]
    [Column("hierarchy_string")]
    [StringLength(300)]
    public string HierarchyString { get; set; } = null!;

    [Column("TSN", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Column("Parent_TSN", TypeName = "int(11)")]
    public int? ParentTsn { get; set; }

    [Column("level", TypeName = "int(11)")]
    public int Level { get; set; }

    [Column(TypeName = "int(11)")]
    public int ChildrenCount { get; set; }
}
