using System;
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
            if (StorytellerUtility.OrdersComp.NewOrder()) // if Siegfried's order is successfully changed
            {
                base.SendStandardLetter(StorytellerUtility.OrdersComp.currentOrder.LabelCap, StorytellerUtility.OrdersComp.currentOrder.description, LetterDefOf.NeutralEvent,
                    parms, null, Array.Empty<NamedArgument>()); // sends letter informing player of order change
            }
        }

    }
}
