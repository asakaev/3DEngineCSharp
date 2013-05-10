namespace Scene3D
{
    static class Kb // keyboard
    {
        public static bool IsKeyDown(int key)
        {
            return (GetKeyState(key) & KEY_PRESSED) != 0;
        }
        private const int KEY_PRESSED = 0x8000;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetKeyState(int key);
    }
}