using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Discipline_Mental : StatPart_Order
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= pawn.health.summaryHealth.SummaryHealthPercent;
				}
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					return key.Translate() + " x" + pawn.health.summaryHealth.SummaryHealthPercent.ToStringPercent();
				}
			}
			return null;
		}

	}
}
