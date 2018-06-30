namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardTrustMasterReward : MonoBehaviour
    {
        [SerializeField]
        private Text mItemName;
        [SerializeField]
        private Text mItemAmount;
        [SerializeField]
        private ItemIcon mItemIcon;
        [SerializeField]
        private ArtifactIcon mArtifactIcon;
        [SerializeField]
        private ConceptCardIcon mCardIcon;
        [SerializeField]
        private Sprite CoinFrame;
        [SerializeField]
        private Sprite GoldFrame;

        public ConceptCardTrustMasterReward()
        {
            base..ctor();
            return;
        }

        public void SetArtifact(ConceptCardTrustRewardItemParam reward_item)
        {
            ArtifactParam param;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(reward_item.iname);
            this.mItemName.set_text(param.name);
            DataSource.Bind<ArtifactParam>(base.get_gameObject(), param);
            if ((this.mArtifactIcon != null) == null)
            {
                goto Label_0071;
            }
            this.mArtifactIcon.get_gameObject().SetActive(1);
            this.mArtifactIcon.UpdateValue();
        Label_0071:
            return;
        }

        public void SetConceptCard(ConceptCardTrustRewardItemParam reward_item)
        {
            ConceptCardParam param;
            ConceptCardData data;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            param = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(reward_item.iname);
            this.mItemName.set_text(param.name);
            DataSource.Bind<ConceptCardParam>(base.get_gameObject(), param);
            if ((this.mCardIcon != null) == null)
            {
                goto Label_007E;
            }
            this.mCardIcon.get_gameObject().SetActive(1);
            data = ConceptCardData.CreateConceptCardDataForDisplay(param.iname);
            this.mCardIcon.Setup(data);
        Label_007E:
            return;
        }

        public unsafe void SetData(ConceptCardData data)
        {
            ConceptCardTrustRewardItemParam param;
            eRewardType type;
            param = data.GetReward();
            if (param != null)
            {
                goto Label_000E;
            }
            return;
        Label_000E:
            switch ((param.reward_type - 1))
            {
                case 0:
                    goto Label_0036;

                case 1:
                    goto Label_0042;

                case 2:
                    goto Label_005A;

                case 3:
                    goto Label_005A;

                case 4:
                    goto Label_004E;
            }
            goto Label_005A;
        Label_0036:
            this.SetItem(param);
            goto Label_005A;
        Label_0042:
            this.SetArtifact(param);
            goto Label_005A;
        Label_004E:
            this.SetConceptCard(param);
        Label_005A:
            this.mItemAmount.set_text(&param.reward_num.ToString());
            return;
        }

        public void SetItem(ConceptCardTrustRewardItemParam reward_item)
        {
            ItemParam param;
            if (string.IsNullOrEmpty(reward_item.iname) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            param = MonoSingleton<GameManager>.Instance.GetItemParam(reward_item.iname);
            this.mItemName.set_text(param.name);
            DataSource.Bind<ItemParam>(base.get_gameObject(), param);
            if ((this.mItemIcon != null) == null)
            {
                goto Label_006C;
            }
            this.mItemIcon.get_gameObject().SetActive(1);
            this.mItemIcon.UpdateValue();
        Label_006C:
            return;
        }
    }
}

