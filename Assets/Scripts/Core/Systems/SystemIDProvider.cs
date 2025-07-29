#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.SceneManagement;

namespace MiniECS
{
    internal static class SystemIDProvider
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void ResetID()
        {
            _NEXT_ID = 0;
        }
#endif

        static SystemIDProvider()
        {
            SceneManager.sceneLoaded += (_, _) =>
            {
                _NEXT_ID = 0;
            };
        }

        private static byte _NEXT_ID = 0;
        public static int Max => 64;
        public static SystemID Next() => new(_NEXT_ID++);
    }
}