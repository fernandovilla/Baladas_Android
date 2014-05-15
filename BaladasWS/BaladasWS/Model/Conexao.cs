using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace BaladasWS.Model
{
    public class Conexao
    {
        private SqlConnection conn = null;
        private SqlTransaction trans = null;

        private string ConnectionString
        {
            get {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.WorkstationID = "baladas.mssql.somee";
                builder.DataSource = "baladas.mssql.somee.com";
                builder.InitialCatalog = "baladas";
                builder.UserID = "fernandovilla_SQLLogin_1";
                builder.Password = "kc95gwqrnj";
                builder.PacketSize = 4096;
                builder.PersistSecurityInfo = false;
                
                return builder.ToString();
            }
        }

        public SqlConnection CurrentConnection
        {
            get { return this.conn; }
        }

        public SqlTransaction CurrentTransaction
        {
            get { return this.trans; }
        }

        public void Open()
        {
            if (this.conn == null)
            {
                this.conn = new SqlConnection(this.ConnectionString);
            }

            if (this.conn.State == System.Data.ConnectionState.Closed)
            {
                this.conn.Open();
            }
        }

        public void Close()
        {
            if (this.conn != null)
            {
                if (this.conn.State != System.Data.ConnectionState.Closed)
                {
                    if (this.trans != null)
                    {
                        this.RolbackTrans();
                    }

                    this.conn.Dispose();
                    this.conn.Dispose();
                    this.conn = null;
                }
            }
        }

        public void BeginTrans()
        {
            if (this.trans == null && (this.conn != null && this.conn.State != System.Data.ConnectionState.Closed))
            {
                this.trans = this.conn.BeginTransaction();
            }
        }

        public void CommitTrans()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                this.trans = null;
            }
        }

        public void RolbackTrans()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                this.trans = null;
            }
        }

        private Conexao()
        { }

        public static Conexao Create()
        {
            return new Conexao();
        }
    }
}
