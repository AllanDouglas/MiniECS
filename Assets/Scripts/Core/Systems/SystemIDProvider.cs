namespace MiniECS
{
    internal static class SystemIDProvider
    {
        private static byte _NEXT_ID = 0;
        public static int Max => 64;
        public static SystemID Next() => new(_NEXT_ID++);
    }
}