using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StatPart_MentalThresholdSetting : StatPart
    {
		// apply the construction speed multiplier
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && StorytellerUtility.SiegfriedSiegeCheck() && StorytellerUtility.settings.SiegfriedSiegeCombatMentalEnable &&
					StorytellerUtility.SiegfriedSiegeCombatCheck(pawn))  // if the storyteller is Siegward
				{
					val *= StorytellerUtility.settings.SiegfriedSiegeCombatMentalMult; // apply the base mechanic construction speed mult
				}
			}
		}

		// shows Siegward's effect on construction speed if he's the current storyteller
		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && StorytellerUtility.SiegfriedSiegeCheck() && StorytellerUtility.settings.SiegfriedSiegeCombatMentalEnable &&
					StorytellerUtility.SiegfriedSiegeCombatCheck(pawn))
				{
					return "SiegfriedSiege_StatsReport_CombatMentalThreshold".Translate() + ": x" + StorytellerUtility.settings.SiegfriedSiegeCombatMentalMult.ToStringPercent();
				}
			}
			return null;
		}
	}
}
