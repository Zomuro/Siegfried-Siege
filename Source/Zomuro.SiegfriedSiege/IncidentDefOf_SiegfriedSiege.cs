using RimWorld;

namespace Zomuro.SiegfriedSiege
{
    [DefOf]
    public static class IncidentDefOf_SiegfriedSiege
    {
        public static IncidentDef Zomuro_SiegfriedSiegeOrder;

        static IncidentDefOf_SiegfriedSiege()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(IncidentDefOf_SiegfriedSiege));
        }
    }
}
