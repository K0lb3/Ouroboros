// Decompiled with JetBrains decompiler
// Type: SRPG.EventBanner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EventBanner : MonoBehaviour
  {
    private RawImage mTarget;
    public float UpdateInterval;
    private float mInterval;
    private string mLastBannerName;
    private LoadRequest mBannerLoadRequest;

    public EventBanner()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mTarget = (RawImage) ((Component) this).GetComponent<RawImage>();
      this.mInterval = this.UpdateInterval;
      this.Update();
    }

    private void Update()
    {
      if ((double) this.UpdateInterval <= 0.0)
        return;
      if (this.mBannerLoadRequest != null)
      {
        if (!this.mBannerLoadRequest.isDone)
          return;
        if (Object.op_Inequality((Object) this.mTarget, (Object) null))
          this.mTarget.set_texture((Texture) (this.mBannerLoadRequest.asset as Texture2D));
        this.mBannerLoadRequest = (LoadRequest) null;
      }
      this.mInterval += Time.get_unscaledDeltaTime();
      if ((double) this.mInterval < (double) this.UpdateInterval)
        return;
      this.mInterval = 0.0f;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<string> stringList = new List<string>();
      ChapterParam[] chapters = instance.Chapters;
      QuestParam[] availableQuests = instance.Player.AvailableQuests;
      long serverTime = Network.GetServerTime();
      for (int index1 = 0; index1 < chapters.Length; ++index1)
      {
        bool flag = false;
        string banner = chapters[index1].banner;
        if (!string.IsNullOrEmpty(banner) && !stringList.Contains(banner))
        {
          for (int index2 = 0; index2 < availableQuests.Length; ++index2)
          {
            if (availableQuests[index2].ChapterID == chapters[index1].iname && availableQuests[index2].IsDateUnlock(serverTime))
            {
              flag = true;
              break;
            }
          }
          if (flag)
            stringList.Add(banner);
        }
      }
      if (stringList.Count <= 0)
        return;
      int index = (stringList.IndexOf(this.mLastBannerName) + 1) % stringList.Count;
      this.mLastBannerName = stringList[index];
      this.mBannerLoadRequest = AssetManager.LoadAsync<Texture2D>("Banners/" + this.mLastBannerName);
    }
  }
}
