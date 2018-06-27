// Decompiled with JetBrains decompiler
// Type: InputToEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

public class InputToEvent : MonoBehaviour
{
  private GameObject lastGo;
  public static Vector3 inputHitPos;
  public bool DetectPointedAtGameObject;
  private Vector2 pressedPosition;
  private Vector2 currentPos;
  public bool Dragging;
  private Camera m_Camera;

  public InputToEvent()
  {
    base.\u002Ector();
  }

  public static GameObject goPointedAt { get; private set; }

  public Vector2 DragVector
  {
    get
    {
      if (this.Dragging)
        return Vector2.op_Subtraction(this.currentPos, this.pressedPosition);
      return Vector2.get_zero();
    }
  }

  private void Start()
  {
    this.m_Camera = (Camera) ((Component) this).GetComponent<Camera>();
  }

  private void Update()
  {
    if (this.DetectPointedAtGameObject)
      InputToEvent.goPointedAt = this.RaycastObject(Vector2.op_Implicit(Input.get_mousePosition()));
    if (Input.get_touchCount() > 0)
    {
      Touch touch = Input.GetTouch(0);
      // ISSUE: explicit reference operation
      this.currentPos = ((Touch) @touch).get_position();
      // ISSUE: explicit reference operation
      if (((Touch) @touch).get_phase() == null)
      {
        // ISSUE: explicit reference operation
        this.Press(((Touch) @touch).get_position());
      }
      else
      {
        // ISSUE: explicit reference operation
        if (((Touch) @touch).get_phase() != 3)
          return;
        // ISSUE: explicit reference operation
        this.Release(((Touch) @touch).get_position());
      }
    }
    else
    {
      this.currentPos = Vector2.op_Implicit(Input.get_mousePosition());
      if (Input.GetMouseButtonDown(0))
        this.Press(Vector2.op_Implicit(Input.get_mousePosition()));
      if (Input.GetMouseButtonUp(0))
        this.Release(Vector2.op_Implicit(Input.get_mousePosition()));
      if (!Input.GetMouseButtonDown(1))
        return;
      this.pressedPosition = Vector2.op_Implicit(Input.get_mousePosition());
      this.lastGo = this.RaycastObject(this.pressedPosition);
      if (!Object.op_Inequality((Object) this.lastGo, (Object) null))
        return;
      this.lastGo.SendMessage("OnPressRight", (SendMessageOptions) 1);
    }
  }

  private void Press(Vector2 screenPos)
  {
    this.pressedPosition = screenPos;
    this.Dragging = true;
    this.lastGo = this.RaycastObject(screenPos);
    if (!Object.op_Inequality((Object) this.lastGo, (Object) null))
      return;
    this.lastGo.SendMessage("OnPress", (SendMessageOptions) 1);
  }

  private void Release(Vector2 screenPos)
  {
    if (Object.op_Inequality((Object) this.lastGo, (Object) null))
    {
      if (Object.op_Equality((Object) this.RaycastObject(screenPos), (Object) this.lastGo))
        this.lastGo.SendMessage("OnClick", (SendMessageOptions) 1);
      this.lastGo.SendMessage("OnRelease", (SendMessageOptions) 1);
      this.lastGo = (GameObject) null;
    }
    this.pressedPosition = Vector2.get_zero();
    this.Dragging = false;
  }

  private GameObject RaycastObject(Vector2 screenPos)
  {
    RaycastHit raycastHit;
    if (!Physics.Raycast(this.m_Camera.ScreenPointToRay(Vector2.op_Implicit(screenPos)), ref raycastHit, 200f))
      return (GameObject) null;
    // ISSUE: explicit reference operation
    InputToEvent.inputHitPos = ((RaycastHit) @raycastHit).get_point();
    // ISSUE: explicit reference operation
    return ((Component) ((RaycastHit) @raycastHit).get_collider()).get_gameObject();
  }
}
