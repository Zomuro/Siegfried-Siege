using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerOrderWorker_Wisdom_Research : StorytellerOrderWorker
    {
        public override float OrderTransformValue(StatRequest req)
        {
            return 0.3f;
        }
        
        public override string OrderExplanationPart(StatRequest req)
        {
            return "";
        }
    }
}
