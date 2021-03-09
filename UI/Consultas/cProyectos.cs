using Parcial2_ap1_2018_0619.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parcial2_ap1_2018_0619.BLL;
using PracticandoParcial.BLL;

namespace Parcial2_ap1_2018_0619.UI.Consultas
{
    public partial class cProyectos : Form
    {
        public cProyectos()
        {
            InitializeComponent();
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            var lista = new List<Proyectos>();

            if (!string.IsNullOrWhiteSpace(FiltrarTextBox.Text))
            {
                switch (FiltrocomboBox.SelectedIndex)
                {
                    case 0://Proyecto Id
                        lista = ProyectosBLL.GetList(r => r.TipoId == Conversiones.ToInt(FiltrarTextBox.Text));
                        break;
                    case 1://Descripcion
                        lista = ProyectosBLL.GetList(r => r.Descripcion.Contains(FiltrarTextBox.Text));
                        break;
                }
            }
            else
                lista = ProyectosBLL.GetList(r => true);

            ConsultasDataGridView.DataSource = null;
            ConsultasDataGridView.DataSource = lista;
        }
    }
}
