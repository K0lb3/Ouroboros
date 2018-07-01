// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Matching", FlowNode.PinTypes.Input, 1)]
  public class VersusMatchingInfo : MonoBehaviour, IFlowInterface
  {
    public VersusMatchingInfo()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      MonoSingleton<GameManager>.Instance.AudienceMode = false;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      ProgressWindow.OpenVersusLoadScreen();
    }
  }
}
