#version 460 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec2 aTexCoord;

out vec3 v2fColor;

out vec2 texCoord;
//out vec4 colorToSendToFrag;

void main()
{
//	colorToSendToFrag = vec4(aPosition, 1);
//	gl_Position = vec4(aPosition, 1.0);

	texCoord = aTexCoord;

	gl_Position = vec4(aPosition, 1.0);
	//ourColor = aColor;

}