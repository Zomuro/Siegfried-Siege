using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Patience : StatPart_Order
	{

		public float FinalTendMult(Pawn pawn)
		{
			if (StorytellerUtility.SiegfriedSiegeCombatCheck(pawn)) return factor + 0.3f;
			return factor;
		}

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= FinalTendMult(pawn);
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
					return key.Translate() + " x" + FinalTendMult(pawn).ToStringPercent();
				}
			}
			return null;
		}
	}
}
