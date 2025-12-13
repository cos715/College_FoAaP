using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Practice5
{
    public partial class SnakeGame : Form
    {
        private List<Point> snake; // Тело змейки
        private Point food;        // Еда
        private string direction;  // Направление движения
        private int score;         // Счет
        private Timer gameTimer;   // Таймер для обновления игры

        // Константы
        private const int CellSize = 20; // Размер одной клетки
        private const int Width = 20;    // Ширина поля в клетках
        private const int Height = 20;   // Высота поля в клетках
        private const int BorderOffset = 30; // Отступ для границ

        public SnakeGame()
        {
            InitializeComponent();
            InitializeGame();
            SetupTimer();
        }
        // Инициализация игры
        private void InitializeGame()
        {
            // Создаем змейку из 3 сегментов
            snake = new List<Point>();
            snake.Add(new Point(5, 5)); // Голова
            snake.Add(new Point(4, 5)); // Тело
            snake.Add(new Point(3, 5)); // Хвост

            direction = "RIGHT"; // Начальное направление
            score = 0;

            GenerateFood(); // Создаем первую еду
        }

        // Настройка таймера
        private void SetupTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 100; // Обновление каждые 100 мс
            gameTimer.Tick += GameLoop;
            gameTimer.Start();
        }

        // Создание еды в случайном месте
        private void GenerateFood()
        {
            Random rand = new Random();
            do
            {
                food = new Point(rand.Next(0, Width), rand.Next(0, Height));
            } while (snake.Contains(food)); // Убедимся, что еда не появилась на змейке
        }

        // Главный игровой цикл
        private void GameLoop(object sender, EventArgs e)
        {
            MoveSnake();
            CheckCollision();
            this.Invalidate(); // Перерисовываем форму
        }

        // Движение змейки
        private void MoveSnake()
        {
            // Получаем текущую позицию головы
            Point head = snake[0];
            Point newHead = head;

            // Вычисляем новую позицию головы в зависимости от направления
            switch (direction)
            {
                case "UP": newHead.Y--; break;
                case "DOWN": newHead.Y++; break;
                case "LEFT": newHead.X--; break;
                case "RIGHT": newHead.X++; break;
            }

            // Добавляем новую голову
            snake.Insert(0, newHead);

            // Проверяем, съела ли змейка еду
            if (newHead == food)
            {
                score += 10;
                GenerateFood();
            }
            else
            {
                // Если не съела - удаляем хвост
                snake.RemoveAt(snake.Count - 1);
            }
        }

        // Проверка столкновений
        private void CheckCollision()
        {
            Point head = snake[0];

            // Столкновение со стенами
            if (head.X < 0 || head.X >= Width || head.Y < 0 || head.Y >= Height)
            {
                GameOver();
                return;
            }

            // Столкновение с собой
            for (int i = 1; i < snake.Count; i++)
            {
                if (head == snake[i])
                {
                    GameOver();
                    return;
                }
            }
        }

        // Конец игры
        private void GameOver()
        {
            gameTimer.Stop();

            DialogResult result = MessageBox.Show(
                $"Игра окончена! Ваш счет: {score}\n\nХотите сыграть еще?",
                "Game Over",
                MessageBoxButtons.YesNo
            );

            if (result == DialogResult.Yes)
            {
                // Перезапускаем игру
                InitializeGame();
                gameTimer.Start();
            }
            else
            {
                // Возвращаемся в главное меню
                this.Close();
            }
        }

        // Отрисовка игры
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Рисуем игровое поле с границами
            DrawGameField(g);

            // Рисуем змейку
            DrawSnake(g);

            // Рисуем еду
            DrawFood(g);

            // Рисуем интерфейс
            DrawUI(g);
        }

        // Рисуем игровое поле с границами
        private void DrawGameField(Graphics g)
        {
            // Вычисляем размеры игрового поля
            int fieldWidth = Width * CellSize;
            int fieldHeight = Height * CellSize;

            // Рисуем внешнюю границу поля (толстая черная рамка)
            Rectangle borderRect = new Rectangle(
                BorderOffset - 3,
                BorderOffset - 3,
                fieldWidth + 6,
                fieldHeight + 6
            );
            g.FillRectangle(Brushes.Black, borderRect);

            // Рисуем внутреннюю область поля (белый фон)
            Rectangle fieldRect = new Rectangle(
                BorderOffset,
                BorderOffset,
                fieldWidth,
                fieldHeight
            );
            g.FillRectangle(Brushes.White, fieldRect);

            // Рисуем сетку (необязательно, но помогает ориентироваться)
            DrawGrid(g);
        }

        // Рисуем сетку игрового поля
        private void DrawGrid(Graphics g)
        {
            Pen gridPen = new Pen(Color.LightGray, 1);

            // Вертикальные линии
            for (int x = 0; x <= Width; x++)
            {
                g.DrawLine(gridPen,
                    BorderOffset + x * CellSize, BorderOffset,
                    BorderOffset + x * CellSize, BorderOffset + Height * CellSize);
            }

            // Горизонтальные линии
            for (int y = 0; y <= Height; y++)
            {
                g.DrawLine(gridPen,
                    BorderOffset, BorderOffset + y * CellSize,
                    BorderOffset + Width * CellSize, BorderOffset + y * CellSize);
            }

            gridPen.Dispose();
        }

        // Рисуем змейку
        private void DrawSnake(Graphics g)
        {
            // Рисуем тело змейки
            for (int i = 0; i < snake.Count; i++)
            {
                Point segment = snake[i];

                // Вычисляем координаты на форме
                int x = BorderOffset + segment.X * CellSize;
                int y = BorderOffset + segment.Y * CellSize;

                // Голова - другого цвета
                if (i == 0)
                {
                    g.FillRectangle(Brushes.DarkGreen, x, y, CellSize, CellSize);
                    // Контур головы
                    g.DrawRectangle(Pens.Black, x, y, CellSize, CellSize);
                }
                else
                {
                    g.FillRectangle(Brushes.Green, x, y, CellSize, CellSize);
                    // Контур тела
                    g.DrawRectangle(Pens.DarkGreen, x, y, CellSize, CellSize);
                }
            }
        }

        // Рисуем еду
        private void DrawFood(Graphics g)
        {
            int x = BorderOffset + food.X * CellSize;
            int y = BorderOffset + food.Y * CellSize;

            // Рисуем яблочко
            g.FillEllipse(Brushes.Red, x, y, CellSize, CellSize);
            // Контур еды
            g.DrawEllipse(Pens.DarkRed, x, y, CellSize, CellSize);

            // Добавляем детали (черенок)
            g.DrawLine(Pens.Brown, x + CellSize / 2, y + 2, x + CellSize / 2, y - 2);
            // Листик
            g.FillEllipse(Brushes.Green, x + CellSize / 2, y - 3, 4, 3);
        }

        // Рисуем интерфейс
        private void DrawUI(Graphics g)
        {
            // Рисуем счет
            g.DrawString($"Счет: {score}",
                new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 10, 10);

            // Рисуем инструкции
            g.DrawString("Управление: Стрелки\nESC - Выход",
                new Font("Arial", 10), Brushes.Black, 10, 450);
        }

        // Обработка нажатия клавиш
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (direction != "DOWN") direction = "UP";
                    break;
                case Keys.Down:
                    if (direction != "UP") direction = "DOWN";
                    break;
                case Keys.Left:
                    if (direction != "RIGHT") direction = "LEFT";
                    break;
                case Keys.Right:
                    if (direction != "LEFT") direction = "RIGHT";
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Space:
                    // Пауза по пробелу
                    if (gameTimer.Enabled)
                        gameTimer.Stop();
                    else
                        gameTimer.Start();
                    break;
            }
        }

        // При закрытии формы возвращаемся в главное меню
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            gameTimer?.Stop();
            Form1 mainMenu = new Form1();
            mainMenu.Show();
        }
    }
}
