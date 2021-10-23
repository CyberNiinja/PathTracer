using System;
using System.Numerics;
using PathTracer.Rays;

namespace PathTracer.Materials
{
    public class GlossyMat : IMaterial
    {
        private readonly Vector3 _diffusion;
        private readonly Vector3 _reflection;

        public GlossyMat(Vector3 color)
        {
            _diffusion = color;
        }

        public Vector3 Brdf(Vector3 wr, Vector3 d, Vector3 n)
        {
            var epsilon = 0.01f;
            var m = 40;
            var dr = Vector3.Normalize(Vector3.Reflect(d, Vector3.Normalize(n)));
            if (Vector3.Dot(wr, dr) > 1f - epsilon)
            {
                return DiffuseColor() + (m * SpecularColor());
            }
            return DiffuseColor() * (1 / MathF.PI);
        }

        public Vector3 EmissionColor()
        {
            return Color.Black();
        }

        public Vector3 DiffuseColor()
        {
            return _diffusion;
        }

        public Vector3 SpecularColor()
        {
            return _reflection;
        }
    }
}

