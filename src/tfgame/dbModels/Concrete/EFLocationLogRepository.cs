﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using tfgame.dbModels.Abstract;
using tfgame.dbModels.Models;

namespace tfgame.dbModels.Concrete
{
    public class EFLocationLogRepository : ILocationLogRepository
    {
        private StatsContext context = new StatsContext();

        public IQueryable<LocationLog> LocationLogs
        {
            get { return context.LocationLogs; }
        }

        public void SaveLocationLog(LocationLog LocationLog)
        {
            if (LocationLog.Id == 0)
            {
                context.LocationLogs.Add(LocationLog);
            }
            else
            {
                LocationLog editMe = context.LocationLogs.Find(LocationLog.Id);
                if (editMe != null)
                {
                    // dbEntry.Name = LocationLog.Name;
                    // dbEntry.Message = LocationLog.Message;
                    // dbEntry.TimeStamp = LocationLog.TimeStamp;

                }
            }

            try
            {
                context.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                //context.(RefreshMode.ClientWins, dbModels.Models.LocationLog);
                //context.SaveChanges();
            }

           // context.SaveChanges();
        }

        public void DeleteLocationLog(int id)
        {

            LocationLog dbEntry = context.LocationLogs.Find(id);
            if (dbEntry != null)
            {
                context.LocationLogs.Remove(dbEntry);
                context.SaveChanges();
            }
        }

    }
}