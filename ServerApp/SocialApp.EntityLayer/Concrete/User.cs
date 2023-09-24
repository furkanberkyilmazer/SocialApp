using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.EntityLayer.Concrete
{
    public class User:IdentityUser<int> //<int> ekleme sebebimiz normalde ıd bilgisini guid tutuyor biz int çevirdik.
    {
        public string Name { get; set; }

        public string Password { get; set; }
        public string Gender { get; set; }
    
        //porstgre de eğer nullable koymazsan hepsini zorunlu kabul ediyor.
        public DateTime? DateOfBirth { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastActive { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Introduction { get; set; }

        public string? Hobbies { get; set; }
        public List<Image> Images { get; set; }

        public ICollection<UserToUser>? Followings { get; set; }
        public ICollection<UserToUser>? Followers { get; set; }

        public ICollection<Message>? MessagesSent { get; set; }

        public ICollection<Message>? MessagesReceived { get; set; }


    }
}
