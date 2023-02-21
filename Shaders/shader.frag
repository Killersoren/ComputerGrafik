#version 460 core
//out vec4 FragColor;
//in vec4 colorToSendToFrag;

//uniform vec4 ourColor;

//in vec3 v2fColor;

out vec4 outputColor;
in vec2 texCoord;

uniform sampler2D texture0;

void main()
{
	//FragColor = vec4(ourColor, 1.0); //ourColor; //colorToSendToFrag; //vec4(0.9f, 0.8f, 0.3f, 1.0f);
	//outputColor = vec4(texCoord.x, texCoord.y,0,1);
	outputColor = texture(texture0, texCoord);


}