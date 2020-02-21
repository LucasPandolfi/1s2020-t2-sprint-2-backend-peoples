using Senai.Peoples.WebApi.Domain;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {

        private string StringConexao = "Data Source=DESKTOP_PCH001\\SQLEXPRESS; initial catalog=T_Peoples; integrated security=true;";

        public List<FuncionarioDomain> ListarFuncionarios()
        {
            // Cria uma lista generos onde serão armazenados os dados
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdFuncionario, NomeFuncionario, Sobrenome from Funcionarios";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para ler, o laço se repete
                    while (rdr.Read())
                    {
                        // Cria um objeto genero do tipo GeneroDomain
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),

                            // Atribui à propriedade IdGenero o valor da primeira coluna da tabela do banco
                            NomeFuncionario = rdr["NomeFuncionario"].ToString(),

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        // Adiciona o genero criado à tabela generos
                        funcionarios.Add(funcionario);
                    }
                }
            }
            // Retorna a lista de generos
            return funcionarios;
        }

        public FuncionarioDomain BuscarFuncionarioPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectId = $"SELECT IdFuncionario, NomeFuncionario, Sobrenome FROM Funcionarios WHERE IdFuncionario = {id}";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectId, con))
                {
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain()
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),
                            NomeFuncionario = rdr["NomeFuncionario"].ToString(),
                            Sobrenome =rdr["Sobrenome"].ToString()
                        };

                        return funcionario;
                    }

                    return null;
                }
            }
        }



        public void DeletarFuncionario(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AtualizarIdUrl(int id, FuncionarioDomain funcionario)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE Funcionarios SET NomeFuncionario = @NomeFuncionario WHERE IdFuncionario = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NomeFuncionario", funcionario.NomeFuncionario);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void AtualizarIdCorpo(FuncionarioDomain funcionario)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE Funcionarios SET NomeFuncionario = @NomeFuncionario WHERE IdFuncionario = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", funcionario.IdFuncionario);
                    cmd.Parameters.AddWithValue("@NomeFuncionario", funcionario.NomeFuncionario);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void CadastrarFuncionario(FuncionarioDomain funcionario)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro, evitando assim os problemas acima
                string queryInsert = "INSERT INTO Funcionarios(NomeFuncionario, Sobrenome) VALUES (@NomeFuncionario, @Sobrenome)";

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(queryInsert, con);

                // Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@NomeFuncionario", funcionario.NomeFuncionario);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionario.Sobrenome);

                // Abre a conexão com o banco de dados
                con.Open();

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }
    }
}
