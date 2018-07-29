#if UNITY_IOS
using System;
using UnityEngine;

namespace SystemVolume.Internal
{
    internal sealed class SystemVolumeForIOS : MonoBehaviour, IPlatform
    {
        float IPlatform.GetSystemVolume()
        {
            return 1.0f;
        }

        void IPlatform.SetSystemVolume(float volume)
        {

        }
    }
}
#endif
