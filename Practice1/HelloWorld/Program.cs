using System;

namespace HelloWorld
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");
            Console.ReadLine();
            Console.WriteLine("Как тебя зовут?");
            string name = Console.ReadLine();
            Console.WriteLine("Привет, " + name + "!");
            Console.WriteLine("Сколько тебе лет?");

            int age = 0;
            bool isAgeCorrect = false;

            while (!isAgeCorrect)
            {
                try
                {
                    age = Convert.ToInt32(Console.ReadLine());
                    isAgeCorrect = true;
                }
                catch
                {
                    Console.WriteLine("Вводите цифры, а не буквы");
                }
            }

            if (age < 18)
            {
                Console.WriteLine("Взрослая жизнь еще впереди");
            }
            else
            {
                Console.WriteLine("А ты уже смешарик");
            }

            //играаа

            Console.WriteLine("А теперь играем в игру. У меня есть случайное число от 1 до 10, попробуй угадай");
            Random rnd = new Random();
            int secretNumber = rnd.Next(1, 11);
            int user = 0;
            while (user != secretNumber)
            {
                Console.Write("Твой вариант: ");
                user = Convert.ToInt32(Console.ReadLine());
                if (user < secretNumber)
                {
                    Console.WriteLine("А мое число больше");
                }
                else if (user > secretNumber)
                {
                    Console.WriteLine("А мое число меньше");
                }
                else
                {
                    Console.WriteLine("Ты угадал! Это действительно " + secretNumber);
                }
            }

            Console.ReadLine();
        }
    }
}
