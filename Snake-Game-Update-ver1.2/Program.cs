using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Snake
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;
            int lastFoodTime = 0;
            int foodDissapearTime = 8000;
            int negativePoints = 0;
            int userPoints = negativePoints;
            int health = 3;
          
            //snake directions from user - if any changes to this will only mess up the direction
            Position[] directions = new Position[]
            {
                //direction speed
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            //sleepTime indicates speed movement of the snake, the higher the number, the slower the speed of the snake
            double sleepTime = 95.5;

            // starts from the right side of terminal
            int direction = right;
            Random randomNumbersGenerator = new Random();
            Console.BufferHeight = Console.WindowHeight;
            lastFoodTime = Environment.TickCount;

            List<Position> obstacles = new List<Position>()
            {
                new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth)),
                new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth))
            };

            foreach (Position obstacle in obstacles)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }

            //Snake Length
            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(5, i));
            }

            Position food;
            do
            {
                food = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
            }
            while (snakeElements.Contains(food) && obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("4");

            Position food1;
            do
            {
                food1 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
            }
            while (snakeElements.Contains(food1) && obstacles.Contains(food1));
            Console.SetCursorPosition(food1.col, food1.row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("1");

            Position food2;
            do
            {
                food2 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
            }
            while (snakeElements.Contains(food2) && obstacles.Contains(food2));
            Console.SetCursorPosition(food2.col, food2.row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("2");

            Position food3;
            do
            {
                food3 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
            }
            while (snakeElements.Contains(food3) && obstacles.Contains(food3));
            Console.SetCursorPosition(food3.col, food3.row);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("3");


            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");
            }

            while (true)
            {
                //negativePoints++; - Score not needed when snake moves
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direction != right) direction = left;
                    } else if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    } else if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    } else if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                    }
                }

                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);

                //snake to move through terminal/program
                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;


                //Score Board - Top Left
                Console.SetCursorPosition(0, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                userPoints = negativePoints;
                Console.WriteLine("Your points are: {0} \nHealth Points: {1}", userPoints, health);

                //Game Over After Health = 0
                if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeNewHead))
                {
                    Console.Clear();
                    health -= 1;
                    for (int x = 1; x<6; x++)
                    {
                        obstacles[x] = new Position(randomNumbersGenerator.Next(3,Console.WindowHeight), randomNumbersGenerator.Next(3,Console.WindowWidth));
                    }
                    foreach (Position obstacle in obstacles)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(obstacle.col, obstacle.row);
                        Console.Write("=");
                    }
                    if (health == 0)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(55, 10);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game over!");
                        Console.SetCursorPosition(51, 12);
                        Console.WriteLine("Your points are: {0}", userPoints);
                        //if (userPoints < 0) userPoints = 0;
                        //userPoints = Math.Max(userPoints, 0);
                        return;
                    }
                }
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                snakeElements.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (direction == right) Console.Write(">");
                if (direction == left) Console.Write("<");
                if (direction == up) Console.Write("^");
                if (direction == down) Console.Write("v");


                if (snakeNewHead.col == food.col && snakeNewHead.row == food.row)
                {
                    // feeding the snake
                    do
                    {
                        negativePoints = negativePoints + 4;
                        food = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) && obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food.col, food.row);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("4");
                    sleepTime--;

                    //add obstacle after eating food '@'
                    Position obstacle = new Position();
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) || obstacles.Contains(obstacle) && (food.row != obstacle.row && food.col != obstacle.col)); // && is the right code to prevent the blocks from staying the same line as food when random position
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("=");
                }
                else if (snakeNewHead.col == food1.col && snakeNewHead.row == food1.row)
                {
                    // feeding the snake
                    do
                    {
                        negativePoints = negativePoints + 1;
                        food1 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food1) && obstacles.Contains(food1));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food1.col, food1.row);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("1");
                    sleepTime--;

                    //add obstacle after eating food '@'
                    Position obstacle = new Position();
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) || obstacles.Contains(obstacle) && (food1.row != obstacle.row && food1.col != obstacle.col)); // && is the right code to prevent the blocks from staying the same line as food when random position
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("=");
                }
                else if (snakeNewHead.col == food2.col && snakeNewHead.row == food2.row)
                {
                    // feeding the snake
                    do
                    {
                        negativePoints = negativePoints + 2;
                        food2 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food2) && obstacles.Contains(food2));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food2.col, food2.row);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("2");
                    sleepTime--;

                    //add obstacle after eating food '@'
                    Position obstacle = new Position();
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) || obstacles.Contains(obstacle) && (food2.row != obstacle.row && food2.col != obstacle.col)); // && is the right code to prevent the blocks from staying the same line as food when random position
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("=");
                }
                else if (snakeNewHead.col == food3.col && snakeNewHead.row == food3.row)
                {
                    // feeding the snake
                    do
                    {
                        negativePoints = negativePoints + 3;
                        food3 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food3) && obstacles.Contains(food3));
                    lastFoodTime = Environment.TickCount;
                    Console.SetCursorPosition(food3.col, food3.row);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("3");
                    sleepTime--;

                    //add obstacle after eating food '@'
                    Position obstacle = new Position();
                    do
                    {
                        obstacle = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(obstacle) || obstacles.Contains(obstacle) && (food3.row != obstacle.row && food3.col != obstacle.col)); // && is the right code to prevent the blocks from staying the same line as food when random position
                    obstacles.Add(obstacle);
                    Console.SetCursorPosition(obstacle.col, obstacle.row);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("=");
                }
                else
                {
                    // Removing snake traces
                    Position last = snakeElements.Dequeue();
                    Console.SetCursorPosition(last.col, last.row);
                    Console.Write(" ");
                }

                //food traces then set at another location
                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    //negativePoints = negativePoints + 1; - only adds when traced
                    Console.SetCursorPosition(food.col, food.row);
                    Console.Write(" ");
                    Console.SetCursorPosition(food1.col, food1.row);
                    Console.Write(" ");
                    Console.SetCursorPosition(food2.col, food2.row);
                    Console.Write(" ");
                    Console.SetCursorPosition(food3.col, food3.row);
                    Console.Write(" ");
                    do
                    {
                        food = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food) || obstacles.Contains(food));
                    lastFoodTime = Environment.TickCount;
                    do
                    {
                        food1 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food1) || obstacles.Contains(food1));
                    lastFoodTime = Environment.TickCount;
                    do
                    {
                        food2 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food2) || obstacles.Contains(food2));
                    lastFoodTime = Environment.TickCount;
                    do
                    {
                        food3 = new Position(randomNumbersGenerator.Next(3, Console.WindowHeight), randomNumbersGenerator.Next(3, Console.WindowWidth));
                    }
                    while (snakeElements.Contains(food3) || obstacles.Contains(food3));
                    lastFoodTime = Environment.TickCount;
                }

                Console.SetCursorPosition(food.col, food.row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("4");

                Console.SetCursorPosition(food1.col, food1.row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("1");

                Console.SetCursorPosition(food2.col, food2.row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("2");

                Console.SetCursorPosition(food3.col, food3.row);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("3");

                sleepTime -= 0.01;

                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
