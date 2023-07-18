using SandBox.CampaignBehaviors;
using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;

namespace ArenaImprovements.Behaviors
{
    public class ArenaImprovementsBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
        }

        private void OnSessionLaunched(CampaignGameStarter starter)
        {
            AddDialogs(starter);
        }

        protected void AddDialogs(CampaignGameStarter starter)
        {
            starter.AddPlayerLine(
                id: "arena_master_ask_for_melee_practice_fight_fight",
                inputToken: "arena_master_talk",
                outputToken: "close_window",
                text: "{=arena_master_26}I'd like to participate in a melee practice fight...",
                conditionDelegate: null,
                consequenceDelegate: new ConversationSentence.OnConsequenceDelegate(conversation_arena_join_melee_fight_on_consequence),
                priority: 100,
                clickableConditionDelegate: null,
                persuasionOptionDelegate: null);
        }

        public override void SyncData(IDataStore dataStore) { }

        public static void conversation_arena_join_melee_fight_on_consequence()
        {
            ArenaConfig.MeleeOnly = true;
            InformationManager.DisplayMessage(new InformationMessage($"MeleeOnly set to False {DateTime.Now:HH:mm}", Color.ConvertStringToColor("#FF0042FF")));

            ArenaMasterCampaignBehavior.conversation_arena_join_fight_on_consequence();
        }

        private enum ArenaType
        {
            Standard,
            MeleeOnly,
            RangedOnly
        }
    }
}
