using CampeonatoFilme.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampeonatoFilme.Dominio.Intreface
{
    public interface INegocioDominio
    {
        IEnumerable<Filme> ObterFilme();

        IEnumerable<Filme> ResultadoCampeonato(List<Filme> filmesSelecionados);

    }
}
