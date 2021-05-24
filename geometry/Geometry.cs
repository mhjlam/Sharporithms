using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace geometry
{
	class Geometry
	{
		public class MultiFormContext : ApplicationContext
		{
			private int openForms;
			public MultiFormContext(params Form[] forms)
			{
				openForms = forms.Length;

				foreach (var form in forms)
				{
					form.FormClosed += (s, args) =>
					{
						//When we have closed the last of the "starting" forms, end the program.
						if (Interlocked.Decrement(ref openForms) == 0)
							ExitThread();
					};

					form.Show();
				}
			}
		}


		static Point[] BruteForceClosestPair(Point[] P)
		{
			int n = P.Length;
			Point[] nn = new Point[2];
			double dmin = Double.PositiveInfinity;

			for (int i = 0; i < n - 1; ++i)
			{
				for (int j = i + 1; j < n; ++j)
				{
					double dsqr = Math.Pow(P[i].X - P[j].X, 2.0) + Math.Pow(P[i].Y - P[j].Y, 2.0);

					if (dsqr < dmin)
					{
						dmin = dsqr;
						nn[0] = P[i];
						nn[1] = P[j];
					}
				}
			}

			return nn;
		}


		static Point[] BruteForceConvexHull(Point[] P)
		{
			int n = P.Length;
			List<Point> hull = new List<Point>();

			for (int i = 0; i < n; ++i)
			{
				for (int j = i + 1; j < n; ++j)
				{
					int sign = 0;
					bool samesign = true;

					for (int k = 0; k < n; ++k)
					{
						if (k == i || k == j)
							continue;

						// check sgn(ax + by - c)
						// where a = (y_2 - y_1), b = (x_1 - x_2), c = (x_1y_2 - y_1x_2)
						int a = P[j].Y - P[i].Y;
						int b = P[i].X - P[j].X;
						int c = P[i].X * P[j].Y - P[i].Y * P[j].X;

						int signk = Math.Sign(a * P[k].X + b * P[k].Y - c);

						if (sign == 0)
						{
							sign = signk;
						}
						else if (signk != sign)
						{
							samesign = false;
							break;
						}
					}

					if (samesign)
					{
						hull.Add(P[i]);
						hull.Add(P[j]);
					}
				}
			}

			return hull.ToArray();
		}


		static void Main(string[] args)
		{
			Random random = new Random(DateTime.Now.Millisecond);


			// brute force closest-pair and convex hull
			Point[] P = new Point[10];
			for (int i = 0; i < P.Length; ++i)
				P[i] = new Point(random.Next(50, 300), random.Next(50, 300));

			Point[] closestPair = BruteForceClosestPair(P);
			Point[] convexHull = BruteForceConvexHull(P);


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new MultiFormContext(
				new Canvas(P, closestPair, convexHull), 
				new Canvas(P, closestPair, convexHull))
				);
		}
	}
}
