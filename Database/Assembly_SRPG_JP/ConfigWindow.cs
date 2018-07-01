// Decompiled with JetBrains decompiler
// Type: SRPG.ConfigWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

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
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SoundVolume, (UnityEngine.Object) null))
      {
        this.SoundVolume.set_value(GameUtility.Config_SoundVolume);
        Slider.SliderEvent onValueChanged = this.SoundVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache1E == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache1E = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__2E0));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache1E = ConfigWindow.\u003C\u003Ef__am\u0024cache1E;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache1E);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MusicVolume, (UnityEngine.Object) null))
      {
        this.MusicVolume.set_value(GameUtility.Config_MusicVolume);
        Slider.SliderEvent onValueChanged = this.MusicVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache1F == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache1F = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__2E1));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache1F = ConfigWindow.\u003C\u003Ef__am\u0024cache1F;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache1F);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceVolume, (UnityEngine.Object) null))
      {
        this.VoiceVolume.set_value(GameUtility.Config_VoiceVolume);
        Slider.SliderEvent onValueChanged = this.VoiceVolume.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache20 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache20 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__2E2));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache20 = ConfigWindow.\u003C\u003Ef__am\u0024cache20;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache20);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAssetBundle, (UnityEngine.Object) null))
      {
        this.UseAssetBundle.set_isOn(GameUtility.Config_UseAssetBundles.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAssetBundle.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache21 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache21 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E3));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache21 = ConfigWindow.\u003C\u003Ef__am\u0024cache21;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache21);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseDevServer, (UnityEngine.Object) null))
      {
        this.UseDevServer.set_isOn(GameUtility.Config_UseDevServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseDevServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache22 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache22 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E4));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache22 = ConfigWindow.\u003C\u003Ef__am\u0024cache22;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache22);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAwsServer, (UnityEngine.Object) null))
      {
        this.UseAwsServer.set_isOn(GameUtility.Config_UseAwsServer.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAwsServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache23 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache23 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E5));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache23 = ConfigWindow.\u003C\u003Ef__am\u0024cache23;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache23);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAutoPlay, (UnityEngine.Object) null))
      {
        this.UseAutoPlay.set_isOn(GameUtility.Config_UseAutoPlay.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseAutoPlay.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache24 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache24 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E6));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache24 = ConfigWindow.\u003C\u003Ef__am\u0024cache24;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache24);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushStamina, (UnityEngine.Object) null))
      {
        this.UsePushStamina.set_isOn(GameUtility.Config_UsePushStamina.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushStamina.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache25 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache25 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E7));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache25 = ConfigWindow.\u003C\u003Ef__am\u0024cache25;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache25);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushNews, (UnityEngine.Object) null))
      {
        this.UsePushNews.set_isOn(GameUtility.Config_UsePushNews.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UsePushNews.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache26 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache26 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E8));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache26 = ConfigWindow.\u003C\u003Ef__am\u0024cache26;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache26);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationFlag, (UnityEngine.Object) null))
      {
        bool multiInvitaionFlag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        GlobalVars.MultiInvitaionFlag = multiInvitaionFlag;
        this.MultiInvitationFlag.set_isOn(multiInvitaionFlag);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.MultiInvitationFlag.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache27 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache27 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2E9));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache27 = ConfigWindow.\u003C\u003Ef__am\u0024cache27;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache27);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationComment, (UnityEngine.Object) null))
      {
        string invitaionComment = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionComment;
        GlobalVars.MultiInvitaionComment = invitaionComment;
        if (!string.IsNullOrEmpty(invitaionComment))
          this.MultiInvitationComment.SetText(invitaionComment);
        InputField.OnChangeEvent onValueChanged = this.MultiInvitationComment.get_onValueChanged();
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache28 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache28 = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__2EA));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCache28 = ConfigWindow.\u003C\u003Ef__am\u0024cache28;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCache28);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleChatState, (UnityEngine.Object) null))
      {
        this.ToggleChatState.set_isOn(GameUtility.Config_ChatState.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.ToggleChatState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache29 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache29 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2EB));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache29 = ConfigWindow.\u003C\u003Ef__am\u0024cache29;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache29);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleMultiState, (UnityEngine.Object) null))
      {
        this.ToggleMultiState.set_isOn(GameUtility.Config_MultiState.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.ToggleMultiState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2A == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2A = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2EC));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2A = ConfigWindow.\u003C\u003Ef__am\u0024cache2A;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2A);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewGame, (UnityEngine.Object) null))
      {
        this.NewGame.set_isOn(GameUtility.Config_NewGame.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.NewGame.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2B == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2B = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2ED));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2B = ConfigWindow.\u003C\u003Ef__am\u0024cache2B;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2B);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiUserSetting, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.MultiUserSetting.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__2EE)));
        ((Component) this.MultiUserSetting).get_gameObject().SetActive(false);
        ((Component) this.MultiUserName).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLocalMasterData, (UnityEngine.Object) null))
      {
        this.UseLocalMasterData.set_isOn(GameUtility.Config_UseLocalData.Value);
        // ISSUE: variable of the null type
        __Null onValueChanged = this.UseLocalMasterData.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2C == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2C = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__2EF));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache2C = ConfigWindow.\u003C\u003Ef__am\u0024cache2C;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache2C);
        ((Component) this.UseLocalMasterData).get_gameObject().SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceCopyButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.VoiceCopyButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__2F0)));
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
          if (dropItemDataList[index2].mIsSecret == record.items[index1].mIsSecret)
          {
            if (dropItemDataList[index2].IsItem)
            {
              if (dropItemDataList[index2].itemParam == record.items[index1].itemParam)
              {
                dropItemDataList[index2].Gain(1);
                flag = true;
                break;
              }
            }
            else if (dropItemDataList[index2].IsConceptCard && dropItemDataList[index2].conceptCardParam == record.items[index1].conceptCardParam)
            {
              dropItemDataList[index2].Gain(1);
              flag = true;
              break;
            }
          }
        }
        if (!flag)
        {
          QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
          if (record.items[index1].IsItem)
          {
            dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, record.items[index1].itemParam.iname, 1);
            if (record.items[index1].itemParam.type != EItemType.Unit)
            {
              ItemData itemDataByItemParam = player.FindItemDataByItemParam(record.items[index1].itemParam);
              dropItemData.IsNew = !player.ItemEntryExists(record.items[index1].itemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
            }
            else
            {
              string iid = record.items[index1].itemParam.iname;
              if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                dropItemData.IsNew = true;
            }
          }
          else if (record.items[index1].IsConceptCard)
            dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, record.items[index1].conceptCardParam.iname, 1);
          dropItemData.mIsSecret = record.items[index1].mIsSecret;
          if (dropItemData.mIsSecret)
            dropItemData.IsNew = false;
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
        DataSource.Bind<QuestResult.DropItemData>(root, item);
        if (item.mIsSecret)
        {
          DropItemIcon component = (DropItemIcon) root.GetComponent<DropItemIcon>();
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
