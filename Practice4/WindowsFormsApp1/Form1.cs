using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboCoffee.Items.Add("Эспрессо - 200 руб");
            comboCoffee.Items.Add("Капучино - 250 руб");
            comboCoffee.Items.Add("Латте - 280 руб");
            comboCoffee.Items.Add("Американо - 220 руб");

            comboCoffee.SelectedIndexChanged += (s, e) => CalculateTotal();
            txtQuantity.TextChanged += (s, e) => CalculateTotal();
            chkSugar.CheckedChanged += (s, e) => CalculateTotal();
            chkMilk.CheckedChanged += (s, e) => CalculateTotal();
            chkCream.CheckedChanged += (s, e) => CalculateTotal();
        }
        private int GetCoffeePrice()
        {
            if (comboCoffee.SelectedItem == null) return 0;

            string selected = comboCoffee.SelectedItem.ToString();

            if (selected.Contains("Эспрессо")) return 200;
            if (selected.Contains("Капучино")) return 250;
            if (selected.Contains("Латте")) return 280;
            if (selected.Contains("Американо")) return 220;

            return 0;
        }
        private void CalculateTotal()
        {
            // Проверяем, выбран ли кофе
            if (comboCoffee.SelectedItem == null)
            {
                lblTotal.Text = "Итого: 0 руб";
                return;
            }

            // Получаем базовую цену кофе
            int basePrice = GetCoffeePrice();

            // Рассчитываем стоимость добавок
            int additions = 0;

            // Добавки
            if (chkSugar.Checked) additions += 10;
            if (chkMilk.Checked) additions += 20;
            if (chkCream.Checked) additions += 30;

            // Рассчитываем общую стоимость
            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
            {
                int total = (basePrice + additions) * quantity;
                lblTotal.Text = $"Итого: {total} руб";
            }
            else
            {
                lblTotal.Text = "Итого: 0 руб";
            }
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // Проверяем выбор кофе
            if (comboCoffee.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип кофе!", "Внимание");
                return;
            }

            // Проверяем количество
            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Введите правильное количество!", "Ошибка");
                return;
            }

            // Получаем данные о заказе
            string selectedCoffee = comboCoffee.SelectedItem.ToString();
            int price = 0;
            string coffeeName = "";

            if (selectedCoffee.Contains("Эспрессо")) { price = 200; coffeeName = "Эспрессо"; }
            else if (selectedCoffee.Contains("Капучино")) { price = 250; coffeeName = "Капучино"; }
            else if (selectedCoffee.Contains("Латте")) { price = 280; coffeeName = "Латте"; }
            else if (selectedCoffee.Contains("Американо")) { price = 220; coffeeName = "Американо"; }

            int total = price * quantity;

            // Обновляем итог на главной форме
            lblTotal.Text = $"Итого: {total} руб";

            // Создаем детализированный чек
            string receiptText = GenerateReceipt(coffeeName, price, quantity, total);

            // Показываем чек в новом окне
            ReceiptForm receiptForm = new ReceiptForm(receiptText);
            receiptForm.Show();
        }

        private string GenerateReceipt(string coffeeName, int price, int quantity, int total)
        {
            return $@"=== КОФЕЙНЯ ===

ДЕТАЛИ ЗАКАЗА:
------------------------------
Напиток: {coffeeName}
Цена за единицу: {price} руб
Количество: {quantity}
------------------------------
ОБЩАЯ СТОИМОСТЬ: {total} руб

Дата: {DateTime.Now:dd.MM.yyyy HH:mm}
Спасибо за заказ!";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            comboCoffee.SelectedIndex = -1;
            txtQuantity.Text = "1";
            lblTotal.Text = "Итого: 0 руб";
        }
    }
}
