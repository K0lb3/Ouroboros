// Decompiled with JetBrains decompiler
// Type: AssetBundleLoadRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class AssetBundleLoadRequest : LoadRequest
{
  private Object mAsset;
  private AssetBundleRequest mLoadRequest;
  private AssetBundleCache mAssetBundle;
  private System.Type mComponentClass;

  public AssetBundleLoadRequest()
  {
  }

  public AssetBundleLoadRequest(AssetBundleCache assetBundle, string assetName, System.Type assetType)
  {
    if (assetType.IsSubclassOf(typeof (Component)))
      this.mComponentClass = assetType;
    this.mAssetBundle = assetBundle;
    this.mLoadRequest = this.mAssetBundle.AssetBundle.LoadAssetAsync(assetName);
  }

  public AssetBundleLoadRequest(Object _asset)
  {
    this.mAsset = _asset;
  }

  public override Object asset
  {
    get
    {
      return this.mAsset;
    }
  }

  public override float progress
  {
    get
    {
      if (this.mLoadRequest != null)
        return ((AsyncOperation) this.mLoadRequest).get_progress();
      return 0.0f;
    }
  }

  public override bool isDone
  {
    get
    {
      this.UpdateLoading();
      return this.mLoadRequest == null;
    }
  }

  public override bool MoveNext()
  {
    this.UpdateLoading();
    return this.mLoadRequest != null;
  }

  private void UpdateLoading()
  {
    if (this.mLoadRequest == null)
      return;
    if (!((AsyncOperation) this.mLoadRequest).get_isDone())
      return;
    try
    {
      this.mAsset = (object) this.mComponentClass == null ? this.mLoadRequest.get_asset() : (Object) (this.mLoadRequest.get_asset() as GameObject).GetComponent(this.mComponentClass);
    }
    catch (Exception ex)
    {
      DebugUtility.LogError("(" + (object) ex.GetType() + ")" + ex.Message + " >>> AssetBundle:" + this.mAssetBundle.Name + " " + (!Object.op_Equality((Object) this.mAssetBundle.AssetBundle, (Object) null) ? (object) string.Empty : (object) "is null"));
    }
    this.mLoadRequest = (AssetBundleRequest) null;
    this.UntrackTextComponents(this.mAsset);
  }
}
