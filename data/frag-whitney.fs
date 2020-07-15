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

vec2 adjustCoordinates(vec2 coords) {
	return (coords - vec2(0.5, 0.5)) * vec2(1.0, height/width);
}

void main(void) {
  vec2 st = adjustCoordinates(vertTexCoord);

  vec4 wTexR = texture2D(whitney, (vertTexCoord - vec2(0.5, 0.5)) * 1.0 + vec2(0.5, 0.5));
  vec4 wTexG = texture2D(whitney, (vertTexCoord - vec2(0.5, 0.5)) * (1.0-level) + vec2(0.5, 0.5));
  vec4 wTexB = texture2D(whitney, (vertTexCoord - vec2(0.5, 0.5)) * (1.0-level*2) + vec2(0.5, 0.5));

  gl_FragColor = vec4(wTexR.g, wTexG.g, wTexB.g, wTexR.a);
}