using System;
using System.Collections.Generic;
using System.Text;

namespace Concurrency_ProducerConsumer
{
    public class IntBuffer
    {
        private int index;
        private int[] buffer;

        public IntBuffer()
        {
            buffer = new int[8];
        }

        public void Add(int num)
        {
            while (true)
            {
                if (index < buffer.Length)
                {
                    buffer[index++] = num; //increments after operation
                    return;
                }
            }
        }

        public int remove()
        {
            while (true)
            {
                if (index > 0)
                {
                    return buffer[--index]; //decrements index, then performs operation
                }
            }
        }
    }
}
