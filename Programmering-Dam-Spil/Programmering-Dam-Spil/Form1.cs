using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programmering_Dam_Spil
{
    public partial class Form1 : Form
    {

        PictureBox[] PictureBoxes = new PictureBox[33];
        int[] BrikPos = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; //0: tom | 1: hvid | 2: sort | 3: hvid HL | 4: sort HL | 5: hvid konge | 6: sort konge | 7: hvid konge HL | 8: sort konge HL
        int[] BoardSides = {5,13,21,29,4,12,20,28 };
        int[] LigeRækker = { 5, 6, 7, 8, 13, 14, 15, 16, 21, 22, 23, 24, 29, 30, 31, 32 };
        int[] UligeRækker = { 1, 2, 3, 4, 9, 10, 11, 12, 17, 18, 19, 20, 25, 26, 27, 28 };
        bool Highlighted;
        int picValgt; //Husker den tallet på den picturebox der er highlighted. 
        string fjernBrik; //String der skal definere hvilken brik der skal fjernes, efter et hop. 
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(800, 600);
            placerBrikker();
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
            //Placerer hvide brikker
            for (int i = 1; i < 13; i++)
            {
                PictureBoxes = Controls.OfType<PictureBox>()
                    .Where(p => p.Name.StartsWith("pictureBox"))
                    .OrderBy(p => p.Name.Length)
                    .ThenBy(propa => propa.Name)
                    .ToArray();
                PictureBoxes[i].Image = whiteCircle;
                BrikPos[i] = 1;

            }
            
            //Placerer sorte brikker 
            for (int i = 21; i < 33; i++)
            {
                PictureBoxes = Controls.OfType<PictureBox>()
                    .Where(p => p.Name.StartsWith("pictureBox"))
                    .OrderBy(p => p.Name.Length)
                    .ThenBy(propa => propa.Name)
                    .ToArray();
                PictureBoxes[i].Image = blackCircle;
                BrikPos[i] = 2;

            }
            //Fjerner brikker i midten af spillebrættet 
            for (int i = 13; i < 21; i++)
            {
                PictureBoxes = Controls.OfType<PictureBox>()
                    .Where(p => p.Name.StartsWith("pictureBox"))
                    .OrderBy(p => p.Name.Length)
                    .ThenBy(propa => propa.Name)
                    .ToArray();
                PictureBoxes[i].Image = null;
                BrikPos[i] = 0;


            }
        }
        
        private void Pick(int nr)
        {
            //Denne metode ændrer billedet i en picturebox, alt efter hvilket tal der står på dets tilsvarende plads i arrayet Brikpos[]
            Image whiteCircle = Image.FromFile("white_circle.png");
            Image blackCircle = Image.FromFile("black_circle_v2.png");
            Image hlBlackCircle = Image.FromFile("hl_black_circle_v2.png");
            Image hlWhiteCircle = Image.FromFile("hl_white_circle.png");
            // Der bruges en bool kaldet highligted, så der højest kan være en brik der er highligted. 
            if (BrikPos[nr] == 0 && Highlighted == true)
            {
                Flytte(nr);
                Hoppe(nr);
            } 
            else if (BrikPos[nr] == 1 && Highlighted == false)
            {
                BrikPos[nr] += 2;
                PictureBoxes[nr].Image = hlWhiteCircle;
                Highlighted = true;
                picValgt = nr;
                txtPicValgt.Text = picValgt.ToString();
            } else if (BrikPos[nr] == 2 && Highlighted == false)
            {
                BrikPos[nr] += 2;
                PictureBoxes[nr].Image = hlBlackCircle;
                Highlighted = true;
                picValgt = nr;
                txtPicValgt.Text = picValgt.ToString();
            } else if (BrikPos[nr] == 3)
            {
                BrikPos[nr] -= 2;
                PictureBoxes[nr].Image = whiteCircle;
                Highlighted = false;
            } else if (BrikPos[nr] == 4)
            {
                BrikPos[nr] -= 2;
                PictureBoxes[nr].Image = blackCircle;
                Highlighted = false;
            }

            //TROUBLESHOOTING: Skriver talværdier i array Brikpos[] ind i tekstboks
            textBox1.Text = "";
            for (int i = 0; i <= 32; i++)
            {
                textBox1.Text += BrikPos[i].ToString() + ", ";
            }
        }

        private void Flytte(int nr)
        {
            // BRIK MOVEMENT 
            Image whiteCircle = Image.FromFile("white_circle.png");
            Image blackCircle = Image.FromFile("black_circle_v2.png");
            //FLYTTE SIDEBRIKKER
            //Flytte hvid sidebrik til feltet den står på plus 4. 
            if (BoardSides.Contains(picValgt) && BrikPos[picValgt] == 3)
            {
                if (nr == picValgt + 4 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                }
            }
            //Flytte sort sidebrik til feltet den står på minus 4. 
            if (BoardSides.Contains(picValgt) && BrikPos[picValgt] == 4)
            {
                if (nr == picValgt - 4 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                }
            }
            //FLYTTE IKKE-SIDEBRIKKER
            //ULIGE RÆKKER
            //Flytte ulige hvid brik til feltet den står på plus 4 eller plus 5.
            if (UligeRækker.Contains(picValgt) && BrikPos[picValgt] == 3)
            {
                if (nr == picValgt + 4 && BrikPos[nr] == 0 || nr == picValgt + 5 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                }
            }
            //Flytte ulige sort brik til feltet den står på minus 3 eller minus 4.
            if (UligeRækker.Contains(picValgt) && BrikPos[picValgt] == 4)
            {
                if (nr == picValgt - 3 && BrikPos[nr] == 0 || nr == picValgt - 4 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                }
            }
            //LIGE RÆKKER 
            //Flytte lige hvid brik til feltet den står på plus 3 eller plus 4.
            if (LigeRækker.Contains(picValgt) && BrikPos[picValgt] == 3)
            {
                if (nr == picValgt + 3 && BrikPos[nr] == 0 || nr == picValgt + 4 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                }
            }
            //Flytte lige sort brik til feltet den står på minus 4 eller minus 5.
            if (LigeRækker.Contains(picValgt) && BrikPos[picValgt] == 4)
            {
                if (nr == picValgt - 4 && BrikPos[nr] == 0 || nr == picValgt - 5 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                }
            }

        }
        //HUSKE Ulige rækjkker plus 7, lige rækker plus 9
        private void Hoppe(int nr)
        {
            //Tanken bag denne metode er at en brik skal kunne hoppe over en brik af modsat farve, men kun hvis der ikke allerede står en der man vil hoppe hen. 
            // BRIK HOPPE MOVEMENT 
            Image whiteCircle = Image.FromFile("white_circle.png");
            Image blackCircle = Image.FromFile("black_circle_v2.png");
            //FLYTTE SIDEBRIKKER
            //Flytte hvid sidebrik til feltet den står på plus 9. 
            if (BrikPos[picValgt] == 3 && BoardSides.Contains(picValgt) && UligeRækker.Contains(picValgt))
            {
                if (nr == picValgt + 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "sideHvidBrikSyv";
                    FjerneBrik(nr);
                }
            }
            if (BrikPos[picValgt] == 3 && BoardSides.Contains(picValgt) && LigeRækker.Contains(picValgt))
            {
                if (nr == picValgt + 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "sideHvidBrikNi";
                    FjerneBrik(nr);
                }
            }
            //Flytte sort sidebrik til feltet den står på minus 9. 
            if (BrikPos[picValgt] == 4 && BoardSides.Contains(picValgt) && UligeRækker.Contains(picValgt))
            {
                if (nr == picValgt - 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "sideSortBrikSyv";
                    FjerneBrik(nr);
                }
            }
            if (BrikPos[picValgt] == 4 && BoardSides.Contains(picValgt) && LigeRækker.Contains(picValgt))
            {
                if (nr == picValgt - 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "sideSortBrikNi";
                    FjerneBrik(nr);
                }
            }
            //FLYTTE IKKE-SIDEBRIKKER
            //ULIGE RÆKKER
            //Flytte ulige hvid brik til feltet den står på plus 7 eller plus 9.
            if (BrikPos[picValgt] == 3 && UligeRækker.Contains(picValgt))
            {
                if (nr == picValgt + 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "uligeHvidBrikNi";
                    FjerneBrik(nr);
                }
                if (nr == picValgt + 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "uligeHvidBrikNi";
                    FjerneBrik(nr);
                }
            }
            //Flytte ulige sort brik til feltet den står på minus 7 eller minus 9.
            if (BrikPos[picValgt] == 4)
            {
                if (nr == picValgt - 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "uligeSortBrikSyv";
                    FjerneBrik(nr);
                }
                if (nr == picValgt - 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "uligeSortBrikNi";
                    FjerneBrik(nr);
                }
            } 
            //LIGE RÆKKER 
            //Flytte lige hvid brik til feltet den står på plus 7 eller plus 9.
            if (BrikPos[picValgt] == 3 && LigeRækker.Contains(picValgt))
            {
                if (nr == picValgt + 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "ligeHvidBrikSyv";
                    FjerneBrik(nr);
                }
                if (nr == picValgt + 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = whiteCircle;
                    BrikPos[nr] = 1;
                    Highlighted = false;
                    fjernBrik = "ligeHvidBrikNi";
                    FjerneBrik(nr);
                }
            }
            //Flytte lige sort brik til feltet den står på minus 7 eller minus 9.
            if (BrikPos[picValgt] == 4 && LigeRækker.Contains(picValgt))
            {
                if (nr == picValgt - 7 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "ligeSortBrikSyv";
                    FjerneBrik(nr);
                }
                if (nr == picValgt - 9 && BrikPos[nr] == 0)
                {
                    PictureBoxes[picValgt].Image = null;
                    BrikPos[picValgt] = 0;
                    PictureBoxes[nr].Image = blackCircle;
                    BrikPos[nr] = 2;
                    Highlighted = false;
                    fjernBrik = "ligeSortBrikNi";
                    FjerneBrik(nr);
                    
                }
            }
            txtBox3.Text = fjernBrik;
        }

        private void FjerneBrik(int nr)
        {
            //Side brikker
            if (fjernBrik == "sideHvidBrikSyv")
            {
                PictureBoxes[picValgt + 4].Image = null;
                BrikPos[picValgt +4] = 1;
                fjernBrik = "";
            }
            if (fjernBrik == "sideHvidBrikNi")
            {
                PictureBoxes[picValgt + 4].Image = null;
                BrikPos[picValgt + 4] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "sideSortBrikSyv")
            {
                PictureBoxes[picValgt - 4].Image = null;
                BrikPos[picValgt - 4] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "sideSortBrikNi")
            {
                PictureBoxes[picValgt - 4].Image = null;
                BrikPos[picValgt - 4] = 0;
                fjernBrik = "";
            }
            //Ulige brikker
            if (fjernBrik == "uligeHvidBrikSyv")
            {
                PictureBoxes[picValgt + 4].Image = null;
                BrikPos[picValgt + 4] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "uligeHvidBrikNi")
            {
                PictureBoxes[picValgt + 5].Image = null;
                BrikPos[picValgt + 5] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "uligeSortBrikSyv")
            {
                PictureBoxes[picValgt - 3].Image = null;
                BrikPos[picValgt - 3] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "uligeSortBrikNi")
            {
                PictureBoxes[picValgt - 4].Image = null;
                BrikPos[picValgt - 4] = 0;
                fjernBrik = "";
            }
            //Lige brikker
            if (fjernBrik == "ligeHvidBrikSyv")
            {
                PictureBoxes[picValgt + 3].Image = null;
                BrikPos[picValgt + 3] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "ligeHvidBrikNi")
            {
                PictureBoxes[picValgt + 4].Image = null;
                BrikPos[picValgt + 4] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "ligeSortBrikSyv")
            {
                PictureBoxes[picValgt - 3].Image = null;
                BrikPos[picValgt - 3] = 0;
                fjernBrik = "";
            }
            if (fjernBrik == "ligeSortBrikNi")
            {
                PictureBoxes[picValgt - 4].Image = null;
                BrikPos[picValgt - 4] = 0;
                fjernBrik = "";
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Pick(1);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Pick(2);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Pick(3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Pick(4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Pick(5);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Pick(6);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Pick(7);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Pick(8);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Pick(9);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Pick(10);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Pick(11);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Pick(12);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Pick(13);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Pick(14);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Pick(15);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Pick(16);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            Pick(17);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            Pick(18);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            Pick(19);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            Pick(20);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            Pick(21);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            Pick(22);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            Pick(23);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            Pick(24);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            Pick(25);
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            Pick(26);
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            Pick(27);
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            Pick(28);
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            Pick(29);
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            Pick(30);
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            Pick(31);
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            Pick(32);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtPicValgt.Text = picValgt.ToString();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            placerBrikker();
        }
    }
}
