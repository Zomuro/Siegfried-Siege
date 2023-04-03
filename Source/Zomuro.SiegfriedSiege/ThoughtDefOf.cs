using RimWorld;
using Verse;

namespace Zomuro.SiegfriedSiege
{
    [DefOf]
    public static class ThoughtDefOf
    {
        public static ThoughtDef Zomuro_Preparation_Thought;

        static ThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
        }
    }
}
