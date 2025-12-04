using System.ComponentModel.DataAnnotations;

namespace Models
{
    public enum StatutAppro
    {
        Recu = 1,
        EnAttente = 2,
        Annule = 3
    }
    public class Approvisionnement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date d'approvisionnement est obligatoire.")]
        public DateTime DateAppro { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La référence est obligatoire.")]
        [StringLength(50)]
        public string Reference { get; set; } = string.Empty;

        // Fournisseur
        [Required]
        public int FournisseurId { get; set; }
        public Fournisseur? Fournisseur { get; set; }

        // Article
        [Required]
        public int ArticleId { get; set; }
        public Article? Article { get; set; }

        // Quantité
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à 0.")]
        public int Quantite { get; set; }

        // Prix
        [Required]
        [Range(1, double.MaxValue)]
        public decimal PrixUnitaire { get; set; }

        // Montant total
        public decimal MontantTotal => Quantite * PrixUnitaire;

        [Required]
        public StatutAppro Statut { get; set; } = StatutAppro.EnAttente;

        public bool IsActive { get; set; } = true;
    }
}
