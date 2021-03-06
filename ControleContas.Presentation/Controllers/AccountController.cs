using ControleContas.Presentation.Models;
using ControleContas.Repository.Entities;
using ControleContas.Repository.Interfaces;
using ControleContas.Messages.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ControleContas.Messages.Services;

namespace ControleContas.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados atraves do email e da senha
                    var usuario = usuarioRepository.Obter(model.Email, model.Senha);

                    //verificar se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //criando a identificação do usuario para o projeto AspNet
                        var identificacao = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                        CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravar um COOKIE com a permissão de acesso para o usuario
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(identificacao));

                        //redirecionamento..
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("Acesso negado, usuário inválido.");
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model, [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //verificar se o email informado já esta cadastrado no banco de dados
                    if (usuarioRepository.Obter(model.Email) != null)
                    {
                        throw new Exception($"O email {model.Email} já está cadastrado no sistema, tente outro.");
                    }
                    else
                    {
                        var usuario = new Usuario();

                        usuario.IdUsuario = Guid.NewGuid();
                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = model.Senha;
                        usuario.DataCadastro = DateTime.Now;

                        usuarioRepository.Inserir(usuario);

                        TempData["MensagemSucesso"] = $"Parabéns {usuario.Nome}, sua conta de usuário foi criada com sucesso.";
                        ModelState.Clear();
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult PasswordRecover()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PasswordRecover(AccountPasswordRecoverModel model,
            [FromServices] IUsuarioRepository usuarioRepository)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados atraves do email..
                    var usuario = usuarioRepository.Obter(model.Email);

                    //verificando se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //gerar uma nova senha para o usuario e alterar no banco de dados
                        usuario.Senha = new Random().Next(99999999, 999999999).ToString();
                        usuarioRepository.Alterar(usuario);

                        //Criando o conteudo do email
                        var emailModel = new EmailModel
                        {
                            EmailDestinatario = usuario.Email,
                            Assunto = "Nova senha gerada com sucesso - Sistema de Contas.",
                            Mensagem = $@"
                            <div style='text-align: center; margin: 40px; padding: 60px; border: 2px solid #ccc; font-size: 16pt;'>
                            <img src='data:image/png;base64,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' />                                
                            <br/><br/>
                            Olá <strong>{usuario.Nome}</strong>,
                            <br/><br/>    
                            O sistema gerou uma nova senha para que você possa acessar sua conta.<br/>
                            Por favor utilize a senha: <strong>{usuario.Senha}</strong>
                            <br/><br/>
                            Não esqueça de, ao acessar o sistema, atualizar esta senha para outra
                            de sua preferência.
                            <br/><br/>              
                            Att<br/>   
                            </div>
                        "
                        };

                        //enviando um email para o usuario com a nova senha..
                        var emailService = new EmailServices();
                        emailService.EnviarMensagem(emailModel);

                        TempData["MensagemSucesso"] = $"Uma nova senha foi gerada com sucesso e enviada para o email {usuario.Email}.";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["MensagemErro"] = "O email informado não está cadastrado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            //Apagar o Cookie de autenticação criado para o usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar o usuario para a página de login do sistema
            return RedirectToAction("Login", "Account");
        }
    }
}