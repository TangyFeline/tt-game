﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tfgame.dbModels.Models;
using tfgame.dbModels.Abstract;
using tfgame.dbModels.Concrete;

namespace tfgame.Procedures
{
    public static class RPClassifiedAdsProcedures
    {

        public static void SaveAd(RPClassifiedAd input, Player player)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            RPClassifiedAd ad = repo.RPClassifiedAds.FirstOrDefault(i => i.Id == input.Id);

            if (ad == null)
            {
                ad = new RPClassifiedAd
                {
                    OwnerMembershipId = player.MembershipId,
                    CreationTimestamp = DateTime.UtcNow

                };
            }
            ad.Text = input.Text;
            ad.YesThemes = input.YesThemes;
            ad.NoThemes = input.NoThemes;
            ad.RefreshTimestamp = DateTime.UtcNow;

            repo.SaveRPClassifiedAd(ad);

        }

        public static void RefreshAd(int id)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            RPClassifiedAd ad = repo.RPClassifiedAds.FirstOrDefault(i => i.Id == id);
            ad.RefreshTimestamp = DateTime.UtcNow;
            repo.SaveRPClassifiedAd(ad);
        }

        public static void DeleteAd(int id)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            repo.DeleteRPClassifiedAd(id);
        }

        public static IEnumerable<RPClassifiedAd> GetPlayersClassifiedAds(Player player)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            return repo.RPClassifiedAds.Where(i => i.OwnerMembershipId == player.MembershipId);
        }

        public static IEnumerable<RPClassifiedAd> GetClassifiedAds()
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            DateTime markOnlineCutoff = DateTime.UtcNow.AddDays(-3);
            return repo.RPClassifiedAds.Where(i => i.RefreshTimestamp > markOnlineCutoff);
        }

        public static RPClassifiedAd GetClassifiedAd(int id)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            RPClassifiedAd output = repo.RPClassifiedAds.FirstOrDefault(i => i.Id == id);

            if (output == null)
            {
                output = new RPClassifiedAd();
            }

            return output;
        }

        public static int GetPlayerClassifiedAdCount(Player player)
        {
            IRPClassifiedAdRepository repo = new EFRPClassifiedAdsRepository();
            return repo.RPClassifiedAds.Where(i => i.OwnerMembershipId == player.MembershipId).Count();
        }


      

    }
}