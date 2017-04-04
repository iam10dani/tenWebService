using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iam10daniService.Models
{
    public class ContactMe
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public int PhoneNumber { get; set; }
        public string email { get; set; }
        public string massage { get; set; }
    }
}