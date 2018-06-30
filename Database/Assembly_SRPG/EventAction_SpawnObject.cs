namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("オブジェクト/配置", "シーンにオブジェクトを配置します。", 0x445555, 0x448888)]
    public class EventAction_SpawnObject : EventAction
    {
        public const string ResourceDir = "EventAssets/";
        [StringIsResourcePath(typeof(GameObject), "EventAssets/")]
        public string ResourceID;
        public string ObjectID;
        private LoadRequest mResourceLoadRequest;
        public bool Persistent;
        public Vector3 Position;
        private GameObject mGO;

        public EventAction_SpawnObject()
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
            <PreloadAssets>c__Iterator98 iterator;
            iterator = new <PreloadAssets>c__Iterator98();
            iterator.<>f__this = this;
            return iterator;
        }

        public override bool IsPreloadAssets
        {
            get
            {
                return 1;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator98 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal EventAction_SpawnObject <>f__this;

            public <PreloadAssets>c__Iterator98()
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
                        goto Label_00B1;

                    case 2:
                        goto Label_00C4;
                }
                goto Label_00CB;
            Label_0025:
                if (string.IsNullOrEmpty(this.<>f__this.ResourceID) != null)
                {
                    goto Label_00B1;
                }
                this.<>f__this.mResourceLoadRequest = GameUtility.LoadResourceAsyncChecked<GameObject>("EventAssets/" + this.<>f__this.ResourceID);
                if ((this.<>f__this.mResourceLoadRequest.asset == null) == null)
                {
                    goto Label_00B1;
                }
                if (this.<>f__this.mResourceLoadRequest.isDone != null)
                {
                    goto Label_00B1;
                }
                this.$current = this.<>f__this.mResourceLoadRequest.StartCoroutine();
                this.$PC = 1;
                goto Label_00CD;
            Label_00B1:
                this.$current = null;
                this.$PC = 2;
                goto Label_00CD;
            Label_00C4:
                this.$PC = -1;
            Label_00CB:
                return 0;
            Label_00CD:
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

