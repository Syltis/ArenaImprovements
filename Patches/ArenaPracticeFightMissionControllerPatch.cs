﻿using HarmonyLib;
using SandBox.Missions.MissionLogics.Arena;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Linq;

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
                    
                    if (ArenaConfig.SelectedWeaponType.WeaponType is ArenaWeaponTypesEnum.All)
                    {
                        equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                        continue;
                    }
                    
                    if (equipmentFromSlot.Item is null) 
                        continue;

                    var item = equipmentFromSlot.Item;

                    if (ArenaConfig.SelectedWeaponType.WeaponType is ArenaWeaponTypesEnum.RangedOnly)
                    {
                        if (item.PrimaryWeapon.IsRangedWeapon)
                            equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                        else if (item.PrimaryWeapon.IsAmmo)
                        {
                            equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                            equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                        }
                    }
                    else if (ArenaConfig.SelectedWeaponType.WeaponType is ArenaWeaponTypesEnum.MeleeOnly)
                    {
                        if (item.PrimaryWeapon.IsMeleeWeapon)
                            equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
                    }
                }
                return false;
            }
            return true;
        }
    }
}
