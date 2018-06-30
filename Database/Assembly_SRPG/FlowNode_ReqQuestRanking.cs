namespace SRPG
{
    using GR;
    using System;
    using System.Text;
    using UnityEngine;

    [Pin(200, "ランキング取得(自身)", 0, 200), Pin(300, "ランキング取得(成功)", 1, 300), NodeType("System/ReqQuestRanking"), Pin(0x12d, "ランキング取得(失敗)", 1, 0x12d), Pin(50, "ランキング取得(TOP + 自身)", 0, 50), Pin(100, "ランキング取得(TOP)", 0, 100)]
    public class FlowNode_ReqQuestRanking : FlowNode_Network
    {
        public const int INPUT_REQUEST_RANKING_TOP_OWN = 50;
        public const int INPUT_REQUEST_RANKING_TOP = 100;
        public const int INPUT_REQUEST_RANKING_OWN = 200;
        public const int OUTPUT_REQUEST_RANKING_SUCCESS = 300;
        public const int OUTPUT_REQUEST_RANKING_FAILED = 0x12d;
        [SerializeField]
        private RankingQuestRankWindow m_TargetWindow;

        public FlowNode_ReqQuestRanking()
        {
            base..ctor();
            return;
        }

        public override void OnActivate(int pinID)
        {
            RankingQuestParam param;
            QuestParam param2;
            param = GlobalVars.SelectedRankingQuestParam;
            if (GlobalVars.SelectedRankingQuestParam != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            param2 = MonoSingleton<GameManager>.Instance.FindQuest(param.iname);
            if (pinID != 50)
            {
                goto Label_005A;
            }
            base.ExecRequest(new API_ReqQuestRanking(param.schedule_id, param.type, param2.iname, 0, 0, new Network.ResponseCallback(this.ResponseCallback_RequestRankingTopOwn)));
            goto Label_00C8;
        Label_005A:
            if (pinID != 100)
            {
                goto Label_0092;
            }
            base.ExecRequest(new API_ReqQuestRanking(param.schedule_id, param.type, param2.iname, 0, 0, new Network.ResponseCallback(this.ResponseCallback_RequestRankingTop)));
            goto Label_00C8;
        Label_0092:
            if (pinID != 200)
            {
                goto Label_00C8;
            }
            base.ExecRequest(new API_ReqQuestRanking(param.schedule_id, param.type, param2.iname, 0, 1, new Network.ResponseCallback(this.ResponseCallback_RequestRankingOwn)));
        Label_00C8:
            return;
        }

        public override void OnSuccess(WWWResult www)
        {
        }

        private unsafe void ResponseCallback_RequestRankingOwn(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            RankingQuestUserData[] dataArray;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_007E;
            }
            if (Network.ErrCode == 0x32c9)
            {
                goto Label_0034;
            }
            if (Network.ErrCode != 0x32ca)
            {
                goto Label_003B;
            }
        Label_0034:
            this.OnFailed();
            return;
        Label_003B:
            if (Network.ErrCode != 0x32cb)
            {
                goto Label_0063;
            }
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12d);
            return;
        Label_0063:
            if (Network.ErrCode != 0xcf)
            {
                goto Label_0077;
            }
            goto Label_007E;
        Label_0077:
            this.OnRetry();
            return;
        Label_007E:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00AE;
            }
            this.OnRetry();
            return;
        Label_00AE:
            Network.RemoveAPI();
            dataArray = RankingQuestUserData.CreateRankingUserDataFromJson(response.body.ranking, GlobalVars.SelectedRankingQuestParam.type);
            this.m_TargetWindow.SetData(dataArray);
            this.Success();
            return;
        }

        private unsafe void ResponseCallback_RequestRankingTop(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            RankingQuestUserData[] dataArray;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_007E;
            }
            if (Network.ErrCode == 0x32c9)
            {
                goto Label_0034;
            }
            if (Network.ErrCode != 0x32ca)
            {
                goto Label_003B;
            }
        Label_0034:
            this.OnFailed();
            return;
        Label_003B:
            if (Network.ErrCode != 0x32cb)
            {
                goto Label_0063;
            }
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12d);
            return;
        Label_0063:
            if (Network.ErrCode != 0xcf)
            {
                goto Label_0077;
            }
            goto Label_007E;
        Label_0077:
            this.OnRetry();
            return;
        Label_007E:
            DebugUtility.Log(&www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00BA;
            }
            this.OnRetry();
            return;
        Label_00BA:
            Network.RemoveAPI();
            dataArray = RankingQuestUserData.CreateRankingUserDataFromJson(response.body.ranking, GlobalVars.SelectedRankingQuestParam.type);
            this.m_TargetWindow.SetData(dataArray);
            this.Success();
            return;
        }

        private unsafe void ResponseCallback_RequestRankingTopOwn(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json> response;
            RankingQuestUserData[] dataArray;
            RankingQuestUserData data;
            if (FlowNode_Network.HasCommonError(www) == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (Network.IsError == null)
            {
                goto Label_007E;
            }
            if (Network.ErrCode == 0x32c9)
            {
                goto Label_0034;
            }
            if (Network.ErrCode != 0x32ca)
            {
                goto Label_003B;
            }
        Label_0034:
            this.OnFailed();
            return;
        Label_003B:
            if (Network.ErrCode != 0x32cb)
            {
                goto Label_0063;
            }
            Network.RemoveAPI();
            base.set_enabled(0);
            base.ActivateOutputLinks(0x12d);
            return;
        Label_0063:
            if (Network.ErrCode != 0xcf)
            {
                goto Label_0077;
            }
            goto Label_007E;
        Label_0077:
            this.OnRetry();
            return;
        Label_007E:
            DebugUtility.Log(&www.text);
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00BA;
            }
            this.OnRetry();
            return;
        Label_00BA:
            Network.RemoveAPI();
            dataArray = RankingQuestUserData.CreateRankingUserDataFromJson(response.body.ranking, GlobalVars.SelectedRankingQuestParam.type);
            data = RankingQuestUserData.CreateRankingUserDataFromJson(response.body.my_info, GlobalVars.SelectedRankingQuestParam.type);
            this.m_TargetWindow.SetData(dataArray, data);
            this.Success();
            return;
        }

        private void Success()
        {
            base.ActivateOutputLinks(300);
            return;
        }

        public class API_ReqQuestRanking : WebAPI
        {
            public API_ReqQuestRanking(int schedule_id, RankingQuestType type, string quest_id, int rank, bool isOwn, Network.ResponseCallback response)
            {
                StringBuilder builder;
                base..ctor();
                base.name = "quest/ranking";
                builder = WebAPI.GetStringBuilder();
                AppendKeyValue(builder, "schedule_id", schedule_id);
                builder.Append(",");
                AppendKeyValue(builder, "type", type);
                builder.Append(",");
                AppendKeyValue(builder, "iname", quest_id);
                builder.Append(",");
                AppendKeyValue(builder, "rank", rank);
                builder.Append(",");
                AppendKeyValue(builder, "is_near", (isOwn == null) ? 0 : 1);
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
            public FlowNode_ReqQuestRanking.Json_OwnRankingData my_info;
            public FlowNode_ReqQuestRanking.Json_OthersRankingData[] ranking;

            public Json()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_OthersRankingData
        {
            public string uid;
            public string name;
            public int rank;
            public string unit_iname;
            public int unit_lv;
            public int job_lv;
            public int main_score;
            public int sub_score;

            public Json_OthersRankingData()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_OwnRankingData
        {
            public FlowNode_ReqQuestRanking.Json_UnitDataLight unit;
            public int rank;
            public int main_score;
            public int sub_score;

            public Json_OwnRankingData()
            {
                base..ctor();
                return;
            }
        }

        [Serializable]
        public class Json_UnitDataLight
        {
            public string unit_iname;
            public int unit_lv;
            public int job_lv;

            public Json_UnitDataLight()
            {
                base..ctor();
                return;
            }
        }
    }
}

