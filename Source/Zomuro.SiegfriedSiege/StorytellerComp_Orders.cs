using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public override void Initialize()
		{
            // each time we initialize the storyteller
            if (!enabledOrders.NullOrEmpty()) // checks if enabled orders isn't empty; if so, we clean it up
            {
                List<StorytellerOrderDef> removeList = new List<StorytellerOrderDef>();
                // if we find keys in enabledOrders not in our compProps (defined in xml), add to remove list
                foreach (var pair in enabledOrders) if (!Props.orders.Contains(pair.Key)) removeList.Add(pair.Key);
                // then remove the keys from enabledOrders
                foreach(var order in removeList) enabledOrders.Remove(order);
            }
            
            // we then add in keys that are in the xml but NOT in enabledOrders
			foreach(var order in Props.orders) if (!enabledOrders.ContainsKey(order)) enabledOrders.Add(order, true);

            //if (currentOrder is null) NewOrder();
		}

		public override IEnumerable<FiringIncident> MakeIntervalIncidents(IIncidentTarget target)
		{
			if (!StorytellerUtility.SiegfriedSiegeCheck()) yield break;
			
			if (currentOrder is null || Rand.MTBEventOccurs(15f, 60000f, 1000f))
			{
                IncidentParms parms = GenerateParms(IncidentDefOf_SiegfriedSiege.Zomuro_SiegfriedSiegeOrder.category, target);
                // put in siegfried's incidentdef here
                yield return new FiringIncident(IncidentDefOf_SiegfriedSiege.Zomuro_SiegfriedSiegeOrder, this, parms);
            }
			yield break;
		}

		public void NewOrder()
        {
            if (Props.orders.NullOrEmpty()) return;
            if (currentOrder is null) currentOrder = Props.orders.RandomElement();
            List<StorytellerOrderDef> selectableOrders = Props.orders.ListFullCopy();
            if(selectableOrders.Count() > 1) selectableOrders.Remove(currentOrder);
            currentOrder = selectableOrders.RandomElement();
        }

        public bool OrderIsEnabled(StorytellerOrderDef order)
        {
            return enabledOrders.ContainsKey(order) && enabledOrders[order];
        }

		public void CompExposeData()
        {
            Scribe_Defs.Look(ref currentOrder, "currentOrder");
            Scribe_Collections.Look(ref enabledOrders, "enabledOrders", LookMode.Def, LookMode.Value);
        }

        private Dictionary<StorytellerOrderDef, bool> enabledOrders = new Dictionary<StorytellerOrderDef, bool>();

		public StorytellerOrderDef currentOrder;
	}

    
}
