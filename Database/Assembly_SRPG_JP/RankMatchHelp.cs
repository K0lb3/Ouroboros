// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchHelp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class RankMatchHelp : MonoBehaviour, IWebHelp
  {
    public RankMatchHelp()
    {
      base.\u002Ector();
    }

    public bool GetHelpURL(out string url, out string title)
    {
      title = (string) null;
      url = (string) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return false;
      VersusRankParam versusRankParam = instance.GetVersusRankParam(instance.RankMatchScheduleId);
      if (versusRankParam == null || string.IsNullOrEmpty(versusRankParam.HelpURL))
        return false;
      title = versusRankParam.Name;
      url = versusRankParam.HelpURL;
      return true;
    }
  }
}
