using System;

namespace Task1
{
    class Programm
    {
        public static void Main(string[] args)
        {
            //Производит замену запятой на точку в дробных числах, это необходимо для более удобной работой с БД
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            while (true)
            {
                switch (GetKey())
                {
                    case '1':
                        {
                            Console.Clear();
                            FileController.Generate();
                            break;
                        }
                    case '2':
                        {
                            Console.Clear();
                            FileController.DeleteRows();
                            FileController.Combine();
                            break;
                        }
                    case '3':
                        {
                            Console.Clear();
                            FileController.Import();
                            break;
                        }
                    case '4':
                        {
                            FileController.Sum();
                            FileController.Median();
                            break;
                        }
                    case '5':
                        {
                            return;
                        }
                }
            }
        }

        //Вывод главного меню на экран и считывание ввода пользователя
        private static char GetKey()
        {
            char key;
            Console.WriteLine("1 - Generate Files\n2 - Combine Files and Delete Rows\n3 - Import into Database\n4 - Calculate Sum and Median\n5 - Exit\n");
            key = Console.ReadKey().KeyChar;
            Console.Clear();
            return key;
        }
    }
}
