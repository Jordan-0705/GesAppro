using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Services;

namespace GestionAppro.Controllers
{
    public class ApprovisionnementController : Controller
    {
        private readonly IApprovisionnementService _approService;
        private readonly IFournisseurService _fournisseurService;
        private readonly IArticleService _articleService;

        public ApprovisionnementController(
            IApprovisionnementService approService,
            IFournisseurService fournisseurService,
            IArticleService articleService)
        {
            _approService = approService;
            _fournisseurService = fournisseurService;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _approService.GetPagedAsync(pageNumber, pageSize);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Fournisseurs = new SelectList(await _fournisseurService.GetAllAsync(), "Id", "Nom");
            ViewBag.Articles = new SelectList(await _articleService.GetAllAsync(), "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Approvisionnement appro)
        {
            if (ModelState.IsValid)
            {
                await _approService.CreateAsync(appro);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fournisseurs = new SelectList(await _fournisseurService.GetAllAsync(), "Id", "Nom");
            ViewBag.Articles = new SelectList(await _articleService.GetAllAsync(), "Id", "Nom");
            return View(appro);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appro = await _approService.GetByIdAsync(id);
            if (appro == null) return NotFound();

            ViewBag.Fournisseurs = new SelectList(await _fournisseurService.GetAllAsync(), "Id", "Nom", appro.FournisseurId);
            ViewBag.Articles = new SelectList(await _articleService.GetAllAsync(), "Id", "Nom", appro.ArticleId);

            return View(appro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Approvisionnement appro)
        {
            if (id != appro.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _approService.UpdateAsync(appro);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Fournisseurs = new SelectList(await _fournisseurService.GetAllAsync(), "Id", "Nom", appro.FournisseurId);
            ViewBag.Articles = new SelectList(await _articleService.GetAllAsync(), "Id", "Nom", appro.ArticleId);

            return View(appro);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var appro = await _approService.GetByIdAsync(id);
            if (appro == null) return NotFound();

            return View(appro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _approService.DeleteAsync(id);
            if (!result) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
