namespace SRPG
{
    using System;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class AreaMapController : MonoBehaviour
    {
        public string MapID;
        public RawImage_Transparent[] Images;
        public string[] ImageNames;
        private CanvasGroup mCanvasGroup;

        public AreaMapController()
        {
            this.Images = new RawImage_Transparent[0];
            this.ImageNames = new string[0];
            base..ctor();
            return;
        }

        private void Awake()
        {
            this.SetVisible(0);
            return;
        }

        private void OnDisable()
        {
            int num;
            num = 0;
            goto Label_003A;
        Label_0007:
            if (num >= ((int) this.Images.Length))
            {
                goto Label_0036;
            }
            if ((this.Images[num] != null) == null)
            {
                goto Label_0036;
            }
            this.Images[num].set_texture(null);
        Label_0036:
            num += 1;
        Label_003A:
            if (num < ((int) this.ImageNames.Length))
            {
                goto Label_0007;
            }
            return;
        }

        private void OnEnable()
        {
            int num;
            num = 0;
            goto Label_0058;
        Label_0007:
            if (num >= ((int) this.Images.Length))
            {
                goto Label_0054;
            }
            if ((this.Images[num] != null) == null)
            {
                goto Label_0054;
            }
            if (string.IsNullOrEmpty(this.ImageNames[num]) != null)
            {
                goto Label_0054;
            }
            this.Images[num].set_texture(AssetManager.Load<Texture2D>(this.ImageNames[num]));
        Label_0054:
            num += 1;
        Label_0058:
            if (num < ((int) this.ImageNames.Length))
            {
                goto Label_0007;
            }
            return;
        }

        public void SetOpacity(float opacity)
        {
            opacity = Mathf.Clamp01(opacity);
            this.SetVisible(opacity > 0f);
            if ((this.mCanvasGroup != null) == null)
            {
                goto Label_0038;
            }
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(opacity));
        Label_0038:
            return;
        }

        private void SetVisible(bool visible)
        {
            base.get_gameObject().SetActive(visible);
            return;
        }

        private void Start()
        {
            this.mCanvasGroup = base.GetComponent<CanvasGroup>();
            return;
        }
    }
}

