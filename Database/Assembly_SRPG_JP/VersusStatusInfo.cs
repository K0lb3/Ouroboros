// Decompiled with JetBrains decompiler
// Type: SRPG.VersusStatusInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
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
      base.\u002Ector();
    }

    private void Start()
    {
      this.RefreshData();
    }

    private void RefreshData()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      if (Object.op_Inequality((Object) this.FreeCnt, (Object) null))
        this.FreeCnt.set_text(player.VersusFreeWinCnt.ToString());
      if (Object.op_Inequality((Object) this.TowerCnt, (Object) null))
        this.TowerCnt.set_text(player.VersusTowerWinCnt.ToString());
      if (Object.op_Inequality((Object) this.FriendCnt, (Object) null))
        this.FriendCnt.set_text(player.VersusFriendWinCnt.ToString());
      ItemData itemDataByItemId = player.FindItemDataByItemID(this.COIN_NAME);
      if (itemDataByItemId != null && Object.op_Inequality((Object) this.VSCoinCnt, (Object) null))
        this.VSCoinCnt.set_text(itemDataByItemId.Num.ToString());
      if (Object.op_Inequality((Object) this.FreeRate, (Object) null))
        this.FreeRate.set_text(this.GenerateWinRateString(player.VersusFreeWinCnt, player.VersusFreeCnt));
      if (Object.op_Inequality((Object) this.TowerRate, (Object) null))
        this.TowerRate.set_text(this.GenerateWinRateString(player.VersusTowerWinCnt, player.VersusTowerCnt));
      if (Object.op_Inequality((Object) this.FriendRate, (Object) null))
        this.FriendRate.set_text(this.GenerateWinRateString(player.VersusFriendWinCnt, player.VersusFriendCnt));
      DataSource.Bind<PlayerData>(((Component) this).get_gameObject(), MonoSingleton<GameManager>.Instance.Player);
    }

    private string GenerateWinRateString(int wincnt, int totalcnt)
    {
      float num = 0.0f;
      if (wincnt > 0)
        num = (float) ((double) wincnt / (double) totalcnt * 100.0);
      if ((double) num >= 100.0)
        return "100";
      return num.ToString("F1");
    }
  }
}
