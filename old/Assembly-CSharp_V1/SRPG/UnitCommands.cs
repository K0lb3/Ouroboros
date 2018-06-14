// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCommands
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitCommands : MonoBehaviour
  {
    public UnitCommands.CommandEvent OnCommandSelect;
    public UnitCommands.YesNoEvent OnYesNoSelect;
    public UnitCommands.MapExitEvent OnMapExitSelect;
    public GameObject MoveButton;
    public GameObject AttackButton;
    public GameObject RenkeiButton;
    public GameObject ItemButton;
    public GameObject MapButton;
    public GameObject ExitMapButton;
    public GameObject EndButton;
    public GameObject AbilityButton;
    public GameObject GridEventButton;
    public GameObject OKButton;
    public GameObject CancelButton;
    public Donuts DonutsBG;
    public float DonutsAnglePerItem;
    public float DonutsAngleStart;
    public string OtherSkillName;
    public string OtherSkillIconName;
    public string AbilityImageBG;
    public string AbilityImageIcon;
    public string AbilityName;
    public Color AbilityDisableColor;
    [HideInInspector]
    private List<GameObject> mAbilityButtons;

    public UnitCommands()
    {
      base.\u002Ector();
    }

    public UnitCommands.ButtonTypes VisibleButtons
    {
      set
      {
        bool flag1 = (value & UnitCommands.ButtonTypes.Action) != (UnitCommands.ButtonTypes) 0;
        bool flag2 = (value & UnitCommands.ButtonTypes.GridEvent) != (UnitCommands.ButtonTypes) 0;
        bool flag3 = (value & UnitCommands.ButtonTypes.Misc) != (UnitCommands.ButtonTypes) 0;
        bool flag4 = (value & UnitCommands.ButtonTypes.IsRenkei) != (UnitCommands.ButtonTypes) 0;
        bool flag5 = (value & UnitCommands.ButtonTypes.Map) != (UnitCommands.ButtonTypes) 0;
        int num = 0;
        if (Object.op_Inequality((Object) this.AttackButton, (Object) null))
        {
          this.AttackButton.SetActive(flag1 && (value & UnitCommands.ButtonTypes.Attack) != (UnitCommands.ButtonTypes) 0 && (!flag4 || Object.op_Equality((Object) this.RenkeiButton, (Object) null)));
          if (this.AttackButton.get_activeSelf())
            ++num;
        }
        if (Object.op_Inequality((Object) this.RenkeiButton, (Object) null))
        {
          this.RenkeiButton.SetActive(flag1 && (value & UnitCommands.ButtonTypes.Attack) != (UnitCommands.ButtonTypes) 0 && (flag4 && Object.op_Inequality((Object) this.RenkeiButton, (Object) null)));
          if (this.RenkeiButton.get_activeSelf())
            ++num;
        }
        if (Object.op_Inequality((Object) this.ItemButton, (Object) null))
        {
          this.ItemButton.SetActive(flag1 && (value & UnitCommands.ButtonTypes.Item) != (UnitCommands.ButtonTypes) 0);
          if (this.ItemButton.get_activeSelf())
            ++num;
        }
        for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        {
          this.mAbilityButtons[index].SetActive(flag1 && (value & UnitCommands.ButtonTypes.Skill) != (UnitCommands.ButtonTypes) 0);
          if (this.mAbilityButtons[index].get_activeSelf())
            ++num;
        }
        if (Object.op_Inequality((Object) this.GridEventButton, (Object) null))
        {
          this.GridEventButton.SetActive(flag2);
          if (this.GridEventButton.get_activeSelf())
            ++num;
        }
        if (Object.op_Inequality((Object) this.EndButton, (Object) null))
        {
          this.EndButton.SetActive(flag3);
          if (this.EndButton.get_activeSelf())
            ++num;
        }
        if (Object.op_Inequality((Object) this.MapButton, (Object) null))
          this.MapButton.SetActive(flag5);
        if (!Object.op_Inequality((Object) this.DonutsBG, (Object) null))
          return;
        if (num >= 2)
          this.DonutsBG.SetRange(this.DonutsAngleStart, this.DonutsAngleStart + (float) (num - 1) * this.DonutsAnglePerItem);
        else
          this.DonutsBG.SetRange(0.0f, 0.0f);
      }
    }

    private void OnDestroy()
    {
    }

    private void SetButtonEvent(GameObject go, UnitCommands.ClickEvent callback)
    {
      Button component = (Button) go.GetComponent<Button>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) callback, __methodptr(Invoke)));
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.MoveButton, (Object) null))
        this.SetButtonEvent(this.MoveButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Move, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.AttackButton, (Object) null))
        this.SetButtonEvent(this.AttackButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null || MonoSingleton<GameManager>.Instance.IsTutorial() && !Object.op_Equality((Object) SGHighlightObject.Instance(), (Object) null))
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Attack, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.RenkeiButton, (Object) null))
        this.SetButtonEvent(this.RenkeiButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Attack, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.ItemButton, (Object) null))
        this.SetButtonEvent(this.ItemButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Item, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.MapButton, (Object) null))
        this.SetButtonEvent(this.MapButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Map, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.EndButton, (Object) null))
        this.SetButtonEvent(this.EndButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.End, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.AbilityButton, (Object) null))
        this.AbilityButton.SetActive(false);
      if (Object.op_Inequality((Object) this.GridEventButton, (Object) null))
        this.SetButtonEvent(this.GridEventButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnCommandSelect == null)
            return;
          this.OnCommandSelect(UnitCommands.CommandTypes.Gimmick, (object) 0);
        }));
      if (Object.op_Inequality((Object) this.OKButton, (Object) null))
        this.SetButtonEvent(this.OKButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnYesNoSelect == null)
            return;
          this.OnYesNoSelect(true);
        }));
      if (Object.op_Inequality((Object) this.CancelButton, (Object) null))
        this.SetButtonEvent(this.CancelButton, (UnitCommands.ClickEvent) (() =>
        {
          if (this.OnYesNoSelect == null)
            return;
          this.OnYesNoSelect(false);
        }));
      if (!Object.op_Inequality((Object) this.ExitMapButton, (Object) null))
        return;
      this.SetButtonEvent(this.ExitMapButton, (UnitCommands.ClickEvent) (() =>
      {
        if (this.OnMapExitSelect == null)
          return;
        this.OnMapExitSelect();
      }));
    }

    public void SetAbilities(AbilityData[] abilities, Unit unit)
    {
      for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        Object.Destroy((Object) this.mAbilityButtons[index]);
      this.mAbilityButtons.Clear();
      Transform parent = this.AbilityButton.get_transform().get_parent();
      for (int index1 = 0; index1 < abilities.Length; ++index1)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        UnitCommands.\u003CSetAbilities\u003Ec__AnonStorey280 abilitiesCAnonStorey280 = new UnitCommands.\u003CSetAbilities\u003Ec__AnonStorey280();
        // ISSUE: reference to a compiler-generated field
        abilitiesCAnonStorey280.\u003C\u003Ef__this = this;
        if (abilities[index1].AbilityType != EAbilityType.Passive)
        {
          bool flag = false;
          int mp = (int) unit.CurrentStatus.param.mp;
          // ISSUE: reference to a compiler-generated field
          abilitiesCAnonStorey280.ability = abilities[index1];
          // ISSUE: reference to a compiler-generated field
          List<SkillData> skills = abilitiesCAnonStorey280.ability.Skills;
          GameObject go = (GameObject) Object.Instantiate<GameObject>((M0) this.AbilityButton);
          go.get_transform().SetParent(parent, false);
          // ISSUE: reference to a compiler-generated field
          DataSource.Bind<AbilityData>(go, abilitiesCAnonStorey280.ability);
          go.SetActive(true);
          for (int index2 = 0; index2 < skills.Count; ++index2)
          {
            SkillData skill = skills[index2];
            int skillUseCount = (int) unit.GetSkillUseCount(skill);
            if (unit.GetSkillUsedCost(skill) <= mp && skillUseCount > 0)
            {
              flag = true;
              break;
            }
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityImageBG))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.get_transform(), this.AbilityImageBG);
            if (Object.op_Inequality((Object) childRecursively, (Object) null))
              ((Graphic) ((Component) childRecursively).GetComponent<Image>()).set_color(this.AbilityDisableColor);
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityImageIcon))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.get_transform(), this.AbilityImageIcon);
            if (Object.op_Inequality((Object) childRecursively, (Object) null))
              ((Graphic) ((Component) childRecursively).GetComponent<RawImage_Transparent>()).set_color(this.AbilityDisableColor);
          }
          if (!flag && !string.IsNullOrEmpty(this.AbilityName))
          {
            Transform childRecursively = GameUtility.findChildRecursively(go.get_transform(), this.AbilityName);
            if (Object.op_Inequality((Object) childRecursively, (Object) null))
              ((Graphic) ((Component) childRecursively).GetComponent<LText>()).set_color(this.AbilityDisableColor);
          }
          // ISSUE: reference to a compiler-generated method
          this.SetButtonEvent(go, new UnitCommands.ClickEvent(abilitiesCAnonStorey280.\u003C\u003Em__312));
          this.mAbilityButtons.Add(go);
        }
      }
      this.SortButtons();
    }

    private void OnSelectAbility(AbilityData ability)
    {
      if (this.OnCommandSelect == null || MonoSingleton<GameManager>.Instance.IsTutorial() && Object.op_Inequality((Object) SGHighlightObject.Instance(), (Object) null) && ability.AbilityID != "AB_SEI_LOWER")
        return;
      this.OnCommandSelect(UnitCommands.CommandTypes.Ability, (object) ability);
    }

    private void SortButtons()
    {
      int num = 0;
      if (Object.op_Inequality((Object) this.AbilityButton, (Object) null))
        num = this.AbilityButton.get_transform().GetSiblingIndex();
      for (int index = 0; index < this.mAbilityButtons.Count; ++index)
        this.mAbilityButtons[index].get_transform().SetSiblingIndex(num + index + 1);
    }

    public enum CommandTypes
    {
      None,
      Move,
      Attack,
      Ability,
      Item,
      Gimmick,
      Map,
      End,
    }

    [Flags]
    public enum ButtonTypes
    {
      Move = 1,
      Action = 2,
      GridEvent = 4,
      Misc = 8,
      Attack = 16, // 0x00000010
      Skill = 32, // 0x00000020
      Item = 64, // 0x00000040
      IsRenkei = 128, // 0x00000080
      Map = 256, // 0x00000100
    }

    private delegate void ClickEvent();

    public delegate void CommandEvent(UnitCommands.CommandTypes command, object data);

    public delegate void YesNoEvent(bool yes);

    public delegate void MapExitEvent();
  }
}
