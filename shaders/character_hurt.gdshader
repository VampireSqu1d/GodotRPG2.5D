shader_type spatial;
render_mode unshaded, cull_disabled;

uniform bool active = false;
uniform vec4 flash_color: source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform sampler2D tex: source_color;


void fragment() {
	vec4 color = texture(tex, UV);
	ALPHA = color.a;
	if (active) {
		color = flash_color;
	}
	ALBEDO = vec3(color.r, color.g, color.b);
	
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
