using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domain;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private IFuncionarioRepository _funcionarioRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public FuncionariosController()
        {
            _funcionarioRepository = new FuncionarioRepository();
        }

        [HttpGet]
        public IEnumerable<FuncionarioDomain> Get()
        {
            // Faz a chamada para o método .Listar();
            return _funcionarioRepository.ListarFuncionarios();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarFuncionarioPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (funcionarioBuscado == null)
            {
                // Caso não seja encontrado, retorna um status code 404 com a mensagem personalizada
                return NotFound("Nenhum funcionario encontrado");
            }

            // Caso seja encontrado, retorna o gênero buscado
            return Ok(funcionarioBuscado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar();
            _funcionarioRepository.DeletarFuncionario(id);

            // Retorna um status code com uma mensagem personalizada
            return Ok("Funcionario deletado");
        }

        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, FuncionarioDomain funcionarioAtualizado)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarFuncionarioPorId(id);

            // Verifica se nenhum gênero foi encontrado
            if (funcionarioBuscado == null)
            {
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionario não encontrado",
                            erro = true
                        }
                    );
            }

            // Tenta atualizar o registro
            try
            {
                // Faz a chamada para o método .AtualizarIdUrl();
                _funcionarioRepository.AtualizarIdUrl(id, funcionarioAtualizado);

                // Retorna um status code 204 - No Content
                return NoContent();
            }
            // Caso ocorra algum erro
            catch (Exception erro)
            {
                // Retorna BadRequest e o erro
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult PutIdCorpo(FuncionarioDomain funcionarioAtualizado)
        {
            // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
            FuncionarioDomain funcionarioBuscado = _funcionarioRepository.BuscarFuncionarioPorId(funcionarioAtualizado.IdFuncionario);

            // Verifica se algum gênero foi encontrado
            if (funcionarioBuscado != null)
            {
                // Tenta atualizar o registro
                try
                {
                    // Faz a chamada para o método .AtualizarIdCorpo();
                    _funcionarioRepository.AtualizarIdCorpo(funcionarioAtualizado);

                    // Retorna um status code 204 - No Content
                    return NoContent();
                }
                // Caso ocorra algum erro
                catch (Exception erro)
                {
                    // Retorna BadRequest e o erro
                    return BadRequest(erro);
                }

            }

            // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
            // e um bool para representar que houve erro
            return NotFound
                (
                    new
                    {
                        mensagem = "Funcionario não encontrado",
                        erro = true
                    }
                );
        }

        [HttpPost]
        public IActionResult Post(FuncionarioDomain novoFuncionario)
        {
            // Faz a chamada para o método .Cadastrar();
            _funcionarioRepository.CadastrarFuncionario(novoFuncionario);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }
    }
}