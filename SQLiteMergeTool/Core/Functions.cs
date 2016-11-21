using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Functions
    {
        public delegate void InsertProgressEventHandler(object sender,
                                                  InsertProgressEventArgs e);

        public event InsertProgressEventHandler InsertProgress;

        protected virtual void OnInsertProgress(InsertProgressEventArgs e)
        {
            if (InsertProgress != null)
                InsertProgress(this, e);
        }

        public SQLiteConnection CreateConnection(string path)
        {
            return new SQLiteConnection(String.Format("Data Source={0};Version=3;", path));
        }

        public List<String> LoadDataBaseTables(String path)
        {
            SQLiteConnection cn;
            SQLiteDataAdapter db;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            List<String> tables = new List<string>();
            
            cn = CreateConnection(path);
            cn.Open();

            string sql = "SELECT name FROM sqlite_master WHERE type='table';";

            SQLiteCommand command = new SQLiteCommand(sql, cn);

            db = new SQLiteDataAdapter(sql, cn);
            ds.Reset();
            db.Fill(ds);
            dt = ds.Tables[0];

            foreach(System.Data.DataRow row in dt.Rows)
            {
                tables.Add(row.ItemArray[0].ToString());
            }

            return tables;
        }
        
        public bool TransferTable(string originPath, string destinationPath, string table)
        {
            try
            {
                SQLiteConnection cn1;
                SQLiteDataAdapter db1;
                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();

                SQLiteConnection cn2;
                
                cn1 = CreateConnection(originPath);
                cn1.Open();

                string sql = $"SELECT * FROM {table}";

                db1 = new SQLiteDataAdapter(sql, cn1);
                ds1.Reset();
                db1.Fill(ds1);
                dt1 = ds1.Tables[0];

                string colums = "";

                List<string> values = new List<string>();

                foreach (var x in dt1.Columns)
                {
                    colums += $"{x},";
                }

                colums = colums.Substring(0, colums.Length - 1);

                int col = 0;
                for (int i =0; i < dt1.Rows.Count; i++)
                {
                    string val = "";
                    foreach (var x in dt1.Rows[i].ItemArray)
                    {
                        if (x.GetType() != typeof(System.DBNull))
                        {
                            var tipo = dt1.Columns[col].DataType.FullName;
                            if (tipo == "System.String") 
                                val += $"'{x.ToString().Replace("'","''")}',";
                            else if (tipo == "System.DateTime")
                                val += $"'{((DateTime)x).ToString("yyyy-MM-dd HH:mm:ss")}',";
                            else if(tipo == "System.Boolean")
                                val += $"{(x.ToString() == "True" ? 1 : 0)},";
                            else if (tipo == "System.Decimal")
                                val += $"{x.ToString().Replace(",",".")},";
                            else
                                val += $"{x},";
                        }
                        else
                            val += "NULL,";

                        col++;
                    }

                    col = 0;
                    val = val.Substring(0, val.Length - 1);

                    values.Add(val);
                }

                cn1.Close();

                cn2 = CreateConnection(destinationPath);
                cn2.Open();

                SQLiteCommand command = new SQLiteCommand();
                command.Connection = cn2;

                command.CommandText = "PRAGMA foreign_keys=OFF;";
                command.ExecuteNonQuery();

                command.CommandText = $"DELETE FROM {table}";

                command.ExecuteNonQuery();

                int progress = 0;
                int total = values.Count;
                foreach (var v in values)
                {
                    try
                    {
                        command.CommandText = $"INSERT INTO {table} ({colums}) VALUES ({v})";
                        command.ExecuteNonQuery();
                        progress++;

                        OnInsertProgress(new Core.InsertProgressEventArgs(progress, total));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                command.CommandText = "PRAGMA foreign_keys=ON;";
                command.ExecuteNonQuery();

                cn2.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public String CreateNewDataBase()
        {
            return "";
        }
    }
}
