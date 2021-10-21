using System.Numerics;
using PathTracer.Materials;
using PathTracer.Rays;

namespace PathTracer.Objects
{
    public abstract class Body
    {
        public Vector3 Position { get; set; }
        public IMaterial Material { get; set; }

        protected Body(Vector3 position, IMaterial material)
        {
            Position = position;
            Material = material;
        }
        public abstract float Intersection(Ray ray);

        public abstract Vector3 SurfaceNormalAtP(Vector3 p);
        public abstract float DistanceToCamera(Camera camera);


        public override string ToString()
        {
            return $"Sphere=(Position:'{Position}')";
        }
    }
}
