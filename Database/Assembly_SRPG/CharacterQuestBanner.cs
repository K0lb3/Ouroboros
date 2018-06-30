namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class CharacterQuestBanner : MonoBehaviour, IGameParameter
    {
        [SerializeField]
        private GameObject UnitIcon1;
        [SerializeField]
        private GameObject UnitIcon2;
        [SerializeField]
        private Image LockIcon;
        [SerializeField]
        private Image CompleteIcon;
        [SerializeField]
        private Image NewIcon;
        [SerializeField]
        private GameObject Unlocks;
        private UnityAction UnitIconClickCallback1;
        private UnityAction UnitIconClickCallback2;

        public CharacterQuestBanner()
        {
            base..ctor();
            return;
        }

        private void ChangeStatusIcon(CharacterQuestData.EStatus status)
        {
            if (status != null)
            {
                goto Label_003B;
            }
            GameUtility.SetGameObjectActive(this.NewIcon, 1);
            GameUtility.SetGameObjectActive(this.LockIcon, 0);
            GameUtility.SetGameObjectActive(this.CompleteIcon, 0);
            GameUtility.SetGameObjectActive(this.Unlocks, 1);
            goto Label_00EA;
        Label_003B:
            if (status != 2)
            {
                goto Label_0077;
            }
            GameUtility.SetGameObjectActive(this.NewIcon, 0);
            GameUtility.SetGameObjectActive(this.LockIcon, 1);
            GameUtility.SetGameObjectActive(this.CompleteIcon, 0);
            GameUtility.SetGameObjectActive(this.Unlocks, 0);
            goto Label_00EA;
        Label_0077:
            if (status != 3)
            {
                goto Label_00B3;
            }
            GameUtility.SetGameObjectActive(this.NewIcon, 0);
            GameUtility.SetGameObjectActive(this.LockIcon, 0);
            GameUtility.SetGameObjectActive(this.CompleteIcon, 1);
            GameUtility.SetGameObjectActive(this.Unlocks, 1);
            goto Label_00EA;
        Label_00B3:
            if (status != 1)
            {
                goto Label_00EA;
            }
            GameUtility.SetGameObjectActive(this.NewIcon, 0);
            GameUtility.SetGameObjectActive(this.LockIcon, 0);
            GameUtility.SetGameObjectActive(this.CompleteIcon, 0);
            GameUtility.SetGameObjectActive(this.Unlocks, 1);
        Label_00EA:
            return;
        }

        private void DataBind(UnitData unitData, UnitParam unitParam, GameObject target)
        {
            if (unitData == null)
            {
                goto Label_0019;
            }
            DataSource.Bind<UnitParam>(target, null);
            DataSource.Bind<UnitData>(target, unitData);
            goto Label_002D;
        Label_0019:
            if (unitParam == null)
            {
                goto Label_002D;
            }
            DataSource.Bind<UnitData>(target, null);
            DataSource.Bind<UnitParam>(target, unitParam);
        Label_002D:
            return;
        }

        private void OnUnitIcon1_Click()
        {
            this.OnUnitIconClickInternal(this.UnitIcon1);
            return;
        }

        private void OnUnitIcon2_Click()
        {
            this.OnUnitIconClickInternal(this.UnitIcon2);
            return;
        }

        private void OnUnitIconClickInternal(GameObject go)
        {
            FlowNode_OnUnitIconClick click;
            UnitParam param;
            UnitData data;
            click = base.GetComponentInParent<FlowNode_OnUnitIconClick>();
            if ((click == null) == null)
            {
                goto Label_0014;
            }
            return;
        Label_0014:
            param = DataSource.FindDataOfClass<UnitParam>(go, null);
            data = DataSource.FindDataOfClass<UnitData>(go, null);
            if (data == null)
            {
                goto Label_0045;
            }
            GlobalVars.UnlockUnitID = data.UnitParam.iname;
            click.Click();
            goto Label_006B;
        Label_0045:
            if (param == null)
            {
                goto Label_0061;
            }
            GlobalVars.UnlockUnitID = param.iname;
            click.Click();
            goto Label_006B;
        Label_0061:
            DebugUtility.LogWarning("UnitDataがバインドされていません");
        Label_006B:
            return;
        }

        private void Start()
        {
            Button button;
            Button button2;
            if ((this.UnitIcon1 != null) == null)
            {
                goto Label_0040;
            }
            button = this.UnitIcon1.GetComponentInChildren<Button>();
            if ((button != null) == null)
            {
                goto Label_0040;
            }
            button.get_onClick().AddListener(new UnityAction(this, this.OnUnitIcon1_Click));
        Label_0040:
            if ((this.UnitIcon2 != null) == null)
            {
                goto Label_0080;
            }
            button2 = this.UnitIcon2.GetComponentInChildren<Button>();
            if ((button2 != null) == null)
            {
                goto Label_0080;
            }
            button2.get_onClick().AddListener(new UnityAction(this, this.OnUnitIcon2_Click));
        Label_0080:
            return;
        }

        public void UpdateValue()
        {
            CharacterQuestData data;
            if (base.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            data = DataSource.FindDataOfClass<CharacterQuestData>(base.get_gameObject(), null);
            if (data == null)
            {
                goto Label_0094;
            }
            if (data.questType != null)
            {
                goto Label_004C;
            }
            this.DataBind(data.unitData1, data.unitParam1, this.UnitIcon1);
            goto Label_0088;
        Label_004C:
            if (data.questType != 1)
            {
                goto Label_0088;
            }
            this.DataBind(data.unitData1, data.unitParam1, this.UnitIcon1);
            this.DataBind(data.unitData2, data.unitParam2, this.UnitIcon2);
        Label_0088:
            this.ChangeStatusIcon(data.Status);
        Label_0094:
            return;
        }
    }
}

