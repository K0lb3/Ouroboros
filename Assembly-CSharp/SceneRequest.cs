// Decompiled with JetBrains decompiler
// Type: SceneRequest
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System.Collections;

public abstract class SceneRequest : IEnumerator
{
  public abstract bool ActivateScene();

  public abstract bool IsActivated { get; }

  public abstract bool isDone { get; }

  public abstract bool canBeActivated { get; }

  public virtual void Reset()
  {
  }

  public abstract bool MoveNext();

  public abstract object Current { get; }

  public virtual float progress
  {
    get
    {
      return 0.0f;
    }
  }

  public abstract bool isAdditive { get; }
}
