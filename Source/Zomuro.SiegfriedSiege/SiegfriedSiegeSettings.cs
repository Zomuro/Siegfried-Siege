using System;
using RimWorld;
using Verse;
using UnityEngine;


namespace Zomuro.SiegfriedSiege
{
    public class SiegfriedSiegeSettings : ModSettings
    {
        public bool SiegfriedSiegeOrderRepeat = false; // Default : false; toggle if orders can be repeated. If there's only one order or none, repeat.

        public float SiegfriedSiegeOrderMTBDays = 7f; // Default : 7f; sets the estimated number of days before Siegfried gives a new order.

        public float SiegfriedSiegeBuildingMult = 2f; // Default: 2x building damage

        public float SiegfriedSiegeConstructionMult = 1.3f; // Default: 1.3x construction speed

        public float SiegfriedSiegeCombatHungerMult = 0.8f; // Default: 80% hunger rate in combat

        public bool SiegfriedSiegeCombatConstructEnable = false; // Optional mechanic: give an additional multplier to construction speed, scaling with time while in combat

        public float SiegfriedSiegeCombatConstructMultTime = 120f; // amount of time to scale to the max bonus

        public float SiegfriedSiegeCombatConstructMultMax = 1.3f; // max multiplier, from 1x -> 1.3x

        public int TicksPassed = 0; // used to save time with the combat boost

        public bool SiegfriedSiegeCombatMentalEnable = false; // Optional mechanic: decrease mental threshold in combat

        public float SiegfriedSiegeCombatMentalMult = 0.8f; // threshold multiplied by 75% in combat

        public override void ExposeData()
        {
            Scribe_Values.Look(ref SiegfriedSiegeOrderRepeat, "SiegfriedSiegeOrderRepeat", false);
            Scribe_Values.Look(ref SiegfriedSiegeOrderMTBDays, "SiegfriedSiegeOrderMTBDays", 7f);

            Scribe_Values.Look(ref SiegfriedSiegeBuildingMult, "SiegfriedSiegeBuildingMult", 2f);
            Scribe_Values.Look(ref SiegfriedSiegeConstructionMult, "SiegfriedSiegeConstructionMult", 1.3f);
            Scribe_Values.Look(ref SiegfriedSiegeCombatHungerMult, "SiegfriedSiegeCombatHungerMult", 0.8f);

            Scribe_Values.Look(ref SiegfriedSiegeCombatConstructEnable, "SiegfriedSiegeCombatConstructEnable", false);
            Scribe_Values.Look(ref SiegfriedSiegeCombatConstructMultTime, "SiegfriedSiegeCombatConstructMultTime", 120f);
            Scribe_Values.Look(ref SiegfriedSiegeCombatConstructMultMax, "SiegfriedSiegeCombatConstructMultMax", 1.3f);

            Scribe_Values.Look(ref SiegfriedSiegeCombatMentalEnable, "SiegfriedSiegeCombatConstructEnable", false);
            Scribe_Values.Look(ref SiegfriedSiegeCombatMentalMult, "SiegfriedSiegeCombatConstructMultTime", 0.8f);

            Scribe_Values.Look(ref TicksPassed, "TicksPassed", 0);
            base.ExposeData();
        }
    }

    public class SiegfriedSiegeMod : Mod
    {
        SiegfriedSiegeSettings settings;

        public SiegfriedSiegeMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<SiegfriedSiegeSettings>();
        }

        public override string SettingsCategory()
        {
            return "SiegfriedSiege_Settings".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            // sets up left section (listing of settings)
            Rect leftThird = new Rect(inRect);
            leftThird.width = inRect.width / 3;

            // sets up right side (image of Siegfried)
            Rect otherTwoThird = new Rect(inRect);
            otherTwoThird.xMin += inRect.width / 3;

            // picture frame
            Rect otherTwoThirdBorder = otherTwoThird.ContractedBy(20f);
            otherTwoThirdBorder.y -= 20f;
            Widgets.DrawBoxSolidWithOutline(otherTwoThirdBorder, Color.clear, Color.grey);
            Widgets.DrawBoxSolidWithOutline(otherTwoThirdBorder.ContractedBy(5f), Color.clear, Color.grey);

            // image of Siegfried Siege
            Rect otherTwoThirdImage = otherTwoThirdBorder.ContractedBy(10f);
            Widgets.DrawTextureFitted(otherTwoThirdImage, StorytellerDefOf.Zomuro_SiegfriedSiege.portraitLargeTex, 1f);

            // listing of settings on left side
            var listing = new Listing_Standard();
            listing.Begin(leftThird);
            listing.Gap(16f);
            SiegfriedSiegeSettings(ref listing);
            listing.End();

            base.DoSettingsWindowContents(inRect);
        }

