namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    [Pin(100, "Close", 1, 100)]
    public class UnitJobMasterWindow : SRPG_FixedList, IFlowInterface
    {
        public GameObject StatusItemTemplate;
        public Button NextButton;
        private List<JobMasterValue> mStatusValues;

        public UnitJobMasterWindow()
        {
            this.mStatusValues = new List<JobMasterValue>();
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.StatusItemTemplate);
        }

        private void OnNextClick()
        {
            if (base.Page >= (base.MaxPage - 1))
            {
                goto Label_001A;
            }
            this.GotoNextPage();
            return;
        Label_001A:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            return;
        }

        protected override unsafe void OnUpdateItem(GameObject go, int index)
        {
            StatusListItem item;
            string str;
            int num;
            int num2;
            item = go.GetComponent<StatusListItem>();
            if ((item != null) == null)
            {
                goto Label_00F4;
            }
            str = this.mStatusValues[index].type;
            num = this.mStatusValues[index].old_value;
            num2 = this.mStatusValues[index].new_value;
            item.get_gameObject().SetActive(1);
            if ((item.Label != null) == null)
            {
                goto Label_0081;
            }
            item.Label.set_text(LocalizedText.Get("sys." + str));
        Label_0081:
            if ((item.Value != null) == null)
            {
                goto Label_00A4;
            }
            item.Value.set_text(&num.ToString());
        Label_00A4:
            if ((item.Bonus != null) == null)
            {
                goto Label_00F4;
            }
            if (num2 == null)
            {
                goto Label_00E3;
            }
            item.Bonus.set_text(&num2.ToString());
            item.Bonus.get_gameObject().SetActive(1);
            goto Label_00F4;
        Label_00E3:
            item.Bonus.get_gameObject().SetActive(0);
        Label_00F4:
            return;
        }

        public void Refresh(BaseStatus old_status, BaseStatus new_status)
        {
            string[] strArray;
            Array array;
            int num;
            ParamTypes types;
            int num2;
            int num3;
            JobMasterValue value2;
            if (old_status == null)
            {
                goto Label_000C;
            }
            if (new_status != null)
            {
                goto Label_000D;
            }
        Label_000C:
            return;
        Label_000D:
            this.mStatusValues.Clear();
            strArray = Enum.GetNames(typeof(ParamTypes));
            array = Enum.GetValues(typeof(ParamTypes));
            num = 0;
            goto Label_00BC;
        Label_003F:
            types = (short) array.GetValue(num);
            if (types == null)
            {
                goto Label_00B8;
            }
            if (types != 2)
            {
                goto Label_005E;
            }
            goto Label_00B8;
        Label_005E:
            num2 = old_status[types];
            num3 = new_status[types];
            if (num2 != num3)
            {
                goto Label_0088;
            }
            goto Label_00B8;
        Label_0088:
            value2 = new JobMasterValue();
            value2.type = strArray[num];
            value2.old_value = num2;
            value2.new_value = num3;
            this.mStatusValues.Add(value2);
        Label_00B8:
            num += 1;
        Label_00BC:
            if (num < array.Length)
            {
                goto Label_003F;
            }
            this.SetData(this.mStatusValues.ToArray(), typeof(JobMasterValue));
            return;
        }

        protected override void Start()
        {
            if ((this.StatusItemTemplate != null) == null)
            {
                goto Label_002D;
            }
            if (this.StatusItemTemplate.get_activeInHierarchy() == null)
            {
                goto Label_002D;
            }
            this.StatusItemTemplate.SetActive(0);
        Label_002D:
            if ((this.NextButton != null) == null)
            {
                goto Label_005A;
            }
            this.NextButton.get_onClick().AddListener(new UnityAction(this, this.OnNextClick));
        Label_005A:
            return;
        }
    }
}

