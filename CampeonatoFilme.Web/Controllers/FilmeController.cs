using CampeonatoFilme.Dominio.Entidade;
using CampeonatoFilme.Dominio.Intreface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CampeonatoFilme.Web.Controllers
{
    public class FilmeController : Controller
    {

        private readonly INegocioDominio _negocioDominio;

        public FilmeController(INegocioDominio negocioDominio)
        {
            _negocioDominio = negocioDominio;
        } 

        // GET: Filme
        public ActionResult Index()
        {
            var Filmes = _negocioDominio.ObterFilme();

            return View(Filmes);
        }

        [HttpPost]
        public ActionResult ResultadoCampeonato(List<Filme> filmesSelecionados)
        {

            var filmesResultado = _negocioDominio.ResultadoCampeonato(filmesSelecionados);

            return View(filmesResultado);
        }
    }
}
