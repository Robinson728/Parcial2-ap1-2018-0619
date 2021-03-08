using Parcial2_ap1_2018_0619.UI.Consultas;
using Parcial2_ap1_2018_0619.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2_ap1_2018_0619.UI
{
    public partial class MainForms : Form
    {
        public MainForms()
        {
            InitializeComponent();
            this.rProyectostoolStripMenuItem.Click += new EventHandler(this.rProyectostoolStripMenuItem_ItemClicked);

            this.cProyectostoolStripMenuItem.Click += new EventHandler(this.cProyectostoolStripMenuItem_ItemClicked);
        }

        public void rProyectostoolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            var proyecto = new rProyectos();
            proyecto.MdiParent = this;
            proyecto.Show();
        }

        public void cProyectostoolStripMenuItem_ItemClicked(object sender, EventArgs e)
        {
            var proyecto = new cProyectos();
            proyecto.MdiParent = this;
            proyecto.Show();
        }
    }
}
