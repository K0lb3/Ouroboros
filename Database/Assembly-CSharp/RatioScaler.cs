// Decompiled with JetBrains decompiler
// Type: RatioScaler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class RatioScaler : MonoBehaviour
{
  private const double ratioToBeFix = 2.0;

  public RatioScaler()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    if ((double) Screen.get_width() / (double) Screen.get_height() < 2.0)
      return;
    if (((Component) this).get_transform().get_localScale().x < 1.0 || ((Component) this).get_transform().get_localScale().y < 1.0)
      ((Component) this).get_transform().set_localScale(new Vector3(1.2f, 1.2f, (float) (((Component) this).get_transform().get_localScale().z * 1.0)));
    else
      ((Component) this).get_transform().set_localScale(new Vector3((float) (((Component) this).get_transform().get_localScale().x * 1.20000004768372), (float) (((Component) this).get_transform().get_localScale().y * 1.20000004768372), (float) (((Component) this).get_transform().get_localScale().z * 1.0)));
    for (Transform transform = ((Component) this).get_transform(); Object.op_Inequality((Object) transform, (Object) null); transform = transform.get_parent())
    {
      Debug.Log((object) ((Object) transform).get_name());
      if (((Object) transform).get_name().ToLower().Contains("demo_cut") || ((Object) transform).get_name().ToLower().Contains("splash_base"))
      {
        Debug.Log((object) "found demo cut");
        ((CanvasScaler) ((Component) this).GetComponent<SRPG_CanvasScaler>()).set_matchWidthOrHeight(0.5f);
        ((Component) this).get_transform().set_localScale(new Vector3(1f, 1f, 1f));
        break;
      }
    }
  }
}
