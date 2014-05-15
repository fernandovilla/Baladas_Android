using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaladasWS.Model
{
    public enum eTipoUsuario {  Admin = 0, Promoter = 1, Comum = 2 }

    public enum eStatusCadastro { Normal = 0, Bloqueado = 1, Excluido = 2 }

    public enum eStatusRetorno { OK = 0 }

    public enum eGenero { Indefinido = 0, Masculino = 1, Feminino = 2 }

}