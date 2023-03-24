using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using RimWorld;
using Verse;
using HarmonyLib;


namespace Zomuro.SiegfriedSiege
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("Zomuro.SiegwardSiege");

            // Need_Food_SiegwardSiege_Post
            // POSTFIX: adjust hunger rate if the pawn is in combat, based on settings
            harmony.Patch(AccessTools.Method(typeof(Need_Food), "FoodFallPerTickAssumingCategory"),
                null, new HarmonyMethod(typeof(HarmonyPatches), nameof(FoodFallPerTickAssumingCategory_SiegwardSiege_Post)));

            // Apply_SiegfriedSiege_Post
            // TRANSPLIER: multiples base damage amount by setting amount if storyteller is Siegward Siege
            harmony.Patch(AccessTools.Method(typeof(DamageWorker), "Apply"),
                null, null, new HarmonyMethod(typeof(HarmonyPatches), nameof(Apply_SiegfriedSiege_Post)));

        }

        // POSTFIX: adjust hunger rate if the pawn is in combat, based on settings
        public static void FoodFallPerTickAssumingCategory_SiegwardSiege_Post(Need_Food __instance, ref float __result)
        {
            if (StorytellerUtility.SiegfriedSiegeCheck())
            {
                Traverse traverse = Traverse.Create(__instance);
                if(StorytellerUtility.SiegfriedSiegeCombatCheck(traverse.Field("pawn").GetValue<Pawn>()))
                        __result *= StorytellerUtility.settings.SiegfriedSiegeCombatHungerMult;
            }
            return;
        }

        // TRANSPLIER: multiples base damage amount by setting amount if storyteller is Siegward Siege
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Apply_SiegfriedSiege_Post(IEnumerable<CodeInstruction> instructions)
        {
            FieldInfo buildingDamageFactor = AccessTools.Field(typeof(DamageDef), "buildingDamageFactor");
            int spacing = 0;
            bool startSpacing = false;
            foreach (CodeInstruction instruction in instructions)
            {
                yield return instruction;
                if (instruction.LoadsField(buildingDamageFactor)) startSpacing = true;
                if (startSpacing)
                {
                    if(spacing == 2)
                    {
                        yield return new CodeInstruction(OpCodes.Ldloc_1, null);
                        yield return CodeInstruction.Call(typeof(HarmonyPatches), "SiegfriedSiegeBuildDamageFactor", null, null);
                        yield return new CodeInstruction(OpCodes.Mul, null);
                        yield return new CodeInstruction(OpCodes.Stloc_1, null);
                    }
                    spacing++;
                }
            }

            yield break;
        }

        public static float SiegfriedSiegeBuildDamageFactor()
        {
            if (StorytellerUtility.SiegfriedSiegeCheck()) return StorytellerUtility.settings.SiegfriedSiegeBuildingMult;
                
            return 1f;

        }
    }
}
