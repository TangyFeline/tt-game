﻿using System;
using TT.Domain.Statics;
using TT.Domain.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TT.Domain.Commands.Player;
using TT.Domain.Procedures;

namespace TT.Domain.Models
{
    public class Player
    {
        public int Id {get; set;}
        [Index("IX_MembershipIdAndCovenant", 1)]
        [Index("IX_MembershipIdAndInPvP", 1)]
        [Index]
        [StringLength(128)]
        public string MembershipId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string dbLocationName { get; set; }
        public string Form { get; set; }
        public decimal Health { get; set; }
        public decimal MaxHealth { get; set; }
        public decimal Mana { get; set; }
        public decimal MaxMana { get; set; }
        public decimal ActionPoints { get; set; }
        public decimal ActionPoints_Refill { get; set; }
        public decimal ResistanceModifier { get; set; }
        public string Gender { get; set; }
        public string Mobility { get; set; }

        public int BotId { get; set; }
        public int IsItemId { get; set; }
        public int IsPetToId { get; set; }
        public bool MindControlIsActive { get; set; }

        public decimal XP { get; set; }
        public int Level { get; set; }
        public int TimesAttackingThisUpdate { get; set; }
        public string IpAddress { get; set; }
        public DateTime LastActionTimestamp { get; set; }
        public DateTime LastCombatTimestamp { get; set; }
        public DateTime LastCombatAttackedTimestamp { get; set; }
        public bool FlaggedForAbuse { get; set; }
        public int UnusedLevelUpPerks { get; set; }
        [Index("IX_MembershipIdAndInPvP", 2)]
        public bool InPvP { get; set; }
        public int GameMode { get; set; }
        public bool NonPvP_GameoverSpellsAllowed { get; set; }
        public DateTime NonPvP_GameOverSpellsAllowedLastChange { get; set; }
        public bool InRP { get; set; }
        public int CleansesMeditatesThisRound { get; set; }
        public decimal Money { get; set; }
        [Index("IX_MembershipIdAndCovenant", 2)]
        public int Covenant { get; set; }
        public string OriginalForm { get; set; }
        public decimal PvPScore { get; set; }
        public int DonatorLevel { get; set; }
        public string Nickname { get; set; }
        public DateTime OnlineActivityTimestamp { get; set; }
        public bool IsBannedFromGlobalChat { get; set; }
        public string ChatColor { get; set; }
        public int ShoutsRemaining { get; set; }
        public int InDuel { get; set; }
        public int InQuest { get; set; }
        public int InQuestState { get; set; }
        public int ItemsUsedThisTurn { get; set; }

        public string GetFullName()
        {
            if (this.DonatorLevel >= 2 && this.Nickname != null && this.Nickname != "")
            {
                return this.FirstName + " '" + this.Nickname + "' " + this.LastName;
            }
            else
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        public void NormalizeHealthMana()
        {

            if (this.Health < 0)
            {
                this.Health = 0;
            }
            else if (this.Health > this.MaxHealth)
            {
                this.Health = this.MaxHealth;
            }
            if (this.Mana < 0)
            {
                this.Mana = 0;
            }
            else if (this.Mana > this.MaxMana)
            {
                this.Mana = this.Mana;
            }
        }

        public void ReadjustMaxes(BuffBox buffs)
        {
            // readjust this health/mana by grabbing base amount plus effects from buffs
            this.MaxHealth = Convert.ToDecimal(PlayerProcedures.GetWillpowerBaseByLevel(this.Level)) * (1.0M + (buffs.HealthBonusPercent() / 100.0M));
            this.MaxMana = Convert.ToDecimal(PlayerProcedures.GetManaBaseByLevel(this.Level)) * (1.0M + (buffs.ManaBonusPercent() / 100.0M));


            // keep this's health within proper bounds
            if (this.MaxHealth < 1)
            {
                this.MaxHealth = 1;
            }

            if (this.MaxMana < 1)
            {
                this.MaxMana = 1;
            }


            if (this.Health > this.MaxHealth)
            {
                this.Health = this.MaxHealth;
            }
            if (this.Mana > this.MaxMana)
            {
                this.Mana = this.MaxMana;
            }
            if (this.Health < 0)
            {
                this.Health = 0;
            }
            if (this.Mana < 0)
            {
                this.Mana = 0;
            }

        }

        public DateTime GetLastCombatTimestamp()
        {
            if (this.LastCombatTimestamp > this.LastCombatAttackedTimestamp)
            {
                return this.LastCombatTimestamp;
            }
            else
            {
                return this.LastCombatAttackedTimestamp;
            }
        }

        public bool IsInDungeon()
        {
            if (this.dbLocationName.Contains("dungeon_")) {
                return true;
            } else {
                return false;
            }
        }

    }

    public class Player_VM
    {
        public int Id { get; set; }
        public string MembershipId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string dbLocationName { get; set; }
        public string Form { get; set; }
        public decimal Health { get; set; }
        public decimal MaxHealth { get; set; }
        public decimal Mana { get; set; }
        public decimal MaxMana { get; set; }
        public decimal ActionPoints { get; set; }
        public decimal ActionPoints_Refill { get; set; }
        public decimal ResistanceModifier { get; set; }
        public string Gender { get; set; }
        public string Mobility { get; set; }
        public int BotId { get; set; }
        public int IsItemId { get; set; }
        public int IsPetToId { get; set; }

        public bool MindControlIsActive { get; set; }

