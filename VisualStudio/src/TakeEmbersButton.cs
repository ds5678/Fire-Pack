using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace FirePack
{
    internal class TakeEmbersButton
    {
        internal static string text;
        private static GameObject button;

        internal static void Execute()
        {
            GearItem emberBox = GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_EmberBox");
            if (emberBox == null)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_ToolRequiredToForceOpen").Replace("{item-name}", Localization.Get("GAMEPLAY_EmberBox")), false);
                return;
            }

            Panel_FeedFire panel = InterfaceManager.m_Panel_FeedFire;
            Fire fire = panel?.m_Fire;
            if (fire && !fire.m_IsPerpetual)
            {
                fire.ReduceHeatByDegrees(1);
            }

            GameManager.GetInventoryComponent().DestroyGear(emberBox.gameObject);
            GearItem activeEmberBox = GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory("GEAR_ActiveEmberBox");
            GearMessage.AddMessage(activeEmberBox, Localization.Get("GAMEPLAY_Harvested"), activeEmberBox.m_DisplayName, false);

            InterfaceManager.m_Panel_FeedFire.ExitFeedFireInterface();
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
}