using System;
using System.Collections;
using RimWorld;
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
		
		/*public override void TransformValue(StatRequest req, ref float val)
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
		}*/
	}
}
