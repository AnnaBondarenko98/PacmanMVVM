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
    public class PacManImg
    {
        private ImageSource pucDown = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.downFirst.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource pucUp = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.upFirst.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource pucRight = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.rigthFirst.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource pucLeft = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.leftFirst.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());

        private ImageSource pucImg = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
      Properties.Resource1.downFirst.GetHbitmap(),
      IntPtr.Zero,
      Int32Rect.Empty,
      BitmapSizeOptions.FromEmptyOptions());
        /// <summary>
        ///  Gets or Sets the image of Pacman,that turns down
        /// </summary>
        public ImageSource PucDown { get => pucDown; set => pucDown = value; }
        /// <summary>
        /// Gets or Sets the image of Pacman,that turns up
        /// </summary>
        public ImageSource PucUp { get => pucUp; set => pucUp = value; }
        /// <summary>
        /// Gets or Sets the image of Pacman,that turns right
        /// </summary>
        public ImageSource PucRight { get => pucRight; set => pucRight = value; }
        /// <summary>
        /// Gets or Sets the image of Pacman,that turns left
        /// </summary>
        public ImageSource PucLeft { get => pucLeft; set => pucLeft = value; }
        /// <summary>
        /// Gets or Sets the current  image of run PacMan
        /// </summary>
        public ImageSource PucImg { get => pucImg; set => pucImg = value; }
    }
}
