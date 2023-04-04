using System;
using System.Collections;
using RimWorld;
using Verse;
using UnityEngine;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Loyalty : StatPart_Order
	{

		public float MentalThreshMult(Pawn pawn)
		{
			return Mathf.Lerp(0.5f, 1f, pawn.needs.TryGetNeed<Need_Mood>().CurLevel / StatDefOf.MentalBreakThreshold.maxValue);
		}

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= MentalThreshMult(pawn);
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
					return key.Translate() + " x" + MentalThreshMult(pawn).ToStringPercent();
				}
			}
			return null;
		}
	}
}
