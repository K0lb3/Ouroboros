namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("オブジェクト/配置(2D)", "シーンにオブジェクトを配置します。", 0x555555, 0x444488)]
    public class Event2dAction_SpawnObject : EventAction
    {
        public const string ResourceDir = "Event2dAssets/";
        [StringIsResourcePath(typeof(GameObject), "Event2dAssets/")]
        public string ResourceID;
        public string ObjectID;
        private LoadRequest mResourceLoadRequest;
        public bool Persistent;
        public Vector3 Position;
        private GameObject mGO;

        public Event2dAction_SpawnObject()
        {
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            Transform transform;
            if (this.mResourceLoadRequest == null)
            {
                goto Label_00DD;
            }
            if ((this.mResourceLoadRequest.asset != null) == null)
            {
                goto Label_00DD;
            }
            obj2 = this.mResourceLoadRequest.asset as GameObject;
            transform = obj2.get_transform();
            this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, this.Position + transform.get_position(), transform.get_rotation()) as GameObject;
            if (string.IsNullOrEmpty(this.ObjectID) != null)
            {
                goto Label_0091;
            }
            GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        Label_0091:
            if (this.Persistent == null)
            {
                goto Label_00C7;
            }
            if ((TacticsSceneSettings.Instance != null) == null)
            {
                goto Label_00C7;
            }
            this.mGO.get_transform().SetParent(TacticsSceneSettings.Instance.get_transform(), 1);
        Label_00C7:
            base.Sequence.SpawnedObjects.Add(this.mGO);
        Label_00DD:
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
            <PreloadAssets>c__IteratorA5 ra;
            ra = new <PreloadAssets>c__IteratorA5();
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
        private sealed class <PreloadAssets>c__IteratorA5 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal Event2dAction_SpawnObject <>f__this;

            public <PreloadAssets>c__IteratorA5()
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
                        goto Label_0096;

                    case 2:
                        goto Label_00A9;
                }
                goto Label_00B0;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.ResourceID) != null)
                {
                    goto Label_0096;
                }
                this.<>f__this.mResourceLoadRequest = GameUtility.LoadResourceAsyncChecked<GameObject>("Event2dAssets/" + this.<>f__this.ResourceID);
                if (this.<>f__this.mResourceLoadRequest.isDone != null)
                {
                    goto Label_0096;
                }
                this.$current = this.<>f__this.mResourceLoadRequest.StartCoroutine();
                this.$PC = 1;
                goto Label_00B2;
            Label_0096:
                this.$current = null;
                this.$PC = 2;
                goto Label_00B2;
            Label_00A9:
                this.$PC = -1;
            Label_00B0:
                return 0;
            Label_00B2:
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
    }
}

