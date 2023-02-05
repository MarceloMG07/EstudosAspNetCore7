using DemoRotas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoRotas.Controllers
{
    [Route("")]
    [Route("teste-rotas")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("Inicial")]
        public IActionResult Index()
        {
            //TODO: Quando o nome da view é o mesmo do metodo não precisa expecificar o nome (Convenção ASP.NET)
            // return View();

            var filme = new Filme
            {
                Titulo = "oi",
                DataLancamento = DateTime.Now,
                Genero = null,
                Avaliacao = 10,
                Valor = 2000
            };

            return RedirectToAction("Privacy", filme);
        }

        //[Route("ComParametros/{id}")]
        //[Route("ComParametros/{id}/{categoria}")]
        //[Route("ComParametros/{id}/{categoria?}")]

        //[Route("ComParametros/{id}/{categoria?}")]
        //TODO: Especificação de rota
        //[Route("ComParametros/{id:int}/{categoria?}")]
        [Route("ComParametros/{id:int}/{categoria:guid}")]

        public IActionResult ComParametros(int id, string categoria)
        {
            //TODO: Retorna uma View
            //return View("Index");

            //TODO: Retorna uma PartialView
            //return PartialView("_Index");

            //TODO: Retorna uma Json
            //return Json(new { id, categoria }); 
            //return Json("{'nome': 'Marcelo', 'sobre_nome': 'Moura Gonçalves'}");

            //TODO: Retorna uma Arquivo
            //var fileBytes = System.IO.File.ReadAllBytes(@"C:\TestFileBytes.txt");
            //var fileName = "ArquivoDownload.txt";
            //return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            //TODO: Retorna string qualquer
            return Content($"Olá mundo! O Id é {id}{(categoria == null ? null : categoria)}.");
        }

        [Route("Privacidade")]
        public IActionResult Privacy(Filme filme)
        {
            //TODO: Verifica se a model esta valida
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("Erro")]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}