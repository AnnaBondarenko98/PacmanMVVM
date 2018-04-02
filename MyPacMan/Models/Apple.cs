using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Models
{
    public class Apple
    {
        private AppleImage image = new AppleImage();
        private int y;
        private int x;
        private Image currentImage;
        public bool Flag=false;
       
        /// <summary>
        /// Initialize a new instance of the <see cref="Apple"/> class
        /// </summary>
        public Apple()
        {
            currentImage = new System.Windows.Controls.Image();
        }
        /// <summary>
        /// Initialize a new instance of the <see cref="Apple"/> class with coordinates 
        /// </summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Apple(int x, int y)
        {
            this.x = x;
            this.y = y;
            currentImage = new System.Windows.Controls.Image();
        }
        /// <summary>
        /// Gets or sets the image  <see cref="Models.AppleImage" /> 
        /// </summary>
        public AppleImage Image { get => image;set { image = value; } }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.Apple" /> 
        /// </summary>
        public int X { get => x; set { x = value; } }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.Apple" /> 
        /// </summary>
        public int Y { get => y; set { y = value; } }
        /// <summary>
        ///  Gets or sets the control for image
        /// </summary>
        public Image CurrentImage { get => currentImage; set => currentImage = value; }
    }
}
