using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace gen // taki tam dump metod
{
    public class Helper
    {
      private static Helper _h;
      public static Helper H
      {
         get
         {
            if (_h == null)
               _h = new Helper();
            return _h;
         }
      }

        public byte TextBoxToByte(TextBox oR)
        {
            if (!string.IsNullOrEmpty(oR.Text))
                return IntToByte(int.Parse(oR.Text));
            return 0;
        }

        public byte IntToByte(int i)
        {
            if (i < 0)
                return 0;
            if (i > 255)
                return 255;
            return (byte)i;
        }
        public static byte BinaryToByte(string s)
        {
            int i = Convert.ToInt32(s, 2);
            if (i < 0)
                return 0;
            if (i > 255)
                return 255;
            return (byte)i;
        }
        public static int Modulo(int i)
        {
            return i > 0 ? i : -i;
        }
        public static Color[] GenetareColors(int amount)
        {
            var colors = new Color[amount];
            Random rnd = new Random();
            for (int i = 0; i < amount; i++)
            {
                colors[i].R = (byte)rnd.Next(255);
                colors[i].G = (byte)rnd.Next(255);
                colors[i].B = (byte)rnd.Next(255);
            }
            return colors;
        }

        public static string ColorToRGBString(Color color)
        {
            return "(" + color.R + ", " + color.G + ", " + color.B + ")";
        }

        internal static double Modulo(double i)
        {
            return i > 0 ? i : -i;
        }
    }
}
