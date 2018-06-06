// Decompiled with JetBrains decompiler
// Type: ShowStatusWhenConnecting
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class ShowStatusWhenConnecting : MonoBehaviour
{
  public GUISkin Skin;

  public ShowStatusWhenConnecting()
  {
    base.\u002Ector();
  }

  private void OnGUI()
  {
    if (Object.op_Inequality((Object) this.Skin, (Object) null))
      GUI.set_skin(this.Skin);
    float num1 = 400f;
    float num2 = 100f;
    Rect rect;
    // ISSUE: explicit reference operation
    ((Rect) @rect).\u002Ector((float) (((double) Screen.get_width() - (double) num1) / 2.0), (float) (((double) Screen.get_height() - (double) num2) / 2.0), num1, num2);
    GUILayout.BeginArea(rect, GUI.get_skin().get_box());
    GUILayout.Label("Connecting" + this.GetConnectingDots(), GUI.get_skin().get_customStyles()[0], new GUILayoutOption[0]);
    GUILayout.Label("Status: " + (object) PhotonNetwork.connectionStateDetailed, new GUILayoutOption[0]);
    GUILayout.EndArea();
    if (!PhotonNetwork.inRoom)
      return;
    ((Behaviour) this).set_enabled(false);
  }

  private string GetConnectingDots()
  {
    string empty = string.Empty;
    int num = Mathf.FloorToInt((float) ((double) Time.get_timeSinceLevelLoad() * 3.0 % 4.0));
    for (int index = 0; index < num; ++index)
      empty += " .";
    return empty;
  }
}
