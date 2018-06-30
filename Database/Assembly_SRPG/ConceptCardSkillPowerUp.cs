namespace SRPG
{
    using System;
    using UnityEngine;

    [Pin(0x65, "表示コンテンツなし", 1, 0x65), Pin(100, "全ページ表示終了", 1, 100), Pin(0, "タップ入力", 0, 0)]
    public class ConceptCardSkillPowerUp : MonoBehaviour, IFlowInterface
    {
        private const int PIN_PAGE_NEXT = 0;
        private const int PIN_FINISHED = 100;
        private const int PIN_NO_CONTENTS = 0x65;
        [SerializeField]
        private Transform resultRoot;
        private SkillPowerUpResult skillPowerUpResult;

        public ConceptCardSkillPowerUp()
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
            if (this.skillPowerUpResult.IsEnd == null)
            {
                goto Label_001D;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0028;
        Label_001D:
            this.skillPowerUpResult.ApplyContent();
        Label_0028:
            return;
        }

        public void SetData(SkillPowerUpResult inSkillPowerUpResult, ConceptCardData currentCardData, int prevAwakeCount, int prevLevel)
        {
            this.skillPowerUpResult = inSkillPowerUpResult;
            this.skillPowerUpResult.SetData(currentCardData, prevAwakeCount, prevLevel, 0);
            if (this.skillPowerUpResult.IsEnd == null)
            {
                goto Label_0045;
            }
            this.skillPowerUpResult.get_gameObject().SetActive(0);
            FlowNode_GameObject.ActivateOutputLinks(this, 0x65);
            goto Label_0050;
        Label_0045:
            this.skillPowerUpResult.ApplyContent();
        Label_0050:
            return;
        }

        public Transform ResultRoot
        {
            get
            {
                return this.resultRoot;
            }
        }
    }
}

