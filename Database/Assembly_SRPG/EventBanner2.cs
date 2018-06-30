namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventBanner2 : MonoBehaviour
    {
        private Image mTarget;
        private LoadRequest mLoadRequest;

        public EventBanner2()
        {
            base..ctor();
            return;
        }

        public void Refresh()
        {
            BannerParam param;
            if (this.mLoadRequest != null)
            {
                goto Label_003B;
            }
            this.mTarget = base.GetComponent<Image>();
            param = DataSource.FindDataOfClass<BannerParam>(base.get_gameObject(), null);
            if (param == null)
            {
                goto Label_003B;
            }
            this.mLoadRequest = AssetManager.LoadAsync<GachaTabSprites>(param.banner);
        Label_003B:
            return;
        }

        private void Start()
        {
            this.Refresh();
            return;
        }

        private void Update()
        {
            BannerParam param;
            GachaTabSprites sprites;
            Sprite[] spriteArray;
            int num;
            if (this.mLoadRequest == null)
            {
                goto Label_001C;
            }
            if ((this.mTarget == null) == null)
            {
                goto Label_0024;
            }
        Label_001C:
            base.set_enabled(0);
            return;
        Label_0024:
            if (this.mLoadRequest.isDone != null)
            {
                goto Label_0035;
            }
            return;
        Label_0035:
            param = DataSource.FindDataOfClass<BannerParam>(base.get_gameObject(), null);
            if (param != null)
            {
                goto Label_0049;
            }
            return;
        Label_0049:
            sprites = this.mLoadRequest.asset as GachaTabSprites;
            if ((sprites != null) == null)
            {
                goto Label_00CE;
            }
            if (sprites.Sprites == null)
            {
                goto Label_00CE;
            }
            if (((int) sprites.Sprites.Length) <= 0)
            {
                goto Label_00CE;
            }
            spriteArray = sprites.Sprites;
            num = 0;
            goto Label_00C5;
        Label_008D:
            if ((spriteArray[num] != null) == null)
            {
                goto Label_00C1;
            }
            if ((spriteArray[num].get_name() == param.banr_sprite) == null)
            {
                goto Label_00C1;
            }
            this.mTarget.set_sprite(spriteArray[num]);
        Label_00C1:
            num += 1;
        Label_00C5:
            if (num < ((int) spriteArray.Length))
            {
                goto Label_008D;
            }
        Label_00CE:
            base.set_enabled(0);
            return;
        }
    }
}

