using System;
using System.Collections.Generic;

namespace Game.Core
{
    public class DepthFirstLayout : ILayout
    {
        [Flags]
        private enum Direction
        {
            None = 0,
            Left = 1,
            Right = 2,
            Up = 4,
            Down = 8,
        }

        public bool[,] Generate(int seed, int width, int height)
        {
            System.Random random = new System.Random(seed);

            bool[,] layout = new bool[width, height];
            bool[,] visited = new bool[width, height];
            Stack<(Position position, Direction direction)> opens = new Stack<(Position position, Direction)>();
            (Position position, Direction direction) first = (new Position(width / 2, 2), Direction.Up);
            opens.Push(first);

            layout[first.position.x, first.position.y] = false;
            while (opens.Count > 0)
            {
                (Position position, Direction direction) current = opens.Pop();
                if (visited[current.position.x, current.position.y])
                    continue;

                visited[current.position.x, current.position.y] = true;
                Position from = From(current.position, current.direction);
                Position inbetween = Inbetween(current.position, current.direction);
                layout[from.x, from.y] = true;
                layout[inbetween.x, inbetween.y] = true;

                Span<(Direction direction, int value)> priority = stackalloc (Direction, int)[4];
                priority[0] = (Direction.Left, random.Next());
                priority[1] = (Direction.Right, random.Next());
                priority[2] = (Direction.Up, random.Next());
                priority[3] = (Direction.Down, random.Next());

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3 - i; j++)
                    {
                        if (priority[j].value < priority[j + 1].value)
                        {
                            (Direction direction, int value) temp = priority[j];
                            priority[j] = priority[j + 1];
                            priority[j + 1] = temp;
                        }
                    }
                }

                for (int i = 0; i < 4; ++i)
                {
                    Position next = Next(current.position, priority[i].direction);
                    if (IsInBound(next, width, height) && !visited[next.x, next.y])
                    {
                        opens.Push((next, priority[i].direction));
                    }
                }
            }

            return layout;
        }

        private Position Next(Position from, Direction direction)
        {
            return direction switch
            {
                Direction.Left => (from.x - 2, from.y),
                Direction.Right => (from.x + 2, from.y),
                Direction.Up => (from.x, from.y + 2),
                Direction.Down => (from.x, from.y - 2),
                _ => throw new NotImplementedException()
            };
        }

        private Position From(Position from, Direction direction)
        {
            return direction switch
            {
                Direction.Left => (from.x + 2, from.y),
                Direction.Right => (from.x - 2, from.y),
                Direction.Up => (from.x, from.y - 2),
                Direction.Down => (from.x, from.y + 2),
                _ => throw new NotImplementedException()
            };
        }

        private Position Inbetween(Position from, Direction direction)
        {
            return direction switch
            {
                Direction.Left => (from.x + 1, from.y),
                Direction.Right => (from.x - 1, from.y),
                Direction.Up => (from.x, from.y - 1),
                Direction.Down => (from.x, from.y + 1),
                _ => throw new NotImplementedException()
            };
        }

        private bool IsInBound(Position value, int width, int height)
        {
            return value.x < width && value.y < height && value.x >= 0 && value.y >= 0;
        }
    }

    internal struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position other &&
                   x == other.x &&
                   y == other.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public static implicit operator (int x, int y)(Position value)
        {
            return (value.x, value.y);
        }

        public static implicit operator Position((int x, int y) value)
        {
            return new Position(value.x, value.y);
        }

        public static Position operator -(Position position, Position other)
        {
            return new Position(position.x - other.x, position.y - other.y);
        }

        public static Position operator +(Position position, Position other)
        {
            return new Position(position.x + other.x, position.y + other.y);
        }
    }
}
