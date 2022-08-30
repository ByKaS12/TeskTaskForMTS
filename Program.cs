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
                        TestTask.Task_One();
                        break;
                    }
                case "2":
                    {
                        TestTask.Task_Two();
                        break;
                    }
                case "3":
                    {
                        TestTask.Task_Three();
                        break;
                    }
                case "4":
                    {

                        TestTask.Task_Four();
                        break;
                    }
                case "5":
                    {
                        TestTask.Task_Five();
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