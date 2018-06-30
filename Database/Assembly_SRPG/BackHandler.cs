namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class BackHandler : MonoBehaviour
    {
        private static List<BackHandler> mHdls;

        static BackHandler()
        {
            mHdls = new List<BackHandler>();
            return;
        }

        public BackHandler()
        {
            base..ctor();
            return;
        }

        public static void Invoke()
        {
            SceneBattle battle;
            int num;
            BackHandler handler;
            ButtonEvent event2;
            GraphicRaycaster raycaster;
            Graphic graphic;
            CanvasGroup[] groupArray;
            bool flag;
            int num2;
            PointerEventData data;
            Button button;
            GraphicRaycaster raycaster2;
            CanvasGroup[] groupArray2;
            bool flag2;
            int num3;
            PointerEventData data2;
            battle = SceneBattle.Instance;
            if (battle == null)
            {
                goto Label_0022;
            }
            if (battle.IsControlBattleUI(0x200) != null)
            {
                goto Label_0022;
            }
            return;
        Label_0022:
            num = mHdls.Count;
            goto Label_0245;
        Label_0032:
            handler = mHdls[num - 1];
            if ((null == handler) == null)
            {
                goto Label_0051;
            }
            goto Label_0241;
        Label_0051:
            event2 = handler.get_gameObject().GetComponent<ButtonEvent>();
            if ((event2 != null) == null)
            {
                goto Label_0163;
            }
            raycaster = handler.get_gameObject().GetComponentInParent<GraphicRaycaster>();
            if ((raycaster == null) != null)
            {
                goto Label_0241;
            }
            if (raycaster.get_enabled() != null)
            {
                goto Label_0094;
            }
            goto Label_0241;
        Label_0094:
            graphic = handler.get_gameObject().GetComponent<Graphic>();
            if ((graphic == null) != null)
            {
                goto Label_0241;
            }
            if (graphic.get_enabled() == null)
            {
                goto Label_0241;
            }
            if (graphic.get_raycastTarget() != null)
            {
                goto Label_00CB;
            }
            goto Label_0241;
        Label_00CB:
            groupArray = handler.GetComponentsInParent<CanvasGroup>();
            flag = 0;
            num2 = 0;
            goto Label_010F;
        Label_00DE:
            if (groupArray[num2].get_blocksRaycasts() != null)
            {
                goto Label_00F5;
            }
            flag = 1;
            goto Label_011A;
        Label_00F5:
            if (groupArray[num2].get_ignoreParentGroups() == null)
            {
                goto Label_0109;
            }
            goto Label_011A;
        Label_0109:
            num2 += 1;
        Label_010F:
            if (num2 < ((int) groupArray.Length))
            {
                goto Label_00DE;
            }
        Label_011A:
            if (flag == null)
            {
                goto Label_0126;
            }
            goto Label_0241;
        Label_0126:
            data = new PointerEventData(EventSystem.get_current());
            data.set_position(handler.get_gameObject().get_transform().get_position());
            data.set_clickCount(1);
            event2.OnPointerClick(data);
            goto Label_024C;
        Label_0163:
            button = handler.get_gameObject().GetComponent<Button>();
            if ((null != button) == null)
            {
                goto Label_0241;
            }
            raycaster2 = handler.get_gameObject().GetComponentInParent<GraphicRaycaster>();
            if ((null == raycaster2) != null)
            {
                goto Label_0241;
            }
            if (raycaster2.get_enabled() != null)
            {
                goto Label_01A8;
            }
            goto Label_0241;
        Label_01A8:
            groupArray2 = handler.GetComponentsInParent<CanvasGroup>();
            flag2 = 0;
            num3 = 0;
            goto Label_01EC;
        Label_01BB:
            if (groupArray2[num3].get_blocksRaycasts() != null)
            {
                goto Label_01D2;
            }
            flag2 = 1;
            goto Label_01F7;
        Label_01D2:
            if (groupArray2[num3].get_ignoreParentGroups() == null)
            {
                goto Label_01E6;
            }
            goto Label_01F7;
        Label_01E6:
            num3 += 1;
        Label_01EC:
            if (num3 < ((int) groupArray2.Length))
            {
                goto Label_01BB;
            }
        Label_01F7:
            if (flag2 == null)
            {
                goto Label_0203;
            }
            goto Label_0241;
        Label_0203:
            data2 = new PointerEventData(EventSystem.get_current());
            data2.set_position(handler.get_gameObject().get_transform().get_position());
            data2.set_clickCount(1);
            button.OnPointerClick(data2);
            goto Label_024C;
        Label_0241:
            num -= 1;
        Label_0245:
            if (0 < num)
            {
                goto Label_0032;
            }
        Label_024C:
            return;
        }

        private void OnDisable()
        {
            mHdls.Remove(this);
            return;
        }

        private void OnEnable()
        {
            mHdls.Add(this);
            return;
        }
    }
}

