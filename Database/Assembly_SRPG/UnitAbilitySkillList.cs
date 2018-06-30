namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitAbilitySkillList : MonoBehaviour
    {
        public ListItemEvents ItemTemplate;
        public ScrollRect ScrollViewRect;
        public SelectSkillEvent OnSelectSkill;
        private List<ListItemEvents> mItems;
        private Unit mUnit;

        public UnitAbilitySkillList()
        {
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Refresh>m__438(GameObject go)
        {
            SkillData data;
            data = DataSource.FindDataOfClass<SkillData>(go, null);
            this.SelectSkill(data);
            return;
        }

        private void DestroyItems()
        {
            int num;
            num = 0;
            goto Label_0021;
        Label_0007:
            Object.Destroy(this.mItems[num].get_gameObject());
            num += 1;
        Label_0021:
            if (num < this.mItems.Count)
            {
                goto Label_0007;
            }
            this.mItems.Clear();
            return;
        }

        public void Refresh()
        {
            AbilityData data;
            Transform transform;
            int num;
            ListItemEvents events;
            SkillData data2;
            Selectable selectable;
            UnitAbilitySkillListItem item;
            bool flag;
            this.DestroyItems();
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0022;
            }
            Debug.LogError("ItemTemplate が未設定です。");
            return;
        Label_0022:
            data = DataSource.FindDataOfClass<AbilityData>(base.get_gameObject(), null);
            if (data != null)
            {
                goto Label_0040;
            }
            Debug.LogWarning("AbilityData を参照できません。");
            return;
        Label_0040:
            this.ScrollViewRect.set_normalizedPosition(new Vector2(0.5f, 1f));
            GameParameter.UpdateAll(base.get_gameObject());
            transform = this.ItemTemplate.get_transform().get_parent();
            num = 0;
            goto Label_01D9;
        Label_007D:
            events = Object.Instantiate<ListItemEvents>(this.ItemTemplate);
            events.get_transform().SetParent(transform, 0);
            this.mItems.Add(events);
            data2 = data.Skills[num];
            DataSource.Bind<SkillData>(events.get_gameObject(), data2);
            DataSource.Bind<Unit>(events.get_gameObject(), this.mUnit);
            events.get_gameObject().SetActive(1);
            events.OnSelect = new ListItemEvents.ListItemEvent(this.<Refresh>m__438);
            selectable = events.GetComponentInChildren<Selectable>();
            if ((selectable == null) == null)
            {
                goto Label_0109;
            }
            selectable = events.GetComponent<Selectable>();
        Label_0109:
            if ((selectable != null) == null)
            {
                goto Label_016E;
            }
            selectable.set_interactable(this.mUnit.CheckEnableUseSkill(data2, 0));
            if (selectable.get_interactable() == null)
            {
                goto Label_014C;
            }
            selectable.set_interactable(this.mUnit.IsUseSkillCollabo(data2, 1));
        Label_014C:
            selectable.set_enabled(selectable.get_enabled() == 0);
            selectable.set_enabled(selectable.get_enabled() == 0);
        Label_016E:
            item = events.get_gameObject().GetComponent<UnitAbilitySkillListItem>();
            if ((item != null) == null)
            {
                goto Label_01D5;
            }
            flag = this.mUnit.CheckEnableSkillUseCount(data2) == 0;
            item.SetSkillCount(this.mUnit.GetSkillUseCount(data2), this.mUnit.GetSkillUseCountMax(data2), flag);
            item.SetCastSpeed(data2.CastSpeed);
        Label_01D5:
            num += 1;
        Label_01D9:
            if (num < data.Skills.Count)
            {
                goto Label_007D;
            }
            return;
        }

        public void Refresh(Unit self)
        {
            this.mUnit = self;
            this.Refresh();
            return;
        }

        private void SelectSkill(SkillData skill)
        {
            if (skill == null)
            {
                goto Label_0012;
            }
            this.OnSelectSkill(skill);
        Label_0012:
            return;
        }

        public void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_0022;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
        Label_0022:
            return;
        }

        public delegate void SelectSkillEvent(SkillData skill);
    }
}

