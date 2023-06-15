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
}