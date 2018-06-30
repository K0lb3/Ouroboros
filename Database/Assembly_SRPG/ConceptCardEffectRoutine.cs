namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using UnityEngine;

    public class ConceptCardEffectRoutine
    {
        private const string mLeveUpPrefabPath = "UI/ConceptCardLevelup";
        private const string mAwakePrefabPath = "UI/ConceptCardLimitUp";
        private const string mTrustMasterPrefabPath = "UI/ConceptCardTrustMaster";
        private const string mTrustMasterRewardPrefabPath = "UI/ConceptCardTrustMasterReward";
        private const string mGroupSkillPowerUpPrefabPath = "UI/ConceptCardSkillPowerUp";
        private const string mGroupMaxPowerUpPrefabPath = "UI/ConceptCardMaxPowerUp";
        private const string mGroupMaxPowerResultPrefabPath = "UI/ConceptCardMaxPowerUpGroupSkillList";

        public ConceptCardEffectRoutine()
        {
            base..ctor();
            return;
        }

        [DebuggerHidden]
        private IEnumerator EffectRoutine(Canvas overlayCanvas, string path, EffectType type, ConceptCardData data, EffectAltData altData)
        {
            <EffectRoutine>c__Iterator101 iterator;
            iterator = new <EffectRoutine>c__Iterator101();
            iterator.path = path;
            iterator.overlayCanvas = overlayCanvas;
            iterator.type = type;
            iterator.data = data;
            iterator.altData = altData;
            iterator.<$>path = path;
            iterator.<$>overlayCanvas = overlayCanvas;
            iterator.<$>type = type;
            iterator.<$>data = data;
            iterator.<$>altData = altData;
            return iterator;
        }

        [DebuggerHidden]
        public IEnumerator PlayAwake(Canvas overlayCanvas)
        {
            <PlayAwake>c__IteratorFC rfc;
            rfc = new <PlayAwake>c__IteratorFC();
            rfc.overlayCanvas = overlayCanvas;
            rfc.<$>overlayCanvas = overlayCanvas;
            rfc.<>f__this = this;
            return rfc;
        }

        [DebuggerHidden]
        public IEnumerator PlayGroupSkilMaxPowerUp(Canvas overlayCanvas, ConceptCardData data, EffectAltData altData)
        {
            <PlayGroupSkilMaxPowerUp>c__Iterator100 iterator;
            iterator = new <PlayGroupSkilMaxPowerUp>c__Iterator100();
            iterator.overlayCanvas = overlayCanvas;
            iterator.data = data;
            iterator.altData = altData;
            iterator.<$>overlayCanvas = overlayCanvas;
            iterator.<$>data = data;
            iterator.<$>altData = altData;
            iterator.<>f__this = this;
            return iterator;
        }

        [DebuggerHidden]
        public IEnumerator PlayGroupSkilPowerUp(Canvas overlayCanvas, ConceptCardData data, EffectAltData altData)
        {
            <PlayGroupSkilPowerUp>c__IteratorFF rff;
            rff = new <PlayGroupSkilPowerUp>c__IteratorFF();
            rff.overlayCanvas = overlayCanvas;
            rff.data = data;
            rff.altData = altData;
            rff.<$>overlayCanvas = overlayCanvas;
            rff.<$>data = data;
            rff.<$>altData = altData;
            rff.<>f__this = this;
            return rff;
        }

        [DebuggerHidden]
        public IEnumerator PlayLevelup(Canvas overlayCanvas)
        {
            <PlayLevelup>c__IteratorFB rfb;
            rfb = new <PlayLevelup>c__IteratorFB();
            rfb.overlayCanvas = overlayCanvas;
            rfb.<$>overlayCanvas = overlayCanvas;
            rfb.<>f__this = this;
            return rfb;
        }

        [DebuggerHidden]
        public IEnumerator PlayTrustMaster(Canvas overlayCanvas, ConceptCardData data)
        {
            <PlayTrustMaster>c__IteratorFD rfd;
            rfd = new <PlayTrustMaster>c__IteratorFD();
            rfd.overlayCanvas = overlayCanvas;
            rfd.data = data;
            rfd.<$>overlayCanvas = overlayCanvas;
            rfd.<$>data = data;
            rfd.<>f__this = this;
            return rfd;
        }

        [DebuggerHidden]
        public IEnumerator PlayTrustMasterReward(Canvas overlayCanvas, ConceptCardData data)
        {
            <PlayTrustMasterReward>c__IteratorFE rfe;
            rfe = new <PlayTrustMasterReward>c__IteratorFE();
            rfe.overlayCanvas = overlayCanvas;
            rfe.data = data;
            rfe.<$>overlayCanvas = overlayCanvas;
            rfe.<$>data = data;
            rfe.<>f__this = this;
            return rfe;
        }

        [CompilerGenerated]
        private sealed class <EffectRoutine>c__Iterator101 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal string path;
            internal GameObject <go>__1;
            internal Canvas overlayCanvas;
            internal ConceptCardEffectRoutine.EffectType type;
            internal ConceptCardTrustMaster <tm>__2;
            internal ConceptCardData data;
            internal ConceptCardTrustMasterReward <tmr>__3;
            internal ConceptCardSkillPowerUp <ccspw>__4;
            internal GameObject <resultObj>__5;
            internal ConceptCardEffectRoutine.EffectAltData altData;
            internal ConceptCardMaxPowerUp <ccmpw>__6;
            internal GameObject <maxResultObj>__7;
            internal int $PC;
            internal object $current;
            internal string <$>path;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardEffectRoutine.EffectType <$>type;
            internal ConceptCardData <$>data;
            internal ConceptCardEffectRoutine.EffectAltData <$>altData;

            public <EffectRoutine>c__Iterator101()
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
                ConceptCardEffectRoutine.EffectType type;
                bool flag;
                num = this.$PC;
                this.$PC = -1;
                switch (num)
                {
                    case 0:
                        goto Label_002D;

                    case 1:
                        goto Label_0072;

                    case 2:
                        goto Label_0169;

                    case 3:
                        goto Label_0225;

                    case 4:
                        goto Label_02AB;
                }
                goto Label_02C3;
            Label_002D:
                this.<req>__0 = null;
                this.<req>__0 = AssetManager.LoadAsync<GameObject>(this.path);
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0072;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_02C5;
            Label_0072:
                this.<go>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<go>__1.get_transform().SetParent(this.overlayCanvas.get_transform(), 0);
                switch ((this.type - 1))
                {
                    case 0:
                        goto Label_00CD;

                    case 1:
                        goto Label_00F4;

                    case 2:
                        goto Label_011B;

                    case 3:
                        goto Label_01D7;
                }
                goto Label_0293;
            Label_00CD:
                this.<tm>__2 = this.<go>__1.GetComponentInChildren<ConceptCardTrustMaster>();
                this.<tm>__2.SetData(this.data);
                goto Label_0293;
            Label_00F4:
                this.<tmr>__3 = this.<go>__1.GetComponentInChildren<ConceptCardTrustMasterReward>();
                this.<tmr>__3.SetData(this.data);
                goto Label_0293;
            Label_011B:
                this.<ccspw>__4 = this.<go>__1.GetComponentInChildren<ConceptCardSkillPowerUp>();
                this.<req>__0 = AssetManager.LoadAsync<GameObject>("UI/ConceptCardMaxPowerUpGroupSkillList");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0169;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 2;
                goto Label_02C5;
            Label_0169:
                this.<resultObj>__5 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<resultObj>__5.get_transform().SetParent(this.<ccspw>__4.ResultRoot, 0);
                this.<ccspw>__4.SetData(this.<resultObj>__5.GetComponent<SkillPowerUpResult>(), this.data, this.altData.prevAwakeCount, this.altData.prevLevel);
                goto Label_0293;
            Label_01D7:
                this.<ccmpw>__6 = this.<go>__1.GetComponentInChildren<ConceptCardMaxPowerUp>();
                this.<req>__0 = AssetManager.LoadAsync<GameObject>("UI/ConceptCardMaxPowerUpGroupSkillList");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0225;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 3;
                goto Label_02C5;
            Label_0225:
                this.<maxResultObj>__7 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<maxResultObj>__7.get_transform().SetParent(this.<ccmpw>__6.ResultRoot, 0);
                this.<ccmpw>__6.SetData(this.<maxResultObj>__7.GetComponent<SkillPowerUpResult>(), this.data, this.altData.prevAwakeCount, this.altData.prevLevel);
            Label_0293:
                goto Label_02AB;
            Label_0298:
                this.$current = null;
                this.$PC = 4;
                goto Label_02C5;
            Label_02AB:
                if ((this.<go>__1 != null) != null)
                {
                    goto Label_0298;
                }
                this.$PC = -1;
            Label_02C3:
                return 0;
            Label_02C5:
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

        [CompilerGenerated]
        private sealed class <PlayAwake>c__IteratorFC : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayAwake>c__IteratorFC()
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
                        goto Label_004C;
                }
                goto Label_0053;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardLimitUp", 0, null, null);
                this.$PC = 1;
                goto Label_0055;
            Label_004C:
                this.$PC = -1;
            Label_0053:
                return 0;
            Label_0055:
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

        [CompilerGenerated]
        private sealed class <PlayGroupSkilMaxPowerUp>c__Iterator100 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal ConceptCardData data;
            internal ConceptCardEffectRoutine.EffectAltData altData;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardData <$>data;
            internal ConceptCardEffectRoutine.EffectAltData <$>altData;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayGroupSkilMaxPowerUp>c__Iterator100()
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
                        goto Label_0056;
                }
                goto Label_005D;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardMaxPowerUp", 4, this.data, this.altData);
                this.$PC = 1;
                goto Label_005F;
            Label_0056:
                this.$PC = -1;
            Label_005D:
                return 0;
            Label_005F:
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

        [CompilerGenerated]
        private sealed class <PlayGroupSkilPowerUp>c__IteratorFF : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal ConceptCardData data;
            internal ConceptCardEffectRoutine.EffectAltData altData;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardData <$>data;
            internal ConceptCardEffectRoutine.EffectAltData <$>altData;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayGroupSkilPowerUp>c__IteratorFF()
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
                        goto Label_0056;
                }
                goto Label_005D;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardSkillPowerUp", 3, this.data, this.altData);
                this.$PC = 1;
                goto Label_005F;
            Label_0056:
                this.$PC = -1;
            Label_005D:
                return 0;
            Label_005F:
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

        [CompilerGenerated]
        private sealed class <PlayLevelup>c__IteratorFB : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayLevelup>c__IteratorFB()
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
                        goto Label_004C;
                }
                goto Label_0053;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardLevelup", 0, null, null);
                this.$PC = 1;
                goto Label_0055;
            Label_004C:
                this.$PC = -1;
            Label_0053:
                return 0;
            Label_0055:
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

        [CompilerGenerated]
        private sealed class <PlayTrustMaster>c__IteratorFD : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal ConceptCardData data;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardData <$>data;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayTrustMaster>c__IteratorFD()
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
                        goto Label_0051;
                }
                goto Label_0058;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardTrustMaster", 1, this.data, null);
                this.$PC = 1;
                goto Label_005A;
            Label_0051:
                this.$PC = -1;
            Label_0058:
                return 0;
            Label_005A:
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

        [CompilerGenerated]
        private sealed class <PlayTrustMasterReward>c__IteratorFE : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal Canvas overlayCanvas;
            internal ConceptCardData data;
            internal int $PC;
            internal object $current;
            internal Canvas <$>overlayCanvas;
            internal ConceptCardData <$>data;
            internal ConceptCardEffectRoutine <>f__this;

            public <PlayTrustMasterReward>c__IteratorFE()
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
                        goto Label_0051;
                }
                goto Label_0058;
            Label_0021:
                this.$current = this.<>f__this.EffectRoutine(this.overlayCanvas, "UI/ConceptCardTrustMasterReward", 2, this.data, null);
                this.$PC = 1;
                goto Label_005A;
            Label_0051:
                this.$PC = -1;
            Label_0058:
                return 0;
            Label_005A:
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

        public class EffectAltData
        {
            public int prevAwakeCount;
            public int prevLevel;

            public EffectAltData()
            {
                base..ctor();
                return;
            }
        }

        public enum EffectType
        {
            DEFAULT,
            TRUST_MASTER,
            TRUST_MASTER_REWARD,
            GROUP_SKILL_POWER_UP,
            GROUP_SKILL_MAX_POWER_UP
        }
    }
}

