﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BatchPay.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BatchPayEntities1 : DbContext
    {
        public BatchPayEntities1()
            : base("name=BatchPayEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ConfigCount> ConfigCount { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
    }
}
