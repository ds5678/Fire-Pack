using HarmonyLib;

namespace FirePack
{
	[HarmonyPatch(typeof(Panel_FeedFire), "CanTakeTorch")]
	[HarmonyPriority(Priority.Last)]
	internal class Panel_FeedFire_CanTakeTorch
	{
		private static void Postfix(ref bool __result)
		{
			if (!Settings.instance.pullTorches) __result = false;
		}
	}

	[HarmonyPatch(typeof(Fire), "CanTakeTorch")]
	[HarmonyPriority(Priority.Last)]
	internal class Fire_CanTakeTorch
	{
		private static void Postfix(ref bool __result)
		{
			if (!Settings.instance.pullTorches) __result = false;
		}
	}

	[HarmonyPatch(typeof(FireManager), "PlayerStartFire")]
	internal class FireManager_PlayerStartFire
	{
		private static void Prefix(FireStarterItem starter)
		{
			if (Settings.instance.consumeTorchOnFirestart && starter.name.StartsWith("GEAR_Torch"))
			{
				starter.m_ConditionDegradeOnUse = 100;
				starter.m_ConsumeOnUse = true;
			}
		}
	}

	[HarmonyPatch(typeof(StartGear), "AddAllToInventory")]
	internal class StartGear_AddAllToInventory
	{
		private static void Postfix()
		{
			if (Settings.instance.noWoodMatches) GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory("GEAR_PackMatches", 20);
		}
	}

	[HarmonyPatch(typeof(GearItem), "Awake")]
	internal class DestroyWoodMatches
	{
		private static void Postfix(GearItem __instance)
		{
			if (Settings.instance.noWoodMatches && __instance.name.Replace("(Clone)", "") == "GEAR_WoodMatches")
			{
				UnityEngine.Object.Destroy(__instance.gameObject);
			}
		}
	}
}