﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncfusionVideosPortal.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class devsyncdbEntities : DbContext
    {
        public devsyncdbEntities()
            : base("name=devsyncdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Hackathon_Control> Hackathon_Control { get; set; }
        public virtual DbSet<Hackathon_Platform> Hackathon_Platform { get; set; }
        public virtual DbSet<Hackathon_Videos> Hackathon_Videos { get; set; }
        public virtual DbSet<Hackathon_Comments> Hackathon_Comments { get; set; }
    }
}
