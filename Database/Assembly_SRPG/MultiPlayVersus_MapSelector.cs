namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class MultiPlayVersus_MapSelector : SRPG_FixedList
    {
        public GameObject ItemTemplate;
        public RectTransform ItemLayoutParent;
        public GameObject SelectWindow;
        public Button ConfirmButton;
        private List<VersusMapParam> m_Param;

        public MultiPlayVersus_MapSelector()
        {
            base..ctor();
            return;
        }

        protected override GameObject CreateItem()
        {
            return Object.Instantiate<GameObject>(this.ItemTemplate);
        }

        private void OnConfirm()
        {
            QuestParam param;
            param = DataSource.FindDataOfClass<QuestParam>(this.SelectWindow, null);
            if (param == null)
            {
                goto Label_002E;
            }
            GlobalVars.SelectedQuestID = param.iname;
            GlobalVars.EditMultiPlayRoomComment = GlobalVars.SelectedMultiPlayRoomComment;
            this.UpdateSelect();
        Label_002E:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "CLOSE_SELECT_WINDOW");
            return;
        }

        protected override void OnItemSelect(GameObject go)
        {
            VersusMapParam param;
            param = DataSource.FindDataOfClass<VersusMapParam>(go, null);
            if (param == null)
            {
                goto Label_003B;
            }
            if ((this.SelectWindow != null) == null)
            {
                goto Label_003B;
            }
            DataSource.Bind<QuestParam>(this.SelectWindow, param.quest);
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "OPEN_SELECTWINDOW");
        Label_003B:
            return;
        }

        protected void RefleshData()
        {
            string str;
            GameManager manager;
            List<QuestParam> list;
            int num;
            VersusMapParam param;
            str = GlobalVars.SelectedQuestID;
            list = MonoSingleton<GameManager>.Instance.GetQuestTypeList(8);
            if (list == null)
            {
                goto Label_0098;
            }
            this.m_Param = new List<VersusMapParam>(list.Count);
            num = 0;
            goto Label_0071;
        Label_0032:
            param = new VersusMapParam();
            param.quest = list[num];
            param.selected = list[num].iname == str;
            this.m_Param.Add(param);
            num += 1;
        Label_0071:
            if (num < list.Count)
            {
                goto Label_0032;
            }
            this.SetData(this.m_Param.ToArray(), typeof(VersusMapParam));
        Label_0098:
            return;
        }

        protected override void Start()
        {
            if ((this.ItemTemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            if ((this.ConfirmButton != null) == null)
            {
                goto Label_003F;
            }
            this.ConfirmButton.get_onClick().AddListener(new UnityAction(this, this.OnConfirm));
        Label_003F:
            base.Start();
            this.RefleshData();
            return;
        }

        protected override void Update()
        {
            base.Update();
            return;
        }

        private void UpdateSelect()
        {
            int num;
            num = 0;
            goto Label_003C;
        Label_0007:
            this.m_Param[num].selected = this.m_Param[num].quest.iname == GlobalVars.SelectedQuestID;
            num += 1;
        Label_003C:
            if (num < this.m_Param.Count)
            {
                goto Label_0007;
            }
            return;
        }

        public override RectTransform ListParent
        {
            get
            {
                return (((this.ItemLayoutParent != null) == null) ? null : this.ItemLayoutParent.GetComponent<RectTransform>());
            }
        }
    }
}

