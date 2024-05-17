using static GXPEngine.Mathf;

namespace GXPEngine
{
	internal static class MathUtils
	{
		/// <summary>
		/// Calculate the discriminant to use in quadratic formula
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="c"></param>
		/// <returns>
		/// The discriminant of a quadratic equation.
		/// - if D < 0 there are no roots for the given equation.
		/// - if D = 0 there is one root for the given equation.
		/// - if D > 0 there are two roots for the given equation.
		/// </returns>
		public static float GetDiscriminant(float a, float b, float c)
		{
			return (b * b) - (4 * a * c);
		}
		/// <summary>
		/// Calculate the roots given a, b and the discriminant of a quadratic equation (ax^2 + bx + c)
		/// </summary>
		/// <param name="discriminant">Discriminant should be positive or zero, will throw an error if negative</param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns>Two floats representing x for the roots of an equation</returns>
		public static (float rootA, float rootB) GetRoots(float discriminant, float a, float b)
		{
			float sqrtDiscriminant = Sqrt(discriminant);
			float rootA = (-b - sqrtDiscriminant) / (2 * a);
			float rootB = (-b + sqrtDiscriminant) / (2 * a);
			return (rootA, rootB);
		}
	}
}
