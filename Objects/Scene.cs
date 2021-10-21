using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using PathTracer.Materials;
using PathTracer.Rays;

namespace PathTracer.Objects
{
    public class Scene
    {
        public List<Body> Children { get; set; }

        public Scene(List<Body> children)
        {
            Children = children;
        }

        /// <summary>
        /// Computes Color of a ray by tracing it back to it's light-origin
        /// </summary>
        /// <param name="ray"></param>
        /// <returns></returns>
        public Color ComputeColor(Ray ray, int bounceCap)
        {
            var closest = FindClosestHit(ray);
            return closest.Body.Material.Emission();
        }

        public void AddBody(Body body)
        {
            Children.Add(body);
        }

        public (float Distance, Body Body) FindClosestHit(Ray ray)
        {
            var all = Children.Select(body => (body.Intersection(ray),body));
            var hits =  all.Where(el => el.Item1 > 0).OrderBy(el => el.Item1);
            return hits.FirstOrDefault();
            
        }
    }
}