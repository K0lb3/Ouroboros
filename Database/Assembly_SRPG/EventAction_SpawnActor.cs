namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [EventActionInfo("アクター/配置", "キャラクターを配置します", 0x664444, 0xaa4444)]
    public class EventAction_SpawnActor : EventAction
    {
        [StringIsActorID]
        public string ActorID;
        [StringIsUnitID]
        public string UnitID;
        [StringIsJobID]
        public string JobID;
        [SerializeField]
        private IntVector2 Position;
        private TacticsUnitController mController;
        public bool Persistent;
        [Range(0f, 359f)]
        public int Angle;
        public bool ShowEquipments;
        public TacticsUnitController.PostureTypes Posture;

        public EventAction_SpawnActor()
        {
            this.ShowEquipments = 1;
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

        public override unsafe void OnActivate()
        {
            if ((this.mController != null) == null)
            {
                goto Label_007C;
            }
            this.mController.get_transform().set_position(new Vector3(((float) &this.Position.x) + 0.5f, 0f, ((float) &this.Position.y) + 0.5f));
            this.mController.get_transform().set_rotation(Quaternion.AngleAxis((float) this.Angle, Vector3.get_up()));
            this.mController.SetVisible(1);
        Label_007C:
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
            <PreloadAssets>c__Iterator94 iterator;
            iterator = new <PreloadAssets>c__Iterator94();
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
        private sealed class <PreloadAssets>c__Iterator94 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal GameObject <go>__0;
            internal GameObject <persistentScene>__1;
            internal int $PC;
            internal object $current;
            internal EventAction_SpawnActor <>f__this;

            public <PreloadAssets>c__Iterator94()
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

