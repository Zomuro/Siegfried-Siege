using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerComp_Orders : StorytellerComp
    {
		protected StorytellerCompProperties_Orders Props
		{
			get
			{
				return (StorytellerCompProperties_Orders)this.props;
			}
		}

        public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
		{
			if (!StorytellerUtility.SiegfriedSiegeCheck()) yield break;
			
			if (currentOrder is null || Rand.MTBEventOccurs(StorytellerUtility.settings.SiegfriedSiegeOrderMTBDays, 60000f, 1000f))
			{
                IncidentParms parms = GenerateParms(IncidentDefOf_SiegfriedSiege.Zomuro_SiegfriedSiegeOrder.category, target);
                yield return new FiringIncident(IncidentDefOf_SiegfriedSiege.Zomuro_SiegfriedSiegeOrder, this, parms);
            }
			yield break;
		}

		public bool NewOrder()
        {
            if (Props.orders.NullOrEmpty()) return false;
            if (currentOrder is null) 
            {
                currentOrder = Props.orders.RandomElement();
                return true;
            }
            List<StorytellerOrderDef> selectableOrders = Props.orders.ListFullCopy();
            if (selectableOrders.Count() > 1 && !StorytellerUtility.settings.SiegfriedSiegeOrderRepeat) 
                selectableOrders.Remove(currentOrder);
            currentOrder = selectableOrders.RandomElement();
            return true;
        }

		public void CompExposeData()
        {
            Scribe_Defs.Look(ref currentOrder, "currentOrder");
        }

		public StorytellerOrderDef currentOrder;
	}

    
}
