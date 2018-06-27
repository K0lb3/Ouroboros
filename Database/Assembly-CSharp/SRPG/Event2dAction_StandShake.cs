// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_StandShake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/立ち絵2/シェイク", "立ち絵2を揺らします", 5592405, 4473992)]
  public class Event2dAction_StandShake : EventAction
  {
    public float Duration = 0.5f;
    public float FrequencyX = 12.51327f;
    public float FrequencyY = 20.4651f;
    public float AmplitudeX = 0.1f;
    public float AmplitudeY = 0.1f;
    public string CharaID;
    public bool Async;
    private EventStandCharaController2 mStandChara;
    private RectTransform mStandCharaTransform;
    private float mSeedX;
    private float mSeedY;
    private float mTime;
    private Vector2 originalPvt;

    public override void PreStart()
    {
      if (!Object.op_Equality((Object) this.mStandChara, (Object) null))
        return;
      this.mStandChara = EventStandCharaController2.FindInstances(this.CharaID);
    }

    public override void OnActivate()
    {
      if (Object.op_Equality((Object) this.mStandChara, (Object) null))
      {
        this.ActivateNext();
      }
      else
      {
        this.mSeedX = Random.get_value();
        this.mSeedY = Random.get_value();
        this.mStandCharaTransform = (RectTransform) ((Component) this.mStandChara).GetComponent<RectTransform>();
        this.originalPvt = this.mStandCharaTransform.get_pivot();
        if (!this.Async)
          return;
        this.ActivateNext(true);
      }
    }

    public override void Update()
    {
      this.mTime += Time.get_deltaTime();
      if ((double) this.mTime >= (double) this.Duration)
      {
        this.mStandCharaTransform.set_pivot(this.originalPvt);
        if (this.Async)
          this.enabled = false;
        else
          this.ActivateNext();
      }
      else
      {
        float num1 = 1f - Mathf.Clamp01(this.mTime / this.Duration);
        float num2 = Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedX) * (double) this.FrequencyX * 3.14159274101257)) * this.AmplitudeX * num1;
        float num3 = Mathf.Sin((float) (((double) Time.get_time() + (double) this.mSeedY) * (double) this.FrequencyY * 3.14159274101257)) * this.AmplitudeY * num1;
        Vector2 vector2;
        // ISSUE: explicit reference operation
        ((Vector2) @vector2).\u002Ector((float) this.originalPvt.x + num2, (float) this.originalPvt.y + num3);
        this.mStandCharaTransform.set_pivot(vector2);
      }
    }
  }
}
