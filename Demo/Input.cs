using System;

namespace Demo
{
    internal class Input
    {

        public static int ReadInt(int min=int.MinValue, int max=int.MaxValue)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out var value))
            {
                if (value >= min && value <= max)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine($"输入的数据不对,请输入{min}到{max}之间的数值");
                    return ReadInt(min, max);
                }
            }
            else {
                Console.WriteLine($"输入的数据不对,请输入{min}到{max}之间的数值");
                return ReadInt(min,max);
            }
        }
    }
}