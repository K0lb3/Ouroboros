namespace SRPG
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Pin(100, "全ページ表示終了", 1, 100), Pin(0, "タップ入力", 0, 0), Pin(0x65, "表示コンテンツなし", 1, 0x65)]
    public class ConceptCardMaxPowerUp : MonoBehaviour, IFlowInterface
    {
        private const int PIN_PAGE_NEXT = 0;
        private const int PIN_FINISHED = 100;
        private const int PIN_NO_CONTENTS = 0x65;
        private const string mGroupMaxAbilityResultPrefabPath = "UI/ConceptCardMaxPowerUpVisionAbility";
        [SerializeField]
        private Transform resultRoot;
        [SerializeField]
        private ConceptCardTrustMaster conceptCardTrustMaster;
        private int prevAwakeCount;
        private ConceptCardData currentCardData;
        private SkillPowerUpResult skillPowerUpResult;
        private AbilityPowerUpResult abilityPowerUpResult;
        private bool isEnd;

        public ConceptCardMaxPowerUp()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            int num;
            if (pinID == null)
            {
                goto Label_000D;
            }
            goto Label_0018;
        Label_000D:
            this.CheckPages();
        Label_0018:
            return;
        }

        private void CheckPages()
        {
            if (this.isEnd == null)
            {
                goto Label_000C;
            }
            return;
        Label_000C:
            if ((this.skillPowerUpResult != null) == null)
            {
                goto Label_0085;
            }
            if (this.skillPowerUpResult.IsEnd == null)
            {
                goto Label_0075;
            }
            if (this.HasAbilityPowerUp() != null)
            {
                goto Label_004C;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            this.isEnd = 1;
            goto Label_0059;
        Label_004C:
            base.StartCoroutine(this.CreateAbilityResultCroutine());
        Label_0059:
            Object.Destroy(this.skillPowerUpResult.get_gameObject());
            this.skillPowerUpResult = null;
            goto Label_0080;
        Label_0075:
            this.skillPowerUpResult.ApplyContent();
        Label_0080:
            goto Label_00D9;
        Label_0085:
            if ((this.abilityPowerUpResult != null) == null)
            {
                goto Label_00CA;
            }
            if (this.abilityPowerUpResult.IsEnd == null)
            {
                goto Label_00BA;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            this.isEnd = 1;
            goto Label_00C5;
        Label_00BA:
            this.abilityPowerUpResult.ApplyContent();
        Label_00C5:
            goto Label_00D9;
        Label_00CA:
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            this.isEnd = 1;
        Label_00D9:
            return;
        }

        [DebuggerHidden]
        private IEnumerator CreateAbilityResultCroutine()
        {
            <CreateAbilityResultCroutine>c__Iterator103 iterator;
            iterator = new <CreateAbilityResultCroutine>c__Iterator103();
            iterator.<>f__this = this;
            return iterator;
        }

        private unsafe bool HasAbilityPowerUp()
        {
            bool flag;
            ConceptCardEquipEffect effect;
            List<ConceptCardEquipEffect>.Enumerator enumerator;
            flag = 0;
            if (this.currentCardData == null)
            {
                goto Label_006A;
            }
            if (this.currentCardData.EquipEffects == null)
            {
                goto Label_006A;
            }
            enumerator = this.currentCardData.EquipEffects.GetEnumerator();
        Label_002E:
            try
            {
                goto Label_004D;
            Label_0033:
                effect = &enumerator.Current;
                if (effect.IsExistAbilityLvMax == null)
                {
                    goto Label_004D;
                }
                flag = 1;
                goto Label_0059;
            Label_004D:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0033;
                }
            Label_0059:
                goto Label_006A;
            }
            finally
            {
            Label_005E:
                ((List<ConceptCardEquipEffect>.Enumerator) enumerator).Dispose();
            }
        Label_006A:
            return flag;
        }

        public void SetData(SkillPowerUpResult inSkillPowerUpResult, ConceptCardData inCurrentCardData, int inPrevAwakeCount, int inPrevLevel)
        {
            this.prevAwakeCount = inPrevAwakeCount;
            this.skillPowerUpResult = inSkillPowerUpResult;
            this.currentCardData = inCurrentCardData;
            this.skillPowerUpResult.SetData(this.currentCardData, this.prevAwakeCount, inPrevLevel, 1);
            this.conceptCardTrustMaster.SetData(this.currentCardData);
            if (this.skillPowerUpResult.IsEnd == null)
            {
                goto Label_0091;
            }
            Object.Destroy(this.skillPowerUpResult.get_gameObject());
            this.skillPowerUpResult = null;
            if (this.HasAbilityPowerUp() != null)
            {
                goto Label_007F;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_008C;
        Label_007F:
            base.StartCoroutine(this.CreateAbilityResultCroutine());
        Label_008C:
            goto Label_0097;
        Label_0091:
            this.CheckPages();
        Label_0097:
            return;
        }

        public Transform ResultRoot
        {
            get
            {
                return this.resultRoot;
            }
        }

        [CompilerGenerated]
        private sealed class <CreateAbilityResultCroutine>c__Iterator103 : IEnumerator, IDisposable, IEnumerator<object>
        {
            internal LoadRequest <req>__0;
            internal GameObject <resultObj>__1;
            internal int $PC;
            internal object $current;
            internal ConceptCardMaxPowerUp <>f__this;

            public <CreateAbilityResultCroutine>c__Iterator103()
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
                        goto Label_0065;
                }
                goto Label_00EF;
            Label_0021:
                this.<req>__0 = null;
                this.<req>__0 = AssetManager.LoadAsync<GameObject>("UI/ConceptCardMaxPowerUpVisionAbility");
                if (this.<req>__0.isDone != null)
                {
                    goto Label_0065;
                }
                this.$current = this.<req>__0.StartCoroutine();
                this.$PC = 1;
                goto Label_00F1;
            Label_0065:
                this.<resultObj>__1 = Object.Instantiate(this.<req>__0.asset) as GameObject;
                this.<resultObj>__1.get_transform().SetParent(this.<>f__this.ResultRoot, 0);
                this.<>f__this.abilityPowerUpResult = this.<resultObj>__1.GetComponent<AbilityPowerUpResult>();
                this.<>f__this.abilityPowerUpResult.SetData(this.<>f__this.currentCardData, this.<>f__this.prevAwakeCount);
                this.<>f__this.abilityPowerUpResult.ApplyContent();
                this.$PC = -1;
            Label_00EF:
                return 0;
            Label_00F1:
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

