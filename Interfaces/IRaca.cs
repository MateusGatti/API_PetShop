using API_Pets.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pets.Interfaces
{
    interface IRaca
    {
        Raca Cadastrar(Raca r);
        List<Raca> LerRacas();
        Raca BuscarPorId(int id);
        Raca Alterar(Raca r);
        Raca Excluir(Raca ra);
    }
}
