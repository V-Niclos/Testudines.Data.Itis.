using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[PrimaryKey("Tsn", "TsnAccepted")]
[Table("synonym_links")]
[Index("Tsn", "TsnAccepted", Name = "synonym_links_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class SynonymLink
{
    [Key]
    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Key]
    [Column("tsn_accepted", TypeName = "int(11)")]
    public int TsnAccepted { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
