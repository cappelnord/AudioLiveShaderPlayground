#ifdef GL_ES
precision mediump float;
precision mediump int;
#endif

#define PROCESSING_TEXTURE_SHADER

varying vec4 vertColor;
varying vec2 vertTexCoord;

uniform sampler2D feedback;
uniform sampler2D audioTexture;
uniform sampler2D circles;
uniform sampler2D whitney;

uniform float width;
uniform float height;
uniform float time;
uniform float level;

uniform vec2 mouse;

float PI = 3.14159265359;

void main(void) {
  vec2 st = vertTexCoord;

  st = floor(st * numPixels) / numPixels;

  vec3 bla = abs(sin(st.x * st.y * PI * vec3(40.0, 39.0, 38.0) * mouse.y + time));

  vec4 c = vec4(bla, 1.0);

  gl_FragColor = c;
}
