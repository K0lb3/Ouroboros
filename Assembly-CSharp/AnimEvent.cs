// Decompiled with JetBrains decompiler
// Type: AnimEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class AnimEvent : ScriptableObject
{
  public float Start;
  public float End;

  public AnimEvent()
  {
    base.\u002Ector();
  }

  public virtual void OnStart(GameObject go)
  {
  }

  public virtual void OnTick(GameObject go, float ratio)
  {
  }

  public virtual void OnEnd(GameObject go)
  {
  }

  public virtual void UpdatePreview(GameObject go, float time)
  {
  }
}
