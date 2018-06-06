// Decompiled with JetBrains decompiler
// Type: CodeStage.AntiCheat.Utils.xxHash
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace CodeStage.AntiCheat.Utils
{
  internal class xxHash
  {
    private const uint PRIME32_1 = 2654435761;
    private const uint PRIME32_2 = 2246822519;
    private const uint PRIME32_3 = 3266489917;
    private const uint PRIME32_4 = 668265263;
    private const uint PRIME32_5 = 374761393;

    public static uint CalculateHash(byte[] buf, int len, uint seed)
    {
      int index1 = 0;
      uint num1;
      if (len >= 16)
      {
        int num2 = len - 16;
        uint num3 = (uint) ((int) seed - 1640531535 - 2048144777);
        uint num4 = seed + 2246822519U;
        uint num5 = seed;
        uint num6 = seed - 2654435761U;
        do
        {
          byte[] numArray1 = buf;
          int index2 = index1;
          int num7 = index2 + 1;
          int num8 = (int) numArray1[index2];
          byte[] numArray2 = buf;
          int index3 = num7;
          int num9 = index3 + 1;
          int num10 = (int) numArray2[index3] << 8;
          int num11 = num8 | num10;
          byte[] numArray3 = buf;
          int index4 = num9;
          int num12 = index4 + 1;
          int num13 = (int) numArray3[index4] << 16;
          int num14 = num11 | num13;
          byte[] numArray4 = buf;
          int index5 = num12;
          int num15 = index5 + 1;
          int num16 = (int) numArray4[index5] << 24;
          uint num17 = (uint) (num14 | num16);
          uint num18 = num3 + num17 * 2246822519U;
          num3 = (num18 << 13 | num18 >> 19) * 2654435761U;
          byte[] numArray5 = buf;
          int index6 = num15;
          int num19 = index6 + 1;
          int num20 = (int) numArray5[index6];
          byte[] numArray6 = buf;
          int index7 = num19;
          int num21 = index7 + 1;
          int num22 = (int) numArray6[index7] << 8;
          int num23 = num20 | num22;
          byte[] numArray7 = buf;
          int index8 = num21;
          int num24 = index8 + 1;
          int num25 = (int) numArray7[index8] << 16;
          int num26 = num23 | num25;
          byte[] numArray8 = buf;
          int index9 = num24;
          int num27 = index9 + 1;
          int num28 = (int) numArray8[index9] << 24;
          uint num29 = (uint) (num26 | num28);
          uint num30 = num4 + num29 * 2246822519U;
          num4 = (num30 << 13 | num30 >> 19) * 2654435761U;
          byte[] numArray9 = buf;
          int index10 = num27;
          int num31 = index10 + 1;
          int num32 = (int) numArray9[index10];
          byte[] numArray10 = buf;
          int index11 = num31;
          int num33 = index11 + 1;
          int num34 = (int) numArray10[index11] << 8;
          int num35 = num32 | num34;
          byte[] numArray11 = buf;
          int index12 = num33;
          int num36 = index12 + 1;
          int num37 = (int) numArray11[index12] << 16;
          int num38 = num35 | num37;
          byte[] numArray12 = buf;
          int index13 = num36;
          int num39 = index13 + 1;
          int num40 = (int) numArray12[index13] << 24;
          uint num41 = (uint) (num38 | num40);
          uint num42 = num5 + num41 * 2246822519U;
          num5 = (num42 << 13 | num42 >> 19) * 2654435761U;
          byte[] numArray13 = buf;
          int index14 = num39;
          int num43 = index14 + 1;
          int num44 = (int) numArray13[index14];
          byte[] numArray14 = buf;
          int index15 = num43;
          int num45 = index15 + 1;
          int num46 = (int) numArray14[index15] << 8;
          int num47 = num44 | num46;
          byte[] numArray15 = buf;
          int index16 = num45;
          int num48 = index16 + 1;
          int num49 = (int) numArray15[index16] << 16;
          int num50 = num47 | num49;
          byte[] numArray16 = buf;
          int index17 = num48;
          index1 = index17 + 1;
          int num51 = (int) numArray16[index17] << 24;
          uint num52 = (uint) (num50 | num51);
          uint num53 = num6 + num52 * 2246822519U;
          num6 = (num53 << 13 | num53 >> 19) * 2654435761U;
        }
        while (index1 <= num2);
        num1 = (uint) (((int) num3 << 1 | (int) (num3 >> 31)) + ((int) num4 << 7 | (int) (num4 >> 25)) + ((int) num5 << 12 | (int) (num5 >> 20)) + ((int) num6 << 18 | (int) (num6 >> 14)));
      }
      else
        num1 = seed + 374761393U;
      uint num54 = num1 + (uint) len;
      while (index1 <= len - 4)
      {
        int num2 = (int) num54;
        byte[] numArray1 = buf;
        int index2 = index1;
        int num3 = index2 + 1;
        int num4 = (int) numArray1[index2];
        byte[] numArray2 = buf;
        int index3 = num3;
        int num5 = index3 + 1;
        int num6 = (int) numArray2[index3] << 8;
        int num7 = num4 | num6;
        byte[] numArray3 = buf;
        int index4 = num5;
        int num8 = index4 + 1;
        int num9 = (int) numArray3[index4] << 16;
        int num10 = num7 | num9;
        byte[] numArray4 = buf;
        int index5 = num8;
        index1 = index5 + 1;
        int num11 = (int) numArray4[index5] << 24;
        int num12 = (num10 | num11) * -1028477379;
        uint num13 = (uint) (num2 + num12);
        num54 = (uint) (((int) num13 << 17 | (int) (num13 >> 15)) * 668265263);
      }
      for (; index1 < len; ++index1)
      {
        uint num2 = num54 + (uint) buf[index1] * 374761393U;
        num54 = (uint) (((int) num2 << 11 | (int) (num2 >> 21)) * -1640531535);
      }
      uint num55 = (num54 ^ num54 >> 15) * 2246822519U;
      uint num56 = (num55 ^ num55 >> 13) * 3266489917U;
      return num56 ^ num56 >> 16;
    }
  }
}
