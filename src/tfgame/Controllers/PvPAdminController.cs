﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using tfgame.dbModels.Abstract;
using tfgame.dbModels.Concrete;
using tfgame.dbModels.Models;
using tfgame.Extensions;
using tfgame.Procedures;
using tfgame.Procedures.BossProcedures;
using tfgame.Statics;
using tfgame.ViewModels;
using Microsoft.AspNet.Identity;

namespace tfgame.Controllers
{
    public class PvPAdminController : Controller
    {
        //
        // GET: /PvPAdmin/
        public ActionResult PvPAdmin()
        {

            #region location checks

            string output = "<h1>Location Errors: </h1><br>";
            List<Location> places = LocationsStatics.LocationList.GetLocation.ToList();

            foreach (Location place in places)
            {
                if (place.Name_North != null)
                {
                    Location x = places.FirstOrDefault(l => l.dbName == place.Name_North);
                    if (x == null)
                    {
                        output += "Location <b>" + place.dbName + " (" + place.Name + ")</b> is referencing location <b>" + place.Name_North + "</b> which is null.<br>";
                    }
                    else
                    {
                        if (x.Name_South != place.dbName)
                        {
                            output += "North:  Location <b>" + place.dbName + "</b> connects to <b>" + x.dbName + "</b> but it doesn't connect back.<br>";
                        }
                    }
                }
                if (place.Name_East != null)
                {
                    Location x = places.FirstOrDefault(l => l.dbName == place.Name_East);
                    if (x == null)
                    {
                        output += "Location <b>" + place.dbName + "(" + place.Name + ")</b> is referencing location <b>" + place.Name_East + "</b> which is null.";
                    }
                    else
                    {
                        if (x.Name_West != place.dbName)
                        {
                            output += "East:  Location <b>" + place.dbName + "</b> connects to <b>" + x.dbName + "</b> but it doesn't connect back.<br>";
                        }
                    }
                }
                if (place.Name_West != null)
                {
                    Location x = places.FirstOrDefault(l => l.dbName == place.Name_West);
                    if (x == null)
                    {
                        output += "Location <b>" + place.dbName + "(" + place.Name + ")</b> is referencing location <b>" + place.Name_West + "</b> which is null.";
                    }
                    else
                    {
                        if (x.Name_East != place.dbName)
                        {
                            output += "West:  Location <b>" + place.dbName + "</b> connects to <b>" + x.dbName + "</b> but it doesn't connect back.<br>";
                        }
                    }
                }
                if (place.Name_South != null)
                {
                    Location x = places.FirstOrDefault(l => l.dbName == place.Name_South);
                    if (x == null)
                    {
                        output += "Location <b>" + place.dbName + "(" + place.Name + ")</b> is referencing location <b>" + place.Name_South + "</b> which is null.";
                    }
                    else
                    {
                        if (x.Name_North != place.dbName)
                        {
                            output += "South:  Location <b>" + place.dbName + "</b> connects to <b>" + x.dbName + "</b> but it doesn't connect back.<br>";
                        }
                    }
                }
            }

            #endregion

            //#region check skills to forms

            //output += "<h1>Skill to Form Errors</h1><br>";
            //List<DbStaticSkill> skills = SkillStatics.GetAllStaticSkills().ToList();

            //foreach (DbStaticSkill skill in skills)
            //{

            //    // if this skill is learned in a region, make sure a region of that names does exist
            //    if (skill.LearnedAtRegion != null && skill.LearnedAtRegion != "")
            //    {
            //        if (LocationsStatics.LocationList.GetLocation.Where(l => l.Region == skill.LearnedAtRegion).Count() == 0)
            //        {
            //            output += "Skill <b> " + skill.dbName + " is said to be found at region <b>" + skill.LearnedAtRegion + "</b>, but that region does not exist.<br>";
            //        }
            //    }

            //    Form form = FormStatics.GetForm.FirstOrDefault(f => f.dbName == skill.FormdbName);

            //    if (form == null && skill.FormdbName != "none" && skill.ExclusiveToForm == null)
            //    {
            //        output += "Skill <b>" + skill.dbName + "</b> refers to form <b>" + skill.FormdbName + "</b> but that form does not exist!<br><br>";
            //    }


            //    if (skill.ExclusiveToForm != null)
            //    {
            //        Form exclusiveToform = FormStatics.GetForm.FirstOrDefault(f => f.dbName == skill.ExclusiveToForm);
            //        if (exclusiveToform == null)
            //        {
            //            output += "Curse <b>" + skill.dbName + "</b> is exclusive to form <b>" + skill.ExclusiveToForm + "</b> but that form does not exist!<br><br>";
            //        }

            //    }

            //}

            //#endregion

            //#region check forms to items

            //output += "<h1>Form to item Errors</h1><br>";
            //List<Form> forms = FormStatics.GetForm.ToList();

            //foreach (Form form in forms)
            //{
            //    StaticItem item = ItemStatics.GetStaticItem.FirstOrDefault(i => i.dbName == form.BecomesItemDbName);

            //    if (item == null && (form.BecomesItemDbName != null))
            //    {
            //        output += "Form <b>" + form.dbName + "</b> refers to item <b>" + form.BecomesItemDbName + "</b> but that item does not exist!<br><br>";
            //    }

            //}

            //#endregion

            //List<StaticItem> itemsWithEffects = ItemStatics.GetStaticItem.Where(i => i.GivesEffect != null && i.GivesEffect != "").ToList();

            //foreach (StaticItem item in itemsWithEffects)
            //{
            //    StaticEffect effect = EffectStatics.GetStaticEffect.FirstOrDefault(e => e.dbName == item.GivesEffect);

            //    if (effect == null)
            //    {
            //        output += "Item <b>" + item.dbName + "  (" + item.FriendlyName + ")</b> refers to effect <b>" + item.GivesEffect + "</b> but that effect does not exist!<br><br>";
            //    }
            //}


            @ViewBag.Message = output;
            return View("~/Views/PvP/PvPAdmin.cshtml");
        }

        public ActionResult Index()
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            ViewBag.Message = TempData["Message"];
            return View();
        }

        public ActionResult ChangeWorldStats()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();

            PvPWorldStat output = repo.PvPWorldStats.First();

