using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Models
{
    public class Wall
    {
        private WallImage image = new WallImage();
        private int y;
        private int x;
        /// <summary>
        /// Gets or sets the image  <see cref="Models.WallImage" /> 
        /// </summary>
        public WallImage Image { get => image; }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.WallImage" /> 
        /// </summary>
        public int X { get => x; set => x = value; }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.WallImage" /> 
        /// </summary>
        public int Y { get => y; set => y = value; }

        public Image CurrentImg { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="Wall"/> class with coordinates 
        /// </summary>
        public Wall()
        {
           CurrentImg= new Image();
        }
        public Wall(int x, int y)
        {
            this.X = x;
            this.Y = y;
            CurrentImg = new Image();
        }
    }
}
