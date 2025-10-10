using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите приложение:");
        Console.WriteLine("1 - Калькулятор");
        Console.WriteLine("2 - Проверка пароля и PIN-кода");
        Console.Write("Введите 1 или 2: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                RunCalculator();
                break;
            case "2":
                RunPasswordCheck();
                break;
            default:
                Console.WriteLine("Неверный выбор! Пожалуйста, введите 1 или 2.");
                break;
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static void RunCalculator()
    {
        Console.WriteLine("\n=== КАЛЬКУЛЯТОР ===");

        Console.Write("Введите первое число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int b = Convert.ToInt32(Console.ReadLine());

        // Арифметические операции
        Console.WriteLine($"\nАрифметические операции:");
        Console.WriteLine($"{a} + {b} = {a + b}");
        Console.WriteLine($"{a} - {b} = {a - b}");
        Console.WriteLine($"{a} * {b} = {a * b}");
        Console.WriteLine($"{a} / {b} = {(double)a / b:F2}"); // Обратите внимание на преобразование типа!
        Console.WriteLine($"{a} % {b} = {a % b}");

        // Операторы сравнения
        Console.WriteLine($"\nРезультаты сравнения:");
        Console.WriteLine($"{a} == {b} : {a == b}");
        Console.WriteLine($"{a} != {b} : {a != b}");
        Console.WriteLine($"{a} > {b} : {a > b}");
        Console.WriteLine($"{a} < {b} : {a < b}");
        Console.WriteLine($"{a} >= {b} : {a >= b}");
        Console.WriteLine($"{a} <= {b} : {a <= b}");
    }

    static void RunPasswordCheck()
    {
        Console.WriteLine("\n=== ПРОВЕРКА ПАРОЛЯ И PIN-КОДА ===");

        string correctPassword = "hello123";
        int correctPin = 4567;

        Console.Write("Введите пароль: ");
        string userPassword = Console.ReadLine();

        Console.Write("Введите PIN-код: ");
        int userPin = Convert.ToInt32(Console.ReadLine());

        // Проверяем оба условия с помощью И (&&)
        bool isPasswordCorrect = (userPassword == correctPassword);
        bool isPinCorrect = (userPin == correctPin);

        bool canEnterSystem = isPasswordCorrect && isPinCorrect;

        Console.WriteLine($"\nПароль верный: {isPasswordCorrect}");
        Console.WriteLine($"PIN верный: {isPinCorrect}");
        Console.WriteLine($"Доступ в систему: {canEnterSystem}");

        // Демонстрация ИЛИ (||)
        bool canRecoverAccess = isPasswordCorrect || isPinCorrect;
        Console.WriteLine($"Можно восстановить доступ: {canRecoverAccess}");
    }
}