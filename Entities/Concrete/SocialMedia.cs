﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Entities.Concrete
{
    public class SocialMedia
    {
        [Key]
        public int SocialMediaID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool Status { get; set; }
    }
}
