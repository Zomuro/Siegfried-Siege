﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerOrderWorker
    {
        public virtual float OrderTransformValue(StatRequest req)
        {
            return 0f;
        }
        
        public virtual string OrderExplanationPart(StatRequest req)
        {
            return "";
        }

        public StorytellerOrderDef def;
    }
}
