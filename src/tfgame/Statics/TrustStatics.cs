﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tfgame.Statics
{
    public static class TrustStatics
    {

       

        // also banned names
        public static string[] ReservedNames = {
           "Judoo",
           "Juderp",
           "Testbot",
           "Psychopath",
           "Admin",
           "Administrator",
           "Moderator",
           "Lindella",
           "Hitler",
           "Jewdewfae",
           "Wuffie",
           "Lovebringer",
           };


        public static string NameIsReserved(string name)
        {

            if (ReservedNames.Contains(name)) {
                return name;
            } else {
                return "";
            }
        }

       

    }

}