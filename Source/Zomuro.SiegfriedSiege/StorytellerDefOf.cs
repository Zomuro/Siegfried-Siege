using RimWorld;

namespace Zomuro.SiegfriedSiege
{
    [DefOf]
    public static class StorytellerDefOf
    {
        public static StorytellerDef Zomuro_SiegfriedSiege;

        static StorytellerDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StorytellerDefOf));
        }
    }
}
