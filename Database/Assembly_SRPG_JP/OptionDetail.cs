// Decompiled with JetBrains decompiler
// Type: SRPG.OptionDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "ToggleChargeDisp", FlowNode.PinTypes.Input, 1)]
  public class OptionDetail : MonoBehaviour, IFlowInterface
  {
    public Slider SoundVolume;
    public Slider MusicVolume;
    public Slider VoiceVolume;
    public LText ChargeDispText;

    public OptionDetail()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 11)
        return;
      bool is_disp = !GameUtility.Config_ChargeDisp.Value;
      this.UpdateChargeDispText(is_disp);
      GameUtility.Config_ChargeDisp.Value = is_disp;
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance))
        return;
      instance.ReflectCastSkill(is_disp);
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.SoundVolume, (Object) null))
      {
        this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
        Slider.SliderEvent onValueChanged = this.SoundVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache4 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__376));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache4 = OptionDetail.\u003C\u003Ef__am\u0024cache4;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache4);
      }
      if (Object.op_Inequality((Object) this.MusicVolume, (Object) null))
      {
        this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
        Slider.SliderEvent onValueChanged = this.MusicVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache5 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__377));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache5 = OptionDetail.\u003C\u003Ef__am\u0024cache5;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache5);
      }
      if (Object.op_Inequality((Object) this.VoiceVolume, (Object) null))
      {
        this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
        Slider.SliderEvent onValueChanged = this.VoiceVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (OptionDetail.\u003C\u003Ef__am\u0024cache6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          OptionDetail.\u003C\u003Ef__am\u0024cache6 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__378));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache6 = OptionDetail.\u003C\u003Ef__am\u0024cache6;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache6);
      }
      this.UpdateChargeDispText(GameUtility.Config_ChargeDisp.Value);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void UpdateChargeDispText(bool is_disp)
    {
      if (!Object.op_Implicit((Object) this.ChargeDispText))
        return;
      if (is_disp)
        this.ChargeDispText.set_text(LocalizedText.Get("sys.BTN_CHARGE_DISP_ON"));
      else
        this.ChargeDispText.set_text(LocalizedText.Get("sys.BTN_CHARGE_DISP_OFF"));
    }
  }
}
