﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfgame.dbModels.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string dbName { get; set; }
        public int OwnerId { get; set; }
        public string dbLocationName { get; set; }
        public string VictimName { get; set; }
        public bool IsEquipped { get; set; }
        public int TurnsUntilUse { get; set; }
        public int Level { get; set; }
        public DateTime TimeDropped { get; set; }
        public bool EquippedThisTurn { get; set; }
        public int PvPEnabled { get; set; }
        public bool IsPermanent { get; set; }
        public string Nickname { get; set; }
        public DateTime LastSouledTimestamp { get; set; }

        public string GetFullName()
        {
             if (this.Nickname == null || this.Nickname == "")
            {
                return VictimName;
            }
             else
             {
                 string[] nameArray = this.VictimName.Split(' ');
                 return nameArray[0] + " '" + this.Nickname + "' " + nameArray[1];
             }
        }

    }

    public class Item_VM
    {
        public int Id { get; set; }
        public string dbName { get; set; }
        public int OwnerId { get; set; }
        public string dbLocationName { get; set; }
        public string VictimName { get; set; }
        public bool IsEquipped { get; set; }
        public int TurnsUntilUse { get; set; }
        public int Level { get; set; }
        public DateTime TimeDropped { get; set; }
        public bool EquippedThisTurn { get; set; }
        public int PvPEnabled { get; set; }
        public bool IsPermanent { get; set; }
        public string Nickname { get; set; }
        public DateTime LastSouledTimestamp { get; set; }

        public string GetFullName()
        {
            if (this.Nickname == null || this.Nickname == "")
            {
                return VictimName;
            }
            else
            {
                string[] nameArray = this.VictimName.Split(' ');
                return nameArray[0] + " " + this.Nickname + " " + nameArray[1];
            }
        }
    }
}