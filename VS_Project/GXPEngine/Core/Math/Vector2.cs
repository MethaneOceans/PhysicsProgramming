using System;
using static GXPEngine.Mathf; // Allows using Mathf functions

public struct Vector2 : IEquatable<Vector2>
{
	private static Random Random = new Random(); // Random object since the MathF implementation tends to be annoying

	public float X, Y;
	public (float x, float y) XY	// Get/Set tuple property 
	{
		get => (X, Y);
		set
		{
			X = value.x;
			Y = value.y;
		}
	}

	//	-------------------------------------
	//		Constructors / Standard vectors
	//	-------------------------------------
	public Vector2(double x = 0, double y = 0)	// Default constructor 
	{
		X = (float)x;
		Y = (float)y;
	}
	public static Vector2 Zero => new Vector2(0, 0);	// Create zero length vector (Does the same as standard constructor without arguments)
	public static Vector2 Right => new Vector2(1, 0);	// Create unit length vector in positive X direction
	public static Vector2 Down => new Vector2(0, 1);	// Create unit length vector in positive Y direction
	public static Vector2 GetUnitVectorRad(float radians) => new Vector2(Cos(radians), Sin(radians));   // Create a unit vector in the specified direction
	public static Vector2 GetUnitVectorDeg(float degrees) => GetUnitVectorRad(degrees * Deg2Rad);    // Create a unit vector in the specified direction
	public static Vector2 RandomUnitVec(Random rng = null)   // Create a unit vector in a random direction 
	{
		if (rng == null) return GetUnitVectorDeg((float)(Random.NextDouble() * 360));
		else return GetUnitVectorDeg((float)(rng.NextDouble() * 360));
	}

	//	-------------------------------------
	//		Math operators
	//	-------------------------------------
	public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);	// Adds two vectors together (component-wise addition)
	public static Vector2 operator -(Vector2 v) => new Vector2(-v.X, -v.Y);	// Negates a vector so it will point in the opposite direction
	public static Vector2 operator -(Vector2 a, Vector2 b) => a + -b;	// Subtracts a vector
	public static Vector2 operator *(Vector2 v, float s) => new Vector2(v.X * s, v.Y * s);	// Scales vector by scalar value
	public static Vector2 operator *(float s, Vector2 v) => v * s;	// Scales vector by scalar value
	public static Vector2 operator /(Vector2 v, float s)    // Divides the components of a vector by a scalar value 
	{
		if (s == 0) throw new DivideByZeroException();
		return v * (1 / s);
	}

	//	-------------------------------------
	//		Length operations
	//	-------------------------------------
	public float LengthSquared() => (X * X) + (Y * Y);  // Calculate the squared length/magnitude of the vector
	public float Length() => Sqrt(LengthSquared()); // Calculate the length (magnitude) of the instance
	public void Normalize() // Normalized the vector instance so the length will be 1 
	{
		if (X == 0 && Y == 0) return;
		this /= Length();
	}
	public Vector2 Normalized() // Creates a normalized (unit length) copy of vector instance 
	{
		float length = Length();
		if (length == 0) return new Vector2();
		return new Vector2(X, Y) / length;
	}

	//	-------------------------------------
	//		Trigonometric methods
	//	-------------------------------------

	// Conversion constants
	public const float Deg2Rad = PI / 180f;
	public const float Rad2Deg = 180f / PI;

	// Angle calculation
	public float GetRadians() => Atan2(Y, X);
	public float GetDegrees() => Rad2Deg * Atan2(Y, X);

	// Set angle
	public void SetRadians(float radians)	
	{
		this = new Vector2(Cos(radians), Sin(radians)) * Length();
	}
	public void SetDegrees(float degrees)	
	{
		SetRadians(degrees * Deg2Rad);
	}

	// Rotated copy
	public Vector2 RotatedRad(float angle)	
	{
		float sin = Sin(angle);
		float cos = Cos(angle);
		float xComp = cos * X - sin * Y;
		float yComp = sin * X + cos * Y;
		return new Vector2(xComp, yComp);
	}
	public Vector2 RotatedDeg(float angle)	
	{
		return RotatedRad(angle * Deg2Rad);
	}

	// Batch rotation
	public static Vector2[] RotateVectorsRad(Vector2[] vecs, float angle)	
	{
		angle = 1 * angle;
		float cosA = Cos(angle);
		float sinA = Sin(angle);

		Vector2[] result = new Vector2[vecs.Length];
		for (int i = 0; i < vecs.Length; i++)
		{
			float x = vecs[i].X;
			float y = vecs[i].Y;

			result[i] = new Vector2(cosA * x - sinA * y, sinA * x + cosA * y);
		}

		return result;
	}
	public static Vector2[] RotateVectorsDeg(Vector2[] vecs, float angle)	
	{
		angle *= Deg2Rad;
		float cosA = Cos(angle);
		float sinA = Sin(angle);

		Vector2[] result = new Vector2[vecs.Length];
		for (int i = 0; i < vecs.Length; i++)
		{
			float x = vecs[i].X;
			float y = vecs[i].Y;

			result[i] = new Vector2(cosA * x - sinA * y, sinA * x + cosA * y);
		}

		return result;
	}

	// Rotation relative to a point
	public Vector2 RotateAroundRadians(Vector2 point, float radians) 
	{
		Vector2 translated = this - point;

		float sin = Sin(radians);
		float cos = Cos(radians);

		float xComp = cos * translated.X - sin * translated.Y;
		float yComp = sin * translated.X + cos * translated.Y;

		return new Vector2(xComp, yComp) + point;
	}
	public Vector2 RotateAroundDegrees(Vector2 point, float degrees) 
	{
		return RotateAroundRadians(point, degrees * Deg2Rad);
	}

	public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => a + ((b - a) * t); // Linear interpolation function for vectors 

	//	-------------------------------------
	//		"Advanced" methods
	//	-------------------------------------
	public static float Dot(Vector2 a, Vector2 b)	// Calculates dot product (scalar product) of two vectors 
	{
		return (a.X * b.X) + (a.Y * b.Y);
	}
	public float Dot(Vector2 other) => Dot(this, other);	// Calculates dot product of this vector instance with another vector

	public Vector2 Normal() => new Vector2(-Y, X).Normalized(); // Creates a left hand normal for vector instance.

	//	-------------------------------------
	//		object class overrides
	//	-------------------------------------
	public override string ToString() => string.Format("({0}; {1})", X, Y);

	public override bool Equals(object obj) => obj is Vector2 vector && Equals(vector);
	public bool Equals(Vector2 other) => (X == other.X) && (Y == other.Y);
	public bool Equals(Vector2 other, float delta)	// Equality with maximum deviation per component 
	{
		if (X == other.X && Y == other.Y) return true;
		float xDif = Abs(X - other.X);
		float yDif = Abs(Y - other.Y);
		if (xDif <= delta && yDif <= delta) return true;
		return false;
	}

	public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);
	public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);

	public override int GetHashCode()   // Auto generated hashcode override 
	{
		int hashCode = 1861411795;
		hashCode = hashCode * -1521134295 + X.GetHashCode();
		hashCode = hashCode * -1521134295 + Y.GetHashCode();
		return hashCode;
	}
}