using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Perseverance : StatPart_Order
	{
		// confirm that Siegfried's current order matches with the statpart's order
		public override bool ConfirmOrder(Pawn pawn = null)
        {
			if (pawn.RaceProps.baseBodySize > 1f)
			{
				return base.ConfirmOrder();
			}
			return false;
		}

		public float IncomingDamageMultCalc(Pawn pawn)
        {
			return 1f + factor * (pawn.RaceProps.baseBodySize - 1f);
		}

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= IncomingDamageMultCalc(pawn);
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
					return key.Translate() + " x" + IncomingDamageMultCalc(pawn).ToStringPercent();
				}
			}
			return null;
		}

	}
}
