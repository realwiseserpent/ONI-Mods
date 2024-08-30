using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

namespace Fervine
{
	public class FervineConfig : IEntityConfig
	{
		public const string Id = "Fervine";
		public const string SeedId = "FervineBulb";

        private const string AnimName = "fervine_kanim";
        private const string AnimNameSeed = "seed_fervine_kanim";

		public GameObject CreatePrefab()
		{
			var plantEntityTemplate = EntityTemplates.CreatePlacedEntity(
				id: Id,
				name: STRINGS.PLANTS.FERVINE.NAME,
				desc: STRINGS.PLANTS.FERVINE.DESC,
				width: 1,
				height: 1,
				mass: 1f,
				anim: Assets.GetAnim(AnimName),
				initialAnim: "close",
				sceneLayer: Grid.SceneLayer.BuildingFront,
				decor: DECOR.BONUS.TIER3,
				defaultTemperature: 350f);

			EntityTemplates.ExtendEntityToBasicPlant(
				template: plantEntityTemplate,
				temperature_lethal_low: 258.15f,
				temperature_warning_low: 288.15f,
				temperature_warning_high: 363.15f,
				temperature_lethal_high: 393.15f,
				pressure_sensitive: false,
				can_tinker: false,
				baseTraitId: $"{Id}Original",
				baseTraitName: STRINGS.PLANTS.FERVINE.NAME);

            var light2D = plantEntityTemplate.AddOrGet<Light2D>();
            light2D.Color = new Color(1f, 1f, 0f);
            light2D.Lux = 1800;
            light2D.Range = 5;

            plantEntityTemplate.AddOrGet<Fervine>();

			var seed = EntityTemplates.CreateAndRegisterSeedForPlant(
				plant: plantEntityTemplate,
				id: SeedId,
				name: STRINGS.SEEDS.FERVINE.NAME,
				desc: STRINGS.SEEDS.FERVINE.DESC,
				productionType: SeedProducer.ProductionType.Hidden,
				anim: Assets.GetAnim(AnimNameSeed),
				additionalTags: new List<Tag> { GameTags.DecorSeed },
				sortOrder: 6,
				width: 0.33f,
				height: 0.33f,
				domesticatedDescription: STRINGS.PLANTS.FERVINE.DOMESTICATEDDESC);

            EntityTemplates.CreateAndRegisterPreviewForPlant(
                seed: seed,
                id: "Heatbulb_preview",
                anim: Assets.GetAnim(AnimName),
                initialAnim: "place",
                width: 1,
                height: 1);

            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);

            return plantEntityTemplate;
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
