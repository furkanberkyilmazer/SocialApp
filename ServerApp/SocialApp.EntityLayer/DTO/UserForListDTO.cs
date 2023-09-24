using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.EntityLayer.DTO
{
    public class UserForListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }
        public string Gender { get; set; }

        //porstgre de eğer nullable koymazsan hepsini zorunlu kabul ediyor.
        public int Age { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastActive { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public ImagesForDetailsDTO Image { get; set; }
    }
}
