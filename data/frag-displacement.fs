#ifdef GL_ES
precision mediump float;
precision mediump int;
#endif

#define PROCESSING_TEXTURE_SHADER

varying vec4 vertColor;
varying vec2 vertTexCoord;

uniform sampler2D feedback;
uniform sampler2D audioTexture;
uniform sampler2D whitney;

uniform float width;
uniform float height;
uniform float time;
uniform float level;

uniform vec2 mouse;

float PI = 3.14159265359;

void main(void) {
  vec2 st = vertTexCoord;

  vec4 t = texture2D(whitney, st);
  vec4 t2 = texture2D(whitney, st + t.rg * vec2(-0.2, 0.2));
  vec4 t3 = texture2D(whitney, st + t2.rb * vec2(-0.05, 0.05));

  gl_FragColor = t3;
}
