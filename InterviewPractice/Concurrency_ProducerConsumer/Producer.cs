using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Concurrency_ProducerConsumer
{
    public class Producer
    {
        private IntBuffer buffer;
        private int prodRound = 0;

        public Producer(IntBuffer _buffer)
        {
            buffer = _buffer;
        }

        public void Run()
        {
            Random rand = new Random();
            int num = Convert.ToInt32(rand.NextDouble() * 15);
            bool added = buffer.Add(num);
            while (added)
            {
                prodRound++;
                Console.WriteLine(String.Format("Production Round: {0}\tProduced: {1}", prodRound, num));
                num = Convert.ToInt32(rand.NextDouble() * 15);
                added = buffer.Add(num);
            }
        }


    }
}
