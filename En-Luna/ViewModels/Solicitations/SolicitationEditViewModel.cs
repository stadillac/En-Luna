﻿using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace En_Luna.ViewModels
{
    public class SolicitationEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public int SolicitorId { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string County { get; set; } = string.Empty;
        
        [DisplayName("Shared Drive Url")]
        public string SharedDriveUrl { get; set; } = string.Empty;

        [Required]
        [DisplayName("Estimated End Date")]
        [DataType(DataType.Date)]
        public DateTime EstimatedEndDate { get; set; } = DateTime.Now;

        [DataType(DataType.Time)]
        [DisplayName("Team Meeting Time")]
        public DateTime TeamMeetingTime { get; set; } = DateTime.Now;

        [DisplayName("Time Zone")]
        [Required]
        public string? TimeZone { get; set; }
        public SelectList? TimeZones { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Complete")]
        public bool IsComplete { get; set; }

        [DisplayName("Approved")]
        public bool IsApproved { get; set; }

        [DisplayName("Cancelled")]
        public bool IsCancelled { get; set; }

        [Required]
        [DisplayName("State")]
        public int StateId { get; set; }

        public SelectList? States { get; set; }

        [BindNever]
        public SolicitationRoleEditViewModel SolicitationRole { get; set; } = new();

        public List<SolicitationRoleViewModel> SolicitationRoles { get; set; } = new List<SolicitationRoleViewModel>();

        public SolicitationDeadlineEditViewModel SolicitationDeadline { get; set; } = new();
        public bool PendingApproval { get; internal set; }
    }

    public class Test
    {
        [DisplayName("Lump Sum")]
        public double LumpSum { get; set; }

        [DisplayName("Hourly Rate")]
        public double HourlyRate { get; set; }

        [DisplayName("Sign-on Bonus")]
        public double SignBonus { get; set; }

        [DisplayName("Workload (Hours per Week)")]
        public double Workload { get; set; }

        public string Description { get; set; } = string.Empty;

        [DisplayName("Deliverable Deadline")]
        [DataType(DataType.Date)]
        public DateTime DeliverableDeadline { get; set; }

        [DisplayName("Project Deliverable")]
        public int ProjectDeliverableId { get; set; }
        public SelectList? ProjectDeliverables { get; set; }
    }
}
