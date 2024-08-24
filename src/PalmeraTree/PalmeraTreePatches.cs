using HarmonyLib;
using static CaiLib.Utils.CarePackagesUtils;
using static CaiLib.Utils.PlantUtils;
using static CaiLib.Utils.RecipeUtils;
using static CaiLib.Utils.StringUtils;

namespace PalmeraTree
{
	public class PalmeraTreePatches
	{
		[HarmonyPatch(typeof(EntityConfigManager))]
		[HarmonyPatch(nameof(EntityConfigManager.LoadGeneratedEntities))]
		public class EntityConfigManager_LoadGeneratedEntities_Patch
		{
			public static void Prefix()
			{
				AddPlantStrings(PalmeraTreeConfig.Id, STRINGS.PLANTS.PALMERATREE.NAME, STRINGS.PLANTS.PALMERATREE.DESC, STRINGS.PLANTS.PALMERATREE.DOMESTICATEDDESC);
				AddPlantSeedStrings(PalmeraTreeConfig.Id, STRINGS.SEEDS.PALMERATREE.NAME, STRINGS.SEEDS.PALMERATREE.DESC);
				AddFoodStrings(SteamedPalmeraBerryConfig.Id, STRINGS.FOOD.STEAMEDPALMERABERRY.NAME, STRINGS.FOOD.STEAMEDPALMERABERRY.DESC, STRINGS.FOOD.STEAMEDPALMERABERRY.RECIPEDESC);
				AddFoodStrings(PalmeraBerryConfig.Id, STRINGS.CROPS.PALMERABERRY.NAME, STRINGS.CROPS.PALMERABERRY.DESC);
				AddCropType(PalmeraBerryConfig.Id, 20, 10);
			}
		}

		[HarmonyPatch(typeof(Immigration))]
		[HarmonyPatch("ConfigureCarePackages")]
		public static class Immigration_ConfigureCarePackages_Patch
		{
			public static void Postfix(ref Immigration __instance)
			{
				AddCarePackage(ref __instance, PalmeraTreeConfig.SeedId, 1f, () => CycleCondition(48));
			}
		}

		[HarmonyPatch(typeof(SupermaterialRefineryConfig))]
		[HarmonyPatch("ConfigureBuildingTemplate")]
		public class SupermaterialRefineryConfig_ConfigureBuildingTemplate_Patch
		{
			public static void Postfix()
			{
				AddComplexRecipe(
					input: new[] {
						new ComplexRecipe.RecipeElement(BasicSingleHarvestPlantConfig.SEED_ID.ToTag(), 10f),
						new ComplexRecipe.RecipeElement(BasicFabricConfig.ID.ToTag(), 10f)
					},
					output: new[] { new ComplexRecipe.RecipeElement(TagManager.Create(PalmeraTreeConfig.SeedId), 1f) },
					fabricatorId: SupermaterialRefineryConfig.ID,
					productionTime: 50f,
					recipeDescription: STRINGS.SEEDS.PALMERATREE.RECIPEDESC,
					nameDisplayType: ComplexRecipe.RecipeNameDisplay.Result,
					sortOrder: 1000
				);
			}
		}

		// seems not needed anymore?
		//      [HarmonyPatch(typeof(KSerialization.Manager))]
		//      [HarmonyPatch("GetType")]
		//      [HarmonyPatch(new[] { typeof(string) })]
		//      public static class KSerializationManager_GetType_Patch
		//      {
		//          public static void Postfix(string type_name, ref Type __result)
		//          {
		//              if (type_name == "PalmeraTree.PalmeraTree")
		//              {
		//                  __result = typeof(PalmeraTree);
		//              }
		//          }
		//      }
	}
}
