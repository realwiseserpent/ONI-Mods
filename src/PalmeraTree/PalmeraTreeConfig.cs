using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

namespace PalmeraTree
{
    public class PalmeraTreeConfig : IEntityConfig
    {
        public const string Id = "PalmeraTreePlant";
        public const string SeedId = "PalmeraTreeSeed";

        private const string AnimName = "custom_palmeratree_kanim";
        private const string AnimNameSeed = "seed_palmeratree_kanim";

        public GameObject CreatePrefab()
        {
            var placedEntity = EntityTemplates.CreatePlacedEntity(
                id: Id,
                name: STRINGS.PLANTS.PALMERATREE.NAME,
                desc: STRINGS.PLANTS.PALMERATREE.DESC,
                mass: 1f,
                anim: Assets.GetAnim(AnimName),
                initialAnim: "idle_loop",
                sceneLayer: Grid.SceneLayer.BuildingFront,
                width: 1,
                height: 3,
                decor: DECOR.BONUS.TIER2,
                defaultTemperature: 350f);

            EntityTemplates.ExtendEntityToBasicPlant(
                template: placedEntity,
                temperature_lethal_low: 258.15f,
                temperature_warning_low: 323.15f,
                temperature_warning_high: 363.15f,
                temperature_lethal_high: 398.15f,
                safe_elements: new[] { SimHashes.ChlorineGas },
                crop_id: PalmeraBerryConfig.Id,
                baseTraitId: $"{Id}Original",
                baseTraitName: STRINGS.PLANTS.PALMERATREE.NAME);

            placedEntity.AddOrGet<PalmeraTree>();

            var consumer = placedEntity.AddOrGet<ElementConsumer>();
            consumer.elementToConsume = SimHashes.ChlorineGas;
            consumer.consumptionRate = 0.001f;

            var emitter = placedEntity.AddOrGet<ElementEmitter>();
            emitter.outputElement = new ElementConverter.OutputElement(0.001f, SimHashes.Hydrogen, 0f, true, false, 0f, 2f);
            emitter.maxPressure = 1.8f;

            var seed = EntityTemplates.CreateAndRegisterSeedForPlant(
                plant: placedEntity,
                id: SeedId,
                name: STRINGS.SEEDS.PALMERATREE.NAME,
                desc: STRINGS.SEEDS.PALMERATREE.DESC,
                productionType: SeedProducer.ProductionType.Harvest,
                anim: Assets.GetAnim(AnimNameSeed),
                additionalTags: new List<Tag> { GameTags.CropSeed },
                sortOrder: 7,
                domesticatedDescription: STRINGS.PLANTS.PALMERATREE.DOMESTICATEDDESC,
                width: 0.33f,
                height: 0.33f);

            EntityTemplates.CreateAndRegisterPreviewForPlant(
                seed: seed,
                id: "PalmeraTree_preview",
                anim: Assets.GetAnim(AnimName),
                initialAnim: "place",
                width: 2,
                height: 3);

            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER1);

            //===> SOLID FERTILIZER THIS CROP REQUIRES <============================================================================
            EntityTemplates.ExtendPlantToFertilizable(placedEntity, new PlantElementAbsorber.ConsumeInfo[]
            {
                new PlantElementAbsorber.ConsumeInfo
                {
                    tag = SimHashes.Phosphorite.CreateTag(),
                    massConsumptionRate = 25/600f
                }
            });

            ////===> LIQUID IRRIGATION THIS CROP REQUIRES <===========================================================================
            //EntityTemplates.ExtendPlantToIrrigated(placedEntity, new PlantElementAbsorber.ConsumeInfo[]
            //{ 
            //    new PlantElementAbsorber.ConsumeInfo
            //    {
            //        tag = SimHashes.LiquidPhosphorus.CreateTag(),
            //        massConsumptionRate = 3/600f
            //    }
            //});

            return placedEntity;
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
