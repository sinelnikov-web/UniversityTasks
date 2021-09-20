using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            Graphics graphics = pictureBox1.CreateGraphics();
            int clientWidth = pictureBox1.Width;
            int clientHeight = pictureBox1.Height;
            int radius = 300;
            List<Brush> gradeBrushes = new List<Brush>()
            {
                Brushes.Black,
                Brushes.Red,
                Brushes.Blue,
                Brushes.Aqua
            };
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);

            Random rand = new Random();
            int[] gradesArray = new int[100];
            int[] studentsStats = new int[6];
            List<Student> studentsList = new List<Student>();

            for (int i = 0; i < 100; i++)
            {
                Student student = new Student(rand.Next(0, 101));
                studentsList.Add(student);
                gradesArray[i] = calculateGrade(student.Grade());
                studentsStats[gradesArray[i]]++;
            }

            for (int i = 2; i < 6; i++)
            {
                Bitmap picture = new Bitmap(64, 64);
                Graphics pictureGraphics = Graphics.FromImage(picture);
                Rectangle smallIcon = new Rectangle(0, 0, 64, 64);
                pictureGraphics.FillRectangle(gradeBrushes[i - 2], smallIcon);
                imageList.Images.Add(picture);

                string[] items = new string[] { "", i.ToString(), studentsStats[i].ToString() };
                ListViewItem item = new ListViewItem(items);
                item.ImageIndex = i - 2;
                
                listView1.Items.Add(item);
            }

            listView1.SmallImageList = imageList;

            Brush blackBrush = Brushes.Black;
            Brush redBrush = Brushes.Red;
            Brush blueBrush = Brushes.Blue;
            Brush aquaBrush = Brushes.Aqua;
            Rectangle rect = new Rectangle((clientWidth - radius) / 2, (clientHeight - radius) / 2, radius, radius);


            float firstPie = 360 * ((float)studentsStats[2] / 100);
            float secondPie = 360 * ((float)studentsStats[3] / 100);
            float thirdPie = 360 * ((float)studentsStats[4] / 100);
            float fourthPie = 360 * ((float)studentsStats[5] / 100);

            graphics.FillPie(blackBrush, rect, 0, firstPie);
            graphics.FillPie(redBrush, rect, firstPie, secondPie);
            graphics.FillPie(blueBrush, rect, secondPie + firstPie, thirdPie);
            graphics.FillPie(aquaBrush, rect, thirdPie + secondPie + firstPie, fourthPie);
        }

        public int calculateGrade(int grade)
        {
            if (grade < 30)
            {
                return 2;
            } else if (grade <= 50)
            {
                return 3;
            } else if (grade <= 80)
            {
                return 4;
            } else
            {
                return 5;
            }
        }
    }
}
