// Decompiled with JetBrains decompiler
// Type: SRPG.BGSlider
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class BGSlider : MonoBehaviour, IDragHandler, IEventSystemHandler
  {
    public float ScrollSpeed;
    public float BGWidth;
    public List<GiziScroll> SyncScrollWith;
    public string SyncScrollWithID;
    private float mScrollPos;
    private float mDesiredScrollPos;
    private Vector2 mDefaultPosition;
    private bool mResetScrollPos;
    public float DefaultScrollRatio;
    private float WHEEL_SCROLL_COEF;
    private float axis_val;
    private List<RaycastResult> raycast_result_list;
    private List<GameObject> child_objects;
    private PointerEventData pointer_event;

    public BGSlider()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mResetScrollPos = true;
      if (this.SyncScrollWith.Count != 0 || string.IsNullOrEmpty(this.SyncScrollWithID))
        return;
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.SyncScrollWithID);
      if (gameObjects == null)
        return;
      foreach (GameObject gameObject in gameObjects)
        this.SyncScrollWith.Add((GiziScroll) gameObject.GetComponent<GiziScroll>());
    }

    private void ClampScrollPos(float min, float max)
    {
      this.mScrollPos = Mathf.Clamp(this.mScrollPos, min, max);
      this.mDesiredScrollPos = Mathf.Clamp(this.mDesiredScrollPos, min, max);
    }

    private void Update()
    {
      this.UpdateWheelScroll();
      this.mScrollPos = Mathf.Lerp(this.mScrollPos, this.mDesiredScrollPos, Time.get_deltaTime() * this.ScrollSpeed);
      Rect rect = (((Component) this).get_transform() as RectTransform).get_rect();
      // ISSUE: explicit reference operation
      float max = Mathf.Max(this.BGWidth - ((Rect) @rect).get_width(), 0.0f);
      if (this.mResetScrollPos)
      {
        this.mScrollPos = this.mDesiredScrollPos = max * this.DefaultScrollRatio;
        this.mResetScrollPos = false;
      }
      this.ClampScrollPos(0.0f, max);
      using (List<GiziScroll>.Enumerator enumerator = this.SyncScrollWith.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GiziScroll current = enumerator.Current;
          if (Object.op_Inequality((Object) current, (Object) null) && (double) max > 0.0)
            current.ScrollPos = this.mScrollPos / max;
        }
      }
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (Object.op_Inequality((Object) eventData.get_pointerDrag(), (Object) ((Component) this).get_gameObject()))
        return;
      this.mDesiredScrollPos -= (float) eventData.get_delta().x;
      ((AbstractEventData) eventData).Use();
    }

    private void UpdateWheelScroll()
    {
      this.axis_val = Input.GetAxis("Mouse ScrollWheel");
      if ((double) this.axis_val == 0.0 || !this.IsHitRayCast())
        return;
      this.mDesiredScrollPos -= this.axis_val * this.WHEEL_SCROLL_COEF;
    }

    private bool IsHitRayCast()
    {
      this.raycast_result_list.Clear();
      this.pointer_event = new PointerEventData(EventSystem.get_current());
      this.pointer_event.set_position(Vector2.op_Implicit(Input.get_mousePosition()));
      EventSystem.get_current().RaycastAll(this.pointer_event, this.raycast_result_list);
      if (this.raycast_result_list.Count <= 0)
        return false;
      this.child_objects.Clear();
      foreach (Component componentsInChild in (Transform[]) ((Component) this).GetComponentsInChildren<Transform>())
        this.child_objects.Add(componentsInChild.get_gameObject());
      List<GameObject> childObjects = this.child_objects;
      RaycastResult raycastResult = this.raycast_result_list[0];
      // ISSUE: explicit reference operation
      GameObject gameObject = ((RaycastResult) @raycastResult).get_gameObject();
      return childObjects.Contains(gameObject);
    }
  }
}
