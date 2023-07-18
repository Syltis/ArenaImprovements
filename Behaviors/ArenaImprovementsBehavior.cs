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
                id: "arena_master_ask_for_melee_practice_type",
                inputToken: "arena_master_talk",
                outputToken: "arena_master_ask_for_melee_practice_type_response",
                text: "{=arena_master_26} I want to change weapon type.",
                conditionDelegate: null,
                consequenceDelegate: new ConversationSentence.OnConsequenceDelegate(conversation_arena_set_melee_fight_type_on_consequence),
                priority: 100,
                clickableConditionDelegate: null,
                persuasionOptionDelegate: null);

            starter.AddDialogLine(
                "arena_master_ask_for_melee_practice_type",
                "arena_master_ask_for_melee_practice_type_response",
                "arena_master_talk",
                "{=arena_master_32}That is ok.",
                null,
                null,
                1,
                null);
        }

        public override void SyncData(IDataStore dataStore) { }

        public static void conversation_arena_set_melee_fight_type_on_consequence()
        {
            ArenaConfig.NextWeaponType();
            InformationManager.DisplayMessage(new InformationMessage($"Weapontype set to {ArenaConfig.SelectedWeaponType.Name}", Color.White));
        }
    }
}
