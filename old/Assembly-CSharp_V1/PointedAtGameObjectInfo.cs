// Decompiled with JetBrains decompiler
// Type: PointedAtGameObjectInfo
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (InputToEvent))]
public class PointedAtGameObjectInfo : MonoBehaviour
{
  public PointedAtGameObjectInfo()
  {
    base.\u002Ector();
  }

  private void OnGUI()
  {
    if (!Object.op_Inequality((Object) InputToEvent.goPointedAt, (Object) null))
      return;
    PhotonView photonView = InputToEvent.goPointedAt.GetPhotonView();
    if (!Object.op_Inequality((Object) photonView, (Object) null))
      return;
    GUI.Label(new Rect((float) (Input.get_mousePosition().x + 5.0), (float) ((double) Screen.get_height() - Input.get_mousePosition().y - 15.0), 300f, 30f), string.Format("ViewID {0} {1}{2}", (object) photonView.viewID, !photonView.isSceneView ? (object) string.Empty : (object) "scene ", !photonView.isMine ? (object) ("owner: " + (object) photonView.ownerId) : (object) "mine"));
  }
}
