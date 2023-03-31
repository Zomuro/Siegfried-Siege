using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

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
