namespace WebApplication2.Models
{
    using DHTMLX.Scheduler;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DHXJson(Alias = "id")]
        public int Id { get; set; }

        [DHXJson(Alias = "text")]
        [Column("Description")]
        [StringLength(10)]
        public string Description { get; set; }

        [DHXJson(Alias = "start_date")]
        public DateTime? StartDate { get; set; }


        [DHXJson(Alias = "end_date")]
        public DateTime? EndDate { get; set; }

        public int? projectNumber { get; set; }
    }
}
