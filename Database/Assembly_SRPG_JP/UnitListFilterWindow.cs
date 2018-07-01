// Decompiled with JetBrains decompiler
// Type: SRPG.UnitListFilterWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitListFilterWindow : FlowWindowBase
  {
    private Dictionary<UnitListFilterWindow.SelectType, Toggle> m_Toggles = new Dictionary<UnitListFilterWindow.SelectType, Toggle>();
    private const string SAVEKEY = "UNITLIST_FILTER";
    private const string SAVEKEY_OLD = "UNITLIST";
    public const UnitListFilterWindow.SelectType MASK_RARITY = UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5;
    public const UnitListFilterWindow.SelectType MASK_WEAPON = UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE;
    public const UnitListFilterWindow.SelectType MASK_BIRTH = UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ;
    public const UnitListFilterWindow.SelectType MASK_SELECT = UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ;
    private UnitListFilterWindow.SerializeParam m_Param;
    private SerializeValueList m_ValueList;
    private UnitListWindow m_Root;
    private UnitListFilterWindow.Result m_Result;
    private UnitListFilterWindow.SelectType m_SelectTypeReg;
    private UnitListFilterWindow.SelectType m_SelectType;

    public override string name
    {
      get
      {
        return nameof (UnitListFilterWindow);
      }
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      base.Initialize(param);
      this.m_Param = param as UnitListFilterWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      SerializeValueBehaviour childComponent = this.GetChildComponent<SerializeValueBehaviour>("filter");
      this.m_ValueList = !UnityEngine.Object.op_Inequality((UnityEngine.Object) childComponent, (UnityEngine.Object) null) ? new SerializeValueList() : childComponent.list;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        this.CacheToggleParam(((Component) this.m_Animator).get_gameObject());
      this.LoadSelectType();
      this.Close(true);
    }

    private void CreateInstance()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Window, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
        return;
      string name = this.m_ValueList.GetString("path");
      if (string.IsNullOrEmpty(name))
        return;
      GameObject gameObject = AssetManager.Load<GameObject>(name);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      GameObject toggle_parent_obj = (GameObject) null;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_Animator, (UnityEngine.Object) null))
      {
        toggle_parent_obj = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) gameObject);
        toggle_parent_obj.get_transform().SetParent(this.m_Window.get_transform(), false);
        this.m_Animator = (Animator) toggle_parent_obj.GetComponent<Animator>();
      }
      this.CacheToggleParam(toggle_parent_obj);
    }

    private void CacheToggleParam(GameObject toggle_parent_obj)
    {
      Toggle[] componentsInChildren = (Toggle[]) toggle_parent_obj.GetComponentsInChildren<Toggle>();
      for (int index = 0; index < componentsInChildren.Length; ++index)
      {
        try
        {
          UnitListFilterWindow.SelectType key = (UnitListFilterWindow.SelectType) Enum.Parse(typeof (UnitListFilterWindow.SelectType), ((UnityEngine.Object) componentsInChildren[index]).get_name());
          ButtonEvent component = (ButtonEvent) ((Component) componentsInChildren[index]).GetComponent<ButtonEvent>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          {
            ButtonEvent.Event @event = component.GetEvent("UNITFILTER_TGL_CHANGE");
            if (@event != null)
              @event.valueList.SetField("select", (int) key);
          }
          this.m_Toggles.Add(key, componentsInChildren[index]);
        }
        catch (Exception ex)
        {
          Debug.LogError((object) ("UnitSortWindow トグル名からSelectTypeを取得できない！ > " + ((UnityEngine.Object) componentsInChildren[index]).get_name() + " ( Exception > " + ex.ToString() + " )"));
        }
      }
    }

    public override void Release()
    {
      base.Release();
    }

    public override int Update()
    {
      base.Update();
      if (this.m_Result != UnitListFilterWindow.Result.NONE)
      {
        if (this.isClosed)
          this.SetActiveChild(false);
      }
      else if (this.isClosed)
        this.SetActiveChild(false);
      return -1;
    }

    private void InitializeToggleContent()
    {
      using (Dictionary<UnitListFilterWindow.SelectType, Toggle>.Enumerator enumerator = this.m_Toggles.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          KeyValuePair<UnitListFilterWindow.SelectType, Toggle> current = enumerator.Current;
          if ((this.m_SelectType & current.Key) != UnitListFilterWindow.SelectType.NONE)
            current.Value.set_isOn(true);
          else
            current.Value.set_isOn(false);
        }
      }
      this.m_SelectTypeReg = this.m_SelectType;
    }

    private void ReleaseToggleContent()
    {
    }

    public void SetRoot(UnitListWindow root)
    {
      this.m_Root = root;
    }

    private void SetSelect(UnitListFilterWindow.SelectType selectType)
    {
      this.m_SelectType |= selectType & (UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ);
    }

    private void ResetSelect(UnitListFilterWindow.SelectType selectType)
    {
      this.m_SelectType &= ~(selectType & (UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ));
    }

    public UnitListFilterWindow.SelectType GetSelect(UnitListFilterWindow.SelectType mask)
    {
      return this.m_SelectType & mask;
    }

    public UnitListFilterWindow.SelectType GetSelectReg(UnitListFilterWindow.SelectType mask)
    {
      return this.m_SelectTypeReg & mask;
    }

    public void CalcUnit(List<UnitListWindow.Data> list)
    {
      UnitListFilterWindow.SelectType selectType = ~this.m_SelectType & (UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ);
      for (int index = list.Count - 1; index >= 0; --index)
      {
        if ((list[index].filterMask & selectType) != UnitListFilterWindow.SelectType.NONE)
          list.RemoveAt(index);
      }
    }

    public void LoadSelectType()
    {
      this.m_SelectType = UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ;
      string key = "UNITLIST_FILTER";
      if (!string.IsNullOrEmpty(key))
      {
        if (PlayerPrefsUtility.HasKey(key))
        {
          string s = PlayerPrefsUtility.GetString(key, string.Empty);
          int result = 0;
          if (!string.IsNullOrEmpty(s) && int.TryParse(s, out result))
            this.ResetSelect((UnitListFilterWindow.SelectType) result);
        }
        else
        {
          string str1 = "UNITLIST";
          if (!string.IsNullOrEmpty(str1))
          {
            if (PlayerPrefsUtility.HasKey(str1 + "&"))
            {
              string str2 = PlayerPrefsUtility.GetString(str1 + "&", string.Empty);
              if (!string.IsNullOrEmpty(str2))
              {
                string str3 = str2;
                char[] chArray = new char[1]{ '&' };
                foreach (string text in str3.Split(chArray))
                  this.ResetSelect(UnitListFilterWindow.ConvertFilterString(text));
              }
            }
            this.SaveSelectType();
          }
        }
      }
      this.m_SelectTypeReg = this.m_SelectType;
    }

    public void SaveSelectType()
    {
      string key = "UNITLIST_FILTER";
      if (string.IsNullOrEmpty(key))
        return;
      int num1 = 0;
      for (int index = 0; index < 32; ++index)
      {
        int num2 = 1 << index;
        if ((num2 & 8388575) != 0 && (this.m_SelectType & (UnitListFilterWindow.SelectType) num2) == UnitListFilterWindow.SelectType.NONE)
          num1 |= num2;
      }
      PlayerPrefsUtility.SetString(key, num1.ToString(), true);
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 600:
          this.CreateInstance();
          this.InitializeToggleContent();
          this.Open();
          break;
        case 610:
          this.ReleaseToggleContent();
          this.m_SelectType = this.m_SelectTypeReg;
          this.m_Result = UnitListFilterWindow.Result.CANCEL;
          this.Close(false);
          return 691;
        case 620:
          this.SaveSelectType();
          this.ReleaseToggleContent();
          this.m_SelectTypeReg = this.m_SelectType;
          this.m_Result = UnitListFilterWindow.Result.CONFIRM;
          this.Close(false);
          return 690;
        case 630:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue != null)
          {
            Toggle uiToggle = currentValue.GetUIToggle("_self");
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) uiToggle, (UnityEngine.Object) null))
            {
              int num = currentValue.GetInt("select", 0);
              if (num > 0)
              {
                if (uiToggle.get_isOn())
                {
                  this.SetSelect((UnitListFilterWindow.SelectType) num);
                  break;
                }
                this.ResetSelect((UnitListFilterWindow.SelectType) num);
                break;
              }
              break;
            }
            break;
          }
          break;
        case 640:
          this.m_SelectType = UnitListFilterWindow.SelectType.NONE;
          using (Dictionary<UnitListFilterWindow.SelectType, Toggle>.Enumerator enumerator = this.m_Toggles.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<UnitListFilterWindow.SelectType, Toggle> current = enumerator.Current;
              this.SetSelect(current.Key);
              current.Value.set_isOn(true);
            }
            break;
          }
        case 650:
          this.m_SelectType = UnitListFilterWindow.SelectType.NONE;
          using (Dictionary<UnitListFilterWindow.SelectType, Toggle>.Enumerator enumerator = this.m_Toggles.GetEnumerator())
          {
            while (enumerator.MoveNext())
              enumerator.Current.Value.set_isOn(false);
            break;
          }
      }
      return -1;
    }

    public static UnitListFilterWindow.SelectType GetFilterMask(UnitListWindow.Data data)
    {
      if (data.param == null)
        return UnitListFilterWindow.SelectType.RARITY_1 | UnitListFilterWindow.SelectType.RARITY_2 | UnitListFilterWindow.SelectType.RARITY_3 | UnitListFilterWindow.SelectType.RARITY_4 | UnitListFilterWindow.SelectType.RARITY_5 | UnitListFilterWindow.SelectType.WEAPON_SLASH | UnitListFilterWindow.SelectType.WEAPON_STAB | UnitListFilterWindow.SelectType.WEAPON_BLOW | UnitListFilterWindow.SelectType.WEAPON_SHOT | UnitListFilterWindow.SelectType.WEAPON_MAG | UnitListFilterWindow.SelectType.WEAPON_NONE | UnitListFilterWindow.SelectType.BIRTH_ENV | UnitListFilterWindow.SelectType.BIRTH_WRATH | UnitListFilterWindow.SelectType.BIRTH_SAGA | UnitListFilterWindow.SelectType.BIRTH_SLOTH | UnitListFilterWindow.SelectType.BIRTH_LUST | UnitListFilterWindow.SelectType.BIRTH_WADATSUMI | UnitListFilterWindow.SelectType.BIRTH_DESERT | UnitListFilterWindow.SelectType.BIRTH_GREED | UnitListFilterWindow.SelectType.BIRTH_GLUTTONY | UnitListFilterWindow.SelectType.BIRTH_OTHER | UnitListFilterWindow.SelectType.BIRTH_NOZ;
      MasterParam masterParam = MonoSingleton<GameManager>.Instance.MasterParam;
      UnitParam unitParam = data.param;
      UnitListFilterWindow.SelectType selectType = UnitListFilterWindow.SelectType.NONE;
      AttackDetailTypes attackDetailTypes = AttackDetailTypes.None;
      if (data.unit != null)
      {
        UnitData unit = data.unit;
        if (unit.CurrentJob.GetAttackSkill() != null)
          attackDetailTypes = unit.CurrentJob.GetAttackSkill().AttackDetailType;
        if (unit.Rarity == 0)
          selectType |= UnitListFilterWindow.SelectType.RARITY_1;
        else if (unit.Rarity == 1)
          selectType |= UnitListFilterWindow.SelectType.RARITY_2;
        else if (unit.Rarity == 2)
          selectType |= UnitListFilterWindow.SelectType.RARITY_3;
        else if (unit.Rarity == 3)
          selectType |= UnitListFilterWindow.SelectType.RARITY_4;
        else if (unit.Rarity == 4)
          selectType |= UnitListFilterWindow.SelectType.RARITY_5;
        else if (unit.Rarity == 5)
          selectType |= UnitListFilterWindow.SelectType.RARITY_6;
      }
      else
      {
        if (unitParam.jobsets != null && unitParam.jobsets.Length > 0)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(unitParam.jobsets[0]);
          if (jobSetParam != null)
          {
            JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(jobSetParam.job);
            if (jobParam != null && jobParam.atkskill != null && jobParam.atkskill.Length > 0)
            {
              string key = jobParam.atkskill[0];
              if (!string.IsNullOrEmpty(key))
              {
                SkillParam skillParam = masterParam.GetSkillParam(key);
                if (skillParam != null)
                  attackDetailTypes = skillParam.attack_detail;
              }
            }
          }
        }
        if (unitParam.rare == (byte) 0)
          selectType |= UnitListFilterWindow.SelectType.RARITY_1;
        else if (unitParam.rare == (byte) 1)
          selectType |= UnitListFilterWindow.SelectType.RARITY_2;
        else if (unitParam.rare == (byte) 2)
          selectType |= UnitListFilterWindow.SelectType.RARITY_3;
        else if (unitParam.rare == (byte) 3)
          selectType |= UnitListFilterWindow.SelectType.RARITY_4;
        else if (unitParam.rare == (byte) 4)
          selectType |= UnitListFilterWindow.SelectType.RARITY_5;
        else if (unitParam.rare == (byte) 5)
          selectType |= UnitListFilterWindow.SelectType.RARITY_6;
      }
      switch (attackDetailTypes)
      {
        case AttackDetailTypes.None:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_NONE;
          break;
        case AttackDetailTypes.Slash:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_SLASH;
          break;
        case AttackDetailTypes.Stab:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_STAB;
          break;
        case AttackDetailTypes.Blow:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_BLOW;
          break;
        case AttackDetailTypes.Shot:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_SHOT;
          break;
        case AttackDetailTypes.Magic:
          selectType |= UnitListFilterWindow.SelectType.WEAPON_MAG;
          break;
      }
      if (!string.IsNullOrEmpty((string) unitParam.birth))
      {
        string birth = (string) unitParam.birth;
        if (birth == "エンヴィリア")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_ENV;
        else if (birth == "ラーストリス")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_WRATH;
        else if (birth == "サガ地方")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_SAGA;
        else if (birth == "スロウスシュタイン")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_SLOTH;
        else if (birth == "ルストブルグ")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_LUST;
        else if (birth == "ワダツミ")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_WADATSUMI;
        else if (birth == "砂漠地帯")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_DESERT;
        else if (birth == "グリードダイク")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_GREED;
        else if (birth == "グラトニー＝フォス")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_GLUTTONY;
        else if (birth == "その他")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_OTHER;
        else if (birth == "ノーザンブライド")
          selectType |= UnitListFilterWindow.SelectType.BIRTH_NOZ;
      }
      return selectType;
    }

    public static UnitListFilterWindow.SelectType ConvertFilterString(string text)
    {
      if (text == "RARE:0")
        return UnitListFilterWindow.SelectType.RARITY_1;
      if (text == "RARE:1")
        return UnitListFilterWindow.SelectType.RARITY_2;
      if (text == "RARE:2")
        return UnitListFilterWindow.SelectType.RARITY_3;
      if (text == "RARE:3")
        return UnitListFilterWindow.SelectType.RARITY_4;
      if (text == "RARE:4")
        return UnitListFilterWindow.SelectType.RARITY_5;
      if (text == "WEAPON:Slash")
        return UnitListFilterWindow.SelectType.WEAPON_SLASH;
      if (text == "WEAPON:Stab")
        return UnitListFilterWindow.SelectType.WEAPON_STAB;
      if (text == "WEAPON:Blow")
        return UnitListFilterWindow.SelectType.WEAPON_BLOW;
      if (text == "WEAPON:Shot")
        return UnitListFilterWindow.SelectType.WEAPON_SHOT;
      if (text == "WEAPON:Magic")
        return UnitListFilterWindow.SelectType.WEAPON_MAG;
      if (text == "WEAPON:None")
        return UnitListFilterWindow.SelectType.WEAPON_NONE;
      if (text == "BIRTH:エンヴィリア")
        return UnitListFilterWindow.SelectType.BIRTH_ENV;
      if (text == "BIRTH:ラーストリス")
        return UnitListFilterWindow.SelectType.BIRTH_WRATH;
      if (text == "BIRTH:サガ地方")
        return UnitListFilterWindow.SelectType.BIRTH_SAGA;
      if (text == "BIRTH:スロウスシュタイン")
        return UnitListFilterWindow.SelectType.BIRTH_SLOTH;
      if (text == "BIRTH:ルストブルグ")
        return UnitListFilterWindow.SelectType.BIRTH_LUST;
      if (text == "BIRTH:ワダツミ")
        return UnitListFilterWindow.SelectType.BIRTH_WADATSUMI;
      if (text == "BIRTH:砂漠地帯")
        return UnitListFilterWindow.SelectType.BIRTH_DESERT;
      if (text == "BIRTH:グリードダイク")
        return UnitListFilterWindow.SelectType.BIRTH_GREED;
      if (text == "BIRTH:グラトニー＝フォス")
        return UnitListFilterWindow.SelectType.BIRTH_GLUTTONY;
      return text == "BIRTH:その他" ? UnitListFilterWindow.SelectType.BIRTH_OTHER : UnitListFilterWindow.SelectType.NONE;
    }

    public enum Result
    {
      NONE,
      CONFIRM,
      CANCEL,
    }

    public enum SelectType
    {
      NONE = 0,
      RARITY_1 = 1,
      RARITY_2 = 2,
      RARITY_3 = 4,
      RARITY_4 = 8,
      RARITY_5 = 16, // 0x00000010
      RARITY_6 = 32, // 0x00000020
      WEAPON_SLASH = 64, // 0x00000040
      WEAPON_STAB = 128, // 0x00000080
      WEAPON_BLOW = 256, // 0x00000100
      WEAPON_SHOT = 512, // 0x00000200
      WEAPON_MAG = 1024, // 0x00000400
      WEAPON_NONE = 2048, // 0x00000800
      BIRTH_ENV = 4096, // 0x00001000
      BIRTH_WRATH = 8192, // 0x00002000
      BIRTH_SAGA = 16384, // 0x00004000
      BIRTH_SLOTH = 32768, // 0x00008000
      BIRTH_LUST = 65536, // 0x00010000
      BIRTH_WADATSUMI = 131072, // 0x00020000
      BIRTH_DESERT = 262144, // 0x00040000
      BIRTH_GREED = 524288, // 0x00080000
      BIRTH_GLUTTONY = 1048576, // 0x00100000
      BIRTH_OTHER = 2097152, // 0x00200000
      BIRTH_NOZ = 4194304, // 0x00400000
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public override System.Type type
      {
        get
        {
          return typeof (UnitListFilterWindow);
        }
      }
    }
  }
}
