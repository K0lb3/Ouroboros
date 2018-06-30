namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardTrustMaster : MonoBehaviour
    {
        [SerializeField]
        private RawImage mCardImage;
        [SerializeField]
        private RawImage mCardImageAdd;

        public ConceptCardTrustMaster()
        {
            base..ctor();
            return;
        }

        public void SetData(ConceptCardData data)
        {
            string str;
            str = AssetPath.ConceptCard(data.Param);
            if ((this.mCardImage != null) == null)
            {
                goto Label_002E;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImage, str);
        Label_002E:
            if ((this.mCardImageAdd != null) == null)
            {
                goto Label_0050;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.mCardImageAdd, str);
        Label_0050:
            return;
        }
    }
}

