// Decompiled with JetBrains decompiler
// Type: SRPG.TouchControlArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class TouchControlArea : MonoBehaviour
  {
    private static readonly float vMin = 1f;
    private static readonly float vMax = 1.4f;
    public string TargetObjID;
    public string SillObjID;
    [SerializeField]
    private Button ResetButton;
    private GameObject TargetObj;
    private GameObject SillObj;
    private Vector3 sPos;
    private Quaternion sRot;
    private float tx;
    private float ty;
    private float v;
    private Vector3 targetScale;
    private Vector3 sillScale;
    private float wid;

    public TouchControlArea()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.ResetButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.ResetButton.get_onClick()).AddListener(new UnityAction((object) this, __methodptr(Reset)));
      }
      if (!string.IsNullOrEmpty(this.TargetObjID))
        this.TargetObj = GameObjectID.FindGameObject(this.TargetObjID);
      if (!string.IsNullOrEmpty(this.SillObjID))
        this.SillObj = GameObjectID.FindGameObject(this.SillObjID);
      this.wid = (float) (Screen.get_width() / 5);
      if (Object.op_Inequality((Object) this.TargetObj, (Object) null))
        this.targetScale = this.TargetObj.get_transform().get_localScale();
      if (!Object.op_Inequality((Object) this.SillObj, (Object) null))
        return;
      this.sillScale = this.SillObj.get_transform().get_localScale();
    }

    private void Update()
    {
      this.GetTouch();
    }

    public void Reset()
    {
      this.TargetObj.get_transform().set_rotation(Quaternion.get_identity());
      this.TargetObj.get_transform().Rotate(new Vector3(0.0f, 180f, 0.0f), (Space) 0);
      this.TargetObj.get_transform().set_localScale(this.targetScale);
      this.SillObj.get_transform().set_localScale(this.sillScale);
      this.v = TouchControlArea.vMin;
    }

    private void GetTouch()
    {
      TouchControlArea.TouchState touchState = TouchControlArea.TouchState.None;
      if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        touchState = TouchControlArea.TouchState.Began;
      if (touchState == TouchControlArea.TouchState.None && (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        touchState = TouchControlArea.TouchState.Moved;
      if (touchState == TouchControlArea.TouchState.None && (Input.GetMouseButtonUp(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        touchState = TouchControlArea.TouchState.Ended;
      switch (touchState)
      {
        case TouchControlArea.TouchState.Began:
          this.sPos = Input.get_mousePosition();
          this.sRot = this.TargetObj.get_transform().get_rotation();
          break;
        case TouchControlArea.TouchState.Moved:
          float num1 = (float) (Input.get_mousePosition().x - this.sPos.x) / this.wid;
          float num2 = 0.0f;
          this.TargetObj.get_transform().set_rotation(this.sRot);
          this.TargetObj.get_transform().Rotate(new Vector3(90f * num2, -90f * num1, 0.0f), (Space) 0);
          break;
      }
      float axis = Input.GetAxis("Mouse ScrollWheel");
      if ((double) axis == 0.0)
        return;
      this.v += axis;
      if ((double) this.v >= (double) TouchControlArea.vMax)
        this.v = TouchControlArea.vMax;
      if ((double) this.v < (double) TouchControlArea.vMin)
        this.v = TouchControlArea.vMin;
      this.TargetObj.get_transform().set_localScale(Vector3.op_Multiply(this.targetScale, this.v));
      this.SillObj.get_transform().set_localScale(Vector3.op_Multiply(this.sillScale, this.v));
    }

    public enum TouchState
    {
      None = -1,
      Began = 0,
      Moved = 1,
      Stationary = 2,
      Ended = 3,
      Canceled = 4,
    }
  }
}
