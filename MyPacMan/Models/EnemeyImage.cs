using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Models
{
    public class EnemeyImage
    {

        private ImageSource enemeyDown = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.down.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource enemeyUp = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.up.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());
        private ImageSource enemeyRight = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.rigth.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());
        private ImageSource enemeyLeft = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.up.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource enemeyImg = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.left.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());


        /// <summary>
        /// Gets or Sets the image of Enemey,that turns down
        /// </summary>
        public ImageSource EnemeyDown { get => enemeyDown; set => enemeyDown = value; }
        /// <summary>
        /// Gets or Sets the image of Enemey,that turns up
        /// </summary>
        public ImageSource EnemeyUp { get => enemeyUp; set => enemeyUp = value; }
        /// <summary>
        /// Gets or Sets the image of Enemey,that turns right
        /// </summary>
        public ImageSource EnemeyRight { get => enemeyRight; set => enemeyRight = value; }
        /// <summary>
        /// Gets or Sets the image of Enemey,that turns left
        /// </summary>
        public ImageSource EnemeyLeft { get => enemeyLeft; set => enemeyLeft = value; }
        /// <summary>
        /// Gets or Sets the current  image of run Enemey
        /// </summary>
        public ImageSource EnemeyImg { get => enemeyImg; set => enemeyImg = value; }
    }
}
