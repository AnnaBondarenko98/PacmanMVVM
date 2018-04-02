
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Models
{
    public class Enemey : IRun, ITurn, Itransparent
    {
        private EnemeyImage image = new EnemeyImage();
        private Image currentImage;
        private int y;
        private int x;
        private int direct_x;
        private int direct_y;
        static Random random;
        private int sizeField;
        /// <summary>
        ///  Initialize a new instance of the <see cref="Enemey"/> class  with coordinates 
        /// </summary>
        /// <param name="sizeField"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Enemey(int sizeField, int x, int y)
        {
            CurrentImage = new Image();
            random = new Random();
            this.SizeField = sizeField;
            if (random.Next(5000) < 2500)
            {
                Direct_y = 0; ;
                Direct_x = random.Next(-1, 2);
                while (Direct_x == 0)
                {
                    Direct_x = random.Next(-1, 2);
                }


            }
            else
            {
                Direct_x = 0; ;
                Direct_y = random.Next(-1, 2);
                while (Direct_y == 0)
                {
                    Direct_y = random.Next(-1, 2);
                }
            }

            PutImg();
            this.x = x;
            this.y = y;

        }
        /// <summary>
        /// Gets or sets the image  <see cref="Models.EnemeyImage" /> 
        /// </summary>
        public EnemeyImage Image { get => image; }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.Enemey" /> 
        /// </summary>
        public int X { get => x; set => x = value; }
        /// <summary>
        ///  Gets or sets the Y coordinate for <see cref="Models.Enemey" /> 
        /// </summary>
        public int Y { get => y; set => y = value; }
        /// <summary>
        /// Gets or sets the X coordinate of  <see cref="Models.Enemey" />  direction
        /// </summary>
        public int Direct_x
        {
            get => direct_x;
            set
            {
                if (value == 0 || value == -1 || value == 1) direct_x = value;
                else direct_x = 0;
            }
        }
        /// <summary>
        /// Gets or sets the Y coordinate of  <see cref="Models.Enemey" />  direction
        /// </summary>
        public int Direct_y
        {
            get => direct_y;
            set
            {
                if (value == 0 || value == -1 || value == 1) direct_y = value;
                else direct_y = 0;
            }
        }
        /// <summary>
        /// Returns or sets a size of field for game
        /// </summary>
        public int SizeField { get => sizeField; set => sizeField = value; }
        /// <summary>
        /// Gets  image
        /// </summary>
        public Image CurrentImage { get => currentImage; set => currentImage = value; }
        /// <summary>
        /// Runs the  <see cref="Models.Enemey" />
        /// </summary>
        public void Run()
        {
            x += Direct_x;
            y += Direct_y;
            if (Math.IEEERemainder(x, 40) == 0 && Math.IEEERemainder(y, 40) == 0)
            {
                Turn();

            }
            Transparent();
        }
        /// <summary>
        /// Turns the  <see cref="Models.Enemey" />
        /// </summary>
        public void Turn()
        {

            if (random.Next(5000) < 2500)
            {
                if (Direct_y == 0)
                    Direct_x = 0;
                while (direct_y == 0)
                {
                    Direct_y = random.Next(-1, 2);
                }

            }
            else
            {
                if (Direct_x == 0)
                    Direct_y = 0;
                while (Direct_x == 0)
                {
                    Direct_x = random.Next(-1, 2);
                }


            }
            PutImg();

        }
        /// <summary>
        /// Runs the  <see cref="Models.Enemey" /> through wall
        /// </summary>
        public void Transparent()
        {
            if (x == -1)
            {
                x = sizeField - 21;
            }
            if (x == sizeField - 19)
            {
                x = 1;
            }
            if (y == -1)
            {
                y = sizeField - 21;
            }
            if (y == sizeField - 19)
            {
                y = 1;
            }
        }
        /// <summary>
        /// Puts to the  <see cref="Models.Enemey" /> image by the turn 
        /// </summary>
        public void PutImg()
        {
            if (direct_x == 1) image.EnemeyImg = image.EnemeyRight;
            if (direct_x == -1) image.EnemeyImg = image.EnemeyLeft;
            if (direct_y == 1) image.EnemeyImg = image.EnemeyDown;

            if (direct_y == -1) image.EnemeyImg = image.EnemeyUp;

        }
    }
}
