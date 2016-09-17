namespace _03.MatrixOperator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MatrixOperator
    {
        private const string End = "end";

        private static readonly List<List<int>> Matrix = new List<List<int>>();

        public static void Main()
        {
            var numOfInputRows = int.Parse(Console.ReadLine());
            for (int i = 0; i < numOfInputRows; i++)
            {
                var inputLine = Console.ReadLine().Split().Select(int.Parse);
                Matrix.Add(new List<int>(inputLine));
            }

            var input = Console.ReadLine();
            while (input != End)
            {
                var commandTokens = input.Split().ToArray();
                var commandName = commandTokens[0];
                switch (commandName)
                {
                    case "remove":
                        var removeType = commandTokens[1];
                        var removePosition = commandTokens[2];
                        var removeIndex = int.Parse(commandTokens[3]);
                        RemoveCommand(removeType, removePosition, removeIndex);
                        break;
                    case "swap":
                        var indexA = int.Parse(commandTokens[1]);
                        var indexB = int.Parse(commandTokens[2]);
                        SwapCommand(indexA, indexB);
                        break;
                    default: // insert
                        var lineIndex = int.Parse(commandTokens[1]);
                        var element = int.Parse(commandTokens[2]);
                        InsertCommand(lineIndex, element);
                        break;
                }

                input = Console.ReadLine();
            }
            
            Matrix.ForEach(row => Console.WriteLine(string.Join(" ", row)));
        }

        private static void InsertCommand(int lineIndex, int element)
        {
            Matrix[lineIndex].Insert(0, element);
        }

        private static void SwapCommand(int indexA, int indexB)
        {
            var temp = Matrix[indexA];
            Matrix[indexA] = Matrix[indexB];
            Matrix[indexB] = temp;
        }

        private static void RemoveCommand(string removeType, string removePosition, int removeIndex)
        {
            if (removePosition == "row")
            {
                RemoveFromRow(removeType, removeIndex);
                return;
            }

            RemoveFromCol(removeType, removeIndex);
        }

        private static void RemoveFromCol(string removeType, int removeIndex)
        {
            foreach (var row in Matrix)
            {
                if (row.Count > removeIndex)
                {
                    switch (removeType)
                    {
                        case "odd":
                            if (row[removeIndex] % 2 != 0)
                            {
                                row.RemoveAt(removeIndex);
                            }

                            break;
                        case "even":
                            if (row[removeIndex] % 2 == 0)
                            {
                                row.RemoveAt(removeIndex);
                            }

                            break;
                        case "positive":
                            if (row[removeIndex] >= 0)
                            {
                                row.RemoveAt(removeIndex);
                            }

                            break;
                        default: // negative                            
                            if (row[removeIndex] < 0)
                            {
                                row.RemoveAt(removeIndex);
                            }

                            break;
                    }
                }
            }
        }

        private static void RemoveFromRow(string removeType, int removeIndex)
        {
            switch (removeType)
            {
                case "odd":
                    Matrix[removeIndex].RemoveAll(i => i % 2 != 0);
                    break;
                case "even":
                    Matrix[removeIndex].RemoveAll(i => i % 2 == 0);
                    break;
                case "positive":
                    Matrix[removeIndex].RemoveAll(i => i >= 0);
                    break;
                default: // negative
                    Matrix[removeIndex].RemoveAll(i => i < 0);
                    break;
            }
        }
    }
}