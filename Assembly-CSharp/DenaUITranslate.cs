// Decompiled with JetBrains decompiler
// Type: DenaUITranslate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class DenaUITranslate : MonoBehaviour
{
  public bool AutoAdapt;
  public Vector2 vecoffsetMin;
  public Vector2 vecoffsetMax;
  [Space]
  public bool doTranslate;
  public Vector3 vecTrans;
  public bool doScale;
  public Vector3 vecScale;
  [Space]
  public bool OnlyShowInIphoneX;
  public GameObject go;

  public DenaUITranslate()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    if ((double) ((float) Screen.get_width() / (float) Screen.get_height()) < 2.0 || !this.doScale)
      return;
    ((Component) this).get_gameObject().get_transform().set_localScale(this.vecScale);
  }
}
