using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BaladasWS.Model
{
    public class Balada
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Local { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraTermino { get; set; }
        public bool OpenBar { get; set; }
        public double ValorHomem { get; set; }
        public double ValorMulher { get; set; }

        public Usuario Promoter { get; set; }

        public bool Incluir()
        {
            Conexao conn = Conexao.Create();
            string sql = string.Empty;
            bool result = false;
            int id = 0;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "Insert Into dbo.Balada (Nome, Descricao, Local, DataHoraInicio, DataHoraTermino, ID_Promoter, OpenBar, ValorHomem, ValorMulher) Values (";
                sql += this.Nome.ToSql() + ", ";
                sql += this.Descricao.ToSql() + ", ";
                sql += this.Local.ToSql() + ", ";
                sql += this.DataHoraInicio.ToSql() + ", ";
                sql += this.DataHoraTermino.ToSql() + ", ";
                sql += this.Promoter.Id.ToSql() + ", ";
                sql += this.OpenBar.ToSql() + ", ";
                sql += this.ValorHomem.ToSql() + ", ";
                sql += this.ValorMulher.ToSql() + ");";
                sql += "SELECT CAST(SCOPE_IDENTITY() AS INTEGER)";

                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                {
                    this.Id = (int)cmd.ExecuteScalar();
                }

                result = this.Id > 0;

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                result = false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool Excluir()
        {
            return true;
        }

        public bool Alterar()
        {
            Conexao conn = Conexao.Create();
            string sql = string.Empty;
            bool result = false;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "Update dbo.Balada Set ";
                sql += string.Format("Nome = {0}, ", this.Nome.ToSql());
                sql += string.Format("Descricao = {0}, ", this.Descricao.ToSql());
                sql += string.Format("Local = {0}, ", this.Local.ToSql());
                sql += string.Format("DataHoraInicio = {0}, ", this.DataHoraInicio.ToSql());
                sql += string.Format("DataHoraTermino = {0}, ", this.DataHoraTermino.ToSql());
                sql += string.Format("ID_Promoter = {0}, ", this.Promoter.Id.ToSql());
                sql += string.Format("OpenBar = {0}, ", this.OpenBar.ToSql());
                sql += string.Format("ValorHomem = {0}, ", this.ValorHomem.ToSql());
                sql += string.Format("ValorMulher = {0} ", this.ValorMulher.ToSql());
                sql += string.Format("Where ID = {0};", this.Id);
                
                using (SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                {
                    result = cmd.ExecuteNonQuery() > 0;
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                result = false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        
        public static Balada Selecionar(int idBalada)
        {
            Conexao conn = Conexao.Create();
            Balada balada = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = string.Format("Select * From dbo.Balada Where ID = {0};", idBalada);

                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        balada = new Balada();
                        balada.Id = reader.GetInteger("ID");
                        balada.Nome = reader.GetString("Nome");
                        balada.Descricao = reader.GetString("Descricao");
                        balada.Local = reader.GetString("Local");
                        balada.DataHoraInicio = reader.GetDateTime("DataHoraInicio");
                        balada.DataHoraTermino = reader.GetDateTime("DataHoraTermino");
                        balada.OpenBar = reader.GetBoolean("OpenBar");
                        balada.ValorHomem = reader.GetDouble("ValorHomem");
                        balada.ValorMulher = reader.GetDouble("ValorMulher");
                        balada.Promoter = Usuario.Selecionar(reader.GetInteger("ID_Promoter"));

                    }
                    reader.Close();
                }

                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                balada = null;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return balada;
        }

        public static Balada Selecionar(string nomeBalada)
        {
            Conexao conn = Conexao.Create();
            Balada balada = null;
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "Select Id From dbo.Balada Where Nome = '{0}';";
                sql = string.Format(sql, nomeBalada);

                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        balada = Selecionar(reader.GetInteger("id"));
                    }
                    reader.Close();
                }
                    
                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                balada = null;
            }
            finally
            {
                conn.Close();
            }

            return balada;
        }

        public static RetornoListaBalada[] Listar()
        {
            Conexao conn = Conexao.Create();
            List<RetornoListaBalada> baladas = new List<RetornoListaBalada>();
            string sql = string.Empty;

            try
            {
                conn.Open();
                conn.BeginTrans();

                sql = "SELECT dbo.Balada.*, dbo.Usuario.Id As \"ID_Pormoter\", dbo.Usuario.Nome As \"Nome_Promoter\" FROM dbo.BALADA Inner Join dbo.Usuario On dbo.Balada.ID_Promoter = dbo.Usuario.ID ORDER BY DataHoraInicio;";
                using(SqlCommand cmd = new SqlCommand(sql, conn.CurrentConnection, conn.CurrentTransaction))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        baladas.Add(new RetornoListaBalada()
                        {
                            Id = reader.GetInteger("id"),
                            Nome = reader.GetString("nome"),
                            Descricao = reader.GetString("Descricao"),
                            Local = reader.GetString("Local"),
                            DataHoraInicio = reader.GetDateTime("DataHoraInicio").ToString(),
                            DataHoraTermino = reader.GetDateTime("DataHoraTermino").ToString(),
                            OpenBar = reader.GetByte("OpenBar") == 1,
                            ValorHomem = reader.GetDouble("ValorHomem"),
                            ValorMulher = reader.GetDouble("ValorMulher"),
                            ID_Promoter = reader.GetInteger("ID_Promoter"),
                            NomePromoter = reader.GetString("Nome_Promoter")
                        });
                    }
                    reader.Close();
                }
                    
                conn.CommitTrans();
            }
            catch (Exception ex)
            {
                conn.RolbackTrans();
                baladas = null;
                throw;
            }
            finally
            {
                conn.Close();
            }

            return baladas.ToArray();
        }

        
    }
}