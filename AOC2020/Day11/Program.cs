using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var grid = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }

            int changes;
            int seats;
            do
            {
                seats = 0;
                changes = 0;
                var after = new char[lines.Length][];
                for (int i = 0; i < grid.Length; i++)
                {
                    after[i] = new char[grid[i].Length];
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        var test = grid[i][j];
                        if (test == 'L' && CountSeats2(grid, i, j) == 0)
                        {
                            changes++;
                            after[i][j] = '#';
                        }
                        else if (test == '#' && CountSeats2(grid, i, j) >= 5)
                        {
                            after[i][j] = 'L';
                            changes++;
                        }
                        else after[i][j] = grid[i][j];

                        if (after[i][j] == '#') seats++;
                    }
                }
                grid = after;
            } while (changes > 0);

            //count seats
            Console.WriteLine(seats);
            Console.ReadLine();
        }


        static int CountSeats(char[][] grid, int x, int y)
        {
            var count = 0;
            if (IsOcc(grid, x - 1, y)) count++; // up
            if (IsOcc(grid, x + 1, y)) count++; // down
            if (IsOcc(grid, x, y - 1)) count++; // left
            if (IsOcc(grid, x, y + 1)) count++; // right
            if (IsOcc(grid, x + 1, y + 1)) count++; //ur
            if (IsOcc(grid, x + 1, y - 1)) count++; //ul
            if (IsOcc(grid, x - 1, y + 1)) count++; //dr
            if (IsOcc(grid, x - 1, y - 1)) count++; //dl
            return count;
        }

        static int CountSeats2(char[][] grid, int x, int y)
        {
            var count = 0;
            if (IsOcc2(grid, x , y, -1, 0)) count++; // up
            if (IsOcc2(grid, x , y, 1, 0)) count++; // down
            if (IsOcc2(grid, x, y , 0, -1)) count++; // left
            if (IsOcc2(grid, x, y , 0, 1)) count++; // right
            if (IsOcc2(grid, x, y,1,1)) count++; //ur
            if (IsOcc2(grid, x , y, 1, -1)) count++; //ul
            if (IsOcc2(grid, x, y, -1, 1)) count++; //dr
            if (IsOcc2(grid, x, y,-1,-1)) count++; //dl
            return count;
        }

        static bool IsOcc(char[][] grid, int x, int y)
        {
            if (x < 0 || x >= grid.Length) return false;
            if (y < 0 || y >= grid[0].Length) return false;
            return grid[x][y] == '#';
        }

        static bool IsOcc2(char[][] grid, int x, int y, int xAdd, int yAdd)
        {
            while (true)
            {
                x += xAdd;
                y += yAdd;
                if (x < 0 || x >= grid.Length) return false;
                if (y < 0 || y >= grid[0].Length) return false;
                if (grid[x][y] == '#') return true;
                if (grid[x][y] == 'L') return false;
            }
        }
    }
}
