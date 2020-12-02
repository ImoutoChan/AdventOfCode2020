using System.IO;

namespace AdventOfCode2020.Services
{
    internal class InputReader
    {
        public string ReadInput(int i) => File.ReadAllText($"Input\\{i:00.}.in");
    }
}
