using RimWorld;

namespace Zomuro.SiegfriedSiege
{
    [DefOf]
    public static class StorytellerOrderDefOf
    {
        public static StorytellerOrderDef Zomuro_Duty;
        public static StorytellerOrderDef Zomuro_Preparation;
        public static StorytellerOrderDef Zomuro_Decisiveness;

        static StorytellerOrderDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StorytellerOrderDefOf));
        }
    }
}
