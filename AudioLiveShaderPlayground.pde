/*
Install Libraries:
- Syphon
- Minim
*/

AudioLiveShader liveShader;

// PImage whitney;
// PGraphics canvas;

int windowWidth = 1024;
int windowHeight = 1024;
int renderWidth = windowWidth * 2;
int renderHeight = windowHeight * 2;

int FPS = 60;

void settings() {
  size(windowWidth, windowHeight, P3D);  
}

void setup() {
  // fullScreen(P3D);
  frameRate(FPS);
  
  /*
  canvas = createGraphics(1280, 720, P3D);
  whitney = loadImage("whitney.png");
  */

  AudioLiveShaderHost.init(this, 512);  
  liveShader = new AudioLiveShader(renderWidth, renderHeight, "data/frag-basic.fs");
  // liveShader.enableVideoOutput("videoName", 30);

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
    image(liveShader.texture, 0, 0, windowWidth, windowHeight);
  }
  
  if(liveShader.hasError()) {
    background(0);
    liveShader.displayError(30, 30);
  }
}

void keyPressed() {
  if(key == 's' || key == 'S') {
    liveShader.snapshot();
  }
}
