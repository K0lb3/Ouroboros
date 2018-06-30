namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("New/立ち絵2/反転(2D)", "立ち絵2を反転します", 0x555555, 0x444488)]
    public class Event2dAction_FlipStandChara : EventAction
    {
        public static List<EventStandCharaController2> InstancesForFlip;
        public string CharaID;
        public float Time;
        public bool async;
        private GameObject mStandObjectFlip;
        private EventStandCharaController2 mEVCharaController;
        private EventStandCharaController2 mEVCharaFlipController;
        private float offset;
        private Color InColor;
        private Color OutColor;
        private List<RawImage> fadeInList;
        private List<RawImage> fadeOutList;

        static Event2dAction_FlipStandChara()
        {
            InstancesForFlip = new List<EventStandCharaController2>();
            return;
        }

        public Event2dAction_FlipStandChara()
        {
            this.Time = 1f;
            this.fadeInList = new List<RawImage>();
            this.fadeOutList = new List<RawImage>();
            base..ctor();
            return;
        }

        public override unsafe void OnActivate()
        {
            RectTransform transform;
            RectTransform transform2;
            int num;
            GameObject obj2;
            GameObject obj3;
            int num2;
            GameObject obj4;
            GameObject obj5;
            int num3;
            int num4;
            if ((this.mStandObjectFlip == null) == null)
            {
                goto Label_0018;
            }
            base.ActivateNext();
            return;
        Label_0018:
            if (this.mStandObjectFlip.get_gameObject().get_activeInHierarchy() != null)
            {
                goto Label_003E;
            }
            this.mStandObjectFlip.get_gameObject().SetActive(1);
        Label_003E:
            this.mEVCharaFlipController.Emotion = this.mEVCharaController.Emotion;
            transform = this.mEVCharaController.get_gameObject().get_transform() as RectTransform;
            transform2 = this.mStandObjectFlip.get_transform() as RectTransform;
            transform2.SetParent(transform.get_parent());
            transform2.set_localScale(transform.get_localScale());
            transform2.set_anchorMax(transform.get_anchorMax());
            transform2.set_anchorMin(transform.get_anchorMin());
            transform2.set_anchoredPosition(transform.get_anchoredPosition());
            transform2.set_localRotation(transform.get_localRotation() * Quaternion.Euler(0f, 180f, 0f));
            this.mEVCharaFlipController.Open(0f);
            num = 0;
            goto Label_0164;
        Label_00F3:
            obj2 = this.mEVCharaController.StandCharaList[num].GetComponent<EventStandChara2>().BodyObject;
            if ((obj2 != null) == null)
            {
                goto Label_0128;
            }
            this.fadeOutList.Add(obj2.GetComponent<RawImage>());
        Label_0128:
            obj3 = this.mEVCharaController.StandCharaList[num].GetComponent<EventStandChara2>().FaceObject;
            if ((obj3 != null) == null)
            {
                goto Label_0160;
            }
            this.fadeOutList.Add(obj3.GetComponent<RawImage>());
        Label_0160:
            num += 1;
        Label_0164:
            if (num < ((int) this.mEVCharaController.StandCharaList.Length))
            {
                goto Label_00F3;
            }
            num2 = 0;
            goto Label_01F7;
        Label_017F:
            obj4 = this.mEVCharaFlipController.StandCharaList[num2].GetComponent<EventStandChara2>().BodyObject;
            if ((obj4 != null) == null)
            {
                goto Label_01B8;
            }
            this.fadeInList.Add(obj4.GetComponent<RawImage>());
        Label_01B8:
            obj5 = this.mEVCharaFlipController.StandCharaList[num2].GetComponent<EventStandChara2>().FaceObject;
            if ((obj5 != null) == null)
            {
                goto Label_01F1;
            }
            this.fadeInList.Add(obj5.GetComponent<RawImage>());
        Label_01F1:
            num2 += 1;
        Label_01F7:
            if (num2 < ((int) this.mEVCharaFlipController.StandCharaList.Length))
            {
                goto Label_017F;
            }
            num3 = 0;
            goto Label_024D;
        Label_0213:
            if (this.fadeOutList[num3].get_isActiveAndEnabled() == null)
            {
                goto Label_0247;
            }
            this.InColor = this.fadeOutList[num3].get_color();
            goto Label_025F;
        Label_0247:
            num3 += 1;
        Label_024D:
            if (num3 < this.fadeOutList.Count)
            {
                goto Label_0213;
            }
        Label_025F:
            this.OutColor = this.InColor;
            &this.OutColor.a = 0f;
            num4 = 0;
            goto Label_02A1;
        Label_0283:
            this.fadeInList[num4].set_color(this.InColor);
            num4 += 1;
        Label_02A1:
            if (num4 < this.fadeInList.Count)
            {
                goto Label_0283;
            }
            this.offset = (float) ((this.Time > 0f) ? 0 : 1);
            if (this.async == null)
            {
                goto Label_02E3;
            }
            base.ActivateNext(1);
        Label_02E3:
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mStandObjectFlip != null) == null)
            {
                goto Label_0021;
            }
            Object.Destroy(this.mStandObjectFlip.get_gameObject());
        Label_0021:
            return;
        }

        public override void PreStart()
        {
            string str;
            int num;
            if (string.IsNullOrEmpty(this.CharaID) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            this.mEVCharaController = EventStandCharaController2.FindInstances(this.CharaID);
            str = this.CharaID + "_Flip";
            num = 0;
            goto Label_0080;
        Label_003A:
            if ((InstancesForFlip[num].CharaID == str) == null)
            {
                goto Label_007C;
            }
            this.mEVCharaFlipController = InstancesForFlip[num];
            this.mStandObjectFlip = this.mEVCharaFlipController.get_gameObject();
            goto Label_0090;
        Label_007C:
            num += 1;
        Label_0080:
            if (num < InstancesForFlip.Count)
            {
                goto Label_003A;
            }
        Label_0090:
            if ((this.mEVCharaFlipController == null) == null)
            {
                goto Label_00F5;
            }
            if ((this.mEVCharaController != null) == null)
            {
                goto Label_00F5;
            }
            this.mStandObjectFlip = Object.Instantiate<GameObject>(this.mEVCharaController.get_gameObject());
            this.mEVCharaFlipController = this.mStandObjectFlip.GetComponent<EventStandCharaController2>();
            this.mEVCharaFlipController.CharaID = str;
            InstancesForFlip.Add(this.mEVCharaFlipController);
        Label_00F5:
            return;
        }

        public override void Update()
        {
            int num;
            Color color;
            int num2;
            Color color2;
            int num3;
            if (this.offset < 1f)
            {
                goto Label_00AB;
            }
            this.mEVCharaFlipController.Close(0f);
            this.mEVCharaFlipController.get_gameObject().SetActive(0);
            this.mEVCharaController.get_gameObject().GetComponent<RectTransform>().Rotate(new Vector3(0f, 180f, 0f));
            num = 0;
            goto Label_007C;
        Label_0061:
            this.fadeOutList[num].set_color(this.InColor);
            num += 1;
        Label_007C:
            if (num < this.fadeOutList.Count)
            {
                goto Label_0061;
            }
            if (this.async == null)
            {
                goto Label_00A4;
            }
            base.enabled = 0;
            goto Label_00AA;
        Label_00A4:
            base.ActivateNext();
        Label_00AA:
            return;
        Label_00AB:
            color = Color.Lerp(this.OutColor, this.InColor, this.offset);
            num2 = 0;
            goto Label_00E0;
        Label_00CA:
            this.fadeInList[num2].set_color(color);
            num2 += 1;
        Label_00E0:
            if (num2 < this.fadeInList.Count)
            {
                goto Label_00CA;
            }
            color2 = Color.Lerp(this.InColor, this.OutColor, this.offset);
            num3 = 0;
            goto Label_012A;
        Label_0111:
            this.fadeOutList[num3].set_color(color2);
            num3 += 1;
        Label_012A:
            if (num3 < this.fadeOutList.Count)
            {
                goto Label_0111;
            }
            this.offset += UnityEngine.Time.get_deltaTime() / this.Time;
            this.offset = Mathf.Clamp01(this.offset);
            return;
        }
    }
}

