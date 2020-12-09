using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdventOfCode2020.Days
{
    internal class Day8 : DayBase
    {
        protected override void Solve()
        {
            var commands = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Command(GetType(x.Substring(0, 3)), int.Parse(x.Substring(4))))
                .ToList();

            Emulator emulator = new(commands);
            emulator.Run();

            Console.WriteLine(emulator.Accumulator);
        }

        protected override void SolvePart2()
        {
            var commands = Input
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Command(GetType(x.Substring(0, 3)), int.Parse(x.Substring(4))))
                .ToList();

            for (int i = 0; i < commands.Count; i++)
            {
                var commandToReplace = commands[i];
                var newCommand = commandToReplace.Type switch
                {
                    CommandType.Nop => new Command(CommandType.Jump, commandToReplace.Argument),
                    CommandType.Jump => new Command(CommandType.Nop, commandToReplace.Argument),
                    _ => commandToReplace
                };

                var newCommandList = new List<Command>(commands);
                newCommandList[i] = newCommand;

                Emulator emulator = new(newCommandList);
                if (emulator.Run() == 0)
                {
                    Console.WriteLine(emulator.Accumulator);
                    break;
                }
            }
        }

        private class Emulator
        {
            private readonly IReadOnlyList<Command> _commands;
            private readonly HashSet<int> _runnedCommands = new();
            private int _executionPointer = 0;

            public Emulator(IReadOnlyList<Command> commands)
            {
                _commands = commands;
            }

            public int Accumulator { get; private set; } = 0;

            public int Run()
            {
                do 
                {
                    _runnedCommands.Add(_executionPointer);
                    var currentCommand = _commands[_executionPointer];
                    switch (currentCommand.Type)
                    {
                        default:
                        case CommandType.Nop:
                            _executionPointer++;
                            break;
                        case CommandType.Acc:
                            Accumulator += currentCommand.Argument;
                            _executionPointer++;
                            break;
                        case CommandType.Jump:
                            _executionPointer += currentCommand.Argument;
                            break;
                    }

                    if (_runnedCommands.Contains(_executionPointer))
                        return 1;
                }
                while(_executionPointer < _commands.Count);

                return 0;
            }
        }

        private CommandType GetType(string type)
        {
            switch (type)
            {
                case "acc":
                    return CommandType.Acc;
                case "nop":
                    return CommandType.Nop;
                case "jmp":
                    return CommandType.Jump;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        private record Command(CommandType Type, int Argument);
        
        private enum CommandType
        {
            Nop,
            Acc,
            Jump,
        }
    }
}
