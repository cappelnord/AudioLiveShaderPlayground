/*
Install Libraries:
- Syphon
- Minim
*/

AudioLiveShader liveShader;

// PImage whitney;
// PGraphics canvas;

void setup() {
  size(1280, 720, P3D);
  // fullScreen(P3D);
  frameRate(60);
  
  /*
  canvas = createGraphics(1280, 720, P3D);
  whitney = loadImage("whitney.png");
  */

  AudioLiveShaderHost.init(this, 512);  
  liveShader = new AudioLiveShader(width, height, "data/frag-basic.fs");

  // liveShader.enableSyphon("LiveShader");
 }

void draw() {
  
  /*
  canvas.beginDraw();
  canvas.background(0);
  canvas.noStroke();
  canvas.fill(255);
  canvas.circle(mouseX, mouseY, 100);
  canvas.endDraw();

  liveShader.set("circles", canvas);
  */
  
  // liveShader.set("whitney", whitney);

  
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
