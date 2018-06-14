// Decompiled with JetBrains decompiler
// Type: SRPG.TouchControlArea
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

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
    private float sDist;
    private float nDist;
    private float hei;
    private float diag;

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
      this.hei = (float) (Screen.get_height() / 5);
      this.diag = Mathf.Sqrt(Mathf.Pow(this.wid, 2f) + Mathf.Pow(this.hei, 2f));
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
      this.sDist = this.nDist = 0.0f;
      this.v = TouchControlArea.vMin;
    }

    private void GetTouch()
    {
      if (Input.get_touchCount() == 1)
      {
        Touch touch = Input.GetTouch(0);
        // ISSUE: explicit reference operation
        if (((Touch) @touch).get_phase() == null)
        {
          // ISSUE: explicit reference operation
          this.sPos = Vector2.op_Implicit(((Touch) @touch).get_position());
          this.sRot = this.TargetObj.get_transform().get_rotation();
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((Touch) @touch).get_phase() != 1 && ((Touch) @touch).get_phase() != 2)
            return;
          // ISSUE: explicit reference operation
          this.tx = (float) (((Touch) @touch).get_position().x - this.sPos.x) / this.wid;
          this.ty = 0.0f;
          this.TargetObj.get_transform().set_rotation(this.sRot);
          this.TargetObj.get_transform().Rotate(new Vector3(90f * this.ty, -90f * this.tx, 0.0f), (Space) 0);
        }
      }
      else
      {
        if (Input.get_touchCount() < 2)
          return;
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);
        // ISSUE: explicit reference operation
        if (((Touch) @touch2).get_phase() == null)
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          this.sDist = Vector2.Distance(((Touch) @touch1).get_position(), ((Touch) @touch2).get_position());
        }
        else
        {
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          if (((Touch) @touch1).get_phase() != 1 && ((Touch) @touch1).get_phase() != 2 || ((Touch) @touch2).get_phase() != 1 && ((Touch) @touch2).get_phase() != 2)
            return;
          // ISSUE: explicit reference operation
          // ISSUE: explicit reference operation
          this.nDist = Vector2.Distance(((Touch) @touch1).get_position(), ((Touch) @touch2).get_position());
          this.v += (this.nDist - this.sDist) / this.diag;
          this.v = (double) this.v <= (double) TouchControlArea.vMax ? this.v : TouchControlArea.vMax;
          this.v = (double) this.v > (double) TouchControlArea.vMin ? this.v : TouchControlArea.vMin;
          this.TargetObj.get_transform().set_localScale(Vector3.op_Multiply(this.targetScale, this.v));
          this.SillObj.get_transform().set_localScale(Vector3.op_Multiply(this.sillScale, this.v));
        }
      }
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
