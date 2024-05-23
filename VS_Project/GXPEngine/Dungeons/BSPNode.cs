using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine.Dungeons
{
	internal abstract class BSPNode
	{
		public BSPNode Self;
		public BSPNode Parent;
		public BSPNode ChildA;
		public BSPNode ChildB;

		public List<BSPNode> this[int i]
		{
			get
			{
				if (i == 0)
				{
					return new List<BSPNode> { Self };
				}
				else
				{
					List<BSPNode> children = new List<BSPNode>();

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

		// Gets a list of all endpoints aka "leaves"
		// Endpoints don't have children.
		public List<BSPNode> Leaves
		{
			get
			{
				List<BSPNode> leaves = new List<BSPNode>();
				if (ChildA != null && ChildB != null)
				{ 
					ChildA.Leaves.ForEach((leaf) => leaves.Add(leaf));
					ChildB.Leaves.ForEach((leaf) => leaves.Add(leaf));
				}
				else leaves.Add(Self);
				return leaves;
			}
		}

		public BSPNode Sibling
		{
			get
			{
				if (Parent == null) return null;
				else if (Parent.ChildA == this) return Parent.ChildB;
				else if (Parent.ChildB == this) return Parent.ChildA;
				else throw new Exception("Check this out because it's silly");
			}
		}

		public BSPNode(BSPNode parent = null)
		{
			Self = this;
			Parent = parent;
		}
	}
}
