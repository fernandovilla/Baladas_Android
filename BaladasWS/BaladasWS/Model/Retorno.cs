using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BaladasWS.Model
{
    public struct Retorno
    {
         public int Status { get; set; }
         public string Mensagem { get; set; }

         public static Retorno Empty
         {
             get { return new Retorno(); }
         }
    }

    public struct RetornoLoginUsuario
    {
        public string HashSessao { get; set; }
        public string UserName { get; set; }
        public string Nome { get; set; }

        public static RetornoLoginUsuario Empty
        {
            get { return new RetornoLoginUsuario(); }
        }
    }

    public struct RetornoSalvarBalada
    {
        public int Status { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraTermino { get; set; }
        public bool OpenBar { get; set; }
        public double ValorHomem { get; set; }
        public double ValorMulher { get; set; }
        public int Id_Promoter { get; set; }
        public string NomePromoter { get; set; }

        public static RetornoSalvarBalada Empty
        {
            get { return new RetornoSalvarBalada(); }
        }
    }

    public struct RetornoListaBalada
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public string DataHoraInicio { get; set; }
        public string DataHoraTermino { get; set; }
        public bool OpenBar { get; set; }
        public double ValorHomem { get; set; }
        public double ValorMulher { get; set; }
        public int ID_Promoter { get; set; }
        public string NomePromoter { get; set; }

        public static RetornoListaBalada Empty
        {
            get { return new RetornoListaBalada(); }
        }
    }

    public struct RetornoListaPresenca
    {
        public int Id_Usuario { get; set; }
        public string NomeUsuario { get; set; }
        public string GeneroUsuario { get; set; }
        public string DataConfirmacao { get; set; }

        public static RetornoListaPresenca Empty
        {
            get { return new RetornoListaPresenca(); }
        }
    }
}

