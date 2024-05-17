using System;
using static GXPEngine.Mathf; // Allows using Mathf functions

/// <summary>
/// Struct <c>Vector2</c> contains data and functionality of a 2-dimensional vector.
/// </summary>
public struct Vector2
{
	// ========================================================
	//
	//      Fields and Properties
	//
	// ========================================================
	private static Random Random = new Random();
	public float x;
	public float y;

	/// <summary>
	/// Gets or sets the angle of the vector in radians
	/// </summary>
	public float Radians
	{
		get => Atan2(y, x);
		set
		{
			float mag = Magnitude;
			this = GetUnitVectorRad(value) * mag;
		}
	}
	/// <summary>
	/// Gets or sets the angle of the vector in degrees
	/// </summary>
	public float Degrees
	{
		get => Rad2Deg(Radians);
		set
		{
			float radians = Deg2Rad(value);
			Radians = radians;
		}
	}
	/// <summary>
	/// Gets or sets the magnitude/length of the vector
	/// </summary>
	public float Magnitude
	{
		get => Length();
		set
		{
			Normalize();
			this *= value;
		}
	}

	/// <summary>
	/// Create a new <c>Vector2</c> instance.
	/// </summary>
	/// <param name="X">X component of the vector. Defaults to 0</param>
	/// <param name="Y">Y component of the vector. Defaults to 0</param>
	public Vector2(double X = 0, double Y = 0)
	{
		this.x = (float)X;
		this.y = (float)Y;
	}


	// ========================================================
	//
	//      Methods - Week 1
	//        - Mostly math operators
	//
	// ========================================================

	/// <summary>
	/// Componentwise vector addition
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static Vector2 operator +(Vector2 a, Vector2 b)
	{
		return new Vector2(a.x + b.x, a.y + b.y);
	}
	/// <summary>
	/// Negates the components of the vector. (Inverses the direction)
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	public static Vector2 operator -(Vector2 v)
	{
		return new Vector2(-v.x, -v.y);
	}
	/// <summary>
	/// Componentwise vector subtration.
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns>Difference <c>Vector2</c> of a and b</returns>
	public static Vector2 operator -(Vector2 a, Vector2 b)
	{
		return a + -b;
	}
	/// <summary>
	/// Scales the vector by a scalar value.
	/// </summary>
	/// <param name="s"></param>
	/// <param name="v"></param>
	/// <returns>Scaled <c>Vector2</c>.</returns>
	public static Vector2 operator *(Vector2 v, float s)
	{
		return new Vector2(v.x * s, v.y * s);
	}
	/// <summary>
	/// Scales the vector by a scalar value.
	/// </summary>
	/// <param name="s"></param>
	/// <param name="v"></param>
	/// <returns>Scaled <c>Vector2</c>.</returns>
	public static Vector2 operator *(float s, Vector2 v)
	{
		return v * s;
	}
	/// <summary>
	/// Scales the vector by inverted scalar.
	/// </summary>
	/// <param name="v"></param>
	/// <param name="s">Non zero scalar value</param>
	/// <returns>Scaled <c>Vector2</c>.</returns>
	/// <exception cref="DivideByZeroException"></exception>
	public static Vector2 operator /(Vector2 v, float s)
	{
		if (s == 0) throw new DivideByZeroException();
		return v * (1 / s);
	}
	/// <summary>
	/// Get the squared length/magnitude of this vector.
	/// </summary>
	/// <returns>Squared length/magnitude of this.</returns>
	public float LengthSquared()
	{
		return (x * x) + (y * y);
	}
	/// <summary>
	/// Get the length/magnitude of this vector.
	/// </summary>
	/// <returns>Length/Magnitude of this.</returns>
	public float Length()
	{
		return Sqrt(LengthSquared());
	}
	/// <summary>
	/// Turns this vector into a unit length vector
	/// </summary>
	public void Normalize()
	{
		this = Normalized();
	}
	/// <summary>
	/// Get the unit vector of this.
	/// </summary>
	/// <returns>Unit vector in same direction as this</returns>
	public Vector2 Normalized()
	{
		float length = Magnitude;
		if (length == 0) return new Vector2();
		return new Vector2(x, y) / length;
	}

	/// <summary>
	/// Sets the x and y component of this instance to supplied x and y
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetXY(float x, float y)
	{
		this.x = x;
		this.y = y;
	}


