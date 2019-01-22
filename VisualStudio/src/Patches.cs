using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace FirePack.src
{
    internal class Patches
    {
        [HarmonyPatch(typeof(Panel_FeedFire), "Start")]
        internal class Panel_FeedFire_Start
        {
            internal static void Postfix(Panel_FeedFire __instance)
            {
                TakeEmbersButton.Initialize(__instance);
            }
        }


        [HarmonyPatch(typeof(Panel_FeedFire), "UpdateButtonLegend")]
        internal class Panel_FeedFire_UpdateButtonLegend
        {
            internal static void Prefix(Panel_FeedFire __instance)
            {
                Fire fire = Traverse.Create(__instance).Field("m_Fire").GetValue<Fire>();
                bool canTakeEmbers = fire && fire.IsBurning() || fire.IsEmbers();

                if (Utils.IsMouseActive())
                {
                    TakeEmbersButton.SetActive(canTakeEmbers);
                }
                else
                {
                    __instance.m_ButtonLegendContainer.BeginUpdate();
                    __instance.m_ButtonLegendContainer.UpdateButton("Inventory_Drop", TakeEmbersButton.text, canTakeEmbers, 2, false);
                }
            }
        }
    }

    internal class TakeEmbersButton
    {
        internal static string text;
        private static GameObject button;

        public static void Execute()
        {
            GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
            if (emberBox == null)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_ToolRequiredToForceOpen").Replace("{item-name}", Localization.Get("GAMEPLAY_EmberBox")), false);
                return;
            }

            Panel_FeedFire panel = InterfaceManager.m_Panel_FeedFire;
            Fire fire = Traverse.Create(panel).Field("m_Fire").GetValue<Fire>();
            if (fire && !fire.m_IsPerpetual)
            {
                fire.ReduceHeatByDegrees(1);
            }

            GameManager.GetInventoryComponent().DestroyGear(emberBox.gameObject);
            GearItem activeEmberBox = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory("GEAR_ActiveEmberBox");
            GearMessage.AddMessage(activeEmberBox, Localization.Get("GAMEPLAY_Harvested"), activeEmberBox.m_DisplayName, false);

            InterfaceManager.m_Panel_FeedFire.ExitFeedFireInterface();
        }

        internal static void Initialize(Panel_FeedFire panel)
        {
            text = Localization.Get("GAMEPLAY_TakeEmbers");

            button = Object.Instantiate<GameObject>(panel.m_ActionSecondaryButtonObject, panel.m_ActionSecondaryButtonObject.transform.parent, true);
            button.transform.localPosition = new Vector3(-625, 42, 0);
            Utils.GetComponentInChildren<UILabel>(button).text = text;
            Utils.GetComponentInChildren<UIButton>(button).onClick = new List<EventDelegate>() { new EventDelegate(Execute) };

            NGUITools.SetActive(button, false);
        }

        internal static void SetActive(bool active)
        {
            NGUITools.SetActive(button, active);
        }
    }
}