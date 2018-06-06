// Decompiled with JetBrains decompiler
// Type: FastLoadRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FastLoadRequest : LoadRequest
{
  private static List<FastLoadRequest> mRequests = new List<FastLoadRequest>();
  private const double MaxLoadTimePerFrame = 0.00833333376795053;
  private static double mLoadTime;
  private Object mAsset;
  private bool mIsDone;
  private AssetBundleCache mAssetBundle;
  private AssetList.Item mAssetBundleNode;
  private string mAssetName;
  private System.Type mAssetType;

  public FastLoadRequest(AssetList.Item assetBundleNode, string assetName, System.Type assetType)
  {
    this.mAssetBundleNode = assetBundleNode;
    this.mAssetName = assetName;
    this.mAssetType = assetType;
    FastLoadRequest.mRequests.Add(this);
    this.UpdateLoading();
  }

  public static void UpdateAll()
  {
    for (int index = 0; index < FastLoadRequest.mRequests.Count && FastLoadRequest.mLoadTime < 0.00833333376795053; ++index)
    {
      FastLoadRequest mRequest = FastLoadRequest.mRequests[index];
      mRequest.UpdateLoading();
      if (mRequest.isDone)
        --index;
    }
    FastLoadRequest.mLoadTime = 0.0;
  }

  private void UpdateLoading()
  {
    try
    {
      if (this.isDone || FastLoadRequest.mLoadTime >= 0.00833333376795053)
        return;
      DateTime now1 = DateTime.Now;
      if (this.mAssetBundleNode != null)
      {
        this.mAssetBundle = AssetManager.Instance.OpenAssetBundleAndDependencies(this.mAssetBundleNode, 0, (List<AssetBundleCache>) null, 15f);
        this.mAsset = this.mAssetBundle.AssetBundle.LoadAsset(Path.GetFileNameWithoutExtension(this.mAssetName));
        this.mAssetBundle = (AssetBundleCache) null;
        AssetManager.Instance.UnloadUnusedAssetBundles(true, false);
      }
      else
        this.mAsset = Resources.Load(this.mAssetName, this.mAssetType);
      DateTime now2 = DateTime.Now;
      FastLoadRequest.mLoadTime += (now2 - now1).TotalSeconds;
      FastLoadRequest.mRequests.Remove(this);
      this.mIsDone = true;
      this.UntrackTextComponents(this.mAsset);
    }
    catch (Exception ex)
    {
      Debug.Log((object) ("Exception: LoadFile[" + this.mAssetName + "]" + ex.ToString()));
      FastLoadRequest.mRequests.Remove(this);
      this.mIsDone = true;
    }
  }

  public override float progress
  {
    get
    {
      return !this.isDone ? 0.0f : 1f;
    }
  }

  public override bool isDone
  {
    get
    {
      return this.mIsDone;
    }
  }

  public override Object asset
  {
    get
    {
      return this.mAsset;
    }
  }

  public override bool MoveNext()
  {
    return !this.mIsDone;
  }

  public override void KeepSourceAlive()
  {
  }
}
