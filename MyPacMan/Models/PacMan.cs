
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Models
{
    public class PacMan : IRun, ITurn, Itransparent
    {
        PacManImg image = new PacManImg();
        private int y;
        private int x;
        private int direct_x;
        private int direct_y;
        private int nextdirect_x;
        private int nextdirect_y;
        private int sizeField;
        private Image currentPacman;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Logger updateLogger = LogManager.GetLogger("UpdateLogger");
        /// <summary>
        ///  Initialize a new instance of the <see cref="PacMan"/> 
        /// </summary>
        /// <param name="sizeField"></param>
        public PacMan(int sizeField)
        {
            this.direct_x = 0;
            this.direct_y = 0;
            this.x = 120; 
            this.y = 200; 
            this.SizeField = sizeField;
            currentPacman = new System.Windows.Controls.Image();
            PutImg();




        }
        /// <summary>
        /// Gets or sets the image  <see cref="Models.PacManImg" /> 
        /// </summary>
        public PacManImg Image { get => image; }
        /// <summary>
        /// Gets or sets the X coordinate for <see cref="Models.PacMan" /> 
        /// </summary>
        public int X { get => x; set => x = value; }
        /// <summary>
        ///  Gets or sets the Y coordinate for <see cref="Models.PacMan" /> 
        /// </summary>
        public int Y { get => y; set => y = value; }
        /// <summary>
        /// Gets or sets the X coordinate of  <see cref="Models.PacMan" />  direction
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
        /// Gets or sets the Y coordinate of  <see cref="Models.PacMan" />  direction
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
        ///  Gets or sets the X coordinate of  <see cref="Models.PacMan" />  direction on the next step
        /// </summary>
        public int Nextdirect_x
        {
            get => nextdirect_x;
            set
            {
                if (value == 0 || value == -1 || value == 1) nextdirect_x = value;
                else nextdirect_x = 0;
            }
        }
        /// <summary>
        ///  Gets or sets the Y coordinate of  <see cref="Models.PacMan" />  direction on the next step
        /// </summary>
        public int Nextdirect_y
        {
            get => nextdirect_y;
            set
            {
                if (value == 0 || value == -1 || value == 1) nextdirect_y = value;
                else nextdirect_y = 0;
            }
        }
        /// <summary>
        ///  Gets  image
        /// </summary>
        public Image CurrentPacman { get => currentPacman; set => currentPacman = value; }
        /// <summary>
        /// Runs the  <see cref="Models.PacMan" />
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
        /// Runs the  <see cref="Models.PacMan" /> through wall
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
        /// Turns the  <see cref="Models.PacMan" />
        /// </summary>
        public void Turn()
        {
            Direct_x = Nextdirect_x;
            Direct_y = Nextdirect_y;
            PutImg();

        }
        /// <summary>
        /// Puts to the  <see cref="Models.PacMan" /> image by the turn 
        /// </summary>
        public void PutImg()
        {
            if (direct_x == 1) image.PucImg = image.PucRight;
            if (direct_x == -1) image.PucImg = image.PucLeft;
            if (direct_y == 1) image.PucImg = image.PucDown;

            if (direct_y == -1) image.PucImg = image.PucUp;

        }
    }
}
