using HarmonyLib;
using SandBox.Missions.MissionLogics.Arena;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;

namespace ArenaImprovements.Patches
{
    [HarmonyPatch]
    public class ArenaPracticeFightMissionControllerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ArenaPracticeFightMissionController), "AddRandomWeapons")]
        private static bool AddRandomWeapons(ArenaPracticeFightMissionController __instance, Settlement ____settlement, Equipment equipment, int spawnIndex)
        {
            if (__instance.IsPlayerPracticing)
            {
                int num = 1 + spawnIndex * 3 / 30;
                List<Equipment> list = (Game.Current.ObjectManager.GetObject<CharacterObject>("weapon_practice_stage_" + num + "_" + ____settlement.MapFaction.Culture.StringId)
                    ?? Game.Current.ObjectManager.GetObject<CharacterObject>("weapon_practice_stage_" + num + "_empire")).BattleEquipments.ToList();

                int index = MBRandom.RandomInt(list.Count);

                for (int i = 0; i <= 3; i++)
                {
                    EquipmentElement equipmentFromSlot = list[index].GetEquipmentFromSlot((EquipmentIndex)i);

                    if (!ArenaConfig.MeleeOnly)
                    {
                        equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                        continue;
                    }

                    if (equipmentFromSlot.Item != null
                        && !equipmentFromSlot.Item.ItemCategory.StringId.Contains("ranged_weapon")
                        && !equipmentFromSlot.Item.ItemCategory.StringId.Contains("arrows")
                        && !equipmentFromSlot.Item.ItemCategory.StringId.Contains("arrows"))
                        {
                            equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                        }
                }
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(ArenaPracticeFightMissionController), "OnMissionTick")]
        private static bool OnMissionTick(ArenaPracticeFightMissionController __instance, BasicMissionTimer ____teleportTimer, float dt)
        {
            if (____teleportTimer != null && ____teleportTimer.ElapsedTime > __instance.TeleportTime)
            {
                ArenaConfig.MeleeOnly = false;
                InformationManager.DisplayMessage(new InformationMessage($"MeleeOnly set to False {DateTime.Now:HH:mm}", Color.ConvertStringToColor("#FF0042FF")));

            }
            return true;
        }
    }
}
