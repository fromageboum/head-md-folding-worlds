using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularSynth : MonoBehaviour
{
    public static GranularSynth instance;

    public AudioClip clip;
    public int playbackSpeed = 1;
    public int grainSize = 1000;
    public int grainStep = 1;

    public float guiPlaybackSpeed = 1.0f;
    public float guiGrainSize = 1000.0f;
    public float guiGrainStep = 1.0f;

    private int sampleLength;
    private float[] samples;

    private int position = 0;
    private int interval = 0;

    public float envMean = 0.5f;
    public float envSd = 0.125f;
    public bool envelopeOn = false;

    public bool showGUI = false;

    public Preset normalPreset;
    public Preset fUpPreset;

    private void Awake()
    {
        instance = this;
        sampleLength = clip.samples;
        samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);
    }

    public void FuckUpSound() {
        StartCoroutine(_FUpSound());
    }

    IEnumerator _FUpSound() {
        Preset p = Preset.CreateRandomPreset(this);
        TransitionToPreset(p, 1f);
        yield return new WaitForSeconds(3f);
        TransitionToPreset(normalPreset, 1f);
        yield return new WaitForSeconds(1f);
    }
    
    private void Update()
    {
        if (showGUI)
        {
            PositionView cursor = FindObjectOfType<PositionView>();
            cursor.position = 1.0f / sampleLength * position;
            cursor.width = 1.0f / sampleLength * interval * playbackSpeed;
        }
    }

    private void OnGUI()
    {
        if (showGUI) {
            GUILayout.BeginArea(new Rect(16, 16, Screen.width - 32, Screen.height - 32));
            GUILayout.FlexibleSpace();
            GUILayout.Label("Playback Speed: " + playbackSpeed);
            guiPlaybackSpeed = GUILayout.HorizontalSlider(guiPlaybackSpeed, -4.0f, 4.0f);
            GUILayout.FlexibleSpace();
            GUILayout.Label("Grain Size: " + grainSize);
            guiGrainSize = GUILayout.HorizontalSlider(guiGrainSize, 2.0f, 10000.0f);
            GUILayout.FlexibleSpace();
            GUILayout.Label("Grain Step: " + grainStep);
            guiGrainStep = GUILayout.HorizontalSlider(guiGrainStep, -3000.0f, 3000.0f);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("RANDOMIZE!"))
            {
                guiPlaybackSpeed = Random.Range(-1.0f, 1.0f);
                guiGrainSize = Random.Range(1000.0f, 1300.0f);
                guiGrainStep = Random.Range(-50.0f, 50.0f);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

            playbackSpeed = Mathf.RoundToInt(guiPlaybackSpeed);
            if (playbackSpeed == 0) playbackSpeed = 1;
            grainSize = Mathf.RoundToInt(guiGrainSize);
            grainStep = Mathf.RoundToInt(guiGrainStep);
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += 2)
        {
            // Compute the position within the current grain, from 0.0 to 1.0
            float grainPosition = interval / (float)grainSize;

            // Compute the amplitude of the Gaussian envelope at this position
            float envelope = Gaussian(grainPosition, envMean, envSd);

            if (!envelopeOn)
            {
                envelope = 1;
            }

            // Fetch the samples and apply the envelope
            data[i] = envelope * samples[Mathf.Clamp(position * 2, 0, samples.Length - 1)];
            data[i + 1] = envelope * samples[Mathf.Clamp(position * 2 + 1, 0, samples.Length - 1)];

            if (--interval <= 0)
            {
                interval = grainSize;
                position += grainStep;
            }
            else
            {
                position += playbackSpeed;
            }

            while (position >= sampleLength)
            {
                position -= sampleLength;
            }
            while (position < 0)
            {
                position += sampleLength;
            }
        }
    }

    private float Gaussian(float x, float mean, float standardDeviation)
    {
        float firstPart = 1.0f / (standardDeviation * Mathf.Sqrt(2.0f * Mathf.PI));
        float exponent = -Mathf.Pow(x - mean, 2) / (2 * Mathf.Pow(standardDeviation, 2));
        return firstPart * Mathf.Exp(exponent);
    }

    public IEnumerator InterpolateToPreset(Preset preset, float duration)
    {
        Preset startPreset = new Preset(this);

        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            playbackSpeed = (int)Mathf.Lerp(startPreset.playbackSpeed, preset.playbackSpeed, t);
            grainSize = (int)Mathf.Lerp(startPreset.grainSize, preset.grainSize, t);
            grainStep = (int)Mathf.Lerp(startPreset.grainStep, preset.grainStep, t);

            guiPlaybackSpeed = Mathf.Lerp(startPreset.guiPlaybackSpeed, preset.guiPlaybackSpeed, t);
            guiGrainSize = Mathf.Lerp(startPreset.guiGrainSize, preset.guiGrainSize, t);
            guiGrainStep = Mathf.Lerp(startPreset.guiGrainStep, preset.guiGrainStep, t);

            envMean = Mathf.Lerp(startPreset.envMean, preset.envMean, t);
            envSd = Mathf.Lerp(startPreset.envSd, preset.envSd, t);
            envelopeOn = t < 0.5 ? startPreset.envelopeOn : preset.envelopeOn;

            yield return null;
        }

        // Make sure the final state is exactly the preset state.
        playbackSpeed = preset.playbackSpeed;
        grainSize = preset.grainSize;
        grainStep = preset.grainStep;

        guiPlaybackSpeed = preset.guiPlaybackSpeed;
        guiGrainSize = preset.guiGrainSize;
        guiGrainStep = preset.guiGrainStep;

        envMean = preset.envMean;
        envSd = preset.envSd;
        envelopeOn = preset.envelopeOn;
    }

    public void TransitionToPreset(Preset preset, float t)
    {
        StartCoroutine(InterpolateToPreset(preset, t)); // 1 second duration
    }

}