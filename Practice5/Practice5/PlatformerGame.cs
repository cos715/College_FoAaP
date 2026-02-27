using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Practice5
{
    public partial class PlatformerGame : Form
    {
        private const int PlayerSize = 30;
        private const float Gravity = 0.5f;
        private const int JumpForce = -12;
        private const float MoveSpeed = 5f;
        private const int MaxVelocityY = 20;

        private Rectangle player;
        private PointF velocity;
        private bool isOnGround;
        private int score;
        private int collectedCoins;

        private int currentLevelIndex;
        private Level[] currentLevels;
        private Level currentLevel;

        private bool levelCompleted;
        private bool gameStarted;
        private bool gameFinished;

        private SolidBrush playerBrush;
        private SolidBrush platformBrush;
        private SolidBrush breakingPlatformBrush;
        private SolidBrush coinBrush;
        private SolidBrush goalBrush;
        private Pen platformPen;
        private Pen breakingPlatformPen;
        private Pen coinPen;
        private Pen goalPen;
        private Font levelFont;

        private Timer gameTimer;

        private HashSet<Keys> pressedKeys = new HashSet<Keys>();

        private Dictionary<Rectangle, int> breakingPlatforms;

        public PlatformerGame()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.ClientSize = new Size(800, 600);
            this.Text = "Platformer Game";

            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.Resize += Form1_Resize;
            this.Paint += Form1_Paint;

            InitializeGraphicsResources();

            currentLevels = new Level[]
            {
                CreateLevel1(),
                CreateLevel2(),
                CreateLevel3()
            };

            breakingPlatforms = new Dictionary<Rectangle, int>();

            currentLevelIndex = 0;
            ResetLevel();

            gameStarted = false;
            levelCompleted = false;
            gameFinished = false;
            score = 0;
            collectedCoins = 0;

            pressedKeys.Clear();

            gameTimer = new Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.FormClosing += PlatformerGame_FormClosing;
        }

        private void InitializeGraphicsResources()
        {
            playerBrush = new SolidBrush(Color.Blue);
            platformBrush = new SolidBrush(Color.Brown);
            breakingPlatformBrush = new SolidBrush(Color.FromArgb(150, 100, 50));
            coinBrush = new SolidBrush(Color.Yellow);
            goalBrush = new SolidBrush(Color.Green);
            platformPen = new Pen(Color.DarkSlateGray, 2);
            breakingPlatformPen = new Pen(Color.DarkRed, 2);
            coinPen = new Pen(Color.DarkOrange, 1);
            goalPen = new Pen(Color.DarkGreen, 3);
            levelFont = new Font("Arial", 12, FontStyle.Bold);
        }

        private Level CreateLevel1()
        {
            var platforms = new List<Rectangle>
            {
                new Rectangle(0, 500, 800, 20),
                new Rectangle(200, 400, 150, 20),
                new Rectangle(450, 350, 150, 20),
                new Rectangle(650, 250, 100, 20)
            };

            var coins = new List<Rectangle>
            {
                new Rectangle(250, 370, 15, 15),
                new Rectangle(500, 320, 15, 15)
            };

            var goal = new Rectangle(720, 170, 40, 40);

            return new Level(platforms, coins, new Point(50, 470))
            {
                Goal = goal
            };
        }

        private Level CreateLevel2()
        {
            var platforms = new List<Rectangle>
            {
                new Rectangle(0, 500, 800, 20),
                new Rectangle(100, 400, 100, 20),
                new Rectangle(250, 350, 100, 20),
                new Rectangle(400, 300, 100, 20),
                new Rectangle(550, 250, 100, 20),
                new Rectangle(700, 200, 80, 20)
            };

            var coins = new List<Rectangle>
            {
                new Rectangle(130, 370, 15, 15),
                new Rectangle(280, 320, 15, 15),
                new Rectangle(430, 270, 15, 15),
                new Rectangle(580, 220, 15, 15)
            };

            var goal = new Rectangle(740, 120, 40, 40);

            return new Level(platforms, coins, new Point(50, 370))
            {
                Goal = goal
            };
        }

        private Level CreateLevel3()
        {
            var platforms = new List<Rectangle>
            {
                new Rectangle(0, 500, 800, 20),
                new Rectangle(150, 450, 100, 20),
                new Rectangle(300, 400, 100, 20),
                new Rectangle(450, 350, 100, 20),
                new Rectangle(600, 300, 100, 20),
                new Rectangle(200, 250, 100, 20),
                new Rectangle(400, 200, 100, 20),
                new Rectangle(600, 150, 100, 20)
            };

            var coins = new List<Rectangle>
            {
                new Rectangle(180, 420, 15, 15),
                new Rectangle(330, 370, 15, 15),
                new Rectangle(480, 320, 15, 15),
                new Rectangle(630, 270, 15, 15),
                new Rectangle(230, 220, 15, 15),
                new Rectangle(430, 170, 15, 15)
            };

            var goal = new Rectangle(620, 90, 40, 40);

            return new Level(platforms, coins, new Point(50, 400))
            {
                Goal = goal
            };
        }

        private void ResetLevel()
        {
            if (currentLevelIndex < currentLevels.Length)
            {
                currentLevel = currentLevels[currentLevelIndex].Clone();
                player = new Rectangle(currentLevel.StartPoint.X, currentLevel.StartPoint.Y, PlayerSize, PlayerSize);
                velocity = new PointF(0, 0); 
                isOnGround = false;
                levelCompleted = false;

                pressedKeys.Clear();

                breakingPlatforms.Clear();
                if (currentLevelIndex == 1)
                {
                    breakingPlatforms.Add(new Rectangle(250, 350, 100, 20), 0);
                }
                else if (currentLevelIndex == 2)
                {
                    breakingPlatforms.Add(new Rectangle(450, 350, 100, 20), 0);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameStarted && e.KeyCode == Keys.Space)
            {
                gameStarted = true;
                return;
            }

            if (gameFinished)
            {
                if (e.KeyCode == Keys.Space)
                {
                    gameStarted = false;
                    gameFinished = false;
                    currentLevelIndex = 0;
                    score = 0;
                    collectedCoins = 0;
                    ResetLevel();
                }
                return;
            }

            if (e.KeyCode == Keys.R)
            {
                ResetLevel();
                return;
            }

            if (levelCompleted) return;

            if (!pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Add(e.KeyCode);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (pressedKeys.Contains(e.KeyCode))
            {
                pressedKeys.Remove(e.KeyCode);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!gameStarted || gameFinished) return;

            if (levelCompleted)
            {
                if (currentLevelIndex < currentLevels.Length - 1)
                {
                    currentLevelIndex++;
                    ResetLevel();
                }
                else
                {
                    gameFinished = true;
                    SystemSounds.Asterisk.Play();
                    MessageBox.Show($"Поздравляем! Вы прошли все уровни!\nИтоговый счёт: {score}\nНажмите ПРОБЕЛ для новой игры",
                            "Победа!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            velocity.X = 0; 

            if (pressedKeys.Contains(Keys.Left) || pressedKeys.Contains(Keys.A))
            {
                velocity.X = -MoveSpeed;
            }
            if (pressedKeys.Contains(Keys.Right) || pressedKeys.Contains(Keys.D))
            {
                velocity.X = MoveSpeed;
            }

            if ((pressedKeys.Contains(Keys.Up) || pressedKeys.Contains(Keys.W) || pressedKeys.Contains(Keys.Space)) && isOnGround)
            {
                SystemSounds.Beep.Play();
                velocity.Y = JumpForce;
                isOnGround = false;

                pressedKeys.Remove(Keys.Up);
                pressedKeys.Remove(Keys.W);
                pressedKeys.Remove(Keys.Space);
            }

            velocity.Y += Gravity;
            if (velocity.Y > MaxVelocityY) velocity.Y = MaxVelocityY;

            player.X += (int)velocity.X;
            player.Y += (int)velocity.Y;

            CheckCollisions();
            UpdateBreakingPlatforms();

            if (player.X < 0) player.X = 0;
            if (player.X + PlayerSize > ClientSize.Width) player.X = ClientSize.Width - PlayerSize;

            if (player.Y > ClientSize.Height)
            {
                ResetLevel();
                return;
            }

            CheckCoinCollisions();
            CheckLevelComplete();

            this.Invalidate();
        }

        private void CheckCollisions()
        {
            isOnGround = false;

            foreach (var platform in currentLevel.Platforms)
            {
                if (breakingPlatforms.ContainsKey(platform) && breakingPlatforms[platform] >= 100)
                    continue;

                if (player.IntersectsWith(platform))
                {
                    Rectangle intersection = Rectangle.Intersect(player, platform);

                    if (intersection.Width > intersection.Height)
                    {
                        if (player.Bottom > platform.Top && player.Top < platform.Top)
                        {
                            player.Y = platform.Top - PlayerSize;
                            velocity.Y = 0;
                            isOnGround = true;

                            if (breakingPlatforms.ContainsKey(platform))
                            {
                                breakingPlatforms[platform] = Math.Min(breakingPlatforms[platform] + 10, 100);
                            }
                        }
                        else if (player.Top < platform.Bottom && player.Bottom > platform.Bottom)
                        {
                            player.Y = platform.Bottom;
                            velocity.Y = 0;
                        }
                    }
                    else
                    {
                        if (player.Right > platform.Left && player.Left < platform.Left)
                        {
                            player.X = platform.Left - PlayerSize;
                        }
                        else if (player.Left < platform.Right && player.Right > platform.Right)
                        {
                            player.X = platform.Right;
                        }
                    }
                }
            }
        }

    }
}
