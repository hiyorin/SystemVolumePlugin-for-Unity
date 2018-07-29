#if UNITY_ANDROID
using System;
using UnityEngine;

namespace SystemVolume.Internal
{
    internal sealed class SystemVolumeForAndroid : MonoBehaviour, IPlatform
    {
        float IPlatform.GetSystemVolume()
        {
            float volume = 1.0f;
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext"))
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            using (AndroidJavaClass audioManagerClass = new AndroidJavaClass("android.media.AudioManager"))
            using (AndroidJavaObject audioManager = context.Call<AndroidJavaObject>("getSystemService", contextClass.GetStatic<string>("AUDIO_SERVICE")))
            {
                float maxVolume = audioManager.Call<int>("getStreamMaxVolume", audioManagerClass.GetStatic<int>("STREAM_MUSIC"));
                float currentVolume = audioManager.Call<int>("getStreamVolume", audioManagerClass.GetStatic<int>("STREAM_MUSIC"));
                volume = currentVolume / maxVolume;
            }
            return volume;
        }

        void IPlatform.SetSystemVolume(float volume)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext"))
            using (AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context"))
            using (AndroidJavaClass audioManagerClass = new AndroidJavaClass("android.media.AudioManager"))
            using (AndroidJavaObject audioManager = context.Call<AndroidJavaObject>("getSystemService", contextClass.GetStatic<string>("AUDIO_SERVICE")))
            {
                float maxVolume = audioManager.Call<int>("getStreamMaxVolume", audioManagerClass.GetStatic<int>("STREAM_MUSIC"));
                int currentVolume = (int)Mathf.Lerp(0.0f, maxVolume, volume);
                audioManager.Call("setStreamVolume", audioManagerClass.GetStatic<int>("STREAM_MUSIC"), currentVolume, 0);
            }
        }
    }
}
#endif
