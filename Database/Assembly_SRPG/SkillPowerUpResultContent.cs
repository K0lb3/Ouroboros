namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;
    using UnityEngine.UI;

    public class SkillPowerUpResultContent : MonoBehaviour
    {
        [SerializeField]
        private SkillPowerUpResultContentStatus statusBase;
        [SerializeField]
        private Text skillNameText;
        private List<SkillPowerUpResultContentStatus> contentsList;

        public SkillPowerUpResultContent()
        {
            this.contentsList = new List<SkillPowerUpResultContentStatus>();
            base..ctor();
            return;
        }

        public unsafe void SetData(Param param, List<ParamTypes> dispTypeList, List<ParamTypes> dispTypeListMul)
        {
            SkillPowerUpResultContentStatus status;
            List<SkillPowerUpResultContentStatus>.Enumerator enumerator;
            DispType type;
            IEnumerator<DispType> enumerator2;
            GameObject obj2;
            SkillPowerUpResultContentStatus status2;
            GameObject obj3;
            SkillPowerUpResultContentStatus status3;
            enumerator = this.contentsList.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0024;
            Label_0011:
                status = &enumerator.Current;
                Object.Destroy(status.get_gameObject());
            Label_0024:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0011;
                }
                goto Label_0041;
            }
            finally
            {
            Label_0035:
                ((List<SkillPowerUpResultContentStatus>.Enumerator) enumerator).Dispose();
            }
        Label_0041:
            this.contentsList.Clear();
            this.skillNameText.set_text(param.skilName);
            enumerator2 = param.GetAllBonusChangeType().GetEnumerator();
        Label_0069:
            try
            {
                goto Label_0165;
            Label_006E:
                type = enumerator2.Current;
                if (type.isScale != null)
                {
                    goto Label_00ED;
                }
                if (dispTypeList.Contains(type.type) == null)
                {
                    goto Label_00ED;
                }
                obj2 = Object.Instantiate<GameObject>(this.statusBase.get_gameObject());
                obj2.get_transform().SetParent(this.statusBase.get_transform().get_parent(), 0);
                status2 = obj2.GetComponent<SkillPowerUpResultContentStatus>();
                status2.SetData(param, type.type, 0);
                obj2.SetActive(1);
                this.contentsList.Add(status2);
            Label_00ED:
                if (type.isScale == null)
                {
                    goto Label_0165;
                }
                if (dispTypeListMul.Contains(type.type) == null)
                {
                    goto Label_0165;
                }
                obj3 = Object.Instantiate<GameObject>(this.statusBase.get_gameObject());
                obj3.get_transform().SetParent(this.statusBase.get_transform().get_parent(), 0);
                status3 = obj3.GetComponent<SkillPowerUpResultContentStatus>();
                status3.SetData(param, type.type, 1);
                obj3.SetActive(1);
                this.contentsList.Add(status3);
            Label_0165:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_006E;
                }
                goto Label_0180;
            }
            finally
            {
            Label_0175:
                if (enumerator2 != null)
                {
                    goto Label_0179;
                }
            Label_0179:
                enumerator2.Dispose();
            }
        Label_0180:
            return;
        }

        public void Start()
        {
            this.statusBase.get_gameObject().SetActive(0);
            return;
        }

        public class DispType
        {
            public bool isScale;
            public ParamTypes type;

            public DispType(ParamTypes inType, bool inIsScale)
            {
                base..ctor();
                this.type = inType;
                this.isScale = inIsScale;
                return;
            }
        }

        public class Param
        {
            public BaseStatus prevParam;
            public BaseStatus prevParamMul;
            public BaseStatus currentParam;
            public BaseStatus currentParamMul;
            public BaseStatus prevParamBonus;
            public BaseStatus prevParamBonusMul;
            public BaseStatus currentParamBonus;
            public BaseStatus currentParamBonusMul;
            private List<ParamTypes> typeList;
            private List<ParamTypes> typeListMul;
            [CompilerGenerated]
            private string <skilName>k__BackingField;

            public unsafe Param(SkillData groupSkill, ConceptCardData currentCardData, int levelCap, int awakeCountCap, int prevLevel, int prevAwakeCount, bool includeMaxPowerUp)
            {
                Array array;
                int num;
                int num2;
                ParamTypes types;
                int num3;
                int num4;
                int num5;
                int num6;
                this.typeList = new List<ParamTypes>();
                this.typeListMul = new List<ParamTypes>();
                base..ctor();
                this.skilName = groupSkill.Name;
                this.prevParam = new BaseStatus();
                this.prevParamMul = new BaseStatus();
                this.currentParam = new BaseStatus();
                this.currentParamMul = new BaseStatus();
                this.prevParamBonus = new BaseStatus();
                this.prevParamBonusMul = new BaseStatus();
                this.currentParamBonus = new BaseStatus();
                this.currentParamBonusMul = new BaseStatus();
                ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, currentCardData.Lv, &this.currentParam, &this.currentParamMul);
                ConceptCardParam.GetSkillAllStatus(groupSkill.SkillID, levelCap, prevLevel, &this.prevParam, &this.currentParamMul);
                GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, currentCardData.Lv, currentCardData.AwakeCount, &this.currentParamBonus, &this.currentParamBonusMul, includeMaxPowerUp);
                GetBonusStatus(groupSkill, currentCardData, levelCap, awakeCountCap, prevLevel, prevAwakeCount, &this.prevParamBonus, &this.currentParamBonusMul, 0);
                num = Enum.GetValues(typeof(ParamTypes)).Length;
                num2 = 0;
                goto Label_01A8;
            Label_0124:
                if (num2 == 2)
                {
                    goto Label_01A4;
                }
                types = (short) num2;
                num3 = this.prevParamBonus[types];
                num4 = this.currentParamBonus[types];
                if (num3 == num4)
                {
                    goto Label_0169;
                }
                this.typeList.Add(types);
            Label_0169:
                num5 = this.prevParamBonusMul[types];
                num6 = this.currentParamBonusMul[types];
                if (num5 == num6)
                {
                    goto Label_01A4;
                }
                this.typeListMul.Add(types);
            Label_01A4:
                num2 += 1;
            Label_01A8:
                if (num2 < num)
                {
                    goto Label_0124;
                }
                return;
            }

            [DebuggerHidden]
            public IEnumerable<SkillPowerUpResultContent.DispType> GetAllBonusChangeType()
            {
                <GetAllBonusChangeType>c__Iterator13C iteratorc;
                iteratorc = new <GetAllBonusChangeType>c__Iterator13C();
                iteratorc.<>f__this = this;
                iteratorc.$PC = -2;
                return iteratorc;
            }

            public int GetBonusChangeTypeNum(bool scalingStatus)
            {
                if (scalingStatus == null)
                {
                    goto Label_0012;
                }
                return this.typeListMul.Count;
            Label_0012:
                return this.typeList.Count;
            }

            private static unsafe void GetBonusStatus(SkillData groupSkill, ConceptCardData currentCardData, int levelCap, int awakeCountCap, int level, int awakeCount, ref BaseStatus bonusAdd, ref BaseStatus bonusScale, bool includeMaxPowerUp)
            {
                ConceptCardEquipEffect effect;
                List<ConceptCardEquipEffect>.Enumerator enumerator;
                BaseStatus status;
                BaseStatus status2;
                if (currentCardData.EquipEffects != null)
                {
                    goto Label_000C;
                }
                return;
            Label_000C:
                enumerator = currentCardData.EquipEffects.GetEnumerator();
            Label_0018:
                try
                {
                    goto Label_00A6;
                Label_001D:
                    effect = &enumerator.Current;
                    if (effect.CardSkill != null)
                    {
                        goto Label_0035;
                    }
                    goto Label_00A6;
                Label_0035:
                    if ((effect.CardSkill.Name == groupSkill.Name) == null)
                    {
                        goto Label_00A6;
                    }
                    effect.GetAddCardSkillBuffStatusAwake(awakeCount, awakeCountCap, bonusAdd, bonusScale);
                    if (includeMaxPowerUp == null)
                    {
                        goto Label_00B2;
                    }
                    if (level != levelCap)
                    {
                        goto Label_00B2;
                    }
                    if (awakeCount != awakeCountCap)
                    {
                        goto Label_00B2;
                    }
                    status = new BaseStatus();
                    status2 = new BaseStatus();
                    effect.GetAddCardSkillBuffStatusLvMax(level, levelCap, awakeCount, &status, &status2);
                    *(bonusAdd).Add(status);
                    *(bonusScale).Add(status2);
                    goto Label_00B2;
                Label_00A6:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_001D;
                    }
                Label_00B2:
                    goto Label_00C3;
                }
                finally
                {
                Label_00B7:
                    ((List<ConceptCardEquipEffect>.Enumerator) enumerator).Dispose();
                }
            Label_00C3:
                return;
            }

            public bool IsBonusParamChanged()
            {
                return ((this.typeListMul.Count > 0) ? 1 : (this.typeList.Count > 0));
            }

            public string skilName
            {
                [CompilerGenerated]
                get
                {
                    return this.<skilName>k__BackingField;
                }
                [CompilerGenerated]
                private set
                {
                    this.<skilName>k__BackingField = value;
                    return;
                }
            }

            [CompilerGenerated]
            private sealed class <GetAllBonusChangeType>c__Iterator13C : IEnumerator, IDisposable, IEnumerable, IEnumerable<SkillPowerUpResultContent.DispType>, IEnumerator<SkillPowerUpResultContent.DispType>
            {
                internal Array <enumValues>__0;
                internal int <typeLength>__1;
                internal int <typeI>__2;
                internal ParamTypes <type>__3;
                internal int $PC;
                internal SkillPowerUpResultContent.DispType $current;
                internal SkillPowerUpResultContent.Param <>f__this;

                public <GetAllBonusChangeType>c__Iterator13C()
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
                            goto Label_00A9;

                        case 2:
                            goto Label_00E2;
                    }
                    goto Label_0108;
                Label_0025:
                    this.<enumValues>__0 = Enum.GetValues(typeof(ParamTypes));
                    this.<typeLength>__1 = this.<enumValues>__0.Length;
                    this.<typeI>__2 = 0;
                    goto Label_00F0;
                Label_0057:
                    if (this.<typeI>__2 == 2)
                    {
                        goto Label_00E2;
                    }
                    this.<type>__3 = (short) this.<typeI>__2;
                    if (this.<>f__this.typeList.Contains(this.<type>__3) == null)
                    {
                        goto Label_00A9;
                    }
                    this.$current = new SkillPowerUpResultContent.DispType(this.<type>__3, 0);
                    this.$PC = 1;
                    goto Label_010A;
                Label_00A9:
                    if (this.<>f__this.typeListMul.Contains(this.<type>__3) == null)
                    {
                        goto Label_00E2;
                    }
                    this.$current = new SkillPowerUpResultContent.DispType(this.<type>__3, 1);
                    this.$PC = 2;
                    goto Label_010A;
                Label_00E2:
                    this.<typeI>__2 += 1;
                Label_00F0:
                    if (this.<typeI>__2 < this.<typeLength>__1)
                    {
                        goto Label_0057;
                    }
                    this.$PC = -1;
                Label_0108:
                    return 0;
                Label_010A:
                    return 1;
                    return flag;
                }

                [DebuggerHidden]
                public void Reset()
                {
                    throw new NotSupportedException();
                }

                [DebuggerHidden]
                unsafe IEnumerator<SkillPowerUpResultContent.DispType> IEnumerable<SkillPowerUpResultContent.DispType>.GetEnumerator()
                {
                    SkillPowerUpResultContent.Param.<GetAllBonusChangeType>c__Iterator13C iteratorc;
                    if (Interlocked.CompareExchange(&this.$PC, 0, -2) != -2)
                    {
                        goto Label_0014;
                    }
                    return this;
                Label_0014:
                    iteratorc = new SkillPowerUpResultContent.Param.<GetAllBonusChangeType>c__Iterator13C();
                    iteratorc.<>f__this = this.<>f__this;
                    return iteratorc;
                }

                [DebuggerHidden]
                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.System.Collections.Generic.IEnumerable<SRPG.SkillPowerUpResultContent.DispType>.GetEnumerator();
                }

                SkillPowerUpResultContent.DispType IEnumerator<SkillPowerUpResultContent.DispType>.Current
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
}

