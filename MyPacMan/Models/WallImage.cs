using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Models
{
    public class WallImage
    {
        public ImageSource image = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        Properties.Resource1.Stone.GetHbitmap(),
        IntPtr.Zero,
        Int32Rect.Empty,
        BitmapSizeOptions.FromEmptyOptions());
        /// <summary>
        /// Gets or sets image
        /// </summary>
        public ImageSource StoneImg { get; set; }

    }
}
