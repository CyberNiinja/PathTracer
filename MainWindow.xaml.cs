using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PathTracer.Materials;
using PathTracer.Objects;
using PathTracer.Rays;
using Color = PathTracer.Materials.Color;

namespace PathTracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image image;
        private WriteableBitmap bitmap;
        private Camera cam;
        public MainWindow()
        {
            //initialize Components
            InitializeComponent();
            Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0,0,0));
            image = new Image();
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(image, EdgeMode.Aliased);
            bitmap = new WriteableBitmap(800, 800,
                72, 72, PixelFormats.Rgb24, BitmapPalettes.WebPalette);
            image.Source = bitmap;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            Content = image;
            cam = new Camera(36f, new Vector3(0, 0, -4f), new Vector3(0, 0, 6f));
            var objects = CornellBox();
            //render Scene
            for (int x = 0; x < bitmap.PixelWidth; x++)
            {
                for (int y = 0; y < bitmap.PixelHeight; y++)
                {
                    if (x == 256 && y == 200)
                    {
                        Console.WriteLine(x);
                    }
                    var numRays = 64;
                    float xf = x / ((float)bitmap.PixelWidth / 2f) - 1f;
                    float yf = y / ((float)bitmap.PixelHeight / 2f) - 1f;
                    var ray = cam.CreateEyeRay(xf, yf);
                    Vector3 addColor = Vector3.Zero;
                    for (var i = 0; i < numRays; i++)
                    {
                        addColor += ComputeColor(ray,objects, new Random(), 0.2f);
                    }
                    var color = Color.ToRGB(addColor / numRays); //average rays
                    bitmap.WritePixels(new Int32Rect(x, y, 1, 1), new [] { color.R, color.G, color.B }, 3, 0);
                }
            }
        }

        /// <summary>
        /// Computes Color of a ray by tracing it back to it's light-origin
        /// </summary>
        /// <param name="ray">ray to compute the color of</param>
        /// <param name="bounces">number of bounces per ray</param>
        /// <returns></returns>
        public Vector3 ComputeColor(Ray ray, List<Sphere> objects, Random r, float p)
        {
            // find closest hitpoint
            foreach (Sphere sphere in objects)
            {
                ray.IntersectWith(sphere);
            }
            if (!ray.HasHit()) return Color.Black();
            var mat = ray.Closest.Material;
            //return mat.DiffuseColor(); // 2D
            var n = ray.Closest.NormalAtP(ray.HitPoint);

            if (r.NextDouble() < p)
            {
                return mat.EmissionColor();
            }
            Vector3 wr = GenerateRandomVectorInOmega(r, n);
            var emission = mat.EmissionColor();
            var cosTheta = Vector3.Dot(wr, Vector3.Normalize(n)) * 2f * MathF.PI / (1f - p);
            var brdf = mat.Brdf(wr, ray.Direction, n);
            var bounce = new Ray(ray.HitPoint, wr);
            var bounceColor = ComputeColor(bounce, objects, r, p);
            return emission + cosTheta * (brdf * bounceColor);
        }

        public static Vector3 GenerateRandomVectorInOmega(Random r, Vector3 n)
        {
            Vector3 v = Vector3.Zero;
            while (v.LengthSquared() < 1)
            {
                v = new Vector3(SampleFromU(r, -1f, 1f), SampleFromU(r, -1f, 1f), SampleFromU(r, -1f, 1f));
            }
            return (Vector3.Dot(Vector3.Normalize(v), n) > 0) ? v : -v;
        }

        public static float SampleFromU(Random r, float min, float max)
        {
            return (max - min) * (float)r.NextDouble() + min;
        }

        public List<Sphere> CornellBox()
        {
            List<Sphere> objects = new List<Sphere>
            {
                new Sphere(-1001f, 0, 0, 1000f, new MatteMat(Color.Red())), //left Sphere
                new Sphere(1001f, 0, 0, 1000f, new MatteMat(Color.Blue())), //right Sphere
                new Sphere(0, 0, 1001f, 1000f, new MatteMat(Color.Gray())), //back Sphere
                new Sphere(0, 1001f, 0, 1000f, new MatteMat(Color.Gray())), //bottom Sphere
                new Sphere(0, -1001f, 0, 1000f, new LightSource()), //top Sphere
                new Sphere(-0.6f, 0.7f, -0.6f, 0.3f, new GlossyMat(Color.Yellow())),
                new Sphere(0.3f, 0.4f, 0.3f, 0.6f, new GlossyMat(Color.Cyan())),

            };
            return objects;
        }


    }
}
