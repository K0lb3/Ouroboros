// Decompiled with JetBrains decompiler
// Type: ApplicationEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class ApplicationEventHandler : MonoBehaviour
{
  private bool m_IsQuiting;
  private EmbedWindowYesNo m_QuitWindow;

  public ApplicationEventHandler()
  {
    base.\u002Ector();
  }

  public void OpenQuitWindow()
  {
    if (!Object.op_Equality((Object) this.m_QuitWindow, (Object) null) || this.m_IsQuiting)
      return;
    this.m_QuitWindow = EmbedWindowYesNo.Create(LocalizedText.Get("embed.APP_QUIT"), new EmbedWindowYesNo.YesNoWindowEvent(this.OnApplicationQuitWindowResult));
  }

  public void OnApplicationQuitWindowResult(bool yes)
  {
    if (yes)
      this.OnDecide();
    else
      this.OnCancel();
  }

  private void OnCancel()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = false;
  }

  private void OnDecide()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = true;
    Application.Quit();
  }
}
