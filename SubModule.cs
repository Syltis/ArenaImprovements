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
            InformationManager.DisplayMessage(new InformationMessage("ArenaImprovements test project loaded", Color.ConvertStringToColor("#FF0042FF")));

        }
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

        }

        protected override void OnSubModuleUnloaded()
        {
            base.OnSubModuleUnloaded();

        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

        }
    }
}