	// ========================================================
	//
	//      Methods - Week 2
	//        - Mostly angles and trigonometry
	//
	// ========================================================

	/// <summary>
	/// Converts angle in degrees to radians
	/// </summary>
	/// <param name="degrees">Angle in degrees</param>
	/// <remarks>Angle is not normalized to a specific range such as 0 to 2 * Pi</remarks>
	/// <returns>Degree in radians</returns>
	public static float Deg2Rad(float degrees)
	{
		return degrees * PI / 180;
	}
	/// <summary>
	/// Converts angle in radians to degrees
	/// </summary>
	/// <param name="radians">Angle in radians</param>
	/// <remarks>Angle is not normalized to a specific range such as 0 to 2 * Pi</remarks>
	/// <returns>Degree in radians</returns>
	public static float Rad2Deg(float radians)
	{
		return radians * 180 / PI;
	}
	/// <summary>
	/// Gets the unit vector for a given angle.
	/// </summary>
	/// <param name="degrees">Any angle in degrees</param>
	/// <returns>Unit vector in given direction</returns>
	public static Vector2 GetUnitVectorDeg(float degrees)
	{
		float radians = Deg2Rad(degrees);
		return GetUnitVectorRad(radians);
	}
	/// <summary>
	/// Gets the unit vector for a given angle.
	/// </summary>
	/// <param name="radians">Any angle in radians</param>
	/// <returns>Unit vector in given direction</returns>
	public static Vector2 GetUnitVectorRad(float radians)
	{
		return new Vector2(Cos(radians), Sin(radians));
	}
	/// <summary>
	/// Gets a unit vector in a random direction
	/// </summary>
	/// <returns>Random unit vector</returns>
	public static Vector2 RandomUnitVec()
	{
		return GetUnitVectorDeg(Random.Next(360));
	}

	public Vector2 RotatedRad(float angle)
	{
		float xComp = Cos(angle) * x - Sin(angle) * y;
		float yComp = Sin(angle) * x + Cos(angle) * y;
		return new Vector2(xComp, yComp);
	}
	public Vector2 RotatedDeg(float angle)
	{
		return RotatedRad(Deg2Rad(angle));
	}
	/// <summary>
	/// Rotates an array of vectors
	/// </summary>
	/// <param name="vecs"></param>
	/// <param name="angle"></param>
	/// <returns></returns>
	public static Vector2[] RotateVectorsRad(Vector2[] vecs, float angle)
	{
		float cosA = Cos(angle);
		float sinA = Sin(angle);

		Vector2[] result = new Vector2[vecs.Length];
		for (int i = 0; i < vecs.Length; i++)
		{
			float x = vecs[i].x;
			float y = vecs[i].y;

			result[i] = new Vector2(cosA * x - sinA * y, sinA * x + cosA * y);
		}

		return result;
	}
	/// <summary>
	/// Rotates an array of vectors
	/// </summary>
	/// <param name="vecs"></param>
	/// <param name="angle"></param>
	/// <returns></returns>
	public static Vector2[] RotateVectorsDeg(Vector2[] vecs, float angle)
	{
		angle = Deg2Rad(angle);
		float cosA = Cos(angle);
		float sinA = Sin(angle);

		Vector2[] result = new Vector2[vecs.Length];
		for (int i = 0; i < vecs.Length; i++)
		{
			float x = vecs[i].x;
			float y = vecs[i].y;

			result[i] = new Vector2(cosA * x - sinA * y, sinA * x + cosA * y);
		}

		return result;
	}

	/// <summary>
	/// Rotates a vector around a given point
	/// </summary>
	/// <param name="point"><c>Vector2</c> representing a pivot point</param>
	/// <param name="radians">Angle to rotate the instance by</param>
	/// <returns>Rotated copy of this</returns>
	public Vector2 RotateAroundRadians(Vector2 point, float radians)
	{
		Vector2 translated = this - point;
		translated.Radians += radians;
		return translated + point;
	}
	/// <summary>
	/// Rotates a vector around a given point
	/// </summary>
	/// <param name="point"><c>Vector2</c> representing a pivot point</param>
	/// <param name="degrees">Angle to rotate the instance by</param>
	/// <returns>Rotated copy of this</returns>
	public Vector2 RotateAroundDegrees(Vector2 point, float degrees)
	{
		float radians = Deg2Rad(degrees);
		return RotateAroundRadians(point, radians);
	}
	/// <summary>
	/// Linear interpolation of two vectors
	/// </summary>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <param name="t">Scalar value where <c>0<=t<=1</c></param>
	/// <returns>Interpolated <c>Vector2</c>. If t is 0 this is the same as a, if t is 1 this is the same as b</returns>
	public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
	{
		// The Lerp function, mathematically, is defined as lerp(a, b, t) = a + (b — a) * t
		return a + (b - a) * t;
	}


