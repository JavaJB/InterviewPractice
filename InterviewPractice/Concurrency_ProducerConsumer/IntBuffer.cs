using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Concurrency_ProducerConsumer
{
    public class IntBuffer
    {
        private int index;
        private int[] buffer;
        private bool producing = false;

        public IntBuffer()
        {
            buffer = new int[8];
            index = 0;
        }

        public bool Add(int num)
        {
            lock(buffer)
            {
                if(index == buffer.Length - 1)
                {
                    producing = false;
                    Monitor.PulseAll(buffer); //wake any blocked consume
                    Console.WriteLine("Waiting in add block.");
                    Monitor.Wait(buffer, 1000);
                    Console.WriteLine("No longer waiting in add block");
                    Thread.Sleep(200);
                    return producing;
                }
                producing = true;
                buffer[index++] = num; //increments after operation
                //Monitor.PulseAll(buffer); //wake any blocked consume
                //Monitor.Wait(buffer);
                return producing;
            }
        }

        public int remove()
        {
            lock(buffer)
            {
                int ret = -1;
                if(index == 0)
                {
                    producing = true; //switching to producing
                    ret = buffer[index];
                    Monitor.PulseAll(buffer);
                    Console.WriteLine("Waiting in remove block.");
                    Monitor.Wait(buffer, 1000);
                    Console.WriteLine("No longer waiting in remove block");
                    Thread.Sleep(200);
                    return ret;
                }
                ret = buffer[index--]; //removes item from current index and then decrements
                Monitor.PulseAll(buffer);
                return ret;
            }
        }

        public bool isProducing()
        {
            return producing;
        }
    }
}
