using UnityEngine;

namespace FirePack
{
	internal static class FireUtils
	{
		internal static bool HasEmberBox()
		{
			GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
			if (emberBox is null || emberBox.IsWornOut()) return false;
			else return true;
		}

		internal static bool IsBurningFire(GameObject gameObject)
		{
			if (gameObject is null) return false;

			Fire componentInChildren = gameObject.GetComponentInChildren<Fire>();
			if (componentInChildren && componentInChildren.IsBurning()) return true;
			else return false;
		}

		internal static void TakeEmbers(Fire fire)
		{
			if (fire is null) return;

			if (!fire.m_IsPerpetual)
			{
				fire.ReduceHeatByDegrees(1);
			}

			GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
			if (emberBox == null)
			{
				GameAudioManager.PlayGUIError();
				HUDMessage.AddMessage(Localization.Get("GAMEPLAY_ToolRequiredToForceOpen").Replace("{item-name}", Localization.Get("GAMEPLAY_EmberBox")), false);
				return;
			}

			GameManager.GetInventoryComponent().DestroyGear(emberBox.gameObject);
			GearItem activeEmberBox = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory("GEAR_ActiveEmberBox");
			GearMessage.AddMessage(activeEmberBox, Localization.Get("GAMEPLAY_Harvested"), activeEmberBox.m_DisplayName, false);

			InterfaceManager.m_Panel_FeedFire.ExitFeedFireInterface();
		}

		internal static System.Collections.Generic.List<T> Convert<T>(Il2CppSystem.Collections.Generic.List<T> list)
		{
			System.Collections.Generic.List<T> result = new System.Collections.Generic.List<T>(list.Count);
			foreach (var element in list)
			{
				result.Add(element);
			}
			return result;
		}

		internal static Il2CppSystem.Collections.Generic.List<T> Convert<T>(System.Collections.Generic.List<T> list)
		{
			Il2CppSystem.Collections.Generic.List<T> result = new Il2CppSystem.Collections.Generic.List<T>(list.Count);
			foreach (var element in list)
			{
				result.Add(element);
			}
			return result;
		}

	}
}
