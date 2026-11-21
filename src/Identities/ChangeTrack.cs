using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Testudines.Data.Itis.Identities;

[Table("change_tracks")]
[Index("ChangeTrackId", Name = "change_tracks_index")]
[MySqlCollation("latin1_swedish_ci")]
public partial class ChangeTrack
{
    [Key]
    [Column("change_track_id", TypeName = "int(11)")]
    public int ChangeTrackId { get; set; }

    [Column("old_tsn", TypeName = "int(11)")]
    public int? OldTsn { get; set; }

    [Column("change_reason")]
    [StringLength(40)]
    public string ChangeReason { get; set; } = null!;

    [Column("change_initiator")]
    [StringLength(100)]
    public string ChangeInitiator { get; set; } = null!;

    [Column("change_reviewer")]
    [StringLength(100)]
    public string ChangeReviewer { get; set; } = null!;

    [Column("change_certifier")]
    [StringLength(100)]
    public string ChangeCertifier { get; set; } = null!;

    [Column("change_time_stamp", TypeName = "datetime")]
    public DateTime ChangeTimeStamp { get; set; }

    [Column("tsn", TypeName = "int(11)")]
    public int Tsn { get; set; }

    [Column("update_date")]
    public DateOnly UpdateDate { get; set; }
}
