namespace TeskTaskForMTS
{

    internal class Program
    {
        public static void menu() {
            Console.WriteLine("1. Ломай меня полностью.");
            Console.WriteLine("2. Операция «Ы».");
            Console.WriteLine("3. Мне только спросить!");
            Console.WriteLine("4. Высший сорт.");
            Console.WriteLine("5. Слон из мухи.");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        break;
                    }
                case "2":
                    {
                        break;
                    }
                case "3":
                    {
                        break;
                    }
                case "4":
                    {
                        break;
                    }
                case "5":
                    {
                        TestTask.Task_five();
                        break;
                    }
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            menu();
        }
    }
}