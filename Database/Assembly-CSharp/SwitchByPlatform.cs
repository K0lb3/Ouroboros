// Decompiled with JetBrains decompiler
// Type: SwitchByPlatform
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class SwitchByPlatform : MonoBehaviour
{
  [SerializeField]
  public RuntimePlatform[] hides;
  public bool CheckAmazonFlag;

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
    if (this.CheckAmazonFlag)
      ;
  }
}
