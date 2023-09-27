using Dapper;
using Microsoft.Data.SqlClient;
using Paytech.Models;
using Paytech.Services;
using System.Net;
using System.Reflection.Metadata;

namespace Paytech.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public async Task<Funcionario> Insert(Funcionario funcionario)
        {
            try
            {
                var cnhService = new CnhService();
                var cnh = cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh);
                cnh ??= await cnhService.Insert(funcionario.Cnh);
                funcionario.Cnh = cnh;

                var tituloService = new TituloEleitorService();
                var titulo = tituloService.GetByTitulo(funcionario.TituloEleitor.Numero_Titulo);
                titulo ??= await tituloService.Insert(funcionario.TituloEleitor);
                funcionario.TituloEleitor = titulo;

                var carteiraService = new CarteiraTrabalhoService();
                var carteira = carteiraService.GetById(funcionario.CarteiraTrabalho.NumCtps, funcionario.CarteiraTrabalho.UFCarteira);
                carteira ??= await carteiraService.Insert(funcionario.CarteiraTrabalho);
                funcionario.CarteiraTrabalho = carteira;

                var enderecoService = new EnderecoService();
                var dto = enderecoService.BuscarEndereco(funcionario.Endereco.Cep).Result;
                Endereco endereco = new()
                {
                    Rua = dto.Rua,
                    Numero = funcionario.Endereco.Numero,
                    Cep = dto.Cep,
                    Bairro = dto.Bairro,
                    Cidade = dto.Cidade,
                    Uf = dto.Uf,
                    Complemento = funcionario.Endereco.Complemento
                };
                funcionario.Endereco = endereco;

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var param = new
                {
                    funcionario.Id,
                    funcionario.Nome,
                    funcionario.Cpf,
                    funcionario.Rg,
                    funcionario.Escolaridade,
                    funcionario.Forma_pagamento,
                    funcionario.Salario,
                    funcionario.Telefone,
                    funcionario.Genero,
                    funcionario.Naturalidade,
                    funcionario.Num_reservista,
                    funcionario.Nome_mae,
                    funcionario.Nome_pai,
                    funcionario.Dt_admissao,
                    funcionario.Dt_nascimento,
                    funcionario.Dt_FGTS,
                    funcionario.TituloEleitor.Numero_Titulo,
                    funcionario.Cnh.Num_cnh,
                    funcionario.CarteiraTrabalho.NumCtps,
                    funcionario.CarteiraTrabalho.UFCarteira,
                    funcionario.Funcao,
                    funcionario.Estado_civil,
                    endereco.Rua,
                    endereco.Numero,
                    endereco.Cep,
                    endereco.Bairro,
                    endereco.Cidade,
                    endereco.Uf,
                    endereco.Complemento
                };
                db.Execute(Funcionario.INSERT, param);
                return funcionario;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }

        }

        public List<Funcionario> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.Query<Funcionario>(Funcionario.SELECT_ALL).AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public Funcionario GetById(int id)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.QueryFirstOrDefault<Funcionario>(Funcionario.SELECT_BY_ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public List<Funcionario> GetByName(string nome)
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.Query<Funcionario>(Funcionario.SELECT_BY_NAME).AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }


        public void Delete(int id)
        {
            try
            {
                Funcionario funcionario = GetById(id);
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(Funcionario.DELETE, funcionario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

    }
}
