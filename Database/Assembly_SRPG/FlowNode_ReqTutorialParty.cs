namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;

    [NodeType("Tutorial/StartParty"), Pin(0, "Request", 0, 0), Pin(10, "Success", 1, 10)]
    public class FlowNode_ReqTutorialParty : FlowNode_Network
    {
        public FlowNode_ReqTutorialParty()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            PlayerData data;
            PlayerPartyTypes types;
            PartyData data2;
            List<UnitData> list;
            List<UnitData> list2;
            int num;
            int num2;
            int num3;
            long num4;
            if (pinID != null)
            {
                goto Label_011B;
            }
            data = MonoSingleton<GameManager>.Instance.Player;
            types = 0;
            data2 = data.FindPartyOfType(types);
            list = data.Units;
            list2 = new List<UnitData>();
            num = data2.MAINMEMBER_START;
            goto Label_0079;
        Label_0036:
            if (num <= list.Count)
            {
                goto Label_0048;
            }
            goto Label_0086;
        Label_0048:
            if (list[num].UnitParam.IsHero() == null)
            {
                goto Label_0064;
            }
            goto Label_0073;
        Label_0064:
            list2.Add(list[num]);
        Label_0073:
            num += 1;
        Label_0079:
            if (num < data2.MAX_MAINMEMBER)
            {
                goto Label_0036;
            }
        Label_0086:
            num2 = list2.Count;
            goto Label_00A2;
        Label_0094:
            list2.Add(null);
            num2 += 1;
        Label_00A2:
            if (num2 < data2.MAX_UNIT)
            {
                goto Label_0094;
            }
            num3 = 0;
            goto Label_00EC;
        Label_00B7:
            num4 = (list2[num3] == null) ? 0L : list2[num3].UniqueID;
            data2.SetUnitUniqueID(num3, num4);
            num3 += 1;
        Label_00EC:
            if (num3 < list2.Count)
            {
                goto Label_00B7;
            }
            base.ExecRequest(new ReqParty(new Network.ResponseCallback(this.ResponseCallback), 0, 0, 0));
            base.set_enabled(1);
        Label_011B:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            GameManager manager;
            Json_Party[] partyArray;
            Json_Party party;
            int num;
            int num2;
            PartyData data;
            PartyWindow2.EditPartyTypes types;
            List<PartyEditData> list;
            int num3;
            int num4;
            PartyEditData data2;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0043;
            }
            code = Network.ErrCode;
            if (code == 0x708)
            {
                goto Label_002E;
            }
            if (code == 0x709)
            {
                goto Label_002E;
            }
            goto Label_0039;
        Label_002E:
            this.OnFailed();
            goto Label_0043;
        Label_0039:
            FlowNode_Network.Retry();
        Label_0043:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            manager = MonoSingleton<GameManager>.Instance;
        Label_0056:
            try
            {
                if (response.body != null)
                {
                    goto Label_0067;
                }
                throw new InvalidJSONException();
            Label_0067:
                manager.Deserialize(response.body.player);
                manager.Deserialize(response.body.parties);
                goto Label_009E;
            }
            catch (Exception)
            {
            Label_008E:
                FlowNode_Network.Retry();
                goto Label_0160;
            }
        Label_009E:
            partyArray = response.body.parties;
            if (partyArray == null)
            {
                goto Label_014B;
            }
            if (((int) partyArray.Length) <= 0)
            {
                goto Label_014B;
            }
            party = partyArray[0];
            num = 0;
            goto Label_0142;
        Label_00C5:
            num2 = num;
            if (num != 9)
            {
                goto Label_00D7;
            }
            goto Label_013C;
        Label_00D7:
            data = new PartyData(num2);
            data.Deserialize(party);
            types = SRPG_Extensions.ToEditPartyType(num2);
            list = new List<PartyEditData>();
            num3 = SRPG_Extensions.GetMaxTeamCount(types);
            num4 = 0;
            goto Label_0128;
        Label_0109:
            data2 = new PartyEditData(PartyUtility.CreateDefaultPartyNameFromIndex(num4), data);
            list.Add(data2);
            num4 += 1;
        Label_0128:
            if (num4 < num3)
            {
                goto Label_0109;
            }
            PartyUtility.SaveTeamPresets(types, 0, list, 0);
        Label_013C:
            num += 1;
        Label_0142:
            if (num < 11)
            {
                goto Label_00C5;
            }
        Label_014B:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(10);
        Label_0160:
            return;
        }
    }
}

