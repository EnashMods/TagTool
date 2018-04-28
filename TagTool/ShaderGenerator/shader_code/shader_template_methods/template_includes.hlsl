#include "albedo/default.hlsl"
#include "albedo/detail_blend.hlsl"
#include "albedo/constant_color.hlsl"
#include "albedo/two_change_color.hlsl"
#include "albedo/four_change_color.hlsl"
#include "albedo/three_detail_blend.hlsl"
#include "albedo/two_detail_overlay.hlsl"
#include "albedo/two_detail.hlsl"
#include "albedo/color_mask.hlsl"
#include "albedo/two_detail_black_point.hlsl"
#include "albedo/two_change_color_anim_overlay.hlsl"
#include "albedo/chameleon.hlsl"
#include "albedo/two_change_color_chameleon.hlsl"
#include "albedo/chameleon_masked.hlsl"
#include "albedo/color_mask_hard_light.hlsl"
#include "albedo/two_change_color_tex_overlay.hlsl"
#include "albedo/chameleon_albedo_masked.hlsl"

#include "bump_mapping/off.hlsl"
#include "bump_mapping/standard.hlsl"
#include "bump_mapping/detail.hlsl"
#include "bump_mapping/detail_masked.hlsl"

#include "alpha_test/none.hlsl"
#include "alpha_test/simple.hlsl"

#include "specular_mask/no_specular_mask.hlsl"
#include "specular_mask/specular_mask_from_diffuse.hlsl"
#include "specular_mask/specular_mask_from_texture.hlsl"
#include "specular_mask/specular_mask_from_color_texture.hlsl"

#include "material_mode/diffuse_only.hlsl"
#include "material_mode/cook_torrance.hlsl"
#include "material_mode/two_lobe_phong.hlsl"
#include "material_mode/foliage.hlsl"
#include "material_mode/none.hlsl"
#include "material_mode/glass.hlsl"
#include "material_mode/organism.hlsl"
#include "material_mode/single_lobe_phong.hlsl"
#include "material_mode/car_paint.hlsl"
#include "material_mode/hair.hlsl"

#include "environment_mapping/none.hlsl"
#include "environment_mapping/per_pixel.hlsl"
#include "environment_mapping/dynamic.hlsl"
#include "environment_mapping/from_flat_texture.hlsl"
#include "environment_mapping/custom_map.hlsl"

#include "self_illumination/off.hlsl"
#include "self_illumination/simple.hlsl"
#include "self_illumination/3_channel_self_illum.hlsl"
#include "self_illumination/plasma.hlsl"
#include "self_illumination/from_diffuse.hlsl"
#include "self_illumination/illum_detail.hlsl"
#include "self_illumination/meter.hlsl"
#include "self_illumination/self_illum_times_diffuse.hlsl"
#include "self_illumination/simple_with_alpha_mask.hlsl"
#include "self_illumination/simple_four_change_color.hlsl"
#include "self_illumination/illum_detail_world_space_four_cc.hlsl"

#include "blend_mode/opaque.hlsl"
#include "blend_mode/additive.hlsl"
#include "blend_mode/multiply.hlsl"
#include "blend_mode/alpha_blend.hlsl"
#include "blend_mode/double_multiply.hlsl"
#include "blend_mode/pre_multiplied_alpha.hlsl"

#include "parallax/off.hlsl"
#include "parallax/simple.hlsl"
#include "parallax/interpolated.hlsl"
#include "parallax/simple_detail.hlsl"

#include "misc/first_person_never.hlsl"
#include "misc/first_person_sometimes.hlsl"
#include "misc/first_person_always.hlsl"
#include "misc/first_person_never_with_rotating_bitmaps.hlsl"

#include "distortion/off.hlsl"
#include "distortion/on.hlsl"

#include "soft_fade/off.hlsl"
#include "soft_fade/on.hlsl"