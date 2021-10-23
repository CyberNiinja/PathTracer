using System;
using System.Numerics;

namespace PathTracer.Materials
{
    public class MatteMat : IMaterial
    {
        private readonly Vector3 _diffusion;

        public MatteMat(Vector3 color)
        {
            _diffusion = color;
        }

        public Vector3 Brdf(Vector3 wr, Vector3 d, Vector3 n)
        {
            return _diffusion * (1 / MathF.PI);
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
            return Color.Black();
        }
    }
}