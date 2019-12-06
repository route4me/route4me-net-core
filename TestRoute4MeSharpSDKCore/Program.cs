using System;

namespace TestRoute4MeSharpSDKCore
{
    class Program
    {
        /// <summary>
        /// Make sure to replace the 11111111111111111111111111111111 (32 characters) demo API key with your API key.
        /// With the demo API key, the Route4Me system provides only limited functionality.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            RunExamples runExamples = new RunExamples();


            runExamples.c_ApiKey = "11111111111111111111111111111111";

            runExamples.SingleDriverRoute10Stops();

            Console.ReadKey();
        }
    }
}
