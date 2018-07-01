// Decompiled with JetBrains decompiler
// Type: SRPG.InGameMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Start Debug", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(2, "Give Up", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(100, "Close Give Up Window", FlowNode.PinTypes.Input, 0)]
  public class InGameMenu : MonoBehaviour, IFlowInterface
  {
    public const int PINID_DEBUG = 1;
    public const int PINID_GIVEUP = 2;
    public const int PINID_CLOSE_GIVEUP_WINDOW = 100;
    public GameObject MissionButton;
    public GameObject ExitButton;
    public GameObject OptionButton;
    public GameObject DebugButton;
    public Button AutoPlayOn;
    public Button AutoPlayOff;
    public Toggle AutoPlay;
    public GameObject AutoMode_Parent;
    public GameObject AutoMode_Treasure;
    public GameObject AutoMode_Skill;
    private GameObject mGiveUpWindow;
    public bool HideMissionButton;

    public InGameMenu()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      QuestParam questParam = (QuestParam) null;
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null))
      {
        questParam = instance.CurrentQuest;
        instance.OnQuestEnd += new SceneBattle.QuestEndEvent(this.OnQuestEnd);
        if (questParam != null && questParam.CheckAllowedAutoBattle())
        {
          if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          {
            ((Component) this.AutoPlayOn).get_gameObject().SetActive(!instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent) this.AutoPlayOn.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(TurnOnAutoPlay)));
          }
          if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          {
            ((Component) this.AutoPlayOff).get_gameObject().SetActive(instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent) this.AutoPlayOff.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(TurnOffAutoPlay)));
          }
          if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          {
            ((Component) this.AutoPlay).get_gameObject().SetActive(true);
            GameUtility.SetToggle(this.AutoPlay, instance.Battle.RequestAutoBattle);
            // ISSUE: method pointer
            ((UnityEvent<bool>) this.AutoPlay.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__123)));
          }
          if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
            this.AutoMode_Parent.get_gameObject().SetActive(instance.Battle.RequestAutoBattle);
          if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
            this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
          if (Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
            this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
        }
        else
        {
          if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
            ((Component) this.AutoPlayOn).get_gameObject().SetActive(false);
          if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
            ((Component) this.AutoPlayOff).get_gameObject().SetActive(false);
          if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
            ((Component) this.AutoPlay).get_gameObject().SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
            this.AutoMode_Parent.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
            this.AutoMode_Treasure.SetActive(false);
          if (Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
            this.AutoMode_Skill.SetActive(false);
        }
      }
      if (Object.op_Inequality((Object) this.DebugButton, (Object) null))
        this.DebugButton.SetActive(false);
      if (Object.op_Inequality((Object) this.MissionButton, (Object) null) && questParam != null)
      {
        bool flag = questParam.HasMission();
        if (Object.op_Implicit((Object) instance) && instance.Battle != null && (instance.Battle.IsOrdeal && questParam.state != QuestStates.Cleared))
          flag = false;
        if (this.HideMissionButton)
        {
          this.MissionButton.SetActive(flag);
        }
        else
        {
          Selectable component = (Selectable) this.MissionButton.GetComponent<Selectable>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.set_interactable(flag);
        }
      }
      if (!Object.op_Inequality((Object) this.ExitButton, (Object) null))
        return;
      bool flag1 = questParam != null && questParam.CheckAllowedRetreat();
      this.ExitButton.SetActive(flag1);
      if (!flag1 || !Object.op_Inequality((Object) instance, (Object) null) || !instance.IsPlayingArenaQuest)
        return;
      ((Text) this.ExitButton.GetComponentInChildren<LText>(true)).set_text(LocalizedText.Get("sys.BTN_RETIRE_ARENA"));
      if (!instance.Battle.IsArenaSkip)
        return;
      Button component1 = (Button) this.ExitButton.GetComponent<Button>();
      if (!Object.op_Implicit((Object) component1))
        return;
      ((Selectable) component1).set_interactable(false);
    }

    private void ToggleAutoPlay(bool enable)
    {
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
      {
        SceneBattle.Instance.Battle.RequestAutoBattle = enable;
        if (enable)
          GameUtility.SetNeverSleep();
        else
          GameUtility.SetDefaultSleepSetting();
      }
      if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
        ((Component) this.AutoPlayOn).get_gameObject().SetActive(!enable);
      if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
        ((Component) this.AutoPlayOff).get_gameObject().SetActive(enable);
      if (!Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
        return;
      this.AutoMode_Parent.get_gameObject().SetActive(true);
      Animator component = (Animator) this.AutoMode_Parent.GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool("open", enable);
    }

    private void TurnOnAutoPlay()
    {
      this.ToggleAutoPlay(true);
    }

    private void TurnOffAutoPlay()
    {
      this.ToggleAutoPlay(false);
    }

    public void ToggleAutoBattle(bool is_active)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Implicit((Object) instance) || instance.CurrentQuest == null || !instance.CurrentQuest.CheckAllowedAutoBattle())
        return;
      if (is_active)
      {
        if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          ((Component) this.AutoPlayOn).get_gameObject().SetActive(!instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          ((Component) this.AutoPlayOff).get_gameObject().SetActive(instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          ((Component) this.AutoPlay).get_gameObject().SetActive(true);
        if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
          this.AutoMode_Parent.get_gameObject().SetActive(instance.Battle.RequestAutoBattle);
        if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
          this.AutoMode_Treasure.SetActive(GameUtility.Config_AutoMode_Treasure.Value);
        if (!Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
          return;
        this.AutoMode_Skill.SetActive(GameUtility.Config_AutoMode_DisableSkill.Value);
      }
      else
      {
        if (Object.op_Inequality((Object) this.AutoPlayOn, (Object) null))
          ((Component) this.AutoPlayOn).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.AutoPlayOff, (Object) null))
          ((Component) this.AutoPlayOff).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.AutoPlay, (Object) null))
          ((Component) this.AutoPlay).get_gameObject().SetActive(false);
        if (Object.op_Inequality((Object) this.AutoMode_Parent, (Object) null))
          this.AutoMode_Parent.SetActive(false);
        if (Object.op_Inequality((Object) this.AutoMode_Treasure, (Object) null))
          this.AutoMode_Treasure.SetActive(false);
        if (!Object.op_Inequality((Object) this.AutoMode_Skill, (Object) null))
          return;
        this.AutoMode_Skill.SetActive(false);
      }
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        return;
      SceneBattle.Instance.OnQuestEnd -= new SceneBattle.QuestEndEvent(this.OnQuestEnd);
    }

    private void OnQuestEnd()
    {
      this.Activated(100);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 2:
          string text = LocalizedText.Get("sys.CONFIRM_GIVEUP");
          if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) && SceneBattle.Instance.IsPlayingArenaQuest)
            text = LocalizedText.Get("sys.CONFIRM_GIVEUP_ARENA");
          this.mGiveUpWindow = UIUtility.ConfirmBox(text, new UIUtility.DialogResultEvent(this.OnGiveUp), (UIUtility.DialogResultEvent) null, (GameObject) null, true, 1, (string) null, (string) null);
          break;
        case 100:
          if (!Object.op_Inequality((Object) this.mGiveUpWindow, (Object) null))
            break;
          Win_Btn_DecideCancel_FL_C component = (Win_Btn_DecideCancel_FL_C) this.mGiveUpWindow.GetComponent<Win_Btn_DecideCancel_FL_C>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.BeginClose();
          this.mGiveUpWindow = (GameObject) null;
          break;
      }
    }

    private void OnGiveUp(GameObject go)
    {
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) && SceneBattle.Instance.IsPlayingArenaQuest)
        SceneBattle.Instance.ForceEndQuestInArena();
      else
        SceneBattle.Instance.ForceEndQuest();
      CanvasGroup component = (CanvasGroup) ((Component) this).GetComponent<CanvasGroup>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.set_blocksRaycasts(false);
    }
  }
}
