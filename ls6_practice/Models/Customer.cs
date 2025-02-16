﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ls6_practice.Models
{
    public class Customer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name is required ... min: 2, max: 20")]
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty ;
        [JsonIgnore] //Ignor for error serialization
        //Navigation property
        public ICollection<Order>? Orders { get; set; }
    }
}
