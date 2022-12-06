using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using magazine_marks;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace magazine_marks
{
    public class Jonner
    {

        public string roled;
        public string subj;
        public string us;

        
        public string logining(string login, string password)
        {

            string status = "0";
            try
            {

                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");

                conn.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = conn;
                command.CommandText = "SELECT role,subject,surname ||' '|| name FROM users WHERE login = @log AND password = @pass";
                command.Parameters.Add("@log", DbType.String).Value = login;
                command.Parameters.Add("@pass", DbType.String).Value = password;
                command.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SQLiteDataAdapter adap = new SQLiteDataAdapter(command);

                adap.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                   
                    status = "OK";
                    roled = Convert.ToString(dt.Rows[0].ItemArray[0]);
                   subj = Convert.ToString(dt.Rows[0].ItemArray[1]);
                    us = Convert.ToString(dt.Rows[0].ItemArray[2]);

                }
                else
                {
                    conn.Close();
                    MessageBox.Show("Пользователь не найден. Проверьте логин или пароль");
                    status = "NO";

                }


                return status;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return status;
            }
        }

        public string roles()
        {
            return roled;
        }

        public string subjects()
        {
            return subj;
        }

        public string users()
        {
            return us;
        }

       public string GetVal(string Y)
        {
            return Y;
        }
    }
}
