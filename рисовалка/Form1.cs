using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using рисовалка.Properties;

namespace рисовалка
{
    public partial class MainForm : Form
    {
        public static Color Color { get; set; }
        public static int Width { get; set; }
        public static Tools CurrentTool { get; set; }
        public static bool Filled { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Color = Color.Black;
            CurrentTool = Tools.Pen;
            Filled = false;
            Width = 3;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }
        
        private void размерХолстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new CanvasSizeForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ActiveMdiChild.Width = 100;
                ActiveMdiChild.Height = 100;
            }

        }

        private void новыйToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var frm = new DocumentForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void другойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                Color = cd.Color;
        }

        private void красныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Red;
        }

        private void синийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Blue;
        }

        private void зеленыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Green;
        }

        private void рисунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            размерХолстаToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void BrushSize_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BrushSize.Text, out int result))
                Width = result;
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).Save();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((DocumentForm)ActiveMdiChild).SaveAs();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Все файлы ()*.*|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dlg.FileName);
                var frm = new DocumentForm();
                frm.MdiParent = this;
                frm.SetImage(image, dlg.FileName);
                frm.Show();
            }
        }

    private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            сохранитьКакToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            сохранитьToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void каскадомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void слеваНаправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void сверхуВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void упорядочитьЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Line;
            Bitmap bmp = new Bitmap(Resources.Line);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        private void CircletoolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Circle;
            Bitmap bmp = new Bitmap(Resources.Circle);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        private void ErasertoolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Eraser;
            Bitmap bmp = new Bitmap(Resources.Eraser);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        /// <summary>
        /// Перо
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PentoolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Pen;
            this.Cursor = DefaultCursor;
        }

        /// <summary>
        /// Включает/отключает заливку фигуры при рисовании
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Filled = !(Filled);
        }

        /// <summary>
        /// Вставка текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TexttoolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Text;
            Bitmap bmp = new Bitmap(Resources.Text);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        /// <summary>
        /// Заливка замкнутой фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuckettoolStripButton_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Bucket;
            Bitmap bmp = new Bitmap(Resources.Bucket);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        /// <summary>
        /// Сердце
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CurrentTool = Tools.Heart;
            Bitmap bmp = new Bitmap(Resources.heart);
            this.Cursor = new Cursor(bmp.GetHicon());
        }

        /// <summary>
        /// Ширина линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrushSize_Click(object sender, EventArgs e)
        {

        }
    }
}
