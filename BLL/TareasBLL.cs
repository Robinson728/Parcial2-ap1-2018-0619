using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial2_ap1_2018_0619.Entidades;
using Parcial2_ap1_2018_0619.DAL;
using System.Linq.Expressions;

namespace Parcial2_ap1_2018_0619.BLL
{
    public class TareasBLL
    {
        public static Tareas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Tareas tareas;

            try
            {
                tareas = contexto.Tareas.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return tareas;
        }

        public static List<Tareas> GetList(Expression<Func<Tareas, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Tareas> lista = new List<Tareas>();

            try
            {
                lista = contexto.Tareas.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }

        public static List<Tareas> GetTareas()
        {
            Contexto contexto = new Contexto();
            List<Tareas> lista = new List<Tareas>();

            try
            {
                lista = contexto.Tareas.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
