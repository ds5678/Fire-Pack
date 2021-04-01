using Harmony;
using System.Collections.Generic;

using System.Reflection;
using System.Reflection.Emit;

namespace FirePack
{
    [HarmonyPatch(typeof(Panel_FeedFire), "Start")]
    internal class Panel_FeedFire_Start
    {
        internal static void Postfix(Panel_FeedFire __instance)
        {
            TakeEmbersButton.Initialize(__instance);
        }
    }

    [HarmonyPatch(typeof(Panel_FeedFire), "CanTakeTorch")]
    [HarmonyPriority(Priority.Last)]
    internal class Panel_FeedFire_CanTakeTorch
    {
        private static void Postfix(ref bool __result)
        {
            if (!Settings.options.pullTorches) __result = false;
        }
    }

    [HarmonyPatch(typeof(Fire), "CanTakeTorch")]
    [HarmonyPriority(Priority.Last)]
    internal class Fire_CanTakeTorch
    {
        private static void Postfix(ref bool __result)
        {
            if(!Settings.options.pullTorches) __result = false;
        }
    }

    [HarmonyPatch(typeof(FireManager),"PlayerStartFire")]
    internal class FireManager_PlayerStartFire
    {
        private static void Prefix(FireStarterItem starter)
        {
            if(Settings.options.consumeTorchOnFirestart && starter.name.StartsWith("GEAR_Torch"))
            {
                starter.m_ConditionDegradeOnUse = 100;
                starter.m_ConsumeOnUse = true;
            }
        }
    }
}