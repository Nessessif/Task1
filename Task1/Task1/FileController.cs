using System;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;


namespace Task1
{
    static public class FileController
    {
        //Для подключение к БД необходимо заменить данные на свои
        private static MySqlConnection connection = new MySqlConnection("Server=localhost;Database=ey;Uid=root;Pwd=12345678;");
        private static int totalRows = 10000000;

        //Генерация данных для 100 файлов
        public static void Generate()
        {
            string path;
            StringBuilder stringBuilder = new StringBuilder();
            totalRows = 10000000;
            Console.WriteLine("Generating files..");
            for (int i = 1; i <= 100; i++)
            {
                stringBuilder.Clear();
                stringBuilder.Append("../files/");
                stringBuilder.Append(i);
                stringBuilder.Append(".txt");
                path = stringBuilder.ToString();
                using (StreamWriter sw = File.CreateText(path))
                {
                    //Генерация 100 000  строк для одного файла
                    for (int j = 0; j < 100000; j++)
                    {
                        stringBuilder.Clear();
                        stringBuilder.Append(DataGenerator.GenerateDate());
                        stringBuilder.Append("||");
                        stringBuilder.Append(DataGenerator.GenerateStringEN());
                        stringBuilder.Append("||");
                        stringBuilder.Append(DataGenerator.GenerateStringRU());
                        stringBuilder.Append("||");
                        stringBuilder.Append(DataGenerator.GenerateInt());
                        stringBuilder.Append("||");
                        stringBuilder.Append(DataGenerator.GenerateDouble());
                        stringBuilder.Append("||");
                        sw.WriteLine(stringBuilder);
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("Generated successfully!\n");
        }

        //Удаление выбранной строки в файлах
        public static void DeleteRows()
        {
            int deletedRows = 0;
            string tempFile = Path.GetTempFileName();
            string path;
            string subString;
            StringBuilder stringBuilder = new StringBuilder();
            Console.Write("Select substring to delete or press \"Enter\" if you don't want to delete rows: ");
            subString = Console.ReadLine();
            Console.Clear();
            if (!subString.Equals(""))
            {
                Console.WriteLine("Deleting rows..");
                for (int i = 1; i <= 100; i++)
                {
                    stringBuilder.Clear();
                    stringBuilder.Append("../files/");
                    stringBuilder.Append(i);
                    stringBuilder.Append(".txt");
                    path = stringBuilder.ToString();
                    string line;
                    using (StreamReader sr = new StreamReader(path))
                    using (StreamWriter sw = new StreamWriter(tempFile))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Contains(subString))
                                deletedRows++;
                            else
                                sw.WriteLine(line);
                        }
                    }
                    File.Delete(path);
                    File.Move(tempFile, path);
                }
                Console.Clear();
            }
            stringBuilder.Clear();
            stringBuilder.Append("Rows deleted: ");
            stringBuilder.Append(deletedRows);
            Console.WriteLine(stringBuilder);
            totalRows -= deletedRows;
        }

        //Группировка всех файлов в один с названием combine.txt
        public static void Combine()
        {
            string combinedFile = "../files/combine.txt";          
            string path;
            StringBuilder stringBuilder = new StringBuilder();
            StreamWriter sw =  File.CreateText(combinedFile);
            Console.WriteLine("Combining files..");
            using (sw)
                for (int i = 1; i <= 100; i++)
                {
                    stringBuilder.Clear();
                    stringBuilder.Append("../files/");
                    stringBuilder.Append(i);
                    stringBuilder.Append(".txt");
                    path = stringBuilder.ToString();
                    string line;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while ((line = sr.ReadLine()) != null)
                            sw.WriteLine(line);
                    }
                }
            Console.WriteLine("Combined successfully!\n");
        }

        //Импорт файлов в БД
        public static void Import()
        {
            int currentRow = 0;
            string path;
            StringBuilder stringBuilder = new StringBuilder();
            Console.WriteLine("Importing..");
            using (connection)
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                for (int i = 1; i <= 100; i++)
                {
                    
                    stringBuilder.Clear();
                    stringBuilder.Append("../files/").Append(i).Append(".txt");
                    path = stringBuilder.ToString();
                    string line;
                    using (StreamReader sr = new StreamReader(path))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            currentRow++;
                            string[] values = line.Split("||");
                            stringBuilder.Clear();
                            stringBuilder.Append("INSERT task1(string_date, string_en, string_ru, my_int, my_double) VALUES('");
                            stringBuilder.Append(values[0]);
                            stringBuilder.Append("','");
                            stringBuilder.Append(values[1]);
                            stringBuilder.Append("','");
                            stringBuilder.Append(values[2]);
                            stringBuilder.Append("',");
                            stringBuilder.Append(values[3]);
                            stringBuilder.Append(",");
                            stringBuilder.Append(values[4]);
                            stringBuilder.Append(")");
                            command.CommandText = stringBuilder.ToString();
                            command.ExecuteNonQuery();
                            stringBuilder.Clear();
                            stringBuilder.Append(currentRow);
                            stringBuilder.Append("/");
                            stringBuilder.Append(totalRows);
                            Console.WriteLine(stringBuilder);
                        }
                    }
                }
            }
            Console.WriteLine("Imported successfully!\n");
        }

        public static void Sum()
        {
            using (connection)
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "CALL SumInt()";
                using (MySqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine("Sum: " + reader.GetString(0));
            }
        }

        public static void Median()
        {
            using (connection)
            using (MySqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "CALL MedianDouble()";
                using (MySqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine("Median: " + reader.GetString(0) + "\n");
            }
        }
    }


   
   
}
