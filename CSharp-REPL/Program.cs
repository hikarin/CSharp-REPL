using System;
using System.Reflection;
using Mono.CSharp;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var evaluator = new Evaluator(
                new CompilerContext(
                    new CompilerSettings(),
                    new ConsoleReportPrinter()));
            evaluator.ReferenceAssembly(Assembly.GetExecutingAssembly());
            evaluator.Run("using System; using ConsoleApp1;");

            var input = "";
            while (input != "exit")
            {
                Console.Write(">");
                input = Console.ReadLine();

                if (input.EndsWith(";"))
                {
                    var result = evaluator.Run(input);
                    Console.WriteLine(result);
                }
                else
                {
                    object result;
                    bool result_set;
                    var ret = evaluator.Evaluate(input, out result, out result_set);
                    if (result != null)
                    {
                        Console.WriteLine("{0}", result);
                    }
                }
            }
        }
    }
}