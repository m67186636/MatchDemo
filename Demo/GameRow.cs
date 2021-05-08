using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo
{
    public class GameRow
    {
        private IList<int> data;

        public GameRow(int count)
        {
            data = new List<int>();
            Initialize(count);
        }
        private void Initialize(int count)
        {
            data.Clear();
            for (int i = 0; i < count; i++)
            {
                data.Add(-1);
            }
        }
        public int Aside => data.Count(x => x < 0);
        public bool Completed => Aside == 0;
        public void Take(int player, int count) {
            var startAt= data.IndexOf(-1);
            for (int i =0; i < count; i++)
            {
                data[startAt + i] = player;
            }
        }
    }
}