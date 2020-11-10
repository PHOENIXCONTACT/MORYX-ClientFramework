using System.Drawing;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Extensions for the <see cref="Color"/>
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Linear interpolation from a specified color
        /// </summary>
        public static float Lerp(this float start, float end, float amount)
        {
            var difference = end - start;
            var adjusted = difference * amount;
            return start + adjusted;
        }

        /// <summary>
        /// Linear interpolation to a specified color
        /// </summary>
        public static Color Lerp(this Color colour, Color to, float amount)
        {
            // start colours as lerp-able floats
            float sr = colour.R, sg = colour.G, sb = colour.B;

            // end colours as lerp-able floats
            float er = to.R, eg = to.G, eb = to.B;

            // lerp the colours to get the difference
            byte r = (byte)sr.Lerp(er, amount),
                 g = (byte)sg.Lerp(eg, amount),
                 b = (byte)sb.Lerp(eb, amount);

            // return the new colour
            return Color.FromArgb(r, g, b);
        }
    }
}