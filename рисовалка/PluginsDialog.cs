using PluginInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace рисовалка
{
    public partial class PluginsDialog : Form
    {
        public PluginsDialog(Dictionary<string, IPlugin> plugins)
        {
            InitializeComponent();

            listViewPlugins.View = View.Details;
            listViewPlugins.FullRowSelect = true;
            listViewPlugins.Columns.Add("Название", 200);
            listViewPlugins.Columns.Add("Автор", 150);

            foreach (var plugin in plugins)
            {
                var item = new ListViewItem(plugin.Value.Name);
                item.SubItems.Add(plugin.Value.Author);
                listViewPlugins.Items.Add(item);
            }
        }
    }
}
