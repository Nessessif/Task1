    using System;

namespace Task1
{
    public static class DataGenerator
    {
        static Random rnd = new Random();
        static DateTime endDate = DateTime.Now;

        //Генерация случайной даты за последние 5 лет
        public static string GenerateDate()
        {
            DateTime startDate = endDate.AddYears(-5);
            int randomYear = rnd.Next(startDate.Year, endDate.Year);
            int randomMonth = rnd.Next(1, 12);
            int randomDay = rnd.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));
            if (randomYear == startDate.Year)
            {
                randomMonth = rnd.Next(startDate.Month, 12);
                if (randomMonth == startDate.Month)
                    randomDay = rnd.Next(startDate.Day, DateTime.DaysInMonth(randomYear, randomMonth));
            }
            if (randomYear == endDate.Year)
            {
                randomMonth = rnd.Next(1, endDate.Month);
                if (randomMonth == endDate.Month)
                    randomDay = rnd.Next(1, endDate.Day);
            }
            DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);
            return randomDate.Date.ToString("dd.MM.yyyy");
        }

        //Генерация случайной строки с 10 русскими символами
        public static string GenerateStringRU()
        {
            const string ru = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            char[] stringChars = new char[10];
            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = ru[rnd.Next(ru.Length)];
            return new string(stringChars);
        }

        //Генерация случайной строки с 10 латинскими символами
        public static string GenerateStringEN()
        {
            const string en = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            char[] stringChars = new char[10];
            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = en[rnd.Next(en.Length)];
            return new string(stringChars);
        }

        //Генерация случайного положительного четного целочисленного числа в диапазоне от 1 до 100 000 000
        public static string GenerateInt()
        {
            return rnd.Next(1, 100000000).ToString();
        }

        //Генерация случайного положительного числа с 8 знаками после запятой в диапазоне от 1 до 20
        public static string GenerateDouble()
        {
            return Math.Round((1 + rnd.NextDouble() * 19), 8).ToString();
        }
    }
}
