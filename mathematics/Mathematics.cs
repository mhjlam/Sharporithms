using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace mathematics
{
	class Mathematics
	{
		static int gcdEuclid(int m, int n)
		{
			while (n != 0)
			{
				int r = m % n;
				m = n;
				n = r;
			}

			return m;
		}


		static int gcdCheck(int m, int n)
		{
			int t = Math.Min(m, n);

		Step2:
			if (m % t == 0)
				goto Step3;
			else
				goto Step4;

			Step3:
			if (n % t == 0)
				return t;
			else
				goto Step4;

			Step4:
			t--;
			goto Step2;
		}


		static List<int> sieveOfEratosthenes(int n)
		{
			int[] A = new int[n];
			List<int> L = new List<int>();

			int sqrtn = (int)Math.Floor(Math.Sqrt(n));

			for (int p = 2; p < n; ++p)
				A[p] = p;

			for (int p = 2; p < sqrtn; ++p)
			{
				if (A[p] != 0)
				{
					int j = p * p;
					while (j < n)
					{
						A[j] = 0;
						j += p;
					}
				}
			}

			for (int p = 2; p < n; ++p)
				if (A[p] != 0)
					L.Add(A[p]);

			return L;
		}


		static List<int> trialDivision(int n)
		{
			List<int> primeFactors = new List<int>();

			int sqrtn = (int)Math.Floor(Math.Sqrt(n));

			if (n < 2)
				return primeFactors;

			foreach (int p in sieveOfEratosthenes(sqrtn))
			{
				if (p * p > n)
					break;

				while (n % p == 0)
				{
					primeFactors.Add(p);
					n /= p;
				}
			}

			if (n > 1)
				primeFactors.Add(n);

			return primeFactors;
		}


		static int gcdPrimes(int m, int n)
		{
			List<int> mPrimes = trialDivision(m);
			List<int> nPrimes = trialDivision(n);

			int gcd = 1;

			int i = 0;
			while (i < mPrimes.Count)
			{
				int j = 0;
				while (j < nPrimes.Count)
				{
					if (mPrimes[i] == nPrimes[j])
					{
						gcd *= mPrimes[i];
						nPrimes.RemoveAt(j);
						break;
					}
					j++;
				}
				i++;
			}

			return gcd;
		}


		static int gcdBinary(int m, int n)
		{
			int d = 0;

			while (m % 2 == 0 && n % 2 == 0)
			{
				m /= 2;
				n /= 2;
				d++;
			}

			while (m != n)
			{
				if (m % 2 == 0) m /= 2;
				else if (n % 2 == 0) n /= 2;
				else if (m > n) m = (m - n) / 2;
				else n = (n - m) / 2;
			}

			return m * (int)Math.Pow(2.0, d);
		}


		static int Factorial(int n)
		{
			if (n == 0) return 1;
			return Factorial(n - 1) * n;
		}


		static int Fibonacci1(int n)
		{
			int[] F = new int[n + 1];
			F[0] = 0; F[1] = 1;

			for (int i = 2; i < n + 1; ++i)
				F[i] = F[i - 1] + F[i - 2];
			return F[n];
		}

		static int Fibonacci(int n)
		{
			if (n <= 1) return n;
			return Fibonacci(n - 1) + Fibonacci(n - 2);
		}


		static int BinaryDigits1(int n)
		{
			int count = 1;
			while (n > 1)
			{
				count++;
				n = (int)Math.Floor(n / 2.0);
			}

			return count;
		}

		static int BinaryDigits(int n)
		{
			if (n == 1) return 1;
			return BinaryDigits((int)Math.Floor(n / 2.0)) + 1;
		}


		static int[] Random(int n, int m, int s, int a, int b)
		{
			// Generates sequence of random numbers based on linear congruential method
			// m: 2^32 or 2^64 (depending on word size)
			// s: current date/time (seed)
			// a: integer between 0.01m and 0.99m such that (a % 8 = 5)
			// b: 1

			int[] r = new int[n];
			r[0] = s;

			for (int i = 1; i < n; ++i)
				r[i] = (a * r[i - 1] + b) % m;

			return r;
		}


		static public double[] QuadraticFormula(double a, double b, double c)
		{
			double d = b * b - 4 * a * c; // discriminant

			if (d < 0) return null;
			else if (d == 0)
				return new double[] { -b / (2 * a) };
			else
				return new double[] { (-b + Math.Sqrt(d)) / (2 * a), (-b - Math.Sqrt(d)) / (2 * a) };
		}


		static void Main(string[] args)
		{
			Random random = new Random(DateTime.Now.Millisecond);

			// greatest common divisor
			int m = 60, n = 24;
			int gcd1 = gcdEuclid(m, n);
			int gcd2 = gcdCheck(m, n);
			int gcd3 = gcdPrimes(m, n);
			int gcd4 = gcdBinary(m, n);

			// binary digits
			int r1 = random.Next(1, 1024);
			int bd1 = BinaryDigits1(r1);
			int bd2 = BinaryDigits(r1);

			// factorial
			int r2 = random.Next(1, 10);
			int fact = Factorial(r2);

			// fibonacci
			int r3 = random.Next(1, 10);
			int fib1 = Fibonacci1(r3);
			int fib2 = Fibonacci(r3);

			// random sequence
			int[] rseq = Random(5, 2147483647, DateTime.Now.Millisecond, 13, 1);


			Debug.WriteLine("Greatest common divisor (Euclid's algorithm)");
			Debug.WriteLine("gcd({0},{1})) = {2}", m, n, gcd1);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Greatest common divisor (consecutive integer checking)");
			Debug.WriteLine("gcd({0},{1})) = {2}", m, n, gcd2);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Greatest common divisor (prime factorization)");
			Debug.WriteLine("gcd({0},{1})) = {2}", m, n, gcd3);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Greatest common divisor (binary method)");
			Debug.WriteLine("gcd({0},{1})) = {2}", m, n, gcd4);


			Debug.Write(Environment.NewLine);
			Debug.Write(Environment.NewLine);


			Debug.WriteLine("Binary digits");
			Debug.WriteLine("digits({0}) = {1}", r1, bd1);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Binary digits (recursive)");
			Debug.WriteLine("digits({0}) = {1}", r1, bd2);


			Debug.Write(Environment.NewLine);
			Debug.Write(Environment.NewLine);


			Debug.WriteLine("Factorial (recursive)");
			Debug.WriteLine("factorial({0}) = {1}", r2, fact);


			Debug.Write(Environment.NewLine);
			Debug.Write(Environment.NewLine);


			Debug.WriteLine("Fibonacci");
			Debug.WriteLine("fibonacci({0}) = {1}", r3, fib1);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Fibonacci (recursive)");
			Debug.WriteLine("fibonacci({0}) = {1}", r3, fib2);


			Debug.Write(Environment.NewLine);
			Debug.Write(Environment.NewLine);


			Debug.WriteLine("Random sequence (linear congruential method)");
			Debug.WriteLine("random({0}, {1}, {2}, {3}, {4}) = [{5}]", 
				5, 2147483647, DateTime.Now.Millisecond, 13, 1, string.Join(",", rseq));
		}
	}
}
