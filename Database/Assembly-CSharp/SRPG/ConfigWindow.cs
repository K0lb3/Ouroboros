// Decompiled with JetBrains decompiler
// Type: SRPG.ConfigWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "UpdatePlayerInfo", FlowNode.PinTypes.Input, 0)]
  public class ConfigWindow : MonoBehaviour, IFlowInterface
  {
    public Slider SoundVolume;
    public Slider MusicVolume;
    public Slider VoiceVolume;
    public Toggle UseAssetBundle;
    public Toggle UseDevServer;
    public Toggle UseAwsServer;
    public Toggle NewGame;
    public Toggle[] InputMethods;
    public Toggle UseAutoPlay;
    public Toggle UsePushStamina;
    public Toggle UsePushNews;
    public Toggle MultiInvitationFlag;
    public InputField MultiInvitationComment;
    public GameObject LoginBonus;
    public GameObject LoginBonus28days;
    public InputField AssetVersion;
    public Toggle UseStgServer;
    public InputField DevServer;
    public InputField StgServer;
    public InputField LangSetting;
    public Button SwitchServer;
    public Button SwitchLanguage;
    public Button CrashButton;
    private int devServerSetting;
    private int devLangSetting;
    public Toggle ToggleChatState;
    public Toggle ToggleMultiState;
    public Toggle MultiUserSetting;
    public InputField MultiUserName;
    public Toggle UseLocalMasterData;
    public Button MasterCheckButton;
    public Button VoiceCopyButton;
    public InputField ClientExPath;
    public GameObject AwardState;
    public GameObject SupportIcon;
    public GameObject Prefab_NewItemBadge;
    public GameObject TreasureList;
    public GameObject TreasureListNode;
    private List<GameObject> mTreasureListNodes;
    public bool IsModeSentaku;

    public ConfigWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.UpdatePlayerInfo();
    }

    private void Start()
    {
      this.IsModeSentaku = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SoundVolume, (UnityEngine.Object) null))
      {
        this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
        Slider.SliderEvent onValueChanged = this.SoundVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache28 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache28 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__340));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache28 = ConfigWindow.\u003C\u003Ef__am\u0024cache28;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache28);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MusicVolume, (UnityEngine.Object) null))
      {
        this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
        Slider.SliderEvent onValueChanged = this.MusicVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache29 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache29 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__341));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache29 = ConfigWindow.\u003C\u003Ef__am\u0024cache29;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache29);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceVolume, (UnityEngine.Object) null))
      {
        this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
        Slider.SliderEvent onValueChanged = this.VoiceVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2A == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2A = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__342));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache2A = ConfigWindow.\u003C\u003Ef__am\u0024cache2A;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache2A);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAssetBundle, (UnityEngine.Object) null))
      {
        this.UseAssetBundle.set_isOn(GameUtility.Config_UseAssetBundles.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAssetBundle.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2B == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2B = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__343));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2B = ConfigWindow.\u003C\u003Ef__am\u0024cache2B;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2B);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseDevServer, (UnityEngine.Object) null))
      {
        this.UseDevServer.set_isOn(GameUtility.Config_UseDevServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseDevServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2C == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2C = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__344));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2C = ConfigWindow.\u003C\u003Ef__am\u0024cache2C;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2C);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAwsServer, (UnityEngine.Object) null))
      {
        this.UseAwsServer.set_isOn(GameUtility.Config_UseAwsServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAwsServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2D == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2D = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__345));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2D = ConfigWindow.\u003C\u003Ef__am\u0024cache2D;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2D);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAutoPlay, (UnityEngine.Object) null))
      {
        this.UseAutoPlay.set_isOn(GameUtility.Config_UseAutoPlay.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAutoPlay.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2E == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2E = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__346));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2E = ConfigWindow.\u003C\u003Ef__am\u0024cache2E;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2E);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushStamina, (UnityEngine.Object) null))
      {
        this.UsePushStamina.set_isOn(GameUtility.Config_UsePushStamina.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushStamina.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2F == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2F = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__347));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2F = ConfigWindow.\u003C\u003Ef__am\u0024cache2F;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2F);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushNews, (UnityEngine.Object) null))
      {
        this.UsePushNews.set_isOn(GameUtility.Config_UsePushNews.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushNews.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache30 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache30 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__348));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache30 = ConfigWindow.\u003C\u003Ef__am\u0024cache30;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache30);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationFlag, (UnityEngine.Object) null))
      {
        bool multiInvitaionFlag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        GlobalVars.MultiInvitaionFlag = multiInvitaionFlag;
        this.MultiInvitationFlag.set_isOn(multiInvitaionFlag);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.MultiInvitationFlag.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache31 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache31 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__349));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache31 = ConfigWindow.\u003C\u003Ef__am\u0024cache31;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache31);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationComment, (UnityEngine.Object) null))
      {
        string invitaionComment = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionComment;
        GlobalVars.MultiInvitaionComment = invitaionComment;
        if (!string.IsNullOrEmpty(invitaionComment))
          this.MultiInvitationComment.SetText(invitaionComment);
        InputField.OnChangeEvent onValueChanged = this.MultiInvitationComment.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache32 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache32 = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__34A));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache32 = ConfigWindow.\u003C\u003Ef__am\u0024cache32;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache32);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleChatState, (UnityEngine.Object) null))
      {
        this.ToggleChatState.set_isOn(GameUtility.Config_ChatState.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.ToggleChatState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache33 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache33 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__34B));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache33 = ConfigWindow.\u003C\u003Ef__am\u0024cache33;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache33);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleMultiState, (UnityEngine.Object) null))
      {
        this.ToggleMultiState.set_isOn(GameUtility.Config_MultiState.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.ToggleMultiState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache34 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache34 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__34C));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache34 = ConfigWindow.\u003C\u003Ef__am\u0024cache34;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache34);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewGame, (UnityEngine.Object) null))
      {
        this.NewGame.set_isOn(GameUtility.Config_NewGame.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.NewGame.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache35 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache35 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__34D));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache35 = ConfigWindow.\u003C\u003Ef__am\u0024cache35;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache35);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiUserSetting, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.MultiUserSetting.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__34E)));
        ((Component) this.MultiUserSetting).get_gameObject().SetActive(false);
        ((Component) this.MultiUserName).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLocalMasterData, (UnityEngine.Object) null))
      {
        this.UseLocalMasterData.set_isOn(GameUtility.Config_UseLocalData.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseLocalMasterData.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache36 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache36 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__34F));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache36 = ConfigWindow.\u003C\u003Ef__am\u0024cache36;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache36);
        ((Component) this.UseLocalMasterData).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceCopyButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.VoiceCopyButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__350)));
        ((Component) ((Component) this.VoiceCopyButton).get_gameObject().get_transform().get_parent()).get_gameObject().SetActive(false);
      }
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[index], (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.InputMethods[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnInputMethodChange)));
        }
      }
      MoveInputMethods configInputMethod = GameUtility.Config_InputMethod;
      if (configInputMethod < (MoveInputMethods) this.InputMethods.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[(int) configInputMethod], (UnityEngine.Object) null))
        this.InputMethods[(int) configInputMethod].set_isOn(true);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginBonus, (UnityEngine.Object) null))
        this.LoginBonus.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus != null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginBonus28days, (UnityEngine.Object) null))
        this.LoginBonus28days.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus28days != null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DevServer, (UnityEngine.Object) null))
      {
        string devServerSetting = GameUtility.DevServerSetting;
        if (!string.IsNullOrEmpty(devServerSetting))
        {
          this.DevServer.set_text(devServerSetting);
          if (devServerSetting == "http://dev01-app.alcww.gumi.sg/")
            this.devServerSetting = 0;
          if (devServerSetting == "http://dev02-app.alcww.gumi.sg/")
            this.devServerSetting = 1;
          if (devServerSetting == "http://dev03-app.alcww.gumi.sg/")
            this.devServerSetting = 2;
          if (devServerSetting == "http://dev04-app.alcww.gumi.sg/")
            this.devServerSetting = 3;
          if (devServerSetting == "http://dev05-app.alcww.gumi.sg/")
            this.devServerSetting = 4;
          if (devServerSetting == "http://stg-app.alcww.gumi.sg/")
            this.devServerSetting = 5;
          if (devServerSetting == "http://stg02-app.alcww.gumi.sg/")
            this.devServerSetting = 6;
          if (devServerSetting == "http://dev06-app.alcww.gumi.sg/")
            this.devServerSetting = 7;
          if (devServerSetting == "http://stg03-app.alcww.gumi.sg/")
            this.devServerSetting = 8;
        }
        InputField.OnChangeEvent onValueChanged = this.DevServer.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache37 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache37 = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__351));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache37 = ConfigWindow.\u003C\u003Ef__am\u0024cache37;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache37);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SwitchServer, (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) this.SwitchServer.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__352)));
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CrashButton, (UnityEngine.Object) null))
        {
          Button.ButtonClickedEvent onClick = this.CrashButton.get_onClick();
          // ISSUE: reference to a compiler-generated field
          if (ConfigWindow.\u003C\u003Ef__am\u0024cache38 == null)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            ConfigWindow.\u003C\u003Ef__am\u0024cache38 = new UnityAction((object) null, __methodptr(\u003CStart\u003Em__353));
          }
          // ISSUE: reference to a compiler-generated field
          UnityAction fAmCache38 = ConfigWindow.\u003C\u003Ef__am\u0024cache38;
          ((UnityEvent) onClick).AddListener(fAmCache38);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LangSetting, (UnityEngine.Object) null) && ((Component) this.LangSetting).get_gameObject().GetActive())
      {
        string configLanguage = GameUtility.Config_Language;
        if (configLanguage == "english")
          this.devLangSetting = 0;
        if (configLanguage == "french")
          this.devLangSetting = 1;
        if (configLanguage == "german")
          this.devLangSetting = 2;
        if (configLanguage == "spanish")
          this.devLangSetting = 3;
        this.LangSetting.set_text(configLanguage);
        InputField.OnChangeEvent onValueChanged = this.LangSetting.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache39 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache39 = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__354));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache39 = ConfigWindow.\u003C\u003Ef__am\u0024cache39;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache39);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SwitchLanguage, (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) this.SwitchLanguage.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__355)));
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MasterCheckButton, (UnityEngine.Object) null))
        ((Component) this.MasterCheckButton).get_gameObject().SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwardState, (UnityEngine.Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportIcon, (UnityEngine.Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
      ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, ((Component) this).get_gameObject(), this.mTreasureListNodes);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnInputMethodChange(bool y)
    {
      if (!y)
        return;
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[index], (UnityEngine.Object) null) && this.InputMethods[index].get_isOn())
        {
          GameUtility.Config_InputMethod = (MoveInputMethods) index;
          break;
        }
      }
    }

    private void UpdatePlayerInfo()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwardState, (UnityEngine.Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportIcon, (UnityEngine.Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    public static void SetupTreasureList(GameObject list, GameObject node, GameObject newIcon, GameObject owner, List<GameObject> itemNodes)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) node, (UnityEngine.Object) null))
        node.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) newIcon, (UnityEngine.Object) null) && newIcon.get_gameObject().get_activeInHierarchy())
        newIcon.SetActive(false);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      BattleCore battle = instance.Battle;
      BattleCore.Record record = new BattleCore.Record();
      battle.GainUnitSteal(record);
      battle.GainUnitDrop(record, true);
      DataSource.Bind<BattleCore.Record>(owner, record);
      if (record == null)
        return;
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) list, (UnityEngine.Object) null) ? node.get_transform().get_parent() : list.get_transform();
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      for (int index1 = 0; index1 < record.items.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < dropItemDataList.Count; ++index2)
        {
          if (dropItemDataList[index2].Param == record.items[index1].mItemParam && dropItemDataList[index2].mIsSecret == record.items[index1].mIsSecret)
          {
            dropItemDataList[index2].Gain(1);
            flag = true;
            break;
          }
        }
        if (!flag)
        {
          ItemData itemDataByItemParam = player.FindItemDataByItemParam(record.items[index1].mItemParam);
          QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
          dropItemData.Setup(0L, record.items[index1].mItemParam.iname, 1);
          dropItemData.mIsSecret = record.items[index1].mIsSecret;
          if (record.items[index1].mItemParam.type != EItemType.Unit)
          {
            dropItemData.IsNew = !player.ItemEntryExists(record.items[index1].mItemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: reference to a compiler-generated method
            if (player.Units.Find(new Predicate<UnitData>(new ConfigWindow.\u003CSetupTreasureList\u003Ec__AnonStorey322() { iid = record.items[index1].mItemParam.iname }.\u003C\u003Em__356)) == null)
              dropItemData.IsNew = true;
          }
          dropItemDataList.Add(dropItemData);
        }
      }
      for (int index = 0; index < dropItemDataList.Count; ++index)
      {
        GameObject itemObject = ConfigWindow.CreateItemObject(node, newIcon, dropItemDataList[index]);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) itemObject, (UnityEngine.Object) null))
        {
          itemObject.get_transform().SetParent(transform, false);
          itemNodes.Add(itemObject);
        }
      }
    }

    public static GameObject CreateItemObject(GameObject node, GameObject newIcon, QuestResult.DropItemData item)
    {
      GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) node);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        DataSource.Bind<ItemData>(root, (ItemData) item);
        if (item.mIsSecret)
        {
          ItemIcon component = (ItemIcon) root.GetComponent<ItemIcon>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.IsSecret = true;
        }
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) newIcon, (UnityEngine.Object) null) && item.IsNew)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) newIcon);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            RectTransform transform = gameObject.get_transform() as RectTransform;
            ((Component) transform).get_gameObject().SetActive(true);
            transform.set_anchoredPosition(Vector2.get_zero());
            ((Transform) transform).SetParent(root.get_transform(), false);
          }
        }
      }
      return root;
    }
  }
}
