﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Abstract;
using tfgame.dbModels.Concrete;
using tfgame.dbModels.Models;
using tfgame.ViewModels;

namespace tfgame.Statics
{
    public static class ItemStatics
    {

        public static DbStaticItem GetStaticItem(string dbName)
        {
            IItemRepository itemRepo = new EFItemRepository();
            return itemRepo.DbStaticItems.FirstOrDefault(i => i.dbName == dbName);
        }

        public static IEnumerable<DbStaticItem> GetAllFindableItems()
        {
            IItemRepository itemRepo = new EFItemRepository();
            return itemRepo.DbStaticItems.Where(i => i.Findable == true);
        }

        public static IEnumerable<DbStaticItem> GetAllNonPetItems()
        {
            IItemRepository itemRepo = new EFItemRepository();
            return itemRepo.DbStaticItems.Where(i => i.ItemType != PvPStatics.ItemType_Pet);
        }

        public static IEnumerable<DbStaticItem> GetAllPetItems()
        {
            IItemRepository itemRepo = new EFItemRepository();
            return itemRepo.DbStaticItems.Where(i => i.ItemType == PvPStatics.ItemType_Pet);
        }

        public static List<RAMBuffBox> ItemRAMBuffBoxes;
      
    }

}