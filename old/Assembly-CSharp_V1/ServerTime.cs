// Decompiled with JetBrains decompiler
// Type: ServerTime
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

public class ServerTime : MonoBehaviour
{
  public ServerTime()
  {
    base.\u002Ector();
  }

  private void OnGUI()
  {
    GUILayout.BeginArea(new Rect((float) (Screen.get_width() / 2 - 100), 0.0f, 200f, 30f));
    GUILayout.Label(string.Format("Time Offset: {0}", (object) (PhotonNetwork.ServerTimestamp - Environment.TickCount)), new GUILayoutOption[0]);
    if (GUILayout.Button("fetch", new GUILayoutOption[0]))
      PhotonNetwork.FetchServerTimestamp();
    GUILayout.EndArea();
  }
}
