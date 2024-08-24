using STRINGS;

namespace PalmeraTree
{
    class STRINGS
    {
        public class FOOD
        {
            public class STEAMEDPALMERABERRY
            {
                public static LocString NAME = UI.FormatAsLink("Steamed Palmera Berry", SteamedPalmeraBerryConfig.Id);
                public static LocString DESC = $"The steamed bud of a {UI.FormatAsLink(CROPS.PALMERABERRY.NAME, PalmeraBerryConfig.Id)}.\n\nLong exposure to heat and exquisite cooking skills turn the toxic berry into a delicious dessert.";
                public static LocString RECIPEDESC = $"Delicious steamed {UI.FormatAsLink(CROPS.PALMERABERRY.NAME, PalmeraBerryConfig.Id)}.";
            }
        }

        public class SEEDS
        {
            public class PALMERATREE
            {
                public static LocString NAME = UI.FormatAsLink("Palmera Tree Seed", PalmeraTreeConfig.Id);
                public static LocString DESC = $"The {UI.FormatAsLink("Seed", "PLANTS")} of a {UI.FormatAsLink(PLANTS.PALMERATREE.NAME, PalmeraTreeConfig.Id)}.";
                public static LocString RECIPEDESC = "What will happen if you mash some organic mass together?";
            }
        }

        public class PLANTS
        {
            public class PALMERATREE
            {
                public static LocString NAME = UI.FormatAsLink("Palmera Tree", PalmeraTreeConfig.Id);
                public static LocString DESC = $"A large, {UI.FormatAsLink("chlorine", "CHLORINEGAS")}-dwelling {UI.FormatAsLink("Plant", "PLANTS")} that can be grown in farm buildings.\n\nPalmeras grow inedible buds that emit toxic {UI.FormatAsLink("hydrogen", "HYDROGEN")} gas.";
                public static LocString DOMESTICATEDDESC = $"A large, chlorine-dwelling {UI.FormatAsLink("Plant", "PLANTS")} that grows inedible buds which emit toxic hydrogen gas.";
            }
        }

        public class CROPS
        {
            public class PALMERABERRY
            {
                public static LocString NAME = UI.FormatAsLink("Palmera Berry", PalmeraBerryConfig.Id);
                public static LocString DESC = $"A toxic, non-edible bud that emits {UI.FormatAsLink("hydrogen", "HYDROGEN")}.";
            }
        }

        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = "Cairath";
            }
        }
    }
}
