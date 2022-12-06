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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp;
using ClosedXML;
using ClosedXML.Excel;

namespace magazine_marks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial  class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string role;
        
        public string user;
        public string logins;
        public string passwords;
        


        //Кнопка входа проверка логина и пароля

        Jonner j = new Jonner();
        public void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if(j.logining(login_box.Text, password_box.Password) == "OK")
            {
                j.logining(login_box.Text, password_box.Password);
                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                conn.Open();
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
                
                MessageBox.Show(j.roles() + " " + j.subjects());
                conn.Close();
                username_text.Content = j.users() + " ( " + j.subjects() + " ) ";
                if (j.roles() == "Администратор")
                {
                    cr_students.Visibility = Visibility.Visible;
                    
                }
                else
                {
                    cr_students.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //Выводить таблицу с средними баллами когда при активной форме
            SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
            conn.Open();
            SQLiteCommand command1 = new SQLiteCommand();
            command1.Connection = conn;
            command1.CommandText = "SELECT * FROM magazine_marks";
            command1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SQLiteDataAdapter adap1 = new SQLiteDataAdapter(command1);
            adap1.Fill(dt1);

            middle_mar.ItemsSource = dt1.DefaultView;
            conn.Close();

            if (mark_text.Text == "1")
            {
                mark_text.Text = "1";

            }
            if (mark_text.Text == "2")
            {
                mark_text.Text = "2";
            }
            if (mark_text.Text == "3")
            {
                mark_text.Text = "3";
            }
            if (mark_text.Text == "4")
            {
                mark_text.Text = "4";
            }
            if (mark_text.Text == "5")
            {
                mark_text.Text = "5";
            }

            else
            {
                mark_text.Clear();
            }

            if (j.roles() == "Администратор")
            {
                cr_students.Visibility = Visibility.Visible;
               
                

            }
            else
            {
                cr_students.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {

            SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
            conn.Open();
            SQLiteCommand command1 = new SQLiteCommand();
            command1.Connection = conn;
            command1.CommandText = "SELECT * FROM magazine_marks";
            command1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SQLiteDataAdapter adap1 = new SQLiteDataAdapter(command1);
            adap1.Fill(dt1);

            middle_mar.ItemsSource = dt1.DefaultView;
            conn.Close();



            conn.Open();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = conn;
            command.CommandText = "SELECT name || ' ' || surname FROM students";
            command.ExecuteNonQuery();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Students_combo.ItemsSource = ds.Tables[0].DefaultView;
            Students_combo.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
            Students_combo.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

            Search_students_marks.ItemsSource = ds.Tables[0].DefaultView;
            Search_students_marks.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
            Search_students_marks.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

            conn.Close();

            if (j.roles() == "Администратор")
            {
                cr_students.Visibility = Visibility.Visible;

            }
            else
            {
                cr_students.Visibility = Visibility.Hidden;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {



        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            db_middle_marks.Visibility = Visibility.Visible;
            marks_panel.Visibility = Visibility.Hidden;
            students_panel.Visibility = Visibility.Hidden;

            SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
            conn.Open();
            SQLiteCommand command1 = new SQLiteCommand();
            command1.Connection = conn;
            command1.CommandText = "SELECT * FROM magazine_marks";
            command1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SQLiteDataAdapter adap1 = new SQLiteDataAdapter(command1);
            adap1.Fill(dt1);

            middle_mar.ItemsSource = dt1.DefaultView;
            conn.Close();
        }

        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
           
            if(j.roled != "Преподаватель")
            {
                MessageBox.Show("У вас нет прав доступа к этому компоненту");
            }

            else
            {
                db_middle_marks.Visibility = Visibility.Hidden;
                marks_panel.Visibility = Visibility.Visible;
                students_panel.Visibility = Visibility.Hidden;
            }
            //Заполнение таблицы db_marks базой данных


        }

        private void Win1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                conn.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = conn;
                command.CommandText = "SELECT name || ' ' || surname FROM students";
                command.ExecuteNonQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                Students_combo.ItemsSource = ds.Tables[0].DefaultView;
                Students_combo.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
                Students_combo.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

                Search_students_marks.ItemsSource = ds.Tables[0].DefaultView;
                Search_students_marks.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
                Search_students_marks.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

                conn.Close();


                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int st_fail = 0;
            try
            {
                

                string mark = mark_text.Text;
                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                conn.Open();
                if (mark == "5")
                {
                    
                    
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO marks(subject,id_student,mark) VALUES (@s,@is,@m)";
                    command.Parameters.AddWithValue("@s", j.subjects());

                    command.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command.Parameters.AddWithValue("@m", mark);
                    command.ExecuteNonQuery();
                }
                else if (mark == "4")
                {

                   
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO marks(subject,id_student,mark) VALUES (@s,@is,@m)";
                    command.Parameters.AddWithValue("@s", j.subjects());

                    command.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command.Parameters.AddWithValue("@m", mark);
                    command.ExecuteNonQuery();
                }
                else if (mark == "3")
                {

                    
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO marks(subject,id_student,mark) VALUES (@s,@is,@m)";
                    command.Parameters.AddWithValue("@s", j.subjects());

                    command.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command.Parameters.AddWithValue("@m", mark);
                    command.ExecuteNonQuery();
                }
                else if (mark == "2")
                {

                    
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO marks(subject,id_student,mark) VALUES (@s,@is,@m)";
                    command.Parameters.AddWithValue("@s", j.subjects());

                    command.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command.Parameters.AddWithValue("@m", mark);
                    command.ExecuteNonQuery();
                }
               else if (mark == "1")
                {

                    
                    SQLiteCommand command = new SQLiteCommand();
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO marks(subject,id_student,mark) VALUES (@s,@is,@m)";
                    command.Parameters.AddWithValue("@s", j.subjects());

                    command.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command.Parameters.AddWithValue("@m", mark);
                    command.ExecuteNonQuery();
                }
                //if (mark_text.Text != "1" && mark_text.Text != "2" && mark_text.Text != "3" && mark_text.Text != "4" && mark_text.Text != "5")
                else
                {
                    st_fail++;
                    mark_text.Text = " ";
                    MessageBox.Show("Внимание не корректное значение");
                }


                SQLiteCommand command1 = new SQLiteCommand();
                command1.Connection = conn;
                command1.CommandText = "SELECT avg(mark) FROM marks WHERE id_student =@studentval AND subject = @sub";
                command1.Parameters.AddWithValue("@studentval", Students_combo.Text.ToString());
                command1.Parameters.AddWithValue("@sub", j.subjects());


                command1.ExecuteNonQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command1);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "marks");
                DataRow row = ds.Tables[0].Rows[0];
               

                SQLiteCommand command2 = new SQLiteCommand();
                command2.Connection = conn;
                if (j.subjects() == "Физика")
                {
                    command2.CommandText = "UPDATE magazine_marks SET middle_marks2 = @mm WHERE id_student = @is";
                    command2.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command2.Parameters.AddWithValue("@mm", Math.Round(Convert.ToDouble(row[0]),2));
                }
                else if (j.subjects() == "Высшая математика")
                {
                    command2.CommandText = "UPDATE magazine_marks SET middle_marks1 = @mm WHERE id_student = @is";
                    command2.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command2.Parameters.AddWithValue("@mm", Math.Round(Convert.ToDouble(row[0]),2));
                }

                else if (j.subjects() == "История России")
                {
                    command2.CommandText = "UPDATE magazine_marks SET middle_marks3 = @mm WHERE id_student = @is";
                    command2.Parameters.AddWithValue("@is", Students_combo.Text.ToString());
                    command2.Parameters.AddWithValue("@mm", Math.Round(Convert.ToDouble(row[0]),2));
                }


                command2.ExecuteNonQuery();
                if(st_fail== 0)
                {
                    MessageBox.Show("OK");
                }
                
                conn.Close();

               


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка возможно у вас нет необходимых прав для данной операции " + ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string students = Search_students_marks.Text;
            string subjects = Search_subject_marks1.Text;
            SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
            conn.Open();
            SQLiteCommand command = new SQLiteCommand();

            command.Connection = conn;
            command.CommandText = "SELECT * FROM marks WHERE subject = @sub AND id_student = @is";
            command.Parameters.AddWithValue("@sub", subjects);
            command.Parameters.AddWithValue("@is", students);
            command.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SQLiteDataAdapter adap = new SQLiteDataAdapter(command);
            adap.Fill(dt);
            
            db_marks.ItemsSource = dt.DefaultView;
        }

        private void btn_Print_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Print_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var wb = new XLWorkbook();
                var sh = wb.Worksheets.Add("Export");
                for (int j = 0; j < middle_mar.Columns.Count; j++)
                {


                    sh.Cell(1, j + 1).SetValue(middle_mar.Columns[j].Header);
                    sh.Cell(1, j + 1).Style.Font.Bold = true;
                    sh.Columns().AdjustToContents();
                    sh.Rows().AdjustToContents();
                }

                int q = 1;
                for (int i = 0; i < middle_mar.Columns.Count; i++)
                {
                   


                    for (int j = 0; j < middle_mar.Items.Count; j++)
                    {
                        DataRowView row = middle_mar.Items[j] as DataRowView;
                        if(row != null)
                            sh.Cell(j + 2, i + 1).SetValue(row[i+1].ToString());








                    }

                }
                wb.SaveAs("First.xlsx");
            }
            catch(Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }

        private void mark_text_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(mark_text.Text == "1")
            {
                mark_text.Text = "1";

            }
            if(mark_text.Text == "2")
            {
                mark_text.Text = "2";
            }
            if (mark_text.Text == "3")
            {
                mark_text.Text = "3";
            }
            if (mark_text.Text == "4")
            {
                mark_text.Text = "4";
            }
            if (mark_text.Text == "5")
            {
                mark_text.Text = "5";
            }

            else
            {
                mark_text.Text = " ";
            }






        }

        private void mark_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (mark_text.Text == "1")
            {
                mark_text.Text = "1";

            }
            if (mark_text.Text == "2")
            {
                mark_text.Text = "2";
            }
            if (mark_text.Text == "3")
            {
                mark_text.Text = "3";
            }
            if (mark_text.Text == "4")
            {
                mark_text.Text = "4";
            }
            if (mark_text.Text == "5")
            {
                mark_text.Text = "5";
            }

            else if (mark_text.Text != null)
            {
                mark_text.Text = "";
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (j.roles() == "Администратор")
            {
                db_middle_marks.Visibility = Visibility.Hidden;
                marks_panel.Visibility = Visibility.Hidden;
                students_panel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("У вас нет прав доступа к этому разделу");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                string surn = surname_box.Text;
                string names = name_box.Text;
                string group = group_box.Text;

                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                conn.Open();
                string com = "INSERT INTO students (surname,name,st_group) VALUES (@sn,@n,@gr)";
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = com;
                cmd.Parameters.AddWithValue("@sn", surn);
                cmd.Parameters.AddWithValue("@n", names);
                cmd.Parameters.AddWithValue("@gr", group);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                SQLiteCommand command = new SQLiteCommand();
                string coms = "INSERT INTO magazine_marks(id_student) VALUES (@is)";
                command.Connection = conn;
                command.CommandText = coms;
                command.Parameters.AddWithValue("@is", names + " " + surn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Win1_Activated(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source = mm.db; Version = 3");
                conn.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = conn;
                command.CommandText = "SELECT name || ' ' || surname FROM students";
                command.ExecuteNonQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                Students_combo.ItemsSource = ds.Tables[0].DefaultView;
                Students_combo.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
                Students_combo.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

                Search_students_marks.ItemsSource = ds.Tables[0].DefaultView;
                Search_students_marks.DisplayMemberPath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();
                Search_students_marks.SelectedValuePath = ds.Tables[0].Columns["name || ' ' || surname"].ToString();

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

        private void surname_box_PreviewTextInput(object sender, TextCompositionEventArgs e)//Ограничение ввода. Активен ввод только букв
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }

            
        }

        private void name_box_PreviewTextInput(object sender, TextCompositionEventArgs e) //Ограничение ввода. Активен ввод только букв
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }
    }
    }


    