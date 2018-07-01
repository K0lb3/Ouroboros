// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCoinChangeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
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
      base.\u002Ector();
    }

    public void Refresh(GachaCoinChangeWindow.CoinType coinType)
    {
      if (coinType == GachaCoinChangeWindow.CoinType.New)
      {
        this.RefreshNewCoin();
      }
      else
      {
        if (coinType != GachaCoinChangeWindow.CoinType.Old)
          return;
        this.RefreshOldCoin();
      }
    }

    private void RefreshNewCoin()
    {
      if (GlobalVars.NewSummonCoinInfo == null)
        return;
      this.ChangeText.set_text(LocalizedText.Get("sys.GACHA_SUMMON_NEW_COIN_CHANGED_TEXT", new object[1]
      {
        (object) this.ToDateString(GlobalVars.NewSummonCoinInfo.ConvertedDate)
      }));
      this.CoinNum.set_text(GlobalVars.NewSummonCoinInfo.ConvertedSummonCoin.ToString());
      this.StoneNum.set_text(GlobalVars.NewSummonCoinInfo.ReceivedStone.ToString());
      this.OldIcon.SetActive(false);
      this.NewIcon.SetActive(true);
    }

    private void RefreshOldCoin()
    {
      if (GlobalVars.OldSummonCoinInfo == null)
        return;
      this.ChangeText.set_text(LocalizedText.Get("sys.GACHA_SUMMON_OLD_COIN_CHANGED_TEXT", new object[1]
      {
        (object) this.ToDateString(GlobalVars.OldSummonCoinInfo.ConvertedDate)
      }));
      this.CoinNum.set_text(GlobalVars.OldSummonCoinInfo.ConvertedSummonCoin.ToString());
      this.StoneNum.set_text(GlobalVars.OldSummonCoinInfo.ReceivedStone.ToString());
      this.OldIcon.SetActive(true);
      this.NewIcon.SetActive(false);
    }

    private string ToDateString(long unixTime)
    {
      return GameUtility.UnixtimeToLocalTime(unixTime).ToString("yyyy/M/dd");
    }

    public enum CoinType
    {
      New,
      Old,
    }
  }
}
