// Decompiled with JetBrains decompiler
// Type: SRPG.UnitModelViewer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "OnBack", FlowNode.PinTypes.Output, 100)]
  public class UnitModelViewer : MonoBehaviour, IFlowInterface
  {
    public static readonly string PLAYBACK_UNITVOICE_PREFAB_PATH = "UI/PlayBackUnitVoice";
    private readonly float TOOLTIP_POSITION_OFFSET_Y;
    [SerializeField]
    private RectTransform JobIconParent;
    [SerializeField]
    private AnimatedToggle JobIconTemplate_Normal;
    [SerializeField]
    private AnimatedToggle JobIconTemplate_CC;
    [SerializeField]
    private GameObject JobNameObject;
    [SerializeField]
    private GameObject TouchArea;
    [SerializeField]
    private Button BackButton;
    [SerializeField]
    private SRPG_Button SkinButton;
    [SerializeField]
    private SRPG_Button ReactionButton;
    [SerializeField]
    private Button VoiceButton;
    [SerializeField]
    private GameObject VoiceUnlock;
    [SerializeField]
    private Tooltip Preafab_UnlockConditionsTooltip;
    private List<AnimatedToggle> mJobIcons;
    private List<AnimatedToggle> mCCIcons;
    private List<AnimatedToggle> mJobSlots;
    public UnitModelViewer.ChangeJobSlotEvent OnChangeJobSlot;
    public UnitModelViewer.SkinSelectEvent OnSkinSelect;
    public UnitModelViewer.PlayReaction OnPlayReaction;
    private TouchControlArea mTouchControlArea;
    private Tooltip mUnlockConditionsTooltip;
    public string JobIconUnlockBool;
    private bool IsInitalized;
    private GameObject mPlayBackVoiceWindow;

    public UnitModelViewer()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Start()
    {
      this.Initalize();
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.mPlayBackVoiceWindow, (Object) null))
        return;
      Object.Destroy((Object) this.mPlayBackVoiceWindow);
    }

    public void Initalize()
    {
      if (this.IsInitalized)
        return;
      if (Object.op_Inequality((Object) this.TouchArea, (Object) null))
      {
        TouchControlArea component = (TouchControlArea) this.TouchArea.GetComponent<TouchControlArea>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.mTouchControlArea = component;
      }
      if (Object.op_Inequality((Object) this.BackButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.BackButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnBack)));
      }
      if (Object.op_Inequality((Object) this.SkinButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.SkinButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnSkinSelectClick)));
      }
      if (Object.op_Inequality((Object) this.ReactionButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ReactionButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnRectionClick)));
      }
      if (Object.op_Inequality((Object) this.VoiceButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.VoiceButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(OnVoiceClick)));
      }
      if (Object.op_Inequality((Object) this.VoiceUnlock, (Object) null))
        this.VoiceUnlock.SetActive(true);
      if (Object.op_Inequality((Object) this.JobIconTemplate_Normal, (Object) null))
        ((Component) this.JobIconTemplate_Normal).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.JobIconTemplate_CC, (Object) null))
        ((Component) this.JobIconTemplate_CC).get_gameObject().SetActive(false);
      for (int index = 0; index < 4; ++index)
      {
        AnimatedToggle animatedToggle1 = (AnimatedToggle) Object.Instantiate<AnimatedToggle>((M0) this.JobIconTemplate_Normal);
        ((Component) animatedToggle1).get_gameObject().SetActive(false);
        ((Component) animatedToggle1).get_transform().SetParent((Transform) this.JobIconParent, false);
        animatedToggle1.OnClick = new AnimatedToggle.ClickEvent(this.OnJobIconClick);
        this.mJobIcons.Add(animatedToggle1);
        AnimatedToggle animatedToggle2 = (AnimatedToggle) Object.Instantiate<AnimatedToggle>((M0) this.JobIconTemplate_CC);
        ((Component) animatedToggle2).get_gameObject().SetActive(false);
        ((Component) animatedToggle2).get_transform().SetParent((Transform) this.JobIconParent, false);
        animatedToggle2.OnClick = new AnimatedToggle.ClickEvent(this.OnJobIconClick);
        this.mCCIcons.Add(animatedToggle2);
      }
      this.IsInitalized = true;
    }

    private void OnJobIconClick(AnimatedToggle toggle)
    {
      if (this.OnChangeJobSlot == null)
        return;
      this.OnChangeJobSlot(this.mJobSlots.IndexOf(toggle));
      GameParameter.UpdateAll(this.JobNameObject);
    }

    private void OnBack()
    {
      if (Object.op_Inequality((Object) this.mTouchControlArea, (Object) null))
        this.mTouchControlArea.Reset();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private void OnSkinSelectClick()
    {
      if (this.OnSkinSelect == null)
        return;
      this.OnSkinSelect(this.SkinButton);
    }

    private void OnRectionClick()
    {
      if (this.OnPlayReaction == null)
        return;
      this.OnPlayReaction();
    }

    private void OnVoiceClick()
    {
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).get_gameObject(), (UnitData) null);
      if (dataOfClass == null)
        DebugUtility.LogError("3DViewerにUnitDataがBindされていません.");
      else if (!dataOfClass.CheckUnlockPlaybackVoice())
        this.ShowUnlockConditionsTooltip(((Component) this.VoiceButton).get_gameObject());
      else
        this.StartCoroutine(this.OpenPlayBackUnitVoice());
    }

    private void OnVoiceClose()
    {
      ((Behaviour) this.mTouchControlArea).set_enabled(true);
    }

    public void Refresh(UnitData unit)
    {
      if (unit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).get_gameObject(), unit);
      int index1 = 0;
      int index2 = 0;
      this.mJobSlots.Clear();
      Toggle.ToggleEvent toggleEvent = new Toggle.ToggleEvent();
      for (int jobNo = 0; jobNo < unit.Jobs.Length; ++jobNo)
      {
        JobData job = unit.Jobs[jobNo];
        if (job != null && job.Param != null)
        {
          if (unit.IsJobAvailable(jobNo))
          {
            this.mJobSlots.Add(this.mJobIcons[index1]);
            ++index1;
          }
          else
          {
            this.mJobSlots.Add(this.mCCIcons[index2]);
            ++index2;
          }
          AnimatedToggle mJobIcon = this.mJobIcons[this.mJobSlots.Count - 1];
          DataSource.Bind<JobData>(((Component) mJobIcon).get_gameObject(), unit.Jobs[jobNo]);
          ((Component) mJobIcon).get_gameObject().SetActive(true);
          ((Selectable) mJobIcon).set_interactable(unit.CheckJobUnlockable(jobNo));
          Toggle.ToggleEvent onValueChanged = (Toggle.ToggleEvent) mJobIcon.onValueChanged;
          mJobIcon.onValueChanged = (__Null) toggleEvent;
          mJobIcon.set_isOn(jobNo == unit.JobIndex);
          mJobIcon.onValueChanged = (__Null) onValueChanged;
          ((Animator) ((Component) mJobIcon).GetComponent<Animator>()).SetBool(this.JobIconUnlockBool, job.IsActivated);
        }
      }
      for (int index3 = index2; index3 < this.mCCIcons.Count; ++index3)
        ((Component) this.mCCIcons[index3]).get_gameObject().SetActive(false);
      for (int index3 = index1; index3 < this.mJobIcons.Count; ++index3)
        ((Component) this.mJobIcons[index3]).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.SkinButton, (Object) null))
        ((Selectable) this.SkinButton).set_interactable(unit.IsSkinUnlocked());
      DataSource.Bind<UnitData>(this.JobNameObject, unit);
      bool flag = true;
      for (int index3 = 0; index3 < this.mJobSlots.Count; ++index3)
      {
        this.mJobSlots[index3].set_isOn(unit.JobIndex == index3);
        Animator component = (Animator) ((Component) this.mJobSlots[index3]).GetComponent<Animator>();
        int num = 0;
        do
        {
          component.Update(!flag ? 0.0f : 1f);
          ++num;
        }
        while (component.IsInTransition(0) && num < 10);
      }
      if (Object.op_Inequality((Object) this.VoiceButton, (Object) null) && Object.op_Inequality((Object) this.VoiceUnlock, (Object) null))
        this.VoiceUnlock.SetActive(!unit.CheckUnlockPlaybackVoice());
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    [DebuggerHidden]
    private IEnumerator OpenPlayBackUnitVoice()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new UnitModelViewer.\u003COpenPlayBackUnitVoice\u003Ec__Iterator13F() { \u003C\u003Ef__this = this };
    }

    private void ShowUnlockConditionsTooltip(GameObject _target_obj)
    {
      if (Object.op_Equality((Object) this.Preafab_UnlockConditionsTooltip, (Object) null))
        return;
      if (Object.op_Equality((Object) this.mUnlockConditionsTooltip, (Object) null))
        this.mUnlockConditionsTooltip = (Tooltip) Object.Instantiate<Tooltip>((M0) this.Preafab_UnlockConditionsTooltip);
      else
        this.mUnlockConditionsTooltip.ResetPosition();
      RectTransform component = (RectTransform) _target_obj.GetComponent<RectTransform>();
      Vector2 localPos = (Vector2) null;
      localPos.x = (__Null) 0.0;
      localPos.y = (__Null) (component.get_sizeDelta().y / 2.0 + (double) this.TOOLTIP_POSITION_OFFSET_Y);
      Tooltip.SetTooltipPosition(component, localPos);
      if (!Object.op_Inequality((Object) this.mUnlockConditionsTooltip.TooltipText, (Object) null))
        return;
      this.mUnlockConditionsTooltip.TooltipText.set_text(LocalizedText.Get("sys.UNLOCK_CONDITIONS_PLAYBACK_FUNCTION"));
    }

    public delegate void ChangeJobSlotEvent(int index);

    public delegate void SkinSelectEvent(SRPG_Button button);

    public delegate void PlayReaction();
  }
}
