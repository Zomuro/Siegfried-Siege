using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerOrderWorker
    {
        public virtual float OrderTransformValue()
        {
            return 0f;
        }
        
        public virtual string OrderExplanationPart()
        {
            return "";
        }

        public StorytellerOrderDef def;
    }
}
