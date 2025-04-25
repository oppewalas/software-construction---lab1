using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
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
            FindPlugins();
            CreatePluginsMenu();
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
        Dictionary<string, IPlugin> plugins = new Dictionary<string, IPlugin>();
        Dictionary<string, bool> pluginStates = new Dictionary<string, bool>();

        void FindPlugins()
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins.config");
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(configPath))
            {
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                XmlElement root = doc.CreateElement("Plugins");
                doc.AppendChild(xmlDeclaration);
                doc.AppendChild(root);
            }
            else
            {
                doc.Load(configPath);
            }

            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode node in rootNode.SelectNodes("Plugin"))
            {
                string name = node.Attributes["name"].Value;
                bool enabled = bool.Parse(node.Attributes["enabled"].Value);
                pluginStates[name] = enabled;
            }

            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");

            bool configModified = false;

            foreach (string file in files)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginInterface.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

                            if (!pluginStates.ContainsKey(plugin.Name))
                            {
                                pluginStates[plugin.Name] = true;
                                XmlElement pluginNode = doc.CreateElement("Plugin");
                                pluginNode.SetAttribute("name", plugin.Name);
                                pluginNode.SetAttribute("enabled", "true");
                                rootNode.AppendChild(pluginNode);
                                configModified = true;
                            }

                            if (pluginStates[plugin.Name])
                            {
                                plugins[plugin.Name] = plugin;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
            }

            if (configModified)
            {
                doc.Save(configPath);
            }
        }

        private void CreatePluginsMenu()
        {
            foreach (var p in plugins)
            {
                var item = фильтрыToolStripMenuItem.DropDownItems.Add(p.Value.Name);
                item.Click += OnPluginClick;
            }
        }

        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            plugin.Transform((Bitmap)pictureBox.Image);
            pictureBox.Refresh();

        }

        private void фильтрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void плагиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginsDialog dialog = new PluginsDialog(plugins);
            dialog.ShowDialog();
        }
    }
}
