using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static void Task_five()
        {
            TransformToElephant();
            Console.WriteLine("Муха");
            //... custom application code

        }
    }
}
