using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class ThoughtWorker_Order_Preparation : ThoughtWorker
    {
		protected override ThoughtState CurrentStateInternal(Pawn p)
		{
			if (p.apparel is null || p.equipment is null || !StorytellerUtility.SiegfriedSiegeCheck() || 
				StorytellerUtility.OrdersComp.currentOrder != StorytellerOrderDefOf.Zomuro_Preparation) return false;

			return true;
		}
	}
}
