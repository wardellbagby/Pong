using Pong.Front_End.ScreenManager;
using System;

namespace Pong
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (ScreenManager manager = new ScreenManager())
            {
                manager.Run();
            }
        }
    }
#endif
}

