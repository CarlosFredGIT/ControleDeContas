using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControleContas.Presentation.Models;
using ControleContas.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleContas.Presentation.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        public IActionResult DadosDaConta([FromServices] IUsuarioRepository usuarioRepository)
        {
            try
            {
                //pesquisar os dados do usuario no banco atraves do email
                var usuario = usuarioRepository.Obter(User.Identity.Name);

                //enviando os dados para a página
                TempData["IdUsuario"] = usuario.IdUsuario.ToString();
                TempData["NomeUsuario"] = usuario.Nome.ToUpper();
                TempData["EmailUsuario"] = usuario.Email;
                TempData["DataCadastro"] = usuario.DataCadastro.ToString("dd/MM/yyyy");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(AlterarSenhaModel model,
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            try
            {
                if (ModelState.IsValid) //se todos os campos passaram nas validações?
                {
                    var email = User.Identity.Name; //email do usuario autenticado
                    var senha = model.SenhaAtual; //senha atual informada pelo usuario

                    //buscar o usuario no banco de dados atraves do email e senha
                    var usuario = usuarioRepository.Obter(email, senha);

                    //verificando se o usuario foi encontrado..
                    if (usuario != null)
                    {
                        //alterar a senha do usuario no banco de dados
                        usuario.Senha = model.NovaSenha;
                        usuarioRepository.Alterar(usuario);

                        TempData["MensagemSucesso"] = "Nova senha atualizada com sucesso.";
                        ModelState.Clear(); //limpar os campos do formulário
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Senha atual é inválida.";
                    }
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }
    }
}




