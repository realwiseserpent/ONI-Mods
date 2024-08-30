using HarmonyLib;
using static CaiLib.Utils.CarePackagesUtils;
using static CaiLib.Utils.RecipeUtils;
using static CaiLib.Utils.StringUtils;

namespace Fervine
{
	public class FervinePatches
	{
		[HarmonyPatch(typeof(EntityConfigManager))]
		[HarmonyPatch("LoadGeneratedEntities")]
		public static class EntityConfigManager_LoadGeneratedEntities_Patch
		{
			public static void Prefix()
			{
				AddPlantStrings(FervineConfig.Id, STRINGS.PLANTS.FERVINE.NAME, STRINGS.PLANTS.FERVINE.DESC, STRINGS.PLANTS.FERVINE.DOMESTICATEDDESC);
				AddPlantSeedStrings(FervineConfig.Id, STRINGS.SEEDS.FERVINE.NAME, STRINGS.SEEDS.FERVINE.DESC);
			}
		}

		[HarmonyPatch(typeof(Immigration))]
		[HarmonyPatch("ConfigureCarePackages")]
		public static class Immigration_ConfigureCarePackages_Patch
		{
			public static void Postfix(ref Immigration __instance)
			{
				AddCarePackage(ref __instance, FervineConfig.SeedId, 1f);
			}
		}

		[HarmonyPatch(typeof(SupermaterialRefineryConfig))]
		[HarmonyPatch("ConfigureBuildingTemplate")]
		public class SupermaterialRefineryConfig_ConfigureBuildingTemplate_Patch
		{
			public static void Postfix()
			{
				AddComplexRecipe(
					input: new[]
					{
						new ComplexRecipe.RecipeElement(SimHashes.Diamond.CreateTag(), 50f),
						new ComplexRecipe.RecipeElement(BasicFabricConfig.ID.ToTag(), 10f)
					},
					output: new[]
					{
						new ComplexRecipe.RecipeElement(TagManager.Create(FervineConfig.SeedId), 1f)
					},
					fabricatorId: SupermaterialRefineryConfig.ID,
					productionTime: 50f,
					recipeDescription: STRINGS.SEEDS.FERVINE.RECIPE_DESC,
					nameDisplayType: ComplexRecipe.RecipeNameDisplay.Result,
					sortOrder: 1000
				);
			}
		}
	}
}
