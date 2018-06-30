namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class AbilityRankUpPanel : MonoBehaviour, IGameParameter
    {
        public Text RemainingCount;
        public GameObject RecoveryTimeParent;
        public Text RecoveryTimeText;
        public SRPG_Button ResetButton;
        public ResetAbilityRankUpCountEvent OnAbilityRankUpCountReset;
        public string AB_RANKUP_ADD_WINDOW_PATH;
        private int UseCoin;

        public AbilityRankUpPanel()
        {
            this.AB_RANKUP_ADD_WINDOW_PATH = "UI/AbilityRankupPointWindow";
            base..ctor();
            return;
        }

        private void Clicked(SRPG_Button button)
        {
            GameObject obj2;
            GameObject obj3;
            AbilityRankUpPointAddWindow window;
            if (button.IsInteractable() != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if (string.IsNullOrEmpty(this.AB_RANKUP_ADD_WINDOW_PATH) != null)
            {
                goto Label_0073;
            }
            obj2 = AssetManager.Load<GameObject>(this.AB_RANKUP_ADD_WINDOW_PATH);
            if ((obj2 != null) == null)
            {
                goto Label_0073;
            }
            obj3 = Object.Instantiate<GameObject>(obj2);
            if ((obj3 != null) == null)
            {
                goto Label_0073;
            }
            window = obj3.GetComponentInChildren<AbilityRankUpPointAddWindow>();
            if ((window != null) == null)
            {
                goto Label_0073;
            }
            window.OnDecide = new AbilityRankUpPointAddWindow.DecideEvent(this.OnDecide);
            window.OnCancel = null;
        Label_0073:
            return;
        }

        private void OnAbilityRankUpCountChange(int count)
        {
            this.UpdateValue();
            this.Update();
            return;
        }

        private void OnAccept(GameObject go)
        {
            GameManager manager;
            PlayerData data;
            int num;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            num = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpCountCoin;
            if (data.Coin >= num)
            {
                goto Label_003C;
            }
            manager.ConfirmBuyCoin(null, null);
            return;
        Label_003C:
            manager.OnAbilityRankUpCountPreReset(0);
            if (Network.Mode != 1)
            {
                goto Label_006C;
            }
            data.DEBUG_CONSUME_COIN(num);
            data.RestoreAbilityRankUpCount();
            this.Success();
            goto Label_0083;
        Label_006C:
            Network.RequestAPI(new ReqItemAbilPointPaid(new Network.ResponseCallback(this.OnRequestResult)), 0);
        Label_0083:
            return;
        }

        private void OnDecide(int value)
        {
            GameManager manager;
            PlayerData data;
            int num;
            manager = MonoSingleton<GameManager>.Instance;
            data = manager.Player;
            num = manager.MasterParam.FixParam.AbilityRankupPointCoinRate * value;
            if (data.Coin >= num)
            {
                goto Label_003A;
            }
            manager.ConfirmBuyCoin(null, null);
            return;
        Label_003A:
            manager.OnAbilityRankUpCountPreReset(0);
            this.UseCoin = value;
            Network.RequestAPI(new ReqItemAbilPointPaid(value, new Network.ResponseCallback(this.OnRequestResult)), 0);
            return;
        }

        private void OnDestroy()
        {
            GameManager local1;
            if ((MonoSingleton<GameManager>.GetInstanceDirect() != null) == null)
            {
                goto Label_0036;
            }
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) Delegate.Remove(local1.OnAbilityRankUpCountChange, new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountChange));
        Label_0036:
            return;
        }

        private unsafe void OnRequestResult(WWWResult www)
        {
            WebAPI.JSON_BodyResponse<Json_PlayerDataAll> response;
            Exception exception;
            Network.EErrCode code;
            if (Network.IsError == null)
            {
                goto Label_002C;
            }
            if (Network.ErrCode == 0xbb8)
            {
                goto Label_0020;
            }
            goto Label_0026;
        Label_0020:
            FlowNode_Network.Back();
            return;
        Label_0026:
            FlowNode_Network.Retry();
            return;
        Label_002C:
            response = JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<Json_PlayerDataAll>>(&www.text);
        Label_0039:
            try
            {
                MonoSingleton<GameManager>.Instance.Deserialize(response.body.player);
                Network.RemoveAPI();
                goto Label_006E;
            }
            catch (Exception exception1)
            {
            Label_0058:
                exception = exception1;
                DebugUtility.LogException(exception);
                FlowNode_Network.Retry();
                goto Label_009E;
            }
        Label_006E:
            if (this.UseCoin <= 0)
            {
                goto Label_008B;
            }
            MyMetaps.TrackSpendCoin("AbilityPoint", this.UseCoin);
        Label_008B:
            this.UseCoin = 0;
            this.UpdateValue();
            this.Success();
        Label_009E:
            return;
        }

        private void Start()
        {
            GameManager local1;
            this.UpdateValue();
            this.Update();
            if ((this.ResetButton != null) == null)
            {
                goto Label_0034;
            }
            this.ResetButton.AddListener(new SRPG_Button.ButtonClickEvent(this.Clicked));
        Label_0034:
            local1 = MonoSingleton<GameManager>.Instance;
            local1.OnAbilityRankUpCountChange = (GameManager.RankUpCountChangeEvent) Delegate.Combine(local1.OnAbilityRankUpCountChange, new GameManager.RankUpCountChangeEvent(this.OnAbilityRankUpCountChange));
            return;
        }

        private void Success()
        {
            if (this.OnAbilityRankUpCountReset == null)
            {
                goto Label_0016;
            }
            this.OnAbilityRankUpCountReset();
        Label_0016:
            MonoSingleton<GameManager>.Instance.NotifyAbilityRankUpCountChanged();
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 1).ToString(), null);
            GlobalEvent.Invoke(((PredefinedGlobalEvents) 0).ToString(), null);
            return;
        }

        private void Update()
        {
            PlayerData data;
            long num;
            string str;
            num = MonoSingleton<GameManager>.Instance.Player.GetNextAbilityRankUpCountRecoverySec();
            if ((this.RecoveryTimeText != null) == null)
            {
                goto Label_0054;
            }
            if (num <= 0L)
            {
                goto Label_0054;
            }
            str = TimeManager.ToMinSecString(num);
            if ((this.RecoveryTimeText.get_text() != str) == null)
            {
                goto Label_0054;
            }
            this.RecoveryTimeText.set_text(str);
        Label_0054:
            if ((this.RecoveryTimeParent != null) == null)
            {
                goto Label_0075;
            }
            this.RecoveryTimeParent.SetActive(num > 0L);
        Label_0075:
            return;
        }

        public void UpdateValue()
        {
            object[] objArray1;
            PlayerData data;
            int num;
            bool flag;
            int num2;
            data = MonoSingleton<GameManager>.Instance.Player;
            if ((this.RemainingCount != null) == null)
            {
                goto Label_0045;
            }
            objArray1 = new object[] { (int) data.AbilityRankUpCountNum };
            this.RemainingCount.set_text(LocalizedText.Get("sys.AB_RANKUPCOUNT", objArray1));
        Label_0045:
            if ((this.ResetButton != null) == null)
            {
                goto Label_0099;
            }
            num = data.AbilityRankUpCountNum;
            flag = 1;
            num2 = MonoSingleton<GameManager>.Instance.MasterParam.FixParam.AbilityRankUpPointMax;
            flag = (num >= num2) ? 0 : (data.Coin > 0);
            this.ResetButton.set_interactable(flag);
        Label_0099:
            return;
        }

        public delegate void ResetAbilityRankUpCountEvent();
    }
}

