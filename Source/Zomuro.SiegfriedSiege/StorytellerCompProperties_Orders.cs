using System.Collections.Generic;
using RimWorld;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerCompProperties_Orders : StorytellerCompProperties
    {
        public StorytellerCompProperties_Orders()
        {
            this.compClass = typeof(StorytellerComp_Orders);
        }

        public List<StorytellerOrderDef> orders;
    }
}
