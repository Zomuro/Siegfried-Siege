using Verse;

namespace Zomuro.SiegfriedSiege
{
    public static class StorytellerUtility
    {
        public static bool SiegfriedSiegeCheck() // not really advanced code, but just useful to have in one place
        {
			if (Find.Storyteller.def != Zomuro.SiegfriedSiege.StorytellerDefOf.Zomuro_SiegfriedSiege) return false;
			return true;
        }
        
        public static bool SiegfriedSiegeCombatCheck(Pawn pawn) // checks of pawn is currently in battle
        {
            if (pawn.records.BattleActive != null) return true;
            return false;
        }

        public static SiegfriedSiegeSettings settings = LoadedModManager.GetMod<SiegfriedSiegeMod>().GetSettings<SiegfriedSiegeSettings>();
    }
}
