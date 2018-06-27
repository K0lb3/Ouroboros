// Decompiled with JetBrains decompiler
// Type: SortingOrder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof (Renderer))]
[AddComponentMenu("Rendering/SortingOrder")]
public class SortingOrder : MonoBehaviour
{
  [SerializeField]
  private int mSortingOrder;

  public SortingOrder()
  {
    base.\u002Ector();
  }

  private void Awake()
  {
    ((Behaviour) this).set_enabled(false);
  }

  private void OnValidate()
  {
    ((Renderer) ((Component) this).GetComponent<Renderer>()).set_sortingOrder(this.mSortingOrder);
  }
}
