// Decompiled with JetBrains decompiler
// Type: DontDestroyOnLoad
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
  public DontDestroyOnLoad()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    Object.DontDestroyOnLoad((Object) ((Component) this).get_gameObject());
  }
}
