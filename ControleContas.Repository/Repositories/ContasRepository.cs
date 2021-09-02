using ControleContas.Repository.Entities;
using ControleContas.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleContas.Repository.Repositories 
{
    public class ContasRepository : IContasRepository
    {
        private readonly string _connectionstring;

        public ContasRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Contas obj)
        {
            var query = @"
                            INSERT INTO CONTAS(IDCONTAS, NOME, PRECO, DATAVENCIMENTO)
                            VALUES(@IdContas, @Nome, @Preco , @DataVencimento)
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Contas obj)
        {
            var query = @"UPDATE CONTAS
                          SET
                            NOME = @Nome, 
                            PRECO = @Preco, 
                            DATAVENCIMENTO = @DataVencimento   
                          WHERE IDCONTAS = @IdContas
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(Contas obj)
        {
            var query = @"
                            DELETE FROM CONTAS
                            WHERE IDCONTAS = @IdContas
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Contas> Consultar()
        {
            var query = @"
                            SELECT * FROM CONTAS
                            ORDER BY NOME DESC
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection.Query<Contas>(query).ToList();
            }
        }

        public Contas ConsultarPorId(Guid id)
        {
            var query = @"
                            SELECT * FROM CONTAS
                            WHERE IDCONTAS = @id
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection.Query<Contas>(query, new { id }).FirstOrDefault();
            }
        }

        public List<Contas> ConsultarPorDatas(DateTime dataMin, DateTime dataMax)
        {
            var query = @"
                            SELECT * FROM CONTAS
                            WHERE DATAVENCIMENTO BETWEEN @dataMin AND @dataMax
                            ORDER BY NOME DESC
                        ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection.Query<Contas>(query, new { dataMin, dataMax }).ToList();
            }
        }
    }
}
