namespace GXPEngine.Control
{
	internal abstract class Scene : GameObject
	{
		protected bool Persistent;  // This name is slightly misleading since the scene won't actually update while it's not loaded. It just won't reset.
		protected bool Ready;
		protected MyGame myGame;
		protected int width;
		protected int height;
		public Scene(bool Persistent = false)
		{
			this.Persistent = Persistent;
			myGame = (MyGame)game;

			width = myGame.Width;
			height = myGame.Height;
		}

		/// <summary>
		/// Acts like the constructor without creating a new scene object. Put all object initializers in this method since children of the scene get removed when reloading.
		/// </summary>
		public virtual void Initialize()
		{
			// To initialize or rather, reinitialize all child objects have to be removed or reset to their original values.
			// It is to the inherited scene object to decide wether to call the base method or reset the child objects
			foreach (GameObject kid in GetChildren())
			{
				kid.Destroy();
			}
		}

		// Removes the scene from hierarchy
		public virtual void Unload()
		{
			if (!Persistent) Ready = false;
			Remove();
		}

		// Loads the scene into the hierarchy
		public virtual void Load(GameObject parent)
		{
			if (!Ready)
			{
				Initialize();
				Ready = true;
			}
			parent.AddChild(this);
		}
	}
}
