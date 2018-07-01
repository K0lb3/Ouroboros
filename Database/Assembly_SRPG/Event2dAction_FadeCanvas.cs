// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_FadeCanvas
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/キャンバスフェード(2D)", "Canvasをフェードさせます", 5592405, 4473992)]
  public class Event2dAction_FadeCanvas : EventAction
  {
    public AnimationCurve Curve = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.0f), new Keyframe(1f, 1f) });
    public float Time = 1f;
    public string CanvasID;
    private CanvasGroup[] mCanvasGroup;
    private float mTime;
    public bool async;

    public override void OnActivate()
    {
      GameObject[] gameObjects = GameObjectID.FindGameObjects(this.CanvasID);
      if (gameObjects.Length > 0)
      {
        this.mCanvasGroup = new CanvasGroup[gameObjects.Length];
        for (int index = 0; index < gameObjects.Length; ++index)
          this.mCanvasGroup[index] = GameUtility.RequireComponent<CanvasGroup>(gameObjects[index]);
        if (!this.async)
          return;
        this.ActivateNext(true);
      }
      else
        this.ActivateNext();
    }

    public override void Update()
    {
      this.mTime += UnityEngine.Time.get_deltaTime();
      for (int index = 0; index < this.mCanvasGroup.Length; ++index)
      {
        if (Object.op_Inequality((Object) this.mCanvasGroup[index], (Object) null))
        {
          float num1;
          if ((double) this.Time > 0.0)
          {
            AnimationCurve curve = this.Curve;
            double num2 = (double) Mathf.Clamp01(this.mTime / this.Time);
            Keyframe keyframe = this.Curve.get_Item(this.Curve.get_length() - 1);
            // ISSUE: explicit reference operation
            double time = (double) ((Keyframe) @keyframe).get_time();
            double num3 = num2 * time;
            num1 = curve.Evaluate((float) num3);
          }
          else
          {
            AnimationCurve curve = this.Curve;
            Keyframe keyframe = this.Curve.get_Item(this.Curve.get_length() - 1);
            // ISSUE: explicit reference operation
            double time = (double) ((Keyframe) @keyframe).get_time();
            num1 = curve.Evaluate((float) time);
          }
          this.mCanvasGroup[index].set_alpha(Mathf.Clamp01(num1));
        }
      }
      if ((double) this.mTime < (double) this.Time)
        return;
      if (this.async)
        this.enabled = false;
      else
        this.ActivateNext();
    }
  }
}
