﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AparejosEntities : DbContext
    {
        public AparejosEntities()
            : base("name=AparejosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<EstadoFabricacionPedido> EstadoFabricacionPedido { get; set; }
        public virtual DbSet<EstadoPagoPedido> EstadoPagoPedido { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<TipoCarro> TipoCarro { get; set; }
        public virtual DbSet<TipoPedido> TipoPedido { get; set; }
    }
}
