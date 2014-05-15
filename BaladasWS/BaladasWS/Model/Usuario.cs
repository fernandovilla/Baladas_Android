using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BaladasWS.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Senha { get; set; }
        public eTipoUsuario Tipo { get; set; }
        public eStatusCadastro Status { get; set; }
        public eGenero Genero { get; set; }

        public static Usuario Selecionar(int id)
        {
            Conexao conn = Conexao.Create();
            Usuario usuario = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = string.Format("Select * From dbo.Usuario Where ID = {0};", id);
                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = id;
                        usuario.Nome = reader.GetString("Nome");
                        usuario.UserName = reader.GetString("Username");
                        usuario.Senha = reader.GetString("Password");
                        usuario.Tipo = (eTipoUsuario)reader.GetByte("Tipo");
                        usuario.Status = (eStatusCadastro)reader.GetByte("Status");
                        usuario.Genero = (eGenero)reader.GetByte("Genero");
                    }
                    reader.Close();
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                usuario = null;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return usuario;
        }

        public static Usuario Selecionar(string userName)
        {
            Conexao conn = Conexao.Create();
            Usuario usuario = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = string.Format("Select ID From dbo.Usuario Where Username = {0};", userName.ToSql());
                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = Selecionar(reader.GetInteger("ID"));
                    }
                    reader.Close();
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                usuario = null;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return usuario;
        }

        public static Usuario RealizarLogin(string username, string senha)
        {
            Conexao conn = Conexao.Create();
            Usuario usuario = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "SELECT ID FROM Usuario Where Username = '{0}' And Password = '{1}';";
                sql = string.Format(sql, username, senha);

                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = Selecionar(reader.GetInteger("ID"));
                    }
                    reader.Close();
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                usuario = null;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return usuario;
        }

        public int Incluir()
        {
            Conexao conn = Conexao.Create();
            string sql = string.Empty;
            int id = 0;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "Insert Into dbo.Usuario (Nome, Username, Password, Tipo, Status, Genero) Values (";
                sql += this.Nome.ToSql() + ", ";
                sql += this.UserName.ToSql() + ", ";
                sql += this.Senha.ToSql() + ", ";
                sql += ((int)this.Tipo).ToSql() + ", ";
                sql += ((int)this.Status).ToSql() + ", ";
                sql += ((int)this.Genero).ToSql() + "); SELECT CAST(SCOPE_IDENTITY() AS INTEGER);";

                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                {
                    id = (int)cmd.ExecuteScalar();
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                id = 0;
            }
            finally
            {
                conn.Close();
            }

            return id;
        }

        public void Alterar()
        { }

        public void Excluir()
        { }
    }
}
