using Microsoft.EntityFrameworkCore;
using Parcial2_ap1_2018_0619.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parcial2_ap1_2018_0619.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA\GestionProyectos.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tareas>().HasData(
                    new Tareas() { TareaId = 1, TipoTarea = "Análisis"},
                    new Tareas() { TareaId = 2, TipoTarea = "Diseño"},
                    new Tareas() { TareaId = 3, TipoTarea = "Programación"},
                    new Tareas() { TareaId = 4, TipoTarea = "Prueba"}
                );
        }
    }
}
