using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Parcial2_ap1_2018_0619.Entidades;
using Parcial2_ap1_2018_0619.BLL;

namespace Parcial2_ap1_2018_0619.UI.Registros
{
    public partial class rProyectos : Form
    {
        public List<ProyectosDetalle> Detalle { get; set; }
        public rProyectos()
        {
            InitializeComponent();
            this.Detalle = new List<ProyectosDetalle>();
        }

        private void CargarGrid()
        {
            DetallesdataGridView.DataSource = null;
            DetallesdataGridView.DataSource = this.Detalle;
        }

        private void Limpiar()
        {
            ErrorProvider.Clear();
            IdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            DescripciontextBox.Clear();

            this.Detalle = new List<ProyectosDetalle>();
            CargarGrid();
        }

        private void LlenaCampo(Proyectos proyectos)
        {
            IdnumericUpDown.Value = proyectos.TipoId;
            FechadateTimePicker.Value = proyectos.Fecha;
            DescripciontextBox.Text = proyectos.Descripcion;
            proyectos.Tiempo = Convert.ToInt32(TotaltextBox.Text);
            this.Detalle = proyectos.Detalle;
            CargarGrid();
        }

        private Proyectos LlenaClase()
        {
            Proyectos proyectos = new Proyectos();

            proyectos.TipoId = (int)IdnumericUpDown.Value;
            proyectos.Fecha = FechadateTimePicker.Value;
            proyectos.Descripcion = DescripciontextBox.Text;
            proyectos.Tiempo = Convert.ToInt32(TotaltextBox.Text);
            proyectos.Detalle = this.Detalle;
            CargarGrid();

            return proyectos;
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                ErrorProvider.SetError(DescripciontextBox, "Este campo no puede estar vacío");
                DescripciontextBox.Focus();
                paso = false;
            }
            if(ProyectosBLL.ExisteProyecto(DescripciontextBox.Text, (int)IdnumericUpDown.Value))
            {
                ErrorProvider.SetError(DescripciontextBox, "Este Proyecto ya existe");
                DescripciontextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(TareacomboBox.Text))
            {
                ErrorProvider.SetError(TareacomboBox, "Debe seleccionar una Tarea");
                TareacomboBox.Focus();
                paso = false;
            }
            if (this.Detalle.Count == 0)
            {
                ErrorProvider.SetError(DetallesdataGridView, "Debe agregar una Tarea");
                DetallesdataGridView.Focus();
                paso = false;
            }

            return paso;
        }

        private bool ValidarDetalle()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(RequerimientotextBox.Text))
            {
                ErrorProvider.SetError(RequerimientotextBox, "Este campo no puede estar vacío");
                RequerimientotextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(TiempotextBox.Text))
            {
                ErrorProvider.SetError(TiempotextBox, "Este campo no puede estar vacío");
                TiempotextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            Proyectos proyectos = new Proyectos(); 
            int id = 0;
            int.TryParse(IdnumericUpDown.Text, out id);

            proyectos = ProyectosBLL.Buscar(id);

            if (proyectos != null)
                LlenaCampo(proyectos);
            else
                MessageBox.Show("Transacción Fallida", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            if (!ValidarDetalle())
                return;

            if (DetallesdataGridView.DataSource != null)
                this.Detalle = (List<ProyectosDetalle>)DetallesdataGridView.DataSource;

            Tareas tareas = TareasBLL.Buscar(Convert.ToInt32(TareacomboBox.Text));

            this.Detalle.Add(new ProyectosDetalle()
            {
                TipoId = tareas.TareaId,
                TipoTarea = tareas.TipoTarea,
                Requerimiento=RequerimientotextBox.Text,
                Tiempo=Convert.ToInt32(TiempotextBox.Text)
            });
            
            int total = Convert.ToInt32(TotaltextBox.Text);
            int tiempo = Convert.ToInt32(TiempotextBox.Text);
            total += tiempo;
            TotaltextBox.Text = Convert.ToString(total);

            CargarGrid();
            TareacomboBox.Focus();
            RequerimientotextBox.Clear();
            TiempotextBox.Clear();
            ErrorProvider.Clear();
        }

        private void RemoverButton_Click(object sender, EventArgs e)
        {
            if((DetallesdataGridView.Rows.Count > 0) && (DetallesdataGridView.CurrentRow != null))
            {
                int total = Convert.ToInt32(TotaltextBox.Text);
                string tiempo = DetallesdataGridView.CurrentRow.Cells[3].Value.ToString();
                total -= Convert.ToInt32(tiempo);
                TotaltextBox.Text = Convert.ToString(total);

                Detalle.RemoveAt(DetallesdataGridView.CurrentRow.Index);
                CargarGrid();
            }
            else
            {
                ErrorProvider.SetError(DetallesdataGridView, "No hay filas para Remover");
                DetallesdataGridView.Focus();
            }
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Proyectos proyectos;

            if (!Validar())
                return;

            proyectos = LlenaClase();

            var paso = ProyectosBLL.Guardar(proyectos);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Transacción Fallida", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (ProyectosBLL.ELiminar(id))
                MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                ErrorProvider.SetError(IdnumericUpDown, "Este Id no existe en la base de datos");
        }

        private void rProyectos_Load(object sender, EventArgs e)
        {
            TareacomboBox.DataSource = TareasBLL.GetTareas();
            TareacomboBox.DisplayMember = "TareaId";
            TareacomboBox.ValueMember = "TareaId";
        }
    }
}
