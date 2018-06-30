namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleInventory : MonoBehaviour
    {
        public SelectEvent OnSelectItem;
        public RectTransform ListParent;
        public ListItemEvents ItemTemplate;
        public ListItemEvents EmptySlotTemplate;
        public bool DisplayEmptySlots;
        private List<ListItemEvents> mItems;
        public ItemData[] mInventory;

        public BattleInventory()
        {
            this.DisplayEmptySlots = 1;
            this.mItems = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Refresh>m__295(GameObject go)
        {
            ItemData data;
            data = DataSource.FindDataOfClass<ItemData>(go, null);
            if (data == null)
            {
                goto Label_0030;
            }
            if (data.Param == null)
            {
                goto Label_0030;
            }
            if (this.OnSelectItem == null)
            {
                goto Label_0030;
            }
            this.OnSelectItem(data);
        Label_0030:
            return;
        }

        public void Refresh()
        {
            int num;
            ListItemEvents events;
            ListItemEvents events2;
            bool flag;
            Unit unit;
            Selectable[] selectableArray;
            int num2;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
            if ((this.ItemTemplate == null) != null)
            {
                goto Label_002D;
            }
            if ((this.ListParent == null) == null)
            {
                goto Label_002E;
            }
        Label_002D:
            return;
        Label_002E:
            this.mInventory = SceneBattle.Instance.Battle.mInventory;
            num = 0;
            goto Label_01DC;
        Label_004A:
            if (this.DisplayEmptySlots != null)
            {
                goto Label_005A;
            }
            goto Label_01D8;
        Label_005A:
            events = null;
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_008C;
            }
            if (SceneBattle.Instance.Battle.IsMultiPlay == null)
            {
                goto Label_008C;
            }
            events = this.EmptySlotTemplate;
            goto Label_00CF;
        Label_008C:
            if ((this.EmptySlotTemplate != null) == null)
            {
                goto Label_00C8;
            }
            if (this.mInventory[num] == null)
            {
                goto Label_00BC;
            }
            if (this.mInventory[num].Param != null)
            {
                goto Label_00C8;
            }
        Label_00BC:
            events = this.EmptySlotTemplate;
            goto Label_00CF;
        Label_00C8:
            events = this.ItemTemplate;
        Label_00CF:
            events2 = Object.Instantiate<ListItemEvents>(events);
            events2.get_gameObject().SetActive(1);
            events2.get_transform().SetParent(this.ListParent, 0);
            this.mItems.Add(events2);
            if ((events == this.EmptySlotTemplate) == null)
            {
                goto Label_0116;
            }
            goto Label_01D8;
        Label_0116:
            DataSource.Bind<ItemData>(events2.get_gameObject(), this.mInventory[num]);
            flag = 0;
            if (this.mInventory[num] == null)
            {
                goto Label_018B;
            }
            if (this.mInventory[num].Param == null)
            {
                goto Label_018B;
            }
            if (this.mInventory[num].Num <= 0)
            {
                goto Label_018B;
            }
            unit = SceneBattle.Instance.Battle.CurrentUnit;
            if (unit == null)
            {
                goto Label_018B;
            }
            flag = unit.CheckEnableUseSkill(this.mInventory[num].Skill, 0);
        Label_018B:
            selectableArray = events2.get_gameObject().GetComponentsInChildren<Selectable>(1);
            if (selectableArray == null)
            {
                goto Label_01C6;
            }
            num2 = ((int) selectableArray.Length) - 1;
            goto Label_01BE;
        Label_01AD:
            selectableArray[num2].set_interactable(flag);
            num2 -= 1;
        Label_01BE:
            if (num2 >= 0)
            {
                goto Label_01AD;
            }
        Label_01C6:
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.<Refresh>m__295);
        Label_01D8:
            num += 1;
        Label_01DC:
            if (num < ((int) this.mInventory.Length))
            {
                goto Label_004A;
            }
            return;
        }

        private void Start()
        {
            if ((this.ItemTemplate != null) == null)
            {
                goto Label_004E;
            }
            this.ItemTemplate.get_gameObject().SetActive(0);
            if ((this.ListParent == null) == null)
            {
                goto Label_004E;
            }
            this.ListParent = this.ItemTemplate.get_transform().get_parent() as RectTransform;
        Label_004E:
            if ((this.EmptySlotTemplate != null) == null)
            {
                goto Label_0070;
            }
            this.EmptySlotTemplate.get_gameObject().SetActive(0);
        Label_0070:
            this.Refresh();
            return;
        }

        public delegate void SelectEvent(ItemData item);
    }
}

