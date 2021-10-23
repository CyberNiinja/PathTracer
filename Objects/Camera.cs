using System;
using System.Numerics;
using PathTracer.Rays;

namespace PathTracer.Objects
{
    public class Camera
    {
        public float FOV { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }

        public Camera(float fov, Vector3 position, Vector3 lookAt)
        {
            FOV = fov;
            Position = position;
            LookAt = lookAt;
        }

        public Ray CreateEyeRay(float x, float y)
        {
            var f = LookAt - Position;
            var r = Vector3.Cross(Vector3.UnitY, f);
            var u = Vector3.Cross(f, r);
            var d = 
                Vector3.Normalize(f) +
                MathF.Tan(FOV * (MathF.PI / 180f) / 2f) * x * Vector3.Normalize(r) +
                MathF.Tan(FOV * (MathF.PI / 180f) / 2f) * y * Vector3.Normalize(u);
            return new Ray(Position, Vector3.Normalize(d));
        }

    }
}
