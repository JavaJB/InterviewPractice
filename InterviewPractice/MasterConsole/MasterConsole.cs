using System;
using Edges;
using Concurrency_ProducerConsumer;

namespace MasterConsole
{
    public class MasterConsole
    {
        private static IntBuffer buff;
        private static Producer prod; 
        private static Consumer cons;

        public static void Main(string[] args)
        {
            buff = new IntBuffer();
            prod = new Producer(buff);
            cons = new Consumer(buff);
            prod.Run();
            cons.Run();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
