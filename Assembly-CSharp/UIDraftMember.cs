// Decompiled with JetBrains decompiler
// Type: UIDraftMember
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Expose")]
[RequireComponent(typeof (RectTransform))]
[ExecuteInEditMode]
public class UIDraftMember : MonoBehaviour
{
  [SerializeField]
  private bool mSearchGraphicComponent;
  public bool UseGraphic;

  public UIDraftMember()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    if (!Application.get_isEditor())
      return;
    ((Component) this).set_tag("EditorOnly");
    if (!this.mSearchGraphicComponent)
      return;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<Text>(), (Object) null))
      this.UseGraphic = true;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<RawImage>(), (Object) null))
      this.UseGraphic = true;
    if (Object.op_Inequality((Object) ((Component) this).GetComponent<Image>(), (Object) null))
      this.UseGraphic = true;
    this.mSearchGraphicComponent = false;
  }
}
