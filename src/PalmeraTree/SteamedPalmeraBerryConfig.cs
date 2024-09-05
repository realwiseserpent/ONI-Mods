﻿using STRINGS;
using UnityEngine;
using static CaiLib.Utils.RecipeUtils;

namespace PalmeraTree
{
	public class SteamedPalmeraBerryConfig : IEntityConfig
	{
		public const string Id = "SteamedPalmeraBerry";

		public ComplexRecipe Recipe;

		public GameObject CreatePrefab()
		{
			var entity = EntityTemplates.CreateLooseEntity(
				id: Id,
				name: STRINGS.FOOD.STEAMEDPALMERABERRY.NAME,
				desc: STRINGS.FOOD.STEAMEDPALMERABERRY.DESC,
                mass: 1f,
				unitMass: false,
				anim: Assets.GetAnim("kukumelon_kanim"),
				initialAnim: "object",
				sceneLayer: Grid.SceneLayer.Front,
				collisionShape: EntityTemplates.CollisionShape.RECTANGLE,
				width: 0.8f,
				height: 0.7f,
				isPickupable: true);

			var foodInfo = new EdiblesManager.FoodInfo(
				id: Id,
				dlcId: "",
				caloriesPerUnit: 2000000f,
				quality: 4,
				preserveTemperatue: 255.15f,
				rotTemperature: 277.15f,
				spoilTime: TUNING.FOOD.SPOIL_TIME.SLOW,
				can_rot: true);

			var food = EntityTemplates.ExtendEntityToFood(entity, foodInfo);

            Recipe = AddComplexRecipe(
				input: new[] {
					new ComplexRecipe.RecipeElement(PalmeraBerryConfig.Id, 1f),
					new ComplexRecipe.RecipeElement(SwampLilyFlowerConfig.ID, 1f),
                },
				output: new[] {new ComplexRecipe.RecipeElement(SteamedPalmeraBerryConfig.Id, 1f)},
				fabricatorId: GourmetCookingStationConfig.ID,
				productionTime: 100f,
				recipeDescription: STRINGS.FOOD.STEAMEDPALMERABERRY.RECIPEDESC,
				nameDisplayType: ComplexRecipe.RecipeNameDisplay.Result,
				sortOrder: 120
			);

			return food;
		}

		public void OnPrefabInit(GameObject inst)
		{
		}

		public void OnSpawn(GameObject inst)
		{
		}

		public string[] GetDlcIds()
		{
			return DlcManager.AVAILABLE_ALL_VERSIONS;
		}
	}
}
