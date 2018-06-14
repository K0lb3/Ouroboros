// Decompiled with JetBrains decompiler
// Type: ListItemEvents
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Event/List Item Events")]
public class ListItemEvents : MonoBehaviour
{
  public ListItemEvents.ListItemEvent OnSelect;
  public ListItemEvents.ListItemEvent OnOpenDetail;
  public ListItemEvents.ListItemEvent OnCloseDetail;
  public Transform Body;
  private RectTransform mTransform;
  public bool IsEnableSkillChange;

  public ListItemEvents()
  {
    base.\u002Ector();
  }

  protected virtual void Awake()
  {
    this.mTransform = ((Component) this).get_transform() as RectTransform;
  }

  public void Select()
  {
    if (this.OnSelect == null)
      return;
    this.OnSelect(((Component) this).get_gameObject());
  }

  public void OpenDetail()
  {
    if (this.OnOpenDetail == null)
      return;
    this.OnOpenDetail(((Component) this).get_gameObject());
  }

  public void CloseDetail()
  {
    if (this.OnCloseDetail == null)
      return;
    this.OnCloseDetail(((Component) this).get_gameObject());
  }

  public void AttachBody()
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null) || !Object.op_Inequality((Object) this.Body.get_parent(), (Object) this.mTransform))
      return;
    this.Body.SetParent((Transform) this.mTransform, false);
    Animator component1 = (Animator) ((Component) this).GetComponent<Animator>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.Rebind();
    ((Component) this.Body).get_gameObject().SetActive(true);
    Selectable component2 = (Selectable) ((Component) this).GetComponent<Selectable>();
    if (!Object.op_Inequality((Object) component2, (Object) null) || !((Behaviour) component2).get_enabled())
      return;
    ((Behaviour) component2).set_enabled(!((Behaviour) component2).get_enabled());
    ((Behaviour) component2).set_enabled(!((Behaviour) component2).get_enabled());
  }

  public void DetachBody(Transform pool)
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null) || !Object.op_Inequality((Object) this.Body.get_parent(), (Object) pool))
      return;
    this.Body.SetParent(pool, false);
    ((Component) this.Body).get_gameObject().SetActive(false);
  }

  public RectTransform GetRectTransform()
  {
    return this.mTransform;
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.Body, (Object) null))
      return;
    Object.Destroy((Object) this.Body);
    this.Body = (Transform) null;
  }

  public delegate void ListItemEvent(GameObject go);
}
