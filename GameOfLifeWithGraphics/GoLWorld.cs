using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeWithGraphics
{
    class GoLWorld
    {
        private readonly Random gen = new Random();
        public int WorldDimension = 50;

        public bool[,] World
        {
            get;
        }

        public GoLWorld ()
        {
            World = new bool[WorldDimension, WorldDimension];
        }

        public GoLWorld (int worldDimension)
        {
            WorldDimension = worldDimension;

            World = new bool[WorldDimension, WorldDimension];
        }

        public void Initialize ()
        {
            for (int i = 0; i <= World.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= World.GetUpperBound(1); j++)
                {
                    World[i, j] = RandomBool();
                }
            }
        }

        private bool RandomBool()
        {
            return gen.Next(100) < 50;
        }

        public void Evolve()
        {
            bool[,] oldWorldState = new bool[WorldDimension, WorldDimension];

            Copy(World, oldWorldState);

            for (int i = 0; i <= World.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= World.GetUpperBound(1); j++)
                {
                    World[i, j] = GetNewCellState(oldWorldState, i, j);
                }
            }
        }

        private void Copy(bool[,] fromWorld, bool[,] toWorld)
        {
            if (fromWorld.GetUpperBound(0) == toWorld.GetUpperBound(0) && fromWorld.GetUpperBound(1) == toWorld.GetUpperBound(1))
            {
                for (int i = 0; i <= fromWorld.GetUpperBound(0); i++)
                {
                    for (int j = 0; j <= fromWorld.GetUpperBound(1); j++)
                    {
                        if (fromWorld[i, j])
                        {
                            toWorld[i, j] = true;
                        }
                        else
                        {
                            toWorld[i, j] = false;
                        }
                    }
                }
            }
        }

        private bool GetNewCellState(bool[,] oldWorldState, int i, int j)
        {
            int numOfAliveCells = 0;

            // We will be checking cells left and right, each by one, and up and down,
            // each by one, of current [i, j] cell
            for (int x = i - 1; x <= i + 1; x++)
            {
                for (int y = j - 1; y <= j + 1; y++)
                {
                    int realI;
                    int realJ;

                    // To "enclose" a world on itself we check if x goes out
                    // of world boundaries and swictch it to the opposite side of the world
                    if (x < 0)
                    {
                        realI = oldWorldState.GetUpperBound(0);
                    }
                    else if (x > oldWorldState.GetUpperBound(0))
                    {
                        realI = 0;
                    }
                    else
                    {
                        realI = x;
                    }

                    // To "enclose" a world on itself we check if y goes out
                    // of world boundaries and swictch it to the opposite side of the world
                    if (y < 0)
                    {
                        realJ = oldWorldState.GetUpperBound(0);
                    }
                    else if (y > oldWorldState.GetUpperBound(0))
                    {
                        realJ = 0;
                    }
                    else
                    {
                        realJ = y;
                    }

                    if (realI != i || realJ != j)
                    {
                        if (oldWorldState[realI, realJ])
                        {
                            numOfAliveCells++;
                        }
                    }
                }
            }

            // Dead cell becomes alive if it has 3 alive neighbores
            if (!oldWorldState[i, j])
            {
                if (numOfAliveCells == 3)
                {
                    return true;
                }

                return false;
            }

            if (numOfAliveCells == 2 || numOfAliveCells == 3)
            {
                return true;
            }

            return false;
        }
    }
}
