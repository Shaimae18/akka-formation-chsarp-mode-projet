using System;
using System.Collections.Generic;
using System.Text;

namespace Entites
{
    class Position
    {
        private int x, y;
        public void SetX(int x)
        {
            this.x = x;
        }
        public int GetX()
        {
            return x;
        }
        public void SetY(int y)
        {
            this.y = y;
        }
        public int GetY()
        {
            return y;
        }
        public Position(int x, int y)
        {
            SetX(x);
            SetY(y);
        }
    }
}
