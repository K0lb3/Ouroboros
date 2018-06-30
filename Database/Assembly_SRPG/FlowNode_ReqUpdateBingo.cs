namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(100, "開催期間外の報酬受け取り", 1, 100), NodeType("System/ReqUpdateBingo", 0x7fe5), Pin(0, "Request", 0, 0), Pin(1, "Success", 1, 1)]
    public class FlowNode_ReqUpdateBingo : FlowNode_Network
    {
        private TrophyParam mTrophyParam;
        private int mLevelOld;
        public GameObject RewardWindow;
        public string ReviewURL_Android;
        public string ReviewURL_iOS;
        public string ReviewURL_Generic;
        public string ReviewURL_Twitter;

        public FlowNode_ReqUpdateBingo()
        {
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <OnOverItemAmount>m__1C3(GameObject go)
        {
            GameManager manager;
            TrophyParam param;
            TrophyState state;
            List<TrophyState> list;
            List<TrophyState> list2;
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetTrophy(GlobalVars.SelectedTrophy);
            state = manager.Player.GetTrophyCounter(param, 0);
            list2 = new List<TrophyState>();
            list2.Add(state);
            list = list2;
            base.ExecRequest(new ReqUpdateBingo(list, new Network.ResponseCallback(this.ResponseCallback), 1));
            base.set_enabled(1);
            return;
        }

        [CompilerGenerated]
        private void <OnOverItemAmount>m__1C4(GameObject go)
        {
            base.set_enabled(0);
            return;
        }

        public override void OnActivate(int pinID)
        {
            GameManager manager;
            TrophyParam param;
            PlayerData data;
            TrophyState state;
            List<TrophyState> list;
            List<TrophyState> list2;
            if (pinID != null)
            {
                goto Label_00DB;
            }
            if (base.get_enabled() == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            param = manager.MasterParam.GetTrophy(GlobalVars.SelectedTrophy);
            this.mTrophyParam = param;
            data = MonoSingleton<GameManager>.Instance.Player;
            this.mLevelOld = data.Lv;
            GlobalVars.PlayerExpOld.Set(data.Exp);
            if (Network.Mode != 1)
            {
                goto Label_0099;
            }
            manager.Player.DEBUG_ADD_COIN(param.Coin, 0, 0);
            manager.Player.DEBUG_ADD_GOLD(param.Gold);
            base.set_enabled(0);
            this.Success();
            return;
        Label_0099:
            state = manager.Player.GetTrophyCounter(param, 0);
            list2 = new List<TrophyState>();
            list2.Add(state);
            list = list2;
            base.ExecRequest(new ReqUpdateBingo(list, new Network.ResponseCallback(this.ResponseCallback), 1));
            base.set_enabled(1);
        Label_00DB:
            return;
        }

        private void OnErrorDialogClosed(GameObject dialog)
        {
            base.ActivateOutputLinks(100);
            return;
        }

        private void OnOverItemAmount()
        {
            string str;
            UIUtility.ConfirmBox(LocalizedText.Get("sys.MAILBOX_ITEM_OVER_MSG"), null, new UIUtility.DialogResultEvent(this.<OnOverItemAmount>m__1C3), new UIUtility.DialogResultEvent(this.<OnOverItemAmount>m__1C4), null, 0, -1);
            return;
        }

        public override unsafe void OnSuccess(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_0071;
            }
            code = Network.ErrCode;
            switch ((code - 0x1004))
            {
                case 0:
                    goto Label_0038;

                case 1:
                    goto Label_0038;

                case 2:
                    goto Label_0038;
            }
            if (code == 0x1010)
            {
                goto Label_003F;
            }
            goto Label_006A;
        Label_0038:
            this.OnBack();
            return;
        Label_003F:
            UIUtility.NegativeSystemMessage(null, LocalizedText.Get("sys.CHALLENGEMISSION_ERROR_OUT_OF_DATE_RECEIVE"), new UIUtility.DialogResultEvent(this.OnErrorDialogClosed), null, 0, -1);
            Network.RemoveAPI();
            Network.ResetError();
            return;
        Label_006A:
            this.OnRetry();
            return;
        Label_0071:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
            DebugUtility.Assert((response == null) == 0, "res == null");
            if (response.body != null)
            {
                goto Label_00A1;
            }
            this.OnRetry();
            return;
        Label_00A1:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                goto Label_00CD;
            }
            catch (Exception exception1)
            {
            Label_00BB:
                exception = exception1;
                base.OnRetry(exception);
                goto Label_00D8;
            }
        Label_00CD:
            Network.RemoveAPI();
            this.Success();
        Label_00D8:
            return;
        }

        private unsafe void Success()
        {
            TrophyParam[] paramArray1;
            PlayerData data;
            RewardData data2;
            GameManager manager;
            int num;
            ItemData data3;
            ItemData data4;
            int num2;
            int num3;
            string str;
            int num4;
            string str2;
            data = MonoSingleton<GameManager>.Instance.Player;
            if (data.Lv <= this.mLevelOld)
            {
                goto Label_002F;
            }
            data.OnPlayerLevelChange(data.Lv - this.mLevelOld);
        Label_002F:
            GlobalVars.PlayerLevelChanged.Set((data.Lv == this.mLevelOld) == 0);
            GlobalVars.PlayerExpNew.Set(data.Exp);
            paramArray1 = new TrophyParam[] { this.mTrophyParam };
            data.MarkTrophiesEnded(paramArray1);
            if ((this.RewardWindow != null) == null)
            {
                goto Label_0173;
            }
            data2 = new RewardData();
            data2.Coin = this.mTrophyParam.Coin;
            data2.Gold = this.mTrophyParam.Gold;
            data2.Exp = this.mTrophyParam.Exp;
            manager = MonoSingleton<GameManager>.Instance;
            num = 0;
            goto Label_0154;
        Label_00C6:
            data3 = new ItemData();
            if (data3.Setup(0L, &(this.mTrophyParam.Items[num]).iname, &(this.mTrophyParam.Items[num]).Num) == null)
            {
                goto Label_0150;
            }
            data2.Items.Add(data3);
            data4 = manager.Player.FindItemDataByItemID(data3.Param.iname);
            num2 = (data4 == null) ? 0 : data4.Num;
            data2.ItemsBeforeAmount.Add(num2);
        Label_0150:
            num += 1;
        Label_0154:
            if (num < ((int) this.mTrophyParam.Items.Length))
            {
                goto Label_00C6;
            }
            DataSource.Bind<RewardData>(this.RewardWindow, data2);
        Label_0173:
            GameCenterManager.SendAchievementProgress(this.mTrophyParam.iname);
            if (this.mTrophyParam == null)
            {
                goto Label_01FD;
            }
            if (this.mTrophyParam.Objectives == null)
            {
                goto Label_01FD;
            }
            num3 = 0;
            goto Label_01E9;
        Label_01A6:
            if (this.mTrophyParam.Objectives[num3].type != 0x11)
            {
                goto Label_01E3;
            }
            str = null;
            str = this.ReviewURL_Generic;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_01FD;
            }
            Application.OpenURL(str);
            goto Label_01FD;
        Label_01E3:
            num3 += 1;
        Label_01E9:
            if (num3 < ((int) this.mTrophyParam.Objectives.Length))
            {
                goto Label_01A6;
            }
        Label_01FD:
            if (this.mTrophyParam == null)
            {
                goto Label_0274;
            }
            if (this.mTrophyParam.Objectives == null)
            {
                goto Label_0274;
            }
            num4 = 0;
            goto Label_0260;
        Label_0220:
            if (this.mTrophyParam.Objectives[num4].type != 0x12)
            {
                goto Label_025A;
            }
            str2 = this.ReviewURL_Twitter;
            if (string.IsNullOrEmpty(str2) != null)
            {
                goto Label_0274;
            }
            Application.OpenURL(str2);
            goto Label_0274;
        Label_025A:
            num4 += 1;
        Label_0260:
            if (num4 < ((int) this.mTrophyParam.Objectives.Length))
            {
                goto Label_0220;
            }
        Label_0274:
            base.set_enabled(0);
            base.ActivateOutputLinks(1);
            return;
        }
    }
}

