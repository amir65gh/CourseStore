using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseStore.Core.Domain
{
    public class Customer : BaseEntity
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The maximum length is 100 character")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "The maximum length is 100 character")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "The email format is not valid.")]
        public string Email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CustomerStatus CustomerStatus { get; set; }
        public DateTime? StatusExpirationDate { get; set; }
        public decimal MoneySpent { get; set; }
        public List<PurchasedCourse> PuchasedCourses { get; set; }
    }
}
