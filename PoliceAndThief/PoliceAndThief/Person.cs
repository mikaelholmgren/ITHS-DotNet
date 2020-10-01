using System;

namespace PoliceAndThief
{
    abstract public class Person
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Xdirection { get; set; }
        public int Ydirection { get; set; }
        public bool Monitored { get; private set; }

        abstract public char Display();
        public Person()
        {
            X = Utils.GetRandomNumber(0, GameSettings.BoardSizeX);
            Y = Utils.GetRandomNumber(0, GameSettings.BoardSizeY);
            do
            {
                Xdirection = Utils.GetRandomNumber(-1, 1 + 1);
                Ydirection = Utils.GetRandomNumber(-1, 1 + 1);
            } while (Xdirection == 0 && Ydirection == 0); // We don't want the person to stand still.
        }
        public void Move()
        {
            if (Xdirection > 0) X++;
            if (Ydirection > 0) Y++;
            if (Xdirection < 0) X--;
            if (Ydirection < 0) Y--;
            if (X == GameSettings.BoardSizeX)
            {
                X = 0;
                if (Ydirection > 0) Y = 0;
                else if (Ydirection < 0) Y = GameSettings.BoardSizeY - 1;

            }
            if (Y == GameSettings.BoardSizeY)
            {
                Y = 0;
                if (Xdirection > 0) X = 0;
                else if (Xdirection < 0) X = GameSettings.BoardSizeX - 1;

            }
            if (X < 0)
            {
                X = GameSettings.BoardSizeX - 1;
                if (Ydirection > 0) Y = 0;
                else if (Ydirection < 0) Y = GameSettings.BoardSizeY - 1;

            }
            if (Y < 0)
            {
                Y = GameSettings.BoardSizeY - 1;
                if (Xdirection > 0)
                {
                    X = X - (1 * GameSettings.BoardSizeY - 1);
                    if (X < 0) X = 0;
                }
                else if (Xdirection < 0)
                {
                    X = X + (1 * GameSettings.BoardSizeY -1);
                    if (X >= GameSettings.BoardSizeX) X = GameSettings.BoardSizeX - 1;
                }
            }
        }

        public virtual bool CheckAction(GameboardCell c)
        {
            // Default is do nothing
            return false;
        }
        public virtual void DoAction(Person destination)
        {
            // Default is do nothing
            return;
        }

}
}
