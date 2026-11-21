using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microting.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Testudines.Data.Itis.Identities;

namespace Testudines.Data.Itis.Context;

public partial class ItisDbContext : DbContext
{
    public ItisDbContext()
    {
    }

    public ItisDbContext(DbContextOptions<ItisDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChangeComment> ChangeComments { get; set; }

    public virtual DbSet<ChangeOperation> ChangeOperations { get; set; }

    public virtual DbSet<ChangeTrack> ChangeTracks { get; set; }

    public virtual DbSet<ChgOperationLkp> ChgOperationLkps { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Expert> Experts { get; set; }

    public virtual DbSet<GeographicDiv> GeographicDivs { get; set; }

    public virtual DbSet<Hierarchy> Hierarchies { get; set; }

    public virtual DbSet<HierarchyToRank> HierarchyToRanks { get; set; }

    public virtual DbSet<Jurisdiction> Jurisdictions { get; set; }

    public virtual DbSet<Kingdom> Kingdoms { get; set; }

    public virtual DbSet<Longname> Longnames { get; set; }

    public virtual DbSet<NodcId> NodcIds { get; set; }

    public virtual DbSet<OtherSource> OtherSources { get; set; }

    public virtual DbSet<Publication> Publications { get; set; }

    public virtual DbSet<ReferenceLink> ReferenceLinks { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Strippedauthor> Strippedauthors { get; set; }

    public virtual DbSet<SynonymLink> SynonymLinks { get; set; }

    public virtual DbSet<TaxonAuthorsLkp> TaxonAuthorsLkps { get; set; }

    public virtual DbSet<TaxonUnitType> TaxonUnitTypes { get; set; }

    public virtual DbSet<TaxonomicUnit> TaxonomicUnits { get; set; }

    public virtual DbSet<TuCommentsLink> TuCommentsLinks { get; set; }

    public virtual DbSet<VernRefLink> VernRefLinks { get; set; }

    public virtual DbSet<Vernacular> Vernaculars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_bin")
            .HasCharSet("latin1");

        modelBuilder.Entity<ChangeComment>(entity =>
        {
            entity.HasKey(e => new { e.ChangeTrackId, e.ChgCmtId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<ChangeOperation>(entity =>
        {
            entity.HasKey(e => new { e.ChangeTrackId, e.ChgOpId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<ChangeTrack>(entity =>
        {
            entity.HasKey(e => e.ChangeTrackId).HasName("PRIMARY");

            entity.Property(e => e.ChangeTrackId).ValueGeneratedNever();
        });

        modelBuilder.Entity<ChgOperationLkp>(entity =>
        {
            entity.HasKey(e => e.ChgOpId).HasName("PRIMARY");

            entity.Property(e => e.ChgOpId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity.Property(e => e.CommentId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Expert>(entity =>
        {
            entity.HasKey(e => new { e.ExpertIdPrefix, e.ExpertId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.ExpertIdPrefix).IsFixedLength();
        });

        modelBuilder.Entity<GeographicDiv>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.GeographicValue })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<Hierarchy>(entity =>
        {
            entity.HasKey(e => e.HierarchyString).HasName("PRIMARY");
        });

        modelBuilder.Entity<HierarchyToRank>(entity =>
        {
            entity.Property(e => e.RankName).IsFixedLength();
        });

        modelBuilder.Entity<Jurisdiction>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.JurisdictionValue })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<Kingdom>(entity =>
        {
            entity.HasKey(e => e.KingdomId).HasName("PRIMARY");

            entity.Property(e => e.KingdomId).ValueGeneratedNever();
            entity.Property(e => e.KingdomName).IsFixedLength();
        });

        modelBuilder.Entity<Longname>(entity =>
        {
            entity.HasKey(e => e.Tsn).HasName("PRIMARY");

            entity.Property(e => e.Tsn).ValueGeneratedNever();
        });

        modelBuilder.Entity<NodcId>(entity =>
        {
            entity.HasKey(e => new { e.NodcId1, e.Tsn })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.NodcId1).IsFixedLength();
        });

        modelBuilder.Entity<OtherSource>(entity =>
        {
            entity.HasKey(e => new { e.SourceIdPrefix, e.SourceId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.SourceIdPrefix).IsFixedLength();
            entity.Property(e => e.SourceType).IsFixedLength();
            entity.Property(e => e.Version).IsFixedLength();
        });

        modelBuilder.Entity<Publication>(entity =>
        {
            entity.HasKey(e => new { e.PubIdPrefix, e.PublicationId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.PubIdPrefix).IsFixedLength();
        });

        modelBuilder.Entity<ReferenceLink>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.DocIdPrefix, e.DocumentationId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.Property(e => e.DocIdPrefix).IsFixedLength();
            entity.Property(e => e.InitItisDescInd).IsFixedLength();
            entity.Property(e => e.OriginalDescInd).IsFixedLength();
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.ReviewStartDate })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<Strippedauthor>(entity =>
        {
            entity.HasKey(e => e.TaxonAuthorId).HasName("PRIMARY");

            entity.Property(e => e.TaxonAuthorId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SynonymLink>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.TsnAccepted })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<TaxonAuthorsLkp>(entity =>
        {
            entity.HasKey(e => new { e.TaxonAuthorId, e.KingdomId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<TaxonUnitType>(entity =>
        {
            entity.HasKey(e => new { e.KingdomId, e.RankId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.RankName).IsFixedLength();
        });

        modelBuilder.Entity<TaxonomicUnit>(entity =>
        {
            entity.HasKey(e => e.Tsn).HasName("PRIMARY");

            entity.Property(e => e.Tsn).ValueGeneratedNever();
            entity.Property(e => e.CompletenessRtng).IsFixedLength();
            entity.Property(e => e.CurrencyRating).IsFixedLength();
            entity.Property(e => e.UncertainPrntInd).IsFixedLength();
            entity.Property(e => e.UnitInd1).IsFixedLength();
            entity.Property(e => e.UnitInd2).IsFixedLength();
            entity.Property(e => e.UnitName1).IsFixedLength();
            entity.Property(e => e.UnnamedTaxonInd).IsFixedLength();
        });

        modelBuilder.Entity<TuCommentsLink>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.CommentId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
        });

        modelBuilder.Entity<VernRefLink>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.DocIdPrefix, e.DocumentationId, e.VernId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.Property(e => e.DocIdPrefix).IsFixedLength();
        });

        modelBuilder.Entity<Vernacular>(entity =>
        {
            entity.HasKey(e => new { e.Tsn, e.VernId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.ApprovedInd).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
