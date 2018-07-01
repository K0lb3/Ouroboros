// Decompiled with JetBrains decompiler
// Type: SRPG.UnitProfileWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Text;
using UnityEngine;

namespace SRPG
{
  public class UnitProfileWindow : MonoBehaviour
  {
    public string DebugUnitID;
    private MySound.Voice mUnitVoice;
    [FlexibleArray]
    public UnityEngine.UI.Text[] ProfileTexts;

    public UnitProfileWindow()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      UnitData data = !string.IsNullOrEmpty(this.DebugUnitID) ? MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(this.DebugUnitID) : MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      string skinVoiceSheetName = data.GetUnitSkinVoiceSheetName(-1);
      string sheetName = "VO_" + skinVoiceSheetName;
      string cueNamePrefix = data.GetUnitSkinVoiceCueName(-1) + "_";
      this.mUnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix, false);
      this.PlayProfileVoice();
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), data);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
      if (data == null)
        return;
      for (int index = 0; index < this.ProfileTexts.Length; ++index)
      {
        if (!Object.op_Equality((Object) this.ProfileTexts[index], (Object) null) && !string.IsNullOrEmpty(this.ProfileTexts[index].get_text()))
        {
          StringBuilder stringBuilder = GameUtility.GetStringBuilder();
          stringBuilder.Append("unit.");
          stringBuilder.Append(data.UnitParam.iname);
          stringBuilder.Append("_");
          stringBuilder.Append(this.ProfileTexts[index].get_text());
          this.ProfileTexts[index].set_text(LocalizedText.Get(stringBuilder.ToString()));
        }
      }
    }

    private void PlayProfileVoice()
    {
      if (this.mUnitVoice == null)
        return;
      this.mUnitVoice.Play("chara_0001", 0.0f, false);
    }

    private void OnDestroy()
    {
      if (this.mUnitVoice != null)
      {
        this.mUnitVoice.StopAll(0.0f);
        this.mUnitVoice.Cleanup();
      }
      this.mUnitVoice = (MySound.Voice) null;
    }
  }
}
