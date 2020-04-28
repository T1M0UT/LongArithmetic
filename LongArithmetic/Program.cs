using System;
using System.Collections.Generic;
namespace LongArithmetic
{

    class Program
    {
        public static void Main()
        {
            LongNumber a = new LongNumber(new List<int>() { 0, 0, 1 }, true);
            LongNumber c = new LongNumber(new List<int>() { 0, 2 }, true);
            // int b = 200;
            // LongNumber d = new LongNumber(new List<int>() { 0, 1 }, true);
            // d += b;
            LongNumber q = new LongNumber("-123");
            Console.WriteLine(Factorial(10));
        }

        public static LongNumber Factorial(int inputValue)
        {
            LongNumber[] fact = new LongNumber[inputValue + 1];
            fact[0] = new LongNumber("0");
            fact[1] = new LongNumber("1");
            fact[2] = new LongNumber("2");
            LongNumber j = new LongNumber("3");
            for (int i = 3; i <= inputValue - 2; i++)
            {

                fact[i] = fact[i - 1] * j;
            }
            return fact[inputValue - 2];
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