	// ========================================================
	//
	//      Methods - Week 4
	//        - Advanced methods
	//
	// ========================================================

	/// <summary>
	/// Gets the dot product of two vectors
	/// </summary>
	/// <param name="a">Any <c>Vector2</c></param>
	/// <param name="b">Any <c>Vector2</c></param>
	/// <returns>Dot product of the input <c>Vector2</c> objects</returns>
	public static float Dot(Vector2 a, Vector2 b)
	{
		return a.x * b.x + a.y * b.y;
	}
	/// <summary>
	/// Gets the dot product of the instance and another <c>Vector2</c>
	/// </summary>
	/// <param name="other"></param>
	/// <returns>Dot product of two <c>Vector2</c> objects</returns>
	public float Dot(Vector2 other)
	{
		return Dot(this, other);
	}
	/// <summary>
	/// Gets the normal (unit vector) of the instance
	/// </summary>
	/// <returns></returns>
	public Vector2 Normal()
	{
		return new Vector2(-y, x).Normalized();
	}
	/// <summary>
	/// Projects the <c>Vector2</c> instance on the normal of the target <c>Vector2</c>
	/// </summary>
	/// <param name="target">Any <c>Vector2</c> object</param>
	/// <returns>The magnitude/length of the projected vector</returns>
	public float VecReject(Vector2 target)
	{
		return Dot(target.Normal());
	}

	/// <summary>
	/// Class <c>Test</c> contains methods for unit testing the <c>Vector2</c> struct
	/// </summary>
	public static class Test
	{
		public static bool Week1Test()
		{
			Console.WriteLine("Constructor test");
			bool constructorTest = Vector2Test();
			Console.WriteLine();
			Console.WriteLine("Arithmetic tests");
			bool arithTest = ArithTest();
			Console.WriteLine();
			Console.WriteLine("Utility tests");
			bool utilTest = UtilTest();
			Console.WriteLine();
			bool allPass = constructorTest && arithTest && utilTest;
			Console.WriteLine($"Week 1 all tests passed: {allPass}");
			Console.WriteLine();
			return allPass;
		}
		public static bool Week2Test()
		{
			// Angle conversion
			Console.WriteLine("Angle conversion tests");
			bool angleConversionPass = AngleConvertTest();
			Console.WriteLine();
			// UnitConstruction
			Console.WriteLine("Unit vector construction tests");
			bool unitConstructionPass = UnitConstructionTest();
			Console.WriteLine();
			// Get/Set angle
			Console.WriteLine("Angle get/set tests");
			bool angleGetSetPass = AngleGetSetTest();
			Console.WriteLine();
			// Rotate around point
			Console.WriteLine("Rotate around point tests");
			bool rotateAroundPass = RotateTest();
			Console.WriteLine();

			bool allPass = angleConversionPass && unitConstructionPass && angleGetSetPass && rotateAroundPass;
			Console.WriteLine($"Week 2 all tests passed: {allPass}");
			Console.WriteLine();
			return allPass;
		}

