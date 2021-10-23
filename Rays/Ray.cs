using System;
using System.Collections.Generic;
using System.Numerics;
using PathTracer.Materials;
using PathTracer.Objects;

namespace PathTracer.Rays
{
    public class Ray
    {
        public Vector3 Origin { get; }
        public Vector3 Direction { get; }

        public Sphere Closest { get; private set; }

        private float _distanceToHit;
        public Vector3 HitPoint { get; private set; }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
            Closest = null;
        }
        public bool HasHit()
        {
            return Closest != null;
        }

        public void IntersectWith(Sphere s)
        {
            var dn = Vector3.Normalize(Direction);
            var b = Vector3.Dot(2 * (s.Position - Origin), dn);
            var c = (s.Position - Origin).LengthSquared() - (s.Radius * s.Radius);

            // Quadratic Formula
            var discriminant = (b * b) - (4 * c);
            var sqrt = Math.Sqrt(discriminant);
            float dist;
            if (discriminant < 0) return;
            var l1 = (float)(b + sqrt) / 2;
            var l2 = (float)(b - sqrt) / 2;
            if (l1 < 0 && l2 < 0) return;
            if (l1 < 0) dist = l2;
            else if (l2 < 0) dist = l1;
            else
            {
                dist = (l1 < l2) ? l1 : l2;
            }
            if (_distanceToHit != 0 && dist > _distanceToHit) return;
            _distanceToHit = dist;
            Closest = s;
            var hp = (Origin + Vector3.Normalize(Direction)) * _distanceToHit;
            HitPoint = Closest.MoveIfPIsInside(hp);
        }




    }

}
