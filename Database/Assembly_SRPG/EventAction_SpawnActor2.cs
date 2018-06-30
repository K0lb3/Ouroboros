namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("New/アクター/配置", "キャラクターを配置します", 0x664444, 0xaa4444)]
    public class EventAction_SpawnActor2 : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        [StringIsLocalUnitIDPopup]
        public string UnitID;
        [StringIsJobIDPopup]
        public string JobID;
        [SerializeField]
        public Vector3 Position;
        protected TacticsUnitController mController;
        public bool Persistent;
        [HideInInspector]
        public int Angle;
        [Range(0f, 359f)]
        public float RotationX;
        [Range(0f, 359f)]
        public float RotationY;
        [Range(0f, 359f)]
        public float RotationZ;
        public bool ShowEquipments;
        [Tooltip("マス目にスナップさせるか？")]
        public bool MoveSnap;
        [Tooltip("地面にスナップさせるか？")]
        public bool GroundSnap;
        [Tooltip("表示設定")]
        public bool Display;
        [Tooltip("ゆれもの設定")]
        public bool Yuremono;
        public TacticsUnitController.PostureTypes Posture;

        public EventAction_SpawnActor2()
        {
            this.ShowEquipments = 1;
            this.MoveSnap = 1;
            this.GroundSnap = 1;
            this.Display = 1;
            this.Yuremono = 1;
            base..ctor();
            return;
        }

        private GameObject GetPersistentScene()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_001B;
            }
            return SceneBattle.Instance.CurrentScene;
        Label_001B:
            return null;
        }

        public override void OnActivate()
        {
            YuremonoInstance[] instanceArray;
            int num;
            if ((this.mController != null) == null)
            {
                goto Label_00A9;
            }
            this.mController.get_transform().set_position(this.Position);
            this.mController.CollideGround = this.GroundSnap;
            this.mController.get_transform().set_rotation(Quaternion.Euler(this.RotationX, this.RotationY, this.RotationZ));
            this.mController.SetVisible(this.Display);
            if (this.Yuremono != null)
            {
                goto Label_00A9;
            }
            instanceArray = this.mController.get_gameObject().GetComponentsInChildren<YuremonoInstance>();
            num = 0;
            goto Label_00A0;
        Label_0093:
            instanceArray[num].set_enabled(0);
            num += 1;
        Label_00A0:
            if (num < ((int) instanceArray.Length))
            {
                goto Label_0093;
            }
        Label_00A9:
            base.ActivateNext();
            return;
        }

        protected override void OnDestroy()
        {
            if ((this.mController != null) == null)
            {
                goto Label_002C;
            }
            if (this.Persistent != null)
            {
                goto Label_002C;
            }
            Object.Destroy(this.mController.get_gameObject());
        Label_002C:
            return;
        }

        [DebuggerHidden]
        public override IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator95 iterator;
            iterator = new <PreloadAssets>c__Iterator95();
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
        private sealed class <PreloadAssets>c__Iterator95 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal GameObject <persistentScene>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_SpawnActor2 <>f__this;

            public <PreloadAssets>c__Iterator95()
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
                Type[] typeArray1;
                uint num;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_0021;

                    case 1:
                        goto Label_017A;
                }
                goto Label_0181;
            Label_0021:
                typeArray1 = new Type[] { typeof(TacticsUnitController) };
                this.<go>__0 = new GameObject("DemoCharacter", typeArray1);
                this.<>f__this.mController = this.<go>__0.GetComponent<TacticsUnitController>();
                this.<>f__this.mController.LoadEquipments = this.<>f__this.ShowEquipments;
                this.<>f__this.mController.Posture = this.<>f__this.Posture;
                this.<>f__this.mController.SetupUnit(this.<>f__this.UnitID, this.<>f__this.JobID);
                this.<>f__this.mController.UniqueName = this.<>f__this.ActorID;
                this.<>f__this.mController.KeepUnitHidden = 1;
                this.<persistentScene>__1 = (this.<>f__this.Persistent == null) ? null : this.<>f__this.GetPersistentScene();
                if ((this.<persistentScene>__1 != null) == null)
                {
                    goto Label_0140;
                }
                this.<>f__this.mController.get_transform().SetParent(this.<persistentScene>__1.get_transform(), 0);
                goto Label_014C;
            Label_0140:
                this.<>f__this.Persistent = 0;
            Label_014C:
                this.<>f__this.Sequence.SpawnedObjects.Add(this.<go>__0);
                this.$current = null;
                this.$PC = 1;
                goto Label_0183;
            Label_017A:
                this.$PC = -1;
            Label_0181:
                return 0;
            Label_0183:
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

