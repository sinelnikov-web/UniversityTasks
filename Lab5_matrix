using System;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        
        // Находи максимальный элемент в матрице
        static int findMaxValue(int[,] matrix)
        {
            int maxValue = matrix[0, 0];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] > maxValue)
                    {
                        maxValue = matrix[i, j];
                    }
                }
            }
            return maxValue;
        }

        // Находит строку с максимальным кол-вом одинаковых элементов
        static (int , int) findMaxSimilarItems(int[,] matrix)
        {
            // Массив для подсчёта кол-ва каждого числа в каждой строке
            int[][] similarCount = new int[matrix.Length][];

            // Ищем максимальное число в массиве, чтобы задать его для второго измерения similarCount
            int matrixMaxValue = findMaxValue(matrix) + 1;

            // Переменная для хранения текущего максимального кол-ва элементов в строке
            int maxSimilarItemsCount = 0;

            // Именованный кортеж для хранения результата в виде (номер строки, кол-во дубликатов)
            (int stringNumber, int maxSimilarItemsCount) result = (0, maxSimilarItemsCount);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                similarCount[i] = new int[matrixMaxValue];
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    similarCount[i][matrix[i, j]]++;
                }

                // Считаем кол-во дубликатов в текущей строке
                int similarItemsCount = similarCount[i].Where(item => item > 1).Sum();

                // Если кол-во дубликатов в текущей строке больше чем максимально известное, то оно становится максимальным
                if (similarItemsCount > maxSimilarItemsCount)
                {
                    maxSimilarItemsCount = similarItemsCount;

                    // Сохраняем номер строки
                    result.stringNumber = i + 1;

                    // Сохраняем кол-во дубликатов в строке
                    result.maxSimilarItemsCount = maxSimilarItemsCount;
                } 
            }

            return result;
        }

        // Заполняет матрицу случайными числами
        static void fillMatrix(int[,] matrix)
        {
            Random rand = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(0, 21);
                }
                
            }
        }

        // Выводит матрицу на экран
        static void printMatrix(int[,] matrix)
        {
            // Пустая строка для построчного вывода матрицы
            string resultString = "";
            Console.WriteLine("Матрица:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Обнуляем resultString при переходе на следующую строку
                resultString = "";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // Делаем элемент матрицы строковым, чтобы узнать кол-во символов
                    string element = Convert.ToString(matrix[i, j]);

                    // Форматируем в зависимости от кол-ва символов
                    if (element.Length % 2 == 0)
                    {
                        resultString += "| " + element + " ";
                    
                    } else
                    {
                        resultString += "| " + element + "  ";
                    }
                    

                }
                resultString += '|';

                // Выводим разделитель строк
                Console.WriteLine("".PadRight(resultString.Length, '-'));
                // Выводим строку
                Console.WriteLine(resultString);
                
            }
            // Закрываем таблицу
            Console.WriteLine("".PadRight(resultString.Length, '-'));
        }
        
        static void Main()
        {
            // Создаём массив 10x10
            int[,] matrix = new int[10, 10];

            // Заполняем массив случайными числами от 0 до 20
            fillMatrix(matrix);

            // Выводим массив на экран
            printMatrix(matrix);

            // Создаём именованный кортеж и помещаем в него результат выполнения функции findMaxSimilarItems
            (int stringNumber, int maxSimilarItemsCount) stringWithMaxSimilarItems = findMaxSimilarItems(matrix);

            // Выводим результат на основании данных в кортеже stringWithMaxSimilarItems
            Console.WriteLine(
                "Номер строки с наибольшим кол-вом повторяющихся элементов: {0}\nКол-во одинаковых элементов: {1}", 
                stringWithMaxSimilarItems.stringNumber, 
                stringWithMaxSimilarItems.maxSimilarItemsCount
                ); 
        }

    }
}
