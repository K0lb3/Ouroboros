namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class BattleUnitChg : MonoBehaviour
    {
        private const int SUB_UNIT_MAX = 2;
        public SelectEvent OnSelectUnit;
        public RectTransform ListParent;
        public ListItemEvents UnitTemplate;
        public ListItemEvents EmptyTemplate;
        private List<Unit> mSubUnitLists;
        private List<ListItemEvents> mUnitEvents;

        public BattleUnitChg()
        {
            this.mSubUnitLists = new List<Unit>(2);
            this.mUnitEvents = new List<ListItemEvents>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Refresh>m__73(GameObject go)
        {
            Unit unit;
            if (this.OnSelectUnit == null)
            {
                goto Label_0025;
            }
            unit = DataSource.FindDataOfClass<Unit>(go, null);
            if (unit == null)
            {
                goto Label_0025;
            }
            this.OnSelectUnit(unit);
        Label_0025:
            return;
        }

        public unsafe void Refresh()
        {
            SceneBattle battle;
            BattleCore core;
            Unit unit;
            List<Unit>.Enumerator enumerator;
            int num;
            ListItemEvents events;
            Unit unit2;
            ListItemEvents events2;
            bool flag;
            Selectable[] selectableArray;
            int num2;
            GameUtility.DestroyGameObjects<ListItemEvents>(this.mUnitEvents);
            this.mUnitEvents.Clear();
            if ((((this.UnitTemplate == null) == null) && ((this.EmptyTemplate == null) == null)) && ((this.ListParent == null) == null))
            {
                goto Label_004A;
            }
            return;
        Label_004A:
            battle = SceneBattle.Instance;
            core = null;
            if (battle == null)
            {
                goto Label_0064;
            }
            core = battle.Battle;
        Label_0064:
            if (core != null)
            {
                goto Label_006B;
            }
            return;
        Label_006B:
            this.mSubUnitLists.Clear();
            enumerator = core.Player.GetEnumerator();
        Label_0082:
            try
            {
                goto Label_00B1;
            Label_0087:
                unit = &enumerator.Current;
                if (core.StartingMembers.Contains(unit) == null)
                {
                    goto Label_00A5;
                }
                goto Label_00B1;
            Label_00A5:
                this.mSubUnitLists.Add(unit);
            Label_00B1:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0087;
                }
                goto Label_00CE;
            }
            finally
            {
            Label_00C2:
                ((List<Unit>.Enumerator) enumerator).Dispose();
            }
        Label_00CE:
            num = 0;
            goto Label_0204;
        Label_00D6:
            if (num < this.mSubUnitLists.Count)
            {
                goto Label_0127;
            }
            events = Object.Instantiate<ListItemEvents>(this.EmptyTemplate);
            events.get_transform().SetParent(this.ListParent, 0);
            this.mUnitEvents.Add(events);
            events.get_gameObject().SetActive(1);
            goto Label_01FE;
        Label_0127:
            unit2 = this.mSubUnitLists[num];
            if (unit2 != null)
            {
                goto Label_0142;
            }
            goto Label_01FE;
        Label_0142:
            events2 = Object.Instantiate<ListItemEvents>(this.UnitTemplate);
            DataSource.Bind<Unit>(events2.get_gameObject(), unit2);
            events2.get_transform().SetParent(this.ListParent, 0);
            this.mUnitEvents.Add(events2);
            events2.get_gameObject().SetActive(1);
            flag = ((unit2.IsDead != null) || (unit2.IsEntry == null)) ? 0 : unit2.IsSub;
            selectableArray = events2.get_gameObject().GetComponentsInChildren<Selectable>(1);
            if (selectableArray == null)
            {
                goto Label_01EB;
            }
            num2 = ((int) selectableArray.Length) - 1;
            goto Label_01E3;
        Label_01D1:
            selectableArray[num2].set_interactable(flag);
            num2 -= 1;
        Label_01E3:
            if (num2 >= 0)
            {
                goto Label_01D1;
            }
        Label_01EB:
            events2.OnSelect = new ListItemEvents.ListItemEvent(this.<Refresh>m__73);
        Label_01FE:
            num += 1;
        Label_0204:
            if (num < 2)
            {
                goto Label_00D6;
            }
            return;
        }

        private void Start()
        {
            if ((this.UnitTemplate != null) == null)
            {
                goto Label_004E;
            }
            this.UnitTemplate.get_gameObject().SetActive(0);
            if ((this.ListParent == null) == null)
            {
                goto Label_004E;
            }
            this.ListParent = this.UnitTemplate.get_transform().get_parent() as RectTransform;
        Label_004E:
            if ((this.EmptyTemplate != null) == null)
            {
                goto Label_0070;
            }
            this.EmptyTemplate.get_gameObject().SetActive(0);
        Label_0070:
            this.Refresh();
            return;
        }

        public delegate void SelectEvent(Unit unit);
    }
}

