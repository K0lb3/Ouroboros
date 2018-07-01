// Decompiled with JetBrains decompiler
// Type: SRPG.TouchController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  [AddComponentMenu("Event/TouchController")]
  public class TouchController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
  {
    private const float DragStartThreshold = 0.1f;
    public TouchController.ClickEvent OnClick;
    public TouchController.DragEvent OnDragDelegate;
    public TouchController.DragEvent OnDragEndDelegate;
    [NonSerialized]
    public Vector2 Velocity;
    [NonSerialized]
    public Vector2 AngularVelocity;
    private Vector2 mClickPos;
    private bool mMultiTouched;
    private bool mIsTouching;
    private float mPointerDownTime;
    [NonSerialized]
    public Vector2 DragDelta;
    private float mClickRadiusThreshold;
    private RectTransform mRectTransform;
    private Vector2 mDragStart;

    public TouchController()
    {
      base.\u002Ector();
    }

    public RectTransform GetRectTransform()
    {
      return this.mRectTransform;
    }

    public bool IsTouching
    {
      get
      {
        return this.mIsTouching;
      }
    }

    public Vector2 DragStart
    {
      get
      {
        return this.mDragStart;
      }
    }

    public Vector2 WorldSpaceVelocity
    {
      get
      {
        Camera main = Camera.get_main();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) main, (UnityEngine.Object) null))
          return Vector2.get_zero();
        Transform transform = ((Component) main).get_transform();
        Vector3 forward = transform.get_forward();
        Vector3 right = transform.get_right();
        forward.y = (__Null) 0.0;
        ((Vector3) @forward).Normalize();
        right.y = (__Null) 0.0;
        ((Vector3) @right).Normalize();
        return new Vector2((float) (right.x * this.Velocity.x + forward.x * this.Velocity.y), (float) (right.z * this.Velocity.x + forward.z * this.Velocity.y));
      }
    }

    public void IgnoreCurrentTouch()
    {
      this.mIsTouching = false;
      this.Velocity = Vector2.get_zero();
      this.AngularVelocity = Vector2.get_zero();
    }

    private void Awake()
    {
      this.mRectTransform = (RectTransform) ((Component) this).GetComponent<RectTransform>();
    }

    private void Start()
    {
      this.mClickRadiusThreshold = (float) (Screen.get_height() / 18);
    }

    private void Update()
    {
      if (!SRPG_TouchInputModule.IsMultiTouching)
        return;
      this.mMultiTouched = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      this.mClickPos = eventData.get_position();
      this.mMultiTouched = false;
      this.mIsTouching = true;
      this.DragDelta = Vector2.get_zero();
      this.mPointerDownTime = Time.get_time();
      this.AngularVelocity = Vector2.get_zero();
      this.Velocity = Vector2.get_zero();
      this.mDragStart = Vector2.op_Implicit(((Component) this).get_transform().InverseTransformPoint(Vector2.op_Implicit(eventData.get_position())));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      this.mIsTouching = false;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) eventData.get_pointerPress(), (UnityEngine.Object) ((Component) this).get_gameObject()))
      {
        Vector2 vector2 = Vector2.op_Subtraction(this.mClickPos, eventData.get_position());
        // ISSUE: explicit reference operation
        if ((double) ((Vector2) @vector2).get_magnitude() <= (double) this.mClickRadiusThreshold && !this.mMultiTouched && this.OnClick != null)
          this.OnClick(eventData.get_position());
      }
      if (this.OnDragEndDelegate == null)
        return;
      this.OnDragEndDelegate();
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!this.mIsTouching || (double) Time.get_time() < (double) this.mPointerDownTime + 0.100000001490116)
        return;
      if (SRPG_TouchInputModule.IsMultiTouching)
      {
        this.AngularVelocity = eventData.get_delta();
        this.DragDelta = Vector2.get_zero();
      }
      else
      {
        Vector2 vector2 = Vector2.op_Implicit(((Component) this).get_transform().InverseTransformPoint(Vector2.op_Implicit(eventData.get_position())));
        this.Velocity = eventData.get_delta();
        this.DragDelta = Vector2.op_Subtraction(vector2, this.mDragStart);
        if (this.OnDragDelegate == null)
          return;
        this.OnDragDelegate();
      }
    }

    public delegate void ClickEvent(Vector2 screenPos);

    public delegate void DragEvent();
  }
}
