// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusEdit
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      int maxUnit = player.Partys[7].MAX_UNIT;
      for (int idx = 0; idx < maxUnit; ++idx)
      {
        if (!PlayerPrefs.HasKey(PlayerData.VERSUS_ID_KEY + (object) idx))
          player.SetVersusPlacement(PlayerData.VERSUS_ID_KEY + (object) idx, idx);
      }
      PlayerPrefs.Save();
    }
  }
}
