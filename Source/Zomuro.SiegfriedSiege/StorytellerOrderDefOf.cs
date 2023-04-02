using RimWorld;

namespace Zomuro.SiegfriedSiege
{
    [DefOf]
    public static class StorytellerOrderDefOf
    {
        public static StorytellerOrderDef Zomuro_Duty;

        static StorytellerOrderDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StorytellerOrderDefOf));
        }
    }
}
