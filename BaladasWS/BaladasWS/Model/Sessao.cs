using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BaladasWS.Model
{
    public class Sessao
    {
        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataRegistro { get; set; }
        public string Hash { get; set; }

        public static Sessao SelecionarSessao(int id)
        {
            Conexao conn = Conexao.Create();
            Sessao sessao = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = string.Format("Select * From Sessao Where ID = {0};", id);
                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sessao = new Sessao();
                        sessao.Id = id;
                        sessao.Usuario = Usuario.Selecionar(reader.GetInteger("ID_Usuario"));
                        sessao.DataRegistro = reader.GetDateTime("DataHora");
                    }
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                sessao = null;
            }
            finally
            {
                conn.Close();
            }

            return sessao;
        }

        public static Sessao SelecionarSessao(string hash)
        {
            Conexao conn = Conexao.Create();
            Sessao sessao = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = string.Format("SELECT ID FROM dbo.Sessao WHERE Hash = {0}", hash.ToSql());
                using(SqlCommand command = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sessao = SelecionarSessao(reader.GetInteger("ID"));
                    }
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                sessao = null;
            }
            finally
            {
                conn.Close();
            }

            return sessao;
        }

        public static Sessao NovaSessao(Usuario usuario)
        {
            if (usuario == null) { return null; }

            Conexao conn = Conexao.Create();
            DateTime dataRegistro = DateTime.Now;
            Sessao sessao = null;
            string sql = string.Empty;
            string key = string.Empty;
            string hash = string.Empty;
            int id = 0;

            try
            {
                conn.Open();
                conn.BeginTrans();

                key = usuario.Id.ToString() + dataRegistro.ToString();
                hash = StringMD5.ComputeHash(key);

                sql = "Insert Into dbo.Sessao (ID_Usuario, DataHora, Hash) Values (";
                sql += usuario.Id.ToSql() + ", ";
                sql += dataRegistro.ToSql() + ", ";
                sql += hash.ToSql() + "); SELECT CAST(SCOPE_IDENTITY() AS INTEGER);";

                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                {
                    id = (int)cmd.ExecuteScalar();
                }

                conn.CommitTrans();

                if (id > 0)
                {
                    sessao = new Sessao()
                    {
                        Id = id,
                        Usuario = usuario,
                        DataRegistro = dataRegistro,
                        Hash = hash
                    };
                }
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                sessao = null;
            }
            finally
            {
                conn.Close();
            }

            return sessao;
        }
    }
}