using System;
using System.Reflection;
using Mono.CSharp;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _stringBuilder = new StringBuilder();
            var _writer = new StringWriter(_stringBuilder);
            var settings = new CompilerSettings();
            var printer = new StreamReportPrinter(_writer);
            var context = new CompilerContext(settings, printer);
            var evaluator = new Evaluator(context);
            evaluator.ReferenceAssembly(Assembly.GetExecutingAssembly());

            var input = "";
            while (input != "exit")
            {
                Console.Write(">");
                input = Console.ReadLine();

                if (input.EndsWith(";"))
                {
                    var result = evaluator.Run(input);
                    if (printer.ErrorsCount > 0 || printer.WarningsCount > 0)
                    {
                        Console.WriteLine(_stringBuilder);
                        _stringBuilder.Clear();
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }
                else
                {
                    object result;
                    bool result_set;
                    var ret = evaluator.Evaluate(input, out result, out result_set);
                    if (printer.ErrorsCount > 0 || printer.WarningsCount > 0)
                    {
                        Console.WriteLine(_stringBuilder);
                        _stringBuilder.Clear();
                    }
                    else if (result != null)
                    {
                        Console.WriteLine(result);
                    }
                }
            }
        }
    }
}