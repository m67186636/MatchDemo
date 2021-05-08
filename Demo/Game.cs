using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
    internal class Game
    {
        public int Frequency { get; private set; }
        public int PlayerCount { get; private set; }
        public int CurrentRow { get; private set; }

        private IList<GameRow> data;

        public Game()
        {
            data = new List<GameRow>();
        }

        public void Initialize()
        {
            Console.WriteLine("请输入玩家数量");
            PlayerCount = Input.ReadInt(1, 5);
            Frequency = 0;
            data.Clear();
            data.Add(new GameRow(3));
            data.Add(new GameRow(5));
            data.Add(new GameRow(7));
            CurrentRow = 0;
        }
        private int GetCurrentPlayer() {
            return (Frequency++) % PlayerCount;
        }
        private GameRow GetCurrentRow() {
            if (!(data.Count > CurrentRow))
                return null;
            var row = data[CurrentRow];
            if (row.Completed)
            {
                Console.WriteLine($"第{++CurrentRow}行已取完");
                return GetCurrentRow();
            }
            else {
                return row;
            }
        }
        private GameRow SelectRow() {
            Console.WriteLine(string.Join('\t',data.Select((x,i)=>$"第{i+1}行剩余{x.Aside}个")));
            Console.WriteLine($"请选择行号");
            var selectRow = Input.ReadInt(1, data.Count) - 1;
            var row = data[selectRow];
            if (row.Completed)
            {
                Console.WriteLine($"第{selectRow+1}行已取完");
                return SelectRow();
            }
            else {
                return row;
            }
        }
        public async Task Start(bool initialize=false) {
            if (initialize) Initialize();
            var mutex = new Mutex();
            await Task.Run(()=> {
                while (true)
                {
                    if (mutex.WaitOne())
                    {
                        Console.Clear();
                        var player = GetCurrentPlayer();
                        Console.WriteLine($"当前玩家:(玩家{player + 1})");
                        //自选行SelectRow,顺序行GetCurrentRow
                        var row = SelectRow();
                        var max = row.Aside;
                        Console.WriteLine($"当前行剩余{max}");
                        var input = Input.ReadInt(1, max);
                        row.Take(player, input);
                        if (!(data.Sum(x => x.Aside) > 0))
                        {
                            Console.WriteLine($"游戏结束:(玩家{player + 1})失败");
                            break;
                        }
                        
                        mutex.ReleaseMutex();
                    }
                    Task.Delay(300);
                }
            });
        }
    }
}