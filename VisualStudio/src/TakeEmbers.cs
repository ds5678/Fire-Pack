using Harmony;
using System.Collections.Generic;

namespace FirePack
{
    internal class TakeEmbers
    {
        internal static void AddFeedFireActions(List<Panel_ActionPicker.ActionPickerItemData> list)
        {
            list.Insert(2, new Panel_ActionPicker.ActionPickerItemData("ico_skills_fireStarting", "GAMEPLAY_TakeEmbers", Execute));
        }

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
    }
}