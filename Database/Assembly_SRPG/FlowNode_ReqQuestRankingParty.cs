namespace SRPG
{
    using GR;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    [Pin(100, "指定ユーザのパーティ取得", 0, 100), Pin(0x65, "指定ユーザのパーティ取得(成功)", 1, 0x65), Pin(0x66, "指定ユーザのパーティ取得(失敗)", 1, 0x66), NodeType("System/ReqQuestRankingParty")]
    public class FlowNode_ReqQuestRankingParty : FlowNode_Network
    {
        public const int INPUT_RANKING_PARTY = 100;
        public const int OUTPUT_RANKING_PARTY_SUCCESS = 0x65;
        public const int OUTPUT_RANKING_PARTY_FAILED = 0x66;
        [CompilerGenerated]
        private static Func<Json_Unit, bool> <>f__am$cache0;

        public FlowNode_ReqQuestRankingParty()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private static bool <OnSuccess>m__1BC(Json_Unit j)
        {
            return ((j == null) == 0);
        }

        public override void OnActivate(int pinID)
        {
            ReqQuestRankingPartyData data;
            data = DataSource.FindDataOfClass<ReqQuestRankingPartyData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_004A;
            }
            if (pinID != 100)
            {
                goto Label_004A;
            }
            base.ExecRequest(new API_ReqQuestRankingParty(data.m_ScheduleID, data.m_RankingType, data.m_QuestID, data.m_TargetUID, new Network.ResponseCallback(this.ResponseCallback)));
        Label_004A:
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            GlobalVars.UserSelectionPartyData data;
            Json json;
            int num;
            UnitData[] dataArray;
            int num2;
            ItemData[] dataArray2;
            int num3;
            SupportData data2;
            if (Network.IsError == null)
            {
                goto Label_006A;
            }
            if (Network.ErrCode == 0x32c9)
            {
                goto Label_0028;
            }
            if (Network.ErrCode != 0x32ca)
            {
                goto Label_002F;
            }
        Label_0028:
            this.OnFailed();
            return;
        Label_002F:
            if (Network.ErrCode == 0x32cb)
            {
                goto Label_004D;
            }
            if (Network.ErrCode != 0xcf)
            {
                goto Label_0063;
            }
        Label_004D:
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x66);
            return;
        Label_0063:
            this.OnRetry();
            return;
        Label_006A:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_009A;
            }
            this.OnRetry();
            return;
        Label_009A:
            Network.RemoveAPI();
            data = new GlobalVars.UserSelectionPartyData();
            json = response.body;
            if (json.party == null)
            {
                goto Label_0157;
            }
            if (<>f__am$cache0 != null)
            {
                goto Label_00D5;
            }
            <>f__am$cache0 = new Func<Json_Unit, bool>(FlowNode_ReqQuestRankingParty.<OnSuccess>m__1BC);
        Label_00D5:
            dataArray = new UnitData[Enumerable.Count<Json_Unit>(json.party, <>f__am$cache0)];
            num2 = 0;
            goto Label_0144;
        Label_00F0:
            if (json.party[num2] == null)
            {
                goto Label_0116;
            }
            if (string.IsNullOrEmpty(json.party[num2].iname) == null)
            {
                goto Label_0121;
            }
        Label_0116:
            dataArray[num2] = null;
            goto Label_013E;
        Label_0121:
            dataArray[num2] = new UnitData();
            dataArray[num2].Deserialize(json.party[num2]);
        Label_013E:
            num2 += 1;
        Label_0144:
            if (num2 < ((int) dataArray.Length))
            {
                goto Label_00F0;
            }
            data.unitData = dataArray;
        Label_0157:
            if (json.items == null)
            {
                goto Label_01C5;
            }
            dataArray2 = new ItemData[(int) json.items.Length];
            num3 = 0;
            goto Label_01B2;
        Label_0179:
            dataArray2[num3] = new ItemData();
            dataArray2[num3].Setup(0L, json.items[num3].iname, json.items[num3].num);
            num3 += 1;
        Label_01B2:
            if (num3 < ((int) dataArray2.Length))
            {
                goto Label_0179;
            }
            data.usedItems = dataArray2;
        Label_01C5:
            if (json.help == null)
            {
                goto Label_01EC;
            }
            data2 = new SupportData();
            data2.Deserialize(json.help);
            data.supportData = data2;
        Label_01EC:
            data.achievements = new int[0];
            GlobalVars.UserSelectionPartyDataInfo = data;
            this.Success();
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(0x65);
            return;
        }

        public class API_ReqQuestRankingParty : WebAPI
        {
            public API_ReqQuestRankingParty(int schedule_id, RankingQuestType type, string quest_id, string uid, Network.ResponseCallback response)
            {
                StringBuilder builder;
                base..ctor();
                base.name = "quest/ranking/party";
                builder = WebAPI.GetStringBuilder();
                AppendKeyValue(builder, "schedule_id", schedule_id);
                builder.Append(",");
                AppendKeyValue(builder, "type", type);
                builder.Append(",");
                AppendKeyValue(builder, "iname", quest_id);
                builder.Append(",");
                AppendKeyValue(builder, "target_uid", uid);
                base.body = WebAPI.GetRequestString(builder.ToString());
                base.callback = response;
                return;
            }

            private static void AppendKeyValue(StringBuilder sb, string key, int value)
            {
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":");
                sb.Append(value);
                return;
            }

            private static void AppendKeyValue(StringBuilder sb, string key, string value)
            {
                sb.Append("\"");
                sb.Append(key);
                sb.Append("\":\"");
                sb.Append(value);
                sb.Append("\"");
                return;
            }
        }

        [Serializable]
        public class Json
        {
            public Json_Unit[] party;
            public Json_Support help;
            public FlowNode_ReqQuestRankingParty.Json_LightItem[] items;

            public Json()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_LightItem
        {
            public string iname;
            public int num;

            public Json_LightItem()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_Party
        {
            public string uid;
            public string fuid;
            public string name;
            public string award;
            public int lv;
            public long lastlogin;
            public bool is_multi_push;
            public string multi_comment;
            public string created_at;
            public Json_Unit[] units;
            public Json_Support help;
            public FlowNode_ReqQuestRankingParty.Json_LightItem[] items;

            public Json_Party()
            {
                base..ctor();
                return;
            }
        }
    }
}

