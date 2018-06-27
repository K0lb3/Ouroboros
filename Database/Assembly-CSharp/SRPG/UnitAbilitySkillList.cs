// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAbilitySkillList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitAbilitySkillList : MonoBehaviour
  {
    public ListItemEvents ItemTemplate;
    public ScrollRect ScrollViewRect;
    public UnitAbilitySkillList.SelectSkillEvent OnSelectSkill;
    private List<ListItemEvents> mItems;
    private Unit mUnit;
    private bool isCommandTutorial;

    public UnitAbilitySkillList()
    {
      base.\u002Ector();
    }

    public void Start()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      ((Component) this.ItemTemplate).get_gameObject().SetActive(false);
    }

    public void Refresh(Unit self)
    {
      this.mUnit = self;
      this.Refresh();
    }

    public void Refresh()
    {
      this.DestroyItems();
      if (Object.op_Equality((Object) this.ItemTemplate, (Object) null))
      {
        Debug.LogError((object) "ItemTemplate が未設定です。");
      }
      else
      {
        AbilityData dataOfClass = DataSource.FindDataOfClass<AbilityData>(((Component) this).get_gameObject(), (AbilityData) null);
        if (dataOfClass == null)
        {
          Debug.LogWarning((object) "AbilityData を参照できません。");
        }
        else
        {
          this.isCommandTutorial = false;
          GameManager instance = MonoSingleton<GameManager>.Instance;
          if ((instance.Player.TutorialFlags & 1L) == 0L && (instance.GetNextTutorialStep() == "ShowAbilityCommand" || instance.GetNextTutorialStep() == "ShowMACommand"))
          {
            instance.CompleteTutorialStep();
            this.isCommandTutorial = true;
          }
          this.ScrollViewRect.set_normalizedPosition(new Vector2(0.5f, 1f));
          GameParameter.UpdateAll(((Component) this).get_gameObject());
          Transform parent = ((Component) this.ItemTemplate).get_transform().get_parent();
          for (int index = 0; index < dataOfClass.Skills.Count; ++index)
          {
            ListItemEvents listItemEvents = (ListItemEvents) Object.Instantiate<ListItemEvents>((M0) this.ItemTemplate);
            ((Component) listItemEvents).get_transform().SetParent(parent, false);
            this.mItems.Add(listItemEvents);
            SkillData skill = dataOfClass.Skills[index];
            DataSource.Bind<SkillData>(((Component) listItemEvents).get_gameObject(), skill);
            DataSource.Bind<Unit>(((Component) listItemEvents).get_gameObject(), this.mUnit);
            ((Component) listItemEvents).get_gameObject().SetActive(true);
            listItemEvents.OnSelect = (ListItemEvents.ListItemEvent) (go => this.SelectSkill(DataSource.FindDataOfClass<SkillData>(go, (SkillData) null)));
            Selectable selectable = (Selectable) ((Component) listItemEvents).GetComponentInChildren<Selectable>();
            if (Object.op_Equality((Object) selectable, (Object) null))
              selectable = (Selectable) ((Component) listItemEvents).GetComponent<Selectable>();
            if (Object.op_Inequality((Object) selectable, (Object) null))
            {
              selectable.set_interactable(this.mUnit.CheckEnableUseSkill(skill, false));
              if (selectable.get_interactable())
                selectable.set_interactable(this.mUnit.IsUseSkillCollabo(skill, true));
              ((Behaviour) selectable).set_enabled(!((Behaviour) selectable).get_enabled());
              ((Behaviour) selectable).set_enabled(!((Behaviour) selectable).get_enabled());
            }
            UnitAbilitySkillListItem component = (UnitAbilitySkillListItem) ((Component) listItemEvents).get_gameObject().GetComponent<UnitAbilitySkillListItem>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              bool noLimit = !this.mUnit.CheckEnableSkillUseCount(skill);
              component.SetSkillCount((int) this.mUnit.GetSkillUseCount(skill), (int) this.mUnit.GetSkillUseCountMax(skill), noLimit);
              component.SetCastSpeed(skill.CastSpeed);
            }
            if (this.isCommandTutorial)
            {
              if (skill.SkillID == "SK_SEI_SWORD_CRASH")
              {
                SGHighlightObject.Instance().highlightedObject = ((Component) listItemEvents).get_gameObject();
                SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_0.005", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.TopLeft, false, false, false);
              }
              else if (skill.SkillID == "SK_SEI_SHINING_CROSS_TUTORIAL")
              {
                SGHighlightObject.Instance().highlightedObject = ((Component) listItemEvents).get_gameObject();
                SGHighlightObject.Instance().Highlight(string.Empty, "sg_tut_0.009", (SGHighlightObject.OnActivateCallback) null, EventDialogBubble.Anchors.BottomLeft, false, false, false);
              }
            }
          }
        }
      }
    }

    private void SelectSkill(SkillData skill)
    {
      if (skill != null)
        this.OnSelectSkill(skill);
      if (!this.isCommandTutorial)
        return;
      MonoSingleton<GameManager>.Instance.CompleteTutorialStep();
    }

    private void DestroyItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
        Object.Destroy((Object) ((Component) this.mItems[index]).get_gameObject());
      this.mItems.Clear();
    }

    public delegate void SelectSkillEvent(SkillData skill);
  }
}
