/*
Install Libraries:
- Syphon
- Minim
*/

AudioLiveShader liveShader;

void setup() {
  size(1280, 720, P3D);
  // fullScreen(P3D);
  frameRate(60);

  AudioLiveShaderHost.init(this, 512);  
  liveShader = new AudioLiveShader(width, height, "data/frag-ikeda.fs");

  // liveShader.enableSyphon("LiveShader");
 }

void draw() {
  
  liveShader.render();
  
  background(0);
    
  if(liveShader.texture != null) {
    image(liveShader.texture, 0, 0, width, height);
  }
  
  if(liveShader.hasError()) {
    background(0);
    liveShader.displayError(30, 30);
  }
}
