﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SocialApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.DataAccessLayer.Concrete
{
    public class SocialContext : IdentityDbContext<User,Role,int>
    {
        //dockerda ınstance ı oluştur ama dbeaverla bağlanmadan migration yap daha sonra database ismiyle bağlan.

        public SocialContext(DbContextOptions<SocialContext> options) : base(options)
        {

        }    
        public DbSet<Image> Images { get; set; }
        public DbSet<UserToUser> UserToUser { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //bunları yapma sebebimiz userın içinde messages danda usertouserdanda iki list var ilişkileri tanımladık.

            builder.Entity<UserToUser>().HasKey(k => new { k.UserId, k.FollowerId });
            builder.Entity<UserToUser>().HasOne(l => l.User).WithMany(a => a.Followers).HasForeignKey(l => l.FollowerId);
            builder.Entity<UserToUser>().HasOne(l => l.Follower).WithMany(a => a.Followings).HasForeignKey(l => l.UserId);

            builder.Entity<Message>().HasOne(i => i.Sender).WithMany(i => i.MessagesSent).HasForeignKey(i => i.SenderId);
            builder.Entity<Message>().HasOne(i=>i.Recipient).WithMany(i=>i.MessagesReceived).HasForeignKey(i => i.RecipientId);
        }
    }
}
