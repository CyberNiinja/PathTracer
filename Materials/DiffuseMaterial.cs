using System.Numerics;

namespace PathTracer.Materials
{
    public class DiffuseMaterial : IMaterial
    {
        public Color _emission;

        public DiffuseMaterial(Color emission)
        {
            _emission = emission;
        }

        public Vector3 Brdf()
        {
            throw new System.NotImplementedException();
        }

        public Color Emission()
        {
            return _emission;
        }
    }
}