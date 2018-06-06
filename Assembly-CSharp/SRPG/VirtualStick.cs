// Decompiled with JetBrains decompiler
// Type: SRPG.VirtualStick
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class VirtualStick : MonoBehaviour
  {
    public static VirtualStick Instance;
    public RectTransform VirtualStickBG;
    public RectTransform VirtualStickFG;
    public RectTransform TouchArea;
    private bool mTouched;
    private Vector3 mTouchStart;
    private Vector3 mTouchPos;
    private Vector3 mVelocity;
    public string OpenFlagName;

    public VirtualStick()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      if (!Object.op_Equality((Object) VirtualStick.Instance, (Object) null))
        return;
      VirtualStick.Instance = this;
    }

    private void OnDisable()
    {
      if (!Object.op_Equality((Object) VirtualStick.Instance, (Object) this))
        return;
      VirtualStick.Instance = (VirtualStick) null;
    }

    public Vector2 GetVelocity(Transform cameraTransform)
    {
      if (!Object.op_Inequality((Object) cameraTransform, (Object) null))
        return Vector2.op_Implicit(this.mVelocity);
      Vector3 forward = cameraTransform.get_forward();
      Vector3 right = cameraTransform.get_right();
      forward.y = (__Null) 0.0;
      // ISSUE: explicit reference operation
      ((Vector3) @forward).Normalize();
      right.y = (__Null) 0.0;
      // ISSUE: explicit reference operation
      ((Vector3) @right).Normalize();
      return new Vector2((float) (right.x * this.mVelocity.x + forward.x * this.mVelocity.y), (float) (right.z * this.mVelocity.x + forward.z * this.mVelocity.y));
    }

    private void Start()
    {
      UIEventListener uiEventListener = UIEventListener.Get((Component) this.TouchArea);
      uiEventListener.onPointerDown = (UIEventListener.PointerEvent) (eventData =>
      {
        ((Animator) ((Component) this.VirtualStickBG).GetComponent<Animator>()).SetBool(this.OpenFlagName, true);
        RaycastResult pointerCurrentRaycast = eventData.get_pointerCurrentRaycast();
        // ISSUE: explicit reference operation
        Vector3 vector3 = ((RaycastResult) @pointerCurrentRaycast).get_gameObject().get_transform().InverseTransformPoint(Vector2.op_Implicit(eventData.get_position()));
        ((RectTransform) ((Component) this.VirtualStickBG).get_transform()).set_anchoredPosition(new Vector2((float) vector3.x, (float) vector3.y));
        this.mTouchStart = vector3;
        this.mTouchPos = vector3;
        this.mTouched = true;
        this.mVelocity = Vector3.get_zero();
      });
      uiEventListener.onPointerUp = (UIEventListener.PointerEvent) (eventData =>
      {
        ((Animator) ((Component) this.VirtualStickBG).GetComponent<Animator>()).SetBool(this.OpenFlagName, false);
        this.mTouched = false;
        this.mVelocity = Vector3.get_zero();
      });
      uiEventListener.onDrag = (UIEventListener.PointerEvent) (eventData => this.mTouchPos = eventData.get_pointerPress().get_transform().InverseTransformPoint(Vector2.op_Implicit(eventData.get_position())));
    }

    private void Update()
    {
      if (!this.mTouched)
        return;
      Vector3 vector3 = Vector3.op_Subtraction(this.mTouchPos, this.mTouchStart);
      RectTransform transform = (RectTransform) ((Component) this.VirtualStickFG).get_transform();
      float num = (float) ((((RectTransform) ((Component) this.VirtualStickBG).get_transform()).get_sizeDelta().x - transform.get_sizeDelta().x) * 0.5);
      // ISSUE: explicit reference operation
      if ((double) ((Vector3) @vector3).get_magnitude() >= (double) num)
      {
        // ISSUE: explicit reference operation
        vector3 = Vector3.op_Multiply(((Vector3) @vector3).get_normalized(), num);
      }
      transform.set_anchoredPosition(Vector2.op_Implicit(vector3));
      this.mVelocity = Vector3.op_Multiply(vector3, 1f / num);
    }
  }
}
