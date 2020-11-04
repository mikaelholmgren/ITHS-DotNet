using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HarborLib
{
   static public class Extensions
    {
        /// <summary>
        /// This method is used to find consecutive series of a specified size in an array of ints
        /// </summary>
        /// <param name="src"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int FindConsecutives(this int[] src, int num)
        {
            int retval = -1;
            bool done = false;
            int curPos = 0;
            while (!done && curPos < src.Length)
            {
                if (curPos + (num - 1) >= src.Length) break;
                var range = Enumerable.Range(src[curPos], num);
                int i = 0;
                bool ok = true;
                foreach (var r in range)
                {

                    if (src[curPos + i] == r)
                    {
                        i++;

                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
                if (ok)
                {
                    retval = src[curPos];
                    done = true;
                }
                else curPos++;
            }
            return retval;
        }

    }
}
