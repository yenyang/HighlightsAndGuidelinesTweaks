using Colossal.Logging;
using Game;
using Game.Input;
using Game.Modding;
using Game.SceneFlow;
using HighlightsAndGuidelinesTweaks.Systems;
using Unity.Entities;

namespace HighlightsAndGuidelinesTweaks
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(HighlightsAndGuidelinesTweaks)}.{nameof(Mod)}").SetShowsErrorsInUI(false);
        public static ProxyAction m_ButtonAction;
        public static ProxyAction m_AxisAction;
        public static ProxyAction m_VectorAction;

        public const string kButtonActionName = "ButtonBinding";
        public const string kAxisActionName = "FloatBinding";
        public const string kVectorActionName = "Vector2Binding";

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            // Initialize Systems
            ModifyRenderingSettingsPrefabSystem modifyRenderingSettingsPrefabSystem = World.DefaultGameObjectInjectionWorld?.GetOrCreateSystemManaged<ModifyRenderingSettingsPrefabSystem>();
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}
