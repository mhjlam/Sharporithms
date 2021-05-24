using System;
using System.Diagnostics;


namespace searching
{
	class Searching
	{
		static int SequentialSearch(int[] A, int k)
		{
			int i = 0;
			int n = A.Length;

			while (i < n && A[i] != k) i++;
			if (i < n) return i;
			else return -1;
		}
		
		static int BinarySearch(int[] A, int k)
		{
			int l = 0;
			int r = A.Length - 1;

			while (l <= r)
			{
				int m = (l + r) / 2;
				if (k == A[m]) return m;
				else if (k < A[m]) r = m - 1;
				else l = m + 1;
			}

			return -1;
		}


		static void Main(string[] args)
		{
			Random random = new Random(DateTime.Now.Millisecond);

			// sequential search
			int[] A = new int[5];
			for (int i = 0; i < A.Length; ++i)
				A[i] = random.Next(1, 10);

			int k = random.Next(1, 10);
			int ss = SequentialSearch(A, k);

			// binary search
			int[] B = new int[10];
			for (int i = 0; i < B.Length; ++i)
				B[i] = random.Next(i * 5 + 1, i * 5 + 5);

			k = B[0] + B[4] + B[9] / 3;
			int bs = BinarySearch(B, k);


			Debug.WriteLine("Sequential search");
			Debug.WriteLine("search([{0}], {1}) = {2}", string.Join(",", A), k, ss);
			
			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Binary search");
			Debug.WriteLine("search([{0}], {1}) = {2}", string.Join(",", B), k, bs);
		}
	}
}
