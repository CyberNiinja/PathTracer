using System;
using System.Numerics;
using System.Runtime.InteropServices;
using PathTracer.Materials;
using PathTracer.Rays;

namespace PathTracer.Objects
{
    public class Sphere
    {
        public Vector3 Position { get; set; }
        public float Radius { get; set; }
        public IMaterial Material { get; set; }

        public Sphere(float x, float y, float z, float radius, IMaterial mat)
        {
            Position = new Vector3(x, y, z);
            Radius = radius;
            Material = mat;
        }

        public Vector3 NormalAtP(Vector3 p)
        {
            return (p - Position);
        }

        /// <summary>
        /// Moves a point p outside of this sphere if it is inside of it.
        /// </summary>
        /// <param name="p">A point p</param>
        /// <returns>Vector of P if it's been moved or p if it wasn't</returns>
        public Vector3 MoveIfPIsInside(Vector3 p)
        {

            Vector3 n = NormalAtP(p);
            var pm = p;
            while(n.LengthSquared() < Radius * Radius)
            {
                pm += n / 10000;
                n = NormalAtP(pm);
            }

            return pm;
        }

        public override string ToString()
        {
            return $"Body=(Position:'{Position}', Color: '{Material.DiffuseColor()}')";
        }



    }
}
