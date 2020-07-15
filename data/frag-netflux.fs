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

uniform vec2 mouse;
uniform float random;
uniform sampler2D whitney;
uniform sampler2D circles;

float PI = 3.14159265359;

void main(void) {
  vec2 st = vertTexCoord;
  float amplitude = texture2D(audioTexture, st).r;
  float b = 1.0 - (distance(st.x, amplitude) * 30.0);

  b = clamp(b, 0.0, 1.0);

  float bFB = texture2D(feedback, (st - vec2(0.5, 0.5)) * 0.9 + vec2(0.5, 0.5)).r;

  b = (b * 0.5) + (bFB * 0.8);

  gl_FragColor = vec4(b, b, b, 1.0);
}