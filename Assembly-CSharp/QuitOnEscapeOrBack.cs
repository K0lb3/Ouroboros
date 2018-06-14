// Decompiled with JetBrains decompiler
// Type: QuitOnEscapeOrBack
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class QuitOnEscapeOrBack : MonoBehaviour
{
  public QuitOnEscapeOrBack()
  {
    base.\u002Ector();
  }

  private void Update()
  {
    if (!Input.GetKeyDown((KeyCode) 27))
      return;
    Application.Quit();
  }
}