        public decimal XP { get; set; }
        public int Level { get; set; }
        public int TimesAttackingThisUpdate { get; set; }
        public string IpAddress { get; set; }
        public DateTime LastActionTimestamp { get; set; }
        public DateTime LastCombatTimestamp { get; set; }
        public DateTime LastCombatAttackedTimestamp { get; set; }
        public bool FlaggedForAbuse { get; set; }
        public int UnusedLevelUpPerks { get; set; }
        public bool InPvP { get; set; } // 0 == Superprotection, 1 == Protection, 2 == PvP
        public int GameMode { get; set; }
        public bool NonPvP_GameoverSpellsAllowed { get; set; }
        public DateTime NonPvP_GameOverSpellsAllowedLastChange { get; set; }
        public bool InRP { get; set; }
        public int CleansesMeditatesThisRound { get; set; }
        public decimal Money { get; set; }
        public int Covenant { get; set; }
        public string OriginalForm { get; set; }
        public decimal PvPScore { get; set; }
        public int DonatorLevel { get; set; }
        public string Nickname { get; set; }
        public DateTime OnlineActivityTimestamp { get; set; }
        public bool IsBannedFromGlobalChat { get; set; }
        

        public string ChatColor { get; set; }
        public int ShoutsRemaining { get; set; }
        public int InDuel { get; set; }
        public int InQuest { get; set; }
        public int InQuestState { get; set; }

        public int ItemsUsedThisTurn { get; set; }

        public string GetFullName()
        {
            if (this.DonatorLevel >= 2 && this.Nickname != null && this.Nickname != "")
            {
                return this.FirstName + " '" + this.Nickname + "' " + this.LastName;
            }
            else
            {
                return this.FirstName + " " + this.LastName;
            }

        }

        public Tuple<string, string> GetDescriptor()
        {
            var name = string.Empty;
            var pic = string.Empty;

            if (MembershipId == "-1")
                return new Tuple<string, string>(name, pic);

            if (ChatStatics.Staff.ContainsKey(MembershipId))
            {
                var descriptor = ChatStatics.Staff[MembershipId];
                name = descriptor.Item1;
                pic = descriptor.Item2;

                return new Tuple<string, string>(name, pic);
            }

            name = GetFullName();

            return new Tuple<string, string>(name, pic);
        }

        public DateTime GetLastCombatTimestamp()
        {
            if (this.LastCombatTimestamp > this.LastCombatAttackedTimestamp)
            {
                return this.LastCombatTimestamp;
            }
            else
            {
                return this.LastCombatAttackedTimestamp;
            }
        }

        public Player ToDbPlayer()
        {
            Player output = new Player{
                Id = this.Id,
                MembershipId = this.MembershipId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                dbLocationName = this.dbLocationName,
                Form = this.Form,
                Health = this.Health,
                MaxHealth = this.MaxHealth,
                Mana = this.Mana,
                MaxMana = this.MaxMana,
                ActionPoints = this.ActionPoints,
                ActionPoints_Refill = this.ActionPoints_Refill,
                ResistanceModifier = this.ResistanceModifier,
                Gender = this.Gender,
                Mobility = this.Mobility,
                BotId = this.BotId,
                IsItemId = this.IsItemId,
                IsPetToId = this.IsPetToId,

                MindControlIsActive = this.MindControlIsActive,

                XP = this.XP,
                Level = this.Level,
                TimesAttackingThisUpdate = this.TimesAttackingThisUpdate,
                IpAddress = this.IpAddress,
                LastActionTimestamp = this.LastActionTimestamp,
                LastCombatTimestamp = this.LastCombatTimestamp,
                LastCombatAttackedTimestamp = this.LastCombatAttackedTimestamp,
                FlaggedForAbuse = this.FlaggedForAbuse,
                UnusedLevelUpPerks = this.UnusedLevelUpPerks,
                GameMode = this.GameMode,
                NonPvP_GameoverSpellsAllowed = this.NonPvP_GameoverSpellsAllowed,
                NonPvP_GameOverSpellsAllowedLastChange = this.NonPvP_GameOverSpellsAllowedLastChange,
                InRP = this.InRP,
                CleansesMeditatesThisRound = this.CleansesMeditatesThisRound,
                Money = this.Money,
                Covenant = this.Covenant,
                OriginalForm = this.OriginalForm,
                PvPScore = this.PvPScore,
                DonatorLevel = this.DonatorLevel,
                Nickname = this.Nickname,
                OnlineActivityTimestamp = this.OnlineActivityTimestamp,

                InDuel = this.InDuel,

                IsBannedFromGlobalChat = this.IsBannedFromGlobalChat,
                InQuest = this.InQuest,
                InQuestState = this.InQuestState,
                ItemsUsedThisTurn = this.ItemsUsedThisTurn,

            };
            return output;
        }


        public bool IsInDungeon()
        {
            if (this.dbLocationName.Contains("dungeon_"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateOnlineActivityTimestamp()
        {
            var markOnlineCutoff = DateTime.UtcNow.AddMinutes(ChatStatics.OnlineActivityCutoffMinutes);

            // update the player's "last online" attribute if it's been long enough
            if (OnlineActivityTimestamp >= markOnlineCutoff || PvPStatics.AnimateUpdateInProgress)
                return;

            var cmd = new UpdateOnlineActivityTimestamp { Player = this };
            DomainRegistry.Root.Execute(cmd);
        }
    }
}