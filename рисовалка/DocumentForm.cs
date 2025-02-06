using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace рисовалка
{
    public partial class DocumentForm : Form
    {
        private int x, y;
        private Bitmap bmpTemp;
        private Bitmap bitmap;
        private bool saved = false;
        private string path;
        private bool changed = false;
        private float scale = 1.0f;
        public DocumentForm()
        {
            InitializeComponent();
            bitmap = new Bitmap(1920, 1080);
            bmpTemp = bitmap;
        }

        public Bitmap Bitmap { get; }

        private void DocumentForm_MouseDown_1(object sender, MouseEventArgs e)
        { 
            x = e.X;
            y = e.Y;
            changed = true;

            switch (MainForm.CurrentTool)
            {
                case Tools.Text:
                    TextBox textBox = new TextBox
                    {
                        Location = new Point(e.X, e.Y),
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font(Font.FontFamily, MainForm.Width),
                        ForeColor = MainForm.Color,
                        Width = 100
                    };

                    textBox.LostFocus += (s, ev) => { PlaceText(textBox, e.X, e.Y); };
                    textBox.KeyDown += (s, ev) =>
                    {
                        if (ev.KeyCode == Keys.Enter)
                        {
                            PlaceText(textBox, e.X, e.Y);
                        }
                    };

                    Controls.Add(textBox);
                    textBox.Focus();
                    break;

                case Tools.Bucket:
                    Bucket(new Point(e.X, e.Y), MainForm.Color);
                    break;
            }

        }

        private void DocumentForm_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var pen = new Pen(MainForm.Color, MainForm.Width);

                SolidBrush brush = new SolidBrush(pen.Color);
                switch (MainForm.CurrentTool)
                {
                    case Tools.Pen:
                        Graphics g = Graphics.FromImage(bitmap);
                        if (MainForm.Width < 15)
                        {
                            g.DrawLine(pen, x, y, e.X, e.Y);
                            x = e.X;
                            y = e.Y;
                        }
                        else
                        {
                            g.FillEllipse(new SolidBrush(pen.Color), x, y, MainForm.Width, MainForm.Width);
                            x = e.X;
                            y = e.Y;
                        }
                        break;

                    case Tools.Circle:
                        bmpTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bmpTemp);
                        g.DrawEllipse(pen, new Rectangle(x, y, e.X - x, e.Y - y));

                        if (MainForm.Filled)
                        {
                            g.FillEllipse(new SolidBrush(pen.Color), new Rectangle(x, y, e.X - x, e.Y - y));
                        }

                        Invalidate();
                        break;

                    case Tools.Line:
                        bmpTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bmpTemp);
                        g.DrawLine(pen, x, y, e.X, e.Y);
                        break;

                    case Tools.Eraser:
                        int radius = MainForm.Width / 2;
                        g = Graphics.FromImage(bmpTemp);
                        g.DrawEllipse(new Pen(BackColor, MainForm.Width), e.X, e.Y, radius, radius);
                        g.FillEllipse(new SolidBrush(BackColor), e.X, e.Y, radius, radius);
                        x = e.X;
                        y = e.Y;
                        break;

                    case Tools.Text:
                        break;

                    case Tools.Bucket:
                        break;

                    case Tools.Heart:
                        bmpTemp = (Bitmap)bitmap.Clone();
                        g = Graphics.FromImage(bmpTemp);
                        Rectangle rec = new Rectangle(x, y, e.X - x, e.Y - y);
                        DrawHeart(g, pen, rec, MainForm.Filled);

                        Invalidate();
                        break;
                }
                
                Invalidate();
            }
        }

        private void DocumentForm_SizeChanged(object sender, EventArgs e)
        {
            //bitmap = ResizeImage(bitmap, new Size(this.Width, this.Height));
            //bmpTemp = bitmap;
            //Invalidate();
        }

        public void Save()
        {
            if (!saved)
                {
                    this.SaveAs();
                }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };
                bitmap.Save(path, ff[dlg.FilterIndex - 1]);
            }
            saved = true;
            changed = false;
        }

        public void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp| Файлы JPEG (*.jpg)|*.jpg";
            ImageFormat[] ff = { ImageFormat.Bmp, ImageFormat.Jpeg };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(dlg.FileName, ff[dlg.FilterIndex - 1]);
            }
            path = Path.GetFullPath(dlg.FileName);
            saved = true;
            changed = false;
        }

        public void SetImage(Image image, string path)
        {
            bitmap = (Bitmap)image;
            bmpTemp = bitmap;

            saved = true;
            this.path = path;
            saved = true;
            changed = false;
            Invalidate();
        }

        private void DocumentForm_MouseUp(object sender, MouseEventArgs e)
        {
            switch (MainForm.CurrentTool)
            {
                case Tools.Pen:
                    {
                        break;
                    }
                case Tools.Line:
                    {
                        bitmap = bmpTemp;
                        break;
                    }
                case Tools.Circle:
                    {
                        bitmap = bmpTemp;
                        break;
                    }
                case Tools.Eraser:
                    {
                        break;
                    }
                case Tools.Text:
                    {
                        bitmap = bmpTemp;
                        break;
                    }
                case Tools.Heart:
                    {
                        bitmap = bmpTemp;
                        break;
                    }
            }
            Invalidate();
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed)
            {
                QuitDialog dlg = new QuitDialog();
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.ScaleTransform(scale, scale);
            if (bmpTemp != null)
            {
                e.Graphics.DrawImage(bmpTemp, 0, 0);
            }
            else
            {
                e.Graphics.DrawImage(bitmap, 0, 0);
            }
        }
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }

        private void Plusbutton_Click(object sender, EventArgs e)
        {
            if (scale * 1.1 < 8)
            {
                scale *= 1.1f;
                Invalidate();
            }
        }

        private void Minusbutton_Click(object sender, EventArgs e)
        {
            if (scale / 1.1 > 0.1)
            {
                scale /= 1.1f;
                Invalidate();
            }
        }

        private void Bucket(Point point, Color color)
        {
            Color targetColor = bitmap.GetPixel(point.X, point.Y);
            if (targetColor == color) return;

            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(point);

            while (queue.Count > 0)
            {
                Point currentPoint = queue.Dequeue();
                int x = currentPoint.X;
                int y = currentPoint.Y;

                if (x < 0 || x >= bitmap.Width || y < 0 || y >= bitmap.Height) continue;
                if (bitmap.GetPixel(x, y) != targetColor) continue;

                bitmap.SetPixel(x, y, color);

                queue.Enqueue(new Point(x - 1, y));
                queue.Enqueue(new Point(x + 1, y));
                queue.Enqueue(new Point(x, y - 1));
                queue.Enqueue(new Point(x, y + 1));
            }
        }

        private void PlaceText(TextBox textBox, int x, int y)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawString(textBox.Text, textBox.Font, new SolidBrush(textBox.ForeColor), x, y);
                }
                Invalidate();
            }

            Controls.Remove(textBox);
            textBox.Dispose();
        }

        private void DrawHeart(Graphics g, Pen pen, Rectangle rect, bool filled = false)
        {
            int x = Math.Min(rect.Left, rect.Right);
            int y = Math.Min(rect.Top, rect.Bottom);
            int w = Math.Abs(rect.Width);
            int h = Math.Abs(rect.Height);

            if (!filled)
            {
                if (w > 1 && h > 1)
                {
                    g.DrawArc(pen, x, y, w / 2, h / 2, 180, 180);
                    g.DrawArc(pen, x + w / 2, y, w / 2, h / 2, 180, 180);

                    g.DrawLine(pen, x, y + h / 4, x + w / 2, y + h);
                    g.DrawLine(pen, x + w, y + h / 4, x + w / 2, y + h);
                }
            }
            else
            {
                if (w > 1 && h > 1)
                {
                    SolidBrush brush = new SolidBrush(pen.Color);
                    Rectangle leftPie = new Rectangle(x, y, w / 2, h / 2);
                    Rectangle rightPie = new Rectangle(x + w / 2, y, w / 2, h / 2);

                    g.FillPie(brush, leftPie, 180, 180);
                    g.FillPie(brush, rightPie, 180, 180);

                    Point[] trianglePoints =
                    {
                        new Point(x, y + h / 4),
                        new Point(x + w, y + h / 4),
                        new Point(x + w / 2, y + h)
                    };

                    g.FillPolygon(brush, trianglePoints);
                }
            }
        }
    }

}
