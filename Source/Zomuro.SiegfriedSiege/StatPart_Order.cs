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
				val *= 1f;
			}
		}

		public override string ExplanationPart(StatRequest req)
		{
			if (req.HasThing)
			{
				return "";
			}
			return null;
		}

		public StorytellerOrderDef requiredOrderDef;

		[MustTranslate]
		public string key;

		public float factor = 1f;
	}
}
