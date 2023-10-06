using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Paytech.Controllers;
using Paytech.Models;
using Paytech.Services;
using Paytech.Utils;
using System;
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

        public async Task<Retorno> Insert(Funcionario funcionario)
        {
            try
            {
                if (!string.IsNullOrEmpty(funcionario?.Cnh?.Num_cnh))
                {
                    var cnhService = new CnhService();
                    var cnh = cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh);
                    cnh ??= await cnhService.Insert(funcionario.Cnh);
                    funcionario.Cnh = cnh;
                }

                if (!string.IsNullOrEmpty(funcionario?.TituloEleitor?.Numero_Titulo))
                {
                    var tituloService = new TituloEleitorService();
                    var titulo = tituloService.GetByTitulo(funcionario.TituloEleitor.Numero_Titulo);
                    titulo ??= await tituloService.Insert(funcionario.TituloEleitor);
                    funcionario.TituloEleitor = titulo;
                }

                if (!string.IsNullOrEmpty(funcionario?.CarteiraTrabalho?.NumCtps) && !string.IsNullOrEmpty(funcionario?.CarteiraTrabalho?.UFCarteira))
                {
                    var carteiraService = new CarteiraTrabalhoService();
                    var carteira = carteiraService.GetById(funcionario.CarteiraTrabalho.NumCtps, funcionario.CarteiraTrabalho.UFCarteira);
                    carteira ??= await carteiraService.Insert(funcionario.CarteiraTrabalho);
                    funcionario.CarteiraTrabalho = carteira;
                }

                //if(!string.IsNullOrEmpty(funcionario.Endereco.Cep))
                //{
                //    var enderecoService = new EnderecoService();
                //    var dto = enderecoService.BuscarEndereco(funcionario.Endereco.Cep).Result;
                //    Endereco endereco = new()
                //    {
                //        Rua = dto.Rua,
                //        Numero = funcionario.Endereco.Numero,
                //        Cep = dto.Cep,
                //        Bairro = dto.Bairro,
                //        Cidade = dto.Cidade,
                //        Uf = dto.Uf,
                //        Complemento = funcionario.Endereco.Complemento
                //    };
                //    funcionario.Endereco = endereco;
                //}

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var param = new
                {
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
                    funcionario.Endereco.Rua,
                    funcionario.Endereco.Numero,
                    funcionario.Endereco.Cep,
                    funcionario.Endereco.Bairro,
                    funcionario.Endereco.Cidade,
                    funcionario.Endereco.Uf,
                    funcionario.Endereco.Complemento
                };
                int id = db.Query<int>(Funcionario.INSERT, param).Single();

                if(id > 0)
                {
                    var func = GetById(id);
                    return new Retorno(true, func, "Dados inseridos com sucesso.");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao inserir");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao inserir: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao inserir: " + ex.Message);
            }

        }

        public List<Funcionario> GetAll()
        {
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.Query<Funcionario>(Funcionario.SELECT_ALL).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public Funcionario GetById(int id)
        {
            try
                {
                Funcionario funcionario = new Funcionario();

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                var selectFunc = db.QueryFirstOrDefault(Funcionario.SELECT_BY_ID, new { Id = id });

                if(selectFunc?.ID > 0)
                {
                    funcionario = JsonConvert.DeserializeObject<Funcionario>(JsonConvert.SerializeObject(selectFunc));
                    funcionario.Cnh = !string.IsNullOrEmpty(selectFunc.num_CNH) ? new CnhService().GetByNumCnh(selectFunc.num_CNH) : new Cnh();
                    funcionario.TituloEleitor = !string.IsNullOrEmpty(selectFunc.numero_titulo) ? new TituloEleitorService().GetByTitulo(selectFunc.numero_titulo) : new TituloEleitor();
                    funcionario.CarteiraTrabalho = !string.IsNullOrEmpty(selectFunc.NumCtps) && !string.IsNullOrEmpty(selectFunc.UFCarteira) ? new CarteiraTrabalhoService().GetById(selectFunc.NumCtps, selectFunc.UFCarteira) : new CarteiraTrabalho();
                    funcionario.Endereco = JsonConvert.DeserializeObject<Endereco>(JsonConvert.SerializeObject(selectFunc));
                }

                return funcionario;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public List<Funcionario> GetByName(string nome)
        {
            nome = "%" + nome + "%";
            try
            {
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                return db.Query<Funcionario>(Funcionario.SELECT_BY_NAME, new { Nome = nome }).AsList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<Retorno> AlterarFuncionario(Funcionario funcionario)
        {
            try
            {
                var carteiraTrabalhoService = new CarteiraTrabalhoService();
                var cnhService = new CnhService();
                var tituloEleitorService = new TituloEleitorService();
                var enderecoService = new EnderecoService();

                if(carteiraTrabalhoService.GetById(funcionario.CarteiraTrabalho.NumCtps, funcionario.CarteiraTrabalho.UFCarteira)  == null)
                {
                    carteiraTrabalhoService.Insert(funcionario.CarteiraTrabalho);
                }
                else
                {
                    carteiraTrabalhoService.AlterarCarteira(funcionario.CarteiraTrabalho);
                }

                if (cnhService.GetByNumCnh(funcionario.Cnh.Num_cnh) == null)
                {
                    cnhService.Insert(funcionario.Cnh);
                }
                else
                {
                    cnhService.AlterarCnh(funcionario.Cnh);
                }

                if (tituloEleitorService.GetByTitulo(funcionario.TituloEleitor.Numero_Titulo) == null)
                {
                    tituloEleitorService.Insert(funcionario.TituloEleitor);
                }
                else
                {
                    tituloEleitorService.AlterarTitulo(funcionario.TituloEleitor);
                }

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
                    funcionario.Endereco.Rua,
                    funcionario.Endereco.Numero,
                    funcionario.Endereco.Cep,
                    funcionario.Endereco.Bairro,
                    funcionario.Endereco.Cidade,
                    funcionario.Endereco.Uf,
                    funcionario.Endereco.Complemento
                };

                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(Funcionario.UPDATE, param);

                if (funcionario.Id > 0)
                {
                    var func = GetById((int)funcionario.Id);
                    return new Retorno(true, func, "Dados atualizados com sucesso.");
                }
                else
                {
                    return new Retorno(false, "Ocorreu um erro ao atualizar");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Retorno(false, "Ocorreu um erro ao atualizar: " + ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var funcionario = GetById(id);

                if (!string.IsNullOrEmpty(funcionario?.Cnh?.Num_cnh))
                    new CnhService().Delete(funcionario.Cnh.Num_cnh);

                if (!string.IsNullOrEmpty(funcionario?.TituloEleitor?.Numero_Titulo))
                    new TituloEleitorService().Delete(funcionario.TituloEleitor.Numero_Titulo);

                if (!string.IsNullOrEmpty(funcionario?.CarteiraTrabalho?.NumCtps) && !string.IsNullOrEmpty(funcionario?.CarteiraTrabalho?.UFCarteira))
                    new CarteiraTrabalhoService().Delete(funcionario.CarteiraTrabalho.NumCtps, funcionario.CarteiraTrabalho.UFCarteira);
                
                using var db = new SqlConnection(configuration.GetConnectionString("sql"));
                db.Execute(Funcionario.DELETE, new { Id = id });
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

    }
}
