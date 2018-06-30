namespace SRPG
{
    using System;
    using System.Runtime.CompilerServices;

    [Extension]
    public static class ArtifactCompareExtention
    {
        [Extension]
        public static int CompareByID(ArtifactData x, ArtifactData y)
        {
            return CompareByID(x.ArtifactParam, y.ArtifactParam);
        }

        [Extension]
        public static int CompareByID(ArtifactParam x, ArtifactParam y)
        {
            return string.Compare(x.iname, y.iname);
        }

        [Extension]
        public static int CompareByType(ArtifactData x, ArtifactData y)
        {
            return CompareByType(x.ArtifactParam, y.ArtifactParam);
        }

        [Extension]
        public static int CompareByType(ArtifactParam x, ArtifactParam y)
        {
            if (x.type <= y.type)
            {
                goto Label_0013;
            }
            return 1;
        Label_0013:
            if (x.type >= y.type)
            {
                goto Label_0026;
            }
            return -1;
        Label_0026:
            return 0;
        }

        [Extension]
        public static int CompareByTypeAndID(ArtifactData x, ArtifactData y)
        {
            return CompareByTypeAndID(x.ArtifactParam, y.ArtifactParam);
        }

        [Extension]
        public static int CompareByTypeAndID(ArtifactParam x, ArtifactParam y)
        {
            int num;
            num = CompareByType(x, y);
            if (num == null)
            {
                goto Label_0010;
            }
            return num;
        Label_0010:
            return CompareByID(x, y);
        }
    }
}

