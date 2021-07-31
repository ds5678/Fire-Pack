using HarmonyLib;
using System;
using Il2CppList = Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>;
using SystemList = System.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>;

namespace FirePack
{
	internal static class EmbersActionPickerButton
	{
		internal static void Execute()
		{
			FireUtils.TakeEmbers(InterfaceManager.m_Panel_ActionPicker?.m_ObjectInteractedWith?.GetComponentInChildren<Fire>());
		}
	}

	[HarmonyPatch(typeof(Panel_ActionPicker), "EnableWithCurrentList")]
	internal class Panel_ActionPicker_ShowCustomActionPicker
	{
		internal static void Prefix(Panel_ActionPicker __instance)
		{
			if (!FireUtils.IsBurningFire(__instance.m_ObjectInteractedWith) || !FireUtils.HasEmberBox()) return;
			
			SystemList replacement = FireUtils.Convert<Panel_ActionPicker.ActionPickerItemData>(__instance.m_ActionPickerItemDataList);

			replacement.Insert(2, new Panel_ActionPicker.ActionPickerItemData("ico_skills_fireStarting", "GAMEPLAY_TakeEmbers", new Action(EmbersActionPickerButton.Execute)));
			ReplaceList(__instance.m_ActionPickerItemDataList, replacement);
		}
		private static void ReplaceList(Il2CppList original, SystemList actionList)
		{
			if (original is null || actionList is null) return;
			original.Clear();
			foreach (var element in actionList) original.Add(element);
		}
	}
}
