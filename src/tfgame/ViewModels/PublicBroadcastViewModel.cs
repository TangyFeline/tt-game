﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfgame.ViewModels
{
    public class PublicBroadcastViewModel
    {
        public string Message { get; set; }
    }

    public class PlayerNameViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewForm { get; set; }
    }
}