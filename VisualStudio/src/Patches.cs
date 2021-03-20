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
}