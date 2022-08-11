using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace ScrapHotkey
{

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Star Valor.exe")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Harmony.CreateAndPatchAll(typeof(Plugin));
        }


        [HarmonyPatch(typeof(Inventory), "SelectItem")]
        [HarmonyPostfix]
        public static void SelectItem(Inventory __instance, int itemIndex, int slotIndex, bool allowAutoActions)
        {
            if (allowAutoActions && Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    __instance.ScrapItem();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    __instance.DestroyCargoItem(destroyAll: true);
                }
            }
        }
    }
}
