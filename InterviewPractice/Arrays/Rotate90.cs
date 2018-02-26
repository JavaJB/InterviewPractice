using System;

namespace Arrays
{
    public class Rotate90
    {
        int[][] array;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_array"></param>
        public Rotate90(int[][] _array)
        {
            array = _array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        private void rotate(int[][] array)
        {
            int remainder = array.Length % 2;
            int bound = 0;
            switch (remainder)
            {
                case 0: //even
                    bound = array.Length / 2 - 1;
                    Swap("Q2", bound);
                    Swap("Q3", bound);
                    Swap("Q4", bound);
                    break;
                case 1: //odd
                    bound = (array.Length - 1) / 2;
                    for (int i = 0; i < bound; i++)
                    {
                        Swap("Q2", bound);
                        Swap("Q3", bound);
                        Swap("Q4", bound);
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quadrant"></param>
        private void Swap(string quadrant, int bound)
        {
            switch(quadrant)
            {
                case "Q2":
                    break;
                case "Q3":
                    break;
                case "Q4":
                    break;
                default:
                    break;
            }
            return;
        }
    }
}
