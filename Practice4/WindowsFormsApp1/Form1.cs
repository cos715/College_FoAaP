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