        public void SiegfriedSiegeSettings(ref Listing_Standard listing)
        {
            Text.Font = GameFont.Medium;
            listing.Label("SiegfriedSiege_SettingsGeneral".Translate());
            listing.GapLine();

            Text.Font = GameFont.Small;

            listing.CheckboxLabeled("SiegfriedSiege_RepeatOrders".Translate(settings.SiegfriedSiegeOrderRepeat.ToString()),
                ref settings.SiegfriedSiegeOrderRepeat, "SiegfriedSiege_RepeatOrdersTooltip".Translate());
            listing.Label("SiegfriedSiege_MTBDaysOrders".Translate(settings.SiegfriedSiegeOrderMTBDays.ToString("F1")), -1,
                "SiegfriedSiege_MTBDaysOrdersTooltip".Translate());
            settings.SiegfriedSiegeOrderMTBDays = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeOrderMTBDays), 0.5f, 14f);

            listing.Label("SiegfriedSiege_BuildingDmgSetting".Translate(settings.SiegfriedSiegeBuildingMult.ToString("F1")), -1,
                "SiegfriedSiege_BuildingDmgSettingTooltip".Translate());
            settings.SiegfriedSiegeBuildingMult = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeBuildingMult), 0.5f, 3f);
            listing.Label("SiegfriedSiege_ConstructSetting".Translate(settings.SiegfriedSiegeConstructionMult.ToString("F1")), -1,
                "SiegfriedSiege_ConstructSettingTooltip".Translate());
            settings.SiegfriedSiegeConstructionMult = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeConstructionMult), 0.5f, 3f);
            listing.Label("SiegfriedSiege_HungerFactor".Translate(settings.SiegfriedSiegeCombatHungerMult.ToString("F1")), -1,
                "SiegfriedSiege_HungerFactorTooltip".Translate());
            settings.SiegfriedSiegeCombatHungerMult = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeCombatHungerMult), 0.5f, 3f);

            listing.GapLine();

            listing.CheckboxLabeled("SiegfriedSiege_CombatConstructEnable".Translate(settings.SiegfriedSiegeCombatConstructEnable.ToString()),
                ref settings.SiegfriedSiegeCombatConstructEnable, "SiegfriedSiege_CombatConstructEnableTooltip".Translate());
            if (settings.SiegfriedSiegeCombatConstructEnable)
            {
                listing.Label("SiegfriedSiege_CombatConstructTime".Translate(settings.SiegfriedSiegeCombatConstructMultTime.ToString("F0")), -1,
                "SiegfriedSiege_CombatConstructTimeTooltip".Translate());
                settings.SiegfriedSiegeCombatConstructMultTime = listing.Slider((int)(settings.SiegfriedSiegeCombatConstructMultTime), 30f, 300f);

                listing.Label("SiegfriedSiege_CombatConstructSetting".Translate(settings.SiegfriedSiegeCombatConstructMultMax.ToString("F1")), -1,
                "SiegfriedSiege_CombatConstructSettingTooltip".Translate());
                settings.SiegfriedSiegeCombatConstructMultMax = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeCombatConstructMultMax), 0.5f, 3f);
            }

            listing.CheckboxLabeled("SiegfriedSiege_CombatMentalEnable".Translate(settings.SiegfriedSiegeCombatMentalEnable.ToString()),
                ref settings.SiegfriedSiegeCombatMentalEnable, "SiegfriedSiege_CombatMentalEnableTooltip".Translate());
            if (settings.SiegfriedSiegeCombatMentalEnable)
            {
                listing.Label("SiegfriedSiege_CombatMentalMult".Translate(settings.SiegfriedSiegeCombatMentalMult.ToString("F1")), -1,
                "SiegfriedSiege_CombatMentalMultTooltip".Translate());
                settings.SiegfriedSiegeCombatMentalMult = listing.Slider(ForceRoundTenths(settings.SiegfriedSiegeCombatMentalMult), 0.1f, 2f);
            }

            listing.Gap(16f);
            if (listing.ButtonText("Reset to default"))
            {
                SiegfriedSiegeDefault();
            }
        }

        public float ForceRoundTenths(float num) // forces the sliders to only change in increments of 0.1.
        {
            return (float)Math.Round(num * 10f) / 10f; ;
        }

        public void SiegfriedSiegeDefault() // button to reset everything
        {
            settings.SiegfriedSiegeOrderRepeat = false;
            settings.SiegfriedSiegeOrderMTBDays = 7f;

            settings.SiegfriedSiegeBuildingMult = 2f;
            settings.SiegfriedSiegeConstructionMult = 1.3f;
            settings.SiegfriedSiegeCombatHungerMult = 0.8f;

            settings.SiegfriedSiegeCombatConstructEnable = false; 
            settings.SiegfriedSiegeCombatConstructMultTime = 120f; 
            settings.SiegfriedSiegeCombatConstructMultMax = 1.3f;

            settings.SiegfriedSiegeCombatMentalEnable = false;
            settings.SiegfriedSiegeCombatMentalMult = 0.8f;
        }

        public bool gotOrders = false;

        public bool ordersView = false;
    }
}
