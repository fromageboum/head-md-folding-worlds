using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularSynth : MonoBehaviour
{
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

    private void Awake()
    {
        sampleLength = clip.samples;
        samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);
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
                guiPlaybackSpeed = Random.Range(-2.0f, 2.0f);
                guiGrainSize = Random.Range(200.0f, 1000.0f);
                guiGrainStep = Random.Range(-1500.0f, 1500.0f);
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
}