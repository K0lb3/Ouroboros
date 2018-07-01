// Decompiled with JetBrains decompiler
// Type: SRPG.EventPopupListItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
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
      base.\u002Ector();
    }

    public void SetupBannerParam(BannerParam _param)
    {
      this.m_Param = _param;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.m_Param == null)
      {
        DebugUtility.LogError("イベントバナー情報がセットされていません.");
      }
      else
      {
        if (Object.op_Inequality((Object) this.BannerImage, (Object) null))
        {
          EventBanner2 component = (EventBanner2) ((Component) this.BannerImage).GetComponent<EventBanner2>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            DataSource.Bind<BannerParam>(((Component) this.BannerImage).get_gameObject(), this.m_Param);
            component.Refresh();
          }
        }
        if (Object.op_Inequality((Object) this.EndAtText, (Object) null))
        {
          ((Component) this.EndAtText).get_gameObject().SetActive(false);
          if (this.m_Param != null && !string.IsNullOrEmpty(this.m_Param.end_at))
          {
            this.EndAtText.set_text(this.m_Param.end_at + " まで");
            ((Component) this.EndAtText).get_gameObject().SetActive(true);
          }
        }
        if (!Object.op_Inequality((Object) this.MessageText, (Object) null))
          return;
        ((Component) this.MessageText).get_gameObject().SetActive(false);
        if (this.m_Param == null || string.IsNullOrEmpty(this.m_Param.message))
          return;
        this.MessageText.set_text(this.m_Param.message);
        ((Component) this.MessageText).get_gameObject().SetActive(true);
      }
    }
  }
}
