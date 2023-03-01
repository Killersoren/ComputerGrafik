#version 460 core
//out vec4 FragColor;
//in vec4 colorToSendToFrag;

//uniform vec4 ourColor;

//in vec3 v2fColor;

out vec4 outputColor;
in vec2 texCoord;

uniform sampler2D texture0; //bricks
uniform sampler2D texture1; // Torben

uniform float xOffset;

out vec2 frag_uv;




void main()
{
	//FragColor = vec4(ourColor, 1.0); //ourColor; //colorToSendToFrag; //vec4(0.9f, 0.8f, 0.3f, 1.0f);
	//outputColor = vec4(texCoord.x, texCoord.y,0,1);
	
	//outputColor = texture(texture0, texCoord);

//	vec4 dst = texture2D(texture0 , frag_uv);
//	vec4 src = texture2D(texture1 , frag_uv);
//
//	float final_alpha = src.a + dst.a * (1.0 - src.a);
//	outputColor = vec4(
//        (src.rgb * src.a + dst.rgb * dst.a * (1.0 - src.a)) / final_alpha,
//        final_alpha
//    );
//


	vec4 dst= texture(texture0, texCoord);
	//vec4 src = texture(texture1, vec2(texCoord.x ,0.5*(1+sin(xOffset))+texCoord.y));
	vec4 src = texture(texture1, texCoord);
	float final_alpha = src.a + dst.a * (1.0 - src.a);
	outputColor = vec4((src.rgb * src.a + dst.rgb * dst.a * (1.0 - src.a)) /
	final_alpha,final_alpha );

	//outputColor = mix(texture(texture0, texCoord), texture(texture1, texCoord), 0.6);

	//outputColor = vec4((texture1.rgb * texture1.a + texture0.rgb * texture0.a * (1.0 - texture1.a) / texture1.a + texture0.a* (1.0- texture1.a)))

}