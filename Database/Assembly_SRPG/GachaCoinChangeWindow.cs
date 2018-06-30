namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class GachaCoinChangeWindow : MonoBehaviour
    {
        [SerializeField]
        private Text ChangeText;
        [SerializeField]
        private Text CoinNum;
        [SerializeField]
        private Text StoneNum;
        [SerializeField]
        private GameObject OldIcon;
        [SerializeField]
        private GameObject NewIcon;

        public GachaCoinChangeWindow()
        {
            base..ctor();
            return;
        }

        public void Refresh(CoinType coinType)
        {
            if (coinType != null)
            {
                goto Label_0011;
            }
            this.RefreshNewCoin();
            goto Label_001E;
        Label_0011:
            if (coinType != 1)
            {
                goto Label_001E;
            }
            this.RefreshOldCoin();
        Label_001E:
            return;
        }

        private unsafe void RefreshNewCoin()
        {
            object[] objArray1;
            string str;
            if (GlobalVars.NewSummonCoinInfo != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            str = this.ToDateString(GlobalVars.NewSummonCoinInfo.ConvertedDate);
            objArray1 = new object[] { str };
            this.ChangeText.set_text(LocalizedText.Get("sys.GACHA_SUMMON_NEW_COIN_CHANGED_TEXT", objArray1));
            this.CoinNum.set_text(&GlobalVars.NewSummonCoinInfo.ConvertedSummonCoin.ToString());
            this.StoneNum.set_text(&GlobalVars.NewSummonCoinInfo.ReceivedStone.ToString());
            this.OldIcon.SetActive(0);
            this.NewIcon.SetActive(1);
            return;
        }

        private unsafe void RefreshOldCoin()
        {
            object[] objArray1;
            string str;
            if (GlobalVars.OldSummonCoinInfo != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            str = this.ToDateString(GlobalVars.OldSummonCoinInfo.ConvertedDate);
            objArray1 = new object[] { str };
            this.ChangeText.set_text(LocalizedText.Get("sys.GACHA_SUMMON_OLD_COIN_CHANGED_TEXT", objArray1));
            this.CoinNum.set_text(&GlobalVars.OldSummonCoinInfo.ConvertedSummonCoin.ToString());
            this.StoneNum.set_text(&GlobalVars.OldSummonCoinInfo.ReceivedStone.ToString());
            this.OldIcon.SetActive(1);
            this.NewIcon.SetActive(0);
            return;
        }

        private unsafe string ToDateString(long unixTime)
        {
            DateTime time;
            return &GameUtility.UnixtimeToLocalTime(unixTime).ToString("yyyy/M/dd");
        }

        public enum CoinType
        {
            New,
            Old
        }
    }
}

