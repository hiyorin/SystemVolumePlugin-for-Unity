using System;
using SystemVolume.Internal;

namespace SystemVolume
{
    public delegate void ChangeSystemVolume(float volume);

    public sealed class SystemVolumeController : IDisposable
    {
        private readonly SystemVolumePlugin _plugin;

        public ChangeSystemVolume OnChangeVoluem;

        private float _volume = 1.0f;

        public float Volume
        {
            get {
                return _volume;
            }
            set
            {
                if (_plugin != null)
                    _plugin.SetSystemVolume(value);
                _volume = value;
            }
        }

        public SystemVolumeController()
        {
            _plugin = SystemVolumePlugin.Instance;
            if (_plugin != null)
            {
                _volume = _plugin.GetSystemVolume();
                _plugin.OnChangeVolume += OnChangeSystemVolume;
            }
        }

        ~SystemVolumeController()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_plugin != null)
                _plugin.OnChangeVolume -= OnChangeSystemVolume;
            OnChangeVoluem = null;
        }

        private void OnChangeSystemVolume(float volume)
        {
            _volume = volume;
            if (OnChangeVoluem != null)
                OnChangeVoluem.Invoke(volume);
        }
    }
}
