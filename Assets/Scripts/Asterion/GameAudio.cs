using UnityEngine;

namespace Asterion
{
    public sealed class GameAudio : MonoBehaviour
    {
        AudioSource effects, ambience;
        AudioClip tap, good, bad, footstep;
        public bool Muted { get; private set; }

        void Awake()
        {
            effects = gameObject.AddComponent<AudioSource>();
            effects.spatialBlend = 0;
            tap = Tone("Mecanismo", 330, .12f, .15f);
            good = Tone("Armonía", 660, .65f, .15f);
            bad = Tone("Bloqueo", 115, .3f, .15f);
            footstep = Tone("Paso", 62, .09f, .05f);
            ambience = gameObject.AddComponent<AudioSource>();
            ambience.clip = Drone();
            ambience.loop = true;
            ambience.volume = .22f;
            ambience.Play();
        }

        AudioClip Tone(string title, float frequency, float seconds, float gain)
        {
            int count = Mathf.CeilToInt(22050 * seconds);
            float[] samples = new float[count];
            for (int i = 0; i < count; i++)
            {
                float t = i / 22050f;
                float envelope = Mathf.Min(1, t * 150) * Mathf.Exp(-t * 7 / seconds);
                samples[i] = (Mathf.Sin(2 * Mathf.PI * frequency * t) + .3f * Mathf.Sin(2 * Mathf.PI * frequency * 1.5f * t)) * envelope * gain;
            }
            AudioClip clip = AudioClip.Create(title, count, 1, 22050, false);
            clip.SetData(samples, 0);
            return clip;
        }

        AudioClip Drone()
        {
            float[] data = new float[22050 * 8];
            for (int i = 0; i < data.Length; i++)
            {
                float t = i / 22050f;
                data[i] = (.035f * Mathf.Sin(t * Mathf.PI * 2 * 55) + .02f * Mathf.Sin(t * Mathf.PI * 2 * 82.5f) + .015f * Mathf.Sin(t * Mathf.PI * 2 * 110)) * (.8f + .2f * Mathf.Cos(t * Mathf.PI / 4));
            }
            AudioClip clip = AudioClip.Create("El observatorio duerme", data.Length, 1, 22050, false);
            clip.SetData(data, 0);
            return clip;
        }

        public void Click() => effects.PlayOneShot(tap);
        public void Success() => effects.PlayOneShot(good);
        public void Error() => effects.PlayOneShot(bad);
        public void Step() => effects.PlayOneShot(footstep);
        public void ToggleMute()
        {
            Muted = !Muted;
            effects.mute = ambience.mute = Muted;
        }
        void OnDestroy()
        {
            if (ambience != null && ambience.clip != null) Destroy(ambience.clip);
            Destroy(tap); Destroy(good); Destroy(bad); Destroy(footstep);
        }
    }
}
