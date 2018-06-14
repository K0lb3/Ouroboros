// Decompiled with JetBrains decompiler
// Type: ShowInfoOfPlayer
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using Photon;
using UnityEngine;

[RequireComponent(typeof (PhotonView))]
public class ShowInfoOfPlayer : MonoBehaviour
{
  private GameObject textGo;
  private TextMesh tm;
  public float CharacterSize;
  public Font font;
  public bool DisableOnOwnObjects;

  private void Start()
  {
    if (Object.op_Equality((Object) this.font, (Object) null))
    {
      this.font = (Font) Resources.FindObjectsOfTypeAll(typeof (Font))[0];
      Debug.LogWarning((object) ("No font defined. Found font: " + (object) this.font));
    }
    if (!Object.op_Equality((Object) this.tm, (Object) null))
      return;
    this.textGo = new GameObject("3d text");
    this.textGo.get_transform().set_parent(((Component) this).get_gameObject().get_transform());
    this.textGo.get_transform().set_localPosition(Vector3.get_zero());
    ((Renderer) this.textGo.AddComponent<MeshRenderer>()).set_material(this.font.get_material());
    this.tm = (TextMesh) this.textGo.AddComponent<TextMesh>();
    this.tm.set_font(this.font);
    this.tm.set_anchor((TextAnchor) 4);
    if ((double) this.CharacterSize <= 0.0)
      return;
    this.tm.set_characterSize(this.CharacterSize);
  }

  private void Update()
  {
    bool flag = !this.DisableOnOwnObjects || this.photonView.isMine;
    if (Object.op_Inequality((Object) this.textGo, (Object) null))
      this.textGo.SetActive(flag);
    if (!flag)
      return;
    PhotonPlayer owner = this.photonView.owner;
    if (owner != null)
      this.tm.set_text(!string.IsNullOrEmpty(owner.NickName) ? owner.NickName : "player" + (object) owner.ID);
    else if (this.photonView.isSceneView)
      this.tm.set_text("scn");
    else
      this.tm.set_text("n/a");
  }
}
