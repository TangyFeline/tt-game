﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfgame.ViewModels
{
    public class BalancePageViewModel
    {
        public string dbName { get; set; }
        public string FriendlyName { get; set; }
        public decimal Balance { get; set; }
        public decimal AbsolutePoints { get; set; }
    }
}