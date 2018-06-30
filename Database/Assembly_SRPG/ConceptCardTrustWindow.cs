namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    [Pin(12, "トラスト報酬なし", 1, 12), Pin(0, "詳細表示", 0, 0), Pin(10, "アイテム詳細を表示", 1, 10), Pin(11, "武具詳細を表示", 1, 11), Pin(13, "念装詳細を表示", 1, 13)]
    public class ConceptCardTrustWindow : ConceptCardDetailBase, IFlowInterface
    {
        private const int PIN_SHOW_DETAIL = 0;
        private const int PIN_DETAIL_ITEM = 10;
        private const int PIN_DETAIL_ARIFACT = 11;
        private const int PIN_DETAIL_NON = 12;
        private const int PIN_DETAIL_CONCEPT_CARD = 13;
        [SerializeField]
        private GameObject mConceptCardPrefab;
        private ConceptCardEquipDetail mConceptCardEquipDetail;

        public ConceptCardTrustWindow()
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
            this.ShowDetail();
        Label_0018:
            return;
        }

        public bool SetArtifact(ConceptCardTrustRewardItemParam reward_item)
        {
            ArtifactParam param;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            ArtifactDetailWindow.SetArtifactParam(MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname));
            FlowNode_GameObject.ActivateOutputLinks(this, 11);
            return 1;
        }

        public bool SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
        {
            ConceptCardParam param;
            ConceptCardData data;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            data = ConceptCardData.CreateConceptCardDataForDisplay(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname).iname);
            GlobalVars.SelectedConceptCardData.Set(data);
            FlowNode_GameObject.ActivateOutputLinks(this, 13);
            return 1;
        }

        public bool SetItem(ConceptCardTrustRewardItemParam reward_item)
        {
            ItemParam param;
            Transform transform;
            ItemData data;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0012;
            }
            return 0;
        Label_0012:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(reward_item.iname);
            transform = base.get_transform().get_parent();
            if ((transform != null) == null)
            {
                goto Label_0069;
            }
            DataSource.Bind<ItemParam>(transform.get_gameObject(), param);
            data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(param.iname);
            DataSource.Bind<ItemData>(transform.get_gameObject(), data);
        Label_0069:
            FlowNode_GameObject.ActivateOutputLinks(this, 10);
            return 1;
        }

        public void ShowDetail()
        {
            ConceptCardTrustRewardItemParam param;
            bool flag;
            eRewardType type;
            base.mConceptCardData = ConceptCardManager.Instance.SelectedConceptCardData;
            if (base.mConceptCardData != null)
            {
                goto Label_0024;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            return;
        Label_0024:
            param = base.mConceptCardData.GetReward();
            if (param != null)
            {
                goto Label_003F;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
            return;
        Label_003F:
            flag = 0;
            switch ((param.reward_type - 1))
            {
                case 0:
                    goto Label_0069;

                case 1:
                    goto Label_0076;

                case 2:
                    goto Label_0090;

                case 3:
                    goto Label_0090;

                case 4:
                    goto Label_0083;
            }
            goto Label_0090;
        Label_0069:
            flag = this.SetItem(param);
            goto Label_0090;
        Label_0076:
            flag = this.SetArtifact(param);
            goto Label_0090;
        Label_0083:
            flag = this.SetConceptCard(param);
        Label_0090:
            if (flag != null)
            {
                goto Label_009E;
            }
            FlowNode_GameObject.ActivateOutputLinks(this, 12);
        Label_009E:
            return;
        }
    }
}

