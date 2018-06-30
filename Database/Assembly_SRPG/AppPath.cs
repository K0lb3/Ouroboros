namespace SRPG
{
    using System;
    using UnityEngine;

    public static class AppPath
    {
        public static string persistentDataPath
        {
            get
            {
                return (Application.get_dataPath() + "/../data");
            }
        }

        public static string temporaryCachePath
        {
            get
            {
                return (Application.get_dataPath() + "/../temp");
            }
        }

        public static string assetCachePath
        {
            get
            {
                return (Application.get_dataPath() + "/..");
            }
        }

        public static string assetCachePathOld
        {
            get
            {
                return (Application.get_dataPath() + "/..");
            }
        }
    }
}

