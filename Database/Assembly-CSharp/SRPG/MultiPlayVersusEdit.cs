// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
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
        if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.VERSUS_ID_KEY + (object) idx))
          player.SetVersusPlacement(PlayerPrefsUtility.VERSUS_ID_KEY + (object) idx, idx);
      }
      PlayerPrefsUtility.Save();
    }
  }
}
