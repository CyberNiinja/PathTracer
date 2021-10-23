using System;
using System.Numerics;

namespace PathTracer.Materials
{
    public class LightSource : IMaterial
    {

        public LightSource()
        {
        }

        public Vector3 Brdf(Vector3 wr, Vector3 d, Vector3 n)
        {
            return DiffuseColor() * (1 / MathF.PI);
        }

        public Vector3 EmissionColor()
        {
            return Color.White();
        }

        public Vector3 DiffuseColor()
        {
            return Color.White()*10;
        }

        public Vector3 SpecularColor()
        {
            return Color.White();
        }
    }
}
