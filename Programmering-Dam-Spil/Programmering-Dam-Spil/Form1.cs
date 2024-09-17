using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programmering_Dam_Spil
{
    public partial class Form1 : Form
    {

        PictureBox[] PictureBoxes = new PictureBox[33];

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(800, 600);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        
        private void placerBrikker()
        {
            Image whiteCircle = Image.FromFile("white_circle.png");
            Image blackCircle = Image.FromFile("black_circle_v2.png");
            Image hlBlackCircle = Image.FromFile("hl_black_circle_v2.png");
            Image hlWhiteCircle = Image.FromFile("hl_white_circle.png");

            for (int i = 1; i < 13; i++)
            {
                PictureBoxes = Controls.OfType<PictureBox>()
                    .Where(p => p.Name.StartsWith("pictureBox"))
                    .OrderBy(p => p.Name.Length)
                    .ThenBy(propa => propa.Name)
                    .ToArray();
                PictureBoxes[i].Image = whiteCircle;
                

            }

            for (int i = 21; i < 33; i++)
            {
                PictureBoxes = Controls.OfType<PictureBox>()
                    .Where(p => p.Name.StartsWith("pictureBox"))
                    .OrderBy(p => p.Name.Length)
                    .ThenBy(propa => propa.Name)
                    .ToArray();
                PictureBoxes[i].Image = blackCircle;
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            placerBrikker();
        }

        
    }
}
