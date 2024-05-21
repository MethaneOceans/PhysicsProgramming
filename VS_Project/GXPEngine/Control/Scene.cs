namespace GXPEngine.Control
{
	internal abstract class Scene : GameObject
	{
		public virtual void Load() => game.AddChild(this);
		public virtual void Unload() => game.RemoveChild(this);
	}
}
