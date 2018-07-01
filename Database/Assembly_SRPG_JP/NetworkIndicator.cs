// Decompiled with JetBrains decompiler
// Type: SRPG.NetworkIndicator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class NetworkIndicator : MonoBehaviour
  {
    public GameObject Body;
    public float FadeTime;
    public float KeepUp;
    private CanvasGroup mCanvasGroup;
    private float mRemainingTime;

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
        if ((double) this.mRemainingTime <= 0.0)
          return;
        this.mRemainingTime -= Time.get_unscaledDeltaTime();
        if (Object.op_Inequality((Object) this.mCanvasGroup, (Object) null) && (double) this.FadeTime > 0.0)
          this.mCanvasGroup.set_alpha(Mathf.Clamp01(this.mRemainingTime / this.FadeTime));
        if (!Object.op_Inequality((Object) this.Body, (Object) null))
          return;
        this.Body.SetActive((double) this.mRemainingTime > 0.0);
      }
    }
  }
}
