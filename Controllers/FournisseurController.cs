using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace WebGestionEmploye.Controllers
{
    public class FournisseurController : Controller
    {
        private readonly IFournisseurService _fournisseurService;

        public FournisseurController(IFournisseurService fournisseurService)
        {
            _fournisseurService = fournisseurService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var fournisseurs = await _fournisseurService.GetAllAsync();
            return View(fournisseurs);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Nom, string Adresse, string Telephone)
        {
            if (!string.IsNullOrWhiteSpace(Nom))
            {
                var fournisseur = new Fournisseur
                {
                    Nom = Nom,
                    Adresse = Adresse,
                    Telephone = Telephone
                };

                await _fournisseurService.CreateAsync(fournisseur);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var fournisseur = await _fournisseurService.GetByIdAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fournisseur fournisseur)
        {
            await _fournisseurService.UpdateAsync(fournisseur);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _fournisseurService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
