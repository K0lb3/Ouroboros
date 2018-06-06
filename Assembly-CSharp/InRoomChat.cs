// Decompiled with JetBrains decompiler
// Type: InRoomChat
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class InRoomChat : MonoBehaviour
{
  public static readonly string ChatRPC = "Chat";
  public Rect GuiRect = new Rect(0.0f, 0.0f, 250f, 300f);
  public bool IsVisible = true;
  public List<string> messages = new List<string>();
  private string inputLine = string.Empty;
  private Vector2 scrollPos = Vector2.get_zero();
  public bool AlignBottom;

  public void Start()
  {
    if (!this.AlignBottom)
      return;
    // ISSUE: explicit reference operation
    // ISSUE: explicit reference operation
    ((Rect) @this.GuiRect).set_y((float) Screen.get_height() - ((Rect) @this.GuiRect).get_height());
  }

  public void OnGUI()
  {
    if (!this.IsVisible || !PhotonNetwork.inRoom)
      return;
    if (Event.get_current().get_type() == 4 && (Event.get_current().get_keyCode() == 271 || Event.get_current().get_keyCode() == 13))
    {
      if (!string.IsNullOrEmpty(this.inputLine))
      {
        this.photonView.RPC("Chat", PhotonTargets.All, new object[1]
        {
          (object) this.inputLine
        });
        this.inputLine = string.Empty;
        GUI.FocusControl(string.Empty);
        return;
      }
      GUI.FocusControl("ChatInput");
    }
    GUI.SetNextControlName(string.Empty);
    GUILayout.BeginArea(this.GuiRect);
    this.scrollPos = GUILayout.BeginScrollView(this.scrollPos, new GUILayoutOption[0]);
    GUILayout.FlexibleSpace();
    for (int index = this.messages.Count - 1; index >= 0; --index)
      GUILayout.Label(this.messages[index], new GUILayoutOption[0]);
    GUILayout.EndScrollView();
    GUILayout.BeginHorizontal(new GUILayoutOption[0]);
    GUI.SetNextControlName("ChatInput");
    this.inputLine = GUILayout.TextField(this.inputLine, new GUILayoutOption[0]);
    if (GUILayout.Button("Send", new GUILayoutOption[1]{ GUILayout.ExpandWidth(false) }))
    {
      this.photonView.RPC("Chat", PhotonTargets.All, new object[1]
      {
        (object) this.inputLine
      });
      this.inputLine = string.Empty;
      GUI.FocusControl(string.Empty);
    }
    GUILayout.EndHorizontal();
    GUILayout.EndArea();
  }

  [PunRPC]
  public void Chat(string newLine, PhotonMessageInfo mi)
  {
    string str = "anonymous";
    if (mi.sender != null)
      str = string.IsNullOrEmpty(mi.sender.NickName) ? "player " + (object) mi.sender.ID : mi.sender.NickName;
    this.messages.Add(str + ": " + newLine);
  }

  public void AddLine(string newLine)
  {
    this.messages.Add(newLine);
  }
}
