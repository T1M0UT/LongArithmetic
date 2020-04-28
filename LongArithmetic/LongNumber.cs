using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongArithmetic
{
    public class LongNumber
    {
        private List<int> digits;
        public int Length { get => digits.Count; }
        public bool IsPositive { get; private set; }
        public LongNumber(List<int> input, bool IsPositive)
        {
            digits = new List<int>(input);
            this.IsPositive = IsPositive;
        }
        public LongNumber(in string number)
        {
            char[] arr = number.ToCharArray();
            digits = new List<int>();
            if (arr.Length >= 1 && arr[0] == '-') IsPositive = false;
            else
            {
                IsPositive = true;
                digits.Add(arr[0]-'0');
            }
            for (int i=arr.Length-1; i>0; i--)
            {
                
                digits.Add(arr[i]-'0');
            }
        }
        public LongNumber(int number)
        {
            if (number < 0)
            {
                IsPositive = false;
                number *= -1;
            }
            else IsPositive = true;
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
        
        private static LongNumber Plus(LongNumber a, LongNumber b, bool znakRes)
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

            return new LongNumber(result, znakRes);

        }
        private static LongNumber Minus(in LongNumber a, in LongNumber b, bool znakRes)
        {
            LongNumber bigger;
            LongNumber smaller;
            if (a.Length != b.Length)
            {
                bigger = a.Length > b.Length ? a : b;
                smaller = a.Length < b.Length ? a : b;
            }
            else
            {
                bigger = Bigger(a, b) ? a : b;
                smaller = Bigger(a, b) ? b : a;
            }
            List<int> result = new List<int>();
            int brain = 0;
            for (int i = 0; i < bigger.Length; i++)
            {
                int divide;
                if (i < smaller.Length)
                {
                    if (bigger.digits[i] >= smaller.digits[i])
                        divide = bigger.digits[i] - smaller.digits[i];
                    else
                    {
                        bigger.digits[i + 1] -= 1;
                        divide = bigger.digits[i] + 10 - smaller.digits[i];
                    }
                }
                else divide = bigger.digits[i];
                brain = divide / 10;
                int newDigit = divide % 10 + brain;

                result.Add(newDigit);
            }

            if (brain != 0)
                result.Add(brain);
            return NaxuiNuli(new LongNumber(result, znakRes));
        }
        private static LongNumber NaxuiNuli(LongNumber a)
        {
            for (int i = a.Length - 1; i > 0 ; i--)
            {
                if (a.digits[i] == 0) a.digits.RemoveAt(i);
                else return a;
            }
            if (a.Length==1 && a.digits[0]==0)
            {
                a.IsPositive = true;
                a = 0;
            }
            return a;
        }
        public override string ToString()
        {
            List<int> output = new List<int>(digits);
            output.Reverse();
            return (IsPositive ? "" : "-") + string.Join("", output);
        }
        public static implicit operator LongNumber(int number)
        {
            return new LongNumber(number);
        }

        public static LongNumber operator +(LongNumber a, LongNumber b)
        {
            bool znak = Bigger(a, b);
            if (!a.IsPositive && b.IsPositive) return Minus(b, a, znak);
            if (a.IsPositive && !b.IsPositive) return Minus(a, b, znak);
            return Plus(a, b, znak);
        }
        public static LongNumber operator -(LongNumber a, LongNumber b)
        {
            bool znak = Bigger(a, b);
            if (a.IsPositive && !b.IsPositive) return Plus(a, b, znak);
            if (!a.IsPositive && b.IsPositive) return Plus(b, a, znak);
            return Minus(a, b, znak);
        }
        private static bool Bigger(in LongNumber a, in LongNumber b)
        {
            if (a.IsPositive && !b.IsPositive) return true;
            if (!a.IsPositive && b.IsPositive) return false;
            if (a.Length > b.Length && a.IsPositive) return true;
            if (a.Length > b.Length && !a.IsPositive) return false;
            if (a.Length < b.Length && b.IsPositive) return false;
            if (a.Length < b.Length && !b.IsPositive) return true;
            if (a.IsPositive)
            {
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    if (a.digits[i] > b.digits[i]) return true;
                    if (a.digits[i] < b.digits[i]) return false;
                }
                return false; //Equals
            }
            else
            {
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    if (a.digits[i] > b.digits[i]) return false;
                    if (a.digits[i] < b.digits[i]) return true;
                }
                return false;    //Equals
            }
        }

        public override bool Equals(object obj)
        {
            return obj is LongNumber number &&
                   EqualityComparer<List<int>>.Default.Equals(digits, number.digits) &&
                   Length == number.Length &&
                   IsPositive == number.IsPositive;
        }

        public override int GetHashCode()
        {
            var hashCode = -1224394762;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<int>>.Default.GetHashCode(digits);
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            hashCode = hashCode * -1521134295 + IsPositive.GetHashCode();
            return hashCode;
        }

        public static bool operator >(LongNumber a, LongNumber b)
        {
            return Bigger(a, b);
        }
        public static bool operator <(LongNumber a, LongNumber b)
        {
            return Bigger(b, a);
        }
        public static bool operator >=(LongNumber a, LongNumber b)
        {
            if (Bigger(a, b)) return true;
            return !Bigger(b, a);
        }
        public static bool operator <=(LongNumber a, LongNumber b)
        {
            if (!Bigger(a, b)) return true;
            return false;
        }

        public static bool operator !=(LongNumber a, LongNumber b)
        {
            return !(a == b);
        }
        public static bool operator ==(LongNumber a, LongNumber b)
        {
            return !Bigger(a, b) && !Bigger(b, a);
        }
        public static LongNumber operator ++(LongNumber a)
        {
            return a += 1;
        }
        public static LongNumber operator *(LongNumber a, int b)
        {
            bool znakRes = b < 0 ? false : true;
            if (b < 0) b *= -1;
            if (!a.IsPositive) znakRes = !znakRes;
            LongNumber zeroHelper = 1;
            LongNumber brain = 0;
            for (int i = 0; i < a.Length; i++)
            {
                brain += (a.digits[i] * b) * zeroHelper;
                zeroHelper *= 10;
            }  
            LongNumber c = new LongNumber(brain.digits,znakRes);
            return c;
        }
        public static LongNumber operator *(LongNumber a, LongNumber b)
        {
            bool znakRes = a.IsPositive? true : false;
            if (!b.IsPositive) znakRes = !znakRes;
            LongNumber zeroHelper = 1;
            LongNumber brain = 0;
            for (int i = 0; i < a.Length; i++)
            {
                LongNumber innerZeroHelper = zeroHelper;
                for (int j = 0; j < b.Length; j++)
                {
                    brain += (a.digits[i] * b.digits[j]) * innerZeroHelper;
                    innerZeroHelper *= 10;
                }
                zeroHelper *= 10;
            }
            LongNumber c = new LongNumber(brain.digits,znakRes);
            return c;
        }
        public static LongNumber operator /(LongNumber a, LongNumber b)
        {
            bool znakRes = a.IsPositive ? true : false;
            if (!b.IsPositive) znakRes = !znakRes;
            LongNumber result=0;
            LongNumber e = new LongNumber(a.digits,znakRes); 
            while (e>=b)
            {
                e -= b;
                result++;
            }
            return result;
        }
        public static LongNumber operator %(LongNumber a, LongNumber b)
        {
            bool znakRes = a.IsPositive ? true : false;
            if (!b.IsPositive) znakRes = !znakRes;
            LongNumber result = 0;
            LongNumber e = new LongNumber(a.digits, znakRes);
            while (e >= b)
            {
                e -= b;
                result++;
            }
            return e;
        }


    }
}
