// Decompiled with JetBrains decompiler
// Type: SortingOrder
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Renderer))]
[ExecuteInEditMode]
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
