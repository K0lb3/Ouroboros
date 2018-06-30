namespace SRPG
{
    using System;
    using UnityEngine;

    public class StatusEffect : MonoBehaviour
    {
        public GameObject[] StatusSlot;
        private bool[] mNowConditions;
        private float mElapsed;
        private int mActiveParamCount;

        public StatusEffect()
        {
            base..ctor();
            return;
        }

        private void Reset()
        {
            int num;
            if (this.StatusSlot == null)
            {
                goto Label_0032;
            }
            num = 0;
            goto Label_0024;
        Label_0012:
            this.StatusSlot[num].SetActive(0);
            num += 1;
        Label_0024:
            if (num < ((int) this.StatusSlot.Length))
            {
                goto Label_0012;
            }
        Label_0032:
            if (this.mNowConditions == null)
            {
                goto Label_0048;
            }
            this.mNowConditions.Initialize();
        Label_0048:
            this.mElapsed = 0f;
            this.mActiveParamCount = 0;
            return;
        }

        public void SetStatus(Unit unit)
        {
            EUnitCondition[] conditionArray;
            int num;
            int num2;
            ImageArray array;
            int num3;
            ImageArray array2;
            this.Reset();
            conditionArray = (EUnitCondition[]) Enum.GetValues(typeof(EUnitCondition));
            num = 0;
            num2 = 0;
            goto Label_00AA;
        Label_0024:
            if (unit.IsUnitCondition(conditionArray[num2]) == null)
            {
                goto Label_009D;
            }
            this.mNowConditions[num2] = 1;
            this.mActiveParamCount += 1;
            if (num >= ((int) this.StatusSlot.Length))
            {
                goto Label_00A6;
            }
            this.StatusSlot[num].SetActive(1);
            array = this.StatusSlot[num].GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_0094;
            }
            if (num2 >= ((int) array.Images.Length))
            {
                goto Label_0094;
            }
            array.ImageIndex = num2;
        Label_0094:
            num += 1;
            goto Label_00A6;
        Label_009D:
            this.mNowConditions[num2] = 0;
        Label_00A6:
            num2 += 1;
        Label_00AA:
            if (num2 < ((int) conditionArray.Length))
            {
                goto Label_0024;
            }
            num3 = (int) conditionArray.Length;
            if (num3 >= ((int) this.mNowConditions.Length))
            {
                goto Label_014E;
            }
            this.mNowConditions[num3] = 0;
            if (unit.Shields.Count == null)
            {
                goto Label_014E;
            }
            this.mNowConditions[num3] = 1;
            this.mActiveParamCount += 1;
            if (num >= ((int) this.StatusSlot.Length))
            {
                goto Label_014E;
            }
            this.StatusSlot[num].SetActive(1);
            array2 = this.StatusSlot[num].GetComponent<ImageArray>();
            if ((array2 != null) == null)
            {
                goto Label_014A;
            }
            if (num3 >= ((int) array2.Images.Length))
            {
                goto Label_014A;
            }
            array2.ImageIndex = num3;
        Label_014A:
            num += 1;
        Label_014E:
            return;
        }

        private void Start()
        {
            this.mNowConditions = new bool[Unit.MAX_UNIT_CONDITION + 1];
            this.Reset();
            return;
        }

        private void Update()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            ImageArray array;
            num = 0;
            if (this.mActiveParamCount <= 0)
            {
                goto Label_00E4;
            }
            num2 = (int) (this.mElapsed / 2f);
            num3 = (this.mActiveParamCount - 1) / ((int) this.StatusSlot.Length);
            if (num3 >= num2)
            {
                goto Label_0042;
            }
            this.mElapsed = 0f;
            num2 = 0;
        Label_0042:
            num4 = 0;
            num5 = 0;
            goto Label_00D5;
        Label_004C:
            if (this.mNowConditions[num5] == null)
            {
                goto Label_00CF;
            }
            if ((num4 - (num2 * ((int) this.StatusSlot.Length))) >= 0)
            {
                goto Label_0075;
            }
            num4 += 1;
            goto Label_00CF;
        Label_0075:
            this.StatusSlot[num].SetActive(1);
            array = this.StatusSlot[num].GetComponent<ImageArray>();
            if ((array != null) == null)
            {
                goto Label_00B8;
            }
            if (num5 >= ((int) array.Images.Length))
            {
                goto Label_00B8;
            }
            array.ImageIndex = num5;
        Label_00B8:
            if ((num += 1) < ((int) this.StatusSlot.Length))
            {
                goto Label_00CF;
            }
            goto Label_00E4;
        Label_00CF:
            num5 += 1;
        Label_00D5:
            if (num5 < ((int) this.mNowConditions.Length))
            {
                goto Label_004C;
            }
        Label_00E4:
            if (num >= (((int) this.StatusSlot.Length) - 1))
            {
                goto Label_0114;
            }
        Label_00F4:
            this.StatusSlot[num].SetActive(0);
            if ((num += 1) < ((int) this.StatusSlot.Length))
            {
                goto Label_00F4;
            }
        Label_0114:
            this.mElapsed += Time.get_deltaTime();
            return;
        }
    }
}

