using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class Thought_Order_Preparation : Thought_Situational
    {
		public override int CurStageIndex
		{
			get
			{
				return FindIndex(BaseMoodOffset);
			}
		}

		public int FindIndex(float mood)
        {
			int index = 0;
			float compare;
			for (int i = 0; i < def.stages.Count; i++)
            {
				compare = def.stages[i].baseMoodEffect;
				index = i;
				if (mood <= compare) return index;
			}
			return index;
        }

		public List<ThoughtStage> SortedStages
        {
            get
            {
                if (cachedStages.NullOrEmpty())
                {
					cachedStages = def.stages;
					cachedStages.OrderBy(x => x.baseMoodEffect);
				}
				return cachedStages;
            }
        }

		public override string LabelCap
		{
			get
			{
				return base.CurStage.label.Translate().CapitalizeFirst();
			}
		}

		public override string Description
		{
			get
			{
				return base.CurStage.description.Translate().CapitalizeFirst();
			}
		}

		protected override float BaseMoodOffset
		{
			get
			{
				return CalculateTotalMoodOffset(pawn);
			}
		}


		public float CalculateTotalMoodOffset(Pawn pawn)
        {
			QualityCategory quality;
			float total = 0;
			foreach (var equip in pawn.equipment.AllEquipmentListForReading)
            {
				if(equip.TryGetQuality(out quality))
                {
					total += qualityScores[quality];
				}
            }

			foreach (var apparel in pawn.apparel.WornApparel)
			{
				if (apparel.TryGetQuality(out quality))
				{
					total += qualityScores[quality];
				}
			}
			return total;
        }


		public Dictionary<QualityCategory, float> qualityScores = new Dictionary<QualityCategory, float>() 
		{
			{ QualityCategory.Awful, -2f },
			{ QualityCategory.Poor, -1f },
			{ QualityCategory.Normal, 0f },
			{ QualityCategory.Good, 1f },
			{ QualityCategory.Excellent, 2f },
			{ QualityCategory.Masterwork, 3f },
			{ QualityCategory.Legendary, 4f }
		};

		private List<ThoughtStage> cachedStages;
	}
}
