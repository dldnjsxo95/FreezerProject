using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Futuregen
{
    public class PumpControl : MonoBehaviour
    {
        public GameObject _selectMenu;

        public AudioSource _sound;
        public AudioClip _normalSound;
        public AudioClip _abnormalSound;


        public ParticleSystem[] _abnormal;

        public void SetActiveSelectMenu(bool value)
        {
            _selectMenu.SetActive(value);
            foreach (ParticleSystem particle in _abnormal)
            {
                particle.gameObject.SetActive(false);
            }
        }

        public void OnSelectFanState(bool normal)
        {
            foreach (ParticleSystem particle in _abnormal)
            {
                particle.gameObject.SetActive(!normal);
            }
            _sound.clip = normal ? _normalSound : _abnormalSound;
            _sound.Play();
        }

        public void Update()
        {
            if (_selectMenu.activeSelf)
            {
                _selectMenu.transform.LookAt(Camera.main.transform);
            }
        }
    }
}
