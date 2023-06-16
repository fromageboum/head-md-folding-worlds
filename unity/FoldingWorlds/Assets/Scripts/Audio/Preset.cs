using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Preset
{
    public int playbackSpeed;
    public int grainSize;
    public int grainStep;

    public float guiPlaybackSpeed;
    public float guiGrainSize;
    public float guiGrainStep;

    public float envMean;
    public float envSd;
    public bool envelopeOn;

    public Preset(GranularSynth synth)
    {
        playbackSpeed = synth.playbackSpeed;
        grainSize = synth.grainSize;
        grainStep = synth.grainStep;

        guiPlaybackSpeed = synth.guiPlaybackSpeed;
        guiGrainSize = synth.guiGrainSize;
        guiGrainStep = synth.guiGrainStep;

        envMean = synth.envMean;
        envSd = synth.envSd;
        envelopeOn = synth.envelopeOn;
    }

    public static Preset CreateRandomPreset(GranularSynth synth) {
        Preset p = new Preset(synth);

        // Make it -1 or 1,    -1 
        p.playbackSpeed = Random.Range(0.0f, 1f) > 0.5f ? -1 : 1;
        p.grainSize = (int)GetBiasedRandom(1000.0f, 1300.0f);
        p.grainStep = (int)GetBiasedRandom(-20.0f, 10.0f);

        return p; 
    }

    private static float GetBiasedRandom(float min, float max)
    {
        float mid = (min + max) / 2;
        float biasedRandom = Mathf.Lerp(min, max, Mathf.Pow(Random.value, 2));

        // Flip half the values to cover both sides.
        if (Random.value < 0.5f)
        {
            return mid + (mid - biasedRandom);
        }
        else
        {
            return biasedRandom;
        }
    }
}