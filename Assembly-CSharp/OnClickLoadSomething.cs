// Decompiled with JetBrains decompiler
// Type: OnClickLoadSomething
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoadSomething : MonoBehaviour
{
  public OnClickLoadSomething.ResourceTypeOption ResourceTypeToLoad;
  public string ResourceToLoad;

  public OnClickLoadSomething()
  {
    base.\u002Ector();
  }

  public void OnClick()
  {
    switch (this.ResourceTypeToLoad)
    {
      case OnClickLoadSomething.ResourceTypeOption.Scene:
        SceneManager.LoadScene(this.ResourceToLoad);
        break;
      case OnClickLoadSomething.ResourceTypeOption.Web:
        Application.OpenURL(this.ResourceToLoad);
        break;
    }
  }

  public enum ResourceTypeOption : byte
  {
    Scene,
    Web,
  }
}
