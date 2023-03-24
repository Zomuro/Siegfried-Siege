using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StatPart_CombatConstructSpeedSetting : StatPart
    {
		// apply the construction speed multiplier
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;

				// if the mod setting enables it, and the storyteller is Siegward
				if (pawn != null && StorytellerUtility.settings.SiegfriedSiegeCombatConstructEnable && StorytellerUtility.SiegfriedSiegeCheck()) 
				{
					// if the pawn is in combat && tick is different
					if (StorytellerUtility.SiegfriedSiegeCombatCheck(pawn) && cachedTick != Find.TickManager.TicksGame)
					{
						// add difference in ticks, cache it, and multiply construction speed based on ticks passed since combat start
						StorytellerUtility.settings.TicksPassed += Find.TickManager.TicksGame - cachedTick;
						cachedTick = Find.TickManager.TicksGame;
						val *= ScaledConstructionMult(StorytellerUtility.settings.TicksPassed);
					}
					else if (!StorytellerUtility.SiegfriedSiegeCombatCheck(pawn))
                    {
						StorytellerUtility.settings.TicksPassed = 0;
						cachedTick = 0;
					}
				}
			}
		}

		public float ScaledConstructionMult(int ticks)
        {
			return 1f + (StorytellerUtility.settings.SiegfriedSiegeCombatConstructMultMax - 1f) * ticks / StorytellerUtility.settings.SiegfriedSiegeCombatConstructMultTime.SecondsToTicks();
		}

		// shows Siegward's effect on construction speed if he's the current storyteller
		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && StorytellerUtility.SiegfriedSiegeCheck() && StorytellerUtility.SiegfriedSiegeCombatCheck(pawn))
				{
					return "SiegfriedSiege_StatsReport_CombatConstructSpeed".Translate() + ": x" + ScaledConstructionMult(StorytellerUtility.settings.TicksPassed).ToStringPercent();
				}
			}
			return null;
		}

		private int cachedTick = 0;
	}
}
