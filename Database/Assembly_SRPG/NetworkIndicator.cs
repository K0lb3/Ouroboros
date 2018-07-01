// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class NetworkIndicator : MonoBehaviour
  {
    public GameObject Body;
    public float FadeTime;
    public float KeepUp;
    private CanvasGroup mCanvasGroup;
    private float mRemainingTime;
    private string lang;
    [SerializeField]
    private Image header;
    [SerializeField]
    private Image fotter;
    [SerializeField]
    private Sprite[] locHeaderSprites;
    [SerializeField]
    private Sprite[] locFotterSprites;

    public NetworkIndicator()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (!Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      this.mCanvasGroup = (CanvasGroup) this.Body.GetComponent<CanvasGroup>();
      this.Body.SetActive(false);
    }

    private void Update()
    {
      if (!Network.IsIndicator)
      {
        this.Body.SetActive(false);
      }
      else
      {
        if (Network.IsBusy || !AssetDownloader.isDone || (FlowNode_NetworkIndicator.NeedDisplay() || EventAction.IsLoading))
          this.mRemainingTime = this.KeepUp + this.FadeTime;
        if ((double) this.mRemainingTime > 0.0)
        {
          this.mRemainingTime -= Time.get_unscaledDeltaTime();
          if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) && (double) this.FadeTime > 0.0)
            this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mRemainingTime / this.FadeTime));
          if (Object.op_Inequality((Object) this.Body, (Object) null))
            this.Body.SetActive((double) this.mRemainingTime > 0.0);
        }
        this.SetLocalizedSprite();
      }
    }

    private void SetLocalizedSprite()
    {
      if (!PlayerPrefs.HasKey("Selected_Language"))
        return;
      string configLanguage = GameUtility.Config_Language;
      if (!(this.lang != configLanguage))
        return;
      this.lang = configLanguage;
      int index = 0;
      string lang = this.lang;
      if (lang != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (NetworkIndicator.\u003C\u003Ef__switch\u0024mapF == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkIndicator.\u003C\u003Ef__switch\u0024mapF = new Dictionary<string, int>(3)
          {
            {
              "french",
              0
            },
            {
              "german",
              1
            },
            {
              "spanish",
              2
            }
          };
        }
        int num;
        // ISSUE: reference to a compiler-generated field
        if (NetworkIndicator.\u003C\u003Ef__switch\u0024mapF.TryGetValue(lang, out num))
        {
          switch (num)
          {
            case 0:
              index = 1;
              break;
            case 1:
              index = 2;
              break;
            case 2:
              index = 3;
              break;
          }
        }
      }
      if (this.locHeaderSprites != null && Object.op_Inequality((Object) this.header, (Object) null) && index < this.locHeaderSprites.Length)
        this.header.set_overrideSprite(this.locHeaderSprites[index]);
      if (this.locFotterSprites == null || !Object.op_Inequality((Object) this.fotter, (Object) null) || index >= this.locFotterSprites.Length)
        return;
      this.fotter.set_overrideSprite(this.locFotterSprites[index]);
    }
  }
}
