// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_PollSystem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [FlowNode.Pin(1100, "DisableInputModule", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(8000, "CriticalSection", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1500, "BlockInterruptUrlSchemeLaunch", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1400, "BlockInterruptPhotonDisconnected", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1300, "BlockInterruptAll", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1280, "BeforeLogin", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1250, "NetworkConnecting", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1200, "FadeVisible", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(11, "すべてパスするまで待つ", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "どれかに引っかかるまで待つ", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "すべてパスした", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(0, "どれかに引っかかった", FlowNode.PinTypes.Output, 0)]
  [FlowNode.NodeType("System/PollSystem")]
  public class FlowNode_PollSystem : FlowNodePersistent
  {
    private bool[] mCheckFlag = new bool[8];
    private bool[] mCheckType = new bool[3];
    private bool mStart;

    public override void OnActivate(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mStart = true;
          this.mCheckType[1] = true;
          break;
        case 11:
          this.mStart = true;
          this.mCheckType[2] = true;
          break;
        case 1100:
          this.mCheckFlag[0] = true;
          break;
        case 1200:
          this.mCheckFlag[1] = true;
          break;
        case 1250:
          this.mCheckFlag[2] = true;
          break;
        case 1280:
          this.mCheckFlag[3] = true;
          break;
        case 1300:
          this.mCheckFlag[4] = true;
          break;
        case 1400:
          this.mCheckFlag[5] = true;
          break;
        case 1500:
          this.mCheckFlag[6] = true;
          break;
        case 8000:
          this.mCheckFlag[7] = true;
          break;
      }
    }

    private void Reset()
    {
      this.mStart = false;
    }

    private void AnyOn()
    {
      if (!this.mCheckType[1])
        return;
      this.Reset();
      this.ActivateOutputLinks(0);
    }

    private void AllPass()
    {
      if (!this.mCheckType[2])
        return;
      this.Reset();
      this.ActivateOutputLinks(1);
    }

    private void Update()
    {
      if (!this.mStart)
        return;
      if (this.mCheckFlag[0] && !((Behaviour) EventSystem.get_current().get_currentInputModule()).get_enabled())
        this.AnyOn();
      else if (this.mCheckFlag[1] && FadeController.InstanceExists && FadeController.Instance.IsFading(0))
        this.AnyOn();
      else if (this.mCheckFlag[2] && Network.IsConnecting)
        this.AnyOn();
      else if (this.mCheckFlag[3] && !FlowNode_GetCurrentScene.IsAfterLogin())
        this.AnyOn();
      else if (this.mCheckFlag[4] && BlockInterrupt.IsBlocked(BlockInterrupt.EType.ALL))
        this.AnyOn();
      else if (this.mCheckFlag[5] && BlockInterrupt.IsBlocked(BlockInterrupt.EType.PHOTON_DISCONNECTED))
        this.AnyOn();
      else if (this.mCheckFlag[6] && BlockInterrupt.IsBlocked(BlockInterrupt.EType.URL_SCHEME_LAUNCH))
        this.AnyOn();
      else if (this.mCheckFlag[7] && CriticalSection.IsActive)
        this.AnyOn();
      else
        this.AllPass();
    }

    private enum EType
    {
      DISABLE_INPUT_MODULE,
      FADE_VISIBLE,
      NETWORK_CONNECTING,
      BEFORE_LOGIN,
      INTERRUPT_STOPPER_ALL,
      INTERRUPT_STOPPER_PHOTON_DISCONNECTED,
      INTERRUPT_STOPPER_URL_SCHEME_LAUNCH,
      CRITIAL_SECTION,
      NUM,
    }

    private enum EState
    {
      NOP,
      CHECK,
      PASS,
      NUM,
    }
  }
}
