namespace SRPG
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class EventPopupListItem : MonoBehaviour
    {
        [SerializeField]
        private Image BannerImage;
        [SerializeField]
        private Text EndAtText;
        [SerializeField]
        private Text MessageText;
        private BannerParam m_Param;

        public EventPopupListItem()
        {
            base..ctor();
            return;
        }

        private void Refresh()
        {
            EventBanner2 banner;
            if (this.m_Param != null)
            {
                goto Label_0016;
            }
            DebugUtility.LogError("イベントバナー情報がセットされていません.");
            return;
        Label_0016:
            if ((this.BannerImage != null) == null)
            {
                goto Label_005B;
            }
            banner = this.BannerImage.GetComponent<EventBanner2>();
            if ((banner != null) == null)
            {
                goto Label_005B;
            }
            DataSource.Bind<BannerParam>(this.BannerImage.get_gameObject(), this.m_Param);
            banner.Refresh();
        Label_005B:
            if ((this.EndAtText != null) == null)
            {
                goto Label_00CE;
            }
            this.EndAtText.get_gameObject().SetActive(0);
            if (this.m_Param == null)
            {
                goto Label_00CE;
            }
            if (string.IsNullOrEmpty(this.m_Param.end_at) != null)
            {
                goto Label_00CE;
            }
            this.EndAtText.set_text(this.m_Param.end_at + " まで");
            this.EndAtText.get_gameObject().SetActive(1);
        Label_00CE:
            if ((this.MessageText != null) == null)
            {
                goto Label_0137;
            }
            this.MessageText.get_gameObject().SetActive(0);
            if (this.m_Param == null)
            {
                goto Label_0137;
            }
            if (string.IsNullOrEmpty(this.m_Param.message) != null)
            {
                goto Label_0137;
            }
            this.MessageText.set_text(this.m_Param.message);
            this.MessageText.get_gameObject().SetActive(1);
        Label_0137:
            return;
        }

        public void SetupBannerParam(BannerParam _param)
        {
            this.m_Param = _param;
            this.Refresh();
            return;
        }
    }
}

