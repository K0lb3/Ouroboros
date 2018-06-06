// Decompiled with JetBrains decompiler
// Type: FlowNode_PlayBGM
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
      MonoSingleton<MySound>.Instance.PlayBGM(this.BGMName, (string) null);
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
        MonoSingleton<MySound>.Instance.PlayBGM(cueID, (string) null);
        return;
      }
    }
    MonoSingleton<MySound>.Instance.PlayBGM("BGM_0027", (string) null);
  }

  public enum EType
  {
    DIRECT,
    HOME_BGM,
  }
}
