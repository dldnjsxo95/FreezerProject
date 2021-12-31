using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public class BurnerBlowerFanControl : MonoBehaviour
    {
        public GameObject _selectMenu;

        public AudioSource _sound;
        public AudioClip _normalSound;
        public AudioClip _abnormalSound;


        public ParticleSystem _normalParticle;

        public void SetActiveSelectMenu(bool value)
        {
            _selectMenu.SetActive(value);
            _normalParticle.gameObject.SetActive(false);
        }

        public void OnSelectFanState(bool normal)
        {
            _normalParticle.gameObject.SetActive(normal);
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
