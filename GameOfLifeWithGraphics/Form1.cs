using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLifeWithGraphics
{
    public partial class Form1 : Form
    {
        private const int CELL_DIMENSION = 2;

        private GoLWorld World = new GoLWorld(250);

        private Rectangle cell = new Rectangle(0, 0, CELL_DIMENSION, CELL_DIMENSION);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Prevent flickering when drawing with GDI+
            DoubleBuffered = true;

            World.Initialize();

            Width = World.WorldDimension * CELL_DIMENSION;
            Height = World.WorldDimension * CELL_DIMENSION;

            Paint += Form1_Paint;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle backdrop = new Rectangle(0, 0, this.Width, this.Height);

            e.Graphics.FillRectangle(Brushes.Black, backdrop);

            for (int i = 0; i < World.World.GetUpperBound(0); i++)
            {
                for (int j = 0; j < World.World.GetUpperBound(1); j++)
                {
                    Brush brush;

                    if(World.World[i,j])
                    {
                        brush = Brushes.White;
                    }
                    else
                    {
                        brush = Brushes.Black;
                    }
                    
                    e.Graphics.FillRectangle(brush, new Rectangle(CELL_DIMENSION * j, CELL_DIMENSION * i, CELL_DIMENSION, CELL_DIMENSION));
                }
            }

            World.Evolve();
        }

        private void appTimer_Tick(object sender, EventArgs e)
        {
            // Refresh form every tick of the timer
            Refresh();
        }
    }
}
