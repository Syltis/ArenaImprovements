using ArenaImprovements.Behaviors;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ArenaImprovements
{
    public class SubModule : MBSubModuleBase
    {
        public override void OnGameInitializationFinished(Game game)
        {
            base.OnGameInitializationFinished(game);
            InformationManager.DisplayMessage(new InformationMessage("ArenaImprovements by Dahl loaded", Color.ConvertStringToColor("#CC9900")));
        }
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            var harmony = new Harmony("Sylte.Bannerlord.ArenaImprovements");
            harmony.PatchAll();
        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }

        protected override void InitializeGameStarter(Game game, IGameStarter starterObject)
        {
            if (starterObject is CampaignGameStarter starter)
            {
                starter.AddBehavior(new ArenaImprovementsBehavior());
            }
        }
    }
}