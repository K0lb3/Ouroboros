// Decompiled with JetBrains decompiler
// Type: SRPG.VersusMatchingInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "DraftMatching", FlowNode.PinTypes.Input, 2)]
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
      GlobalVars.IsVersusDraftMode = false;
    }

    public void Activated(int pinID)
    {
      if (pinID == 1)
      {
        GlobalVars.IsVersusDraftMode = false;
        ProgressWindow.OpenVersusLoadScreen();
      }
      else
      {
        if (pinID != 2)
          return;
        GlobalVars.IsVersusDraftMode = true;
        ProgressWindow.OpenVersusDraftLoadScreen();
      }
    }
  }
}
