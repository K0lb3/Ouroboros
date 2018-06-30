namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/オブジェクト/配置", "シーンにオブジェクトを配置します。", 0x445555, 0x448888)]
    public class EventAction_SpawnObject2 : EventAction
    {
        public const string ResourceDir = "EventAssets/";
        [StringIsResourcePathPopup(typeof(GameObject), "EventAssets/")]
        public string ResourceID;
        [StringIsObjectList]
        public string ObjectID;
        private LoadRequest mResourceLoadRequest;
        public bool Persistent;
        public Vector3 Position;
        [Range(0f, 360f)]
        private float Angle;
        [Range(0f, 359f)]
        public float Rotate_x;
        [Range(0f, 359f)]
        public float Rotate_y;
        [Range(0f, 359f)]
        public float Rotate_z;
        public Vector3 Scale;
        public Vector3 mousepos;
        private GameObject mGO;

        public EventAction_SpawnObject2()
        {
            this.Scale = Vector3.get_one();
            base..ctor();
            return;
        }

        public override void OnActivate()
        {
            GameObject obj2;
            Transform transform;
            Quaternion quaternion;
            if (this.mResourceLoadRequest == null)
            {
                goto Label_011C;
            }
            if ((this.mResourceLoadRequest.asset != null) == null)
            {
                goto Label_011C;
            }
            obj2 = this.mResourceLoadRequest.asset as GameObject;
            transform = obj2.get_transform();
            quaternion = Quaternion.Euler(this.Rotate_x, this.Rotate_y, this.Rotate_z);
            this.mGO = Object.Instantiate(this.mResourceLoadRequest.asset, this.Position + transform.get_position(), quaternion * transform.get_rotation()) as GameObject;
            this.mGO.get_transform().set_localScale(Vector3.Scale(transform.get_localScale(), this.Scale));
            if (string.IsNullOrEmpty(this.ObjectID) != null)
            {
                goto Label_00D0;
            }
            GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        Label_00D0:
            if (this.Persistent == null)
            {
                goto Label_0106;
            }
            if ((TacticsSceneSettings.Instance != null) == null)
            {
                goto Label_0106;
            }
            this.mGO.get_transform().SetParent(TacticsSceneSettings.Instance.get_transform(), 1);
        Label_0106:
            base.Sequence.SpawnedObjects.Add(this.mGO);
        Label_011C:
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
            <PreloadAssets>c__Iterator99 iterator;
            iterator = new <PreloadAssets>c__Iterator99();
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
        private sealed class <PreloadAssets>c__Iterator99 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;
            internal EventAction_SpawnObject2 <>f__this;

            public <PreloadAssets>c__Iterator99()
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
    }
}

