using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// �����̼� ������ ���� ������.
    /// </summary>
    [Serializable]
    public sealed class Narration
    {
        private const string Path = "Narration";

        public string Text;
        public AudioClip Clip;

        public Narration(string text, string clipPath)
        {
            Text = text;
            Clip = Resources.Load<AudioClip>(Path + "/" + clipPath);
        }
    }
}
