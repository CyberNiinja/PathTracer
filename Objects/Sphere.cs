using System;
using System.Numerics;
using PathTracer.Materials;
using PathTracer.Rays;

namespace PathTracer.Objects
{
    public class Sphere : Body
    {
        public float Radius { get; set; }

        public Sphere(Vector3 position, float radius, IMaterial mat) : base(position, mat)
        {
            Radius = radius;
        }

        public override Vector3 SurfaceNormalAtP(Vector3 p)
        {
            return Position + p;
        }

        /// <summary>
        /// calculates if a ray hits this object
        /// </summary>
        /// <param name="ray"></param>
        /// <returns>length at which the ray intersects or -1 if it doesn't</returns>
        public override float Intersection(Ray ray)
        {
            var b = Vector3.Dot(2 * (Position - ray.Origin), Vector3.Normalize(ray.Direction));
            var c = (Position - ray.Origin).LengthSquared() - (Radius * Radius);

            // Quadratic Formula
            var discriminant = (b * b) - (4 * c);
            if (discriminant < 0) return -1f;
            var sqrt = Math.Sqrt(discriminant);
            var l1 = (float)(b + sqrt) / 2;
            var l2 = (float)(b - sqrt) / 2;

            if (l1 < 0 && l2 < 0) return -1f;
            if (l1 < 0) return l2;
            if (l2 < 0) return l1;
            return (l1 < l2) ? l1 : l2;
        }

        public override float DistanceToCamera(Camera camera)
        {
            return (Position - camera.Position).Length() - Radius;
        }
    }
}
