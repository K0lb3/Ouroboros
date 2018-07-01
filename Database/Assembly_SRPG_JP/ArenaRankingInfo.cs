// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ranking_image, (UnityEngine.Object) null))
          this.ranking_image.ImageIndex = 0;
        ((Component) this.Ranking).get_gameObject().SetActive(true);
        this.Ranking.set_text(dataOfClass.ArenaRank.ToString() + LocalizedText.Get("sys.ARENA_LBL_RANK"));
      }
      if (!string.IsNullOrEmpty(dataOfClass.PlayerName))
        this.PlayerName.set_text(dataOfClass.PlayerName.ToString());
      if (!(dataOfClass.battle_at > DateTime.MinValue))
        return;
      this.PlayerKOs.set_text(dataOfClass.battle_at.ToString("MM/dd HH:mm"));
    }
  }
}
