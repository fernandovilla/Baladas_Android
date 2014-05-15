using BaladasWS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace BaladasWS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BaladasService : System.Web.Services.WebService
    {
        [WebMethod(Description="Realiza cadastro de usuários.")]
        [ScriptMethod(UseHttpGet=true, ResponseFormat = ResponseFormat.Json)]
        public string CadastrarUsuario(string hashSessao, string nome, string userName, string password, int tipo)
        {
            Sessao sessao = null;
            Usuario usuario = null;
            Retorno retorno = Retorno.Empty;
            string ret = string.Empty;

            try
            {
                sessao = Sessao.SelecionarSessao(hashSessao);

                if (sessao != null)
                {
                    usuario = new Usuario();
                    usuario.Nome = nome;
                    usuario.UserName = userName;
                    usuario.Senha = password;
                    usuario.Tipo = (eTipoUsuario)tipo;
                    usuario.Status = eStatusCadastro.Normal;
                    usuario.Incluir();

                    retorno = new Retorno();
                    retorno.Status = 0;
                    retorno.Mensagem = "Usuário cadastrado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Falha ao incluir usuário.";
            }

            ret = JSON.Serialize(retorno);
            return ret;
        }

        [WebMethod(Description="Realiza login de usuário e inicia nova sessão.")]
        [ScriptMethod(UseHttpGet=true, ResponseFormat = ResponseFormat.Json)]
        public string RealizarLogin(string username, string senha)
        {
            Usuario usuario = null;
            Sessao sessao = null;
            string ret = string.Empty;

            try
            {
                usuario = Usuario.RealizarLogin(username, senha);

                if (usuario != null)
                {
                    RetornoLoginUsuario retorno = new RetornoLoginUsuario();
                    sessao = Sessao.NovaSessao(usuario);
                    retorno.HashSessao = sessao.Hash;
                    retorno.UserName = usuario.UserName;
                    retorno.Nome = usuario.Nome;
                    ret = JSON.Serialize(retorno);
                }
                else
                {
                    Retorno retorno = new Retorno();
                    retorno.Status = 1;
                    retorno.Mensagem = "Usuario não cadastrado.";
                    ret = JSON.Serialize(retorno);
                }
            }
            catch (Exception ex)
            {
                Retorno retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Erro ao realizar login.";
                ret = JSON.Serialize(retorno);
            }

            return ret;
        }

        [WebMethod(Description="Retorna lista de baladas cadastradas.")]
        [ScriptMethod(UseHttpGet=true, ResponseFormat= ResponseFormat.Json)]
        public string ListarBaladas(string hashSessao)
        {
            string ret = string.Empty;
            Sessao sessao = null;
            RetornoListaBalada[] baladas = null;

            try
            {
                sessao = Sessao.SelecionarSessao(hashSessao);

                if (sessao != null)
                {
                    baladas = Balada.Listar();
                    ret = JSON.Serialize(baladas);
                }
                else
                {
                    Retorno retorno = new Retorno();
                    retorno.Status = 1;
                    retorno.Mensagem = "Sessão não iniciada.";
                    ret = JSON.Serialize(retorno);
                }
            }
            catch (Exception ex)
            {
                Retorno retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Erro ao listar baladas";
                ret = JSON.Serialize(retorno);
            }

            return ret;
        }

        [WebMethod(Description = "Retorna lista de presenças confirmadas na balada.")]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ListarPresencas(string hashSessao, int idBalada)
        {
            string ret = string.Empty;
            Sessao sessao = null;
            RetornoListaPresenca[] presencas = null;

            try
            {
                sessao = Sessao.SelecionarSessao(hashSessao);

                if (sessao != null)
                {
                    presencas = Presenca.ListarPresencas(idBalada);
                    ret = JSON.Serialize(presencas);
                }
                else
                {
                    Retorno retorno = new Retorno();
                    retorno.Status = 1;
                    retorno.Mensagem = "Sessão não iniciada.";
                    ret = JSON.Serialize(retorno);
                }
            }
            catch (Exception ex)
            {
                Retorno retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Erro ao listar presencas";
                ret = JSON.Serialize(retorno);
            }

            return ret;
        }

        [WebMethod(Description="Inclui/Atuliza balada")]
        [ScriptMethod(UseHttpGet=true, ResponseFormat= ResponseFormat.Json)]
        public string SalvarBalada(string hashSessao, int idBalada, string nome, string descricao, string local, string dataHoraInicio, string dataHoraTermino, bool openBar, double valorHomem, double valorMulher)
        {
            string ret = string.Empty;
            Sessao sessao = null;
            Balada balada = null;

            try
            {
                sessao = Sessao.SelecionarSessao(hashSessao);

                if (sessao != null)
                {
                    if (idBalada > 0)
                    {
                        balada = Balada.Selecionar(idBalada);
                        balada.Nome = nome;
                        balada.Descricao = descricao;
                        balada.Local = local;
                        balada.DataHoraInicio = DateTime.Parse(dataHoraInicio);
                        balada.DataHoraTermino = DateTime.Parse(dataHoraTermino);
                        balada.OpenBar = openBar;
                        balada.ValorHomem = valorHomem;
                        balada.ValorMulher = valorMulher;
                        balada.Promoter = sessao.Usuario;
                        balada.Alterar();
                    }
                    else
                    {
                        balada = new Balada();
                        balada.Nome = nome;
                        balada.Descricao = descricao;
                        balada.Local = local;
                        balada.DataHoraInicio = DateTime.Parse(dataHoraInicio);
                        balada.DataHoraTermino = DateTime.Parse(dataHoraTermino);
                        balada.OpenBar = openBar;
                        balada.ValorHomem = valorHomem;
                        balada.ValorMulher = valorMulher;
                        balada.Promoter = sessao.Usuario;
                        balada.Incluir();
                    }

                    RetornoSalvarBalada retorno = new RetornoSalvarBalada();
                    retorno.Status = 0;
                    retorno.Id = balada.Id;
                    retorno.Nome = balada.Nome;
                    retorno.Descricao = balada.Descricao;
                    retorno.Local = balada.Local;
                    retorno.DataHoraInicio = balada.DataHoraInicio.ToString();
                    retorno.DataHoraTermino = balada.DataHoraTermino.ToString();
                    retorno.OpenBar = balada.OpenBar;
                    retorno.ValorHomem = balada.ValorHomem;
                    retorno.ValorMulher = balada.ValorMulher;
                    retorno.Id_Promoter = balada.Promoter.Id;
                    retorno.NomePromoter = balada.Promoter.Nome;
                    
                    ret = JSON.Serialize(retorno);
                }
                else
                {
                    Retorno retorno = new Retorno();
                    retorno.Status = 1;
                    retorno.Mensagem = "Sessão não iniciada.";
                    ret = JSON.Serialize(retorno);
                }
            }
            catch (Exception ex)
            {
                Retorno retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Falha ao salvar balada.";
                ret = JSON.Serialize(retorno);
            }
            
            return ret;
        }

        [WebMethod(Description="Confirmar presença na balada.")]
        [ScriptMethod(UseHttpGet=true, ResponseFormat = ResponseFormat.Json)]
        public string ConfirmarPresenca(string hashSessao, int idBalada, int idUsuario)
        {
            string ret = string.Empty;
            Sessao sessao = null;
            Balada balada = null;
            
            try
            {
                sessao = Sessao.SelecionarSessao(hashSessao);

                if (sessao != null)
                {
                    Retorno retorno = new Retorno();

                    if (sessao.Usuario.Id.Equals(idUsuario))
                    {
                        if (Presenca.Confirmar(idBalada, idUsuario))
                        {
                            retorno.Status = 0;
                            retorno.Mensagem = "Presença confirmada.";
                        }
                        else
                        {
                            retorno.Status = 1;
                            retorno.Mensagem = "Falha ao confirmar presenca";
                        }
                    }
                    else
                    {
                        retorno.Status = 1;
                        retorno.Mensagem = "Usuário inválido.";
                    }

                    ret = JSON.Serialize(retorno);
                }
                else
                {
                    Retorno retorno = new Retorno();
                    retorno.Status = 1;
                    retorno.Mensagem = "Sessão não iniciada.";
                    ret = JSON.Serialize(retorno);
                }
            }
            catch (Exception ex)
            {
                Retorno retorno = new Retorno();
                retorno.Status = 1;
                retorno.Mensagem = "Falha ao confirmar presença.";
                ret = JSON.Serialize(retorno);
            }
            
            return ret;
        }
    }
}