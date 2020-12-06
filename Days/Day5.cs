using System;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day5 : DayBase
    {
        protected override void Solve()
        {
            var seatIds =
                //new[] {"FBFBBFFRLR", "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL"}
                Input
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Select(FindSeat)
                    .Select(x => x.Row * 8 + x.Seat);

            Console.WriteLine(seatIds.Max());
        }

        private (int Row, int Seat) FindSeat(string pass)
        {
            var row = pass.Substring(0, 7);

            var front = 0;
            var back = 127;
            foreach (var letter in row)
            {
                var border = (back - front) / 2;

                if (letter == 'F')
                {
                    back = front + border;
                }
                else
                {
                    front = front + border + 1;
                }
            }

            var seat = pass.Substring(7);

            var left = 0;
            var right = 7;
            foreach (var letter in seat)
            {
                var border = (right - left) / 2;

                if (letter == 'L')
                {
                    right = left + border;
                }
                else
                {
                    left = left + border + 1;
                }
            }

            return (back, left);
        }


        protected override void SolvePart2()
        {
            var seatIds =
                //new[] {"FBFBBFFRLR", "BFFFBBFRRR", "FFFBBBFRRR", "BBFFBBFRLL"}
                Input
                    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                    .Select(FindSeat)
                    .Select(x => x.Row * 8 + x.Seat)
                    .ToHashSet();

            var allSeatIds = Enumerable
                .Range(0, 127)
                .SelectMany(x => Enumerable.Range(0, 7).Select(y => (Row: x, Seat: y)))
                .Select(x => x.Row * 8 + x.Seat);

            var potentialSeats = allSeatIds.Except(seatIds);

            var mySeat = potentialSeats
                .FirstOrDefault(x => seatIds.Contains(x + 1) && seatIds.Contains(x - 1));

            Console.WriteLine(mySeat);
        }
    }
}
