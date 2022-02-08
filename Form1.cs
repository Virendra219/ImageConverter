using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreyToColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            originalImgTab.Controls.Add(originalImage);
            originalImage.SizeMode = PictureBoxSizeMode.Zoom;
            originalImage.Dock = DockStyle.Fill;

            editedImgTab.Controls.Add(editedImage);
            editedImage.SizeMode = PictureBoxSizeMode.Zoom;
            editedImage.Dock = DockStyle.Fill;
        }

        PictureBox originalImage = new(), editedImage = new();
        Bitmap BMOrg, BMEdit;

        private void Show_Button_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage.Load(openFileDialog1.FileName);
                tabControl1.SelectedTab = originalImgTab;
            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            BMOrg = new(originalImage.Image);
            int w = BMOrg.Width, h = BMOrg.Height;
            BMEdit = new(w, h);

            for (int i=0; i<h; i++)
            {
                for (int j=0; j<w; j++)
                {
                    BMEdit.SetPixel(j, i, BMOrg.GetPixel(j, i)); //customise here
                }
            }

            editedImage.Image = BMEdit;
            tabControl1.SelectedTab = editedImgTab;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
            string fileAddress;
            saveFileDialog1.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileAddress = saveFileDialog1.FileName;
                    BMEdit.Save(fileAddress);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
