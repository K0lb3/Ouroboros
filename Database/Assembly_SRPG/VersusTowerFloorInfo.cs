namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class VersusTowerFloorInfo : MonoBehaviour
    {
        private readonly int ScrollMargin;
        private readonly float ScrollSpd;
        public GameObject Keytemplate;
        public GameObject parent;
        public GameObject playerInfo;
        public ScrollListController ScrollCtrl;
        public Button currentButton;
        public ScrollRect Scroll;
        private float AutoScrollGoal;
        private bool AutoScroll;

        public VersusTowerFloorInfo()
        {
            this.ScrollMargin = 3;
            this.ScrollSpd = 50f;
            base..ctor();
            return;
        }

        private void OnClickScroll()
        {
            GameManager manager;
            int num;
            float num2;
            float num3;
            if ((this.ScrollCtrl != null) == null)
            {
                goto Label_008A;
            }
            num = Mathf.Max(MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor - this.ScrollMargin, 0);
            num2 = this.ScrollCtrl.ItemScale;
            num3 = -((((float) num) * num2) + (num2 * 0.5f));
            this.AutoScrollGoal = Mathf.Min(num3, 0f);
            this.AutoScroll = 1;
            if ((this.Scroll != null) == null)
            {
                goto Label_007F;
            }
            this.Scroll.set_inertia(0);
        Label_007F:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "START_CURRENT_SCROLL");
        Label_008A:
            return;
        }

        private void Refresh()
        {
            GameManager manager;
            PlayerData data;
            int num;
            VersusTowerParam param;
            int num2;
            GameObject obj2;
            Transform transform;
            Transform transform2;
            if ((this.Keytemplate == null) == null)
            {
                goto Label_0012;
            }
            return;
        Label_0012:
            manager = MonoSingleton<GameManager>.Instance;
            num = manager.Player.VersusTowerKey;
            param = manager.GetCurrentVersusTowerParam(-1);
            if (param == null)
            {
                goto Label_0117;
            }
            num2 = 0;
            goto Label_00F9;
        Label_003C:
            obj2 = Object.Instantiate<GameObject>(this.Keytemplate);
            if ((obj2 == null) == null)
            {
                goto Label_005B;
            }
            goto Label_00EF;
        Label_005B:
            obj2.SetActive(1);
            if ((this.parent != null) == null)
            {
                goto Label_008C;
            }
            obj2.get_transform().SetParent(this.parent.get_transform(), 0);
        Label_008C:
            transform = obj2.get_transform().FindChild("on");
            transform2 = obj2.get_transform().FindChild("off");
            if ((transform != null) == null)
            {
                goto Label_00CF;
            }
            transform.get_gameObject().SetActive(num > 0);
        Label_00CF:
            if ((transform2 != null) == null)
            {
                goto Label_00EF;
            }
            transform2.get_gameObject().SetActive((num > 0) == 0);
        Label_00EF:
            num2 += 1;
            num -= 1;
        Label_00F9:
            if (num2 < param.RankupNum)
            {
                goto Label_003C;
            }
            this.Keytemplate.SetActive(0);
        Label_0117:
            return;
        }

        private void Start()
        {
            if ((this.currentButton != null) == null)
            {
                goto Label_002D;
            }
            this.currentButton.get_onClick().AddListener(new UnityAction(this, this.OnClickScroll));
        Label_002D:
            this.Refresh();
            return;
        }

        public void Update()
        {
            bool flag;
            List<RectTransform> list;
            GameManager manager;
            int num;
            int num2;
            VersusTowerFloor floor;
            int num3;
            if (this.AutoScroll == null)
            {
                goto Label_0067;
            }
            if ((this.ScrollCtrl != null) == null)
            {
                goto Label_0067;
            }
            if (this.ScrollCtrl.MovePos(this.AutoScrollGoal, this.ScrollSpd) == null)
            {
                goto Label_0067;
            }
            this.AutoScroll = 0;
            if ((this.Scroll != null) == null)
            {
                goto Label_005C;
            }
            this.Scroll.set_inertia(1);
        Label_005C:
            FlowNode_TriggerLocalEvent.TriggerLocalEvent(this, "STOP_CURRENT_SCROLL");
        Label_0067:
            if ((this.ScrollCtrl != null) == null)
            {
                goto Label_011C;
            }
            if ((this.playerInfo != null) == null)
            {
                goto Label_011C;
            }
            flag = 0;
            list = this.ScrollCtrl.ItemList;
            num = MonoSingleton<GameManager>.Instance.Player.VersusTowerFloor;
            num2 = 0;
            goto Label_00FD;
        Label_00B1:
            floor = list[num2].get_gameObject().GetComponent<VersusTowerFloor>();
            if ((floor != null) == null)
            {
                goto Label_00F7;
            }
            if (floor.Floor != num)
            {
                goto Label_00F7;
            }
            flag = 1;
            floor.SetPlayerObject(this.playerInfo);
            goto Label_010A;
        Label_00F7:
            num2 += 1;
        Label_00FD:
            if (num2 < list.Count)
            {
                goto Label_00B1;
            }
        Label_010A:
            if (flag != null)
            {
                goto Label_011C;
            }
            this.playerInfo.SetActive(0);
        Label_011C:
            return;
        }
    }
}

