// Decompiled with JetBrains decompiler
// Type: SRPG.ConfigWindow
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(11, "ToggleChargeDisp", FlowNode.PinTypes.Input, 1)]
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
    public Toggle MultiUserSetting;
    public InputField MultiUserName;
    public Toggle UseLocalMasterData;
    public Button MasterCheckButton;
    public GameObject AwardState;
    public GameObject SupportIcon;
    public LText ChargeDispText;
    public bool IsModeSentaku;

    public ConfigWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.UpdatePlayerInfo();
          break;
        case 11:
          bool is_disp = !GameUtility.Config_ChargeDisp.Value;
          this.UpdateChargeDispText(is_disp);
          GameUtility.Config_ChargeDisp.Value = is_disp;
          SceneBattle instance = SceneBattle.Instance;
          if (!Object.op_Implicit((Object) instance))
            break;
          instance.ReflectCastSkill(is_disp);
          break;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.SoundVolume, (Object) null))
      {
        this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
        Slider.SliderEvent onValueChanged = this.SoundVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache20 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache20 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__25A));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache20 = ConfigWindow.\u003C\u003Ef__am\u0024cache20;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache20);
      }
      if (Object.op_Inequality((Object) this.MusicVolume, (Object) null))
      {
        this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
        Slider.SliderEvent onValueChanged = this.MusicVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache21 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache21 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__25B));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache21 = ConfigWindow.\u003C\u003Ef__am\u0024cache21;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache21);
      }
      if (Object.op_Inequality((Object) this.VoiceVolume, (Object) null))
      {
        this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
        Slider.SliderEvent onValueChanged = this.VoiceVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache22 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache22 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__25C));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache22 = ConfigWindow.\u003C\u003Ef__am\u0024cache22;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache22);
      }
      if (Object.op_Inequality((Object) this.UseAssetBundle, (Object) null))
      {
        this.UseAssetBundle.set_isOn(GameUtility.Config_UseAssetBundles.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAssetBundle.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache23 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache23 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__25D));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache23 = ConfigWindow.\u003C\u003Ef__am\u0024cache23;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache23);
      }
      if (Object.op_Inequality((Object) this.UseDevServer, (Object) null))
      {
        this.UseDevServer.set_isOn(GameUtility.Config_UseDevServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseDevServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache24 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache24 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__25E));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache24 = ConfigWindow.\u003C\u003Ef__am\u0024cache24;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache24);
      }
      if (Object.op_Inequality((Object) this.UseStgServer, (Object) null))
      {
        this.UseStgServer.set_isOn(GameUtility.Config_UseStgServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseStgServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache25 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache25 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__25F));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache25 = ConfigWindow.\u003C\u003Ef__am\u0024cache25;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache25);
      }
      if (Object.op_Inequality((Object) this.UseAwsServer, (Object) null))
      {
        this.UseAwsServer.set_isOn(GameUtility.Config_UseAwsServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAwsServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache26 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache26 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__260));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache26 = ConfigWindow.\u003C\u003Ef__am\u0024cache26;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache26);
      }
      if (Object.op_Inequality((Object) this.UseAutoPlay, (Object) null))
      {
        this.UseAutoPlay.set_isOn(GameUtility.Config_UseAutoPlay.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAutoPlay.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache27 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache27 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__261));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache27 = ConfigWindow.\u003C\u003Ef__am\u0024cache27;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache27);
      }
      if (Object.op_Inequality((Object) this.UsePushStamina, (Object) null))
      {
        this.UsePushStamina.set_isOn(GameUtility.Config_UsePushStamina.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushStamina.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache28 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache28 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__262));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache28 = ConfigWindow.\u003C\u003Ef__am\u0024cache28;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache28);
      }
      if (Object.op_Inequality((Object) this.UsePushNews, (Object) null))
      {
        this.UsePushNews.set_isOn(GameUtility.Config_UsePushNews.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushNews.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache29 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache29 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__263));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache29 = ConfigWindow.\u003C\u003Ef__am\u0024cache29;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache29);
      }
      if (Object.op_Inequality((Object) this.ToggleChatState, (Object) null))
      {
        this.ToggleChatState.set_isOn(GameUtility.Config_ChatState.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.ToggleChatState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2A == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2A = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__264));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2A = ConfigWindow.\u003C\u003Ef__am\u0024cache2A;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2A);
      }
      if (Object.op_Inequality((Object) this.NewGame, (Object) null))
      {
        this.NewGame.set_isOn(GameUtility.Config_NewGame.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.NewGame.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2B == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2B = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__265));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2B = ConfigWindow.\u003C\u003Ef__am\u0024cache2B;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2B);
      }
      if (Object.op_Inequality((Object) this.MultiUserSetting, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.MultiUserSetting.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__266)));
        ((Component) this.MultiUserSetting).get_gameObject().SetActive(false);
        ((Component) this.MultiUserName).get_gameObject().SetActive(false);
      }
      if (Object.op_Inequality((Object) this.UseLocalMasterData, (Object) null))
      {
        this.UseLocalMasterData.set_isOn(GameUtility.Config_UseLocalData.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseLocalMasterData.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2C == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2C = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__267));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2C = ConfigWindow.\u003C\u003Ef__am\u0024cache2C;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2C);
        ((Component) this.UseLocalMasterData).get_gameObject().SetActive(false);
      }
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.InputMethods[index], (Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.InputMethods[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnInputMethodChange)));
        }
      }
      MoveInputMethods configInputMethod = GameUtility.Config_InputMethod;
      if (configInputMethod < (MoveInputMethods) this.InputMethods.Length && Object.op_Inequality((Object) this.InputMethods[(int) configInputMethod], (Object) null))
        this.InputMethods[(int) configInputMethod].set_isOn(true);
      if (Object.op_Inequality((Object) this.LoginBonus, (Object) null))
        this.LoginBonus.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus != null);
      if (Object.op_Inequality((Object) this.LoginBonus28days, (Object) null))
        this.LoginBonus28days.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus28days != null);
      if (Object.op_Inequality((Object) this.DevServer, (Object) null))
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
        }
        InputField.OnChangeEvent onValueChanged = this.DevServer.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2D == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2D = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__268));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache2D = ConfigWindow.\u003C\u003Ef__am\u0024cache2D;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache2D);
        if (Object.op_Inequality((Object) this.SwitchServer, (Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) this.SwitchServer.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__269)));
        }
        if (Object.op_Inequality((Object) this.CrashButton, (Object) null))
        {
          Button.ButtonClickedEvent onClick = this.CrashButton.get_onClick();
          // ISSUE: reference to a compiler-generated field
          if (ConfigWindow.\u003C\u003Ef__am\u0024cache2E == null)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: method pointer
            ConfigWindow.\u003C\u003Ef__am\u0024cache2E = new UnityAction((object) null, __methodptr(\u003CStart\u003Em__26A));
          }
          // ISSUE: reference to a compiler-generated field
          UnityAction fAmCache2E = ConfigWindow.\u003C\u003Ef__am\u0024cache2E;
          ((UnityEvent) onClick).AddListener(fAmCache2E);
        }
      }
      if (Object.op_Inequality((Object) this.LangSetting, (Object) null) && ((Component) this.LangSetting).get_gameObject().GetActive())
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
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2F == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2F = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__26B));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache2F = ConfigWindow.\u003C\u003Ef__am\u0024cache2F;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache2F);
        if (Object.op_Inequality((Object) this.SwitchLanguage, (Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent) this.SwitchLanguage.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__26C)));
        }
      }
      if (Object.op_Inequality((Object) this.MasterCheckButton, (Object) null))
        ((Component) this.MasterCheckButton).get_gameObject().SetActive(false);
      if (Object.op_Inequality((Object) this.AwardState, (Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (Object.op_Inequality((Object) this.SupportIcon, (Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
      this.UpdateChargeDispText(GameUtility.Config_ChargeDisp.Value);
      GameParameter.UpdateAll(((Component) this).get_gameObject());
    }

    private void OnInputMethodChange(bool y)
    {
      if (!y)
        return;
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.InputMethods[index], (Object) null) && this.InputMethods[index].get_isOn())
        {
          GameUtility.Config_InputMethod = (MoveInputMethods) index;
          break;
        }
      }
    }

    private void UpdatePlayerInfo()
    {
      if (Object.op_Inequality((Object) this.AwardState, (Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (Object.op_Inequality((Object) this.SupportIcon, (Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
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
