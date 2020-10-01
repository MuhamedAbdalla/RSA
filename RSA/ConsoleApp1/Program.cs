using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    #region App
    class Program
    {
        #region Complex Class
        public class Complex
        {
            public double re, im;

            public Complex() { }

            public Complex(double _re, double _im)
            {
                re = _re;
                im = _im;
            }

            public static bool operator !=(Complex z1, Complex z2)
            {
                return (z1.re != z2.re || z1.im != z2.im);
            }

            public static bool operator ==(Complex z1, Complex z2)
            {
                return (z1.re == z2.re && z1.im == z2.im);
            }

            static public Complex operator /(Complex z, double dval)
            {
                Complex z2 = new Complex();
                z2.re = z.re / dval;
                z2.im = z.im / dval;
                return z2;
            }

            public static Complex operator /(Complex z1, Complex z2)
            {
                double value = z2.re * z2.re + z2.im * z2.im;

                return new Complex(
                    (z1.re * z2.re + z1.im * z2.im) / value,
                    (z1.im * z2.re - z1.re * z2.im) / value);
            }

            public static Complex operator *(Complex A, Complex B)
            {
                return new Complex(
                    A.re * B.re - A.im * B.im,
                    A.re * B.im + A.im * B.re);
            }

            static public Complex operator *(Complex z, double dval)
            {
                Complex z2 = new Complex();
                z2.re = (z.re * dval);
                z2.im = (z.im * dval);
                return z2;
            }

            public static Complex operator +(Complex num1, Complex num2)
            {
                Complex complex = new Complex();
                complex.re = num1.re + num2.re;
                complex.im = num1.im + num2.im;
                return complex;
            }

            public static Complex operator -(Complex num1, Complex num2)
            {
                Complex complex = new Complex();
                complex.re = num1.re - num2.re;
                complex.im = num1.im - num2.im;
                return complex;
            }
        }
        #endregion

        #region Pair
        class Mpair
        {
            public List<char> Key, Value;

            public Mpair()
            {
                Key = new List<char>();
                Value = new List<char>();
            }

            public Mpair(List<char> big, List<char> rbig)
            {
                Key = big;
                Value = rbig;
            }
        }
        #endregion

        #region FCI_d7k_BOUNS
        class num
        {
            public List<char> remA;
            public List<char> remB;
            public List<char> gcd;

            public num()
            {
                remA = new List<char>();
                gcd = new List<char>();
                remB = new List<char>();
            }

            static public num operator +(num val1, num val2)
            {
                num res = new num();
                res.remA = val2.remA.ToList();
                res.remB = val2.remB.ToList();
                res.gcd = val2.gcd.ToList();
                return res;
            }
        }
        #endregion

        #region BigInteger
        class BigInteger
        {
            //------------------------------------------------------
            //    Variables
            //------------------------------------------------------

            private Dictionary<char, int> dic = new Dictionary<char, int>();
            private List<char> rdic = new List<char>();

            //------------------------------------------------------
            //    Inialization
            //------------------------------------------------------

            public BigInteger()
            {
                char j = '0';
                for (int i = 0; i <= 9; i++, j++)
                {
                    dic.Add(j, i);
                }
                j = '0';
                for (int i = 0; i <= 9; i++, j++)
                {
                    rdic.Add(j);
                }
            }

            #region Bouns
            num exgcd(List<char> x, List<char> y)
            {
                num res = new num();
                if (BigInteger.CompareTo("0".ToList(), y) == 0)
                {
                    res.remA = "1".ToList();
                    res.remB = "0".ToList();
                    res.gcd = x.ToList();
                    return res;
                }
                if (equal(y, "2".ToList()))
                {
                    res = res + exgcd(y, longDivision(x, 2).Value);
                }
                else
                {
                    res = res + exgcd(y, module(x, y));
                }
                num res2 = new num();
                res2.remA = res.remB.ToList();
                res2.remB = Subtract(res.remA, Logmultiply(divide(x, y), res.remB));
                res2.gcd = res.gcd;
                return res2;
            }

            public void GetKey()
            {
                num ret = new num();
                List<char> p = "961748941".ToList();
                List<char> q = new List<char>();
                q.Add('9');
                q.Add('8');
                q.Add('2');
                q.Add('4');
                q.Add('5');
                q.Add('1');
                q.Add('6');
                q.Add('5');
                q.Add('3');
                List<char> n = Logmultiply(p, q);
                p = Subtract(p, "1".ToList());
                q = Subtract(q, "1".ToList());
                List<char> phi = Logmultiply(p, q);
                List<char> e = "2".ToList();
                while (less(e, phi))
                {
                    num val = exgcd(e.ToList(), phi.ToList());
                    if (equal(val.gcd, "1".ToList()))
                    {
                        break;
                    }
                    else
                    {
                        e = Add(e, "1".ToList());
                    }
                }
                List<char> d = divide(Add("1".ToList(), Logmultiply("2".ToList(), phi)), e);
                List<char> message = "20".ToList();
                List<char> c = modPower(message, e, n);
                List<char> m = modPower(c, d, n);
                Console.Write("Public Key Is (e, n) --> (");
                for (int i = 0; i < e.Count; i++)
                {
                    Console.Write(e[i]);
                }
                Console.Write(", ");
                for (int i = 0; i < n.Count; i++)
                {
                    Console.Write(n[i]);
                }
                Console.WriteLine(")");
            }
            #endregion

            //------------------------------------------------------
            //    Comparsion between two string
            //    return 1-->Greater
            //           0-->Equal
            //          -1-->Less
            //------------------------------------------------------

            public static int CompareTo(List<char> val1, List<char> val2)
            {
                int CharDifference = val1.Count - val2.Count;
                if (CharDifference < 0)
                {
                    return -1;
                }
                else if (CharDifference > 0)
                {
                    return 1;
                }
                else
                {
                    for (int i = 0; i < val1.Count; i++)
                    {
                        int ThisValue = val1[i] - '0';
                        int ArgValue = val2[i] - '0';
                        if (ThisValue > ArgValue)
                        {
                            return 1;
                        }
                        else if (ThisValue < ArgValue)
                        {
                            return -1;
                        }
                    }
                }
                return 0;
            }

            //------------------------------------------------------
            //    Comparsion between two string
            //    return true if it -1
            //           false otherwise
            //------------------------------------------------------

            private bool less(List<char> val1, List<char> val2)
            {
                return (CompareTo(val1, val2) == -1);
            }

            public bool equal(List<char> val1, List<char> val2)
            {
                return (CompareTo(val1, val2) == 0);
            }

            //------------------------------------------------------------------
            //     Remove leading zeros from parametrized string by reference
            //------------------------------------------------------------------

            private static void RemoveLeadingZero(ref List<char> result)
            {
                int indx = 0;
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i] != '0')
                    {
                        break;
                    }
                    else
                    {
                        indx++;
                    }
                }
                result.RemoveRange(0, Math.Min(indx, result.Count - 1));
            }

            //------------------------------------------------------------------
            //     Append Zeros In last
            //------------------------------------------------------------------

            private List<char> AppendZeros(List<char> Str, int length)
            {
                if (length > 0)
                {
                    List<char> free = new List<char>();
                    for (int i = 0; i < length; i++)
                    {
                        free.Add('0');
                    }
                    for (int i = 0; i < Str.Count; i++)
                    {
                        free.Add(Str[i]);
                    }
                    return free;
                }
                return Str;
            }

            //---------------------------------------------------------------
            //     ADD two number Anothor One
            //     Parameter is the first string (lhs), Second string (rhs)
            //     return Is Summation (result)
            //---------------------------------------------------------------

            public List<char> Add(List<char> lhs, List<char> rhs)
            {
                int length = Math.Max(lhs.Count, rhs.Count), carry = 0;
                List<char> result = new List<char>(length);

                lhs = AppendZeros(lhs, length - lhs.Count);
                rhs = AppendZeros(rhs, length - rhs.Count);

                char[] array = new char[length + 5];
                int indx = 0;

                for (int i = lhs.Count - 1; i >= 0; i--)
                {
                    carry += (lhs[i] - '0');
                    carry += (rhs[i] - '0');
                    int rem = carry % 10;
                    array[indx] = rdic[rem];
                    indx++;
                    carry /= 10;
                }
                if (carry != 0)
                {
                    array[indx] = rdic[carry];
                    indx++;
                }
                for(int i = indx - 1; i >= 0; i--)
                {
                    result.Add(array[i]);
                }
                RemoveLeadingZero(ref result);
                return result;
            }

            //---------------------------------------------------------------
            //     Subtract two number Anothor One
            //     Parameter is the first string (ls), Second string (rs)
            //     return Is Summation (result)
            //---------------------------------------------------------------

            public List<char> Subtract(List<char> lhs, List<char> rhs)
            {
                int length = Math.Max(lhs.Count, rhs.Count), diff;
                List<char> result = new List<char>(length + 5);

                lhs = AppendZeros(lhs, length - lhs.Count);
                rhs = AppendZeros(rhs, length - rhs.Count);

                char[] array = new char[length + 5];
                int indx = 0;

                for (int i = length - 1; i >= 0; i--)
                {
                    diff = (lhs[i] - '0') - (rhs[i] - '0');
                    if (diff >= 0)
                    {
                        array[indx] = rdic[diff];
                        indx++;
                    }
                    else
                    {
                        int j = i - 1;
                        while (j >= 0)
                        {
                            int num1 = (dic[lhs[j]] - 1) % 10 + 48;
                            lhs[j] = (char)num1;
                            if (lhs[j] != '9')
                            {
                                break;
                            }
                            else
                            {
                                j--;
                            }
                        }
                        array[indx] = rdic[diff + 10];
                        indx++;
                    }
                }

                for (int i = indx - 1; i >= 0; i--)
                {
                    result.Add(array[i]);
                }

                RemoveLeadingZero(ref result);

                return result;
            }

            //-------------------------------------------------------------------------
            //     Multiply two number Anothor One recursively By Karatsuba Algorithm
            //     Parameter is the first string (ls), Second string (rs)
            //     return Is Summation (result)
            //-------------------------------------------------------------------------

            private List<char> Slow_multiply_fci_d7k(List<char> lhs, List<char> rhs)
            {
                int length = Math.Max(lhs.Count, rhs.Count);
                lhs = AppendZeros(lhs, length - lhs.Count);
                rhs = AppendZeros(rhs, length - rhs.Count);
                if (length == 1)
                {
                    return new List<char>(((lhs[0] - '0') * (rhs[0] - '0')).ToString());
                }
                List<char> Half_FA = new List<char>(length / 2);
                List<char> Half_SA = new List<char>(length / 2);
                List<char> Half_FB = new List<char>(length / 2);
                List<char> Half_SB = new List<char>(length / 2);
                for (int i = 0; i < length / 2; i++)
                {
                    Half_FA.Add(lhs[i]);
                    Half_FB.Add(rhs[i]);
                }
                for (int i = length / 2; i <= length - length / 2; i++)
                {
                    Half_SA.Add(lhs[i]);
                    Half_SB.Add(rhs[i]);
                }

                List<char> FA_FB = Slow_multiply_fci_d7k(Half_FA, Half_FB); //b * d
                List<char> SA_SB = Slow_multiply_fci_d7k(Half_SA, Half_SB); //a * c
                List<char> SA_SB_FA_FB = Slow_multiply_fci_d7k(Add(Half_FA, Half_SA), Add(Half_FB, Half_SB)); //z = (a + b) * (c + d) = ac + bd + bc + ad
                List<char> Sub = Subtract(SA_SB_FA_FB, Add(FA_FB, SA_SB)); // z -= ((b * d) + (a * c))

                for (int i = 0; i < 2 * (length - length / 2); i++)
                {
                    FA_FB.Add('0');
                }
                for (int i = 0; i < length - length / 2; i++)
                {
                    Sub.Add('0');
                }
                List<char> result = Add(Add(FA_FB, SA_SB), Sub); //b * d + a * c + (bc + ad) = res
                RemoveLeadingZero(ref result);
                return result;
            }

            private Mpair divideAndMod(List<char> lhs, List<char> rhs)
            {
                if (less(lhs, rhs))
                {
                    Mpair mpair = new Mpair("0".ToList(), lhs);
                    return mpair;
                }
                Mpair tmp = divideAndMod(lhs, Add(rhs, rhs));
                tmp = new Mpair(Add(tmp.Key, tmp.Key), tmp.Value);
                if (less(tmp.Value, rhs))
                {
                    return tmp;
                }
                tmp = new Mpair(Add(tmp.Key, "1".ToList()), Subtract(tmp.Value, rhs));
                return tmp;
            }

            public Mpair rdivideAndMod(List<char> lhs, List<char> rhs)
            {
                return divideAndMod(lhs, rhs);
            }

            public List<char> divide(List<char> lhs, List<char> rhs)
            {
                return divideAndMod(lhs, rhs).Key;
            }

            public List<char> module(List<char> lhs, List<char> rhs)
            {
                List<char> ret = divideAndMod(lhs, rhs).Value;
                if (ret.Count == 0)
                {
                    return "0".ToList();
                }
                return ret;
            }

            public List<char> modPower(List<char> value, List<char> power, List<char> mod)
            {
                if (power[0] == '0' && power.Count == 1)
                {
                    return ("1".ToList());
                }
                value = module(value, mod);
                Mpair ans = longDivision(power, 2);
                List<char> res = modPower(value, ans.Key, mod);
                res = module(res, mod);
                res = new List<char>(Logmultiply(res, res));
                res = module(res, mod);
                if (ans.Value.Count == 1 && ans.Value[0] == '1')
                {
                    res = new List<char>(Logmultiply(res, value));
                }
                res = module(res, mod);
                return res;
            }

            public static void fft(ref List<Complex> a, bool invert)
            {
                int n = a.Count;
                if (n == 1)
                {
                    return;
                }

                List<Complex> a0 = new List<Complex>();
                List<Complex> a1 = new List<Complex>();

                for (int i = 0; i < n / 2; i++)
                {
                    a0.Add(new Complex(0, 0));
                    a1.Add(new Complex(0, 0));
                }

                for (int i = 0; 2 * i < n; i++)
                {
                    a0[i] = a[2 * i];
                    a1[i] = a[2 * i + 1];
                }

                fft(ref a0, invert);
                fft(ref a1, invert);

                double ang = 2 * Math.PI / n * (invert == true ? -1 : 1);

                Complex w = new Complex(1, 0);
                Complex wn = new Complex(Math.Cos(ang), Math.Sin(ang));
                for (int i = 0; 2 * i < n; i++)
                {
                    a[i] = (a0[i] + (w * a1[i]));
                    a[i + n / 2] = (a0[i] - (w * a1[i]));
                    if (invert == true)
                    {
                        a[i] /= 2.0;
                        a[i + n / 2] /= 2.0;
                    }

                    w *= wn;
                }
            }

            public static List<int> multiplyFFT(ref List<int> a, ref List<int> b)
            {
                int n = 1;
                while (n < a.Count + b.Count)
                {
                    n <<= 1;
                }

                List<Complex> fa = new List<Complex>(Enumerable.Repeat(new Complex(0.0, 0.0), n)),
                    fb = new List<Complex>(Enumerable.Repeat(new Complex(0.0, 0.0), n)),
                    fourierResult = new List<Complex>(Enumerable.Repeat(new Complex(0.0, 0.0), n));

                for (int i = 0; i < Math.Max(a.Count, b.Count); i++)
                {
                    if (i < a.Count)
                    {
                        fa[i] = new Complex(a[i], 0);
                    }
                    if (i < b.Count)
                    {
                        fb[i] = new Complex(b[i], 0);
                    }
                }

                fft(ref fa, false);
                fft(ref fb, false);

                for (int i = 0; i < n; i++)
                {
                    fourierResult[i] = fa[i] * fb[i];
                }

                fft(ref fourierResult, true);

                List<int> res = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    res.Add(0);
                }

                for (int i = 0; i < n; i++)
                {
                    res[i] = (int)Math.Round(fourierResult[i].re);
                }

                int carry = 0;
                for (int i = 0; i < n; i++)
                {
                    res[i] += carry;
                    carry = res[i] / 10;
                    res[i] %= 10;
                }

                return res;
            }

            public static List<int> fromStrToListReverse(List<char> s)
            {
                int n = s.Count;
                List<int> arr = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    arr.Add(0);
                }

                for (int i = 0; i < n; i++)
                {
                    arr[i] = s[n - i - 1] - '0';
                }

                return arr;
            }

            public static List<int> reverseAndRemove(List<int> s)
            {
                int lst = s.Count - 1;

                while (lst > 0 && s[lst] == 0)
                {
                    lst -= 1;
                }

                List<int> res = new List<int>();
                for (int i = 0; i <= lst; i++)
                {
                    res.Add(0);
                }

                while (lst >= 0)
                {
                    res[lst] = s[lst];
                    lst -= 1;
                }

                return res;
            }

            public List<char> Logmultiply(List<char> a, List<char> b)
            {
                List<int> lhs = fromStrToListReverse(a);
                List<int> rhs = fromStrToListReverse(b);
                List<int> res = new List<int>();


                res = multiplyFFT(ref lhs, ref rhs);
                res = reverseAndRemove(res);
                List<char> ret = new List<char>();
                for (int i = 0, j = res.Count - 1; i < res.Count; i++, j--)
                {
                    ret.Add(rdic[res[j]]);
                }
                return ret;
            }

            public Mpair longDivision(List<char> number, int divisor)
            {
                List<char> module = new List<char>();
                if (dic[number[number.Count - 1]] % divisor == 0)
                {
                    module.Add('0');
                }
                else
                {
                    module.Add('1');
                }
                if(number.Count == 1)
                {
                    Mpair corner = new Mpair();
                    int rnum = (dic[number[0]] / 2);
                    corner.Key.Add(rdic[rnum]);
                    corner.Value = module;
                    return corner;
                }
                List<char> ans = new List<char>(); 
                int idx = 0;
                int temp = (int)(number[idx] - '0');
                while (temp < divisor)
                {
                    temp = temp * 10 + (int)(number[idx + 1] - '0');
                    idx++;
                }
                ++idx;
                while (number.Count > idx)
                {
                    ans.Add((char)(temp / divisor + '0'));
                    temp = (temp % divisor) * 10 + (int)(number[idx] - '0');
                    idx++;
                }
                ans.Add((char)(temp / divisor + '0'));
                if (ans.Count == 0) {
                    ans.Add('0');
                }
                Mpair ret = new Mpair(ans, module);
                return ret;
            }
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {


            int sum = 0;
            
            BigInteger oper = new BigInteger();

            oper.GetKey();

            Environment.Exit(0);

            string[] lines = System.IO.File.ReadAllLines(@"D:\Third Year\algo\[MS2 Tests] RSA\Complete Test\TestToLosse.txt");

            int tests = Convert.ToInt32(lines[0]);

            string docPath = @"D:\Third Year\algo\[MS2 Tests] RSA\Complete Test\out.txt";

            System.IO.File.WriteAllText(docPath, string.Empty);

            string[] Inline = new string[tests + 5];

            for (int i = 0, j = 1; i < tests; i++, j += 4)
            {
                List<char> mod = lines[j].ToList();
                List<char> power = lines[j + 1].ToList();
                List<char> val = lines[j + 2].ToList();
                var time1 = System.Environment.TickCount;

                List<char> ans = oper.modPower(val, power, mod);

                var time2 = System.Environment.TickCount;

                sum += Math.Abs(time1 - time2);

                using (StreamWriter outputFile = File.AppendText(docPath))
                {
                    foreach (char vs in ans)
                    {
                        outputFile.Write(vs);
                    }
                    outputFile.WriteLine();
                }
            }

            double sec = sum / 1000.0;
            double min = sec / 60.0;

            Console.WriteLine("Min : " + min);
            Console.WriteLine(" & Sec : " + sec);

        }
        #endregion
    }
    #endregion
}