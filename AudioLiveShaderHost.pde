static class AudioLiveShaderHost {
  static int bufferSize;

  public static AudioInput input;
  public static FFT fft;
  public static Minim minim;
  public static PApplet main;

  public static PGraphics tex;
  

  final static int audioTextureBandwidth = 1;

  private static int lastProcessedFrame = -1;
  
  private static float audioLevel;
  public static float audioRunningLevelSum = 0.0;

  public static void init(PApplet app, int bufferSize) {
    minim = new Minim(app);
    input = minim.getLineIn(Minim.STEREO, bufferSize);
    init(app, input);
  }

  public static void init(PApplet app, AudioInput in) {
    input = in;
    main = app;
    
    bufferSize = input.bufferSize();
    tex = main.createGraphics(bufferSize, audioTextureBandwidth, P3D);
    fft = new FFT( bufferSize, input.sampleRate() );
  }

  private static int simpleValueFromAudioSample(float sample) {
    int ret = (int) (sample * 127.0 + 127);
    return ret;
  }

  private static int simpleValueFromFFTBin(float sample) {
    double ret = Math.sqrt((double) sample / 32.0);
    if (ret > 1.0) {
      ret = 1.0;
    }
    return (int) (ret * 256);
  }

  private static void drawSimpleAudioTexture() {
    for (int i = 1; i <= bufferSize; i++) {
      int magnitude = (int) simpleValueFromFFTBin(fft.getBand(1 + i/2));
      // https://forum.processing.org/two/discussion/8086/what-is-a-color-in-processing
      int r = simpleValueFromAudioSample(input.left.get(i-1));
      int g = simpleValueFromAudioSample(input.right.get(i-1));
      int b = magnitude;
      int c = (255 << 24) | (r << 16) | (g << 8) | b;
      tex.stroke(c);
      tex.line(i, 0, i, audioTextureBandwidth);
    }
  }
  
  public static void update() {
    if (lastProcessedFrame == main.frameCount) {
      return;
    }

    fft.forward( input.mix );
    audioLevel = (input.left.level() + input.right.level()) * 0.5;
    
    audioRunningLevelSum += audioLevel;
    
    if(audioRunningLevelSum > 1000.0) {
      audioRunningLevelSum -= 1000.0;
    }

    tex.noSmooth();
    tex.beginDraw();
    tex.background(0);

    drawSimpleAudioTexture();
    tex.endDraw();
  }
  
  // this should never fail, try before!
  
  public static void setUniforms(PShader program) {
    program.set("level", audioLevel);
    program.set("runningLevelSum", audioRunningLevelSum);
    program.set("audioTexture", tex);
  }
}
