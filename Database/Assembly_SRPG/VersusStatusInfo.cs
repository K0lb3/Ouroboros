namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class VersusStatusInfo : MonoBehaviour
    {
        private readonly string COIN_NAME;
        public Text FreeCnt;
        public Text TowerCnt;
        public Text FriendCnt;
        public Text VSCoinCnt;
        public Text FreeRate;
        public Text TowerRate;
        public Text FriendRate;

        public VersusStatusInfo()
        {
            this.COIN_NAME = "IT_EVENT_VS";
            base..ctor();
            return;
        }

        private unsafe string GenerateWinRateString(int wincnt, int totalcnt)
        {
            float num;
            num = 0f;
            if (wincnt <= 0)
            {
                goto Label_0019;
            }
            num = (((float) wincnt) / ((float) totalcnt)) * 100f;
        Label_0019:
            if (num < 100f)
            {
                goto Label_002A;
            }
            return "100";
        Label_002A:
            return &num.ToString("F1");
        }

        private unsafe void RefreshData()
        {
            GameManager manager;
            PlayerData data;
            ItemData data2;
            int num;
            int num2;
            int num3;
            int num4;
            data = MonoSingleton<GameManager>.Instance.Player;
            if ((this.FreeCnt != null) == null)
            {
                goto Label_0037;
            }
            this.FreeCnt.set_text(&data.VersusFreeWinCnt.ToString());
        Label_0037:
            if ((this.TowerCnt != null) == null)
            {
                goto Label_0062;
            }
            this.TowerCnt.set_text(&data.VersusTowerWinCnt.ToString());
        Label_0062:
            if ((this.FriendCnt != null) == null)
            {
                goto Label_008D;
            }
            this.FriendCnt.set_text(&data.VersusFriendWinCnt.ToString());
        Label_008D:
            data2 = data.FindItemDataByItemID(this.COIN_NAME);
            if (data2 == null)
            {
                goto Label_00CB;
            }
            if ((this.VSCoinCnt != null) == null)
            {
                goto Label_00CB;
            }
            this.VSCoinCnt.set_text(&data2.Num.ToString());
        Label_00CB:
            if ((this.FreeRate != null) == null)
            {
                goto Label_00F9;
            }
            this.FreeRate.set_text(this.GenerateWinRateString(data.VersusFreeWinCnt, data.VersusFreeCnt));
        Label_00F9:
            if ((this.TowerRate != null) == null)
            {
                goto Label_0127;
            }
            this.TowerRate.set_text(this.GenerateWinRateString(data.VersusTowerWinCnt, data.VersusTowerCnt));
        Label_0127:
            if ((this.FriendRate != null) == null)
            {
                goto Label_0155;
            }
            this.FriendRate.set_text(this.GenerateWinRateString(data.VersusFriendWinCnt, data.VersusFriendCnt));
        Label_0155:
            DataSource.Bind<PlayerData>(base.get_gameObject(), MonoSingleton<GameManager>.Instance.Player);
            return;
        }

        private void Start()
        {
            this.RefreshData();
            return;
        }
    }
}

