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

		public StorytellerOrderWorker CurrentWorker
        {
            get
            {
                if (currentOrder is null) currentOrder = SelectOrder();
                return currentOrder.Worker;
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
		}

        public StorytellerOrderDef SelectOrder()
        {
            if (currentOrder is null) return Props.orders.RandomElement();
            List<StorytellerOrderDef> selectableOrders = Props.orders.ListFullCopy();
            selectableOrders.Remove(currentOrder);
            return selectableOrders.RandomElement();
        }

		public void CompExposeData()
        {
            Scribe_Defs.Look(ref currentOrder, "currentOrder");
            Scribe_Collections.Look(ref enabledOrders, "enabledOrders", LookMode.Def, LookMode.Value);
        }

        public Dictionary<StorytellerOrderDef, bool> enabledOrders = new Dictionary<StorytellerOrderDef, bool>();

		private StorytellerOrderDef currentOrder;

	}

    
}
