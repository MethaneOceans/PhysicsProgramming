namespace GXPEngine.Physics
{

	// Object to connect the GameObject class to the custom collider/physics code
	internal abstract class PhysicsObject : GameObject
	{
		public ACollider body;

		public new Vector2 Position
		{
			get => base.Position;
			set
			{
				base.Position = value;
				body.Position = value;
			}
		}
		public new float Rotation
		{
			get => base.Rotation;
			set
			{
				base.Rotation = value;
				body.Angle = value;
			}
		}

		public PhysicsObject()
		{

		}
	}
}