		// Week 1
		public static bool Vector2Test()
		{
			Vector2 test = new Vector2(3, 8);
			DbgOutput(test, nameof(test));
			return !Equals(test, null);
		}
		public static bool ArithTest()
		{
			Vector2 vecA = new Vector2(1, 2);
			Vector2 vecB = new Vector2(6, 3);
			Vector2 expected;
			Vector2 actual;

			// Addition
			expected = new Vector2(7, 5);
			actual = vecA + vecB;
			bool addPassed = Equals(expected, actual);
			Console.WriteLine($"Addition test passed: {addPassed}");
			DbgOutput(expected, nameof(expected));
			DbgOutput(actual, nameof(expected));

			// Negation
			expected = new Vector2(-1, -2);
			actual = -vecA;
			bool negPassed = Equals(expected, actual);
			Console.WriteLine($"Negation test passed: {negPassed}");

			DbgOutput(expected, nameof(expected));
			DbgOutput(actual, nameof(expected));

			// Subtraction
			expected = new Vector2(-5, -1);
			actual = vecA - vecB;
			bool subPassed = Equals(expected, actual);
			Console.WriteLine($"Subtraction test passed: {subPassed}");
			DbgOutput(expected, nameof(expected));
			DbgOutput(actual, nameof(expected));

			// Scaling
			expected = new Vector2(4, 8);
			actual = vecA * 4;
			bool sclPassed = Equals(expected, actual);
			Console.WriteLine($"Scaling test passed: {sclPassed}");
			DbgOutput(expected, nameof(expected));
			DbgOutput(actual, nameof(expected));

			bool allPassed = addPassed && negPassed && subPassed && sclPassed;
			Console.WriteLine($"Arithmetic tests passed: {allPassed}");
			return allPassed;
		}
		public static bool UtilTest()
		{
			Vector2 vecA = new Vector2(3, 4);
			Vector2 vecB = new Vector2(8, 6);
			Vector2 expected;
			Vector2 actual;

			// Length/Magnitude
			bool lengthPassed = 5.0 == vecA.Magnitude;
			Console.WriteLine($"Length test passed: {lengthPassed}");
			Console.WriteLine("[expected: 5.0]");
			Console.WriteLine($"[actual: {vecA.Magnitude:F1}]");

			// Normalize
			expected = new Vector2(8 / 10f, 6 / 10f);
			actual = vecB.Normalized();

			bool normalPassed = Compare(actual, expected, 0.00001f);
			Console.WriteLine($"Normalize test passed: {normalPassed}");
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));

