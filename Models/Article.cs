using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le libellé est obligatoire.")]
        [StringLength(150)]
        public string Libelle { get; set; } = string.Empty;

        public decimal? PrixUnitaire { get; set; }

        // Navigation
        public ICollection<Approvisionnement>? Approvisionnements { get; set; }
    }
}
