﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCamDo.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CamDoEntities : DbContext
    {
        public CamDoEntities()
            : base("name=CamDoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Extend> Extends { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserController> UserControllers { get; set; }
        public virtual DbSet<UserModule> UserModules { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    }
}
