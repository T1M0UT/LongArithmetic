using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic
{
    class LongNumber
    {
        private List<int> digits;

        public int Length { get => digits.Count; }

        public LongNumber(List<int> digits)
        {
            digits.Reverse();
            this.digits = digits;
        }

        public LongNumber(int number)
        {
            digits = new List<int>();
            if (number == 0)
            {
                digits.Add(0);
            }

            while (number > 0)
            {
                int lastDigit = number % 10;
                digits.Add(lastDigit);
                number /= 10;
            }
        }
        public static implicit operator String(LongNumber number)
        {
            return string.Join("",number.digits);
        }
        public static implicit operator LongNumber(int number)
        {
            return new LongNumber(number);
        }
        /*public static LongNumber operator >(LongNumber a, LongNumber b)
        {
            return a - b > 0;
        }*/
        public static LongNumber operator +(LongNumber a, LongNumber b)
        {
            LongNumber bigger = a.Length > b.Length ? a : b;
            LongNumber smaller = a.Length <= b.Length ? a : b;

            List<int> result = new List<int>();
            int brain = 0;

            for (int i = 0; i < bigger.Length; i++)
            {
                int sum;
                if (i < smaller.Length) sum = bigger.digits[i] + smaller.digits[i] + brain;
                else sum = bigger.digits[i] + brain;
                brain = sum / 10;
                int newDigit = sum % 10;

                result.Add(newDigit);
            }

            if (brain != 0)
                result.Add(brain);

            return new LongNumber(result);
        }
        public static LongNumber operator *(LongNumber a, LongNumber b)
        {
            LongNumber bigger = a.Length > b.Length ? a : b;
            LongNumber smaller = a.Length <= b.Length ? a : b;

            List<int> result = new List<int>();
            int brain = 0;

            for (int i = 0; i < bigger.Length; i++)
            {
                for (int j = 0; j < smaller.Length; j++)
                {
                    int current;
                    if (i < smaller.Length) current = (bigger.digits[i] * smaller.digits[j]) + brain;
                    else current = bigger.digits[i] + brain;
                    brain = current / 10;
                    int newDigit = current % 10;

                    result.Add(newDigit);
                }
            }

            if (brain != 0)
                result.Add(brain);

            return new LongNumber(result);
        }
    }
}
