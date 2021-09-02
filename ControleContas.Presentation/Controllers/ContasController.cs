using ControleContas.Presentation.Models;
using ControleContas.Repository.Entities;
using ControleContas.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Controllers
{
    public class ContasController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ContasCadastroModel model, 
            [FromServices] IContasRepository contasRepository)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var contas = new Contas();

                    contas.IdContas = Guid.NewGuid();
                    contas.Nome = model.Nome;
                    contas.Preco = decimal.Parse(model.Preco);
                    contas.DataVencimento = DateTime.Parse(model.DataVencimento);

                    contasRepository.Inserir(contas);

                    TempData["MensagemSucesso"] = $"Conta {contas.Nome}, cadastrada com sucesso.";

                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Erro {e.Message}";
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Consulta(ContasConsultaModel model, [FromServices] IContasRepository ContasRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Contas = ContasRepository.ConsultarPorDatas
                        (DateTime.Parse(model.DataMin), DateTime.Parse(model.DataMax));
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Erro: {e.Message}";
                }
            }

            return View(model);
        }

        public IActionResult Edicao(Guid id, [FromServices] IContasRepository ContasRepository)
        {
            var model = new ContasEdicaoModel();

            try
            {
                var contas = ContasRepository.ConsultarPorId(id);

                model.IdContas = contas.IdContas;
                model.Nome = contas.Nome;
                model.Preco = contas.Preco.ToString();
                model.DataVencimento = contas.DataVencimento.ToString("yyyy-MM-dd");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Erro: {e.Message}";
            }

            return View(model);
        }

        [HttpPost] 
        public IActionResult Edicao(ContasEdicaoModel model, [FromServices] IContasRepository ContasRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contas = new Contas();

                    contas.IdContas = model.IdContas;
                    contas.Nome = model.Nome;                   
                    contas.Preco = Decimal.Parse(model.Preco);
                    contas.DataVencimento = DateTime.Parse(model.DataVencimento);

                    ContasRepository.Alterar(contas);

                    TempData["MensagemSucesso"] = $"Conta {contas.Nome} atualizada com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = $"Erro: {e.Message}";
                }
            }

            return View();
        }

        public IActionResult Exclusao(Guid id, [FromServices] IContasRepository ContasRepository)
        {
            try
            {
                //consultar a tarefa no banco de dados atraves do ID..
                var contas = ContasRepository.ConsultarPorId(id);

                //excluindo a tarefa no banco de dados..
                ContasRepository.Excluir(contas);

                TempData["MensagemSucesso"] = $"Conta {contas.Nome}, excluída com sucesso.";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Erro: {e.Message}";
            }

            //redirecionamento para a página de consulta
            return RedirectToAction("Consulta");
        }
    }
}
