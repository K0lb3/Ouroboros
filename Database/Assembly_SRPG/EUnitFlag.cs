// Decompiled with JetBrains decompiler
// Type: SRPG.EUnitFlag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public enum EUnitFlag
  {
    Entried = 1,
    Moved = 2,
    Action = 4,
    Searched = 8,
    Defended = 16, // 0x00000010
    SideAttack = 32, // 0x00000020
    BackAttack = 64, // 0x00000040
    Escaped = 128, // 0x00000080
    Sneaking = 256, // 0x00000100
    Paralysed = 512, // 0x00000200
    ForceMoved = 1024, // 0x00000400
    ForceEntried = 2048, // 0x00000800
    ForceAuto = 4096, // 0x00001000
    EntryDead = 8192, // 0x00002000
    FirstAction = 16384, // 0x00004000
    DisableFirstVoice = 32768, // 0x00008000
    DamagedActionStart = 65536, // 0x00010000
    TriggeredAutoSkills = 131072, // 0x00020000
    DisableUnitChange = 262144, // 0x00040000
    UnitChanged = 524288, // 0x00080000
    UnitWithdraw = 1048576, // 0x00100000
    CreatedBreakObj = 2097152, // 0x00200000
    Reinforcement = 4194304, // 0x00400000
    ToDying = 8388608, // 0x00800000
  }
}
