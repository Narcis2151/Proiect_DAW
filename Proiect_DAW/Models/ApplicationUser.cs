﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_DAW.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Profile? Profile { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
        public virtual ICollection<ApplicationUserGroup>? ApplicationUserGroups { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<Message>? Messages { get; set; }
        [InverseProperty("Requester")]
        public virtual ICollection<Friendship>? FriendshipsSent { get; set; }
        [InverseProperty("Adresee")]
        public virtual ICollection<Friendship>? FriendshipsReveived { get; set; }



    }
}
