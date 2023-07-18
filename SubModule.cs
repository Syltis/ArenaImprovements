using ArenaImprovements.Behaviors;
using HarmonyLib;
using SandBox.Missions.MissionLogics.Arena;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.MissionViews;

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

        public override void OnMissionBehaviorInitialize(Mission mission)
        {
            base.OnMissionBehaviorInitialize(mission);
            if (!mission.HasMissionBehavior<ArenaPracticeFightMissionController>())
                return;

            mission.AddMissionBehavior(new ArenaImprovementsMissionBehavior());
        }

        public class ArenaImprovementsMissionBehavior : MissionView
        {
            public override void OnMissionScreenInitialize()
            {
                base.OnMissionScreenInitialize();

                // Handle reset meleeonly

                InformationManager.DisplayMessage(new InformationMessage("Mission screen intitialize", Color.ConvertStringToColor("#FF0042FF")));
                InformationManager.DisplayMessage(new InformationMessage(DateTime.Now.ToString("HH:mm"), Color.ConvertStringToColor("#FF0042FF")));

            }
        }
    }
}