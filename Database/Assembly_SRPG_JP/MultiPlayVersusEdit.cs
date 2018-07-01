// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class MultiPlayVersusEdit : MonoBehaviour
  {
    public MultiPlayVersusEdit()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.Set();
    }

    public void Set()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int maxUnit;
      string str;
      if (GlobalVars.SelectedMultiPlayVersusType == VERSUS_TYPE.RankMatch)
      {
        maxUnit = player.Partys[10].MAX_UNIT;
        str = PlayerPrefsUtility.RANKMATCH_ID_KEY;
      }
      else
      {
        maxUnit = player.Partys[7].MAX_UNIT;
        str = PlayerPrefsUtility.VERSUS_ID_KEY;
      }
      for (int idx = 0; idx < maxUnit; ++idx)
      {
        if (!PlayerPrefsUtility.HasKey(str + (object) idx))
          player.SetVersusPlacement(str + (object) idx, idx);
      }
      PlayerPrefsUtility.Save();
    }
  }
}
