namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    [ExecuteInEditMode]
    public abstract class EventAction : ScriptableObject
    {
        [NonSerialized]
        public SRPG.EventScript.Sequence Sequence;
        private bool mEnabled;
        [HideInInspector]
        public bool Skip;
        [NonSerialized]
        public EventAction NextAction;
        public static bool IsPreloading;

        static EventAction()
        {
        }

        protected EventAction()
        {
            base..ctor();
            return;
        }

        protected void ActivateNext()
        {
            this.ActivateNext(0);
            return;
        }

        protected void ActivateNext(bool keepActive)
        {
            EventAction action;
            this.enabled = keepActive;
            action = this.NextAction;
            goto Label_0036;
        Label_0013:
            if (action.Skip == null)
            {
                goto Label_0023;
            }
            goto Label_002F;
        Label_0023:
            action.enabled = 1;
            goto Label_0042;
        Label_002F:
            action = action.NextAction;
        Label_0036:
            if ((action != null) != null)
            {
                goto Label_0013;
            }
        Label_0042:
            return;
        }

        public static GameObject FindActor(string actorID)
        {
            TacticsUnitController controller;
            if (string.IsNullOrEmpty(actorID) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            if (((controller = TacticsUnitController.FindByUnitID(actorID)) != null) == null)
            {
                goto Label_0027;
            }
            return controller.get_gameObject();
        Label_0027:
            if (((controller = TacticsUnitController.FindByUniqueName(actorID)) != null) == null)
            {
                goto Label_0041;
            }
            return controller.get_gameObject();
        Label_0041:
            return GameObjectID.FindGameObject(actorID);
        }

        public virtual bool Forward()
        {
            return 0;
        }

        public static string[] GetStreamResources(string[] pair)
        {
            string[] textArray1;
            if (pair == null)
            {
                goto Label_0046;
            }
            if (pair[0].IndexOf("VO_") == null)
            {
                goto Label_001F;
            }
            return new string[0];
        Label_001F:
            textArray1 = new string[] { pair[0] + ".awb", pair[0] + ".acb" };
            return textArray1;
        Label_0046:
            return new string[0];
        }

        public virtual string[] GetUnManagedAssetListData()
        {
            return null;
        }

        public static unsafe string[] GetUnManagedStreamAssets(string[] pair, bool isBGM)
        {
            string[] textArray2;
            string[] textArray1;
            string str;
            int num;
            int num2;
            if (pair == null)
            {
                goto Label_00D3;
            }
            if (isBGM == null)
            {
                goto Label_008D;
            }
            str = pair[0];
            if (str.IndexOf("BGM_") == -1)
            {
                goto Label_0036;
            }
            str = pair[0].Replace("BGM_", string.Empty);
        Label_0036:
            num2 = -1;
            if (int.TryParse(str, &num2) == null)
            {
                goto Label_00D3;
            }
            if (num2 < EventAction_BGM.DEMO_BGM_ST)
            {
                goto Label_00D3;
            }
            if (num2 > EventAction_BGM.DEMO_BGM_ED)
            {
                goto Label_00D3;
            }
            textArray1 = new string[] { "sound/BGM_" + str + ".awb", "sound/BGM_" + str + ".acb" };
            return textArray1;
            goto Label_00D3;
        Label_008D:
            if (pair[0].IndexOf("VO_") != -1)
            {
                goto Label_00D1;
            }
            textArray2 = new string[] { "sound/" + pair[0] + ".awb", "sound/" + pair[0] + ".acb" };
            return textArray2;
        Label_00D1:
            return null;
        Label_00D3:
            return null;
        }

        public virtual void GoToEndState()
        {
        }

        public static unsafe bool IsUnManagedAssets(string name, bool isBGM)
        {
            string str;
            int num;
            int num2;
            if (string.IsNullOrEmpty(name) == null)
            {
                goto Label_000D;
            }
            return 0;
        Label_000D:
            if (isBGM == null)
            {
                goto Label_0062;
            }
            str = name;
            if (str.IndexOf("BGM_") == -1)
            {
                goto Label_0039;
            }
            str = name.Replace("BGM_", string.Empty);
        Label_0039:
            num2 = -1;
            if (int.TryParse(str, &num2) == null)
            {
                goto Label_0060;
            }
            if (num2 < EventAction_BGM.DEMO_BGM_ST)
            {
                goto Label_0060;
            }
            if (num2 > EventAction_BGM.DEMO_BGM_ED)
            {
                goto Label_0060;
            }
            return 1;
        Label_0060:
            return 0;
        Label_0062:
            if (name.IndexOf("VO_") == -1)
            {
                goto Label_0075;
            }
            return 0;
        Label_0075:
            return 1;
        }

        public virtual void OnActivate()
        {
        }

        protected virtual void OnDestroy()
        {
        }

        public virtual void OnInactivate()
        {
        }

        protected static unsafe Vector3 PointToWorld(IntVector2 pt)
        {
            Vector3 vector;
            Vector3 vector2;
            &vector..ctor(((float) &pt.x) + 0.5f, 0f, ((float) &pt.y) + 0.5f);
            &vector.y = &GameUtility.RaycastGround(vector).y;
            return vector;
        }

        [DebuggerHidden]
        public virtual IEnumerator PreloadAssets()
        {
            <PreloadAssets>c__Iterator87 iterator;
            iterator = new <PreloadAssets>c__Iterator87();
            return iterator;
        }

        public virtual void PreStart()
        {
        }

        public virtual bool ReplaySkipButtonEnable()
        {
            return 1;
        }

        public virtual void SkipImmediate()
        {
        }

        public virtual void Update()
        {
        }

        public bool enabled
        {
            get
            {
                return this.mEnabled;
            }
            set
            {
                if (this.mEnabled == value)
                {
                    goto Label_002F;
                }
                this.mEnabled = value;
                if (this.mEnabled == null)
                {
                    goto Label_0029;
                }
                this.OnActivate();
                goto Label_002F;
            Label_0029:
                this.OnInactivate();
            Label_002F:
                return;
            }
        }

        public virtual bool IsPreloadAssets
        {
            get
            {
                return 0;
            }
        }

        protected Canvas ActiveCanvas
        {
            get
            {
                return EventScript.Canvas;
            }
        }

        public static bool IsLoading
        {
            get
            {
                return IsPreloading;
            }
        }

        [CompilerGenerated]
        private sealed class <PreloadAssets>c__Iterator87 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal int $PC;
            internal object $current;

            public <PreloadAssets>c__Iterator87()
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
                        goto Label_0021;

                    case 1:
                        goto Label_0034;
                }
                goto Label_003B;
            Label_0021:
                this.$current = null;
                this.$PC = 1;
                goto Label_003D;
            Label_0034:
                this.$PC = -1;
            Label_003B:
                return 0;
            Label_003D:
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

