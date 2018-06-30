namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventBanner : MonoBehaviour
    {
        private RawImage mTarget;
        public float UpdateInterval;
        private float mInterval;
        private string mLastBannerName;
        private LoadRequest mBannerLoadRequest;

        public EventBanner()
        {
            this.UpdateInterval = 4f;
            base..ctor();
            return;
        }

        private void Start()
        {
            this.mTarget = base.GetComponent<RawImage>();
            this.mInterval = this.UpdateInterval;
            this.Update();
            return;
        }

        private void Update()
        {
            GameManager manager;
            List<string> list;
            ChapterParam[] paramArray;
            QuestParam[] paramArray2;
            long num;
            int num2;
            bool flag;
            string str;
            int num3;
            int num4;
            if (this.UpdateInterval > 0f)
            {
                goto Label_0011;
            }
            return;
        Label_0011:
            if (this.mBannerLoadRequest == null)
            {
                goto Label_0060;
            }
            if (this.mBannerLoadRequest.isDone != null)
            {
                goto Label_002D;
            }
            return;
        Label_002D:
            if ((this.mTarget != null) == null)
            {
                goto Label_0059;
            }
            this.mTarget.set_texture(this.mBannerLoadRequest.asset as Texture2D);
        Label_0059:
            this.mBannerLoadRequest = null;
        Label_0060:
            this.mInterval += Time.get_unscaledDeltaTime();
            if (this.mInterval >= this.UpdateInterval)
            {
                goto Label_0084;
            }
            return;
        Label_0084:
            this.mInterval = 0f;
            manager = MonoSingleton<GameManager>.Instance;
            list = new List<string>();
            paramArray = manager.Chapters;
            paramArray2 = manager.Player.AvailableQuests;
            num = Network.GetServerTime();
            num2 = 0;
            goto Label_014F;
        Label_00BD:
            flag = 0;
            str = paramArray[num2].banner;
            if (string.IsNullOrEmpty(str) != null)
            {
                goto Label_0149;
            }
            if (list.Contains(str) == null)
            {
                goto Label_00E9;
            }
            goto Label_0149;
        Label_00E9:
            num3 = 0;
            goto Label_012B;
        Label_00F1:
            if ((paramArray2[num3].ChapterID == paramArray[num2].iname) == null)
            {
                goto Label_0125;
            }
            if (paramArray2[num3].IsDateUnlock(num) == null)
            {
                goto Label_0125;
            }
            flag = 1;
            goto Label_0135;
        Label_0125:
            num3 += 1;
        Label_012B:
            if (num3 < ((int) paramArray2.Length))
            {
                goto Label_00F1;
            }
        Label_0135:
            if (flag != null)
            {
                goto Label_0141;
            }
            goto Label_0149;
        Label_0141:
            list.Add(str);
        Label_0149:
            num2 += 1;
        Label_014F:
            if (num2 < ((int) paramArray.Length))
            {
                goto Label_00BD;
            }
            if (list.Count > 0)
            {
                goto Label_0166;
            }
            return;
        Label_0166:
            num4 = (list.IndexOf(this.mLastBannerName) + 1) % list.Count;
            this.mLastBannerName = list[num4];
            this.mBannerLoadRequest = AssetManager.LoadAsync<Texture2D>("Banners/" + this.mLastBannerName);
            return;
        }
    }
}

