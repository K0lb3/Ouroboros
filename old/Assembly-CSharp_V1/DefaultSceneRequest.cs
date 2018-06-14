// Decompiled with JetBrains decompiler
// Type: DefaultSceneRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DefaultSceneRequest : SceneRequest
{
  private bool mSceneActivated;
  private AsyncOperation mRequest;
  private bool mAdditive;

  public DefaultSceneRequest(AsyncOperation request, bool additive)
  {
    request.set_allowSceneActivation(false);
    this.mAdditive = additive;
    this.mRequest = request;
  }

  public override bool ActivateScene()
  {
    if (this.mSceneActivated)
      return this.mSceneActivated;
    this.mRequest.set_allowSceneActivation(true);
    this.mSceneActivated = true;
    AssetManager.OnSceneActivate((SceneRequest) this);
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

  public override bool isDone
  {
    get
    {
      if (this.mSceneActivated)
        return this.mRequest.get_isDone();
      return false;
    }
  }

  public override bool canBeActivated
  {
    get
    {
      return (double) this.mRequest.get_progress() >= 0.899999976158142;
    }
  }

  public override bool MoveNext()
  {
    if (this.mRequest != null)
      return !this.isDone;
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
        return this.mRequest.get_progress();
      return 0.0f;
    }
  }
}
