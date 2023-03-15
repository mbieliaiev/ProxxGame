using ProxxGame.Contract;

namespace ProxxGame.Logics
{
    public class CellsTableConsolePrinter : ICellTablePrinter
    {
        public void Print(ICell[,] cells, int width, int height)
        {
            var initialTab = string.Empty;
            var heightStringLength = height.ToString().Length;
            var widthStringLength = width.ToString().Length;
            var biggestStringLength = heightStringLength > widthStringLength ? heightStringLength : widthStringLength;
            for (int i = 0; i < biggestStringLength; i++)
            {
                initialTab += " ";
            }
            Console.Write($"{initialTab}{initialTab}");
            for (int i = 0; i < width; i++)
            {
                Console.Write($"{i.ToString("D" + biggestStringLength)}{initialTab}");
            }
            for (var i = 0; i < height; i++)
            {
                Console.Write($"\n{i.ToString("D" + biggestStringLength)}{initialTab}");
                for (var j = 0; j < width; j++)
                {
                    if (cells[i, j].IsOpen)
                    {
                        if (cells[i, j].IsBlackHole)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            var diffWithInitialTabString = new string('H', initialTab.Length);
                            Console.Write($"{diffWithInitialTabString}{initialTab}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            switch (cells[i, j].Value)
                            {
                                case 0:
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    break;
                                case 1:
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case 2:
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    break;
                                case 3:
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    break;
                                case 4:
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    break;
                                case 5:
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    break;
                                case 6:
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    break;
                                case 7:
                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    break;
                                case 8:
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    break;
                                default:
                                    break;
                            }
                            Console.Write($"{cells[i, j].Value.ToString("D" + biggestStringLength)}{initialTab}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        if (cells[i, j].IsMarkedAsBlackHole)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        var diffWithInitialTabString = new string('#', initialTab.Length);
                        Console.Write($"{diffWithInitialTabString}{initialTab}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
    }
}
