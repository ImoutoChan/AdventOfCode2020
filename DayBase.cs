using System;

namespace AdventOfCode2020
{
    internal abstract class DayBase
    {
        private readonly InputReader _inputReader;

        private int DayNumber { get; }

        protected string Input { get; private set; }

        protected DayBase()
        {
            _inputReader = new InputReader();
            DayNumber = int.Parse(GetType().Name.Substring(3));
        }
        
        public void Run()
        {
            Input = _inputReader.ReadInput(DayNumber);
            
            Console.Write($"Day {DayNumber}: ");
            Solve();

            Console.Write($"Day {DayNumber}: ");
            SolvePart2();
        }

        protected abstract void Solve();

        protected abstract void SolvePart2();
    }
}