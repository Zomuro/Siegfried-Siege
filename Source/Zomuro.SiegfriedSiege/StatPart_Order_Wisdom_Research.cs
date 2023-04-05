using Verse;
using UnityEngine;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Wisdom_Research : StatPart_Order
	{
		// confirm that Siegfried's current order matches with the statpart's order
		public override bool ConfirmOrder(Pawn pawn = null)
        {
			if(Mathf.Lerp(0f, 1f, pawn.MapHeld.skyManager.CurSkyGlow) > 0)
            {
				return base.ConfirmOrder();
            }
			return false;
        }
	}
}
