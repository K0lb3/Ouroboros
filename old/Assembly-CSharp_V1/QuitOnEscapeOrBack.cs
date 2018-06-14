// Decompiled with JetBrains decompiler
// Type: QuitOnEscapeOrBack
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
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
