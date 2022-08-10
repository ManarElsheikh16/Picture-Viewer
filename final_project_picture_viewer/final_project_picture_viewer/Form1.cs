using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project_picture_viewer
{
    public partial class Form1 : Form
    {
        List<string> s = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Title = "you can select one or multiple pictures";
                file.Filter = ".JPG|*.jpg|PNG|*.png|GIF|*.gif|JPEG|*.jpeg";
                file.Multiselect = true;
                file.CheckFileExists = true;
                DialogResult dr = file.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string[] files = file.FileNames;

                    foreach (string img in files)
                    {
                        listBox1.Items.Add(Path.GetFileName(img));
                        s.Add(img);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void singleModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                 panel2.Visible = true;
                 panel3.Visible = false;
                 panel4.Visible = false;
                if (listBox1.SelectedItems.Count > 1)
                {
                    MessageBox.Show("you must select one picture only");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(s[listBox1.SelectedIndex]);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }

            }
            catch
            {
                MessageBox.Show("you must select one picture only");
            }
        }

        private void slideShowModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                panel3.Visible = true;
                panel2.Visible = false;
                panel4.Visible = false;
                timer1.Enabled = true;
                timer1.Interval = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int imagenum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
        
                toolStripStatusLabel1.Text = listBox1.Items[imagenum].ToString();
                pictureBox2.Image = new Bitmap(s[imagenum]);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                if (imagenum == listBox1.Items.Count - 1)
                {
                    imagenum = 0;
                }
                else
                {
                    imagenum++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void multiPicModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                panel4.Visible = true;
                panel2.Visible=false;
                panel3.Visible=false;
                panel4.Controls.Clear();
                int x = 10, y = 10, maxHeight = -1;
                foreach (int img in listBox1.SelectedIndices)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Image = Image.FromFile(s[img].ToString());
                    pic.Location = new Point(x, y);
                    x += pic.Width + 10;  //space between each picture
                    pic.Height = 100;
                    pic.Width = 100;
                    maxHeight = Math.Max(pic.Height, maxHeight);
                    if (x > this.panel4.ClientSize.Width - 100)
                    {
                        x = 10;
                        y += maxHeight + 10;
                    }
                    this.panel4.Controls.Add(pic);
                }
            }
            catch
            {
                MessageBox.Show("you must select one picture or more");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
