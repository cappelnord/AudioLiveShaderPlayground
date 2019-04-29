#ifdef GL_ES
precision mediump float;
precision mediump int;
#endif

#define PROCESSING_TEXTURE_SHADER

varying vec4 vertColor;
varying vec2 vertTexCoord;

uniform sampler2D audioTexture;

uniform float width;
uniform float height;
uniform float time;
uniform float level;

float PI = 3.14159265359;

void main(void) {
  vec2 st = vertTexCoord;

  vec2 audio = texture2D(audioTexture, vec2(st.x, 0.0)).xy;
  float b;
  
  if(st.y < 0.5) {
    b = smoothstep(abs(audio.r * 0.5 - st.y), 0, 0.002);
  } else {
    b = smoothstep(abs(audio.g * 0.5 - (st.y - 0.5)), 0, 0.002);
  }

  gl_FragColor = vec4(b, b, b, 1.0);
}