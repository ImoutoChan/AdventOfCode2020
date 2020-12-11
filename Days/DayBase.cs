using System;
using AdventOfCode2020.Services;

namespace AdventOfCode2020.Days
{
    internal abstract class DayBase
    {
        private readonly InputReader _inputReader;

        private int DayNumber { get; }

        protected string Input { get; private set; }

        protected string[] InputStrings { get; private set; }

        protected DayBase()
        {
            _inputReader = new InputReader();
            DayNumber = int.Parse(GetType().Name.Substring(3));
        }
        
        public void Run()
        {
            Input = _inputReader.ReadInput(DayNumber);
            InputStrings = _inputReader.ReadInputStrings(DayNumber);
            
            Console.Write($"Day {DayNumber:D2}: ");
            Solve();

            Console.Write($"Day {DayNumber:D2}: ");
            SolvePart2();
        }

        protected abstract void Solve();

        protected abstract void SolvePart2();
    }
}