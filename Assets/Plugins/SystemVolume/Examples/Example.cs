using UnityEngine;
using UnityEngine.UI;

namespace SystemVolume.Example
{
    public sealed class Example : MonoBehaviour
    {
        [SerializeField] private Text _currentVoluemText = null;

        private SystemVolumeController _controller;

        private void Start()
        {
            _controller = new SystemVolumeController();
            _controller.OnChangeVoluem = volume => {
                _currentVoluemText.text = volume.ToString();
            };
        }

        public void OnClickDownButton()
        {
            _controller.Volume = Mathf.Clamp01(_controller.Volume - 0.1f);
        }

        public void OnClickUpButton()
        {
            _controller.Volume = Mathf.Clamp01(_controller.Volume + 0.1f);
        }
    }
}
