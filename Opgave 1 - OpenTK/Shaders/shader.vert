#version 460 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec3 v2fColor;

out vec2 texCoord;

uniform mat4 transform;

//uniform mat4 model;
//uniform mat4 view;
//uniform mat4 projection;

uniform mat4 mvp;

void main(void)
{
	texCoord = aTexCoord;

	gl_Position = vec4(aPosition, 1.0) * mvp;

}