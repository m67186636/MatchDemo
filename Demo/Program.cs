using System;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool start = true;
            var game = new Game();
            while (start)
            {
                var task = game.Start(true);
                task.Wait();
                Console.WriteLine("输入r重新开始");
                start = Console.ReadKey().Key== ConsoleKey.R;
            }
        }
    }
}
