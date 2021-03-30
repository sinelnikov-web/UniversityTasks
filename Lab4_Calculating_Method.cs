using System;
namespace SortApp
{
    class Program
    {

        // Метод для вычисления факториала
        static int factorial(int n)
        {
            
            if (n == 1 || n == 0)
            {
                return 1;
            }

            return n * factorial(n - 1);
        }

        // Метод для красивого вывода промежуточных значений
        static void prettyResultPrint(int numerator, int powerValue, int factorialValue, double result, double stepNumber, double x)
        {
            double middleResult = numerator * Math.Pow(x, powerValue) / (factorial(factorialValue) * Math.Pow(3, powerValue));
            // Формируем строки для вывода результатов
            string numeratorString = $"Числитель: {numerator}";
            string powerValueString = $"Степень: {powerValue}";
            string factorialValueString = $"Факториал: {factorialValue}";
            string middleResultString = String.Format("Член последовательности: {0:f5}", middleResult);
            string resultString = String.Format("Результат: {0:f5}", result);
            
            // Выводим строки с одинаковым кол-вом отступов справа
            Console.WriteLine("".PadRight(60, '-'));
            Console.WriteLine($"РЕЗУЛЬТАТ №{stepNumber}".PadRight(60, ' ') + '|');
            Console.WriteLine(numeratorString.PadRight(60, ' ') + '|');
            Console.WriteLine(powerValueString.PadRight(60, ' ') + '|');
            Console.WriteLine(factorialValueString.PadRight(60, ' ') + '|');
            Console.WriteLine(middleResultString.PadRight(60, ' ') + '|');
            Console.WriteLine(resultString.PadRight(60, ' ') + '|');
            Console.WriteLine("".PadRight(60, '-'));
        }

        // Метод для вычисления математической функции Math.Pow(1 + x, 1/3) с заданной точностью
        static double calculateMathFunction(double x, double accuracy = 0.00001)
        {

            // Переменная для хранения результата вычислений
            double result = 1 + x/3;

            // factorialValue - текущее значение для факториала
            // powerValue - текущее значение для степени тройки в знаменателе
            // numeratorValue - текущее значение на которое должен будет умножаться числитель
            int factorialValue = 2, powerValue = 2, numeratorValue = 2;

            // Текущее значение числителя
            int numerator = numeratorValue;

            // Номер текущего шага вычислений
            int stepNumber = 1;

            // Переключатель для смены знака + / -
            bool switcher = false;

            // Пока очередной член вычислений больше заданной точности
            while (numerator * Math.Pow(x, powerValue) / (factorial(factorialValue) * Math.Pow(3, powerValue)) > accuracy)
            {

                // Если switcher == true - прибавляем, в другом случае отнимаем
                if (switcher)
                {
                    result += numerator * Math.Pow(x, powerValue) / (factorial(factorialValue) * Math.Pow(3, powerValue));
                } else
                {
                    result -= numerator * Math.Pow(x, powerValue) / (factorial(factorialValue) * Math.Pow(3, powerValue));
                }

                // Функция для вывода промежуточных данных в хорошем виде
                prettyResultPrint(numerator, powerValue, factorialValue, result, stepNumber, x);

                // Увеличиваем число факториала, степень и шаг на 1, число на которое умножается числитель на 3
                factorialValue++;
                powerValue++;
                stepNumber++;
                numeratorValue += 3;

                // Умножаем числитель на текущее значение numeratorValue
                numerator *= numeratorValue;

                // Меняем состояние переменной-переключателя
                switcher = !switcher;
            }

            // Возвращаем результат вычислений
            return result;
        }

        static void Main(string[] args)
        {
            // Считываем x
            double x = Convert.ToDouble(Console.ReadLine());

            // Вывод результата вычисления метода calculateMathFunction(x, [accuracy=0.0001])
            Console.WriteLine("Результат: {0:f5}", calculateMathFunction(x));

        }
    }
}
