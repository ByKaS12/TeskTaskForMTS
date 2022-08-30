using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TeskTaskForMTS
{
    public static class TestTask
    {
        public static void TransformToElephant()
        {
            if (Console.Out.NewLine !="Слон")
            {
                Console.WriteLine("Слон");
                Console.SetOut(new StringWriter() { NewLine = "Слон" });
            }
        }
        public static void Task_Five()
        {
            TransformToElephant();
            Console.WriteLine("Муха");
            //... custom application code

        }
        public static void TestValueToTaskFour( ref int sortFactor, ref int maxValue, ref int length)
        {
            int LimitMaxValue = 2000;
            int LimitLength = 1000000000;
            Console.WriteLine("Введите  максимально возможное значение элемента в потоке не более 2000 ");
            maxValue = Convert.ToInt32(Console.ReadLine());
            if(maxValue>LimitMaxValue)
            {
                Console.WriteLine("Введено значение более 2000 ");
                Task_Four();
            }
            Console.WriteLine("Введите значение упорядоченности потока не более чем maxValue");
            sortFactor = Convert.ToInt32(Console.ReadLine());
            if (sortFactor > maxValue)
            {
                Console.WriteLine("Введено значение более значения maxValue ");
                Task_Four();
            }
            Console.WriteLine("Введите максимальное количество элементов потока не превышая миллиарда");
            length = Convert.ToInt32(Console.ReadLine());
            if (length > LimitLength)
            {
                Console.WriteLine("Введено значение более одного миллиарда ");
                Task_Four();
            }

        }
        public static void Task_Four()
        {
            List<int> inputStream = new List<int>();
            int sortFactor = 0;
            int maxValue = 0;
            int length = 0;
            TestValueToTaskFour(ref sortFactor,ref maxValue,ref length);
            Random random = new Random();
            inputStream.Add(random.Next(0, maxValue + 1));
            var prevValue = inputStream.ElementAt(0);
            for (int i = 1; i < length; i++)
            {
                int raz = prevValue - sortFactor;
                if (raz < 0)
                    raz = 0;
                var curValue = random.Next(raz, maxValue + 1);
                inputStream.Add(curValue);
                prevValue = curValue;
            }

            Console.WriteLine("\n Отсортированный поток");
            foreach (var item in Sort(inputStream, sortFactor, maxValue))
            {
                Console.Write($"{item} ");
            }
            

        }
        public static IEnumerable<int> MergeSort(IEnumerable<int> values)
        {
            var inputStreamArray = values.ToArray();
            if (inputStreamArray.Length!=0)
            {
                int[] buffer = new int[values.Count()];
                MergeSortImpl(inputStreamArray, buffer, 0, values.Count() - 1);
            }
            return inputStreamArray;
        }
        public static void MergeSortImpl(int[] values, int[] buffer, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSortImpl(values, buffer, l, m);
                MergeSortImpl(values, buffer, m + 1, r);

                int k = l;
                for (int i = l, j = m + 1; i <= m || j <= r;)
                {
                    if (j > r || (i <= m && values[i] < values[j]))
                    {
                        buffer[k] = values[i];
                        ++i;
                    }
                    else
                    {
                        buffer[k] = values[j];
                        ++j;
                    }
                    ++k;
                }
                for (int i = l; i <= r; ++i)
                {
                    values[i] = buffer[i];
                }
            }
        }
        public static IEnumerable<int>  Sort(IEnumerable<int> inputStream, int sortFactor, int maxValue)
        {
            return MergeSort(inputStream);
            /*
             * Выбрал данный вид сортировки по 3 причинам:
             * 1. худшее,среднее,лучшее время O(n*log n) - что даёт точное понимание сколько пользователю ждать времени, а не надеется на удачное попадание на опорный элемент, как у Хоара
             * 2. Затраты памяти O(n), что хуже чем у сортировки "Расчёской" или шейкерной (O(1)), но тогда не жертвуется время алгоритма, так как у представленных видах сортировки есть шанс попасть на худшее время O(n2)
             * 3. https://www.nookery.ru/how-to-sort-the-array/ и на https://habr.com/ru/post/335920/ были представлены отчёты о тестировании, где данная сортировка неплохо показала себя при частично отсортированном массиве.
             *  на данных ресурсах представлены и другие виды сортировок которые по некоторым показателям и обходят мой выбор, но при изучении каждой из них были выявлены недостатки, которые показались мне достаточно важными для того, чтобы не выбирать их для данного задания.
             */
        }
        static readonly IFormatProvider _ifp = CultureInfo.InvariantCulture;


        class Number
        {
            readonly int _number;

            public Number(object number)
            {
                _number = Convert.ToInt32(number);
            }
            public static string operator +(Number n1, string n2) => new Number(n1._number + Convert.ToInt32(n2)).ToString();


            public override string ToString() => _number.ToString(_ifp);

        }

        public static void Task_Two()
        {
            int someValue1 = 10;
            int someValue2 = 5;
            
            string result = new Number(someValue1) + someValue2.ToString(_ifp);
            Console.WriteLine(result);
            Console.ReadKey();

            /*
             * Без изменений, результатом работы будет конкатинация числа и строки  (10+5 = 105)
             * После изменений, при разных значениях и типах переменных someValue1 и someValue2 обрабатывается сложение (10+5=15)


         */
        }
        public static IEnumerable<(T item, int? tail)> EnumerateFromTail<T>(IEnumerable<T> enumerable, int? tailLength)
        {
            if (tailLength == null || tailLength <= 0)
                return new List<(T item, int? tail)>();

            List<(T item, int? tail)> items = new List<(T item, int? tail)>();
            int count = enumerable.Count();
            if (tailLength >= count)
            {
                foreach (var item in enumerable)
                {
                    items.Add((item, --count));
                }
            }
            else
            {
                int i = 0;
                int? raznica = count - tailLength;
                foreach (var item in enumerable)
                {
                    if(i >= raznica)
                        items.Add((item, --tailLength));
                    else
                        items.Add((item, null));
                    i++;
                }
            }
            return items;
            // Возможно ли реализовать такой метод выполняя перебор значений перечисления только 1 раз? Ответ: можно, что и демонстрируется в методе, если число отсчета больше количества элементов, то просто будет полный проход коллекции и отсчётом
            // Если же меньше, то пока разница количества и отсчёта не будет меньше или равна i , то записываем null, иначе tailLength-1
        }

        public static void  Task_Three()
        {
            int[] ints = new[] { 1, 2, 3, 4 };
            var list = EnumerateFromTail(ints, 2);
        }
        static void FailProcess()
        {
            //Первый способ 
            Process.GetCurrentProcess().Kill();
            //Второй способ 
            Environment.Exit(0);
            //Третий способ
            Environment.FailFast("Successful FailProccess");
            //Четвертый  способ
            string ProcessName = "Name of Proccess";
            Process.GetProcessesByName(ProcessName).FirstOrDefault()?.Kill();
            //Пятый  способ
            int ProcessId = Environment.ProcessId; // Id of Proccess
            Process.GetProcessById(ProcessId).Kill();


        }
        public static void Task_One()
        {
            try
            {
                FailProcess();
            }
            catch { }
            Console.WriteLine("Failed to fail process!");
            Console.ReadKey();

        }
    }
}