			// Lerp
			expected = new Vector2(11 / 2f, 5);
			actual = Lerp(vecA, vecB, 0.5f);
			bool lerpPassed = Compare(expected, actual, 0.00001f);
			Console.WriteLine($"Lerp test passed: {lerpPassed}");
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));

			bool allPassed = lengthPassed && normalPassed && lerpPassed;
			Console.WriteLine($"Utility tests passed: {allPassed}");
			return allPassed;
		}

		// Week 2
		public static bool AngleConvertTest()
		{
			float degrees = 135f;
			float radians = 3 * PI / 4;
			float actual = Deg2Rad(degrees);
			bool d2rPass = Compare(actual, radians, 0.00001f);
			Console.WriteLine($"Degrees to radians test passed: {d2rPass}");
			DbgOutput(degrees, nameof(degrees));
			DbgOutput(actual, "actual");
			DbgOutput(3 * PI / 4, "expected");

			degrees = 45f;
			radians = PI / 4;
			actual = Rad2Deg(radians);
			bool r2dPass = Compare(degrees, actual, 0.00001f);
			Console.WriteLine($"Radians to degrees test passed: {r2dPass}");
			DbgOutput(radians, nameof(radians));
			DbgOutput(actual, "actual");
			DbgOutput(45f, "expected");

			bool angleConvertPass = d2rPass && r2dPass;
			Console.WriteLine($"Angle conversion test passed: {angleConvertPass}");
			return angleConvertPass;
		}
		public static bool UnitConstructionTest()
		{
			Vector2 actual;
			Vector2 expected;

			// GetUnitVectorDeg()
			expected = new Vector2(0, -1);
			actual = GetUnitVectorDeg(-90);
			bool unitDegTest = Compare(expected, actual, 0.00001f);
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));
			Console.WriteLine($"Unit from degrees test passed: {unitDegTest}");

			// GetUnitVectorRad()
			expected = new Vector2(0, 1);
			actual = GetUnitVectorRad(PI / 2);
			bool unitRadTest = Compare(expected, actual, 0.00001f);
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));
			Console.WriteLine($"Unit from radians test passed: {unitRadTest}");

			// RandomUnitVector()
			actual = RandomUnitVec();
			float magnitude = actual.Magnitude;
			float expectedMagnitude = 1.0f;
			bool randomUnitTest = Compare(expectedMagnitude, magnitude, 0.00001f);
			DbgOutput(magnitude, nameof(magnitude));
			DbgOutput(expectedMagnitude, "expected");
			Console.WriteLine($"Random unit vector test passed: {randomUnitTest}");

			bool unitConstructionPass = unitDegTest && unitRadTest && randomUnitTest;
			Console.WriteLine($"Unit construction test passed: {unitConstructionPass}");
			return unitConstructionPass;
		}
		public static bool AngleGetSetTest()
		{
			Vector2 vecA = new Vector2(-3, 3);
			float actualFloat;
			float expectedFloat;

			// Get degrees
			expectedFloat = 135f;
			actualFloat = vecA.Degrees;
			bool getDegPass = actualFloat == expectedFloat;
			Console.WriteLine($"Get degrees test passed: {getDegPass}");
			DbgOutput(actualFloat, nameof(actualFloat));
			DbgOutput(expectedFloat, nameof(expectedFloat));

			// Get radians
			expectedFloat = 3 * PI / 4;
			actualFloat = vecA.Radians;
			bool getRadPass = actualFloat == expectedFloat;
			Console.WriteLine($"Get radians test passed: {getRadPass}");
			DbgOutput(actualFloat, nameof(actualFloat));
			DbgOutput(expectedFloat, nameof(expectedFloat));

			Vector2 expectedVec;
			Vector2 actualVec;
			// Set degrees
			vecA = new Vector2(3, 4);
			expectedVec = new Vector2(0, 5);
			actualVec = vecA;
			actualVec.Degrees = 90;
			bool setDegPass = Compare(actualVec, expectedVec, 0.00001f);
			Console.WriteLine($"Set degrees test passed: {setDegPass}");
			DbgOutput(actualVec, nameof(actualVec));
			DbgOutput(expectedVec, nameof(expectedVec));

			// Set radians
			vecA = new Vector2(3, 4);
			expectedVec = new Vector2(0, -5);
			actualVec = vecA;
			actualVec.Radians = -PI / 2;
			bool setRadPass = Compare(actualVec, expectedVec, 0.00001f);
			Console.WriteLine($"Set radians test passed: {setRadPass}");
			DbgOutput(actualVec, nameof(actualVec));
			DbgOutput(expectedVec, nameof(expectedVec));

			bool getSetTestPass = getDegPass && getRadPass && setDegPass && setRadPass;
			Console.WriteLine($"Get/Set angles test passed: {getSetTestPass}");
			return getSetTestPass;
		}
		public static bool RotateTest()
		{
			Vector2 vecA = new Vector2(2, 1);
			Vector2 vecB = new Vector2(3, 4);
			// Rotate vecB quarter turn around vecA. Result (-1, 2)
			// Rotate around point (degrees)
			Vector2 expected = new Vector2(-1, 2);
			Vector2 actual = vecB.RotateAroundDegrees(vecA, 90);
			bool rotateDeg = Compare(actual, expected, 0.00001f);
			Console.WriteLine($"Rotate around a point (degrees) passed: {rotateDeg}");
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));

			// Rotate around point (radians)
			vecB = new Vector2(3, 4);
			actual = vecB.RotateAroundRadians(vecA, PI / 2);
			bool rotateRad = Compare(actual, expected, 0.00001f);
			Console.WriteLine($"Rotate around a point (radians) passed: {rotateDeg}");
			DbgOutput(actual, nameof(actual));
			DbgOutput(expected, nameof(expected));

			bool rotateAroundPass = rotateDeg && rotateRad;
			Console.WriteLine($"Rotate around point test passed: {rotateAroundPass}");
			return rotateAroundPass;
		}

		// Week 3
		public static bool DotProductTest()
		{
			Vector2 vecA = new Vector2(2, 1);
			Vector2 vecB = new Vector2(3, 4);
			float dotProduct = Dot(vecA, vecB);
			// Should be, 2 * 3 + 1 * 4 = 10;
			float expected = 10;
			DbgOutput(expected, "Expected");
			DbgOutput(dotProduct, "Actual");
			bool DotProductPass = expected == dotProduct;
			Console.WriteLine($"Dot product test passed: {DotProductPass}");
			return DotProductPass;
		}

		// Util
		public static void DbgOutput(Vector2 vec, string name)
		{
			Console.WriteLine($"[{name}: {vec:F2}]");
		}
		public static void DbgOutput(object obj, string name)
		{
			Console.WriteLine($"[{name}: {obj:F2}]");
		}
		public static bool Compare(Vector2 vec1, Vector2 Vector2, float delta)
		{
			return Compare(vec1.x, Vector2.x, delta) && Compare(vec1.y, Vector2.y, delta);
		}
		public static bool Compare(float a, float b, float delta)
		{
			return Abs(a - b) < delta;
		}
	}

	public override string ToString()
	{
		return String.Format("({0};{1})", x, y);
	}
}