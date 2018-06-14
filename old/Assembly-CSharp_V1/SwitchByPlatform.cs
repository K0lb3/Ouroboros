// Decompiled with JetBrains decompiler
// Type: SwitchByPlatform
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SwitchByPlatform : MonoBehaviour
{
  [SerializeField]
  public RuntimePlatform[] hides;

  public SwitchByPlatform()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    foreach (int hide in this.hides)
    {
      if (Application.get_platform() == (RuntimePlatform) hide)
        ((Component) this).get_gameObject().SetActive(false);
    }
  }
}
