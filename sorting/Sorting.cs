using System;
using System.Diagnostics;


namespace sorting
{
	class Sorting
	{
		static bool NonDecreasingOrder(int[] A)
		{
			for (int i = 1; i < A.Length; ++i)
				if (A[i - 1] > A[i])
					return false;
			return true;
		}

		static void CheckNonDecreasingOrder(int[] A)
		{
			if (NonDecreasingOrder(A))
				Debug.WriteLine("SUCCESS");
			else Debug.WriteLine("FAIL");
		}


		static int[] SelectionSort(int[] arr)
		{
			int n = arr.Length;
			int[] A = new int[n];
			Array.Copy(arr, A, n);

			for (int i = 0; i < n - 1; ++i)
			{
				int min = i;

				for (int j = i + 1; j < n; ++j)
					if (A[j] < A[min]) min = j;

				int temp = A[i];
				A[i] = A[min];
				A[min] = temp;
			}

			return A;
		}

		static int[] BubbleSort(int[] arr)
		{
			int n = arr.Length;
			int[] A = new int[n];
			Array.Copy(arr, A, n);

			for (int i = 0; i < n - 1; i++)
			{
				for (int j = 0; j < n - 1 - i; ++j)
				{
					if (A[j + 1] < A[j])
					{
						int temp = A[j];
						A[j] = A[j + 1];
						A[j + 1] = temp;
					}
				}
			}

			return A;
		}


		static int[] Merge(int[] left, int[] right)
		{
			int p = left.Length;
			int q = right.Length;

			int i = 0, j = 0, k = 0;
			int[] sorted = new int[p + q];

			while (i < p && j < q)
			{
				if (left[i] <= right[j])
					sorted[k++] = left[i++];
				else
					sorted[k++] = right[j++];
			}

			if (i == p)
				Array.Copy(right, j, sorted, k, p + q - k);
			else
				Array.Copy(left, i, sorted, k, p + q - k);

			return sorted;
		}

		static int[] MergeSort(int[] A)
		{
			if (A.Length < 2)
				return A;

			int n = A.Length;
			int m = A.Length / 2;

			int[] left = new int[m];
			int[] right = new int[n - m];

			Array.Copy(A, 0, left, 0, m);
			Array.Copy(A, m, right, 0, n - m);

			left = MergeSort(left);
			right = MergeSort(right);

			return Merge(left, right);
		}


		static void Swap(ref int a, ref int b)
		{
			int t = a;
			a = b;
			b = t;
		}


		static int Partition1(int[] A, int lo, int hi) // Lomuto partition scheme
		{
			int i = lo; // place for swapping
			int pivot = A[hi];

			for (int j = lo; j < hi; ++j)
				if (A[j] <= pivot)
					Swap(ref A[i++], ref A[j]);

			Swap(ref A[i], ref A[hi]);
			return i;
		}

		static void QuickSort1(int[] A, int lo, int hi)
		{
			if (lo < hi)
			{
				int p = Partition1(A, lo, hi);
				QuickSort1(A, lo, p - 1);
				QuickSort1(A, p + 1, hi);
			}
		}


		static int Partition(int[] A, int lo, int hi) // Hoare partition scheme
		{
			int pivot = A[lo];
			int i = lo - 1;
			int j = hi + 1;

			while (true)
			{
				do i++; while (A[i] < pivot); // skips over elements smaller than pivot (left)
				do j--; while (A[j] > pivot); // skips over elements larger than pivot (right)

				if (i < j)
					Swap(ref A[i], ref A[j]); // swap left selection with right selection
				else
					return j;
			}
		}

		static void QuickSort(int[] A, int lo, int hi)
		{
			if (lo < hi)
			{
				int p = Partition(A, lo, hi);
				QuickSort(A, lo, p);
				QuickSort(A, p + 1, hi);
			}
		}


		static void InsertionSort(int[] A)
		{
			int n = A.Length;

			for (int i = 1; i < n; ++i)
			{
				int v = A[i];
				int j = i - 1;

				while (j >= 0 && A[j] > v)
					A[j + 1] = A[j--];

				A[j + 1] = v;
			}
		}


		static void ShellSort(int[] A)
		{
			int n = A.Length;
			int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };
			
			foreach (int gap in gaps)
				for (int i = gap; i < n; ++i)
					for (int j = i; j >= gap && A[j - gap] > A[j]; j -= gap)
						Swap(ref A[j - gap], ref A[j]);
		}




		static void Main(string[] args)
		{
			Random random = new Random(DateTime.Now.Millisecond);

			// selection sort
			int[] A1 = new int[10];
			for (int i = 0; i < A1.Length; ++i)
				A1[i] = random.Next(1, 100);

			int[] S1 = SelectionSort(A1);

			// bubble sort
			int[] A2 = new int[10];
			for (int i = 0; i < A2.Length; ++i)
				A2[i] = random.Next(1, 100);

			int[] S2 = BubbleSort(A2);

			// merge sort
			int[] A3 = new int[10];
			for (int i = 0; i < A3.Length; ++i)
				A3[i] = random.Next(1, 100);

			int[] S3 = MergeSort(A3);

			// quick sort
			int[] A4 = new int[10];
			for (int i = 0; i < A4.Length; ++i)
				A4[i] = random.Next(1, 100);

			int[] S4 = new int[A4.Length];
			Array.Copy(A4, S4, A4.Length);
			QuickSort(S4, 0, S4.Length - 1);
			
			// insertion sort
			int[] A5 = new int[10];
			for (int i = 0; i < A5.Length; ++i)
				A5[i] = random.Next(1, 100);

			int[] S5 = new int[A5.Length];
			Array.Copy(A5, S5, A5.Length);
			InsertionSort(S5);

			// Shell sort
			int[] A6 = new int[10];
			for (int i = 0; i < A6.Length; ++i)
				A6[i] = random.Next(1, 100);

			int[] S6 = new int[A6.Length];
			Array.Copy(A6, S6, A6.Length);
			ShellSort(S6);


			Debug.WriteLine("Selection sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A1), string.Join(",", S1));
			CheckNonDecreasingOrder(S1);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Bubble sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A2), string.Join(",", S2));
			CheckNonDecreasingOrder(S2);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Merge sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A3), string.Join(",", S3));
			CheckNonDecreasingOrder(S3);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Quick sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A4), string.Join(",", S4));
			CheckNonDecreasingOrder(S4);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Insertion sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A5), string.Join(",", S5));
			CheckNonDecreasingOrder(S5);

			Debug.Write(Environment.NewLine);

			Debug.WriteLine("Shell sort");
			Debug.WriteLine("sort([{0}]) = {1}", string.Join(",", A6), string.Join(",", S6));
			CheckNonDecreasingOrder(S6);
		}
	}
}
