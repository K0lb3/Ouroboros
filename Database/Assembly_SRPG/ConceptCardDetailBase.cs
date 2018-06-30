namespace SRPG
{
    using GR;
    using System;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;

    public class ConceptCardDetailBase : MonoBehaviour
    {
        protected ConceptCardData mConceptCardData;

        public ConceptCardDetailBase()
        {
            base..ctor();
            return;
        }

        public void LoadImage(string path, RawImage image)
        {
            string str;
            if ((image != null) == null)
            {
                goto Label_0035;
            }
            str = Path.GetFileName(path);
            if ((image.get_mainTexture().get_name() != str) == null)
            {
                goto Label_0035;
            }
            MonoSingleton<GameManager>.Instance.ApplyTextureAsync(image, path);
        Label_0035:
            return;
        }

        public virtual void Refresh()
        {
        }

        public virtual void SetParam(ConceptCardData card_data)
        {
            this.mConceptCardData = card_data;
            return;
        }

        public virtual void SetParam(ConceptCardData card_data, int addExp, int addTrust, int addAwakeLv)
        {
        }

        public void SetSprite(Image image, Sprite sprite)
        {
            if ((image != null) == null)
            {
                goto Label_0013;
            }
            image.set_sprite(sprite);
        Label_0013:
            return;
        }

        public void SetText(Text text, string str)
        {
            if ((text != null) == null)
            {
                goto Label_0013;
            }
            text.set_text(str);
        Label_0013:
            return;
        }

        public void SwitchObject(bool is_on, GameObject obj, GameObject opposite_obj)
        {
            if ((obj != null) == null)
            {
                goto Label_0013;
            }
            obj.SetActive(is_on);
        Label_0013:
            if ((opposite_obj != null) == null)
            {
                goto Label_0029;
            }
            opposite_obj.SetActive(is_on == 0);
        Label_0029:
            return;
        }

        protected GameManager GM
        {
            get
            {
                return MonoSingleton<GameManager>.Instance;
            }
        }

        protected MasterParam Master
        {
            get
            {
                return this.GM.MasterParam;
            }
        }
    }
}

