#ifdef GL_ES
precision mediump float;
precision mediump int;
#endif

#define PROCESSING_TEXTURE_SHADER

varying vec4 vertColor;
varying vec2 vertTexCoord;

uniform sampler2D feedback;
uniform sampler2D audioTexture;

uniform float width;
uniform float height;
uniform float time;
uniform float level;
uniform float runningLevelSum;

float PI = 3.14159265359;

float getAudio(vec2 st) {
  st = floor(st * vec2(65.0,50.0)) / vec2(65.0, 50.0);
  float r = texture2D(audioTexture, vec2(st.x, 0.5)).r;
  r = mod(r * 2.0, 1.0);
  float b = smoothstep(abs(st.y - r), 0.0, 0.03);
  b = 1.0 - b;
  return b;
}


void main(void) {

  vec2 st = vertTexCoord;
  vec2 sto = st;

  st = st * 2.0 - 1.0;
  st.y = st.y * (height / width);

  float d = distance(st, vec2(0, 0)) * 0.125;
  float dp = floor(d * 1024.0) / 1024.0;

  d = mod(d + runningLevelSum * 0.9, 1.0);

  vec2 stp = vec2(  abs(atan(st.x, st.y) / (PI*2.0) ), d);

  float b = getAudio(stp);

  vec4 c = texture2D(feedback, sto + (b * st) * 5.0) + vec4(b, 0.0, 0.0, 1.0);
  float s = c.r;

  c.r = c.r - texture2D(audioTexture, vec2(sto.x, 0.5)).g * 0.9;

  c.r = c.r + (1.0 - smoothstep(distance(st, vec2(0.0, 0.0)), 0, 0.5)) * level * 0.5;


  if(d < 1.0) {
  	c.r = c.r + (texture2D(audioTexture, vec2(dp, 0.5)).b * 5.0);
  }

  c.r = clamp(c.r, 0.0, 1.0) - (s * 0.5);

  float levelScale = level * 7.0;

  gl_FragColor = vec4(c.r * levelScale, c.r * levelScale, c.r * levelScale, 1.0);
}