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

        public int ProyectoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("ProyectoId")]
        public virtual List<ProyectosDetalle> Detalle { get; set; }
    }
}
