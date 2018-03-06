using System;


namespace Concurrency_ProducerConsumer
{
    public class Producer_Consumer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            IntBuffer buff = new IntBuffer();
            Producer prod = new Producer(buff);
            Consumer cons = new Consumer(buff);
            prod.Run();
            cons.Run();
        }
    }
}
