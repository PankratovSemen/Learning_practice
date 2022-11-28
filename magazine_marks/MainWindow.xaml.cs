using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace magazine_marks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string role;
        public string subject;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "SELECT role,subject FROM users WHERE login = @log AND password = @pass";
                    command.Parameters.Add("@log", DbType.String).Value = login_box.Text;
                    command.Parameters.Add("@pass", DbType.String).Value = password_box.Password ;
                    command.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    SQLiteDataAdapter adap = new SQLiteDataAdapter(command);

                    adap.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        log_in_.Visibility = Visibility.Hidden;
                        back_gr1.Visibility = Visibility.Hidden;
                        GUI.Visibility = Visibility.Visible;
                        SQLiteCommand command1 = new SQLiteCommand();
                        command1.Connection = conn;
                        command1.CommandText = "SELECT * FROM magazine_marks";
                        command1.ExecuteNonQuery();
                        DataTable dt1 = new DataTable();
                        SQLiteDataAdapter adap1 = new SQLiteDataAdapter(command1);
                        adap1.Fill(dt1);

                        middle_mar.ItemsSource = dt1.DefaultView;
                        role = Convert.ToString(dt.Rows[0].ItemArray[0]);
                        subject = Convert.ToString(dt.Rows[0].ItemArray[1]);
                        MessageBox.Show(role + subject);
                        conn.Close();
                    

                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("Пользователь не найден. Проверьте логин или пароль");
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {


           
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            db_middle_marks.Visibility = Visibility.Visible;
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            db_middle_marks.Visibility = Visibility.Hidden;
        }
    }
}
