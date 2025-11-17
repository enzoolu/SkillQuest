using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.Models;
using SkillQuest.Api.Models.ValueObjects;

namespace SkillQuest.Api.Repositories
{
    public class SkillQuestDbContext : DbContext
    {
        public SkillQuestDbContext(DbContextOptions<SkillQuestDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Trilha> Trilhas { get; set; }
        public DbSet<Missao> Missoes { get; set; }
        public DbSet<Recompensa> Recompensas { get; set; }
        public DbSet<ProgressoUsuario> ProgressosUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Pontos).HasDefaultValue(0);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasConversion(
                        v => v.Address,
                        v => new Email(v));

                entity.HasIndex(u => u.Email).IsUnique();
            });

            modelBuilder.Entity<Trilha>(entity =>
            {
                entity.ToTable("Trilhas");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Nome).IsRequired().HasMaxLength(150);
                entity.Property(t => t.Descricao).HasMaxLength(1000);
                entity.Property(t => t.Categoria).HasMaxLength(100);
                entity.Property(t => t.Nivel).HasMaxLength(50);
            });

            modelBuilder.Entity<Missao>(entity =>
            {
                entity.ToTable("Missoes");
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Nome).IsRequired().HasMaxLength(200);
                entity.Property(m => m.XP).IsRequired();

                entity.HasOne(m => m.Trilha)
                      .WithMany(t => t.Missoes)
                      .HasForeignKey(m => m.IdTrilha)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Recompensa>(entity =>
            {
                entity.ToTable("Recompensas");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Nome).IsRequired().HasMaxLength(150);
                entity.Property(r => r.PontosNecessarios).IsRequired();
            });

            modelBuilder.Entity<ProgressoUsuario>(entity =>
            {
                entity.ToTable("ProgressosUsuarios");
                entity.HasKey(pu => new { pu.IdUsuario, pu.IdMissao });

                entity.HasOne(pu => pu.Usuario)
                      .WithMany(u => u.Progressos)
                      .HasForeignKey(pu => pu.IdUsuario)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(pu => pu.Missao)
                      .WithMany(m => m.Progressos)
                      .HasForeignKey(pu => pu.IdMissao)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}