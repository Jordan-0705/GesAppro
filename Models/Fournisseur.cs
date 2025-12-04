using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Fournisseur
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du fournisseur est obligatoire.")]
        [StringLength(150)]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        [StringLength(200)]
        public string Adresse { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le téléphone est obligatoire.")]
        [Phone]
        [StringLength(20)]
        public string Telephone { get; set; } = string.Empty;

        public DateTime DateCreation { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Approvisionnement>? Approvisionnements { get; set; }
    }
}