            return View(output);

        }

        public ActionResult ChangeWorldStatsSend(PvPWorldStat input)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();

            PvPWorldStat data = repo.PvPWorldStats.FirstOrDefault(i => i.Id == input.Id);

            data.TurnNumber = input.TurnNumber;
            data.RoundDuration = input.RoundDuration;
            data.ChaosMode = input.ChaosMode;
            data.TestServer = input.TestServer;

            repo.SavePvPWorldStat(data);

            PvPStatics.ChaosMode = data.ChaosMode;
            PvPStatics.RoundDuration = data.RoundDuration;

            TempData["Result"] = "World Data Saved!";
            return RedirectToAction("Play", "PvP");

        }

        public ActionResult SpawnAI(int number, int offset)
        {
            AIProcedures.SpawnAIPsychopaths(number, offset);
            return View("Play");
        }

        public ActionResult RunAIActions(string password)
        {

            if (password == null || password != "oogabooga99")
            {
                TempData["Result"] = "WRONG PASSWORD";
                return RedirectToAction("Play");
            }

            AIProcedures.RunPsychopathActions();

            IPlayerRepository playerRepo = new EFPlayerRepository();

            // automatically spawn in more bots when they go down
            int botCount = playerRepo.Players.Where(b => b.BotId == AIStatics.PsychopathBotId && b.Mobility == "full").Count();
            if (botCount < 15)
            {
                AIProcedures.SpawnAIPsychopaths(15 - botCount, 0);

            }

            return View("Play");
        }

        public ActionResult WriteContributionToFile(int contributionId)
        {
            //http://localhost:53130/PvPAdmin/WriteContributionToFile?contributionId=110

            IContributionRepository contributionRepo = new EFContributionRepository();
            Contribution con = contributionRepo.Contributions.FirstOrDefault(c => c.Id == contributionId);

            string path = Server.MapPath("~/Z_selfHelp/");
            string output = "";

            //  }, new StaticSkill {
            //                dbName = "",
            //                FriendlyName = "",
            //                FormdbName = "",
            //                Description = "",
            //                ManaCost = 0,
            //                HealthDamageAmount = 0,
            //                TFPointsAmount = 0,
            //                DiscoveryMessage = "",
            //                LearnedAtRegion = "",
            //}

            string nl = Environment.NewLine;

            string skilldbname = "skill_" + con.Skill_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
            string formdbname = "form_" + con.Form_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
            string itemdbname = "";

            if (con.Form_MobilityType == "inanimate")
            {
                itemdbname = "item_" + con.Form_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
            }
            else if (con.Form_MobilityType == "animal")
            {
                itemdbname = "animal_" + con.Form_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
            }

            #region write skill



            output = "}, new StaticSkill {" + nl;
            output += "\t\t dbName = \"" + skilldbname + "\"," + nl;
            output += "\t\t FriendlyName = \"" + con.Skill_FriendlyName + "\"," + nl;
            output += "\t\t FormdbName = \"" + formdbname + "\"," + nl;
            output += "\t\t Description = \"" + con.Skill_Description + "\"," + nl;
            output += "\t\t ManaCost = " + con.Skill_ManaCost + "M," + nl;
            output += "\t\t HealthDamageAmount = " + con.Skill_HealthDamageAmount + "M," + nl;
            output += "\t\t TFPointsAmount = " + con.Skill_TFPointsAmount + "M," + nl;

            // new system for writing skill messages out?

            string skillXMLPath = Server.MapPath("~/XMLs/SkillMessages/" + skilldbname + ".xml");

            SkillProcedures.DeleteOldXML(skillXMLPath);
            using (StreamWriter writer = new StreamWriter(skillXMLPath, false))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><StaticSkill xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + nl);
                writer.WriteLine("<DiscoveryMessage>" + con.Skill_DiscoveryMessage + "</DiscoveryMessage>" + nl);
                writer.WriteLine("<Description>" + con.Skill_Description + "</Description>" + nl);
                writer.WriteLine("</StaticSkill>" + nl);
            }

            //  output += "\t\t DiscoveryMessage = \"" + con.Skill_DiscoveryMessage + "\"," + nl;

            if (con.Skill_LearnedAtRegion != null && con.Skill_LearnedAtRegion != "")
            {
                output += "// YOU MUST DO THIS YOURSELF" + nl;
                output += "\t\t LearnedAtRegion = \"" + con.Skill_LearnedAtRegion + "\"," + nl;
            }

            output += "}" + nl + nl + nl;

            #endregion

            //new Form {
            //    dbName = "",
            //   FriendlyName = "",
            //   Description = "",
            //   Gender = "female",
            //   TFEnergyRequired = 68,
            //   MobilityType = "inanimate",
            //   PortraitUrl = "",
            //    BecomesItemDbName = "",
            //    FormBuffs = new BuffBox{},

            #region write form

            output += "}, new Form {" + nl;
            output += "\t\t dbName = \"" + formdbname + "\"," + nl;
            output += "\t\t FriendlyName = \"" + con.Form_FriendlyName + "\"," + nl;
            //output += "\t\t Description = \"" + con.Form_Description + "\"," + nl;
            output += "\t\t Gender = \"" + con.Form_Gender + "\"," + nl;
            output += "\t\t TFEnergyRequired = " + con.Form_TFEnergyRequired + "M," + nl;
            output += "\t\t MobilityType = \"" + con.Form_MobilityType + "\"," + nl;
            output += "\t\t PortraitUrl = \"\"," + nl;

            if (con.Form_MobilityType == "inanimate" || con.Form_MobilityType == "animal")
            {
                output += "\t\t BecomesItemDbName = \"" + itemdbname + "\"," + nl;
            }

            output += "\t\t FormBuffs = new BuffBox{}" + nl;
            output += "}" + nl + nl + nl;

            if (con.Form_Bonuses != null && con.Form_Bonuses != "")
            {
                output += con.Form_Bonuses + nl + nl + nl;
            }

            #endregion

            #region write to item

            if (con.Form_MobilityType != "full")
            {

                //   new StaticItem {
                //    dbName = "",
                //    FriendlyName = "",
                //    PortraitUrl = "",
                //    Description = "",
                //    ItemType = "",
                //    Findable = false,
                //    MoneyValue = 50,

                output += "}, new StaticItem {" + nl;
                output += "\t\t dbName = \"" + itemdbname + "\"," + nl;
                output += "\t\t FriendlyName = \"" + con.Item_FriendlyName + "\"," + nl;
                output += "\t\t PortraitUrl = \"\"," + nl;
                output += "\t\t Description = \"" + con.Item_Description + "\"," + nl;
                output += "\t\t ItemType = PvPStatics.ItemType_" + con.Item_ItemType + "," + nl;
                output += "\t\t Findable = false," + nl;

                output += "}" + nl + nl + nl;

                if (con.Item_Bonuses != null && con.Item_Bonuses != "")
                {
                    output += con.Item_Bonuses + nl + nl + nl;
                }


            }

            #endregion

            output += "New spell, " + con.Skill_FriendlyName + ", submitted by ";

            if (con.SubmitterUrl != null && con.SubmitterUrl != "")
            {
                output += "<a href=\"" + con.SubmitterUrl + "\">" + con.SubmitterName + "</a>!";
            }
            else
            {
                output += con.SubmitterName + "!";
            }

            if (con.AdditionalSubmitterNames != null && con.AdditionalSubmitterNames != "")
            {
                output += "  Additional credits go to " + con.AdditionalSubmitterNames + ".";
            }

            if (con.AssignedToArtist != null && con.AssignedToArtist != "")
            {
                output += "  .  Graphic is by " + con.AssignedToArtist + ".";
            }

            #region write to XML

            // ---------- WRITE TF XML --------------

            using (var stream = new FileStream(path + "spell.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(output);
                }
            }


            string xmlPath = Server.MapPath("~/XMLs/TFMessages/" + formdbname + ".xml");




            string xmlout = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Form xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + nl;

            if (con.Form_Description != null)
            {
                xmlout += "<Description>" + con.Form_Description + "</Description>" + nl + nl;
            }

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_1st != null && con.Form_TFMessage_20_Percent_1st != "")
            {
                xmlout += "<TFMessage_20_Percent_1st>" + con.Form_TFMessage_20_Percent_1st + "</TFMessage_20_Percent_1st>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_1st != null && con.Form_TFMessage_40_Percent_1st != "")
            {
                xmlout += "<TFMessage_40_Percent_1st>" + con.Form_TFMessage_40_Percent_1st + "</TFMessage_40_Percent_1st>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_1st != null && con.Form_TFMessage_60_Percent_1st != "")
            {
                xmlout += "<TFMessage_60_Percent_1st>" + con.Form_TFMessage_60_Percent_1st + "</TFMessage_60_Percent_1st>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_1st != null && con.Form_TFMessage_80_Percent_1st != "")
            {
                xmlout += "<TFMessage_80_Percent_1st>" + con.Form_TFMessage_80_Percent_1st + "</TFMessage_80_Percent_1st>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_1st != null && con.Form_TFMessage_100_Percent_1st != "")
            {
                xmlout += "<TFMessage_100_Percent_1st>" + con.Form_TFMessage_100_Percent_1st + "</TFMessage_100_Percent_1st>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_1st != null && con.Form_TFMessage_Completed_1st != "")
            {
                xmlout += "<TFMessage_Completed_1st>" + con.Form_TFMessage_Completed_1st + "</TFMessage_Completed_1st>" + nl + nl;
            }

            /////////// 1st M

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_1st_M != null && con.Form_TFMessage_20_Percent_1st_M != "")
            {
                xmlout += "<TFMessage_20_Percent_1st_M>" + con.Form_TFMessage_20_Percent_1st_M + "</TFMessage_20_Percent_1st_M>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_1st_M != null && con.Form_TFMessage_40_Percent_1st_M != "")
            {
                xmlout += "<TFMessage_40_Percent_1st_M>" + con.Form_TFMessage_40_Percent_1st_M + "</TFMessage_40_Percent_1st_M>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_1st_M != null && con.Form_TFMessage_60_Percent_1st_M != "")
            {
                xmlout += "<TFMessage_60_Percent_1st_M>" + con.Form_TFMessage_60_Percent_1st_M + "</TFMessage_60_Percent_1st_M>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_1st_M != null && con.Form_TFMessage_80_Percent_1st_M != "")
            {
                xmlout += "<TFMessage_80_Percent_1st_M>" + con.Form_TFMessage_80_Percent_1st_M + "</TFMessage_80_Percent_1st_M>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_1st_M != null && con.Form_TFMessage_100_Percent_1st_M != "")
            {
                xmlout += "<TFMessage_100_Percent_1st_M>" + con.Form_TFMessage_100_Percent_1st_M + "</TFMessage_100_Percent_1st_M>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_1st_M != null && con.Form_TFMessage_Completed_1st_M != "")
            {
                xmlout += "<TFMessage_Completed_1st_M>" + con.Form_TFMessage_Completed_1st_M + "</TFMessage_Completed_1st_M>" + nl + nl;
            }

            /////////// 1st M

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_1st_F != null && con.Form_TFMessage_20_Percent_1st_F != "")
            {
                xmlout += "<TFMessage_20_Percent_1st_F>" + con.Form_TFMessage_20_Percent_1st_F + "</TFMessage_20_Percent_1st_F>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_1st_F != null && con.Form_TFMessage_40_Percent_1st_F != "")
            {
                xmlout += "<TFMessage_40_Percent_1st_F>" + con.Form_TFMessage_40_Percent_1st_F + "</TFMessage_40_Percent_1st_F>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_1st_F != null && con.Form_TFMessage_60_Percent_1st_F != "")
            {
                xmlout += "<TFMessage_60_Percent_1st_F>" + con.Form_TFMessage_60_Percent_1st_F + "</TFMessage_60_Percent_1st_F>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_1st_F != null && con.Form_TFMessage_80_Percent_1st_F != "")
            {
                xmlout += "<TFMessage_80_Percent_1st_F>" + con.Form_TFMessage_80_Percent_1st_F + "</TFMessage_80_Percent_1st_F>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_1st_F != null && con.Form_TFMessage_100_Percent_1st_F != "")
            {
                xmlout += "<TFMessage_100_Percent_1st_F>" + con.Form_TFMessage_100_Percent_1st_F + "</TFMessage_100_Percent_1st_F>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_1st_F != null && con.Form_TFMessage_Completed_1st_F != "")
            {
                xmlout += "<TFMessage_Completed_1st_F>" + con.Form_TFMessage_Completed_1st_F + "</TFMessage_Completed_1st_F>" + nl + nl;
            }

            //////////////////////////////

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_3rd != null && con.Form_TFMessage_20_Percent_3rd != "")
            {
                xmlout += "<TFMessage_20_Percent_3rd>" + con.Form_TFMessage_20_Percent_3rd + "</TFMessage_20_Percent_3rd>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_3rd != null && con.Form_TFMessage_40_Percent_3rd != "")
            {
                xmlout += "<TFMessage_40_Percent_3rd>" + con.Form_TFMessage_40_Percent_3rd + "</TFMessage_40_Percent_3rd>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_3rd != null && con.Form_TFMessage_60_Percent_3rd != "")
            {
                xmlout += "<TFMessage_60_Percent_3rd>" + con.Form_TFMessage_60_Percent_3rd + "</TFMessage_60_Percent_3rd>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_3rd != null && con.Form_TFMessage_80_Percent_3rd != "")
            {
                xmlout += "<TFMessage_80_Percent_3rd>" + con.Form_TFMessage_80_Percent_3rd + "</TFMessage_80_Percent_3rd>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_3rd != null && con.Form_TFMessage_100_Percent_3rd != "")
            {
                xmlout += "<TFMessage_100_Percent_3rd>" + con.Form_TFMessage_100_Percent_3rd + "</TFMessage_100_Percent_3rd>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_3rd != null && con.Form_TFMessage_Completed_3rd != "")
            {
                xmlout += "<TFMessage_Completed_3rd>" + con.Form_TFMessage_Completed_3rd + "</TFMessage_Completed_3rd>" + nl + nl;
            }

            /////////// 1st M

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_3rd_M != null && con.Form_TFMessage_20_Percent_3rd_M != "")
            {
                xmlout += "<TFMessage_20_Percent_3rd_M>" + con.Form_TFMessage_20_Percent_3rd_M + "</TFMessage_20_Percent_3rd_M>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_3rd_M != null && con.Form_TFMessage_40_Percent_3rd_M != "")
            {
                xmlout += "<TFMessage_40_Percent_3rd_M>" + con.Form_TFMessage_40_Percent_3rd_M + "</TFMessage_40_Percent_3rd_M>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_3rd_M != null && con.Form_TFMessage_60_Percent_3rd_M != "")
            {
                xmlout += "<TFMessage_60_Percent_3rd_M>" + con.Form_TFMessage_60_Percent_3rd_M + "</TFMessage_60_Percent_3rd_M>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_3rd_M != null && con.Form_TFMessage_80_Percent_3rd_M != "")
            {
                xmlout += "<TFMessage_80_Percent_3rd_M>" + con.Form_TFMessage_80_Percent_3rd_M + "</TFMessage_80_Percent_3rd_M>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_3rd_M != null && con.Form_TFMessage_100_Percent_3rd_M != "")
            {
                xmlout += "<TFMessage_100_Percent_3rd_M>" + con.Form_TFMessage_100_Percent_3rd_M + "</TFMessage_100_Percent_3rd_M>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_3rd_M != null && con.Form_TFMessage_Completed_3rd_M != "")
            {
                xmlout += "<TFMessage_Completed_3rd_M>" + con.Form_TFMessage_Completed_3rd_M + "</TFMessage_Completed_3rd_M>" + nl + nl;
            }

            /////////// 1st M

            // 1st generic 20
            if (con.Form_TFMessage_20_Percent_3rd_F != null && con.Form_TFMessage_20_Percent_3rd_F != "")
            {
                xmlout += "<TFMessage_20_Percent_3rd_F>" + con.Form_TFMessage_20_Percent_3rd_F + "</TFMessage_20_Percent_3rd_F>" + nl + nl;
            }

            // 1st generic 40
            if (con.Form_TFMessage_40_Percent_3rd_F != null && con.Form_TFMessage_40_Percent_3rd_F != "")
            {
                xmlout += "<TFMessage_40_Percent_3rd_F>" + con.Form_TFMessage_40_Percent_3rd_F + "</TFMessage_40_Percent_3rd_F>" + nl + nl;
            }

            // 1st generic 60
            if (con.Form_TFMessage_60_Percent_3rd_F != null && con.Form_TFMessage_60_Percent_3rd_F != "")
            {
                xmlout += "<TFMessage_60_Percent_3rd_F>" + con.Form_TFMessage_60_Percent_3rd_F + "</TFMessage_60_Percent_3rd_F>" + nl + nl;
            }

            // 1st generic 80
            if (con.Form_TFMessage_80_Percent_3rd_F != null && con.Form_TFMessage_80_Percent_3rd_F != "")
            {
                xmlout += "<TFMessage_80_Percent_3rd_F>" + con.Form_TFMessage_80_Percent_3rd_F + "</TFMessage_80_Percent_3rd_F>" + nl + nl;
            }


            // 1st generic 100
            if (con.Form_TFMessage_100_Percent_3rd_F != null && con.Form_TFMessage_100_Percent_3rd_F != "")
            {
                xmlout += "<TFMessage_100_Percent_3rd_F>" + con.Form_TFMessage_100_Percent_3rd_F + "</TFMessage_100_Percent_3rd_F>" + nl + nl;
            }

            // 1st generic completed
            if (con.Form_TFMessage_Completed_3rd_F != null && con.Form_TFMessage_Completed_3rd_F != "")
            {
                xmlout += "<TFMessage_Completed_3rd_F>" + con.Form_TFMessage_Completed_3rd_F + "</TFMessage_Completed_3rd_F>" + nl + nl;
            }




            xmlout += "</Form>";

            //using (var stream = new FileStream(xmlPath, FileMode.Truncate))
            //{
            //    using (var writer = new StreamWriter(stream))
            //    {
            //        writer.Write(xmlout);
            //    }
            //}

            SkillProcedures.DeleteOldXML(xmlPath);

            using (StreamWriter writer = new StreamWriter(xmlPath, false))
            {

                writer.WriteLine(xmlout);
            }

            #endregion

            return View("Play");
        }

        public ActionResult WriteEffectContributionToFile(int contributionId)
        {
            IEffectContributionRepository contributionRepo = new EFEffectContributionRepository();

            EffectContribution con = contributionRepo.EffectContributions.FirstOrDefault(c => c.Id == contributionId);

            string path = Server.MapPath("~/Z_selfHelp/");
            string nl = Environment.NewLine;
            string output = "";


            string effectdbName = "effect_" + con.Effect_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
            string effectXMLPath = Server.MapPath("~/XMLs/Effects/" + effectdbName + ".xml");

            // if there is any skill to be used, write that out to file as needed
            if (con.Skill_FriendlyName != null)
            {
                string skilldbName = "skill_" + con.Skill_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");

                output = "}, new StaticSkill {" + nl;
                output += "\t\t dbName = \"" + skilldbName + "\"," + nl;
                output += "\t\t FriendlyName = \"" + con.Skill_FriendlyName + "\"," + nl;
                output += "\t\t Description = \"" + con.Skill_Description + "\"," + nl;
                output += "\t\t ManaCost = " + con.Skill_ManaCost + "M," + nl;

                if (con.Skill_UniqueToForm != null && con.Skill_UniqueToForm != null)
                {
                    output += "\t\t ExclusiveToForm = \"" + con.Skill_UniqueToForm + "\"," + nl;
                }

                if (con.Skill_UniqueToItem != null && con.Skill_UniqueToItem != null)
                {
                    output += "\t\t ExclusiveToItem = \"" + con.Skill_UniqueToItem + "\"," + nl;
                }

                if (con.Skill_UniqueToLocation != null && con.Skill_UniqueToLocation != "")
                {
                    output += "\t\t UniqueToLocation = \"" + con.Skill_UniqueToLocation + "\"," + nl;
                }

                output += "\t\t GivesEffect = \"" + effectdbName + "\"," + nl;

                output += "}," + nl + nl + nl;

                // Is there any need to write a skill XML?  The text all happens with the StaticEffect object, so I think maybe not.

                //string skillXMLPath = Server.MapPath("~/XMLs/SkillMessages/" + skilldbname + ".xml");
                //SkillProcedures.DeleteOldXML(skillXMLPath);
                //using (StreamWriter writer = new StreamWriter(skillXMLPath, false))
                //{
                //    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><StaticSkill xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + nl);
                //    writer.WriteLine("<DiscoveryMessage>" + con.Effect_AttackHitText + "</DiscoveryMessage>" + nl);
                //    writer.WriteLine("<Description>" + con.Skill_Description + "</Description>" + nl);
                //    writer.WriteLine("</StaticSkill>" + nl);
                //}
            }

            // write effect

            output += "}, new StaticEffect {" + nl;
            output += "\t\t dbName = \"" + effectdbName + "\"," + nl;
            output += "\t\t FriendlyName = \"" + con.Effect_FriendlyName + "\"," + nl;
            output += "\t\t Description = \"" + con.Effect_Description + "\"," + nl;
            output += "\t\t Duration = " + con.Effect_Duration + "," + nl;
            output += "\t\t Cooldown = " + con.Effect_Cooldown + "," + nl;
            output += "\t\t AvailableAtLevel = 0," + nl;
            output += "}," + nl + nl + nl;

            output += con.Effect_Bonuses + nl + nl + nl;




            SkillProcedures.DeleteOldXML(effectXMLPath);
            using (StreamWriter writer = new StreamWriter(effectXMLPath, false))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><StaticEffect xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + nl);

                writer.WriteLine("<Description>" + con.Effect_Description + "</Description>" + nl);

                if (con.Effect_AttackHitText != null)
                {
                    writer.WriteLine("<AttackerWhenHit>" + con.Effect_AttackHitText + "</AttackerWhenHit>" + nl);
                }

                if (con.Effect_AttackHitText != null)
                {
                    writer.WriteLine("<AttackerWhenHit_M>" + con.Effect_AttackHitText + "</AttackerWhenHit_M>" + nl);
                }

                if (con.Effect_AttackHitText != null)
                {
                    writer.WriteLine("<AttackerWhenHit_F>" + con.Effect_AttackHitText + "</AttackerWhenHit_F>" + nl);
                }

                //////////////

                if (con.Effect_VictimHitText != null)
                {
                    writer.WriteLine("<MessageWhenHit>" + con.Effect_VictimHitText + "</MessageWhenHit>" + nl);
                }

                if (con.Effect_VictimHitText_M != null)
                {
                    writer.WriteLine("<MessageWhenHit_M>" + con.Effect_VictimHitText_M + "</MessageWhenHit_M>" + nl);
                }

                if (con.Effect_VictimHitText_F != null)
                {
                    writer.WriteLine("<MessageWhenHit_F>" + con.Effect_VictimHitText_F + "</MessageWhenHit_F>" + nl);
                }

                writer.WriteLine("</StaticEffect>" + nl);
            }

            output += "New curse, " + con.Skill_FriendlyName + ", submitted by " + con.SubmitterName + " with additional credits by " + con.AdditionalSubmitterNames + ".";


            using (var stream = new FileStream(path + "spell.txt", FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(output);
                }
            }

            string effectdbname = "skill_" + con.Skill_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");

            return View("Play");
        }

        public ActionResult ApproveEffectContributionList()
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IEffectContributionRepository effectConRepo = new EFEffectContributionRepository();
            IEnumerable<EffectContribution> output = effectConRepo.EffectContributions.Where(c => c.IsLive == false && c.ReadyForReview == true && c.ApprovedByAdmin == false && c.ProofreadingCopy == false);

            return View(output);
        }

        public ActionResult ApproveEffectContribution(int id)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IEffectContributionRepository effectConRepo = new EFEffectContributionRepository();

            EffectContribution OldCopy = effectConRepo.EffectContributions.FirstOrDefault(c => c.Id == id);

            EffectContribution ProofreadCopy = effectConRepo.EffectContributions.FirstOrDefault(c => c.ProofreadingCopy == true && c.Effect_FriendlyName == OldCopy.Effect_FriendlyName);

            if (ProofreadCopy != null)
            {
                ViewBag.Message = "There's already a proofreading copy for this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }
            else
            {
                ProofreadCopy = new EffectContribution();
            }

            OldCopy.ApprovedByAdmin = true;

            effectConRepo.SaveEffectContribution(OldCopy);

            ProofreadCopy.OwnerMemberhipId = OldCopy.OwnerMemberhipId;
            ProofreadCopy.SubmitterName = OldCopy.SubmitterName;
            ProofreadCopy.SubmitterURL = OldCopy.SubmitterURL;
            ProofreadCopy.ReadyForReview = OldCopy.ReadyForReview;

            ProofreadCopy.Skill_FriendlyName = OldCopy.Skill_FriendlyName;
            ProofreadCopy.Skill_UniqueToForm = OldCopy.Skill_UniqueToForm;
            ProofreadCopy.Skill_UniqueToItem = OldCopy.Skill_UniqueToItem;
            ProofreadCopy.Skill_UniqueToLocation = OldCopy.Skill_UniqueToLocation;
            ProofreadCopy.Skill_Description = OldCopy.Skill_Description;
            ProofreadCopy.Skill_ManaCost = OldCopy.Skill_ManaCost;

            ProofreadCopy.Effect_FriendlyName = OldCopy.Effect_FriendlyName;
            ProofreadCopy.Effect_Description = OldCopy.Effect_Description;
            ProofreadCopy.Effect_Duration = OldCopy.Effect_Duration;
            ProofreadCopy.Effect_Cooldown = OldCopy.Effect_Cooldown;
            ProofreadCopy.Effect_Bonuses = OldCopy.Effect_Bonuses;
            ProofreadCopy.Effect_VictimHitText = OldCopy.Effect_VictimHitText;
            ProofreadCopy.Effect_VictimHitText_M = OldCopy.Effect_VictimHitText_M;
            ProofreadCopy.Effect_VictimHitText_F = OldCopy.Effect_VictimHitText_F;
            ProofreadCopy.Effect_AttackHitText = OldCopy.Effect_AttackHitText;
            ProofreadCopy.Effect_AttackHitText_M = OldCopy.Effect_AttackHitText_M;
            ProofreadCopy.Effect_AttackHitText_F = OldCopy.Effect_AttackHitText_F;

            ProofreadCopy.Timestamp = DateTime.UtcNow;
            ProofreadCopy.ProofreadingCopy = true;
            ProofreadCopy.ProofreadingCopyForOriginalId = OldCopy.Id;
            ProofreadCopy.ApprovedByAdmin = true;

            ProofreadCopy.AdditionalSubmitterNames = OldCopy.AdditionalSubmitterNames;
            ProofreadCopy.Notes = OldCopy.Notes;

            // stats
            ProofreadCopy.HealthBonusPercent = OldCopy.HealthBonusPercent;
            ProofreadCopy.ManaBonusPercent = OldCopy.ManaBonusPercent;
            ProofreadCopy.ExtraSkillCriticalPercent = OldCopy.ExtraSkillCriticalPercent;
            ProofreadCopy.HealthRecoveryPerUpdate = OldCopy.HealthRecoveryPerUpdate;
            ProofreadCopy.ManaRecoveryPerUpdate = OldCopy.ManaRecoveryPerUpdate;
            ProofreadCopy.SneakPercent = OldCopy.SneakPercent;
            ProofreadCopy.EvasionPercent = OldCopy.EvasionPercent;
            ProofreadCopy.EvasionNegationPercent = OldCopy.EvasionNegationPercent;
            ProofreadCopy.MeditationExtraMana = OldCopy.MeditationExtraMana;
            ProofreadCopy.CleanseExtraHealth = OldCopy.CleanseExtraHealth;
            ProofreadCopy.MoveActionPointDiscount = OldCopy.MoveActionPointDiscount;
            ProofreadCopy.SpellExtraTFEnergyPercent = OldCopy.SpellExtraTFEnergyPercent;
            ProofreadCopy.SpellExtraHealthDamagePercent = OldCopy.SpellExtraHealthDamagePercent;
            ProofreadCopy.CleanseExtraTFEnergyRemovalPercent = OldCopy.CleanseExtraTFEnergyRemovalPercent;
            ProofreadCopy.SpellMisfireChanceReduction = OldCopy.SpellMisfireChanceReduction;
            ProofreadCopy.SpellHealthDamageResistance = OldCopy.SpellHealthDamageResistance;
            ProofreadCopy.SpellTFEnergyDamageResistance = OldCopy.SpellTFEnergyDamageResistance;
            ProofreadCopy.ExtraInventorySpace = OldCopy.ExtraInventorySpace;

            ProofreadCopy.Discipline = OldCopy.Discipline;
            ProofreadCopy.Perception = OldCopy.Perception;
            ProofreadCopy.Charisma = OldCopy.Charisma;
            ProofreadCopy.Fortitude = OldCopy.Fortitude;
            ProofreadCopy.Agility = OldCopy.Agility;
            ProofreadCopy.Allure = OldCopy.Allure;
            ProofreadCopy.Magicka = OldCopy.Magicka;
            ProofreadCopy.Succour = OldCopy.Succour;
            ProofreadCopy.Luck = OldCopy.Luck;

            effectConRepo.SaveEffectContribution(ProofreadCopy);


            return RedirectToAction("ApproveEffectContributionList");

            // IEffectContributionRepository conRepo = new EFEffectContributionRepository();
        }

        public ActionResult DoesLocationHaveXML()
        {

            string nl = Environment.NewLine;
            string output = "";
            ViewBag.Message = "";

            foreach (Location loc in LocationsStatics.LocationList.GetLocation.Where(l => l.dbName != ""))
            {

                if (loc.Description == null || loc.Description == "")
                {

                    string xmlpathLocation = Server.MapPath("~/XMLs/LocationDescriptions/" + loc.dbName + ".xml");

                    if (!System.IO.File.Exists(xmlpathLocation))
                    {
                        output += xmlpathLocation + "</br>";
                    }
                    // SkillProcedures.DeleteOldXML(xmlpathLocation);

                    //using (StreamWriter writer = new StreamWriter(xmlpathLocation, false))
                    //{
                    //    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?><Location xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + nl);
                    //    writer.WriteLine("<Description>" + loc.Description + "</Description>" + nl);
                    //    writer.WriteLine("</Location>" + nl);
                    //}

                }



            }

            ViewBag.Text = output;

            return View("Play");
        }

        public ActionResult PublicBroadcast()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }
            else
            {
                PublicBroadcastViewModel output = new PublicBroadcastViewModel();
                return View(output);
            }
        }

        public ActionResult SendPublicBroadcast(PublicBroadcastViewModel input)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }
            else
            {
                IPlayerRepository playerRepo = new EFPlayerRepository();
                IPlayerLogRepository logRepo = new EFPlayerLogRepository();

                string msg = "<span class='bad'>PUBLIC SERVER NOTE:  " + input.Message + "</span>";

                List<Player> players = playerRepo.Players.Where(p => p.BotId == AIStatics.ActivePlayerBotId).ToList();

                string errors = "";

                foreach (Player p in players)
                {
                    try
                    {
                        PlayerLogProcedures.AddPlayerLog(p.Id, msg, true);
                    }
                    catch (Exception e)
                    {
                        errors += "<p>" + p.GetFullName() + "encountered:  " + e.ToString() + "</p><br/>";
                    }
                }

                TempData["Message"] = errors;

                return RedirectToAction("Index");
            }
        }

        public ActionResult ChangeGameDate()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }
            else
            {
                PublicBroadcastViewModel output = new PublicBroadcastViewModel();
                IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();

                PvPWorldStat stats = repo.PvPWorldStats.FirstOrDefault(p => p.Id != -1);

                if (stats != null)
                {
                    output.Message = stats.GameNewsDate;
                }

                return View(output);
            }
        }

        public ActionResult ChangeGameDateSend(PublicBroadcastViewModel input)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }
            else
            {
                IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();

                PvPWorldStat stats = repo.PvPWorldStats.FirstOrDefault(p => p.Id != -1);
                stats.GameNewsDate = input.Message;
                repo.SavePvPWorldStat(stats);
                PvPStatics.LastGameUpdate = stats.GameNewsDate;

                //   TempData["Message"] = errors;

                return RedirectToAction("Index");
            }
        }

        public ActionResult test()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            BossProcedures_FaeBoss.RunTurnLogic();

            return RedirectToAction("Index");
        }

        public ActionResult MigrateItemPortraits()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticItemRepository itemRepo = new EFDbStaticItemRepository();
            IDbStaticFormRepository formRepo = new EFDbStaticFormRepository();

            List<DbStaticForm> forms = formRepo.DbStaticForms.Where(f => f.MobilityType == "inanimate" || f.MobilityType == "animal").ToList();
            
            foreach (DbStaticForm form in forms) {

                DbStaticItem item = itemRepo.DbStaticItems.FirstOrDefault(i => i.dbName == form.BecomesItemDbName);

                if (item != null)
                {
                    form.PortraitUrl = item.PortraitUrl;
                    formRepo.SaveDbStaticForm(form);
                }
            }

            return RedirectToAction("Index");
        }

        

        public ActionResult SpawnNPCs()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            AIProcedures.SpawnLindella();
            BossProcedures_PetMerchant.SpawnPetMerchant();
            BossProcedures_Jewdewfae.SpawnFae();
            AIProcedures.SpawnBartender();
            BossProcedures_Loremaster.SpawnLoremaster();

            return RedirectToAction("Index");
        }

        public ActionResult ItemPetJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticItemRepository itemRepo = new EFDbStaticItemRepository();
            return Json(itemRepo.DbStaticItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FormJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticFormRepository formRepo = new EFDbStaticFormRepository();
            return Json(formRepo.DbStaticForms, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SpellJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticSkillRepository skillRepo = new EFDbStaticSkillRepository();
            IEnumerable<DbStaticSkill> output = skillRepo.DbStaticSkills.ToList();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EffectJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticEffectRepository effectRepo = new EFDbStaticEffectRepository();
            return Json(effectRepo.DbStaticEffects, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FurnitureJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            IDbStaticFurnitureRepository effectRepo = new EFDbStaticFurnitureRepository();
            return Json(effectRepo.DbStaticFurnitures, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LocationJSON()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_JSON) == false)
            {
                return View("Play", "PvP");
            }

            List<Location> locations = LocationsStatics.LocationList.GetLocation.Where(l => l.dbName.Contains("_dungeon") == false).ToList();


            // conceal some data about dungeon location in case whoever pulls this JSON is trying to make a map
            foreach (Location l in locations)
            {
                if (l.dbName.Contains("_dungeon"))
                {
                    l.X = 0;
                    l.Y = 0;
                    l.Name_East = "";
                    l.Name_North = "";
                    l.Name_South = "";
                    l.Name_West = "";
                }
            }

            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ApproveContributionList()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_Previewer) == false)
            {
                return View("Play", "PvP");
            }

            IContributionRepository contRepo = new EFContributionRepository();

            IEnumerable<Contribution> output = contRepo.Contributions.Where(c => c.IsLive == false && c.IsReadyForReview == true && c.AdminApproved == false && c.ProofreadingCopy == false);
            return View(output);

        }



        public ActionResult ApproveContribution(int id)
        {
            // assert only admin can view this
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IContributionRepository contributionRepo = new EFContributionRepository();

            Contribution OldCopy = contributionRepo.Contributions.FirstOrDefault(c => c.Id == id);

            Contribution ProofreadCopy = contributionRepo.Contributions.FirstOrDefault(c => c.ProofreadingCopy == true && c.Skill_FriendlyName == OldCopy.Skill_FriendlyName);


            Player owner = PlayerProcedures.GetPlayerFromMembership(OldCopy.OwnerMembershipId);

            if (owner != null)
            {
                PlayerLogProcedures.AddPlayerLog(owner.Id, "<b>A contribution you have submitted has been approved.</b>", true);
            }

            if (ProofreadCopy != null)
            {
                ViewBag.Message = "There's already a proofreading copy for this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }
            else
            {
                ProofreadCopy = new Contribution();
            }

            // unlock the proofreading flag since it has been saved


            ProofreadCopy.OwnerMembershipId = OldCopy.OwnerMembershipId;
            ProofreadCopy.IsReadyForReview = true;
            ProofreadCopy.IsLive = false;
            ProofreadCopy.ProofreadingCopy = true;
            ProofreadCopy.CheckedOutBy = "";
            ProofreadCopy.ProofreadingLockIsOn = false;
            ProofreadCopy.AdminApproved = true;

            ProofreadCopy.Skill_FriendlyName = OldCopy.Skill_FriendlyName;
            ProofreadCopy.Skill_FormFriendlyName = OldCopy.Skill_FormFriendlyName;
            ProofreadCopy.Skill_Description = OldCopy.Skill_Description;
            ProofreadCopy.Skill_ManaCost = OldCopy.Skill_ManaCost;
            ProofreadCopy.Skill_TFPointsAmount = OldCopy.Skill_TFPointsAmount;
            ProofreadCopy.Skill_HealthDamageAmount = OldCopy.Skill_HealthDamageAmount;
            ProofreadCopy.Skill_LearnedAtLocationOrRegion = OldCopy.Skill_LearnedAtLocationOrRegion;
            ProofreadCopy.Skill_LearnedAtRegion = OldCopy.Skill_LearnedAtRegion;
            ProofreadCopy.Skill_DiscoveryMessage = OldCopy.Skill_DiscoveryMessage;
            ProofreadCopy.Skill_IsPlayerLearnable = OldCopy.Skill_IsPlayerLearnable;

            ProofreadCopy.Form_FriendlyName = OldCopy.Form_FriendlyName;
            ProofreadCopy.Form_Description = OldCopy.Form_Description;
            ProofreadCopy.Form_TFEnergyRequired = OldCopy.Form_TFEnergyRequired;
            ProofreadCopy.Form_Gender = OldCopy.Form_Gender;
            ProofreadCopy.Form_MobilityType = OldCopy.Form_MobilityType;
            ProofreadCopy.Form_BecomesItemDbName = OldCopy.Form_BecomesItemDbName;
            ProofreadCopy.Form_Bonuses = OldCopy.Form_Bonuses;

            ProofreadCopy.Form_TFMessage_20_Percent_1st = OldCopy.Form_TFMessage_20_Percent_1st;
            ProofreadCopy.Form_TFMessage_40_Percent_1st = OldCopy.Form_TFMessage_40_Percent_1st;
            ProofreadCopy.Form_TFMessage_60_Percent_1st = OldCopy.Form_TFMessage_60_Percent_1st;
            ProofreadCopy.Form_TFMessage_80_Percent_1st = OldCopy.Form_TFMessage_80_Percent_1st;
            ProofreadCopy.Form_TFMessage_100_Percent_1st = OldCopy.Form_TFMessage_100_Percent_1st;
            ProofreadCopy.Form_TFMessage_Completed_1st = OldCopy.Form_TFMessage_Completed_1st;

            ProofreadCopy.Form_TFMessage_20_Percent_1st_M = OldCopy.Form_TFMessage_20_Percent_1st_M;
            ProofreadCopy.Form_TFMessage_40_Percent_1st_M = OldCopy.Form_TFMessage_40_Percent_1st_M;
            ProofreadCopy.Form_TFMessage_60_Percent_1st_M = OldCopy.Form_TFMessage_60_Percent_1st_M;
            ProofreadCopy.Form_TFMessage_80_Percent_1st_M = OldCopy.Form_TFMessage_80_Percent_1st_M;
            ProofreadCopy.Form_TFMessage_100_Percent_1st_M = OldCopy.Form_TFMessage_100_Percent_1st_M;
            ProofreadCopy.Form_TFMessage_Completed_1st_M = OldCopy.Form_TFMessage_Completed_1st_M;

            ProofreadCopy.Form_TFMessage_20_Percent_1st_F = OldCopy.Form_TFMessage_20_Percent_1st_F;
            ProofreadCopy.Form_TFMessage_40_Percent_1st_F = OldCopy.Form_TFMessage_40_Percent_1st_F;
            ProofreadCopy.Form_TFMessage_60_Percent_1st_F = OldCopy.Form_TFMessage_60_Percent_1st_F;
            ProofreadCopy.Form_TFMessage_80_Percent_1st_F = OldCopy.Form_TFMessage_80_Percent_1st_F;
            ProofreadCopy.Form_TFMessage_100_Percent_1st_F = OldCopy.Form_TFMessage_100_Percent_1st_F;
            ProofreadCopy.Form_TFMessage_Completed_1st_F = OldCopy.Form_TFMessage_Completed_1st_F;

            ProofreadCopy.Form_TFMessage_20_Percent_3rd = OldCopy.Form_TFMessage_20_Percent_3rd;
            ProofreadCopy.Form_TFMessage_40_Percent_3rd = OldCopy.Form_TFMessage_40_Percent_3rd;
            ProofreadCopy.Form_TFMessage_60_Percent_3rd = OldCopy.Form_TFMessage_60_Percent_3rd;
            ProofreadCopy.Form_TFMessage_80_Percent_3rd = OldCopy.Form_TFMessage_80_Percent_3rd;
            ProofreadCopy.Form_TFMessage_100_Percent_3rd = OldCopy.Form_TFMessage_100_Percent_3rd;
            ProofreadCopy.Form_TFMessage_Completed_3rd = OldCopy.Form_TFMessage_Completed_3rd;

            ProofreadCopy.Form_TFMessage_20_Percent_3rd_M = OldCopy.Form_TFMessage_20_Percent_3rd_M;
            ProofreadCopy.Form_TFMessage_40_Percent_3rd_M = OldCopy.Form_TFMessage_40_Percent_3rd_M;
            ProofreadCopy.Form_TFMessage_60_Percent_3rd_M = OldCopy.Form_TFMessage_60_Percent_3rd_M;
            ProofreadCopy.Form_TFMessage_80_Percent_3rd_M = OldCopy.Form_TFMessage_80_Percent_3rd_M;
            ProofreadCopy.Form_TFMessage_100_Percent_3rd_M = OldCopy.Form_TFMessage_100_Percent_3rd_M;
            ProofreadCopy.Form_TFMessage_Completed_3rd_M = OldCopy.Form_TFMessage_Completed_3rd_M;

            ProofreadCopy.Form_TFMessage_20_Percent_3rd_F = OldCopy.Form_TFMessage_20_Percent_3rd_F;
            ProofreadCopy.Form_TFMessage_40_Percent_3rd_F = OldCopy.Form_TFMessage_40_Percent_3rd_F;
            ProofreadCopy.Form_TFMessage_60_Percent_3rd_F = OldCopy.Form_TFMessage_60_Percent_3rd_F;
            ProofreadCopy.Form_TFMessage_80_Percent_3rd_F = OldCopy.Form_TFMessage_80_Percent_3rd_F;
            ProofreadCopy.Form_TFMessage_100_Percent_3rd_F = OldCopy.Form_TFMessage_100_Percent_3rd_F;
            ProofreadCopy.Form_TFMessage_Completed_3rd_F = OldCopy.Form_TFMessage_Completed_3rd_F;

            ProofreadCopy.CursedTF_FormdbName = OldCopy.CursedTF_FormdbName;
            ProofreadCopy.CursedTF_Fail = OldCopy.CursedTF_Fail;
            ProofreadCopy.CursedTF_Fail_M = OldCopy.CursedTF_Fail_M;
            ProofreadCopy.CursedTF_Fail_F = OldCopy.CursedTF_Fail_F;
            ProofreadCopy.CursedTF_Succeed = OldCopy.CursedTF_Succeed;
            ProofreadCopy.CursedTF_Succeed_M = OldCopy.CursedTF_Succeed_M;
            ProofreadCopy.CursedTF_Succeed_F = OldCopy.CursedTF_Succeed_F;

            ProofreadCopy.Item_FriendlyName = OldCopy.Item_FriendlyName;
            ProofreadCopy.Item_Description = OldCopy.Item_Description;
            ProofreadCopy.Item_ItemType = OldCopy.Item_ItemType;
            ProofreadCopy.Item_UseCooldown = OldCopy.Item_UseCooldown;
            ProofreadCopy.Item_UsageMessage_Item = OldCopy.Item_UsageMessage_Item;
            ProofreadCopy.Item_UsageMessage_Player = OldCopy.Item_UsageMessage_Player;
            ProofreadCopy.Item_Bonuses = OldCopy.Item_Bonuses;

            ProofreadCopy.SubmitterName = OldCopy.SubmitterName;
            ProofreadCopy.AdditionalSubmitterNames = OldCopy.AdditionalSubmitterNames;
            ProofreadCopy.Notes = OldCopy.Notes;
            ProofreadCopy.SubmitterUrl = OldCopy.SubmitterUrl;

            ProofreadCopy.AssignedToArtist = OldCopy.AssignedToArtist;

            ProofreadCopy.CreationTimestamp = DateTime.UtcNow;

            ProofreadCopy.ProofreadingCopyForOriginalId = OldCopy.Id;

            ProofreadCopy.HealthBonusPercent = OldCopy.HealthBonusPercent;
            ProofreadCopy.ManaBonusPercent = OldCopy.ManaBonusPercent;
            ProofreadCopy.ExtraSkillCriticalPercent = OldCopy.ExtraSkillCriticalPercent;
            ProofreadCopy.HealthRecoveryPerUpdate = OldCopy.HealthRecoveryPerUpdate;
            ProofreadCopy.ManaRecoveryPerUpdate = OldCopy.ManaRecoveryPerUpdate;
            ProofreadCopy.SneakPercent = OldCopy.SneakPercent;
            ProofreadCopy.EvasionPercent = OldCopy.EvasionPercent;
            ProofreadCopy.EvasionNegationPercent = OldCopy.EvasionNegationPercent;
            ProofreadCopy.MeditationExtraMana = OldCopy.MeditationExtraMana;
            ProofreadCopy.CleanseExtraHealth = OldCopy.CleanseExtraHealth;
            ProofreadCopy.MoveActionPointDiscount = OldCopy.MoveActionPointDiscount;
            ProofreadCopy.SpellExtraTFEnergyPercent = OldCopy.SpellExtraTFEnergyPercent;
            ProofreadCopy.SpellExtraHealthDamagePercent = OldCopy.SpellExtraHealthDamagePercent;
            ProofreadCopy.CleanseExtraTFEnergyRemovalPercent = OldCopy.CleanseExtraTFEnergyRemovalPercent;
            ProofreadCopy.SpellMisfireChanceReduction = OldCopy.SpellMisfireChanceReduction;
            ProofreadCopy.SpellHealthDamageResistance = OldCopy.SpellHealthDamageResistance;
            ProofreadCopy.SpellTFEnergyDamageResistance = OldCopy.SpellTFEnergyDamageResistance;
            ProofreadCopy.ExtraInventorySpace = OldCopy.ExtraInventorySpace;


            ProofreadCopy.Discipline = OldCopy.Discipline;
            ProofreadCopy.Perception = OldCopy.Perception;
            ProofreadCopy.Charisma = OldCopy.Charisma;
            ProofreadCopy.Fortitude = OldCopy.Fortitude;
            ProofreadCopy.Agility = OldCopy.Agility;
            ProofreadCopy.Allure = OldCopy.Allure;
            ProofreadCopy.Magicka = OldCopy.Magicka;
            ProofreadCopy.Succour = OldCopy.Succour;
            ProofreadCopy.Luck = OldCopy.Luck;

            ProofreadCopy.ImageURL = OldCopy.ImageURL;

            // ------------------ ORIGINAL COPY ------------------------------


            OldCopy.IsReadyForReview = true;
            OldCopy.IsLive = false;
            OldCopy.AdminApproved = true;
            OldCopy.ProofreadingCopy = false;



            contributionRepo.SaveContribution(ProofreadCopy);
            contributionRepo.SaveContribution(OldCopy);

            ViewBag.Message = "Success.";
            return RedirectToAction("ApproveContributionList");

        }

        public ActionResult RejectContribution(int id)
        {
            // assert only admin can view this
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IContributionRepository contRepo = new EFContributionRepository();

            Contribution contribution = contRepo.Contributions.FirstOrDefault(i => i.Id == id);
            contribution.IsReadyForReview = false;
            contRepo.SaveContribution(contribution);

            Player owner = PlayerProcedures.GetPlayerFromMembership(contribution.OwnerMembershipId);

            if (owner != null)
            {
                PlayerLogProcedures.AddPlayerLog(owner.Id, "<b>A contribution you have submitted has been rejected.  You should have received or will soon a message explaining why or what else needs to be done before this contribution can be accepted.  If you do not, please message Judoo on the forums.</b>", true);
            }


            return RedirectToAction("ApproveContributionList");
        }

        public ActionResult RenameStuff()
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            return View();
        }

        public ActionResult RenameSkill(string oldSkillName, string newSkillName, bool practice)
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            string output = "<br><br>";

            if (practice == true)
            {
                output += "<b>PRACTICE MODE</b>";
            }
            else
            {
                output += "<b>LIVE EDIT MODE</b>";
            }

            oldSkillName = oldSkillName.Trim();
            newSkillName = newSkillName.Trim();

            IDbStaticSkillRepository sskillRepo = new EFDbStaticSkillRepository();
            DbStaticSkill sskill = sskillRepo.DbStaticSkills.FirstOrDefault(s => s.dbName == oldSkillName);

            if (sskill != null)
            {
                sskill.dbName = newSkillName;

                if (practice == false)
                {
                    sskillRepo.SaveDbStaticSkill(sskill);
                }
                output += "Renamed static skill to <b>" + newSkillName + "</b>.</br>";
            }
            else
            {
                output += "NO STATIC SKILL TO RENAME.</br>";
            }

            ISkillRepository skillRepo = new EFSkillRepository();
            List<Skill> skills = skillRepo.Skills.Where(s => s.Name == oldSkillName).ToList();

            foreach (Skill s in skills)
            {
                s.Name = newSkillName;
                if (practice == false)
                {
                    skillRepo.SaveSkill(s);
                }
            }

            output += "Renamed <b>" + skills.Count() + "</b> player-known skills to <b>" + newSkillName + "</b>.</br>";

            output += "</br> DON'T FORGET TO UPDATE ANY SKILLS HARDCODED INTO THE PROJECT.</br>";

            TempData["Message"] = output;
            return RedirectToAction("Index");
        }



        public ActionResult RenameForm(string oldFormName, string newFormName, bool practice)
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            string output = "<br><br>";

            if (practice == true)
            {
                output += "<b>PRACTICE MODE</b>";
            }
            else
            {
                output += "<b>LIVE EDIT MODE</b>";
            }

            oldFormName = oldFormName.Trim();
            newFormName = newFormName.Trim();

            #region static form to rename
            IDbStaticFormRepository sFormRepo = new EFDbStaticFormRepository();
            DbStaticForm sform = sFormRepo.DbStaticForms.FirstOrDefault(s => s.dbName == oldFormName);
            if (sform != null)
            {
                sform.dbName = newFormName;

                if (practice == false)
                {
                    sFormRepo.SaveDbStaticForm(sform);
                }
                output += "Renamed static form to <b>" + newFormName + "</b>.</br>";
            }
            else
            {
                output += "NO STATIC FORM TO RENAME.</br>";
            }
            #endregion

            #region players form to update
            IPlayerRepository playerRepo = new EFPlayerRepository();
            List<Player> players = playerRepo.Players.Where(s => s.Form == oldFormName).ToList();
            foreach (Player p in players)
            {
                p.Form = newFormName;
                if (practice == false)
                {
                    playerRepo.SavePlayer(p);
                }
            }
            output += "Set <b>" + players.Count() + "</b> players to new form <b>" + newFormName + "</b>.</br>";
            #endregion

            #region static skills that turn target into this form
            IDbStaticSkillRepository sskillRepo = new EFDbStaticSkillRepository();
            List<DbStaticSkill> skills = sskillRepo.DbStaticSkills.Where(s => s.FormdbName == oldFormName).ToList();
            foreach (DbStaticSkill ss in skills)
            {
                ss.FormdbName = newFormName;

                if (practice == false)
                {
                    sskillRepo.SaveDbStaticSkill(ss);
                }
            }
            output += "Set <b>" + skills.Count() + "</b> static skills to that turn victim into form <b>" + newFormName + "</b>.</br>";
            #endregion

            #region static skills exclusive to form
            //IDbStaticSkillRepository sskillRepo = new EFDbStaticSkillRepository();
            skills = sskillRepo.DbStaticSkills.Where(s => s.ExclusiveToForm == oldFormName).ToList();
            foreach (DbStaticSkill ss in skills)
            {
                ss.ExclusiveToForm = newFormName;

                if (practice == false)
                {
                    sskillRepo.SaveDbStaticSkill(ss);
                }
            }
            output += "Set <b>" + skills.Count() + "</b> static skills to be exclusive to form <b>" + newFormName + "</b>.</br>";
            #endregion

            #region effect contributions to update
            IEffectContributionRepository contRepo = new EFEffectContributionRepository();
            List<EffectContribution> contributions = contRepo.EffectContributions.Where(s => s.Skill_UniqueToForm == oldFormName).ToList();
            foreach (EffectContribution c in contributions)
            {
                c.Skill_UniqueToForm = newFormName;

                if (practice == false)
                {
                    contRepo.SaveEffectContribution(c);
                }
            }
            output += "Set <b>" + contributions.Count() + "</b> effect contributions to be exclusive to form <b>" + newFormName + "</b>.</br>";
            #endregion


            #region tf energies to update
            ITFEnergyRepository energyRepo = new EFTFEnergyRepository();
            List<TFEnergy> energies = energyRepo.TFEnergies.Where(s => s.FormName == oldFormName).ToList();
            foreach (TFEnergy en in energies)
            {
                en.FormName = newFormName;

                if (practice == false)
                {
                    energyRepo.SaveTFEnergy(en);
                }
            }
            output += "Set <b>" + energies.Count() + "</b> TF energies to use form <b>" + newFormName + "</b>.</br>";
            #endregion


            #region tf messages
            ITFMessageRepository messageRepo = new EFTFMessageRepository();
            List<TFMessage> messages = messageRepo.TFMessages.Where(s => s.FormDbName == oldFormName).ToList();
            foreach (TFMessage fm in messages)
            {
                fm.FormDbName = newFormName;

                if (practice == false)
                {
                    messageRepo.SaveTFMessage(fm);
                }
            }
            output += "Set <b>" + messages.Count() + "</b> TF messages to use form <b>" + newFormName + "</b>.</br>";
            #endregion

            // TODO:  rename the field in Contributions and DbStaticItems for CurseTFFormDbName 

            output += "</br> DON'T FORGET TO UPDATE ANY PLAYERS HARDCODED INTO THE PROJECT, INCLUDING JEWDEWFAE.</br>";

            TempData["Message"] = output;
            return RedirectToAction("Index");
        }

        public ActionResult RenameItem(string oldItemName, string newItemName, bool practice)
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            string output = "<br><br>";

            if (practice == true)
            {
                output += "<b>PRACTICE MODE</b>";
            }
            else
            {
                output += "<b>LIVE EDIT MODE</b>";
            }

            oldItemName = oldItemName.Trim();
            newItemName = newItemName.Trim();

            #region static item to rename
            IDbStaticItemRepository sitemRepo = new EFDbStaticItemRepository();
            DbStaticItem sitem = sitemRepo.DbStaticItems.FirstOrDefault(s => s.dbName == oldItemName);
            if (sitem != null)
            {
                sitem.dbName = newItemName;

                if (practice == false)
                {
                    sitemRepo.SaveDbStaticItem(sitem);
                }
                output += "Renamed static item to <b>" + newItemName + "</b>.</br>";
            }
            else
            {
                output += "NO STATIC ITEM TO RENAME.</br>";
            }
            #endregion

            #region items to update
            IItemRepository itemRepo = new EFItemRepository();
            List<Item> items = itemRepo.Items.Where(s => s.dbName == oldItemName).ToList();
            foreach (Item i in items)
            {
                i.dbName = newItemName;
                if (practice == false)
                {
                    itemRepo.SaveItem(i);
                }
            }
            output += "Set <b>" + items.Count() + "</b> items to new item type <b>" + newItemName + "</b>.</br>";
            #endregion

            #region static skills exclusive to item
            IDbStaticSkillRepository sskillRepo = new EFDbStaticSkillRepository();
            List<DbStaticSkill> skills = sskillRepo.DbStaticSkills.Where(s => s.ExclusiveToItem == oldItemName).ToList();
            foreach (DbStaticSkill ss in skills)
            {
                ss.ExclusiveToItem = oldItemName;

                if (practice == false)
                {
                    sskillRepo.SaveDbStaticSkill(ss);
                }
            }
            output += "Set <b>" + skills.Count() + "</b> static skills to be exclusive to form <b>" + oldItemName + "</b>.</br>";
            #endregion


            #region effect contributions to update
            IEffectContributionRepository contRepo = new EFEffectContributionRepository();
            List<EffectContribution> contributions = contRepo.EffectContributions.Where(s => s.Skill_UniqueToItem == oldItemName).ToList();
            foreach (EffectContribution c in contributions)
            {
                c.Skill_UniqueToItem = newItemName;

                if (practice == false)
                {
                    contRepo.SaveEffectContribution(c);
                }
            }
            output += "Set <b>" + contributions.Count() + "</b> effect contributions to be exclusive to item <b>" + newItemName + "</b>.</br>";
            #endregion

            output += "</br> DON'T FORGET TO UPDATE ANY ITEMS HARDCODED INTO THE PROJECT.</br>";

            TempData["Message"] = output;
            return RedirectToAction("Index");
        }

        public ActionResult SpawnLindella()
        {
            AIProcedures.SpawnLindella();
            return View("Play");
        }

        public ActionResult SkillsFormsWithoutXMLs()
        {
            IContributionRepository contributionRepo = new EFContributionRepository();

            string output = "";

            List<Contribution> liveContributions = contributionRepo.Contributions.Where(i => i.IsLive == true && i.ProofreadingCopy == true).ToList();
            List<Contribution> noXMLs = new List<Contribution>();

            foreach (Contribution con in liveContributions)
            {

                string skilldbname = "skill_" + con.Skill_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");
                string formdbname = "form_" + con.Form_FriendlyName.Replace(" ", "_") + "_" + con.SubmitterName.Replace(" ", "_");

                string skillXMLName = Server.MapPath("~/XMLs/SkillMessages/" + skilldbname + ".xml");
                string formXMLName = Server.MapPath("~/XMLs/TFMessages/" + formdbname + ".xml");

                if (!System.IO.File.Exists(skillXMLName))
                {
                    output += "No XML file found for <span class='skill'>Skill</span> <b>" + con.Skill_FriendlyName + "</b>.  (Expected <i>" + skilldbname + ".xml</i>)</br>";
                }

                if (!System.IO.File.Exists(formXMLName))
                {
                    output += "No XML file found for <span class='form'>Form</span> <b>" + con.Form_FriendlyName + "</b>. (Expected <i>" + formdbname + ".xml</i>)</br>";
                }

                output += "<hr>";

            }


            ViewBag.Text = output;
            return View("Play");
        }

        public ActionResult ServerBalance_Forms()
        {
            List<BalancePageViewModel> output = new List<BalancePageViewModel>();

            foreach (DbStaticForm form in FormStatics.GetAllAnimateForms())
            {
                BalanceBox bbox = new BalanceBox();
                bbox.LoadBalanceBox(form);
                decimal balance = bbox.GetBalance();
                decimal absolute = bbox.GetPointTotal();
                BalancePageViewModel addme = new BalancePageViewModel
                {
                    dbName = form.dbName,
                    FriendlyName = form.FriendlyName,
                    Balance = balance,
                    AbsolutePoints = absolute
                };
                output.Add(addme);
            }

            ViewBag.Text = "Forms";
            return View("ServerBalance", output.OrderByDescending(s => s.Balance));

        }

        public ActionResult ServerBalance_Items()
        {
            List<BalancePageViewModel> output = new List<BalancePageViewModel>();

            foreach (DbStaticItem item in ItemStatics.GetAllNonPetItems())
            {
                BalanceBox bbox = new BalanceBox();
                bbox.LoadBalanceBox(item);
                decimal balance = bbox.GetBalance();
                decimal absolute = bbox.GetPointTotal();
                BalancePageViewModel addme = new BalancePageViewModel
                {
                    dbName = item.dbName,
                    FriendlyName = item.FriendlyName,
                    Balance = balance,
                    AbsolutePoints = absolute
                };
                output.Add(addme);
            }

            ViewBag.Text = "Items";
            return View("ServerBalance", output.OrderByDescending(s => s.Balance));

        }

        public ActionResult ServerBalance_Pets()
        {
            List<BalancePageViewModel> output = new List<BalancePageViewModel>();

            foreach (DbStaticItem item in ItemStatics.GetAllPetItems())
            {
                BalanceBox bbox = new BalanceBox();
                bbox.LoadBalanceBox(item);
                decimal balance = bbox.GetBalance();
                decimal absolute = bbox.GetPointTotal();
                BalancePageViewModel addme = new BalancePageViewModel
                {
                    dbName = item.dbName,
                    FriendlyName = item.FriendlyName,
                    Balance = balance,
                    AbsolutePoints = absolute
                };
                output.Add(addme);
            }

            ViewBag.Text = "Pets";
            return View("ServerBalance", output.OrderByDescending(s => s.Balance));

        }

        public ActionResult ServerBalance_Effects()
        {
            List<BalancePageViewModel> output = new List<BalancePageViewModel>();

            IEffectContributionRepository effectContributionRepo = new EFEffectContributionRepository();
            List<EffectContribution> effectsToAnalyze = effectContributionRepo.EffectContributions.Where(e => e.IsLive == true && e.ProofreadingCopy == true).ToList();

            foreach (EffectContribution effect in effectsToAnalyze)
            {
                BalanceBox bbox = new BalanceBox();
                bbox.LoadBalanceBox(effect);
                decimal balance = bbox.GetBalance__NoModifiersOrCaps();
                decimal absolute = bbox.GetPointTotal();
                BalancePageViewModel addme = new BalancePageViewModel
                {
                    dbName = effect.GetEffectDbName(),
                    FriendlyName = effect.Effect_FriendlyName,
                    Balance = balance,
                    AbsolutePoints = absolute
                };
                output.Add(addme);
            }

            ViewBag.Text = "Effects";
            return View("ServerBalance", output.OrderByDescending(s => s.Balance));

        }

        //public ActionResult WriteSkillsFromMemoryToDatabase()
        //{
        //    IDbStaticSkillRepository repo = new EFDbStaticSkillRepository();
        //    foreach (StaticSkill sskill in SkillStatics.GetOldStaticSkill)
        //    {
        //        DbStaticSkill addme = new DbStaticSkill();

        //        addme.dbName = sskill.dbName;
        //        addme.Description = sskill.Description;
        //        addme.ExclusiveToForm = sskill.ExclusiveToForm;
        //        addme.ExclusiveToItem = sskill.ExclusiveToItem;
        //        addme.FormdbName = sskill.FormdbName;
        //        addme.FriendlyName = sskill.FriendlyName;
        //        addme.LearnedAtRegion = sskill.LearnedAtRegion;
        //        addme.LearnedAtLocation = sskill.LearnedAtLocation;
        //        addme.ManaCost = sskill.ManaCost;
        //        addme.HealthDamageAmount = sskill.HealthDamageAmount;
        //        addme.TFPointsAmount = sskill.TFPointsAmount;
        //        addme.GivesEffect = sskill.GivesEffect;

        //        if (sskill.DiscoveryMessage == null || sskill.DiscoveryMessage == "")
        //        {
        //            string path = HttpContext.Server.MapPath("~/XMLs/SkillMessages/" + sskill.dbName + ".xml");
        //            StaticSkill xmlSkill = null;
        //            try
        //            {
        //                var serializer = new XmlSerializer(typeof(StaticSkill));
        //                using (var reader = XmlReader.Create(path))
        //                {
        //                    xmlSkill = (StaticSkill)serializer.Deserialize(reader);
        //                    addme.DiscoveryMessage = xmlSkill.DiscoveryMessage;
        //                }

        //            }
        //            catch
        //            {
        //                // throw a more user-friendly exception if the XML file can't be loaded for some reason or other.
        //                //throw new Exception("Failed to load XML for this spell's transformation form.  This is a server error.");
        //            }

        //        }
        //        else
        //        {
        //            addme.DiscoveryMessage = sskill.DiscoveryMessage;
        //        }

        //        if (sskill.FormdbName != null && sskill.FormdbName != "")
        //        {
        //            try
        //            {
        //                addme.MobilityType = FormStatics.GetForm.FirstOrDefault(f => f.dbName == sskill.FormdbName).MobilityType;
        //            }
        //            catch
        //            {

        //            }
        //        }
        //        else if (sskill.GivesEffect != null && sskill.GivesEffect != "")
        //        {
        //            addme.MobilityType = "curse";
        //            // addme.MobilityType = EffectStatics.GetStaticEffect.FirstOrDefault(f => f.dbName == sskill.GivesEffect);
        //        }
        //        else
        //        {
        //            addme.MobilityType = "weaken";
        //        }


        //        repo.SaveDbStaticSkill(addme);

        //    }

        //    return View("Play");
        //}

        public ActionResult WriteFormsFromMemoryToDatabase()
        {
            //IDbStaticSkillRepository repo = new EFDbStaticSkillRepository();
            IDbStaticFormRepository repo = new EFDbStaticFormRepository();
            ITFMessageRepository tfrepo = new EFTFMessageRepository();

            List<DbStaticForm> updateNeeded = new List<DbStaticForm>();

            //foreach (DbStaticForm s in repo.DbStaticForms)
            //{
            //    if (s.Description == null || s.Description == "")
            //    {
            //        string path = HttpContext.Server.MapPath("~/XMLs/TFMessages/" + s.dbName + ".xml");

            //        Form xmlForm = null;

            //        try
            //        {

            //            var serializer = new XmlSerializer(typeof(Form));
            //            using (var reader = XmlReader.Create(path))
            //            {
            //                xmlForm = (Form)serializer.Deserialize(reader);
            //            }

            //            s.Description = xmlForm.Description;
            //            updateNeeded.Add(s);

            //        }
            //        catch (Exception e)
            //        {
            //            // throw a more user-friendly exception if the XML file can't be loaded for some reason or other.
            //            //throw new Exception("Failed to load XML for this spell's transformation form.  This is a server error.");
            //        }
            //    }
            //}

            //foreach (DbStaticForm s in updateNeeded)
            //{
            //    repo.SaveDbStaticForm(s);
            //}



            //    public string dbName { get; set; }
            //public string FriendlyName { get; set; }
            //public string Description { get; set; }
            //public string TFEnergyType { get; set; }
            //public decimal TFEnergyRequired { get; set; }
            //public string Gender { get; set; }
            //public string MobilityType { get; set; }
            //public string BecomesItemDbName { get; set; }
            //public string PortraitUrl { get; set; }

            //foreach (Form f in FormStatics.GetForm2)
            //{
            //    DbStaticForm sf = new DbStaticForm
            //    {
            //        dbName = f.dbName,
            //        FriendlyName = f.FriendlyName,
            //        Description = f.Description,
            //        TFEnergyType = f.TFEnergyType,
            //        TFEnergyRequired = f.TFEnergyRequired,
            //        Gender = f.Gender,
            //        MobilityType = f.MobilityType,
            //        BecomesItemDbName = f.BecomesItemDbName,
            //        PortraitUrl = f.PortraitUrl,
            //        IsUnique = false,

            //        HealthBonusPercent = f.FormBuffs.FromForm_HealthBonusPercent,
            //        ManaBonusPercent = f.FormBuffs.FromForm_ManaBonusPercent,
            //        ExtraSkillCriticalPercent = f.FormBuffs.FromForm_ExtraSkillCriticalPercent,
            //        HealthRecoveryPerUpdate = f.FormBuffs.FromForm_HealthRecoveryPerUpdate,
            //        ManaRecoveryPerUpdate = f.FormBuffs.FromForm_ManaRecoveryPerUpdate,
            //        SneakPercent = f.FormBuffs.FromForm_SneakPercent,
            //        EvasionPercent = f.FormBuffs.FromForm_EvasionPercent,
            //        EvasionNegationPercent = f.FormBuffs.FromForm_EvasionNegationPercent,
            //        MeditationExtraMana = f.FormBuffs.FromForm_MeditationExtraMana,
            //        CleanseExtraHealth = f.FormBuffs.FromForm_CleanseExtraHealth,
            //        MoveActionPointDiscount = f.FormBuffs.FromForm_MoveActionPointDiscount,
            //        SpellExtraTFEnergyPercent = f.FormBuffs.FromForm_SpellExtraTFEnergyPercent,
            //        SpellExtraHealthDamagePercent = f.FormBuffs.FromForm_SpellExtraHealthDamagePercent,
            //        CleanseExtraTFEnergyRemovalPercent = f.FormBuffs.FromForm_CleanseExtraTFEnergyRemovalPercent,
            //        SpellMisfireChanceReduction = f.FormBuffs.FromForm_SpellMisfireChanceReduction,
            //        SpellHealthDamageResistance = f.FormBuffs.FromForm_SpellHealthDamageResistance,
            //        SpellTFEnergyDamageResistance = f.FormBuffs.FromForm_SpellTFEnergyDamageResistance,
            //        ExtraInventorySpace = f.FormBuffs.FromForm_ExtraInventorySpace,

            //    };

            //    repo.SaveDbStaticForm(sf);

            // load the TF Messages from the appropriate XML file if need be
            //if (f.TFMessage_20_Percent_1st == null && f.TFMessage_20_Percent_1st_M == null && f.TFMessage_20_Percent_1st_F == null)
            //{
            //    Form copy = f;

            //    try
            //    {
            //   copy = TFEnergyProcedures.LoadTFMessagesFromXML(f);

            //        TFMessage tfm = new TFMessage
            //        {
            //            FormDbName = copy.dbName,
            //            TFMessage_20_Percent_1st = copy.TFMessage_20_Percent_1st,
            //            TFMessage_40_Percent_1st = copy.TFMessage_40_Percent_1st,
            //            TFMessage_60_Percent_1st = copy.TFMessage_60_Percent_1st,
            //            TFMessage_80_Percent_1st = copy.TFMessage_80_Percent_1st,
            //            TFMessage_100_Percent_1st = copy.TFMessage_100_Percent_1st,
            //            TFMessage_Completed_1st = copy.TFMessage_Completed_1st,

            //            TFMessage_20_Percent_1st_M = copy.TFMessage_20_Percent_1st_M,
            //            TFMessage_40_Percent_1st_M = copy.TFMessage_40_Percent_1st_M,
            //            TFMessage_60_Percent_1st_M = copy.TFMessage_60_Percent_1st_M,
            //            TFMessage_80_Percent_1st_M = copy.TFMessage_80_Percent_1st_M,
            //            TFMessage_100_Percent_1st_M = copy.TFMessage_100_Percent_1st_M,
            //            TFMessage_Completed_1st_M = copy.TFMessage_Completed_1st_M,

            //            TFMessage_20_Percent_1st_F = copy.TFMessage_20_Percent_1st_F,
            //            TFMessage_40_Percent_1st_F = copy.TFMessage_40_Percent_1st_F,
            //            TFMessage_60_Percent_1st_F = copy.TFMessage_60_Percent_1st_F,
            //            TFMessage_80_Percent_1st_F = copy.TFMessage_80_Percent_1st_F,
            //            TFMessage_100_Percent_1st_F = copy.TFMessage_100_Percent_1st_F,
            //            TFMessage_Completed_1st_F = copy.TFMessage_Completed_1st_F,

            //            TFMessage_20_Percent_3rd = copy.TFMessage_20_Percent_3rd,
            //            TFMessage_40_Percent_3rd = copy.TFMessage_40_Percent_3rd,
            //            TFMessage_60_Percent_3rd = copy.TFMessage_60_Percent_3rd,
            //            TFMessage_80_Percent_3rd = copy.TFMessage_80_Percent_3rd,
            //            TFMessage_100_Percent_3rd = copy.TFMessage_100_Percent_3rd,
            //            TFMessage_Completed_3rd = copy.TFMessage_Completed_3rd,

            //            TFMessage_20_Percent_3rd_M = copy.TFMessage_20_Percent_3rd_M,
            //            TFMessage_40_Percent_3rd_M = copy.TFMessage_40_Percent_3rd_M,
            //            TFMessage_60_Percent_3rd_M = copy.TFMessage_60_Percent_3rd_M,
            //            TFMessage_80_Percent_3rd_M = copy.TFMessage_80_Percent_3rd_M,
            //            TFMessage_100_Percent_3rd_M = copy.TFMessage_100_Percent_3rd_M,
            //            TFMessage_Completed_3rd_M = copy.TFMessage_Completed_3rd_M,

            //            TFMessage_20_Percent_3rd_F = copy.TFMessage_20_Percent_3rd_F,
            //            TFMessage_40_Percent_3rd_F = copy.TFMessage_40_Percent_3rd_F,
            //            TFMessage_60_Percent_3rd_F = copy.TFMessage_60_Percent_3rd_F,
            //            TFMessage_80_Percent_3rd_F = copy.TFMessage_80_Percent_3rd_F,
            //            TFMessage_100_Percent_3rd_F = copy.TFMessage_100_Percent_3rd_F,
            //            TFMessage_Completed_3rd_F = copy.TFMessage_Completed_3rd_F,

            //        };
            //        tfrepo.SaveTFMessage(tfm);

            //    }
            //    catch
            //    {

            //    }

            //}
            //else
            //{
            //    TFMessage tfm = new TFMessage
            //    {
            //        FormDbName = f.dbName,
            //        TFMessage_20_Percent_1st = f.TFMessage_20_Percent_1st,
            //        TFMessage_40_Percent_1st = f.TFMessage_40_Percent_1st,
            //        TFMessage_60_Percent_1st = f.TFMessage_60_Percent_1st,
            //        TFMessage_80_Percent_1st = f.TFMessage_80_Percent_1st,
            //        TFMessage_100_Percent_1st = f.TFMessage_100_Percent_1st,
            //        TFMessage_Completed_1st = f.TFMessage_Completed_1st,

            //        TFMessage_20_Percent_1st_M = f.TFMessage_20_Percent_1st_M,
            //        TFMessage_40_Percent_1st_M = f.TFMessage_40_Percent_1st_M,
            //        TFMessage_60_Percent_1st_M = f.TFMessage_60_Percent_1st_M,
            //        TFMessage_80_Percent_1st_M = f.TFMessage_80_Percent_1st_M,
            //        TFMessage_100_Percent_1st_M = f.TFMessage_100_Percent_1st_M,
            //        TFMessage_Completed_1st_M = f.TFMessage_Completed_1st_M,

            //        TFMessage_20_Percent_1st_F = f.TFMessage_20_Percent_1st_F,
            //        TFMessage_40_Percent_1st_F = f.TFMessage_40_Percent_1st_F,
            //        TFMessage_60_Percent_1st_F = f.TFMessage_60_Percent_1st_F,
            //        TFMessage_80_Percent_1st_F = f.TFMessage_80_Percent_1st_F,
            //        TFMessage_100_Percent_1st_F = f.TFMessage_100_Percent_1st_F,
            //        TFMessage_Completed_1st_F = f.TFMessage_Completed_1st_F,

            //        TFMessage_20_Percent_3rd = f.TFMessage_20_Percent_3rd,
            //        TFMessage_40_Percent_3rd = f.TFMessage_40_Percent_3rd,
            //        TFMessage_60_Percent_3rd = f.TFMessage_60_Percent_3rd,
            //        TFMessage_80_Percent_3rd = f.TFMessage_80_Percent_3rd,
            //        TFMessage_100_Percent_3rd = f.TFMessage_100_Percent_3rd,
            //        TFMessage_Completed_3rd = f.TFMessage_Completed_3rd,

            //        TFMessage_20_Percent_3rd_M = f.TFMessage_20_Percent_3rd_M,
            //        TFMessage_40_Percent_3rd_M = f.TFMessage_40_Percent_3rd_M,
            //        TFMessage_60_Percent_3rd_M = f.TFMessage_60_Percent_3rd_M,
            //        TFMessage_80_Percent_3rd_M = f.TFMessage_80_Percent_3rd_M,
            //        TFMessage_100_Percent_3rd_M = f.TFMessage_100_Percent_3rd_M,
            //        TFMessage_Completed_3rd_M = f.TFMessage_Completed_3rd_M,

            //        TFMessage_20_Percent_3rd_F = f.TFMessage_20_Percent_3rd_F,
            //        TFMessage_40_Percent_3rd_F = f.TFMessage_40_Percent_3rd_F,
            //        TFMessage_60_Percent_3rd_F = f.TFMessage_60_Percent_3rd_F,
            //        TFMessage_80_Percent_3rd_F = f.TFMessage_80_Percent_3rd_F,
            //        TFMessage_100_Percent_3rd_F = f.TFMessage_100_Percent_3rd_F,
            //        TFMessage_Completed_3rd_F = f.TFMessage_Completed_3rd_F,

            //    };

            //    tfrepo.SaveTFMessage(tfm);
            //}



            //}

            return View("Play");
        }

        //public ActionResult WriteItemsFromMemoryToDatabase()
        //{

        //    IDbStaticItemRepository repo = new EFDbStaticItemRepository();
        //    foreach (StaticItem i in ItemStatics.GetStaticItem)
        //    {

        //        DbStaticItem ni = new DbStaticItem
        //        {
        //            dbName = i.dbName,
        //            FriendlyName = i.FriendlyName,
        //            Description = i.Description,
        //            PortraitUrl = i.PortraitUrl,
        //            MoneyValue = i.MoneyValue,
        //            ItemType = i.ItemType,
        //            UseCooldown = i.UseCooldown,
        //            Findable = i.Findable,
        //            FindWeight = i.FindWeight,
        //            GivesEffect = i.GivesEffect,
        //            IsUnique = false,

        //            HealthBonusPercent = i.HealthBonusPercent,
        //            ManaBonusPercent = i.ManaBonusPercent,
        //            ExtraSkillCriticalPercent = i.ExtraSkillCriticalPercent,
        //            HealthRecoveryPerUpdate = i.HealthRecoveryPerUpdate,
        //            ManaRecoveryPerUpdate = i.ManaRecoveryPerUpdate,
        //            SneakPercent = i.SneakPercent,
        //            EvasionPercent = i.EvasionPercent,
        //            EvasionNegationPercent = i.EvasionNegationPercent,
        //            MeditationExtraMana = i.MeditationExtraMana,
        //            CleanseExtraHealth = i.CleanseExtraHealth,
        //            MoveActionPointDiscount = i.MoveActionPointDiscount,
        //            SpellExtraTFEnergyPercent = i.SpellExtraTFEnergyPercent,
        //            SpellExtraHealthDamagePercent = i.SpellExtraHealthDamagePercent,
        //            CleanseExtraTFEnergyRemovalPercent = i.CleanseExtraTFEnergyRemovalPercent,
        //            SpellMisfireChanceReduction = i.SpellMisfireChanceReduction,
        //            SpellHealthDamageResistance = i.SpellHealthDamageResistance,
        //            SpellTFEnergyDamageResistance = i.SpellTFEnergyDamageResistance,
        //            ExtraInventorySpace = i.ExtraInventorySpace,

        //        };
        //        repo.SaveDbStaticItem(ni);
        //    }

        //    return View("Play");
        //}

        //public ActionResult WriteEffectsFromMemoryToDatabase()
        //{
        //    IDbStaticEffectRepository effectRepo = new EFDbStaticEffectRepository();

        //    foreach (StaticEffect ramEffect in EffectStatics.GetStaticEffect)
        //    {
        //        DbStaticEffect effect = new DbStaticEffect
        //        {
        //            dbName = ramEffect.dbName,
        //            FriendlyName = ramEffect.FriendlyName,
        //            AvailableAtLevel = ramEffect.AvailableAtLevel,
        //            Description = ramEffect.Description,
        //            PreRequesite = ramEffect.PreRequesite,
        //            isLevelUpPerk = ramEffect.isLevelUpPerk,
        //            Cooldown = ramEffect.Cooldown,
        //            Duration = ramEffect.Duration,
        //            ObtainedAtLocation = ramEffect.ObtainedAtLocation,

        //            AttackerWhenHit = ramEffect.AttackerWhenHit,
        //            AttackerWhenHit_M = ramEffect.AttackerWhenHit_M,
        //            AttackerWhenHit_F = ramEffect.AttackerWhenHit_F,

        //            MessageWhenHit = ramEffect.MessageWhenHit,
        //            MessageWhenHit_M = ramEffect.MessageWhenHit_M,
        //            MessageWhenHit_F = ramEffect.MessageWhenHit_F,

        //            HealthBonusPercent = ramEffect.HealthBonusPercent,
        //            ManaBonusPercent = ramEffect.ManaBonusPercent,
        //            ExtraSkillCriticalPercent = ramEffect.ExtraSkillCriticalPercent,
        //            HealthRecoveryPerUpdate = ramEffect.HealthRecoveryPerUpdate,
        //            ManaRecoveryPerUpdate = ramEffect.ManaRecoveryPerUpdate,
        //            SneakPercent = ramEffect.SneakPercent,
        //            EvasionPercent = ramEffect.EvasionPercent,
        //            EvasionNegationPercent = ramEffect.EvasionNegationPercent,
        //            MeditationExtraMana = ramEffect.MeditationExtraMana,
        //            CleanseExtraHealth = ramEffect.CleanseExtraHealth,
        //            MoveActionPointDiscount = ramEffect.MoveActionPointDiscount,
        //            SpellExtraTFEnergyPercent = ramEffect.SpellExtraTFEnergyPercent,
        //            SpellExtraHealthDamagePercent = ramEffect.SpellExtraHealthDamagePercent,
        //            CleanseExtraTFEnergyRemovalPercent = ramEffect.CleanseExtraTFEnergyRemovalPercent,
        //            SpellMisfireChanceReduction = ramEffect.SpellMisfireChanceReduction,
        //            SpellHealthDamageResistance = ramEffect.SpellHealthDamageResistance,
        //            SpellTFEnergyDamageResistance = ramEffect.SpellTFEnergyDamageResistance,
        //            ExtraInventorySpace = ramEffect.ExtraInventorySpace,

        //        };

        //        effectRepo.SaveDbStaticEffect(effect);
        //    }

        //    return View("Play");
        //}

        public ActionResult ViewServerLog(int turn)
        {
            IServerLogRepository serverLogRepo = new EFServerLogRepository();
            ServerLog log = serverLogRepo.ServerLogs.FirstOrDefault(t => t.TurnNumber == turn);
            return View(log);
        }

        public ActionResult ViewUpdateLogs()
        {
            IServerLogRepository serverLogRepo = new EFServerLogRepository();
            return View(serverLogRepo.ServerLogs);
        }

        public ActionResult FaeList()
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IJewdewfaeEncounterRepository repo = new EFJewdewfaeEncounterRepository();
            IEnumerable<JewdewfaeEncounter> encounters = repo.JewdewfaeEncounters.ToList();

            return View(encounters);

        }

        public ActionResult WriteFae(int id)
        {
            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            IJewdewfaeEncounterRepository repo = new EFJewdewfaeEncounterRepository();
            JewdewfaeEncounter encounter = repo.JewdewfaeEncounters.FirstOrDefault(e => e.Id == id);

            if (encounter == null)
            {
                encounter = new JewdewfaeEncounter
                {
                    IsLive = false,
                };
            }

            if (encounter.IntroText == null)
            {
                encounter.IntroText = "";
            }

            if (encounter.FailureText == null)
            {
                encounter.FailureText = "";
            }

            if (encounter.CorrectFormText == null)
            {
                encounter.CorrectFormText = "";
            }


            ViewBag.IntroText = encounter.IntroText.Replace("[", "<").Replace("]", ">");
            ViewBag.FailureText = encounter.FailureText.Replace("[", "<").Replace("]", ">");
            ViewBag.CorrectFormText = encounter.CorrectFormText.Replace("[", "<").Replace("]", ">");

            ViewBag.LocationExists = "";
            ViewBag.FormExists = "";

            Location loc = LocationsStatics.LocationList.GetLocation.FirstOrDefault(l => l.dbName == encounter.dbLocationName);

            if (loc == null)
            {
                ViewBag.LocationExists = "<span class='bad'>LOCATION " + encounter.dbLocationName + " DOES NOT EXIST.</span>";
            }
            else
            {
                ViewBag.LocationExists = "<span class='good'>LOCATION " + encounter.dbLocationName + " EXISTs.</span>";
            }

            DbStaticForm form = FormStatics.GetForm(encounter.RequiredForm);

            if (form == null)
            {
                ViewBag.FormExists = "<span class='bad'>FORM " + encounter.RequiredForm + " DOES NOT EXIST.</span>";
            }
            else
            {
                ViewBag.FormExists = "<span class='good'>FORM " + encounter.RequiredForm + " EXISTs.</span>";
            }

            return View(encounter);
        }

        public ActionResult WriteFaeSend(JewdewfaeEncounter input)
        {
            // assert only admins can do this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            input.RequiredForm = input.RequiredForm.Trim();
            input.dbLocationName = input.dbLocationName.Trim();

            IJewdewfaeEncounterRepository repo = new EFJewdewfaeEncounterRepository();

            JewdewfaeEncounter encounter = repo.JewdewfaeEncounters.FirstOrDefault(f => f.Id == input.Id);

            if (encounter == null)
            {
                encounter = new JewdewfaeEncounter();
            }

            //  encounter.Id = input.Id;
            encounter.dbLocationName = input.dbLocationName;
            encounter.RequiredForm = input.RequiredForm;
            encounter.IsLive = input.IsLive;
            encounter.FailureText = input.FailureText;
            encounter.IntroText = input.IntroText;
            encounter.CorrectFormText = input.CorrectFormText;

            repo.SaveJewdewfaeEncounter(encounter);

            return RedirectToAction("FaeList", "PvPAdmin");


        }

        public ActionResult WriteFaeEncounter()
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }


            FairyChallengeBag output = new FairyChallengeBag();

            try
            {
                // load data from the xml
                string filename = System.Web.HttpContext.Current.Server.MapPath("~/XMLs/FairyChallengeText/fae_temp.xml");
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(FairyChallengeBag));
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                output = (FairyChallengeBag)reader.Deserialize(file);
            }
            catch
            {

            }


            return View(output);
        }

        public ActionResult LoadSpecificEncounter(string filename)
        {

            FairyChallengeBag output = new FairyChallengeBag();

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            // TODO:  finish this!
            return View("WriteFaeEncounter", output);
        }

        [ValidateInput(false)]
        public ActionResult WriteFaeEncounterSend(FairyChallengeBag input)
        {

            // assert only admins can view this
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return View("Play", "PvP");
            }

            string path = Server.MapPath("~/XMLs/FairyChallengeText/");

            // input.title = "Serialization Overview";
            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(FairyChallengeBag));

            System.IO.StreamWriter file = new System.IO.StreamWriter(
                path + "fae_temp.xml");
            writer.Serialize(file, input);
            file.Close();

            ViewBag.Result = "done";

            ViewBag.IntroText = input.IntroText.Replace("[", "<").Replace("]", ">");
            ViewBag.FailureText = input.FailureText.Replace("[", "<").Replace("]", ">");
            ViewBag.CorrectFormText = input.CorrectFormText.Replace("[", "<").Replace("]", ">");



            return View("WriteFaeEncounter", input);
        }

        [Authorize]
        public ActionResult ResetAllPlayersWithIPAddress(string address)
        {

            // assert only admin can view this
            bool iAmModerator = User.IsInRole(PvPStatics.Permissions_Moderator);
            if (iAmModerator == false)
            {
                ViewBag.Message = "You aren't allowed to do this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }


            IPlayerRepository playerRepo = new EFPlayerRepository();
            List<Player> players = playerRepo.Players.Where(p => p.IpAddress == address).ToList();
            foreach (Player p in players)
            {
                p.IpAddress = "reset";
                playerRepo.SavePlayer(p);
                PlayerLogProcedures.AddPlayerLog(p.Id, "<b class='good'>Server notice:  Your IP address has been reset.</b>", true);
            }

            return View("Play");

        }

        [Authorize]
        public ActionResult ToggleBanOnGlobalChat(int id)
        {

            // assert only admin can view this
            bool iAmModerator = User.IsInRole(PvPStatics.Permissions_Moderator);
            if (iAmModerator == false)
            {
                ViewBag.Message = "You aren't allowed to do this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }


            IPlayerRepository playerRepo = new EFPlayerRepository();
            Player bannedPlayer = playerRepo.Players.FirstOrDefault(p => p.Id == id);

            if (bannedPlayer.IsBannedFromGlobalChat == true)
            {

                bannedPlayer.IsBannedFromGlobalChat = false;
                TempData["Result"] = "Ban has been LIFTED.";
            }
            else
            {
                bannedPlayer.IsBannedFromGlobalChat = true;
                TempData["Result"] = "Player has been banned from global chat.";
            }
            playerRepo.SavePlayer(bannedPlayer);

            return RedirectToAction("Play", "PvP");

        }

        public ActionResult Scratchpad()
        {
            return View();
        }

        public ActionResult AuditDonators()
        {

            // assert only admin can view this
            bool iAmAdmin = User.IsInRole(PvPStatics.Permissions_Admin);
            if (iAmAdmin == false)
            {
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }

            string output = "";

            IDonatorRepository repo = new EFDonatorRepository();
            IPlayerRepository playerRepo = new EFPlayerRepository();

            foreach (Donator d in repo.Donators.ToList())
            {
                Player player = playerRepo.Players.FirstOrDefault(p => p.MembershipId == d.OwnerMembershipId);

                if (player != null && player.DonatorLevel > 0)
                {
                    output += "Looking at " + player.GetFullName() + ".";

                    if (player.DonatorLevel > d.Tier)
                    {
                        player.DonatorLevel = d.Tier;
                        output += "  Knocking down to tier " + d.Tier + ".  </br>";
                        string message = "<span class='bad'>MESSAGE FROM SERVER:  Your Patreon donation tier has been changed to " + d.Tier + ".  If you feel this is in error, please send a private message to Judoo on the forums or through Patreon.  Thank you for your past support!</span>";
                        PlayerLogProcedures.AddPlayerLog(player.Id, message, true);
                    }
                    else
                    {
                        output += "  Okay at tier " + d.Tier + ".  </br>";
                        string message = "<span class='good'>MESSAGE FROM SERVER:  Your Patreon donation has been processed and remains at Tier " + d.Tier + ".  Thank you for your support!</span>";
                        PlayerLogProcedures.AddPlayerLog(player.Id, message, true);
                    }

                    playerRepo.SavePlayer(player);

                }



            }

            TempData["Result"] = output;
            return RedirectToAction("Play", "PvP");
        }

        public ActionResult Killswitch()
        {
            if (User.IsInRole(PvPStatics.Permissions_Killswitcher) == true)
            {
                IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();
                PvPWorldStat stats = repo.PvPWorldStats.FirstOrDefault(p => p.Id != -1);
                stats.LastUpdateTimestamp = stats.LastUpdateTimestamp.AddDays(1);
                repo.SavePvPWorldStat(stats);

                IPlayerLogRepository logRepo = new EFPlayerLogRepository();
                Player me = PlayerProcedures.GetPlayerFromMembership("69");
                PlayerLog newlog = new PlayerLog
                {
                    IsImportant = true,
                    Message = "<span class='bad'><b>" + User.Identity.Name + " activated the game pause killswitch.</b></span>",
                    PlayerId = me.Id,
                    Timestamp = DateTime.UtcNow,
                };
                logRepo.SavePlayerLog(newlog);

            }
            return RedirectToAction("Play", "PvP");
        }

        public ActionResult KillswitchRestore()
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true)
            {
                IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();
                PvPWorldStat stats = repo.PvPWorldStats.FirstOrDefault(p => p.Id != -1);
                stats.LastUpdateTimestamp = DateTime.UtcNow;
                repo.SavePvPWorldStat(stats);
            }
            return RedirectToAction("Play", "PvP");
        }

        [Authorize]
        public ActionResult FindMissingThumbnails()
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "You aren't allowed to do this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }
        }

        [Authorize]
        public ActionResult ViewPlayerItems(int id)
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true || User.IsInRole(PvPStatics.Permissions_Moderator) == true)
            {
                ViewBag.playeritems = ItemProcedures.GetAllPlayerItems(id).OrderByDescending(i => i.dbItem.Level);
                ViewBag.player = PlayerProcedures.GetPlayerFormViewModel(id);
                return View();
            }
            else
            {
                ViewBag.Message = "You aren't allowed to do this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }
        }

        [Authorize]
        public ActionResult ViewItemTransferLog(int id)
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true || User.IsInRole(PvPStatics.Permissions_Moderator) == true)
            {
                ViewBag.item = ItemProcedures.GetItemViewModel(id);
                ViewBag.transferlog = ItemTransferLogProcedures.GetItemTransferLog(id);
                return View();
            }
            else
            {
                ViewBag.Message = "You aren't allowed to do this.";
                return View("~/Views/PvP/PvPAdmin.cshtml");
            }
        }

        [Authorize]
        public ActionResult RenamePlayer(int id)
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true)
            {
                PlayerNameViewModel output = new PlayerNameViewModel();

                if (id != -1)
                {
                    PlayerFormViewModel pm = PlayerProcedures.GetPlayerFormViewModel(id);
                    output.Id = id;
                    output.NewFirstName = pm.Player.FirstName;
                    output.NewLastName = pm.Player.LastName;
                    output.NewForm = pm.Player.Form;
                }

                return View(output);
            }
            else
            {
                return View("Play", "PvP");
            }
        }

        [Authorize]
        public ActionResult RenamePlayerSend(PlayerNameViewModel input)
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == true)
            {

                PvPWorldStat stats = PvPWorldStatProcedures.GetWorldStats();
                if (PvPStatics.ChaosMode == false && stats.TestServer == false)
                {
                    return RedirectToAction("Play", "PvP");
                }

                IPlayerRepository playerRepo = new EFPlayerRepository();
                Player player = playerRepo.Players.FirstOrDefault(p => p.Id == input.Id);

                string origFirstName = player.FirstName;
                string origLastName = player.LastName;

                if (input.NewFirstName != null && input.NewFirstName.Length > 0)
                {
                    player.FirstName = input.NewFirstName;
                }

                if (input.NewLastName != null && input.NewLastName.Length > 0)
                {
                    player.LastName = input.NewLastName;
                }

                if (input.NewForm != null && input.NewForm.Length > 0)
                {
                    IDbStaticFormRepository staticFormRepo = new EFDbStaticFormRepository();
                    DbStaticForm form = staticFormRepo.DbStaticForms.FirstOrDefault(f => f.dbName == input.NewForm);

                    if (form != null && form.MobilityType == "full")
                    {
                        player.Form = form.dbName;
                        player.Health = 99999;
                        player.MaxHealth = 99999;
                    }

                    if (form.MobilityType == "full" && player.Mobility != "full")
                    {
                        IItemRepository itemRepo = new EFItemRepository();
                        Item item = itemRepo.Items.FirstOrDefault(i => i.VictimName == origFirstName + " " + origLastName);
                        player.Mobility = "full";
                        itemRepo.DeleteItem(item.Id);
                    }

                }

                playerRepo.SavePlayer(player);

                if (player.Mobility != "full")
                {
                    IItemRepository itemRepo = new EFItemRepository();
                    Item item = itemRepo.Items.FirstOrDefault(i => i.VictimName == origFirstName + " " + origLastName);
                    item.VictimName = input.NewFirstName + " " + input.NewLastName;
                    itemRepo.SaveItem(item);
                }

                // if Donna, give player her spells
                if (player.Form == BossProcedures_Donna.DonnaDbForm)
                {
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Donna.Spell1);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Donna.Spell2);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Donna.Spell3);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Donna.Spell4);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Donna.Spell5);
                }

                // if Valentine, give player his spells
                else if (player.Form == BossProcedures_Valentine.ValentineFormDbName)
                {
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.BloodyCurseSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.ValentinesPresenceSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.SwordSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.DayVampireFemaleSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.NightVampireFemaleSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.DayVampireMaleSpell);
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Valentine.NightVampireMaleSpell);
                }

                // if plague mother, give player her spells
                else if (player.Form == BossProcedures_BimboBoss.BossFormDbName)
                {
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_BimboBoss.RegularTFSpellDbName);
                }

                // if master rat thief, give player her spells
                else if (player.Form == BossProcedures_Thieves.FemaleBossFormDbName)
                {
                    SkillProcedures.GiveSkillToPlayer(player.Id, BossProcedures_Thieves.GoldenTrophySpellDbName);
                }

                // mouse sisters have no unique spells yet.


            }
            else
            {
                return RedirectToAction("Play", "PvP");
            }

            TempData["Result"] = "Yay!";
            return RedirectToAction("Play", "PvP");
        }

        [Authorize]
        public ActionResult ModDeleteClassified(int id)
        {
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false && User.IsInRole(PvPStatics.Permissions_Moderator) == false)
            {
                return View("Play", "PvP");
            }

            RPClassifiedAdsProcedures.DeleteAd(id);

            TempData["Result"] = "Delete successful.";
            return RedirectToAction("RecentRPClassifieds", "Info");
        }

        [Authorize]
        public ActionResult FastInanimateMe()
        {
            string myMembershipId = User.Identity.GetUserId();
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            PvPWorldStat stats = PvPWorldStatProcedures.GetWorldStats();
            if (stats.TestServer == false && PvPStatics.ChaosMode == false)
            {
                TempData["Error"] = "Cant' do this in live non-chaos server.";
                return RedirectToAction("Play", "PvP");
            }

            IPlayerRepository playerRepo = new EFPlayerRepository();
            IItemRepository itemRepo = new EFItemRepository();

            Player me = playerRepo.Players.FirstOrDefault(p => p.MembershipId == myMembershipId);
            me.Mobility = "inanimate";
            me.Form = "form_Flirty_Three-Tiered_Skirt_Martiandawn";
            playerRepo.SavePlayer(me);

            // delete old item you are if you are one
            Item possibleMeItem = itemRepo.Items.FirstOrDefault(i => i.VictimName == me.FirstName + " " + me.LastName); // DO NOT use GetFullName.  It will break things here.
            if (possibleMeItem != null) { 
                itemRepo.DeleteItem(possibleMeItem.Id);
            }

            Item newMeItem = new Item{
                dbLocationName = me.dbLocationName,
                dbName = "item_Flirty_Three-Tiered_Skirt_Martiandawn",
                VictimName = me.FirstName + " " + me.LastName, // DO NOT use GetFullName.  It will break things here.
                Nickname = me.Nickname,
                TimeDropped = DateTime.UtcNow,
                OwnerId = -1,
                IsEquipped = false,
                Level = me.Level,
                LastSouledTimestamp = DateTime.UtcNow.AddYears(-1),
            };

            itemRepo.SaveItem(newMeItem);

            TempData["Result"] = "You are inanimate.";
            return RedirectToAction("Play", "PvP");

        }

        [Authorize]
        public ActionResult FastPetMe()
        {
            string myMembershipId = User.Identity.GetUserId();
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            PvPWorldStat stats = PvPWorldStatProcedures.GetWorldStats();
            if (stats.TestServer == false && PvPStatics.ChaosMode == false)
            {
                TempData["Error"] = "Cant' do this in live non-chaos server.";
                return RedirectToAction("Play", "PvP");
            }

            IPlayerRepository playerRepo = new EFPlayerRepository();
            IItemRepository itemRepo = new EFItemRepository();

            Player me = playerRepo.Players.FirstOrDefault(p => p.MembershipId == myMembershipId);
            me.Mobility = "animal";
            me.Form = "form_Cuddly_Pocket_Goo_Girl_GooGirl";
            playerRepo.SavePlayer(me);

            // delete old item you are if you are one
            Item possibleMeItem = itemRepo.Items.FirstOrDefault(i => i.VictimName == me.FirstName + " " + me.LastName);
            if (possibleMeItem != null)
            {
                itemRepo.DeleteItem(possibleMeItem.Id);
            }

            Item newMeItem = new Item
            {
                dbLocationName = me.dbLocationName,
                dbName = "animal_Cuddly_Pocket_Goo_Girl_GooGirl",
                VictimName = me.FirstName + " " + me.LastName,
                Nickname = me.Nickname,
                TimeDropped = DateTime.UtcNow,
                OwnerId = -1,
                IsEquipped = false,
                Level = me.Level,
                LastSouledTimestamp = DateTime.UtcNow.AddYears(-1),
            };

            itemRepo.SaveItem(newMeItem);

            TempData["Result"] = "You are now a pet.";
            return RedirectToAction("Play", "PvP");

        }

        [Authorize]
        public ActionResult FastAnimateMe()
        {
            string myMembershipId = User.Identity.GetUserId();
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            PvPWorldStat stats = PvPWorldStatProcedures.GetWorldStats();
            if (stats.TestServer == false && PvPStatics.ChaosMode == false)
            {
                TempData["Error"] = "Cant' do this in live non-chaos server.";
                return RedirectToAction("Play", "PvP");
            }

            IPlayerRepository playerRepo = new EFPlayerRepository();
            IItemRepository itemRepo = new EFItemRepository();

            Player me = playerRepo.Players.FirstOrDefault(p => p.MembershipId == myMembershipId);
            me.Mobility = "full";
            me.Form = me.OriginalForm;
            playerRepo.SavePlayer(me);

            // delete old item you are if you are one
            Item possibleMeItem = itemRepo.Items.FirstOrDefault(i => i.VictimName == me.FirstName + " " + me.LastName);
            if (possibleMeItem != null)
            {
                itemRepo.DeleteItem(possibleMeItem.Id);
            }


            TempData["Result"] = "You are now fully animate.";
            return RedirectToAction("Play", "PvP");

        }

        [Authorize]
        public ActionResult FastGiveTPScroll()
        {
            string myMembershipId = User.Identity.GetUserId();
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            IPvPWorldStatRepository repo = new EFPvPWorldStatRepository();
            PvPWorldStat stat = repo.PvPWorldStats.First();

            bool test = stat.TestServer;

            if (PvPStatics.ChaosMode==false && test == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            Player me = PlayerProcedures.GetPlayerFromMembership(myMembershipId);
           
            IItemRepository itemRepo = new EFItemRepository();


            Item scroll = new Item
            {
                dbName = "item_consumeable_teleportation_scroll",
                OwnerId = me.Id,
                dbLocationName = "",
                EquippedThisTurn = false,
                LastSouledTimestamp = DateTime.UtcNow,
                VictimName = "",
                Level = 0,
                TimeDropped = DateTime.UtcNow,
            };
            itemRepo.SaveItem(scroll);


            TempData["Result"] = "You are now fully animate.";
            return RedirectToAction("Play", "PvP");

        }

        [Authorize]
        public ActionResult AssignLeadersBadges()
        {

            string myMembershipId = User.Identity.GetUserId();
            if (User.IsInRole(PvPStatics.Permissions_Admin) == false)
            {
                return RedirectToAction("Play", "PvP");
            }

            if (PvPStatics.ChaosMode==true)
            {
                TempData["Error"] = "Can't do this in chaos mode.";
                return RedirectToAction("Play", "PvP");
            }

            if (PvPWorldStatProcedures.GetWorldTurnNumber() != PvPStatics.RoundDuration)
            {
                TempData["Error"] = "Turn must be the final turn of the round for this to work.";
                return RedirectToAction("Play", "PvP");
            }

            string output = StatsProcedures.AssignLeadersBadges();

            TempData["Result"] = output;
            return RedirectToAction("Play", "PvP");
        }



    }


}