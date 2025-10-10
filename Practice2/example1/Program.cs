using System;

namespace example1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать в кофейню!");

            // 1. ЗАПРОС ИМЕНИ КЛИЕНТА
            Console.Write("\nКак ваше имя? ");
            string customerName = Console.ReadLine();

            // 2. ОПРЕДЕЛЕНИЕ КОНСТАНТ - ЦЕН И НАЛОГА
            const double CoffeePrice = 250.0;
            const double TeaPrice = 150.0;
            const double CroissantPrice = 120.50;
            const double CakePrice = 200.75;
            const double Nalog = 0.2;

            // 3. ОБЪЯВЛЕНИЕ ПЕРЕМЕННЫХ ДЛЯ ХРАНЕНИЯ КОЛИЧЕСТВА ТОВАРОВ
            byte coffeeCups = 0;
            byte teaCups = 0;
            sbyte croissants = 0;
            short cakes = 0;

            // 4. ПРОСТОЙ ВВОД КОЛИЧЕСТВА ТОВАРОВ
            Console.WriteLine("\n=== Оформление заказа ===");

            Console.Write("Сколько чашек кофе? ");
            coffeeCups = Convert.ToByte(Console.ReadLine());

            Console.Write("Сколько чашек чая? ");
            teaCups = Convert.ToByte(Console.ReadLine());

            Console.Write("Сколько круассанов? ");
            croissants = Convert.ToSByte(Console.ReadLine());

            Console.Write("Сколько кусочков торта? ");
            cakes = Convert.ToInt16(Console.ReadLine());

            // 5. ВЫЧИСЛЕНИЕ СТОИМОСТИ ЗАКАЗА
            double coffeeTotal = coffeeCups * CoffeePrice;
            double teaTotal = teaCups * TeaPrice;
            double croissantTotal = croissants * CroissantPrice;
            double cakeTotal = cakes * CakePrice;

            double subtotal = coffeeTotal + teaTotal + croissantTotal + cakeTotal;
            double taxAmount = subtotal * Nalog;
            double total = subtotal + taxAmount;

            // 6. ДОПОЛНИТЕЛЬНЫЕ ДАННЫЕ
            DateTime orderTime = DateTime.Now;

            // 7. ВЫВОД ИНФОРМАЦИИ О ЗАКАЗЕ (ТОЛЬКО ЗАКАЗАННЫЕ ТОВАРЫ)
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("           ВАШ ЗАКАЗ");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"Время: {orderTime:HH:mm:ss}");
            Console.WriteLine($"Клиент: {customerName}");
            Console.WriteLine(new string('-', 40));

            // Выводим только те товары, которые были заказаны (количество > 0)
            if (coffeeCups > 0)
                Console.WriteLine($"Кофе: {coffeeCups} шт. - {coffeeTotal:F2} руб");

            if (teaCups > 0)
                Console.WriteLine($"Чай: {teaCups} шт. - {teaTotal:F2} руб");

            if (croissants > 0)
                Console.WriteLine($"Круассаны: {croissants} шт. - {croissantTotal:F2} руб");

            if (cakes > 0)
                Console.WriteLine($"Торт: {cakes} шт. - {cakeTotal:F2} руб");

            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Промежуточный итог: {subtotal:F2} руб");
            Console.WriteLine($"НДС (20%): {taxAmount:F2} руб");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"ИТОГО К ОПЛАТЕ: {total:F2} руб");
            Console.WriteLine(new string('=', 40));

            // 8. ПРОЦЕСС ОПЛАТЫ С ПРОВЕРКОЙ
            double payment = 0;
            double remainingAmount = total;

            Console.WriteLine("\n=== ОПЛАТА ===");

            // Цикл продолжается, пока не внесена вся сумма
            while (remainingAmount > 0)
            {
                Console.WriteLine($"Осталось внести: {remainingAmount:F2} руб");
                Console.Write("Введите сумму для оплаты: ");
                double currentPayment = Convert.ToDouble(Console.ReadLine());

                payment += currentPayment;
                remainingAmount = total - payment;

                if (remainingAmount > 0)
                {
                    Console.WriteLine($"Внесено: {payment:F2} руб");
                }
            }

            // 9. РАСЧЕТ СДАЧИ
            double change = payment - total;

            // 10. ФИНАЛЬНЫЙ ЧЕК
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("           ЧЕК ОПЛАТЫ");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine($"Общая сумма: {total:F2} руб");
            Console.WriteLine($"Внесено: {payment:F2} руб");

            if (change > 0)
            {
                Console.WriteLine($"Сдача: {change:F2} руб");
            }

            Console.WriteLine(new string('=', 40));

            // 11. ПРОЩАНИЕ
            Console.WriteLine("\nСпасибо за заказ! Приходите ещё!");
            Console.ReadLine();
        }
    }
}