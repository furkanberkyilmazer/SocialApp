using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.EntityLayer.DTO
{
    public class ImagesForDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public bool IsProfile { get; set; }

    }
}
