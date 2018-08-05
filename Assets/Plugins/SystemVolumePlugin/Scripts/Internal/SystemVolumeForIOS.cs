#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace SystemVolume.Internal
{
    internal sealed class SystemVolumeForIOS : MonoBehaviour, IPlatform
    {

        [DllImport("__Internal")]
        private static extern void _InitializeSystemVolume();

        [DllImport("__Internal")]
        private static extern void _DisposeSystemVolume();

        [DllImport("__Internal")]
        private static extern float _GetSystemVolume();

        [DllImport("__Internal")]
        private static extern void _SetSystemVolume(float volume);

        private void Awake()
        {
            _InitializeSystemVolume();
        }

        private void OnDestroy()
        {
            _DisposeSystemVolume();
        }

        float IPlatform.GetSystemVolume()
        {
            return _GetSystemVolume();
        }

        void IPlatform.SetSystemVolume(float volume)
        {
            _SetSystemVolume(volume);
        }
    }
}
#endif
