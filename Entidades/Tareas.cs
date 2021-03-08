using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2_ap1_2018_0619.Entidades
{
    public class Tareas
    {
        [Key]

        public int TareaId { get; set; }
        public string TipoTarea { get; set; }

        [ForeignKey("TareaId")]
        public virtual Tareas tareas { get; set; }
    }
}
