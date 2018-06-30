namespace SRPG
{
    using System;
    using System.Text;

    public class JsonEscape
    {
        public JsonEscape()
        {
            base..ctor();
            return;
        }

        public static unsafe string Escape(string s)
        {
            int num;
            StringBuilder builder;
            int num2;
            char ch;
            string str;
            char ch2;
            int num3;
            if (s == null)
            {
                goto Label_0011;
            }
            if (s.Length != null)
            {
                goto Label_0017;
            }
        Label_0011:
            return string.Empty;
        Label_0017:
            num = s.Length;
            builder = new StringBuilder(num);
            num2 = 0;
            goto Label_0143;
        Label_002C:
            ch = s[num2];
            ch2 = ch;
            switch ((ch2 - 8))
            {
                case 0:
                    goto Label_00A4;

                case 1:
                    goto Label_00B5;

                case 2:
                    goto Label_00C6;

                case 3:
                    goto Label_0058;

                case 4:
                    goto Label_00D7;

                case 5:
                    goto Label_00E8;
            }
        Label_0058:
            if (ch2 == 0x22)
            {
                goto Label_0078;
            }
            if (ch2 == 0x2f)
            {
                goto Label_008E;
            }
            if (ch2 == 0x5c)
            {
                goto Label_0078;
            }
            goto Label_00F9;
        Label_0078:
            builder.Append(0x5c);
            builder.Append(ch);
            goto Label_013F;
        Label_008E:
            builder.Append(0x5c);
            builder.Append(ch);
            goto Label_013F;
        Label_00A4:
            builder.Append(@"\b");
            goto Label_013F;
        Label_00B5:
            builder.Append(@"\t");
            goto Label_013F;
        Label_00C6:
            builder.Append(@"\n");
            goto Label_013F;
        Label_00D7:
            builder.Append(@"\f");
            goto Label_013F;
        Label_00E8:
            builder.Append(@"\r");
            goto Label_013F;
        Label_00F9:
            if (ch > 0x7f)
            {
                goto Label_010E;
            }
            builder.Append(ch);
            goto Label_013A;
        Label_010E:
            num3 = ch;
            str = &num3.ToString("X");
            builder.Append(@"\u" + str.PadLeft(4, 0x30));
        Label_013A:;
        Label_013F:
            num2 += 1;
        Label_0143:
            if (num2 < num)
            {
                goto Label_002C;
            }
            return builder.ToString();
        }
    }
}

