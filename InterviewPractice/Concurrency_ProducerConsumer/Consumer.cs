using System;
using System.Collections.Generic;
using System.Text;

namespace Concurrency_ProducerConsumer
{
    public class Consumer
    {
        private IntBuffer buffer;
        private int consRound = 0;

        public Consumer(IntBuffer _buffer)
        {
            buffer = _buffer;
        }

        public void Run()
        {
            while (!buffer.isProducing())
            {
                int num = buffer.remove();
                consRound++;
                Console.WriteLine(String.Format("Consumption Round: {0}\tConsumed: {1}", consRound, num));
            }
        }
    }
}
