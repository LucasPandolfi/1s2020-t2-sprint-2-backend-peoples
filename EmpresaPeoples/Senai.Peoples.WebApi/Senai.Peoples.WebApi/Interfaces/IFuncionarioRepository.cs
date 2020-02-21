using Senai.Peoples.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarioRepository
    {
        //Listando todos os funcionarios
        List<FuncionarioDomain> ListarFuncionarios();

        FuncionarioDomain BuscarFuncionarioPorId(int id);

        void DeletarFuncionario(int id);

        void AtualizarIdUrl(int id, FuncionarioDomain funcionario);

        void AtualizarIdCorpo(FuncionarioDomain funcionario);

        void CadastrarFuncionario(FuncionarioDomain funcionario);
    }
}
