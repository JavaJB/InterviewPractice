using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Concurrency_ProducerConsumer
{
    public class Producer
    {
        private IntBuffer buffer;

        public Producer(IntBuffer _buffer)
        {
            buffer = _buffer;
        }

        public void Run()
        {
            Random rand = new Random();
            while (true)
            {
                int num = Convert.ToInt32(rand.NextDouble() * 15);
                buffer.Add(num);
                System.Console.WriteLine(String.Format("Produced: {0}", num));
            }
        }


    }
}
