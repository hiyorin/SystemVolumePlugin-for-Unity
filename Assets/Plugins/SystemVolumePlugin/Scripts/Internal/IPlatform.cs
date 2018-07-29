using System;

namespace SystemVolume.Internal
{
    internal interface IPlatform
    {
        float GetSystemVolume();
        void SetSystemVolume(float volume);
    }
}
