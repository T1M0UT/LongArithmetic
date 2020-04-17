//345678
//[8,7,6,5,4,3]
using System;
using System.Collections.Generic;
namespace LongArithmetic
{

    class Program
    {
        public static void Main()
        {
            List<int> q = new List<int>() { 1 };
            LongNumber a = new LongNumber(new List<int>() { 1,2 });
            int b = 2;
            Console.WriteLine(a*b);
        }
    }

    /*
    // 6 
    LongNumber* int
        LongNumber* LongNumber

        // 9 
        > < >= <= == !=
        -
        /

        //12
        в качестве "цифры" будет uint */
}