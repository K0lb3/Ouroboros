// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerMatchInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusTowerMatchInfo : MonoBehaviour
  {
    public GameObject template;
    public GameObject winbonus;
    public GameObject keyrateup;
    public GameObject parent;
    public Text nowKey;
    public Text maxKey;
    public Text floor;
    public Text bonusRate;
    public Text winCnt;
    public Text endAt;

    public VersusTowerMatchInfo()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.template, (Object) null))
        return;
      this.RefreshData();
    }

    private void RefreshData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      List<GameObject> gameObjectList = new List<GameObject>();
      int versusTowerKey = player.VersusTowerKey;
      VersusTowerParam versusTowerParam = instance.GetCurrentVersusTowerParam(-1);
      if (versusTowerParam == null)
        return;
      int num = 0;
      while (num < (int) versusTowerParam.RankupNum)
      {
        GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.template);
        if (!Object.op_Equality((Object) gameObject, (Object) null))
        {
          gameObject.SetActive(true);
          if (Object.op_Inequality((Object) this.parent, (Object) null))
            gameObject.get_transform().SetParent(this.parent.get_transform(), false);
          Transform child1 = gameObject.get_transform().FindChild("on");
          Transform child2 = gameObject.get_transform().FindChild("off");
          if (Object.op_Inequality((Object) child1, (Object) null))
            ((Component) child1).get_gameObject().SetActive(versusTowerKey > 0);
          if (Object.op_Inequality((Object) child2, (Object) null))
            ((Component) child2).get_gameObject().SetActive(versusTowerKey <= 0);
          gameObjectList.Add(gameObject);
        }
        ++num;
        --versusTowerKey;
      }
      this.template.SetActive(false);
      if (Object.op_Inequality((Object) this.nowKey, (Object) null))
        this.nowKey.set_text(player.VersusTowerKey.ToString());
      if (Object.op_Inequality((Object) this.maxKey, (Object) null))
        this.maxKey.set_text(versusTowerParam.RankupNum.ToString());
      if (Object.op_Inequality((Object) this.floor, (Object) null))
        this.floor.set_text(player.VersusTowerFloor.ToString());
      if (Object.op_Inequality((Object) this.winbonus, (Object) null))
        this.winbonus.SetActive(player.VersusTowerWinBonus > 0);
      if (Object.op_Inequality((Object) this.keyrateup, (Object) null))
        this.keyrateup.SetActive(player.VersusTowerWinBonus > 0);
      if (Object.op_Inequality((Object) this.bonusRate, (Object) null) && player.VersusTowerWinBonus > 0 && (int) versusTowerParam.WinNum > 0)
        this.bonusRate.set_text((((int) versusTowerParam.WinNum + (int) versusTowerParam.BonusNum) / (int) versusTowerParam.WinNum).ToString());
      if (Object.op_Inequality((Object) this.winCnt, (Object) null))
        this.winCnt.set_text(player.VersusTowerWinBonus.ToString());
      if (!Object.op_Inequality((Object) this.endAt, (Object) null))
        return;
      DateTime dateTime = TimeManager.FromUnixTime(instance.VersusTowerMatchEndAt);
      this.endAt.set_text(string.Format(LocalizedText.Get("sys.MULTI_VERSUS_END_AT"), (object) dateTime.Year, (object) dateTime.Month, (object) dateTime.Day, (object) dateTime.Hour, (object) dateTime.Minute));
    }
  }
}
