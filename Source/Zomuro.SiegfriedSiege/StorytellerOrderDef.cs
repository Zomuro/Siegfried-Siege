using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class StorytellerOrderDef : Def
    {
        public Type workerClass = typeof(StorytellerOrderWorker);

		[MustTranslate]
		public string explainationKey;

		public StorytellerOrderWorker Worker
		{
			get
			{
				if (cachedWorker == null)
				{
					cachedWorker = (StorytellerOrderWorker)Activator.CreateInstance(workerClass);
					cachedWorker.def = this;
				}
				return cachedWorker;
			}
		}

		[Unsaved(false)]
		private StorytellerOrderWorker cachedWorker;
	}
}
