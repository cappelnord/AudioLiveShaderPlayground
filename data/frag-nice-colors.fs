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

float PI = 3.14159265359;

void main(void) {
  vec2 st = vertTexCoord;
  vec2 audio = texture2D(audioTexture, vec2(st.x, 0.0)).rg;
  vec2 fbst = ((st - 0.5) * 0.97) + 0.5;

  vec4 fb = texture2D(feedback, fbst);

  vec4 col = vec4(sin(st.x * vec4(13.0, 15.1, 15.3, 5.6) + (time * vec4(3.0, 5.1, 1.2, 8.3) + (audio.r * 32.0))) + sin(st.y * 4) * 0.29 * (audio.r * 5.0));

  col = col * sin(st.y*PI) * (level * 5.0);

  col = col * vec4(0.7, 0.5, 1.0, 1.0);

  gl_FragColor = (col * 0.8) + (fb * 0.9);
}












