// Decompiled with JetBrains decompiler
// Type: SRPG.StatusEffect
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  public class StatusEffect : MonoBehaviour
  {
    public GameObject[] StatusSlot;
    private bool[] mNowConditions;
    private float mElapsed;
    private int mActiveParamCount;

    public StatusEffect()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mNowConditions = new bool[(int) Unit.MAX_UNIT_CONDITION];
      this.Reset();
    }

    private void Reset()
    {
      if (this.StatusSlot != null)
      {
        for (int index = 0; index < this.StatusSlot.Length; ++index)
          this.StatusSlot[index].SetActive(false);
      }
      if (this.mNowConditions != null)
        this.mNowConditions.Initialize();
      this.mElapsed = 0.0f;
      this.mActiveParamCount = 0;
    }

    public void SetStatus(Unit unit)
    {
      this.Reset();
      EUnitCondition[] values = (EUnitCondition[]) Enum.GetValues(typeof (EUnitCondition));
      int index1 = 0;
      for (int index2 = 0; index2 < values.Length; ++index2)
      {
        if (unit.IsUnitCondition(values[index2]))
        {
          this.mNowConditions[index2] = true;
          ++this.mActiveParamCount;
          if (index1 < this.StatusSlot.Length)
          {
            this.StatusSlot[index1].SetActive(true);
            ((ImageArray) this.StatusSlot[index1].GetComponent<ImageArray>()).ImageIndex = index2;
            ++index1;
          }
        }
        else
          this.mNowConditions[index2] = false;
      }
    }

    private void Update()
    {
      int index1 = 0;
      if (this.mActiveParamCount > 0)
      {
        int num1 = (int) ((double) this.mElapsed / 2.0);
        if ((this.mActiveParamCount - 1) / this.StatusSlot.Length < num1)
        {
          this.mElapsed = 0.0f;
          num1 = 0;
        }
        int num2 = 0;
        for (int index2 = 0; index2 < this.mNowConditions.Length; ++index2)
        {
          if (this.mNowConditions[index2])
          {
            if (num2 - num1 * this.StatusSlot.Length < 0)
            {
              ++num2;
            }
            else
            {
              this.StatusSlot[index1].SetActive(true);
              ((ImageArray) this.StatusSlot[index1].GetComponent<ImageArray>()).ImageIndex = index2;
              if (++index1 >= this.StatusSlot.Length)
                break;
            }
          }
        }
      }
      if (index1 < this.StatusSlot.Length - 1)
      {
        do
        {
          this.StatusSlot[index1].SetActive(false);
        }
        while (++index1 < this.StatusSlot.Length);
      }
      this.mElapsed += Time.get_deltaTime();
    }
  }
}
