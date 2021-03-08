using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial2_ap1_2018_0619.Entidades;
using Parcial2_ap1_2018_0619.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Parcial2_ap1_2018_0619.BLL
{
    public class ProyectosBLL
    {
        public static bool Guardar(Proyectos proyectos)
        {
            if (!Existe(proyectos.ProyectoId))
                return Insertar(proyectos);
            else
                return Modificar(proyectos);
        }

        private static bool Insertar(Proyectos proyectos)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Proyectos.Add(proyectos);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool Modificar(Proyectos proyectos)
        {
            Contexto contexto = new Contexto();
            bool paso = true;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM ProyectosDetalle where ProyectoId={proyectos.ProyectoId}");
                foreach(var anterior in proyectos.Detalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(proyectos).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        private static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Proyectos.Any(e => e.ProyectoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static bool ELiminar(int id)
        {
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                var proyectos = contexto.Proyectos.Find(id);
                if (proyectos != null)
                {
                    contexto.Entry(proyectos).State = EntityState.Deleted;
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public static Proyectos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Proyectos proyectos;
            try
            {
                proyectos = contexto.Proyectos.Include(e => e.Detalle).Where(r => r.ProyectoId == id).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return proyectos;
        }

        public static bool ExisteProyecto(string pronombre, int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Proyectos.Any(e => e.Descripcion == pronombre);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            if (encontrado)
            {
                Proyectos proyectos = Buscar(id);

                if (proyectos != null)
                    return true;

                if (proyectos.Descripcion == pronombre)
                    encontrado = false;
            }

            return encontrado;
        }

        public static List<Proyectos> GetList(Expression<Func<Proyectos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Proyectos> lista = new List<Proyectos>();

            try
            {
                lista = contexto.Proyectos.Where(criterio).ToList();
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

        public static List<Proyectos> GetProyectos()
        {
            Contexto contexto = new Contexto();
            List<Proyectos> lista = new List<Proyectos>();

            try
            {
                lista = contexto.Proyectos.ToList();
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
