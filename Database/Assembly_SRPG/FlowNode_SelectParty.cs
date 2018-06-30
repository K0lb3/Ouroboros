namespace SRPG
{
    using GR;
    using System;

    [Pin(100, "Out", 1, 0x3e9), Pin(0x97, "SaveTeamID", 0, 0x97), Pin(0x3e8, "ApplyToPlayerData", 0, 0x3e8), Pin(1, "Select Team", 0, 1), Pin(150, "LoadTeamID", 0, 150), NodeType("UI/SelectParty", 0x7fe5)]
    public class FlowNode_SelectParty : FlowNode
    {
        public PartyTypes PartyType;
        public int PartyIndex;

        public FlowNode_SelectParty()
        {
            base..ctor();
            return;
        }

        public static void LoadTeamID(PartyTypes type)
        {
            int num;
            PartyTypes types;
            types = type;
            switch (types)
            {
                case 0:
                    goto Label_0021;

                case 1:
                    goto Label_0054;

                case 2:
                    goto Label_0087;

                case 3:
                    goto Label_00BA;

                case 4:
                    goto Label_00DA;
            }
            goto Label_010D;
        Label_0021:
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.TEAM_ID_KEY, 0);
            num = ((num >= 0) && (num < 11)) ? num : 0;
            GlobalVars.SelectedPartyIndex.Set(num);
            goto Label_010D;
        Label_0054:
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.MULTI_PLAY_TEAM_ID_KEY, 0);
            num = ((num >= 0) && (num < 11)) ? num : 0;
            GlobalVars.SelectedPartyIndex.Set(num);
            goto Label_010D;
        Label_0087:
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.ARENA_TEAM_ID_KEY, 0);
            num = ((num >= 0) && (num < 11)) ? num : 0;
            GlobalVars.SelectedPartyIndex.Set(num);
            goto Label_010D;
        Label_00BA:
            num = MonoSingleton<GameManager>.Instance.Player.GetDefensePartyIndex();
            GlobalVars.SelectedPartyIndex.Set(num);
            goto Label_010D;
        Label_00DA:
            num = PlayerPrefsUtility.GetInt(PlayerPrefsUtility.RANKMATCH_TEAM_ID_KEY, 0);
        Label_00F5:
            num = ((num >= 0) && (num < 11)) ? num : 0;
            GlobalVars.SelectedPartyIndex.Set(num);
        Label_010D:
            return;
        }

        public override void OnActivate(int pinID)
        {
            if (pinID != 1)
            {
                goto Label_0025;
            }
            GlobalVars.SelectedPartyIndex.Set(this.PartyIndex);
            base.ActivateOutputLinks(100);
            goto Label_0095;
        Label_0025:
            if (pinID != 150)
            {
                goto Label_0049;
            }
            LoadTeamID(this.PartyType);
            base.ActivateOutputLinks(100);
            goto Label_0095;
        Label_0049:
            if (pinID != 0x97)
            {
                goto Label_0068;
            }
            this.SaveTeamID();
            base.ActivateOutputLinks(100);
            goto Label_0095;
        Label_0068:
            if (pinID != 0x3e8)
            {
                goto Label_0095;
            }
            MonoSingleton<GameManager>.Instance.Player.SetPartyCurrentIndex(GlobalVars.SelectedPartyIndex);
            base.ActivateOutputLinks(100);
        Label_0095:
            return;
        }

        private void SaveTeamID()
        {
            PartyTypes types;
            switch (this.PartyType)
            {
                case 0:
                    goto Label_0026;

                case 1:
                    goto Label_0041;

                case 2:
                    goto Label_005C;

                case 3:
                    goto Label_0077;

                case 4:
                    goto Label_0095;
            }
            goto Label_00B0;
        Label_0026:
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.TEAM_ID_KEY, GlobalVars.SelectedPartyIndex, 0);
            goto Label_00B0;
        Label_0041:
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.MULTI_PLAY_TEAM_ID_KEY, GlobalVars.SelectedPartyIndex, 0);
            goto Label_00B0;
        Label_005C:
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.ARENA_TEAM_ID_KEY, GlobalVars.SelectedPartyIndex, 0);
            goto Label_00B0;
        Label_0077:
            MonoSingleton<GameManager>.Instance.Player.SetDefenseParty(GlobalVars.SelectedPartyIndex);
            goto Label_00B0;
        Label_0095:
            PlayerPrefsUtility.SetInt(PlayerPrefsUtility.RANKMATCH_TEAM_ID_KEY, GlobalVars.SelectedPartyIndex, 0);
        Label_00B0:
            return;
        }

        public enum PartyTypes
        {
            Normal,
            Multi,
            Arena,
            ArenaDefense,
            RankMatch
        }
    }
}

