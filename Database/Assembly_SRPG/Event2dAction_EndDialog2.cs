namespace SRPG
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [EventActionInfo("New/会話/閉じる(2D)", "表示されている吹き出しを閉じます", 0x555555, 0x444488)]
    public class Event2dAction_EndDialog2 : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        private EventDialogBubbleCustom mBubble;
        public bool Async;
        public float FadeTime;
        private float fadingTime;
        private bool IsFading;
        private List<GameObject> fadeInList;
        private List<CanvasGroup> fadeInParticleList;

        public Event2dAction_EndDialog2()
        {
            this.FadeTime = 0.2f;
            this.fadeInList = new List<GameObject>();
            this.fadeInParticleList = new List<CanvasGroup>();
            base..ctor();
            return;
        }

        private unsafe void FadeIn(float time)
        {
            float num;
            Color color;
            GameObject obj2;
            List<GameObject>.Enumerator enumerator;
            EventStandChara2 chara;
            string str;
            Color color2;
            Color color3;
            float num2;
            CanvasGroup group;
            List<CanvasGroup>.Enumerator enumerator2;
            Color color4;
            num = time / this.FadeTime;
            color = Color.Lerp(Color.get_white(), Color.get_grey(), num);
            enumerator = this.fadeInList.GetEnumerator();
        Label_0026:
            try
            {
                goto Label_00C9;
            Label_002B:
                obj2 = &enumerator.Current;
                chara = obj2.GetComponent<EventStandChara2>();
                str = obj2.GetComponentInParent<EventStandCharaController2>().CharaID;
                color2 = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(str) == null)
                {
                    goto Label_006E;
                }
                color2 = Event2dAction_OperateStandChara.CharaColorDic[str];
            Label_006E:
                color3 = color2 * color;
                if (&chara.BodyObject.GetComponent<RawImage>().get_color().get_maxColorComponent() <= &color3.get_maxColorComponent())
                {
                    goto Label_00A3;
                }
                goto Label_00C9;
            Label_00A3:
                chara.FaceObject.GetComponent<RawImage>().set_color(color3);
                chara.BodyObject.GetComponent<RawImage>().set_color(color3);
            Label_00C9:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002B;
                }
                goto Label_00E6;
            }
            finally
            {
            Label_00DA:
                ((List<GameObject>.Enumerator) enumerator).Dispose();
            }
        Label_00E6:
            num2 = Mathf.Lerp(1f, 0f, num);
            enumerator2 = this.fadeInParticleList.GetEnumerator();
        Label_0105:
            try
            {
                goto Label_012F;
            Label_010A:
                group = &enumerator2.Current;
                if (group.get_alpha() <= num2)
                {
                    goto Label_0126;
                }
                goto Label_012F;
            Label_0126:
                group.set_alpha(num2);
            Label_012F:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_010A;
                }
                goto Label_014D;
            }
            finally
            {
            Label_0140:
                ((List<CanvasGroup>.Enumerator) enumerator2).Dispose();
            }
        Label_014D:
            return;
        }

        public override unsafe void OnActivate()
        {
            int num;
            EventStandCharaController2 controller;
            List<EventStandCharaController2>.Enumerator enumerator;
            Color color;
            GameObject obj2;
            GameObject[] objArray;
            int num2;
            GameObjectID[] tidArray;
            int num3;
            CanvasGroup group;
            if (string.IsNullOrEmpty(this.ActorID) == null)
            {
                goto Label_0042;
            }
            num = EventDialogBubbleCustom.Instances.Count - 1;
            goto Label_0036;
        Label_0022:
            EventDialogBubbleCustom.Instances[num].Close();
            num -= 1;
        Label_0036:
            if (num >= 0)
            {
                goto Label_0022;
            }
            goto Label_006F;
        Label_0042:
            this.mBubble = EventDialogBubbleCustom.Find(this.ActorID);
            if ((this.mBubble != null) == null)
            {
                goto Label_006F;
            }
            this.mBubble.Close();
        Label_006F:
            this.fadeInList.Clear();
            this.fadeInParticleList.Clear();
            this.IsFading = 0;
            if (EventStandCharaController2.Instances == null)
            {
                goto Label_01D1;
            }
            if (EventStandCharaController2.Instances.Count <= 0)
            {
                goto Label_01D1;
            }
            enumerator = EventStandCharaController2.Instances.GetEnumerator();
        Label_00B1:
            try
            {
                goto Label_01B4;
            Label_00B6:
                controller = &enumerator.Current;
                if (controller.IsClose == null)
                {
                    goto Label_00CE;
                }
                goto Label_01B4;
            Label_00CE:
                color = Color.get_white();
                if (Event2dAction_OperateStandChara.CharaColorDic.ContainsKey(controller.CharaID) == null)
                {
                    goto Label_00FA;
                }
                color = Event2dAction_OperateStandChara.CharaColorDic[controller.CharaID];
            Label_00FA:
                objArray = controller.StandCharaList;
                num2 = 0;
                goto Label_014C;
            Label_010A:
                obj2 = objArray[num2];
                if ((obj2.GetComponent<EventStandChara2>().FaceObject.GetComponent<RawImage>().get_color() != color) == null)
                {
                    goto Label_0146;
                }
                this.fadeInList.Add(obj2);
                this.IsFading = 1;
            Label_0146:
                num2 += 1;
            Label_014C:
                if (num2 < ((int) objArray.Length))
                {
                    goto Label_010A;
                }
                tidArray = controller.get_gameObject().GetComponentsInChildren<GameObjectID>();
                num3 = 0;
                goto Label_01A9;
            Label_016C:
                group = tidArray[num3].GetComponent<CanvasGroup>();
                if ((group != null) == null)
                {
                    goto Label_01A3;
                }
                if (group.get_alpha() == 1f)
                {
                    goto Label_01A3;
                }
                this.fadeInParticleList.Add(group);
            Label_01A3:
                num3 += 1;
            Label_01A9:
                if (num3 < ((int) tidArray.Length))
                {
                    goto Label_016C;
                }
            Label_01B4:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_00B6;
                }
                goto Label_01D1;
            }
            finally
            {
            Label_01C5:
                ((List<EventStandCharaController2>.Enumerator) enumerator).Dispose();
            }
        Label_01D1:
            if (this.IsFading != null)
            {
                goto Label_01E3;
            }
            base.ActivateNext();
            return;
        Label_01E3:
            this.fadingTime = this.FadeTime;
            if (this.Async == null)
            {
                goto Label_0201;
            }
            base.ActivateNext(1);
        Label_0201:
            return;
        }

        public override void Update()
        {
            if (this.IsFading != null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            this.fadingTime -= Time.get_deltaTime();
            if (this.fadingTime > 0f)
            {
                goto Label_005D;
            }
            this.fadingTime = 0f;
            this.IsFading = 0;
            if (this.Async == null)
            {
                goto Label_0057;
            }
            base.enabled = 0;
            goto Label_005D;
        Label_0057:
            base.ActivateNext();
        Label_005D:
            this.FadeIn(this.fadingTime);
            return;
        }
    }
}

