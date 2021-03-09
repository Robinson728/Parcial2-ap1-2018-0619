using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_ap1_2018_0619.Entidades
{
    public class Proyectos
    {
        [Key]

        public int TipoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }

        [ForeignKey("TipoId")]
        public virtual List<ProyectosDetalle> Detalle { get; set; }

        public Proyectos()
        {
            TipoId = 0;
            Fecha = DateTime.Now;
            Descripcion = string.Empty;
            Tiempo = 0;

            Detalle = new List<ProyectosDetalle>();
        }
    }
}
