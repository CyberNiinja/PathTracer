using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PathTracer.Materials;
using PathTracer.Objects;
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
        public MainWindow()
        {
            //initialize Components
            InitializeComponent();
            image = new Image();
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(image, EdgeMode.Aliased);
            bitmap = new WriteableBitmap((int)Width, (int)Height,
                96, 96, PixelFormats.Bgr32, null);
            image.Source = bitmap;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;
            Content = image;

            //render Scene
            Camera cam = new Camera(36f, new Vector3(0, 0, -4f), new Vector3(0, 0, 6f));
            Scene scene = CornellBox();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var ray = cam.CreateEyeRay(((float)x / ((float)Width / 2f) - 1f), ((float)y / ((float)Height / 2f) - 1f));
                    var color = scene.ComputeColor(ray, 3).ToRGB();
                    byte[] colors = { color.R, color.G, color.B, 0 };
                    bitmap.WritePixels(new Int32Rect(x, y, 1, 1), colors, 4, 0);
                }
            }
        }

        public Scene CornellBox()
        {
            List<Body> objects = new List<Body>
            {
                new Sphere(new Vector3(-1001f, 0, 0), 1000f, new DiffuseMaterial(Color.Red())), //Red Sphere
                new Sphere(new Vector3(1001f, 0, 0), 1000f, new DiffuseMaterial(Color.Blue())), //blue Sphere
                new Sphere(new Vector3(0, 0, 1001f), 1000f, new DiffuseMaterial(Color.Gray())), //blue Sphere
                new Sphere(new Vector3(0, 1001f, 0), 1000f, new DiffuseMaterial(Color.Gray())), //blue Sphere
                new Sphere(new Vector3(0, -1001f, 0), 1000f, new DiffuseMaterial(Color.White())), //blue Sphere
                new Sphere(new Vector3(-0.6f, 0.7f, -0.6f), 0.3f, new DiffuseMaterial(Color.Yellow())), //blue Sphere
                new Sphere(new Vector3(0.3f, 0.4f, 0.3f), 0.6f, new DiffuseMaterial(Color.Cyan())), //blue Sphere

            };

            return new Scene(objects);
        }
    }
}
