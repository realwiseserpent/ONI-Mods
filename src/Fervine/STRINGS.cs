using STRINGS;

namespace Fervine
{
    class STRINGS
    {
        public class SEEDS
        {
            public class FERVINE
            {
                public static LocString NAME = UI.FormatAsLink("Fervine Bulb", FervineConfig.Id);
                public static LocString DESC = $"The {UI.FormatAsLink("Seed", "PLANTS")} of a {UI.FormatAsLink(PLANTS.FERVINE.NAME, FervineConfig.Id)}.";
                public static LocString RECIPE_DESC = "Plant + shiny = ?";
            }
        }

        public class PLANTS
        {
            public class FERVINE
            {
                public static LocString NAME = UI.FormatAsLink("Fervine", FervineConfig.Id);
                public static LocString DESC = $"Fervine uses tiny amounts of heat energy from the atmosphere to keep its light up. " +
                                                               $"It won't use any if the temperature falls under {GameUtil.GetFormattedTemperature(293.15f)}.";
                public static LocString DOMESTICATEDDESC = $"A temperature reactive, subterranean {UI.FormatAsLink("Plant", "PLANTS")}.";
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
