using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BaladasWS.Model
{
    public class Presenca
    {
        public static bool Confirmar(int idBalada, int idUsuario)
        {
            Conexao conn = Conexao.Create();
            bool usuarioAssociado = false;
            string sql = string.Empty;
            bool result = false;

            try
            {
                conn.Open();
                conn.BeginTrans();

                //Verifica se usuário já está associado a balada;
                sql = string.Format("Select ID_Balada, ID_Usuario From dbo.Presenca Where ID_Balada = {0} And ID_Usuario = {1};", idBalada, idUsuario);
                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuarioAssociado = true;
                        result = true;
                    }
                    reader.Close();
                }

                if (!usuarioAssociado)
                {
                    //Insere um registro de associação caso não estiver;
                    sql = "Insert Into dbo.Presenca (Id_Balada, Id_Usuario, Confirmacao, DataConfirmacao) Values (";
                    sql += idBalada.ToSql() + ", ";
                    sql += idUsuario.ToSql() + ", ";
                    sql += "1, ";
                    sql += DateTime.Now.ToSql() + ");";

                    using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                    {
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                result = false;
                throw;
            }
            finally
            {
                conn.Close();
            }


            return result;
        }

        public static RetornoListaPresenca[] ListarPresencas(int idBalada)
        {
            Conexao conn = Conexao.Create();
            List<RetornoListaPresenca> presencas = new List<RetornoListaPresenca>();
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "Select dbo.Usuario.Id As \"Id_Usuario\", dbo.Usuario.Nome As \"Nome_Usuario\", "; 
                sql += "dbo.Usuario.Genero As \"Genero_Usuario\", dbo.Presenca.DataConfirmacao As \"Data_Confirmacao\"  ";
                sql += "From (dbo.Presenca Right Join dbo.Balada On dbo.Presenca.Id_Balada = dbo.Balada.Id) ";
                sql += "Left Join dbo.Usuario on dbo.Usuario.Id = dbo.Presenca.Id_Usuario ";
                sql += "Where dbo.Balada.Id = " + idBalada.ToSql();

                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        presencas.Add(new RetornoListaPresenca()
                        {
                            Id_Usuario = reader.GetInteger("Id_Usuario"),
                            NomeUsuario = reader.GetString("Nome_Usuario"),
                            GeneroUsuario = reader.GetByte("Genero_Usuario").ToString(),
                            DataConfirmacao = reader.GetDateTime("Data_Confirmacao").ToString()
                        });
                    }
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                presencas = new List<RetornoListaPresenca>();
            }
            finally
            {
                conn.Close();
            }

            return presencas.ToArray();
        }
    }
}