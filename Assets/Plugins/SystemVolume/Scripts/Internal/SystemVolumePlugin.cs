using System;
using UnityEngine;

namespace SystemVolume.Internal
{
    internal sealed class SystemVolumePlugin : MonoBehaviour
    {
        private const string GameObjectName = "SystemVolumePlugin";

        private static SystemVolumePlugin _instance;

        private static bool _isDestroyed = false;

        public static SystemVolumePlugin Instance {
            get {
                if (!_isDestroyed && _instance == null)
                {
                    _instance = FindObjectOfType<SystemVolumePlugin>();
                    if (_instance == null)
                    {
                        GameObject gameObject = new GameObject(typeof(SystemVolumePlugin).Name);
                        _instance = gameObject.AddComponent<SystemVolumePlugin>();
                        _instance.Initialize();
                    }
                }
                return _instance;
            }
        }

        public ChangeSystemVolume OnChangeVolume;

        private IPlatform _platform = null;

        private bool _isInitialized = false;

        private void Awake()
        {
            if (_instance == null)
                _instance = gameObject.GetComponent<SystemVolumePlugin>();
            else if (_instance != this)
            {
                _instance.OnDestroy();
                _instance = gameObject.GetComponent<SystemVolumePlugin>();
            }

            DontDestroyOnLoad(this);
            Initialize();
        }

        private void OnDestroy()
        {
            if (this == _instance)
            {
                _instance = null;
                _isDestroyed = true;
            }
            Destroy(this);
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            gameObject.name = GameObjectName;

            _platform =
#if UNITY_EDITOR
                gameObject.AddComponent<SystemVolumeForEditor>();
#elif UNITY_ANDROID
                gameObject.AddComponent<SystemVolumeForAndroid>();
#elif UNITY_IOS
#else
                Debug.unityLogger.LogError(GetType().Name, "This platform is not supported.");
                null;
#endif
        }

        public float GetSystemVolume()
        {
            return _platform.GetSystemVolume();
        }

        public void SetSystemVolume(float volume)
        {
            _platform.SetSystemVolume(volume);
        }

        /// <summary>
        /// call back from native
        /// </summary>
        private void OnChangeSystemVolume(string volumeString)
        {
            float volume = 1.0f;
            if (float.TryParse(volumeString, out volume))
            {
                if (OnChangeVolume != null)
                    OnChangeVolume.Invoke(volume);
            }
        }
    }
}
