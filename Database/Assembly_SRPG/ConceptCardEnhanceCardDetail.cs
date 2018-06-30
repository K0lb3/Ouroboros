namespace SRPG
{
    using GR;
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;

    [Pin(1, "イメージが閉じられた", 0, 1), Pin(100, "イメージ拡大設定完了", 1, 100), Pin(0, "イメージ拡大", 0, 0)]
    public class ConceptCardEnhanceCardDetail : MonoBehaviour, IFlowInterface
    {
        public const int PIN_OPEN_IN_IMAGE = 0;
        public const int PIN_CLOSE_IMAGE = 1;
        public const int PIN_OPEN_OUT_IMAGE = 100;
        [SerializeField]
        private RawImage mIllustImage;
        [SerializeField]
        private ImageArray mIllustFrame;
        [SerializeField]
        private Text mCardNameText;
        [SerializeField]
        private Text mFlavorText;
        [SerializeField]
        private StarGauge mStarGauge;
        private ConceptCardData mConceptCardData;

        public ConceptCardEnhanceCardDetail()
        {
            base..ctor();
            return;
        }

        public void Activated(int pinID)
        {
            ConceptCardManager manager;
            int num;
            if ((ConceptCardManager.Instance == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            manager = ConceptCardManager.Instance;
            num = pinID;
            if (num == null)
            {
                goto Label_002B;
            }
            if (num == 1)
            {
                goto Label_0044;
            }
            goto Label_0050;
        Label_002B:
            manager.SelectedConceptCardMaterialData = this.mConceptCardData;
            FlowNode_GameObject.ActivateOutputLinks(this, 100);
            goto Label_0050;
        Label_0044:
            manager.SelectedConceptCardMaterialData = null;
        Label_0050:
            return;
        }

        private void SetFlavorTextText()
        {
            string str;
            str = this.mConceptCardData.Param.GetLocalizedTextFlavor();
            this.SetText(this.mFlavorText, str);
            return;
        }

        private void SetText(Text text, string str)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(str);
        Label_0013:
            return;
        }

        private void Start()
        {
            SerializeValueList list;
            ConceptCardIcon icon;
            string str;
            string str2;
            int num;
            Scrollbar[] scrollbarArray;
            Scrollbar scrollbar;
            Scrollbar[] scrollbarArray2;
            int num2;
            if (FlowNode_ButtonEvent.currentValue != null)
            {
                goto Label_000B;
            }
            return;
        Label_000B:
            list = FlowNode_ButtonEvent.currentValue as SerializeValueList;
            if (list != null)
            {
                goto Label_001D;
            }
            return;
        Label_001D:
            icon = list.GetComponent<ConceptCardIcon>("_self");
            if ((icon == null) == null)
            {
                goto Label_0036;
            }
            return;
        Label_0036:
            this.mConceptCardData = icon.ConceptCard;
            if (this.mConceptCardData != null)
            {
                goto Label_004E;
            }
            return;
        Label_004E:
            if ((this.mIllustImage != null) == null)
            {
                goto Label_00A3;
            }
            str = AssetPath.ConceptCard(this.mConceptCardData.Param);
            str2 = Path.GetFileName(str);
            if ((this.mIllustImage.get_mainTexture().get_name() != str2) == null)
            {
                goto Label_00A3;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mIllustImage, str);
        Label_00A3:
            if ((this.mIllustFrame != null) == null)
            {
                goto Label_00F1;
            }
            num = Mathf.Min(Mathf.Max(this.mConceptCardData.Rarity, 0), ((int) this.mIllustFrame.Images.Length) - 1);
            this.mIllustFrame.ImageIndex = num;
        Label_00F1:
            this.SetText(this.mCardNameText, this.mConceptCardData.Param.name);
            this.SetFlavorTextText();
            if ((this.mStarGauge != null) == null)
            {
                goto Label_015E;
            }
            this.mStarGauge.Max = this.mConceptCardData.Rarity + 1;
            this.mStarGauge.Value = this.mConceptCardData.Rarity + 1;
        Label_015E:
            scrollbarArray2 = base.GetComponentsInChildren<Scrollbar>();
            num2 = 0;
            goto Label_018B;
        Label_0172:
            scrollbar = scrollbarArray2[num2];
            scrollbar.set_value(1f);
            num2 += 1;
        Label_018B:
            if (num2 < ((int) scrollbarArray2.Length))
            {
                goto Label_0172;
            }
            return;
        }
    }
}

