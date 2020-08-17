using System;

namespace circleDeteciton.circellogic
{
    class Position
    {
        public int X;
        public int Y;
        public Position()
        {

        }
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            try
            {
                Position toCompare = (Position)obj;
                return toCompare.X == X && toCompare.Y == Y;
            }
            catch
            {
            return base.Equals(obj);
            }
        }

    }
}
