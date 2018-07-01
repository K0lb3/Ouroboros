// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuestBattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  public class MapEffectQuestBattleUI : MonoBehaviour
  {
    public SRPG_Button ButtonMapEffect;
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffect;

    public MapEffectQuestBattleUI()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Implicit((Object) this.ButtonMapEffect))
        return;
      this.ButtonMapEffect.AddListener((SRPG_Button.ButtonClickEvent) (button => this.ReqOpenMapEffect()));
    }

    private void ReqOpenMapEffect()
    {
      this.StartCoroutine(this.OpenMapEffect());
    }

    [DebuggerHidden]
    private IEnumerator OpenMapEffect()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new MapEffectQuestBattleUI.\u003COpenMapEffect\u003Ec__Iterator126()
      {
        \u003C\u003Ef__this = this
      };
    }
  }
}
