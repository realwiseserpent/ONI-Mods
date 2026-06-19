using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
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

                if ((temp = CreateCodex(FervineConfig.Id, speciesNameTemplate, $"STRINGS.CODEX.BLISSBURST.SUBTITLE",
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
    }
}
