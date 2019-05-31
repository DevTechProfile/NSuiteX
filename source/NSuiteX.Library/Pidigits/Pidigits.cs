using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace NSuiteX.Library.Pidigits
{
    public static class Pidigits
    {
        const int DigitsPerLine = 10;

        public static void RunSingleThread()
        {
            const int n = 10000;
            // var i = 1;

            var digits = GenDigits().Take(n);
            var numbers = new int[10];

            foreach (var d in digits)
            {
                numbers[d]++;
            }

            Console.WriteLine($"Deviation between min and max: {numbers.Max() - numbers.Min()}");

            //foreach (var d in digits)
            //{
            //    Console.Out.Write(d);
            //    if ((i % DigitsPerLine) == 0)
            //        Console.Out.WriteLine("\t:" + i);
            //    i++;
            //}            
        }

        private static IEnumerable<int> GenDigits()
        {
            var k = 1;
            var n1 = new GmpInteger(4);
            var n2 = new GmpInteger(3);
            var d = new GmpInteger(1);
            var u = new GmpInteger();
            var v = new GmpInteger();
            var w = new GmpInteger(0);

            while (true)
            {
                // digit
                u.Div(n1, d);
                v.Div(n2, d);

                if (u.Cmp(v) == 0)
                {
                    yield return u.IntValue();

                    // extract
                    u.Mul(u, -10);
                    u.Mul(u, d);
                    n1.Mul(n1, 10);
                    n1.Add(n1, u);
                    n2.Mul(n2, 10);
                    n2.Add(n2, u);
                }
                else
                {
                    // produce
                    var k2 = k * 2;
                    u.Mul(n1, k2 - 1);
                    v.Add(n2, n2);
                    w.Mul(n1, k - 1);
                    n1.Add(u, v);
                    u.Mul(n2, k + 2);
                    n2.Add(w, u);
                    d.Mul(d, k2 + 1);
                    k++;
                }
            }
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Mpz_t
    {
        public int _mp_alloc;
        public int _mp_size;
        public IntPtr ptr;
    }

    class GmpInteger
    {
        public GmpInteger()
        {
            mpz_init(ref pointer);
        }

        public GmpInteger(int value)
        {
            mpz_init(ref pointer);
            mpz_set_si(ref pointer, value);
        }

        public void Set(int value) => mpz_set_si(ref pointer, value);

        public void Mul(GmpInteger op1, GmpInteger op2) =>
            mpz_mul(ref pointer, ref op1.pointer, ref op2.pointer);

        public void Mul(GmpInteger src, int val) =>
            mpz_mul_si(ref pointer, ref src.pointer, val);

        public void Add(GmpInteger op1, GmpInteger op2) =>
            mpz_add(ref pointer, ref op1.pointer, ref op2.pointer);

        public void Sub(GmpInteger op1, GmpInteger op2) =>
            mpz_sub(ref pointer, ref op1.pointer, ref op2.pointer);

        public void Div(GmpInteger op1, GmpInteger op2) =>
            mpz_tdiv_q(ref pointer, ref op1.pointer, ref op2.pointer);

        public int IntValue() => mpz_get_si(ref pointer);

        public int Cmp(GmpInteger op1) =>
            mpz_cmp(ref pointer, ref op1.pointer);

        Mpz_t pointer;

        const string dllName = @"Ressources\gmp.dll";

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_init")]
        extern static void mpz_init(ref Mpz_t value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_mul")]
        extern static void mpz_mul(ref Mpz_t dest, ref Mpz_t op1, ref Mpz_t op2);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_mul_si")]
        extern static void mpz_mul_si(ref Mpz_t dest, ref Mpz_t src, int val);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_add")]
        extern static void mpz_add(ref Mpz_t dest, ref Mpz_t src, ref Mpz_t src2);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_sub")]
        extern static void mpz_sub(ref Mpz_t dest, ref Mpz_t src, ref Mpz_t src2);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_tdiv_q")]
        extern static void mpz_tdiv_q(ref Mpz_t dest, ref Mpz_t src, ref Mpz_t src2);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_set_si")]
        extern static void mpz_set_si(ref Mpz_t src, int value);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_get_si")]
        extern static int mpz_get_si(ref Mpz_t src);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "__gmpz_cmp")]
        extern static int mpz_cmp(ref Mpz_t op1, ref Mpz_t op2);
    }
}
