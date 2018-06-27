// Decompiled with JetBrains decompiler
// Type: SRPG.SGButtonHighlightController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class SGButtonHighlightController : MonoBehaviour
  {
    [SerializeField]
    private GameObject speedupHighlight;
    [SerializeField]
    private GameObject autoHighlight;

    public SGButtonHighlightController()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.autoHighlight, (Object) null) && SceneBattle.Instance.Battle.RequestAutoBattle)
        this.autoHighlight.SetActive(false);
      if (!Object.op_Inequality((Object) this.speedupHighlight, (Object) null) || !PlayerPrefs.HasKey("SPEED_UP") || PlayerPrefs.GetInt("SPEED_UP") != 1)
        return;
      this.speedupHighlight.SetActive(false);
    }
  }
}
