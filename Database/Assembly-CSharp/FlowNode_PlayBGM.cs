// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayBGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using UnityEngine;

[FlowNode.Pin(1, "Output", FlowNode.PinTypes.Output, 1)]
[FlowNode.NodeType("Sound/PlayBGM", 32741)]
[FlowNode.Pin(100, "Play", FlowNode.PinTypes.Input, 0)]
public class FlowNode_PlayBGM : FlowNode
{
  public FlowNode_PlayBGM.EType Type;
  public string BGMName;

  public override string[] GetInfoLines()
  {
    if (string.IsNullOrEmpty(this.BGMName))
      return base.GetInfoLines();
    return new string[1]{ "BGM is [" + this.BGMName + "]" };
  }

  public override void OnActivate(int pinID)
  {
    if (this.Type == FlowNode_PlayBGM.EType.HOME_BGM)
      FlowNode_PlayBGM.PlayHomeBGM();
    else if (string.IsNullOrEmpty(this.BGMName))
      MonoSingleton<MySound>.Instance.StopBGM();
    else
      MonoSingleton<MySound>.Instance.PlayBGM(this.BGMName, (string) null, false);
    this.ActivateOutputLinks(1);
  }

  public static void PlayHomeBGM()
  {
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
    {
      SectionParam currentSectionParam = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
      string cueID = currentSectionParam != null ? currentSectionParam.bgm : (string) null;
      if (!string.IsNullOrEmpty(cueID))
      {
        MonoSingleton<MySound>.Instance.PlayBGM(cueID, (string) null, false);
        return;
      }
    }
    MonoSingleton<MySound>.Instance.PlayBGM("BGM_0027", (string) null, false);
  }

  public static string[] GetHomeBGM()
  {
    string str = "BGM_0027";
    if (Object.op_Inequality((Object) MonoSingleton<GameManager>.GetInstanceDirect(), (Object) null))
    {
      SectionParam currentSectionParam = MonoSingleton<GameManager>.Instance.GetCurrentSectionParam();
      if (currentSectionParam != null && !string.IsNullOrEmpty(currentSectionParam.bgm))
        str = currentSectionParam.bgm;
    }
    return new string[2]
    {
      "StreamingAssets/" + str + ".acb",
      "StreamingAssets/" + str + ".awb"
    };
  }

  public enum EType
  {
    DIRECT,
    HOME_BGM,
  }
}
