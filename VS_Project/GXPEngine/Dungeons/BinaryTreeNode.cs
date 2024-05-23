using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Dungeons
{
	internal class BinaryTreeNode<T>
	{
		public T Self;
		public BinaryTreeNode<T> Parent;
		public BinaryTreeNode<T> ChildA;
		public BinaryTreeNode<T> ChildB;

		public List<T> this[int i]
		{
			get
			{
				if (i == 0)
				{
					return new List<T> { Self };
				}
				else
				{
					List<T> children = new List<T>();

					if (ChildA != null && ChildB != null)
					{
						ChildA[i - 1].ForEach((child) => children.Add(child));
						ChildB[i - 1].ForEach((child) => children.Add(child));
					}
					//else children.Add(Self);

					return children;
				}
			}
		}

		public List<T> Leaves
		{
			get
			{
				List<T> leaves = new List<T>();
				if (ChildA != null && ChildB != null)
				{ 
					ChildA.Leaves.ForEach((leaf) => leaves.Add(leaf));
					ChildB.Leaves.ForEach((leaf) => leaves.Add(leaf));
				}
				else leaves.Add(Self);
				return leaves;
			}
		}

		public BinaryTreeNode<T> Sibling
		{
			get
			{
				if (Parent == null) return null;
				else if (Parent.ChildA == this) return Parent.ChildB;
				else if (Parent.ChildB == this) return Parent.ChildA;
				else throw new Exception("Check this out because it's silly");
			}
		}

		public BinaryTreeNode(T self, BinaryTreeNode<T> parent = null)
		{
			Self = self;
			Parent = parent;
		}
	}
}
