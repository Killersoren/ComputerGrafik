﻿#version 330 core
layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;
layout (location = 2) in vec2 aTexCoords;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 fragPos;
out vec3 normal;
out vec2 texCoords;

void main()
{
    gl_Position = vec4(aPosition, 1.0) * model * view * projection;
    fragPos = vec3(vec4(aPosition, 1.0) * model);
    normal = aNormal * mat3(transpose(inverse(model)));
    texCoords = aTexCoords;
}