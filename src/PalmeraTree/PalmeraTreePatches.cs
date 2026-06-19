using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
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
				AddCropType(PalmeraBerryConfig.Id, 20, 4);
			}
		}

		[HarmonyPatch(typeof(Immigration))]
		[HarmonyPatch("ConfigureCarePackages")]
		public static class Immigration_ConfigureCarePackages_Patch
		{
			public static void Postfix(ref Immigration __instance)
			{
				AddCarePackage(ref __instance, PalmeraTreeConfig.SeedId, 1f, () => CycleCondition(48));
                AddCarePackage(ref __instance, SteamedPalmeraBerryConfig.Id, 2f, () => DiscoveredResources.Instance.IsDiscovered(PalmeraTreeConfig.SeedId));
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

        [HarmonyPatch(typeof(CodexCache), "CollectEntries")]
        public class CodexCache_CollectEntries_Patch
        {
            public static void Postfix(string folder, List<CodexEntry> __result)
            {
                if (folder != string.Empty)
                    return;

                string speciesNameTemplate = "STRINGS.CREATURES.SPECIES.{0}.NAME";
                string speciesDescTemplate = "STRINGS.CREATURES.SPECIES.{0}.DESC";

                CodexEntry temp;

                if ((temp = CreateCodex(PalmeraTreeConfig.Id, speciesNameTemplate, $"STRINGS.CODEX.MEALWOOD.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
            }
        }

        private static CodexEntry CreateCodex(string id, string title, string subtitle, string body,
            string category, bool cutVersion = false)
        {
            GameObject go = Assets.GetPrefab(id);

            if (go == null)
                return null;

            List<ContentContainer> containers = new List<ContentContainer>
            {
                new ContentContainer(new List<ICodexWidget>()
                    {
                        new CodexText() { stringKey = string.Format(title, id.ToUpperInvariant()), style = CodexTextStyle.Title },
                        new CodexText() { stringKey = string.Format(subtitle, id.ToUpperInvariant()), style = CodexTextStyle.Subtitle },
                        new CodexDividerLine()
                    }, ContentContainer.ContentLayout.Vertical)
            };

            Sprite first = Def.GetUISprite(go).first;

            if (!cutVersion)
                CodexEntryGenerator.GenerateImageContainers(first, containers);

            List<ICodexWidget> content = new List<ICodexWidget>
            {
                new CodexText()
                {
                    stringKey = string.Format(body, id.ToUpperInvariant()),
                    style = CodexTextStyle.Body
                }
            };
            ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
            containers.Add(contentContainer);

            CodexEntry entry = new CodexEntry(category, containers, go.GetProperName());

            entry.icon = first;

            entry.id = id;
            entry.disabled = false;

            if (!cutVersion)
                entry.contentMadeAndUsed.Add(new CodexEntry_MadeAndUsed() { tag = id });

            return entry;
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
