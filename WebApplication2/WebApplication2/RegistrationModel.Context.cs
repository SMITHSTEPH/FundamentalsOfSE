﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApplication2
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class RegistrationEntities1 : DbContext
{
    public RegistrationEntities1()
        : base("name=RegistrationEntities1")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<memberTableV2> memberTableV2 { get; set; }

    public virtual DbSet<administrationV2> administrationV2 { get; set; }

    public virtual DbSet<leaderTableV2> leaderTableV2 { get; set; }

    public virtual DbSet<JunctionTableProjectAndAccount> JunctionTableProjectAndAccounts { get; set; }

    public virtual DbSet<MultipleChoiceTable> MultipleChoiceTables { get; set; }

    public virtual DbSet<WaterfallTable2> WaterfallTable2 { get; set; }

    public virtual DbSet<COTSTable> COTSTables { get; set; }

    public virtual DbSet<RADTable> RADTables { get; set; }

    public virtual DbSet<WaterfallIterationTable> WaterfallIterationTables { get; set; }

    public virtual DbSet<Questions2Table> Questions2Table { get; set; }

}

}

