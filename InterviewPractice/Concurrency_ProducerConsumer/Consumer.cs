using System;
using System.Collections.Generic;
using System.Text;

namespace Concurrency_ProducerConsumer
{
    public class Consumer
    {
        private IntBuffer buffer;
        public Consumer(IntBuffer _buffer)
        {
            buffer = _buffer;
        }

        public void Run()
        {
            while (true)
            {
                int num = buffer.remove();
                System.Console.WriteLine(String.Format("Consumed: {0}", num));
            }
        }
    }
}
