﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Models;

namespace tfgame.ViewModels
{
    public class BlacklistEntryViewModel
    {
        public BlacklistEntry dbBlacklistEntry { get; set; }
        public string PlayerName { get; set;}
        public int PlayerId { get; set; }
    }
}