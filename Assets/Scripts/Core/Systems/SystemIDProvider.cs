using UnityEditor;

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

        private static byte _NEXT_ID = 0;
        public static int Max => 64;
        public static SystemID Next() => new(_NEXT_ID++);
    }
}