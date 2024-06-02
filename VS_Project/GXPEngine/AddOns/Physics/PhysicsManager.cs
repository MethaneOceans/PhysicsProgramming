using GXPEngine.Wormshocked.Objects;
using System;
using System.Collections.Generic;
using static GXPEngine.Mathf;
using static GXPEngine.Physics.ACollider;

namespace GXPEngine.Physics
{
	internal class PhysicsManager
	{
		public IReadOnlyList<ACollider> Objects => bodies.AsReadOnly();
		public Vector2 Gravity = new Vector2();

		private readonly List<ACollider> bodies;

		// It may have been a mistake to make these lists since they complicate adding colliders. Unfortunately a mess to rip out
		private readonly List<ACollider> staticColliders;
		private readonly List<ACollider> triggerColliders;
		private readonly List<ACollider> rigidColliders;

		private bool ActiveStep;

		public PhysicsManager()
		{
			bodies = new List<ACollider>();
			staticColliders = new List<ACollider>();
			triggerColliders = new List<ACollider>();
			rigidColliders = new List<ACollider>();

			ActiveStep = false;
		}

		public void Add(ACollider obj)
		{
			// Prevent adding objects during steps
			if (ActiveStep)
			{
				throw new InvalidOperationException("Currently running a step, cannot add object");
			}
			else
			{
				bodies.Add(obj);

				if (obj.Behavior == ColliderType.Static) staticColliders.Add(obj);
				else if (obj.Behavior == ColliderType.Rigid) rigidColliders.Add(obj);
				else if (obj.Behavior == ColliderType.Trigger) triggerColliders.Add(obj);

				obj.BehaviorChanged += BehaviorChangeHandler;
			}
		}
		public void Add(PhysicsObject pObj) => Add(pObj.body);
		public void Remove(ACollider obj)
		{
			if (ActiveStep)
			{
				throw new InvalidOperationException("Currently running a step, cannot remove object");
			}
			else
			{
				bodies.Remove(obj);

				if (obj.Behavior == ColliderType.Static) staticColliders.Remove(obj);
				else if (obj.Behavior == ColliderType.Rigid) rigidColliders.Remove(obj);
				else if (obj.Behavior == ColliderType.Trigger) triggerColliders.Remove(obj);

				obj.BehaviorChanged -= BehaviorChangeHandler;
			}
		}
		public void Remove(PhysicsObject pObj) => Remove(pObj.body);

		private void ForceRemove(ACollider obj)
		{
			bodies.Remove(obj);

			if (obj.Behavior == ColliderType.Static) staticColliders.Remove(obj);
			else if (obj.Behavior == ColliderType.Rigid) rigidColliders.Remove(obj);
			else if (obj.Behavior == ColliderType.Trigger) triggerColliders.Remove(obj);

			//obj.BehaviorChanged -= BehaviorChangeHandler;
			obj.Destroy();
		}

		public void Step()
		{
			for (int i = bodies.Count - 1; i >= 0; i--)
			{
				ACollider obj = bodies[i];

				if (obj.ShouldRemove)
				{
					ForceRemove(obj);
				}
			}

			ActiveStep = true;

			foreach (ACollider obj in rigidColliders)
			{
				Step(obj);
			}

			ActiveStep = false;
		}

		private void Step(ACollider obj)
		{
			if (obj.Behavior == ColliderType.Rigid)
			{
				Step_Triggers(obj);

				// Copied from ACollider

				// If true then the object collided with something beneath it
				if (obj.CollidedLastFrame && Vector2.Dot(obj.LastCollision.Normal, Gravity) > 0)
				{
					//Debug.WriteLine("Touching floor");
					if (obj.Owner is PlayerCharacter pc) pc.sprite.SetColor(1, 0, 0);

					obj.Velocity += Gravity - Vector2.Dot(Gravity, obj.LastCollision.Normal) * Gravity;
					// Friction
					obj.Velocity *= 0.95f;
				}
				else
				{
					obj.Velocity += Gravity;
					if (obj.Owner is PlayerCharacter pc) pc.sprite.SetColor(1, 1, 1);
				}
				obj.Position += obj.Velocity;

				bool collided = false;
				CollisionInfo bestCol = new CollisionInfo();

				// Check if there are collisions and which one is the deepest into another collider
				foreach (ACollider collider in bodies)
				{
					// Don't resolve collisions with triggers or self
					if (collider == obj || collider.Behavior == ColliderType.Trigger) continue;
					if (obj.Overlapping(collider))
					{
						CollisionInfo colInfo = obj.LastCollision;
						if (colInfo.Depth > bestCol.Depth)
						{
							bestCol = colInfo;
						}
						collided = true;
						obj.CollidedLastFrame = true;
					}
				}

				// Resolve collision if there is one
				if (collided)
				{
					// Move object out of the other object
					obj.Position -= bestCol.Normal * bestCol.Depth;
					// Calculate the velocity along the normal of the collision
					Vector2 q = Vector2.Dot(bestCol.Normal, obj.Velocity) * bestCol.Normal;
					float bounciness = CalculateBounciness(obj.BounceMode, obj.Bounciness, bestCol.Other.Bounciness);

					// Reflect velocity along normal
					obj.Velocity -= (1f + bounciness) * q;
				}
				else obj.CollidedLastFrame = false;

				// Update connected game object of collider, if it has one
				if (obj.Owner != null)
				{
					obj.Owner.Position = obj.Position;
					obj.Owner.Rotation = obj.Angle;
				}
			}
		}

		private float CalculateBounciness(BounceCalcMode bounceMode, float bouncinessA, float bouncinessB)
		{
			switch (bounceMode)
			{
				case BounceCalcMode.Min:
					return Min(bouncinessA, bouncinessB);
				case BounceCalcMode.Max:
					return Max(bouncinessA, bouncinessB);
				case BounceCalcMode.Average:
					return (bouncinessA + bouncinessB) / 2f;
				case BounceCalcMode.Multiply:
					return bouncinessA * bouncinessB;
				default:
					goto case BounceCalcMode.Average;
			}
		}

		// Checks overlaps with triggers and invokes the method attached to a trigger
		private void Step_Triggers(ACollider obj)
		{
			foreach (ACollider trigger in triggerColliders)
			{
				if (obj.Overlapping(trigger))
				{
					// Call the attached method if it exists
					trigger.TriggerMethod?.Invoke(trigger, obj);
				}
			}
		}

		// Method handles sorting of behavior changes.
		// TODO: Method is UNSAFE, add safety checks to prevent changes during a step.
		private void BehaviorChangeHandler(object sender, BehaviorChangeEvent args)
		{
			ColliderType oldB = args.OldBehavior;
			ColliderType newB = args.NewBehavior;

			if (oldB == ColliderType.Rigid) rigidColliders.Remove((ACollider)sender);
			else if (oldB == ColliderType.Trigger) triggerColliders.Remove((ACollider)sender);
			else if (oldB == ColliderType.Rigid) rigidColliders.Remove((ACollider)sender);

			if (newB == ColliderType.Rigid) rigidColliders.Add((ACollider)sender);
			else if (newB == ColliderType.Trigger) triggerColliders.Add((ACollider)sender);
			else if (newB == ColliderType.Rigid) rigidColliders.Add((ACollider)sender);
		}
	}

	internal enum BounceCalcMode
	{
		Min,
		Max,
		Average,
		Multiply,
	}
}
