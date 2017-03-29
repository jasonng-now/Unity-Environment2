using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] audioClipArray;
    AudioSource audio;
    private int randomMusic = 0;
    private float randomTime = 10F;
    private float timeCounter = 0F;

    public float currentRange = 0F;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (timeCounter > randomTime)
        //{
        //    randomTime = Random.Range(15.0F, 30.0F);
        //    Debug.Log(randomTime);
        //    timeCounter = 0.0F;
        //    audio.Stop();
        //    ChooseMusic();
        //    audio.Play();
        //}

        //// Fade in current clip
        //if (timeCounter < 5)
        //{
        //    if (audio.volume < 1)
        //    {
        //        audio.volume += 0.1F * Time.deltaTime * 2;
        //    }
        //}

        //// Fade out current clip
        //if (timeCounter > randomTime - 5)
        //{
        //    if (audio.volume > 0.0)
        //    {
        //        audio.volume -= 0.1F * Time.deltaTime * 2;
        //    }
        //}

        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

        //c1 = 64Hz
        //c3 = 256Hz
        //c4 = 513Hz
        //c5 = 1024Hz

        float c1 = (spectrum[2] + spectrum[2] + spectrum[4]);
        float c3 = (spectrum[11] + spectrum[12] + spectrum[13]);
        float c4 = (spectrum[22] + spectrum[23] + spectrum[24]);
        float c5 = (spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49]);

        currentRange = c3;

        timeCounter += Time.deltaTime;
    }

    void ChooseMusic()
    {
        randomMusic = Random.Range(0, 2);
        Debug.Log(randomMusic);
        if (randomMusic < audioClipArray.Length - 1)
        {
            audio.clip = audioClipArray[randomMusic];
        }
        else
        {
            audio.clip = null;
        }
        audio.volume = 0;
    }
}
