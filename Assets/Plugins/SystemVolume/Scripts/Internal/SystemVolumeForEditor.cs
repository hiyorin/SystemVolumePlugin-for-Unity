#if UNITY_EDITOR
using System;
using UnityEngine;

namespace SystemVolume.Internal
{
    internal sealed class SystemVolumeForEditor : MonoBehaviour, IPlatform
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
