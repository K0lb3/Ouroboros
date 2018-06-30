namespace SRPG
{
    using GR;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using UnityEngine;
    using UnityEngine.UI;

    public class UnitAttackRangeWindow : MonoBehaviour
    {
        public Transform Parent;
        public GameObject RangeTemplate;
        public GameObject SpaceTemplate;
        public Text RangeMinMax;
        public int BlockSize;
        private static readonly int RANGE_BLOCK_MAX;

        static UnitAttackRangeWindow()
        {
            RANGE_BLOCK_MAX = 7;
            return;
        }

        public UnitAttackRangeWindow()
        {
            this.BlockSize = 0x19;
            base..ctor();
            return;
        }

        private unsafe List<string> GetAllRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            string str;
            list = new List<string>();
            num = -max;
            goto Label_0045;
        Label_000E:
            num2 = -max;
            goto Label_003A;
        Label_0016:
            str = &num2.ToString() + "," + &num.ToString();
            list.Add(str);
            num2 += 1;
        Label_003A:
            if (num2 <= max)
            {
                goto Label_0016;
            }
            num += 1;
        Label_0045:
            if (num <= max)
            {
                goto Label_000E;
            }
            return list;
        }

        private unsafe List<string> GetBishopRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            string str;
            float num3;
            float num4;
            list = new List<string>();
            num = -max;
            goto Label_00B1;
        Label_000E:
            if (min <= 0)
            {
                goto Label_002D;
            }
            if (max <= 0)
            {
                goto Label_002D;
            }
            if (min < Math.Abs(num))
            {
                goto Label_002D;
            }
            goto Label_00AD;
        Label_002D:
            num2 = -max;
            goto Label_00A6;
        Label_0035:
            if (min <= 0)
            {
                goto Label_0054;
            }
            if (max <= 0)
            {
                goto Label_0054;
            }
            if (min < Math.Abs(num2))
            {
                goto Label_0054;
            }
            goto Label_00A2;
        Label_0054:
            if (Math.Abs(num2) == Math.Abs(num))
            {
                goto Label_006A;
            }
            goto Label_00A2;
        Label_006A:
            num3 = &target.x + ((float) num2);
            num4 = &target.y + ((float) num);
            str = &num3.ToString() + "," + &num4.ToString();
            list.Add(str);
        Label_00A2:
            num2 += 1;
        Label_00A6:
            if (num2 <= max)
            {
                goto Label_0035;
            }
        Label_00AD:
            num += 1;
        Label_00B1:
            if (num <= max)
            {
                goto Label_000E;
            }
            return list;
        }

        private unsafe List<string> GetCrossRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str;
            list = new List<string>();
            num = min + 1;
            goto Label_007F;
        Label_000F:
            num2 = 0;
            goto Label_0074;
        Label_0016:
            num3 = Unit.DIRECTION_OFFSETS[num2, 0] * num;
            num4 = Unit.DIRECTION_OFFSETS[num2, 1] * num;
            num5 = ((int) &target.x) + num3;
            num6 = ((int) &target.y) + num4;
            str = &num5.ToString() + "," + &num6.ToString();
            list.Add(str);
            num2 += 1;
        Label_0074:
            if (num2 < 4)
            {
                goto Label_0016;
            }
            num += 1;
        Label_007F:
            if (num <= max)
            {
                goto Label_000F;
            }
            return list;
        }

        private unsafe List<string> GetDiamondRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            int num3;
            string str;
            float num4;
            float num5;
            list = new List<string>();
            num = -max;
            goto Label_00A3;
        Label_000E:
            num2 = -max;
            goto Label_0098;
        Label_0016:
            num3 = Math.Abs(num2) + Math.Abs(num);
            if (num3 <= max)
            {
                goto Label_0030;
            }
            goto Label_0094;
        Label_0030:
            if (min <= 0)
            {
                goto Label_005A;
            }
            if (max <= 0)
            {
                goto Label_005A;
            }
            if ((Math.Abs(0 - num2) + Math.Abs(0 - num)) > min)
            {
                goto Label_005A;
            }
            goto Label_0094;
        Label_005A:
            num4 = &target.x + ((float) num2);
            num5 = &target.y + ((float) num);
            str = &num4.ToString() + "," + &num5.ToString();
            list.Add(str);
        Label_0094:
            num2 += 1;
        Label_0098:
            if (num2 <= max)
            {
                goto Label_0016;
            }
            num += 1;
        Label_00A3:
            if (num <= max)
            {
                goto Label_000E;
            }
            return list;
        }

        private unsafe List<string> GetHorseRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            string str;
            float num3;
            float num4;
            list = new List<string>();
            max += 1;
            num = -max;
            goto Label_00CE;
        Label_0013:
            if (min <= 0)
            {
                goto Label_0032;
            }
            if (max <= 0)
            {
                goto Label_0032;
            }
            if (min < Math.Abs(min))
            {
                goto Label_0032;
            }
            goto Label_00CA;
        Label_0032:
            num2 = -max;
            goto Label_00C3;
        Label_003A:
            if (min <= 0)
            {
                goto Label_0059;
            }
            if (max <= 0)
            {
                goto Label_0059;
            }
            if (min < Math.Abs(min))
            {
                goto Label_0059;
            }
            goto Label_00BF;
        Label_0059:
            if (Math.Abs(num2) == Math.Abs(num))
            {
                goto Label_0087;
            }
            if (Math.Abs(num2) > 1)
            {
                goto Label_00BF;
            }
            if (Math.Abs(num) <= 1)
            {
                goto Label_0087;
            }
            goto Label_00BF;
        Label_0087:
            num3 = &target.x + ((float) num2);
            num4 = &target.y + ((float) num);
            str = &num3.ToString() + "," + &num4.ToString();
            list.Add(str);
        Label_00BF:
            num2 += 1;
        Label_00C3:
            if (num2 <= max)
            {
                goto Label_003A;
            }
        Label_00CA:
            num += 1;
        Label_00CE:
            if (num <= max)
            {
                goto Label_0013;
            }
            return list;
        }

        private List<string> GetLaserRange(Vector2 target, int min, int max)
        {
            List<string> list;
            list = new List<string>();
            return list;
        }

        private unsafe List<string> GetSquareRange(Vector2 target, int min, int max)
        {
            List<string> list;
            int num;
            int num2;
            int num3;
            int num4;
            string str;
            list = new List<string>();
            num = -max;
            goto Label_007D;
        Label_000E:
            num2 = -max;
            goto Label_0072;
        Label_0016:
            if (min <= 0)
            {
                goto Label_0035;
            }
            if (max <= 0)
            {
                goto Label_0035;
            }
            if (min < Math.Abs(num2))
            {
                goto Label_0035;
            }
            goto Label_006E;
        Label_0035:
            num3 = ((int) &target.x) + num2;
            num4 = ((int) &target.y) + num;
            str = &num3.ToString() + "," + &num4.ToString();
            list.Add(str);
        Label_006E:
            num2 += 1;
        Label_0072:
            if (num2 <= max)
            {
                goto Label_0016;
            }
            num += 1;
        Label_007D:
            if (num <= max)
            {
                goto Label_000E;
            }
            return list;
        }

        private unsafe void Start()
        {
            object[] objArray2;
            object[] objArray1;
            UnitData data;
            JobData data2;
            long num;
            int num2;
            SkillData data3;
            int num3;
            int num4;
            ESelectType type;
            GridLayoutGroup group;
            int num5;
            List<string> list;
            int num6;
            int num7;
            string str;
            GameObject obj2;
            GameObject obj3;
            Image image;
            string str2;
            ESelectType type2;
            if ((this.RangeTemplate != null) == null)
            {
                goto Label_001D;
            }
            this.RangeTemplate.SetActive(0);
        Label_001D:
            if ((this.SpaceTemplate != null) == null)
            {
                goto Label_003A;
            }
            this.SpaceTemplate.SetActive(0);
        Label_003A:
            data = null;
            data = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID(GlobalVars.SelectedUnitUniqueID);
            if (data != null)
            {
                goto Label_0067;
            }
            DebugUtility.Log("Not Selected Unit!!");
            return;
        Label_0067:
            data2 = data.CurrentJob;
            num = GlobalVars.SelectedJobUniqueID;
            num2 = 0;
            goto Label_00A5;
        Label_0080:
            if (data.Jobs[num2].UniqueID != num)
            {
                goto Label_00A1;
            }
            data2 = data.Jobs[num2];
            goto Label_00B3;
        Label_00A1:
            num2 += 1;
        Label_00A5:
            if (num2 < ((int) data.Jobs.Length))
            {
                goto Label_0080;
            }
        Label_00B3:
            data3 = data2.GetAttackSkill();
            num3 = data.GetAttackRangeMax(data3);
            num4 = data.GetAttackRangeMin(data3);
            type = data3.SkillParam.select_range;
            group = this.Parent.GetComponent<GridLayoutGroup>();
            if ((group == null) == null)
            {
                goto Label_0102;
            }
            DebugUtility.Log("Parent is not attachment GridLayoutGroup");
            return;
        Label_0102:
            group.set_constraintCount(Mathf.Max((num3 * 2) + 1, RANGE_BLOCK_MAX));
            if (group.get_constraintCount() <= (RANGE_BLOCK_MAX + 1))
            {
                goto Label_015C;
            }
            group.set_cellSize(new Vector2((float) this.BlockSize, (float) this.BlockSize));
            group.set_spacing(new Vector2(5f, 5f));
        Label_015C:
            num5 = group.get_constraintCount() / 2;
            list = new List<string>();
            type2 = type;
            switch (type2)
            {
                case 0:
                    goto Label_0256;

                case 1:
                    goto Label_01AE;

                case 2:
                    goto Label_01CA;

                case 3:
                    goto Label_01E6;

                case 4:
                    goto Label_0202;

                case 5:
                    goto Label_0256;

                case 6:
                    goto Label_0256;

                case 7:
                    goto Label_021E;

                case 8:
                    goto Label_0256;

                case 9:
                    goto Label_0256;

                case 10:
                    goto Label_0256;

                case 11:
                    goto Label_023A;
            }
            goto Label_0256;
        Label_01AE:
            list = this.GetDiamondRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_01CA:
            list = this.GetSquareRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_01E6:
            list = this.GetLaserRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_0202:
            list = this.GetAllRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_021E:
            list = this.GetBishopRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_023A:
            list = this.GetHorseRange(new Vector2((float) num5, (float) num5), num4, num3);
            goto Label_0272;
        Label_0256:
            list = this.GetCrossRange(new Vector2((float) num5, (float) num5), num4, num3);
        Label_0272:
            num6 = 0;
            goto Label_03A4;
        Label_027A:
            num7 = 0;
            goto Label_0390;
        Label_0282:
            str = &num7.ToString() + "," + &num6.ToString();
            obj2 = this.SpaceTemplate;
            if (list.IndexOf(str) != -1)
            {
                goto Label_02C5;
            }
            if (num7 != num5)
            {
                goto Label_02CD;
            }
            if (num6 != num5)
            {
                goto Label_02CD;
            }
        Label_02C5:
            obj2 = this.RangeTemplate;
        Label_02CD:
            obj3 = Object.Instantiate<GameObject>(obj2);
            obj3.get_transform().SetParent(this.Parent, 0);
            obj3.set_name("Grid" + &num7.ToString() + "-" + &num6.ToString());
            obj3.SetActive(1);
            if ((obj2 == this.SpaceTemplate) == null)
            {
                goto Label_032C;
            }
            goto Label_038A;
        Label_032C:
            image = obj3.GetComponent<Image>();
            if (num7 != num5)
            {
                goto Label_036D;
            }
            if (num6 != num5)
            {
                goto Label_036D;
            }
            image.set_color(new Color32(0, 0xff, 0xff, 0xff));
            goto Label_038A;
        Label_036D:
            image.set_color(new Color32(0xff, 0, 0, 0xff));
        Label_038A:
            num7 += 1;
        Label_0390:
            if (num7 < group.get_constraintCount())
            {
                goto Label_0282;
            }
            num6 += 1;
        Label_03A4:
            if (num6 < group.get_constraintCount())
            {
                goto Label_027A;
            }
            num4 += 1;
            objArray1 = new object[] { &num4.ToString(), &num3.ToString() };
            str2 = LocalizedText.Get("sys.TEXT_RANGE_DEFAULT", objArray1);
            if (num4 != num3)
            {
                goto Label_0403;
            }
            objArray2 = new object[] { (int) num3 };
            str2 = LocalizedText.Get("sys.TEXT_RANGE_MINMAX_EQUAL", objArray2);
        Label_0403:
            if ((this.RangeMinMax != null) == null)
            {
                goto Label_0421;
            }
            this.RangeMinMax.set_text(str2);
        Label_0421:
            return;
        }
    }
}

