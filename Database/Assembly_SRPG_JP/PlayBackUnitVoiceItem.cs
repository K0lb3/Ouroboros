// Decompiled with JetBrains decompiler
// Type: SRPG.PlayBackUnitVoiceItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class PlayBackUnitVoiceItem : MonoBehaviour
  {
    [SerializeField]
    private Text VoiceName;
    [SerializeField]
    private Toggle PlayingBadge;
    [SerializeField]
    private GameObject LockIcon;
    private UnitData.UnitVoiceCueInfo mUnitVoiceCueInfo;
    private string m_CueName;
    private string m_ButtonName;
    private bool m_IsLocked;
    public PlayBackUnitVoiceItem.TapEvent OnTabEvent;

    public PlayBackUnitVoiceItem()
    {
      base.\u002Ector();
    }

    public string CueName
    {
      get
      {
        return this.m_CueName;
      }
    }

    public bool IsLocked
    {
      get
      {
        return this.m_IsLocked;
      }
    }

    private void Start()
    {
    }

    public void SetUp(UnitData.UnitVoiceCueInfo UnitVoiceCueInfo)
    {
      this.mUnitVoiceCueInfo = UnitVoiceCueInfo;
      this.m_CueName = (string) UnitVoiceCueInfo.cueInfo.name;
      this.m_ButtonName = UnitVoiceCueInfo.voice_name;
    }

    public void Refresh()
    {
      if (this.m_ButtonName == null)
        return;
      this.VoiceName.set_text(this.m_ButtonName);
    }

    public void SetPlayingBadge(bool value)
    {
      if (Object.op_Equality((Object) this.PlayingBadge, (Object) null))
        DebugUtility.LogError("PlayingBadgeが指定されていません");
      else
        this.PlayingBadge.set_isOn(value);
    }

    public string GetUnlockConditionsText()
    {
      return this.mUnitVoiceCueInfo.unlock_conditions_text;
    }

    public void Lock()
    {
      this.m_IsLocked = true;
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) this).GetComponentInChildren<SRPG_Button>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        ((Selectable) componentInChildren).set_interactable(false);
      if (!Object.op_Inequality((Object) this.LockIcon, (Object) null))
        return;
      this.LockIcon.SetActive(true);
    }

    public void Unlock()
    {
      this.m_IsLocked = false;
      SRPG_Button componentInChildren = (SRPG_Button) ((Component) this).GetComponentInChildren<SRPG_Button>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
        ((Selectable) componentInChildren).set_interactable(true);
      if (!Object.op_Inequality((Object) this.LockIcon, (Object) null))
        return;
      this.LockIcon.SetActive(false);
    }

    public delegate void TapEvent();
  }
}
