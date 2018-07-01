// Decompiled with JetBrains decompiler
// Type: SRPG.PreviewUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using SRPG.AnimEvents;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class PreviewUnitController : UnitController
  {
    private static readonly string SLOT0 = "0";
    public string UnitID = "UN010000";
    public string JobID = "ninja";
    private List<string> mCameraAnimationNames = new List<string>();
    public string mCurrentAnimation = string.Empty;
    public List<string> mAnimationFiles = new List<string>();
    private float mSpeed = 1f;
    private Vector3 mStartPosition;
    private Transform mCameraPos;
    private Transform mEnemyPos;
    private int mCameraID;
    private Quaternion mCameraShakeOffset;
    private float mCameraShakeSeedX;
    private float mCameraShakeSeedY;
    private AnimationClip mCameraAnimation;
    private bool mUseCamera;
    private Vector2 mAnimListScrollPos;
    private Vector2 mCameraAnimListScrollPos;
    private bool mLoadingAnimation;
    private bool mLoopAnimation;
    private bool mMirror;
    private bool mAnimationLoaded;

    private void Awake()
    {
      MonoSingleton<GameManager>.Instance.Deserialize(JSONParser.parseJSONObject<JSON_MasterParam>(((TextAsset) Resources.Load<TextAsset>("Data/MasterParam")).get_text()));
      UnitData unitData = new UnitData();
      unitData.Setup(this.UnitID, 0, 0, 0, this.JobID, 0, EElement.None, 0);
      this.SetupUnit(unitData, -1);
      this.mStartPosition = ((Component) this).get_transform().get_position();
    }

    protected override void PostSetup()
    {
    }

    protected override void OnEventStart(AnimEvent e, float weight)
    {
      if (e is ToggleCamera)
      {
        this.mCameraID = (e as ToggleCamera).CameraID;
        if (this.mCameraID >= 0 && this.mCameraID <= 1)
          return;
        Debug.LogError((object) ("Invalid CameraID: " + (object) this.mCameraID));
      }
      else
      {
        if (!(e is CameraShake))
          return;
        this.mCameraShakeSeedX = Random.get_value();
        this.mCameraShakeSeedY = Random.get_value();
      }
    }

    protected override void OnEvent(AnimEvent e, float time, float weight)
    {
      if (!(e is CameraShake))
        return;
      this.mCameraShakeOffset = (e as CameraShake).CalcOffset(time, this.mCameraShakeSeedX, this.mCameraShakeSeedY);
    }

    private new void Update()
    {
      this.mCameraShakeOffset = Quaternion.get_identity();
      base.Update();
      if (!this.mLoadingAnimation || this.IsLoading)
        return;
      this.mAnimationLoaded = true;
      this.mLoadingAnimation = false;
      ((Component) this).get_transform().set_position(this.mStartPosition);
      this.mCameraID = 0;
      this.PlayAnimation(PreviewUnitController.SLOT0, this.mLoopAnimation);
      AnimDef animation = this.GetAnimation(PreviewUnitController.SLOT0);
      if (Object.op_Inequality((Object) this.mCameraAnimation, (Object) null))
      {
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).AddClip(this.mCameraAnimation, PreviewUnitController.SLOT0);
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).Play(PreviewUnitController.SLOT0);
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).get_Item(PreviewUnitController.SLOT0).set_speed((float) (1.0 / (double) this.mCameraAnimation.get_length() * 1.0) / animation.Length);
      }
      else
      {
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).AddClip(animation.animation, PreviewUnitController.SLOT0);
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).Play(PreviewUnitController.SLOT0);
        ((Animation) ((Component) this.mCameraPos).GetComponent<Animation>()).get_Item(PreviewUnitController.SLOT0).set_speed(1f);
      }
      ((Animation) ((Component) this.mEnemyPos).GetComponent<Animation>()).AddClip(animation.animation, PreviewUnitController.SLOT0);
      ((Animation) ((Component) this.mEnemyPos).GetComponent<Animation>()).Play(PreviewUnitController.SLOT0);
    }

    private new void LateUpdate()
    {
      base.LateUpdate();
      if (!this.mUseCamera)
        return;
      Transform child = this.mCameraPos.FindChild(this.mCameraID != 0 ? "Camera002" : "Camera001");
      Transform transform = ((Component) Camera.get_main()).get_transform();
      Vector3 position = child.get_position();
      Quaternion quaternion = child.get_rotation();
      if (this.mMirror)
      {
        quaternion.z = -quaternion.z;
        quaternion.y = -quaternion.y;
      }
      quaternion = Quaternion.op_Multiply(quaternion, GameUtility.Yaw180);
      transform.set_position(position);
      transform.set_rotation(Quaternion.op_Multiply(this.mCameraShakeOffset, quaternion));
      Debug.DrawLine(position, Vector3.op_Addition(position, Vector3.op_Multiply(transform.get_forward(), 5f)), Color.get_yellow());
      PreviewUnitController.DrawAxis(position);
      Debug.DrawLine(child.get_position(), Vector3.op_Subtraction(child.get_position(), Vector3.op_Multiply(child.get_forward(), 5f)), Color.get_magenta());
      PreviewUnitController.DrawAxis(child.get_position());
    }

    private static void DrawAxis(Vector3 center)
    {
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.get_right()), Color.get_red());
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.get_up()), Color.get_green());
      Debug.DrawLine(center, Vector3.op_Addition(center, Vector3.get_forward()), Color.get_blue());
    }

    private void OnDrawGizmos()
    {
    }

    private void OnGUI()
    {
    }
  }
}
