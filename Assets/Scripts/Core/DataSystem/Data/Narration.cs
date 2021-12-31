using System;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 내레이션 문구와 음성 데이터.
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
