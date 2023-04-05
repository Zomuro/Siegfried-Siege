using RimWorld;
using Verse;
using UnityEngine;

namespace Zomuro.SiegfriedSiege
{
	public class StatPart_Order_Honor : StatPart_Order
	{

		public float RelationsMult(Pawn pawn)
		{
			float totalRelation = 0;
			foreach(var relation in pawn.relations.DirectRelations)
            {
				totalRelation += Mathf.Clamp(relation.otherPawn.relations.OpinionOf(pawn), 0, 100);
            }

			return 1f + totalRelation / 100f;
		}

		public override void TransformValue(StatRequest req, ref float val)
		{
			if (req.HasThing)
			{
				Pawn pawn = req.Thing as Pawn;
				if (pawn != null && ConfirmOrder(pawn))
				{
					val *= RelationsMult(pawn);
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
					return key.Translate() + " x" + RelationsMult(pawn).ToStringPercent();
				}
			}
			return null;
		}
	}
}
