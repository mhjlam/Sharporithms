using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace geometry
{
	public partial class Canvas : Form
	{
		Point[] Points;
		Point[] ConvexHull;
		Point[] ClosestPair;


		public Canvas(Point[] p, Point[] cp, Point[] ch)
		{
			Points = new Point[p.Length];
			Array.Copy(p, Points, p.Length);

			ClosestPair = new Point[cp.Length];
			Array.Copy(cp, ClosestPair, cp.Length);

			ConvexHull = new Point[ch.Length];
			Array.Copy(ch, ConvexHull, ch.Length);

			InitializeComponent();
		}

		private void Canvas_Paint(object sender, PaintEventArgs e)
		{
			for (int i = 1; i < ConvexHull.Length; i += 2)
				e.Graphics.DrawLine(new Pen(Color.Blue, 3f), ConvexHull[i - 1], ConvexHull[i]);

			foreach (Point p in Points)
				e.Graphics.FillEllipse(System.Drawing.Brushes.Black, p.X - 5, p.Y - 5, 10, 10);

			foreach (Point p in ClosestPair)
				e.Graphics.FillEllipse(System.Drawing.Brushes.Red, p.X - 5, p.Y - 5, 10, 10);
		}
	}
}
