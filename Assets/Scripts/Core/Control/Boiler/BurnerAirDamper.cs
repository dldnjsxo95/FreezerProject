using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    public class BurnerAirDamper : MonoBehaviour
    {
        public Transform _part1;
        public Transform _part2;

        private Vector3 _originPart1;
        private Vector3 _originPart2;
        private void Awake()
        {
            _originPart1 = _part1.localPosition;
            _originPart2 = _part2.localPosition;
        }

        public void SetDamperMove()
        {
            _part1.localPosition = new Vector3(-3.45f, 0.0f, -10.72314f);
            _part2.localPosition = new Vector3(-3.45f, 0.0f, -10.72314f);
        }

        public void ResetMove()
        {
            _part1.localPosition = _originPart1;
            _part2.localPosition = _originPart2;
        }
    }
}
