namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/オブジェクト/配置2(2D)", "シーンにオブジェクトを配置します。", 0x555555, 0x444488)]
    public class Event2dAction_SpawnObject3 : EventAction
    {
        public const string ResourceDir = "Event2dAssets/";
        [StringIsResourcePathPopup(typeof(GameObject), "Event2dAssets/")]
        public string ResourceID;
        public string ObjectID;
        private LoadRequest mResourceLoadRequest;
        [HideInInspector]
        public bool Persistent;
        [HideInInspector]
        public Vector2 Position;
        private GameObject mGO;
        public SiblingOrder Order;
        [HideInInspector]
        public string CharaID;
        [HideInInspector]
        public ActorChildOrder ChildOrder;
        private RectTransform rectTransform;
        private Vector2 pvt;

        public Event2dAction_SpawnObject3()
        {
            this.pvt = new Vector2(0.5f, 0.5f);
            base..ctor();
            return;
        }

        private Vector2 convertPosition(Vector2 pos)
        {
            Vector2 vector;
            vector = pos + Vector2.get_one();
            return Vector2.Scale(vector, new Vector2(0.5f, 0.5f));
        }

        public override unsafe void OnActivate()
        {
            GameObject obj2;
            RectTransform transform;
            Transform transform2;
            EventStandCharaController2 controller;
            float num;
            int num2;
            Transform transform3;
            Vector3 vector;
            int num3;
            int num4;
            Transform transform4;
            Vector3 vector2;
            int num5;
            int num6;
            Transform transform5;
            Vector2 vector3;
            Vector3 vector4;
            if (this.mResourceLoadRequest == null)
            {
                goto Label_0477;
            }
            if ((this.mResourceLoadRequest.asset != null) == null)
            {
                goto Label_0477;
            }
            obj2 = this.mResourceLoadRequest.asset as GameObject;
            transform = obj2.GetComponent<RectTransform>();
            if ((transform != null) == null)
            {
                goto Label_0051;
            }
            this.pvt = transform.get_pivot();
        Label_0051:
            transform2 = obj2.get_transform();
            this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.get_zero(), transform2.get_rotation()) as GameObject;
            if (string.IsNullOrEmpty(this.ObjectID) != null)
            {
                goto Label_00A4;
            }
            GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        Label_00A4:
            if (this.Order != null)
            {
                goto Label_00EA;
            }
            if (this.Persistent == null)
            {
                goto Label_0461;
            }
            if ((TacticsSceneSettings.Instance != null) == null)
            {
                goto Label_0461;
            }
            this.mGO.get_transform().SetParent(TacticsSceneSettings.Instance.get_transform(), 1);
            goto Label_0461;
        Label_00EA:
            if (this.Order != 4)
            {
                goto Label_01DA;
            }
            if (string.IsNullOrEmpty(this.CharaID) != null)
            {
                goto Label_0461;
            }
            controller = EventStandCharaController2.FindInstances(this.CharaID);
            if ((controller != null) == null)
            {
                goto Label_0461;
            }
            this.mGO.get_transform().SetParent(controller.get_gameObject().get_transform(), 1);
            if (this.ChildOrder != null)
            {
                goto Label_015A;
            }
            this.mGO.get_transform().SetAsLastSibling();
            goto Label_016A;
        Label_015A:
            this.mGO.get_transform().SetAsFirstSibling();
        Label_016A:
            this.rectTransform = this.mGO.GetComponent<RectTransform>();
            if ((this.rectTransform != null) == null)
            {
                goto Label_0461;
            }
            this.rectTransform.set_pivot(this.pvt);
            this.rectTransform.set_anchoredPosition(Vector2.get_zero());
            vector3 = this.convertPosition(this.Position);
            this.rectTransform.set_anchorMax(vector3);
            this.rectTransform.set_anchorMin(vector3);
            goto Label_0461;
        Label_01DA:
            num = 0f;
            num2 = 0;
            goto Label_0231;
        Label_01E9:
            transform3 = base.ActiveCanvas.get_transform().GetChild(num2);
            if ((transform3.GetComponent<EventDialogBubbleCustom>() != null) == null)
            {
                goto Label_022B;
            }
            num = &transform3.get_transform().get_position().z;
            goto Label_0248;
        Label_022B:
            num2 += 1;
        Label_0231:
            if (num2 < base.ActiveCanvas.get_transform().get_childCount())
            {
                goto Label_01E9;
            }
        Label_0248:
            this.mGO.get_transform().SetParent(base.ActiveCanvas.get_transform(), 1);
            this.mGO.get_transform().SetAsLastSibling();
            if (this.Order != 1)
            {
                goto Label_02B8;
            }
            vector = this.mGO.get_transform().get_position();
            &vector.z = num - 1f;
            this.mGO.get_transform().set_position(vector);
            goto Label_03F6;
        Label_02B8:
            if (this.Order != 2)
            {
                goto Label_0379;
            }
            num3 = -1;
            num4 = 0;
            goto Label_0304;
        Label_02CF:
            if ((base.ActiveCanvas.get_transform().GetChild(num4).GetComponent<EventDialogBubbleCustom>() != null) == null)
            {
                goto Label_02FE;
            }
            num3 = num4;
            goto Label_031B;
        Label_02FE:
            num4 += 1;
        Label_0304:
            if (num4 < base.ActiveCanvas.get_transform().get_childCount())
            {
                goto Label_02CF;
            }
        Label_031B:
            if (num3 <= 0)
            {
                goto Label_0335;
            }
            this.mGO.get_transform().SetSiblingIndex(num3);
        Label_0335:
            if (num >= -1f)
            {
                goto Label_03F6;
            }
            vector2 = this.mGO.get_transform().get_position();
            &vector2.z = num + 1f;
            this.mGO.get_transform().set_position(vector2);
            goto Label_03F6;
        Label_0379:
            if (this.Order != 3)
            {
                goto Label_03F6;
            }
            num5 = -1;
            num6 = 0;
            goto Label_03C5;
        Label_0390:
            if ((base.ActiveCanvas.get_transform().GetChild(num6).GetComponent<EventStandCharaController2>() != null) == null)
            {
                goto Label_03BF;
            }
            num5 = num6;
            goto Label_03DC;
        Label_03BF:
            num6 += 1;
        Label_03C5:
            if (num6 < base.ActiveCanvas.get_transform().get_childCount())
            {
                goto Label_0390;
            }
        Label_03DC:
            if (num5 <= 0)
            {
                goto Label_03F6;
            }
            this.mGO.get_transform().SetSiblingIndex(num5);
        Label_03F6:
            this.rectTransform = this.mGO.GetComponent<RectTransform>();
            if ((this.rectTransform != null) == null)
            {
                goto Label_0461;
            }
            this.rectTransform.set_pivot(this.pvt);
            this.rectTransform.set_anchoredPosition(Vector2.get_zero());
            vector3 = this.convertPosition(this.Position);
            this.rectTransform.set_anchorMax(vector3);
            this.rectTransform.set_anchorMin(vector3);
        Label_0461:
            base.Sequence.SpawnedObjects.Add(this.mGO);
        Label_0477:
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if ((this.mGO != null) == null)
            {
                goto Label_0048;
            }
            if (this.Persistent == null)
            {
                goto Label_003D;
            }
            if ((this.mGO.get_transform().get_parent() == null) == null)
            {
                goto Label_0048;
            }
        Label_003D:
            Object.Destroy(this.mGO);
        Label_0048:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__IteratorA7 ra;
            ra = new <PreloadAssets>c__IteratorA7();
            ra.<>f__this = this;
            return ra;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__IteratorA7 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_SpawnObject3 <>f__this;

            public <PreloadAssets>c__IteratorA7()
            {
                base..ctor();
                return;
            }

            [DebuggerHidden]
            public void Dispose()
            {
                this.$PC = -1;
                return;
            }

            public bool MoveNext()
            {
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0025;

                    case 1:
                        goto Label_008C;

                    case 2:
                        goto Label_009F;
                }
                goto Label_00A6;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.ResourceID) != null)
                {
                    goto Label_008C;
                }
                this.<>f__this.mResourceLoadRequest = GameUtility.LoadResourceAsyncChecked<GameObject>(this.<>f__this.ResourceID);
                if (this.<>f__this.mResourceLoadRequest.isDone != null)
                {
                    goto Label_008C;
                }
                this.$current = this.<>f__this.mResourceLoadRequest.StartCoroutine();
                this.$PC = 1;
                goto Label_00A8;
            Label_008C:
                this.$current = null;
                this.$PC = 2;
                goto Label_00A8;
            Label_009F:
                this.$PC = -1;
            Label_00A6:
                return 0;
            Label_00A8:
                return 1;
                return flag;
            }

            [DebuggerHidden]
            public void Reset()
            {
                throw new NotSupportedException();
            }

            object IEnumerator<object>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.$current;
                }
            }
        }

        public enum ActorChildOrder
        {
            Over,
            Under
        }

        public enum SiblingOrder
        {
            Root,
            OnDialog,
            OnStandChara,
            OnBackGround,
            ChildOfActor
        }
    }
}

