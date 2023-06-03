﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.EntityLayer.Concrete
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; } 
        public DateTime DateAdded { get; set; }

        public bool IsProfile { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
