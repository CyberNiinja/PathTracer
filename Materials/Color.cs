using System;
using System.Numerics;
using System.Windows;

namespace PathTracer.Materials
{
    public class Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        private static float GammaCorrect(float val, bool valIsCorrect)
        {
            return valIsCorrect ? MathF.Pow(val, 2.2f) : MathF.Pow((val > 1) ? 1 : val, 1f / 2.2f);
        }

        /// <summary>
        /// Takes gamma-corrected rgb values and converts them to linear rgb
        /// </summary>
        /// <param name="r">gamma-corrected red channel</param>
        /// <param name="g">gamma-corrected green channel</param>
        /// <param name="b">gamma-corrected blue channel</param>
        /// <returns>Color Instance (linear RGB)</returns>
        public static Color ToLinear(float r, float g, float b)
        {
            var gamma = 1 / 2.2f;
            return new Color(
                (int)GammaCorrect(r, true) / 255,
                (int)GammaCorrect(g, true) / 255,
                (int)GammaCorrect(b, true) / 255
                );
        }

        /// <summary>
        /// Converts this linearRGB Color to sRGB
        /// </summary>
        /// <returns>gamma-corrected rgb 8-bit integers</returns>
        public (byte R, byte G, byte B) ToRGB()
        {
            var gamma = 1 / 2.2f;
            var r = GammaCorrect(R, false) * 255;
            var g = GammaCorrect(G, false) * 255;
            var b = GammaCorrect(B, false) * 255;
            return ((byte)r, (byte)g, (byte)b);
        }



        public static Color Black()
        {
            return new Color(0, 0, 0);
        }

        public static Color White()
        {
            return new Color(1, 1, 1);
        }

        public static Color Red()
        {
            return new Color(1, 0, 0);
        }

        public static Color Green()
        {
            return new Color(0, 1, 0);
        }

        public static Color Blue()
        {
            return new Color(0, 0, 1);
        }

        public static Color Cyan()
        {
            return new Color(0, 1, 1);
        }

        public static Color Yellow()
        {
            return new Color(1, 1, 0);
        }

        public static Color Magenta()
        {
            return new Color(1, 0, 1);
        }

        public static Color Gray()
        {
            return new Color(0.5f, 0.5f, 0.5f);
        }

    }
}