using System;
using System.Numerics;

namespace PathTracer.Materials
{
    public interface IMaterial
    {
        public Vector3 Brdf(Vector3 wr, Vector3 d, Vector3 n);
        public Vector3 EmissionColor();
        public Vector3 DiffuseColor();
        public Vector3 SpecularColor();
    }

    public static class Color
    {
        /// <summary>
        /// Gamma Correction as shown in Slide 24 of 02 Optics and ColorSpaces.pdf
        /// </summary>
        /// <param name="val"></param>
        /// <param name="valIsCorrect"></param>
        /// <returns></returns>
        private static float GammaCorrect(float val, bool valIsCorrect)
        {
            return valIsCorrect ? MathF.Pow(val, 2.2f) : MathF.Pow((val > 1f) ? 1f : val, 1f / 2.2f);
        }

        /// <summary>
        /// Converts this linearRGB Color to sRGB
        /// </summary>
        /// <returns>gamma-corrected rgb 8-bit integers</returns>
        public static (byte R, byte G, byte B) ToRGB(Vector3 color)
        {
            return (
                (byte)(GammaCorrect(color.X, false) * 255),
                (byte)(GammaCorrect(color.Y, false) * 255),
                (byte)(GammaCorrect(color.Z, false) * 255));
        }

        /// <summary>
        /// Takes gamma-corrected rgb values and converts them to linear rgb
        /// </summary>
        /// <param name="r">gamma-corrected red channel</param>
        /// <param name="g">gamma-corrected green channel</param>
        /// <param name="b">gamma-corrected blue channel</param>
        /// <returns>Color Instance (linear RGB)</returns>
        public static Vector3 ToLinear(int r, int g, int b)
        {
            return new Vector3(
                GammaCorrect((float)r / 255f, true),
                GammaCorrect((float)g / 255f, true),
                GammaCorrect((float)b / 255f, true)
            );
        }



        public static Vector3 Black()
        {
            return new Vector3(0, 0, 0);
        }

        public static Vector3 White()
        {
            return ToLinear(255, 255, 255);
        }

        public static Vector3 Red()
        {
            return ToLinear(255, 0, 0);
        }

        public static Vector3 Green()
        {
            return ToLinear(0, 255, 0);
        }

        public static Vector3 Blue()
        {
            return ToLinear(0, 0, 255);
        }

        public static Vector3 Cyan()
        {
            return ToLinear(0, 255, 255);
        }

        public static Vector3 Yellow()
        {
            return ToLinear(255, 255, 0);
        }

        public static Vector3 Magenta()
        {
            return ToLinear(255, 0, 255);
        }

        public static Vector3 Gray()
        {
            return ToLinear(128, 128, 128);
        }

    }
}