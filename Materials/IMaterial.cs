using System.Numerics;

namespace PathTracer.Materials
{
    public interface IMaterial
    {
        public Vector3 Brdf();
        public Color Emission();
    }
}