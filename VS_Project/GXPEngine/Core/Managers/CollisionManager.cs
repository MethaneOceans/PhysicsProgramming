using System;
using System.Collections.Generic;
using System.Reflection;

namespace GXPEngine
{
	//------------------------------------------------------------------------------------------------------------------------
	//														CollisionManager
	//------------------------------------------------------------------------------------------------------------------------
	public class CollisionManager
	{
		/// <summary>
		/// Set this to false if you want to be able to remove game objects from the game during OnCollision (=the old, unsafe default behavior).
		/// </summary>
		public static bool SafeCollisionLoop = true;
		/// <summary>
		/// Set this to true if you only want to include trigger colliders in OnCollision (=more efficient).
		/// </summary>
		public static bool TriggersOnlyOnCollision = false;

		private delegate void CollisionDelegate(GameObject gameObject);

		//------------------------------------------------------------------------------------------------------------------------
		//														ColliderInfo
		//------------------------------------------------------------------------------------------------------------------------
		private struct ColliderInfo
		{
			public GameObject gameObject;
			public CollisionDelegate onCollision;

			//------------------------------------------------------------------------------------------------------------------------
			//														ColliderInfo()
			//------------------------------------------------------------------------------------------------------------------------
			public ColliderInfo(GameObject gameObject, CollisionDelegate onCollision)
			{
				this.gameObject = gameObject;
				this.onCollision = onCollision;
			}
		}

		private readonly List<GameObject> colliderList = new List<GameObject>();
		private static readonly List<ColliderInfo> colliderInfos = new List<ColliderInfo>();
		private readonly List<ColliderInfo> activeColliderList = colliderInfos;
		private static readonly Dictionary<GameObject, ColliderInfo> dictionary = new Dictionary<GameObject, ColliderInfo>();
		private readonly Dictionary<GameObject, ColliderInfo> _collisionReferences = dictionary;

		private bool collisionLoopActive = false;

		//------------------------------------------------------------------------------------------------------------------------
		//														CollisionManager()
		//------------------------------------------------------------------------------------------------------------------------
		public CollisionManager()
		{
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Step()
		//------------------------------------------------------------------------------------------------------------------------
		public void Step()
		{
			collisionLoopActive = SafeCollisionLoop;
			for (int i = activeColliderList.Count - 1; i >= 0; i--)
			{
				ColliderInfo info = activeColliderList[i];
				for (int j = colliderList.Count - 1; j >= 0; j--)
				{
					if (j >= colliderList.Count) continue; //fix for removal in loop
					GameObject other = colliderList[j];
					if (other.collider == null || !(other.collider.isTrigger || !TriggersOnlyOnCollision)) continue;
					if (info.gameObject != other)
					{
						if (info.gameObject.HitTest(other))
						{
							info.onCollision?.Invoke(other);
						}
					}
				}
			}
			collisionLoopActive = false;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//												 GetCurrentCollisions()
		//------------------------------------------------------------------------------------------------------------------------
		public GameObject[] GetCurrentCollisions(GameObject gameObject, bool includeTriggers = true, bool includeSolid = true)
		{
			List<GameObject> list = new List<GameObject>();
			for (int j = colliderList.Count - 1; j >= 0; j--)
			{
				if (j >= colliderList.Count) continue; //fix for removal in loop				
				GameObject other = colliderList[j];
				if (other.collider == null || (other.collider.isTrigger && !includeTriggers) || (!other.collider.isTrigger && !includeSolid)) continue;
				if (gameObject != other)
				{
					if (gameObject.HitTest(other))
					{
						list.Add(other);
					}
				}
			}
			return list.ToArray();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Add()
		//------------------------------------------------------------------------------------------------------------------------
		public void Add(GameObject gameObject)
		{
			if (collisionLoopActive)
			{
				throw new Exception("Cannot call AddChild for gameobjects during OnCollision - use LateAddChild instead.");
			}
			if (gameObject.collider != null && !colliderList.Contains(gameObject))
			{
				colliderList.Add(gameObject);
			}

			MethodInfo info = gameObject.GetType().GetMethod("OnCollision", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

			if (info != null)
			{

				CollisionDelegate onCollision = (CollisionDelegate)Delegate.CreateDelegate(typeof(CollisionDelegate), gameObject, info, false);
				if (onCollision != null && !_collisionReferences.ContainsKey(gameObject))
				{
					ColliderInfo colliderInfo = new ColliderInfo(gameObject, onCollision);
					_collisionReferences[gameObject] = colliderInfo;
					activeColliderList.Add(colliderInfo);
				}

			}
			else
			{
				ValidateCase(gameObject);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														validateCase()
		//------------------------------------------------------------------------------------------------------------------------
		private void ValidateCase(GameObject gameObject)
		{
			MethodInfo info = gameObject.GetType().GetMethod("OnCollision", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (info != null)
			{
				throw new Exception("'OnCollision' function was not binded. Please check its case (capital O?)");
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Remove()
		//------------------------------------------------------------------------------------------------------------------------
		public void Remove(GameObject gameObject)
		{
			if (collisionLoopActive)
			{
				throw new Exception("Cannot destroy or remove gameobjects during OnCollision - use LateDestroy or LateRemove instead.");
			}
			colliderList.Remove(gameObject);
			if (_collisionReferences.ContainsKey(gameObject))
			{
				ColliderInfo colliderInfo = _collisionReferences[gameObject];
				activeColliderList.Remove(colliderInfo);
				_collisionReferences.Remove(gameObject);
			}
		}

		public string GetDiagnostics()
		{
			string output = "";
			output += "Number of colliders: " + colliderList.Count + '\n';
			output += "Number of active colliders: " + activeColliderList.Count + '\n';
			return output;
		}
	}
}

