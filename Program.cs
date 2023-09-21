using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronic_Journal
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = true;
            string connectionString = "Data Source=Electronic_Journal.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                while (exit)
                {
                    Console.WriteLine("-----ДОБРО ПОЖАЛОВАТЬ В ЭЛЕКТРОННЫЙ ЖУРНАЛ!-----\n\n");
                    Console.WriteLine("-----Для управления введите цифру в соответствующее поле-----\n\n\n");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("+                                                 +");
                    Console.WriteLine("+              1 - Добавить студента              +");
                    Console.WriteLine("+              2 - Изменить студента              +");
                    Console.WriteLine("+              3 - Удалить студента               +");
                    Console.WriteLine("+ 4 - Вывести всех студентов в алфавитном порядке +");
                    Console.WriteLine("+              0 - Выход из журнала               +");
                    Console.WriteLine("+                                                 +");
                    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++\n\n");
                    Console.WriteLine("Введите цифру: \n");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Console.WriteLine("\n---Введите, пожалуйста, номер студенческого билета студента, которого вы хотите добавить в журнал---\n");
                                int student_number = Convert.ToInt32(Console.ReadLine());
                                if (student_number < 1)
                                {
                                    Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                                    break;
                                }
                                Console.WriteLine("\n---Введите, пожалуйста, фамилию студента--- \n");
                                string surname = Console.ReadLine();
                                Console.WriteLine("\n---Введите, пожалуйста, имя студента--- \n");
                                string name = Console.ReadLine();
                                Console.WriteLine("\n---Введите, пожалуйста, отчество студента--- \n");
                                string middle_name = Console.ReadLine();
                                Console.WriteLine("\n-------------Студент успешно добавлен в журнал-----------------\n\n");
                                SqliteCommand add_command = new SqliteCommand("INSERT INTO Students VALUES (@student_number,@surname,@name,@middle_name)", connection);
                                SqliteParameter n1 = new SqliteParameter("@student_number", student_number);
                                add_command.Parameters.Add(n1);
                                SqliteParameter n2 = new SqliteParameter("@surname", surname);
                                add_command.Parameters.Add(n2);
                                SqliteParameter n3 = new SqliteParameter("@name", name);
                                add_command.Parameters.Add(n3);
                                SqliteParameter n4 = new SqliteParameter("@middle_name", middle_name);
                                add_command.Parameters.Add(n4);
                                add_command.ExecuteScalar();
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                            }
                            catch (SqliteException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Студент с введёнными данными уже существует\n\n**************************************\n\n");
                            }
                            catch (OverflowException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Слишком длинный номер студенческого билета\n\n**************************************\n\n");
                            }

                            break;
                        case 4:
                            
                            SqliteCommand select_command = new SqliteCommand("SELECT * FROM Students ORDER BY surname, name", connection);
                            using (SqliteDataReader dataReader = select_command.ExecuteReader())
                            {
                                string s1 = dataReader.GetName(0);
                                string s2 = dataReader.GetName(1);
                                string s3 = dataReader.GetName(2);
                                string s4 = dataReader.GetName(3);
                                Console.WriteLine("\n---Список студентов---\n");
                                Console.WriteLine($"{s1}\t{s2}\t{s3}\t{s4}\t");
                                Console.WriteLine("--------------------------------------------------------------");
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    { 
                                        int student_number = dataReader.GetInt32(0);
                                        string surname = dataReader.GetString(1);
                                        string name = dataReader.GetString(2);
                                        string middle_name = dataReader.GetString(3);
                                        Console.WriteLine($"{student_number}\t{surname}\t{name}\t{middle_name}");
                                    }
                                    Console.WriteLine("\n---------------------------------------------------------------\n\n");
                                }
                            }
                            break;
                        case 2:
                            try
                            {
                                Console.WriteLine("\n---Введите, пожалуйста, номер студенческого билета студента, которого вы хотите изменить---\n");
                                int update_num = Convert.ToInt32(Console.ReadLine());
                                if (update_num < 1)
                                {
                                    Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                                    break;
                                }
                                SqliteCommand update_command = new SqliteCommand("SELECT * FROM Students WHERE student_number = @update_num", connection);
                                SqliteParameter n6 = new SqliteParameter("@update_num", update_num);
                                update_command.Parameters.Add(n6);
                                update_command.ExecuteScalar();
                            

                            using (SqliteDataReader dataReader = update_command.ExecuteReader())
                            {
                                string s5 = dataReader.GetName(0);
                                string s6 = dataReader.GetName(1);
                                string s7 = dataReader.GetName(2);
                                string s8 = dataReader.GetName(3);
                                Console.WriteLine("\n---Студент, которого вы хотите изменить:---\n");
                                
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    { 
                                        int update_student = dataReader.GetInt32(0);
                                        string update_surname = dataReader.GetString(1);
                                        string update_name = dataReader.GetString(2);
                                        string update_middle_name = dataReader.GetString(3);
                                        Console.WriteLine($"{update_student}\t{update_surname}\t{update_name}\t{update_middle_name}");
                                    }
                                    Console.WriteLine("---------------------------------------------------------------\n\n");
                                }
                            }
                            
                            
                            Console.WriteLine("\n---Введите, пожалуйста, новый студенческий номер--- \n");
                                int u_student_number = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("\n---Введите, пожалуйста, новую фамилию студента--- \n");
                                string u_surname = Console.ReadLine();
                            Console.WriteLine("\n---Введите, пожалуйста, новое имя студента--- \n");
                                string u_name = Console.ReadLine();
                            Console.WriteLine("\n---Введите, пожалуйста, новое отчество студента--- \n");
                                string u_middle_name = Console.ReadLine();
                            SqliteCommand mega_update_command = new SqliteCommand("UPDATE Students SET student_number=@u_student_number, surname=@u_surname, name=@u_name, middle_name=@u_middle_name WHERE student_number=@update_num", connection);
                                SqliteParameter n11 = new SqliteParameter("@update_num", update_num);
                                mega_update_command.Parameters.Add(n11);
                                SqliteParameter n7 = new SqliteParameter("@u_student_number", u_student_number);
                                mega_update_command.Parameters.Add(n7);
                                SqliteParameter n8 = new SqliteParameter("@u_surname", u_surname);
                                mega_update_command.Parameters.Add(n8);
                                SqliteParameter n9 = new SqliteParameter("@u_name", u_name);
                                mega_update_command.Parameters.Add(n9);
                                SqliteParameter n10 = new SqliteParameter("@u_middle_name", u_middle_name);
                                mega_update_command.Parameters.Add(n10);
                                object p = mega_update_command.ExecuteScalar();
                                mega_update_command.ExecuteScalar();
                                Console.WriteLine("\n-------------Студент успешно изменён-----------------\n\n");
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                            }
                            catch (SqliteException e)
                            {
                                Console.WriteLine(e);
                            }
                            catch (OverflowException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Слишком длинный номер студенческого билета\n\n**************************************\n\n");
                            }
                            break;
                        case 3:
                            try
                            {
                                Console.WriteLine("\n---Введите, пожалуйста, номер студенческого билета студента, которого вы хотите удалить из журнала---\n");
                                int num = Convert.ToInt32(Console.ReadLine());
                                if (num < 1)
                                {
                                    Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                                    break;
                                }

                                SqliteCommand add_command = new SqliteCommand("DELETE FROM Students WHERE student_number = @num", connection);
                                SqliteParameter n5 = new SqliteParameter("@num", num);
                                add_command.Parameters.Add(n5);
                                add_command.ExecuteScalar();
                                Console.WriteLine("\n-------------Студент успешно удалён из журнала-----------------\n\n");
                                
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Неккоректно введён номер студенческого билета\n\n**************************************\n\n");
                            }
                            catch (OverflowException e)
                            {
                                Console.WriteLine("\n**************************************\n\nОШИБКА! Слишком длинный номер студенческого билета\n\n**************************************\n\n");
                            }

                            break;
                        case 0:
                            exit = false;
                            break;
                        default:
                            exit = false;
                            break;
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}