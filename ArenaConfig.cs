using System.Collections.Generic;
using System.Linq;

namespace ArenaImprovements
{
    public class ArenaConfig
    {
        public static List<ArenaWeaponType> ArenaWeaponTypes = new()
        {
            new ArenaWeaponType { WeaponType = ArenaWeaponTypesEnum.RangedOnly, Name = "ranged only" },
            new ArenaWeaponType { WeaponType = ArenaWeaponTypesEnum.MeleeOnly, Name = "melee only" },
            new ArenaWeaponType { WeaponType = ArenaWeaponTypesEnum.All, Name = "all" },
        };

        public static ArenaWeaponType SelectedWeaponType = ArenaWeaponTypes.First(x => x.WeaponType == ArenaWeaponTypesEnum.All);

        public static void NextWeaponType()
        {
            int index = ArenaWeaponTypes.IndexOf(SelectedWeaponType);
            if (index == ArenaWeaponTypes.Count - 1)
                SelectedWeaponType = ArenaWeaponTypes[0];
            else
                SelectedWeaponType= ArenaWeaponTypes[index + 1];
        }
    }

    public enum ArenaWeaponTypesEnum
    {
        RangedOnly,
        MeleeOnly,
        All
    }

    public class ArenaWeaponType
    {
        public ArenaWeaponTypesEnum WeaponType { get; set; }

        public string Name;
    }
}