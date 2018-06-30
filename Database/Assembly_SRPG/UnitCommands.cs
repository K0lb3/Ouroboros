namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class UnitCommands : MonoBehaviour
    {
        public CommandEvent OnCommandSelect;
        public YesNoEvent OnYesNoSelect;
        public MapExitEvent OnMapExitSelect;
        public UnitChgEvent OnUnitChgSelect;
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
        public GameObject UnitChgButton;
        public Donuts DonutsBG;
        public float DonutsAnglePerItem;
        public float DonutsAngleStart;
        public string OtherSkillName;
        public string OtherSkillIconName;
        public string AbilityImageBG;
        public string AbilityImageIcon;
        public Color AbilityDisableColor;
        [HideInInspector]
        private List<GameObject> mAbilityButtons;
        private bool mIsEnableUnitChgButton;
        private bool mIsActiveUnitChgButton;

        public UnitCommands()
        {
            this.mAbilityButtons = new List<GameObject>();
            base..ctor();
            return;
        }

        [CompilerGenerated]
        private void <Start>m__43D()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(1, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__43E()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(2, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__43F()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(2, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__440()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(4, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__441()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(6, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__442()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(7, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__443()
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_001D;
            }
            this.OnCommandSelect(5, (int) 0);
        Label_001D:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__444()
        {
            if (this.OnYesNoSelect == null)
            {
                goto Label_0017;
            }
            this.OnYesNoSelect(1);
        Label_0017:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__445()
        {
            if (this.OnYesNoSelect == null)
            {
                goto Label_0017;
            }
            this.OnYesNoSelect(0);
        Label_0017:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__446()
        {
            if (this.OnMapExitSelect == null)
            {
                goto Label_0016;
            }
            this.OnMapExitSelect();
        Label_0016:
            return;
        }

        [CompilerGenerated]
        private void <Start>m__447()
        {
            if (this.OnUnitChgSelect == null)
            {
                goto Label_0016;
            }
            this.OnUnitChgSelect();
        Label_0016:
            return;
        }

        public void EnableUnitChgButton(bool is_enable, bool is_active)
        {
            Selectable[] selectableArray;
            int num;
            this.mIsEnableUnitChgButton = is_enable;
            this.mIsActiveUnitChgButton = is_active;
            if ((this.UnitChgButton != null) == null)
            {
                goto Label_0063;
            }
            this.UnitChgButton.SetActive(is_enable);
            if (is_enable == null)
            {
                goto Label_0063;
            }
            selectableArray = this.UnitChgButton.GetComponentsInChildren<Selectable>(1);
            if (selectableArray == null)
            {
                goto Label_0063;
            }
            num = ((int) selectableArray.Length) - 1;
            goto Label_005C;
        Label_004F:
            selectableArray[num].set_interactable(is_active);
            num -= 1;
        Label_005C:
            if (num >= 0)
            {
                goto Label_004F;
            }
        Label_0063:
            return;
        }

        private void OnDestroy()
        {
        }

        private void OnSelectAbility(AbilityData ability)
        {
            if (this.OnCommandSelect == null)
            {
                goto Label_0018;
            }
            this.OnCommandSelect(3, ability);
        Label_0018:
            return;
        }

        public void SetAbilities(AbilityData[] abilities, Unit unit)
        {
            int num;
            Transform transform;
            int num2;
            bool flag;
            int num3;
            List<SkillData> list;
            GameObject obj2;
            int num4;
            SkillData data;
            int num5;
            int num6;
            bool flag2;
            Transform transform2;
            Image image;
            Transform transform3;
            RawImage_Transparent transparent;
            <SetAbilities>c__AnonStorey3BB storeybb;
            num = 0;
            goto Label_001C;
        Label_0007:
            Object.Destroy(this.mAbilityButtons[num]);
            num += 1;
        Label_001C:
            if (num < this.mAbilityButtons.Count)
            {
                goto Label_0007;
            }
            this.mAbilityButtons.Clear();
            transform = this.AbilityButton.get_transform().get_parent();
            num2 = 0;
            goto Label_0210;
        Label_0050:
            storeybb = new <SetAbilities>c__AnonStorey3BB();
            storeybb.<>f__this = this;
            if (abilities[num2].AbilityType != 1)
            {
                goto Label_0072;
            }
            goto Label_020C;
        Label_0072:
            flag = 0;
            num3 = unit.CurrentStatus.param.mp;
            storeybb.ability = abilities[num2];
            list = storeybb.ability.Skills;
            obj2 = Object.Instantiate<GameObject>(this.AbilityButton);
            obj2.get_transform().SetParent(transform, 0);
            DataSource.Bind<AbilityData>(obj2, storeybb.ability);
            obj2.SetActive(1);
            num4 = 0;
            goto Label_0142;
        Label_00DC:
            data = list[num4];
            num5 = unit.GetSkillUseCount(data);
            num6 = unit.GetSkillUsedCost(data);
            if (data.IsPassiveSkill() == null)
            {
                goto Label_0111;
            }
            goto Label_013C;
        Label_0111:
            flag2 = (num5 > 0) ? 1 : data.IsSkillCountNoLimit;
            if (num6 > num3)
            {
                goto Label_013C;
            }
            if (flag2 == null)
            {
                goto Label_013C;
            }
            flag = 1;
            goto Label_0150;
        Label_013C:
            num4 += 1;
        Label_0142:
            if (num4 < list.Count)
            {
                goto Label_00DC;
            }
        Label_0150:
            if (flag != null)
            {
                goto Label_019D;
            }
            if (string.IsNullOrEmpty(this.AbilityImageBG) != null)
            {
                goto Label_019D;
            }
            transform2 = GameUtility.findChildRecursively(obj2.get_transform(), this.AbilityImageBG);
            if ((transform2 != null) == null)
            {
                goto Label_019D;
            }
            transform2.GetComponent<Image>().set_color(this.AbilityDisableColor);
        Label_019D:
            if (flag != null)
            {
                goto Label_01EA;
            }
            if (string.IsNullOrEmpty(this.AbilityImageIcon) != null)
            {
                goto Label_01EA;
            }
            transform3 = GameUtility.findChildRecursively(obj2.get_transform(), this.AbilityImageIcon);
            if ((transform3 != null) == null)
            {
                goto Label_01EA;
            }
            transform3.GetComponent<RawImage_Transparent>().set_color(this.AbilityDisableColor);
        Label_01EA:
            this.SetButtonEvent(obj2, new ClickEvent(storeybb.<>m__448));
            this.mAbilityButtons.Add(obj2);
        Label_020C:
            num2 += 1;
        Label_0210:
            if (num2 < ((int) abilities.Length))
            {
                goto Label_0050;
            }
            this.SortButtons();
            return;
        }

        private void SetButtonEvent(GameObject go, ClickEvent callback)
        {
            Button button;
            button = go.GetComponentInChildren<Button>();
            if ((button != null) == null)
            {
                goto Label_002A;
            }
            button.get_onClick().AddListener(new UnityAction(callback, this.Invoke));
        Label_002A:
            return;
        }

        private void SortButtons()
        {
            int num;
            int num2;
            num = 0;
            if ((this.AbilityButton != null) == null)
            {
                goto Label_0024;
            }
            num = this.AbilityButton.get_transform().GetSiblingIndex();
        Label_0024:
            num2 = 0;
            goto Label_004A;
        Label_002B:
            this.mAbilityButtons[num2].get_transform().SetSiblingIndex((num + num2) + 1);
            num2 += 1;
        Label_004A:
            if (num2 < this.mAbilityButtons.Count)
            {
                goto Label_002B;
            }
            return;
        }

        private void Start()
        {
            if ((this.MoveButton != null) == null)
            {
                goto Label_002E;
            }
            this.SetButtonEvent(this.MoveButton, new ClickEvent(this.<Start>m__43D));
        Label_002E:
            if ((this.AttackButton != null) == null)
            {
                goto Label_005C;
            }
            this.SetButtonEvent(this.AttackButton, new ClickEvent(this.<Start>m__43E));
        Label_005C:
            if ((this.RenkeiButton != null) == null)
            {
                goto Label_0085;
            }
            this.SetButtonEvent(this.RenkeiButton, new ClickEvent(this.<Start>m__43F));
        Label_0085:
            if ((this.ItemButton != null) == null)
            {
                goto Label_00B3;
            }
            this.SetButtonEvent(this.ItemButton, new ClickEvent(this.<Start>m__440));
        Label_00B3:
            if ((this.MapButton != null) == null)
            {
                goto Label_00E1;
            }
            this.SetButtonEvent(this.MapButton, new ClickEvent(this.<Start>m__441));
        Label_00E1:
            if ((this.EndButton != null) == null)
            {
                goto Label_010F;
            }
            this.SetButtonEvent(this.EndButton, new ClickEvent(this.<Start>m__442));
        Label_010F:
            if ((this.AbilityButton != null) == null)
            {
                goto Label_0131;
            }
            this.AbilityButton.SetActive(0);
        Label_0131:
            if ((this.GridEventButton != null) == null)
            {
                goto Label_015F;
            }
            this.SetButtonEvent(this.GridEventButton, new ClickEvent(this.<Start>m__443));
        Label_015F:
            if ((this.OKButton != null) == null)
            {
                goto Label_0188;
            }
            this.SetButtonEvent(this.OKButton, new ClickEvent(this.<Start>m__444));
        Label_0188:
            if ((this.CancelButton != null) == null)
            {
                goto Label_01B1;
            }
            this.SetButtonEvent(this.CancelButton, new ClickEvent(this.<Start>m__445));
        Label_01B1:
            if ((this.ExitMapButton != null) == null)
            {
                goto Label_01DA;
            }
            this.SetButtonEvent(this.ExitMapButton, new ClickEvent(this.<Start>m__446));
        Label_01DA:
            if ((this.UnitChgButton != null) == null)
            {
                goto Label_0203;
            }
            this.SetButtonEvent(this.UnitChgButton, new ClickEvent(this.<Start>m__447));
        Label_0203:
            this.EnableUnitChgButton(0, 0);
            return;
        }

        public List<GameObject> AbilityButtons
        {
            get
            {
                return this.mAbilityButtons;
            }
        }

        public ButtonTypes VisibleButtons
        {
            set
            {
                bool flag;
                bool flag2;
                bool flag3;
                bool flag4;
                bool flag5;
                int num;
                int num2;
                flag = ((value & 2) == 0) == 0;
                flag2 = ((value & 4) == 0) == 0;
                flag3 = ((value & 8) == 0) == 0;
                flag4 = ((value & 0x80) == 0) == 0;
                flag5 = ((value & 0x100) == 0) == 0;
                num = 0;
                if ((this.AttackButton != null) == null)
                {
                    goto Label_0097;
                }
                this.AttackButton.SetActive(((flag == null) || ((value & 0x10) == null)) ? 0 : ((flag4 == null) ? 1 : (this.RenkeiButton == null)));
                if (this.AttackButton.get_activeSelf() == null)
                {
                    goto Label_0097;
                }
                num += 1;
            Label_0097:
                if ((this.RenkeiButton != null) == null)
                {
                    goto Label_00F0;
                }
                this.RenkeiButton.SetActive(((flag == null) || ((value & 0x10) == null)) ? 0 : ((flag4 == null) ? 0 : (this.RenkeiButton != null)));
                if (this.RenkeiButton.get_activeSelf() == null)
                {
                    goto Label_00F0;
                }
                num += 1;
            Label_00F0:
                if ((this.ItemButton != null) == null)
                {
                    goto Label_0135;
                }
                this.ItemButton.SetActive((flag == null) ? 0 : (((value & 0x40) == 0) == 0));
                if (this.ItemButton.get_activeSelf() == null)
                {
                    goto Label_0135;
                }
                num += 1;
            Label_0135:
                num2 = 0;
                goto Label_0185;
            Label_013D:
                this.mAbilityButtons[num2].SetActive((flag == null) ? 0 : (((value & 0x20) == 0) == 0));
                if (this.mAbilityButtons[num2].get_activeSelf() == null)
                {
                    goto Label_017F;
                }
                num += 1;
            Label_017F:
                num2 += 1;
            Label_0185:
                if (num2 < this.mAbilityButtons.Count)
                {
                    goto Label_013D;
                }
                if ((this.GridEventButton != null) == null)
                {
                    goto Label_01CA;
                }
                this.GridEventButton.SetActive(flag2);
                if (this.GridEventButton.get_activeSelf() == null)
                {
                    goto Label_01CA;
                }
                num += 1;
            Label_01CA:
                if ((this.EndButton != null) == null)
                {
                    goto Label_01FD;
                }
                this.EndButton.SetActive(flag3);
                if (this.EndButton.get_activeSelf() == null)
                {
                    goto Label_01FD;
                }
                num += 1;
            Label_01FD:
                if ((this.MapButton != null) == null)
                {
                    goto Label_021B;
                }
                this.MapButton.SetActive(flag5);
            Label_021B:
                if ((this.UnitChgButton != null) == null)
                {
                    goto Label_023F;
                }
                if (this.mIsEnableUnitChgButton == null)
                {
                    goto Label_023F;
                }
                this.EnableUnitChgButton(flag, 0);
            Label_023F:
                if ((this.DonutsBG != null) == null)
                {
                    goto Label_0296;
                }
                if (num < 2)
                {
                    goto Label_0281;
                }
                this.DonutsBG.SetRange(this.DonutsAngleStart, this.DonutsAngleStart + (((float) (num - 1)) * this.DonutsAnglePerItem));
                goto Label_0296;
            Label_0281:
                this.DonutsBG.SetRange(0f, 0f);
            Label_0296:
                return;
            }
        }

        public bool IsEnableUnitChgButton
        {
            get
            {
                return this.mIsEnableUnitChgButton;
            }
        }

        public bool IsActiveUnitChgButton
        {
            get
            {
                return this.mIsActiveUnitChgButton;
            }
        }

        [CompilerGenerated]
        private sealed class <SetAbilities>c__AnonStorey3BB
        {
            internal AbilityData ability;
            internal UnitCommands <>f__this;

            public <SetAbilities>c__AnonStorey3BB()
            {
                base..ctor();
                return;
            }

            internal void <>m__448()
            {
                this.<>f__this.OnSelectAbility(this.ability);
                return;
            }
        }

        [Flags]
        public enum ButtonTypes
        {
            Move = 1,
            Action = 2,
            GridEvent = 4,
            Misc = 8,
            Attack = 0x10,
            Skill = 0x20,
            Item = 0x40,
            IsRenkei = 0x80,
            Map = 0x100
        }

        private delegate void ClickEvent();

        public delegate void CommandEvent(UnitCommands.CommandTypes command, object data);

        public enum CommandTypes
        {
            None,
            Move,
            Attack,
            Ability,
            Item,
            Gimmick,
            Map,
            End
        }

        public delegate void MapExitEvent();

        public delegate void UnitChgEvent();

        public delegate void YesNoEvent(bool yes);
    }
}

