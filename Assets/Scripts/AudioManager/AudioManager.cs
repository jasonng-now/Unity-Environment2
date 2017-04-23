﻿using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public AudioClip[] audioClipArray;
    public GameObject[] preFabArray;
    public float audioClipLength = 30F;

    AudioSource audio;
    public bool useAudioClips = false;
    public float timeCounter = 0F;
    public float currentRange = 0F;
    public float[] spectrum;
    public int audioIndex = 0;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        spectrum = new float[1024];

        //if (useAudioClips)
        //{
        //    InvokeRepeating("ChooseAudioClip", 0, audioClipLength);
        //}
        if (useAudioClips)
        {
            ChooseAudioClip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useAudioClips)
        {
            if (timeCounter > audioClipLength)
            {
                // Reset timer
                timeCounter = 0.0F;

                if (audioIndex == audioClipArray.Length - 1)
                {
                    audioIndex = 0;
                }
                else
                {
                    audioIndex++;
                }
                audio.Stop();
                ChooseAudioClip();
                audio.Play();
            }
        }

        //if (useAudioClips)
        //{
        //    if (timeCounter > randomTime)
        //    {
        //        randomTime = 10F;
        //        Debug.Log(randomTime);
        //        timeCounter = 0.0F;
        //        audio.Stop();
        //        ChooseAudioClip();
        //        audio.Play();
        //    }
        //}

        //// Fade in current clip
        //if (timeCounter < 1.5)
        //{
        //    if (audio.volume < 1)
        //    {
        //        audio.volume += 0.1F * Time.deltaTime * 2;
        //    }
        //}

        //// Fade out current clip
        //if (timeCounter > audioClipLength - 1.5)
        //{
        //    if (audio.volume > 0.0)
        //    {
        //        audio.volume -= 0.1F * Time.deltaTime * 2;
        //    }
        //}

        spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);

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

    void ChooseAudioClip()
    {
        // Turn off all prefabs
        foreach(GameObject preFab in preFabArray)
        {
            if (preFab != null)
            {
                preFab.GetComponent<PreFabManager>().isRunning = false;
            }
        }

        audio.Stop();
        //audioIndex = Random.Range(0, audioClipArray.Length);
        if (audioIndex <= audioClipArray.Length - 1)
        {
            audio.clip = audioClipArray[audioIndex];
            preFabArray[audioIndex].GetComponent<PreFabManager>().isRunning = true;
        }
        else
        {
            audio.clip = null;
        }
        //audio.volume = 0;
        audio.Play();
    }
}
