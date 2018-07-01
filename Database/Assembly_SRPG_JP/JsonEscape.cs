// Decompiled with JetBrains decompiler
// Type: SRPG.JsonEscape
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class JsonEscape
  {
    public static string Escape(string s)
    {
      if (s == null || s.Length == 0)
        return string.Empty;
      int length = s.Length;
      StringBuilder stringBuilder = new StringBuilder(length);
      for (int index = 0; index < length; ++index)
      {
        char ch1 = s[index];
        char ch2 = ch1;
        switch (ch2)
        {
          case '\b':
            stringBuilder.Append("\\b");
            break;
          case '\t':
            stringBuilder.Append("\\t");
            break;
          case '\n':
            stringBuilder.Append("\\n");
            break;
          case '\f':
            stringBuilder.Append("\\f");
            break;
          case '\r':
            stringBuilder.Append("\\r");
            break;
          default:
            switch (ch2)
            {
              case '"':
              case '\\':
                stringBuilder.Append('\\');
                stringBuilder.Append(ch1);
                continue;
              case '/':
                stringBuilder.Append('\\');
                stringBuilder.Append(ch1);
                continue;
              default:
                if (ch1 <= '\x007F')
                {
                  stringBuilder.Append(ch1);
                  continue;
                }
                string str = ((int) ch1).ToString("X");
                stringBuilder.Append("\\u" + str.PadLeft(4, '0'));
                continue;
            }
        }
      }
      return stringBuilder.ToString();
    }
  }
}
