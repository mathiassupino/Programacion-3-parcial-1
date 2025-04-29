using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TorneoUTN.Web.Datos;
using TorneoUTN.Web.Models;

namespace TorneoUTN.Web.Controllers
{
    public class CompetidorController : Controller
    {
        private readonly TorneoUTNDatos _db = new();

        public IActionResult Index()
        {
            return View(_db.ListarCompetidores());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Disciplinas = new SelectList(_db.ListarDisciplinas(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Competidor competidor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Disciplinas = new SelectList(_db.ListarDisciplinas(), "Id", "Nombre");
                    return View(competidor);
                }

                ViewBag.Error = _db.CrearCompetidor(competidor);

                if (ViewBag.Error != "")
                {
                    ViewBag.Disciplinas = new SelectList(_db.ListarDisciplinas(), "Id", "Nombre");
                    return View("Create");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.Disciplinas = new SelectList(_db.ListarDisciplinas(), "Id", "Nombre");
                return View(competidor);
            }
        }

        public IActionResult CompetidoresPorDisciplina()
        {
            return View(_db.ListarCantidadInscriptosPorDisciplina());
        }
    }
}