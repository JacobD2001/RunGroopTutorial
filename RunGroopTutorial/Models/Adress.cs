﻿using System.ComponentModel.DataAnnotations;

namespace RunGroopTutorial.Models
{
    public class Adress
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
