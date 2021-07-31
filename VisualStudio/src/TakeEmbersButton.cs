using HarmonyLib;
using UnityEngine;

namespace FirePack
{
	internal class TakeEmbersButton
	{
		internal static string text;
		private static GameObject button;

		internal static System.Action GetActionDelegate() => new System.Action(Execute);

		internal static void Execute()
		{
			FireUtils.TakeEmbers(InterfaceManager.m_Panel_FeedFire?.m_Fire);
		}

		internal static void Initialize(Panel_FeedFire panel_FeedFire)
		{
			text = Localization.Get("GAMEPLAY_TakeEmbers");

			button = Object.Instantiate<GameObject>(panel_FeedFire.m_ActionButtonObject, panel_FeedFire.m_ActionButtonObject.transform.parent, true);
			button.transform.Translate(0, 0.09f, 0);
			Utils.GetComponentInChildren<UILabel>(button).text = text;
			Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
			placeHolderList.Add(new EventDelegate(new System.Action(Execute)));
			Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;

			NGUITools.SetActive(button, true);
		}

		internal static void SetActive(bool active)
		{
			NGUITools.SetActive(button, active);
		}
	}

	[HarmonyPatch(typeof(Panel_FeedFire), "Start")]
	internal class Panel_FeedFire_Start
	{
		private static void Postfix(Panel_FeedFire __instance)
		{
			TakeEmbersButton.Initialize(__instance);
		}
	}

	[HarmonyPatch(typeof(Panel_FeedFire), "Enable")]
	internal class Panel_FeedFire_Enable
	{
		private static void Postfix(bool enable)
		{
			if (!enable) return;
			if (FireUtils.HasEmberBox()) TakeEmbersButton.SetActive(true);
			else TakeEmbersButton.SetActive(false);
		}
	}
}