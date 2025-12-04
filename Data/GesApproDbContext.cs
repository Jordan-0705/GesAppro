using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class GesApproDbContext : DbContext
    {
        // --- Tables ---
        public DbSet<Article>? Articles { get; set; }
        public DbSet<Fournisseur>? Fournisseurs { get; set; }
        public DbSet<Approvisionnement>? Approvisionnements { get; set; }

        // --- Constructeur optionnel (si utilisé en DI) ---
        public GesApproDbContext(DbContextOptions<GesApproDbContext> options)
            : base(options)
        {
        }

        public GesApproDbContext() { }

        // --- Configuration des entités ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // -----------------------------
            // TABLE Article
            // -----------------------------
            modelBuilder.Entity<Article>()
                .ToTable("articles");

            modelBuilder.Entity<Article>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Article>()
                .Property(a => a.Libelle)
                .IsRequired()
                .HasMaxLength(150);


            // -----------------------------
            // TABLE Fournisseur
            // -----------------------------
            modelBuilder.Entity<Fournisseur>()
                .ToTable("fournisseurs");

            modelBuilder.Entity<Fournisseur>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Fournisseur>()
                .Property(f => f.Nom)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Fournisseur>()
                .Property(f => f.Adresse)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Fournisseur>()
                .Property(f => f.Telephone)
                .IsRequired()
                .HasMaxLength(20);


            // -----------------------------
            // TABLE Approvisionnement
            // -----------------------------
            modelBuilder.Entity<Approvisionnement>()
                .ToTable("approvisionnements");

            modelBuilder.Entity<Approvisionnement>()
                .HasKey(ap => ap.Id);

            modelBuilder.Entity<Approvisionnement>()
                .Property(ap => ap.Reference)
                .IsRequired()
                .HasMaxLength(50);


            // -- Relation Approvisionnement -> Fournisseur (1 fournisseur : N approvisionnements)
            modelBuilder.Entity<Approvisionnement>()
                .HasOne(ap => ap.Fournisseur)
                .WithMany(f => f.Approvisionnements)
                .HasForeignKey(ap => ap.FournisseurId)
                .OnDelete(DeleteBehavior.Restrict); 
                // Important pour éviter suppression en cascade


            // -- Relation Approvisionnement -> Article (1 article : N approvisionnements)
            modelBuilder.Entity<Approvisionnement>()
                .HasOne(ap => ap.Article)
                .WithMany(a => a.Approvisionnements)
                .HasForeignKey(ap => ap.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

        }


        // --- Configuration MySQL ---
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost;Port=3306;Database=GesApproDB;User=root;Password=;",
                    new MySqlServerVersion(new Version(8, 0, 0))
                );
            }
        }
    }
}
