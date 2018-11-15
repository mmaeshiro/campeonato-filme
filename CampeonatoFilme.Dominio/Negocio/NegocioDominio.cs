using CampeonatoFilme.Dominio.Intreface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampeonatoFilme.Dominio.Entidade;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CampeonatoFilme.Dominio.Negocio
{
    public class NegocioDominio : INegocioDominio
    {
        public IEnumerable<Filme> ObterFilme()
        {
            //var filmes = new List<Filme>()
            //{
            //    new Filme { id = "1", titulo = "LaboratorioFantasma 1", ano = 2000, nota = 9 },
            //    new Filme { id = "2", titulo = "LaboratorioFantasma 2", ano = 2010, nota = 9 },
            //    new Filme { id = "3", titulo = "LaboratorioFantasma 3", ano = 2020, nota = 9 },
            //    new Filme { id = "4", titulo = "LaboratorioFantasma 4", ano = 1900, nota = 5 },
            //    new Filme { id = "5", titulo = "LaboratorioFantasma 5", ano = 1981, nota = 9 },
            //    new Filme { id = "6", titulo = "LaboratorioFantasma 6", ano = 1960, nota = 8},
            //    new Filme { id = "7", titulo = "LaboratorioFantasma 7", ano = 2015, nota = 9 },
            //    new Filme { id = "8", titulo = "LaboratorioFantasma 8", ano = 2018, nota = 10 },
            //    new Filme { id = "9", titulo = "LaboratorioFantasma 9", ano = 2011, nota = 9 },
            //    new Filme { id = "10", titulo = "LaboratorioFantasma 10", ano = 1991, nota = 2 },
            //    new Filme { id = "11", titulo = "LaboratorioFantasma 11", ano = 1999, nota = 3 },
            //    new Filme { id = "12", titulo = "LaboratorioFantasma 12", ano = 2001, nota = 4 },
            //    new Filme { id = "13", titulo = "LaboratorioFantasma 13", ano = 1985, nota = 5 },
            //    new Filme { id = "14", titulo = "LaboratorioFantasma 14", ano = 2005, nota = 6 },
            //    new Filme { id = "15", titulo = "LaboratorioFantasma 15", ano = 2004, nota = 7 },
            //    new Filme { id = "16", titulo = "LaboratorioFantasma 16", ano = 2018, nota = 9 }
            //};
            //return filmes;

            var Filmes = new List<Filme>();

            const string url = "http://copafilmes.azurewebsites.net/";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync("api/filmes").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    Filmes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Filme>>(retorno);
                };

            }

            return Filmes;

        }

        public IEnumerable<Filme> ResultadoCampeonato(List<Filme> filmesSelecionados)
        {            
            var filmesOrdenados = filmesSelecionados.OrderBy(x => x.titulo).ToList();

            var campeonatoQuartas = new List<Filme>();
            var campeonatoSemiFinal = new List<Filme>();           

            // Quartas 
            var filmeA = filmesOrdenados[0];
            var filmeH = filmesOrdenados[7];

            campeonatoQuartas.Add(FilmeGanhador(filmeA, filmeH));

            var filmeB = filmesOrdenados[1];
            var filmeG = filmesOrdenados[6];

            campeonatoQuartas.Add(FilmeGanhador(filmeB, filmeG));

            var filmeC = filmesOrdenados[2];
            var filmeF = filmesOrdenados[5];

            campeonatoQuartas.Add(FilmeGanhador(filmeC, filmeF));

            var filmeD = filmesOrdenados[3];
            var filmeE = filmesOrdenados[4];

            campeonatoQuartas.Add(FilmeGanhador(filmeD, filmeE));

            // Semi Final 
            filmeA = campeonatoQuartas[0];
            filmeB = campeonatoQuartas[1];

            campeonatoSemiFinal.Add(FilmeGanhador(filmeA, filmeB));

            filmeC = campeonatoQuartas[2];
            filmeD = campeonatoQuartas[3];

            campeonatoSemiFinal.Add(FilmeGanhador(filmeC, filmeD));

            // Final
            filmeA = campeonatoSemiFinal[0];
            filmeB = campeonatoSemiFinal[1];

             return FilmeCampeao(filmeA, filmeB);

        }

        private Filme FilmeGanhador(Filme a, Filme b)
        {
            var ganhador = new Filme();

            if (a.nota >= b.nota)
            {
                ganhador = a;
            }

            if (a.nota < b.nota)
            {
                ganhador = b;
            }

            return ganhador;
        }

        private  List<Filme> FilmeCampeao(Filme a, Filme b)
        {
            List<Filme> filmes = new List<Filme>();

            if (a.nota >= b.nota)
            {            
                filmes.Add(a);
                filmes.Add(b);
            };

            if (a.nota < b.nota)
            {              
                filmes.Add(b);
                filmes.Add(a);
            }

            return filmes;
        }
    }
}
