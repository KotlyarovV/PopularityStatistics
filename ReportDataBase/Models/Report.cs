using System;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace ReportDataBase.Models
{
    public class Report
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Display(Name = "Theme")]
        public string Theme { get; set; }
        [Display(Name = "AgeReport")]
        public string AgeReport { get; set; }
        [Display(Name = "SexReport")]
        public string SexReport { get; set; }
        [Display(Name = "FirstUserName")]
        public string FirstUserName { get; set; }
        [Display(Name = "DateTime")]
        public DateTime DateTime { get; set; }
        [Display(Name = "IsFailed")]
        public bool IsFailed { get; set; }
        [Display(Name = "ErrorMessage")]
        public string ErrorMessage { get; set; }
        [Display(Name = "WayToFile")]
        public string WayToFile { get; set; }
    }
}
