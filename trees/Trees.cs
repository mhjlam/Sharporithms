using System;
using System.Diagnostics;


namespace trees
{
	class Trees
	{
		public class Node
		{
			public int value;
			public Node left;
			public Node right;

			public Node(int v = 0)
			{
				value = v;
				left = null;
				right = null;
			}
		}

		public class Tree
		{
			public Node root;

			public Tree()
			{
				root = null;
			}

			public void Insert(int value)
			{
				Node node = new Node(value);

				Node parent = null;
				Node current = root;

				while (current != null)
				{
					if (value <= current.value)
					{
						parent = current;
						current = current.left;
					}
					else
					{
						parent = current;
						current = current.right;
					}
				}

				if (parent == null)
					root = node;
				else if (value < parent.value)
					parent.left = node;
				else
					parent.right = node;
			}

			public void Insert(int[] values)
			{
				foreach (int v in values)
					Insert(v);
			}

			public bool Remove(int value)
			{
				if (root == null)
					return false;

				Node parent = null;
				Node current = root;

				while (current != null)
				{
					if (current.value < value)
					{
						parent = current;
						current = current.left;
					}
					else if (current.value > value)
					{
						parent = current;
						current = current.right;
					}
					else
					{
						break; // found matching node
					}
				}

				if (current == null)
					return false; // did not find item


				// case 1: current.right = null
				if (current.right == null)
				{
					if (parent == null)
						root = current.left;
					else
					{
						if (current.value < parent.value)
							parent.left = current.left;
						else if (current.value > parent.value)
							parent.right = current.left;
					}
				}

				// case 2: current.right.left = null
				else if (current.right.left == null)
				{
					current.right.left = current.left;

					if (parent == null)
						root = current.right;
					else
					{
						if (current.value < parent.value)
							parent.left = current.right;
						else if (current.value > parent.value)
							parent.right = current.right;
					}
				}

				// case 3: current.right.left != null
				else
				{
					Node leftmost = current.right.left;
					Node lmparent = current.right; // left-most parent

					while (leftmost.left != null)
					{
						lmparent = leftmost;
						leftmost = leftmost.left;
					}


					lmparent = leftmost.right;
					leftmost.left = current.left;
					leftmost.right = current.right;

					if (parent == null)
						root = leftmost;
					else
					{
						if (current.value < parent.value)
							parent.left = leftmost;
						else if (current.value > parent.value)
							parent.right = leftmost;
					}
				}

				return true;
			}

			public bool Search(int value)
			{
				Node current = root;

				while (current != null)
				{
					if (current.value < value)
						current = current.left;
					else if (current.value > value)
						current = current.right;
					else return true;
				}

				return false;
			}

			private void PreorderTraversal(Node n)
			{
				if (n == null) return;

				Debug.Write(string.Format("{0} ", n.value));
				PreorderTraversal(n.left);
				PreorderTraversal(n.right);
			}

			public void PreorderTraversal()
			{
				Debug.Write("Pre-order traversal: ");
				PreorderTraversal(root);
				Debug.Write(Environment.NewLine);
			}

			private void InorderTraversal(Node n)
			{
				if (n == null) return;

				InorderTraversal(n.left);
				Debug.Write(string.Format("{0} ", n.value));
				InorderTraversal(n.right);
			}

			public void InorderTraversal()
			{
				Debug.Write("In-order traversal: ");
				InorderTraversal(root);
				Debug.Write(Environment.NewLine);
			}

			private void PostorderTraversal(Node n)
			{
				if (n == null) return;

				PostorderTraversal(n.left);
				PostorderTraversal(n.right);
				Debug.Write(string.Format("{0} ", n.value));
			}

			public void PostorderTraversal()
			{
				Debug.Write("Post-order traversal: ");
				PostorderTraversal(root);
				Debug.Write(Environment.NewLine);
			}


			private int Height(Node n)
			{
				if (n == null) return -1;
				else return Math.Max(Height(n.left), Height(n.right)) + 1;
			}

			public void Height()
			{
				Debug.WriteLine("Tree height = {0}", Height(root));
			}
		}


		static void Main(string[] args)
		{
			// binary search tree
			Tree T = new Tree();
			T.Insert(new int[] { 9, 5, 12, 1, 7, 10, 4 });

			T.Height();
			T.PreorderTraversal();
			T.InorderTraversal();
			T.PostorderTraversal();
		}
	}
}
