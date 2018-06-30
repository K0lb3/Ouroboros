namespace SRPG
{
    using GR;
    using System;
    using UnityEngine;

    public class ConceptCardDetailImage : MonoBehaviour
    {
        public RawImage_Transparent Image;

        public ConceptCardDetailImage()
        {
            base..ctor();
            return;
        }

        private void Start()
        {
            ConceptCardManager manager;
            ConceptCardData data;
            string str;
            if ((ConceptCardManager.Instance == null) == null)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            manager = ConceptCardManager.Instance;
            data = null;
            if (manager.SelectedConceptCardMaterialData == null)
            {
                goto Label_0030;
            }
            data = manager.SelectedConceptCardMaterialData;
            goto Label_0037;
        Label_0030:
            data = manager.SelectedConceptCardData;
        Label_0037:
            if ((this.Image != null) == null)
            {
                goto Label_0065;
            }
            str = AssetPath.ConceptCard(data.Param);
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.Image, str);
        Label_0065:
            return;
        }
    }
}

