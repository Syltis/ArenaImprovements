using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using System;

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
                "{=arena_master_31}{NEARBY_TOURNAMENT_STRING}",
                new ConversationSentence.OnConditionDelegate(conversation_tournament_soon_on_condition),
                null,
                100,
                null);
        }

        public override void SyncData(IDataStore dataStore) { }

        public static void conversation_arena_set_melee_fight_type_on_consequence() => ArenaConfig.NextWeaponType();
        
        public static bool conversation_tournament_soon_on_condition()
        {
            string text = string.Empty;
            switch (ArenaConfig.SelectedWeaponType.WeaponType)
            {
                case ArenaWeaponTypesEnum.All:
                    text = "Ok, let the guys downstairs know you'll enter a fight using all available weapons.";
                    break;
                case ArenaWeaponTypesEnum.MeleeOnly:
                    text = "Ok, let the guys downstairs know you'll enter a fight using melee only weapons.";
                    break;
                case ArenaWeaponTypesEnum.RangedOnly:
                    text = "Ok, let the guys downstairs know you'll enter a fight using ranged only weapons.";
                    break;
                default:
                    throw new InvalidOperationException("Missing switch path for weapontype.");
            }
            MBTextManager.SetTextVariable("NEARBY_TOURNAMENT_STRING", text, false);
            return true;
        }
    }
}
