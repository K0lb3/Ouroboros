// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaRankingInfo : MonoBehaviour
  {
    [Space(10f)]
    public Text Ranking;
    public Text PlayerName;
    public Text PlayerKOs;
    public ImageArray ranking_image;

    public ArenaRankingInfo()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).get_gameObject(), (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.ArenaRank <= 3)
      {
        this.ranking_image.ImageIndex = dataOfClass.ArenaRank;
        ((Component) this.Ranking).get_gameObject().SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.ranking_image, (Object) null))
          this.ranking_image.ImageIndex = 0;
        ((Component) this.Ranking).get_gameObject().SetActive(true);
        this.Ranking.set_text(string.Format(LocalizedText.Get("sys.RANKING_RANK"), (object) dataOfClass.ArenaRank.ToString()));
      }
      if (!string.IsNullOrEmpty(dataOfClass.PlayerName))
        this.PlayerName.set_text(dataOfClass.PlayerName.ToString());
      if (!(dataOfClass.battle_at > DateTime.MinValue))
        return;
      this.PlayerKOs.set_text(dataOfClass.battle_at.ToString(GameUtility.Localized_TimePattern_Short));
    }
  }
}
