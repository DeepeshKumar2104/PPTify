using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PPTify.Infrastructure;

namespace PPTify.Infrastructure.Persistence.DbContexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLogs> AuditLogs { get; set; }

    public virtual DbSet<AuthTokens> AuthTokens { get; set; }

    public virtual DbSet<PresentationHistory> PresentationHistory { get; set; }

    public virtual DbSet<Presentations> Presentations { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }

    public virtual DbSet<UserCredentials> UserCredentials { get; set; }

    public virtual DbSet<UserPreferences> UserPreferences { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AuditLogs>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.ToTable("audit_logs");

            entity.HasIndex(e => e.ActionType, "idx_audit_logs_action_type");

            entity.HasIndex(e => e.UserId, "idx_audit_logs_user_id");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.ActionDetails)
                .HasColumnType("text")
                .HasColumnName("action_details");
            entity.Property(e => e.ActionType).HasColumnName("action_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("ip_address");
            entity.Property(e => e.LogLevel)
                .HasDefaultValueSql("'INFO'")
                .HasColumnType("enum('INFO','WARNING','ERROR')")
                .HasColumnName("log_level");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .HasColumnName("source");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("audit_logs_ibfk_1");
        });

        modelBuilder.Entity<AuthTokens>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PRIMARY");

            entity.ToTable("auth_tokens");

            entity.HasIndex(e => e.ExpiresAt, "idx_auth_tokens_expires_at");

            entity.HasIndex(e => e.UserId, "idx_auth_tokens_user_id");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("timestamp")
                .HasColumnName("expires_at");
            entity.Property(e => e.Revoked)
                .HasDefaultValueSql("'0'")
                .HasColumnName("revoked");
            entity.Property(e => e.Token)
                .HasMaxLength(512)
                .HasColumnName("token");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuthTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("auth_tokens_ibfk_1");
        });

        modelBuilder.Entity<PresentationHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PRIMARY");

            entity.ToTable("presentation_history");

            entity.HasIndex(e => e.ChangedBy, "changed_by");

            entity.HasIndex(e => e.PresentationId, "presentation_id");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.ChangeDetails)
                .HasColumnType("text")
                .HasColumnName("change_details");
            entity.Property(e => e.ChangeType)
                .HasColumnType("enum('created','updated','deleted')")
                .HasColumnName("change_type");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.PresentationId).HasColumnName("presentation_id");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.PresentationHistory)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("presentation_history_ibfk_2");

            entity.HasOne(d => d.Presentation).WithMany(p => p.PresentationHistory)
                .HasForeignKey(d => d.PresentationId)
                .HasConstraintName("presentation_history_ibfk_1");
        });

        modelBuilder.Entity<Presentations>(entity =>
        {
            entity.HasKey(e => e.PresentationId).HasName("PRIMARY");

            entity.ToTable("presentations");

            entity.HasIndex(e => e.UserId, "idx_presentations_user_id");

            entity.Property(e => e.PresentationId).HasColumnName("presentation_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.FileUrl)
                .HasMaxLength(255)
                .HasColumnName("file_url");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'pending'")
                .HasColumnType("enum('pending','in_progress','completed','failed')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Version)
                .HasDefaultValueSql("'1'")
                .HasColumnName("version");

            entity.HasOne(d => d.User).WithMany(p => p.Presentations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("presentations_ibfk_1");
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PRIMARY");

            entity.ToTable("tasks");

            entity.HasIndex(e => e.PresentationId, "idx_tasks_presentation_id");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("timestamp")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.PresentationId).HasColumnName("presentation_id");
            entity.Property(e => e.Result).HasColumnName("result");
            entity.Property(e => e.StartedAt)
                .HasColumnType("timestamp")
                .HasColumnName("started_at");
            entity.Property(e => e.TaskStatus)
                .HasDefaultValueSql("'queued'")
                .HasColumnType("enum('queued','in_progress','completed','failed')")
                .HasColumnName("task_status");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Presentation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.PresentationId)
                .HasConstraintName("tasks_ibfk_1");
        });

        modelBuilder.Entity<UserCredentials>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user_credentials");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.LastLoginAt)
                .HasColumnType("timestamp")
                .HasColumnName("last_login_at");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordResetToken)
                .HasMaxLength(255)
                .HasColumnName("password_reset_token");
            entity.Property(e => e.ResetTokenExpiry)
                .HasColumnType("timestamp")
                .HasColumnName("reset_token_expiry");
            entity.Property(e => e.TwoFactorSecret)
                .HasMaxLength(512)
                .HasColumnName("two_factor_secret");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.UserCredentials)
                .HasForeignKey<UserCredentials>(d => d.UserId)
                .HasConstraintName("user_credentials_ibfk_1");
        });

        modelBuilder.Entity<UserPreferences>(entity =>
        {
            entity.HasKey(e => e.PreferenceId).HasName("PRIMARY");

            entity.ToTable("user_preferences");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.PreferenceId).HasColumnName("preference_id");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasDefaultValueSql("'en'")
                .HasColumnName("language");
            entity.Property(e => e.Theme)
                .HasMaxLength(50)
                .HasDefaultValueSql("'light'")
                .HasColumnName("theme");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_preferences_ibfk_1");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.UserUniqueId, "user_unique_id").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_active");
            entity.Property(e => e.IsEmailVerified)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_email_verified");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePictureUrl)
                .HasMaxLength(255)
                .HasColumnName("profile_picture_url");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'user'")
                .HasColumnType("enum('admin','user')")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserUniqueId)
                .HasMaxLength(50)
                .HasColumnName("user_unique_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
