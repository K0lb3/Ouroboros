namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class ResultMask : MonoBehaviour
    {
        public RawImage ImgBg;

        public ResultMask()
        {
            base..ctor();
            return;
        }

        public void SetBg(Texture2D tex)
        {
            if (this.ImgBg == null)
            {
                goto Label_001C;
            }
            if ((tex == null) == null)
            {
                goto Label_001D;
            }
        Label_001C:
            return;
        Label_001D:
            this.ImgBg.set_texture(tex);
            this.ImgBg.get_gameObject().SetActive(1);
            return;
        }
    }
}

