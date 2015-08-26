using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class SnakeFood
    {
        public int XAxis { get; set; }
        public int YAxis { get; set; }

        public SnakeFood()
        {
            XAxis = 0;
            YAxis = 0;
        }
    }
}
