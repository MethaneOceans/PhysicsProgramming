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

		public BinaryTreeNode(T self)
		{
			Self = self;
		}
	}
}
