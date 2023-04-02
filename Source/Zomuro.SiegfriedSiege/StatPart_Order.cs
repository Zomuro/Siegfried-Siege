using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
	// base statpart class that checks for orders
	public class StatPart_Order : StatPart
    {
		// confirm that Siegfried's current order matches with the statpart's order
		public virtual bool ConfirmOrder(Pawn pawn = null)
        {
			if (!StorytellerUtility.SiegfriedSiegeCheck()) return false;
			if (requiredOrderDef is null || requiredOrderDef == StorytellerUtility.OrdersComp.currentOrder) return true;
			return false;
        }

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= factor;
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
					return key.Translate() + " x" + factor.ToStringPercent();
				}
			}
			return null;
		}

		public StorytellerOrderDef requiredOrderDef;

		[MustTranslate]
		public string key;

		public float factor = 1f;
	}
}
