using System.Runtime.InteropServices;

namespace Scripts {
    public static class Agora {
        [DllImport("__Internal")]
        public static extern void Init(string channel, string uid);

        [DllImport("__Internal")]
        public static extern void ToggleMic();

        [DllImport("__Internal")]
        public static extern void ToggleVideo();

        [DllImport("__Internal")]
        public static extern void Subscribe(string uid);

        [DllImport("__Internal")]
        public static extern void Unsubscribe(string uid);
    }
}
