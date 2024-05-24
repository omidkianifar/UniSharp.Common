#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UniSharp.Common.Extensions
{
    public static class AssetDatabaseExtensions
    {
        public static T FindAndLoadAsset<T>() where T : UnityEngine.Object
        {
            var asset = AssetDatabase.FindAssets("t:" + typeof(T).FullName, new string[] { }).FirstOrDefault();

            if (string.IsNullOrEmpty(asset))
                return default;

            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(asset));
        }

        public static IEnumerable<T> FindAndLoadAssets<T>() where T : UnityEngine.Object
        {
            var assets = AssetDatabase.FindAssets("t:" + typeof(T).FullName, new string[] { });

            if (assets is null || assets.Length == 0)
                return default;

            return assets.Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x)));
        }
    }
}

#endif
