using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    public class IncidentWorker_Order : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (StorytellerUtility.SiegfriedSiegeCheck()) return true;
            return false;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            CreateNewOrder(parms);
            return true;
        }

        public void CreateNewOrder(IncidentParms parms)
        {
            StorytellerUtility.OrdersComp.NewOrder(); // changes Siegfried's order
            base.SendStandardLetter(StorytellerUtility.OrdersComp.currentOrder.LabelCap, StorytellerUtility.OrdersComp.currentOrder.description, LetterDefOf.NeutralEvent,
                parms, null, Array.Empty<NamedArgument>()); // sends letter informing player of order change
        }

    }
}
