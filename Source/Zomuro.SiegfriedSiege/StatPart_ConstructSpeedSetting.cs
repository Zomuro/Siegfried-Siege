using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StatPart_ConstructSpeedSetting : StatPart
    {
		// apply the construction speed multiplier
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && StorytellerUtility.SiegfriedSiegeCheck())  // if the storyteller is Siegward
				{
					val *= StorytellerUtility.settings.SiegfriedSiegeConstructionMult; // apply the base mechanic construction speed mult
				}
			}
		}

		// shows Siegward's effect on construction speed if he's the current storyteller
		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && StorytellerUtility.SiegfriedSiegeCheck())
				{
					return "SiegfriedSiege_StatsReport_ConstructSpeed".Translate() + ": x" + StorytellerUtility.settings.SiegfriedSiegeConstructionMult.ToStringPercent();
				}
			}
			return null;
		}
	}
}
