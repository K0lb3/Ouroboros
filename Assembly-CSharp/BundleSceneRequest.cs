// Decompiled with JetBrains decompiler
// Type: BundleSceneRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class BundleSceneRequest : SceneRequest
{
  private bool mSceneActivated;
  private bool mAdditive;
  private LoadRequest mRequest;

  public BundleSceneRequest(LoadRequest request, bool additive)
  {
    this.mRequest = request;
    this.mAdditive = additive;
  }

  public override bool ActivateScene()
  {
    if (!this.isDone || this.mSceneActivated)
      return this.mSceneActivated;
    if (!this.mAdditive)
    {
      SceneAssetBundleLoader.SceneBundle = this.mRequest.asset;
      SceneManager.LoadScene("EmptyScene");
    }
    else if (Object.op_Inequality(this.mRequest.asset, (Object) null))
      Object.Instantiate(this.mRequest.asset);
    this.mSceneActivated = true;
    return true;
  }

  public override bool IsActivated
  {
    get
    {
      return this.mSceneActivated;
    }
  }

  public override bool isAdditive
  {
    get
    {
      return this.mAdditive;
    }
  }

  public override bool canBeActivated
  {
    get
    {
      return this.mRequest.isDone;
    }
  }

  public override bool isDone
  {
    get
    {
      if (this.mSceneActivated)
        return (double) this.mRequest.progress >= 1.0;
      return false;
    }
  }

  public override bool MoveNext()
  {
    if (this.mRequest != null)
      return !this.mRequest.isDone;
    return false;
  }

  public override object Current
  {
    get
    {
      return (object) null;
    }
  }

  public override float progress
  {
    get
    {
      if (this.mRequest != null)
        return this.mRequest.progress;
      return 0.0f;
    }
  }
}
