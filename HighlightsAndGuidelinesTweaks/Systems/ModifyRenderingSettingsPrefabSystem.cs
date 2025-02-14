// <copyright file="ModifyRenderingSettingsPrefabSystem.cs" company="Yenyangs Mods. MIT License">
// Copyright (c) Yenyangs Mods. MIT License. All rights reserved.
// </copyright>

namespace HighlightsAndGuidelinesTweaks.Systems
{
    using Colossal.Entities;
    using Colossal.Logging;
    using Colossal.Serialization.Entities;
    using Game;
    using Game.Prefabs;
    using Unity.Entities;

    /// <summary>
    /// Modifies the rending and guidelines setting components on rendering settings prefab.
    /// </summary>
    public partial class ModifyRenderingSettingsPrefabSystem : GameSystemBase
    {
        public readonly PrefabID RenderingSettingsPrefab = new PrefabID(nameof(RenderingSettingsPrefab), "RenderingSettings");

        private ILog m_Log;
        private PrefabSystem m_PrefabSystem;

        /// <summary>
        /// Sets the rendering settings data component on the rendering settings prefab.
        /// </summary>
        public void SetRenderingSettingsData(RenderingSettingsData newRenderingSettingsData)
        {
            if (m_PrefabSystem.TryGetPrefab(RenderingSettingsPrefab, out PrefabBase prefab)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.HasComponent<RenderingSettingsData>(prefabEntity))
            {
                EntityManager.SetComponentData(prefabEntity, newRenderingSettingsData);
            }            

            m_Log.Info($"{nameof(ModifyRenderingSettingsPrefabSystem)}.{nameof(SetRenderingSettingsData)} Complete.");
        }

        /// <summary>
        /// Sets the guidelines Settings settings data component on the rendering settings prefab.
        /// </summary>
        public void SetGuideLineSettingsData(GuideLineSettingsData newGuideLinesSettingsData)
        {
            if (m_PrefabSystem.TryGetPrefab(RenderingSettingsPrefab, out PrefabBase prefab)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.HasComponent<GuideLineSettingsData>(prefabEntity))
            {
                EntityManager.SetComponentData(prefabEntity, newGuideLinesSettingsData);
            }

            m_Log.Info($"{nameof(ModifyRenderingSettingsPrefabSystem)}.{nameof(SetGuideLineSettingsData)} Complete.");
        }


        /// <summary>
        /// Needs to be modified to reset prefab components related to this project.
        /// </summary>
        public void ResetFireProbability(PrefabID prefabID)
        {
            if (m_PrefabSystem.TryGetPrefab(prefabID, out PrefabBase prefab) 
                && prefab.TryGet(out Game.Prefabs.Fire fire)
                && m_PrefabSystem.TryGetEntity(prefab, out Entity prefabEntity)
                && EntityManager.TryGetComponent(prefabEntity, out Game.Prefabs.FireData fireData))
            {
                fireData.m_StartProbability = fire.m_StartProbability;
                EntityManager.SetComponentData(prefabEntity, fireData);
            }

            m_Log.Info($"{nameof(ModifyRenderingSettingsPrefabSystem)}.{nameof(ResetFireProbability)} Complete.");
        }

        /// <inheritdoc/>
        protected override void OnCreate()
        {
            base.OnCreate();
            m_Log = Mod.log;
            m_Log.Info($"{nameof(ModifyRenderingSettingsPrefabSystem)}.OnCreate");

            m_PrefabSystem = World.GetOrCreateSystemManaged<PrefabSystem>();

            Enabled = false;
        }

        /// <inheritdoc/>
        protected override void OnUpdate()
        {
            return;
        }

        // Hard coded. Proof of concept.
        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            RenderingSettingsData renderingSettingsData = new RenderingSettingsData()
            {
                m_ErrorColor = new UnityEngine.Color(0.25f, 0.35f, 0.85f, 0.25f),
                m_HoveredColor = new UnityEngine.Color(0.25f, 0.15f, 0.25f, 0.01f),
                m_OverrideColor = new UnityEngine.Color(1f, 1f, 0.5f, 0.4f),
                m_OwnerColor = new UnityEngine.Color(1f, 0.5f, 0.5f, 0.4f),
                m_WarningColor = new UnityEngine.Color(0.247f, 0.981f, 0.247f, 0.1f),
            };

            GuideLineSettingsData guideLineSettingsData = new GuideLineSettingsData()
            {
                m_HighPriorityColor = new UnityEngine.Color(1f, 1f, 1f, 0.05f),
                m_LowPriorityColor = new UnityEngine.Color(0.502f, 0.869f, 1.0f, 0.25f),
                m_MediumPriorityColor = new UnityEngine.Color(0.753f, 0.753f, 0.753f, 0.55f),
                m_VeryLowPriorityColor = new UnityEngine.Color(0.695f, 0.877f, 1.00f, 0.584f),
            };

            SetGuideLineSettingsData(guideLineSettingsData);
            SetRenderingSettingsData(renderingSettingsData);
        }
    }
}
