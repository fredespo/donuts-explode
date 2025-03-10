#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1>
struct VirtualActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1, typename T2>
struct VirtualFuncInvoker2
{
	typedef R (*Func)(void*, T1, T2, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};

struct Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682;
struct Dictionary_2_t8692D65464844EB649ABD241057B7447F5B945B6;
struct Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232;
struct Dictionary_2_t02150DAF80DD1F49C7D778D850960419D8DF445A;
struct Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710;
struct IEnumerable_1_tF95C9E01A913DD50575531C8305932628663D9E9;
struct IEnumerable_1_t273B98BF36B2463299ABDCDE05D7CA29FDC88557;
struct IEqualityComparer_1_tDBFC8496F14612776AF930DBF84AFE7D06D1F0E9;
struct KeyCollection_t66E188B36DF8E1A23EBDA7253CDCE978CD3EF298;
struct KeyCollection_t9A9C42D197B12107BDA92805EF1F796FCC760732;
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73;
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
struct List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B;
struct ValueCollection_t95D69F17F87935E5166EE0E3C174BB8A8F051F5B;
struct ValueCollection_t8D12F468CADE655C2F49E97F8F3ACC0CBA569ADF;
struct EntryU5BU5D_t6E8F51999B5C2D1A5E09B1963CF042E7B5CAA500;
struct EntryU5BU5D_tB35BD67B4E02E95C4F2984FDEEF40DE6EDE4ACCC;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6;
struct RenderTextureU5BU5D_t9C963C4B9AAD862BBE402147E82F7BEBF699F6A6;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct Texture2DU5BU5D_t05332F1E3F7D4493E304C702201F9BE4F9236191;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354;
struct AutoExposure_t3DC0981C2B844D7B8E12CE9C8C731F4387846F5C;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184;
struct CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7;
struct ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8;
struct FieldInfo_t;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5;
struct IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA;
struct LogHistogram_tFD0177A61EF64A4720A90B188FB18B1AEAC02A24;
struct Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3;
struct MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A;
struct PostProcessDebugLayer_tD1025B624D67FB5F369C673972D2219EFF179D02;
struct PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7;
struct PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D;
struct PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397;
struct PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D;
struct RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692;
struct Spline_tD8C641273FEAA3A7958A261C41D694636049CB21;
struct String_t;
struct TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C;
struct TemporalAntialiasing_t66F588EBDAB51F3C7094E5495132B8256CB0C644;
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700;
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4;
struct Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1;
struct TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct ComputeShaders_t8F1C8B34C544DEC3B4D302509211C37989AAC39F;
struct SMAALuts_t62105E31B4A58EC4A668D9C79B9720F7CD5C3CB2;
struct Shaders_t2934A1A9726776BE88E31A97A67A9BD9ACEED86B;

IL2CPP_EXTERN_C RuntimeClass* AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral3E63EA4D6F8144DD6406580EE9A7B6F874A529E4;
IL2CPP_EXTERN_C String_t* _stringLiteral4F889A7069FB348E59778EC4C275B6A09FE34752;
IL2CPP_EXTERN_C String_t* _stringLiteral5ED45B85ADA1AFA9C8962A3063BC0DB7FA04521E;
IL2CPP_EXTERN_C String_t* _stringLiteral6067E93B7ED6BC9634C2207045961FBB1126B92A;
IL2CPP_EXTERN_C String_t* _stringLiteral96AFB5CB3B476CE64056EF8716AEA14B385714ED;
IL2CPP_EXTERN_C String_t* _stringLiteral9C7F6CFACBA60E7454344DA7A7EB629EB0C4A8F2;
IL2CPP_EXTERN_C String_t* _stringLiteralADD218EEEC60165FC319ED66CAD59FFB5160614D;
IL2CPP_EXTERN_C String_t* _stringLiteralD754031EB09A4061A82F00A1E77A9972C831A1FB;
IL2CPP_EXTERN_C String_t* _stringLiteralF51190EEE90545F9CD168B86B0A73EF3C85E3A2C;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_TryGetValue_m7D68C17F2EFEADA12A803841CFD7C5A8D3ED159B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_mAEC79FC89BB27663084F0D041157806C2B4858B4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_AddRange_mEC23918422A46D768ED6755FC601960B173E6D8B_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m9F1BA8EEA06819CC244EC23459573BB8E8D32C78_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_RemoveAt_m164E41C28FA43F25F51C417135CF61976ECF3B69_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_mFD8C82612D4654B884F2F68C8C1D93B56A1E0081_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeType* ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_0_0_0_var;
IL2CPP_EXTERN_C const RuntimeType* TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_0_0_0_var;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3;
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
struct ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6;
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets;
	EntryU5BU5D_t6E8F51999B5C2D1A5E09B1963CF042E7B5CAA500* ____entries;
	int32_t ____count;
	int32_t ____freeList;
	int32_t ____freeCount;
	int32_t ____version;
	RuntimeObject* ____comparer;
	KeyCollection_t66E188B36DF8E1A23EBDA7253CDCE978CD3EF298* ____keys;
	ValueCollection_t95D69F17F87935E5166EE0E3C174BB8A8F051F5B* ____values;
	RuntimeObject* ____syncRoot;
};
struct Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets;
	EntryU5BU5D_tB35BD67B4E02E95C4F2984FDEEF40DE6EDE4ACCC* ____entries;
	int32_t ____count;
	int32_t ____freeList;
	int32_t ____freeCount;
	int32_t ____version;
	RuntimeObject* ____comparer;
	KeyCollection_t9A9C42D197B12107BDA92805EF1F796FCC760732* ____keys;
	ValueCollection_t8D12F468CADE655C2F49E97F8F3ACC0CBA569ADF* ____values;
	RuntimeObject* ____syncRoot;
};
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B  : public RuntimeObject
{
	RenderTextureU5BU5D_t9C963C4B9AAD862BBE402147E82F7BEBF699F6A6* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct U3CPrivateImplementationDetailsU3E_t60D390955C994A4CAC0539E533ED349C64C34EF4  : public RuntimeObject
{
};
struct Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA  : public RuntimeObject
{
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397  : public RuntimeObject
{
	MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* ___U3CpropertiesU3Ek__BackingField;
	Material_t18053F08F347D0DCA5E1140EC7EC4533DD8A14E3* ___U3CmaterialU3Ek__BackingField;
};
struct PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D  : public RuntimeObject
{
	Dictionary_2_t02150DAF80DD1F49C7D778D850960419D8DF445A* ___m_Sheets;
};
struct ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99  : public RuntimeObject
{
};
struct Spline_tD8C641273FEAA3A7958A261C41D694636049CB21  : public RuntimeObject
{
	AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* ___curve;
	bool ___m_Loop;
	float ___m_ZeroValue;
	float ___m_Range;
	AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* ___m_InternalLoopingCurve;
	int32_t ___frameCount;
	SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* ___cachedData;
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C  : public RuntimeObject
{
	List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* ___m_Pool;
	int32_t ___m_Current;
};
struct TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D  : public RuntimeObject
{
};
struct TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424  : public RuntimeObject
{
	CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___m_Command;
	PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* ___m_PropertySheets;
	PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* ___m_Resources;
	List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* ___m_Recycled;
	List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* ___m_Actives;
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct ComputeShaders_t8F1C8B34C544DEC3B4D302509211C37989AAC39F  : public RuntimeObject
{
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___autoExposure;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___exposureHistogram;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___lut3DBaker;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___texture3dLerp;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___gammaHistogram;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___waveform;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___vectorscope;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___multiScaleAODownsample1;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___multiScaleAODownsample2;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___multiScaleAORender;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___multiScaleAOUpsample;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___gaussianDownsample;
};
struct Shaders_t2934A1A9726776BE88E31A97A67A9BD9ACEED86B  : public RuntimeObject
{
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___bloom;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___copy;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___copyStd;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___copyStdFromTexArray;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___copyStdFromDoubleWide;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___discardAlpha;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___depthOfField;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___finalPass;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___grainBaker;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___motionBlur;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___temporalAntialiasing;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___subpixelMorphologicalAntialiasing;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___texture2dLerp;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___uber;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___lut2DBaker;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___lightMeter;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___gammaHistogram;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___waveform;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___vectorscope;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___debugOverlays;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___deferredFog;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___scalableAO;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___multiScaleAO;
	Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___screenSpaceReflections;
};
struct Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A 
{
	List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* ____list;
	int32_t ____index;
	int32_t ____version;
	RuntimeObject* ____current;
};
struct Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 
{
	List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* ____list;
	int32_t ____index;
	int32_t ____version;
	RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* ____current;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Color_tD001788D726C3A7F1379BEED0260B9591F440C1F 
{
	float ___r;
	float ___g;
	float ___b;
	float ___a;
};
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct FieldInfo_t  : public MemberInfo_t
{
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 
{
	float ___m_Time;
	float ___m_Value;
	float ___m_InTangent;
	float ___m_OutTangent;
	int32_t ___m_WeightedMode;
	float ___m_InWeight;
	float ___m_OutWeight;
};
struct ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A  : public Attribute_tFDA8EFEFB0711976D22474794576DAF28F7440AA
{
	String_t* ____message;
	bool ____error;
};
struct Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D 
{
	float ___m_XMin;
	float ___m_YMin;
	float ___m_Width;
	float ___m_Height;
};
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	float ___m_value;
};
struct UInt32_t1833D51FFA667B18A5AA4B8D34DE284F8495D29B 
{
	uint32_t ___m_value;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 
{
	float ___x;
	float ___y;
};
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 
{
	float ___x;
	float ___y;
	float ___z;
	float ___w;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D20_tE536C13103641FC6425E75BF920C347C0C2F39A0 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D20_tE536C13103641FC6425E75BF920C347C0C2F39A0__padding[20];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D5941_tB91938418F882A3AF5F00ADFE1F2392D38DA543F 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D5941_tB91938418F882A3AF5F00ADFE1F2392D38DA543F__padding[5941];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D6082_t47F7F4CFED0C129AFC46BFEE7C2DBE26A32643E6 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D6082_t47F7F4CFED0C129AFC46BFEE7C2DBE26A32643E6__padding[6082];
	};
};
#pragma pack(pop, tp)
struct Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 
{
	bool ___hasValue;
	Rect_tA04E0F8A1830E767F40FB27ECD8D309303571F0D ___value;
};
struct AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354  : public RuntimeObject
{
	intptr_t ___m_Ptr;
};
struct AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354_marshaled_pinvoke
{
	intptr_t ___m_Ptr;
};
struct AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354_marshaled_com
{
	intptr_t ___m_Ptr;
};
struct BuiltinRenderTextureType_t3D56813CAC7C6E4AC3B438039BD1CE7E62FE7C4E 
{
	int32_t ___value__;
};
struct CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7  : public RuntimeObject
{
	intptr_t ___m_Ptr;
};
struct CubemapFace_t300D6E2CD7DF60D44AA28338748B607677ED1D1B 
{
	int32_t ___value__;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct FilterMode_t4AD57F1A3FE272D650E0E688BA044AE872BD2A34 
{
	int32_t ___value__;
};
struct GraphicsFormat_tC3D1898F3F3F1F57256C7F3FFD6BA9A37AE7E713 
{
	int32_t ___value__;
};
struct Int32Enum_tCBAC8BA2BFF3A845FA599F303093BBBA374B6F0C 
{
	int32_t ___value__;
};
struct MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D  : public RuntimeObject
{
	intptr_t ___m_Ptr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct RenderTextureCreationFlags_t1C01993691E5BA956575134696509089FE852F50 
{
	int32_t ___value__;
};
struct RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F 
{
	int32_t ___value__;
};
struct RenderTextureMemoryless_tE3B7F3AE353C3E9ACF86076376EB862131D19A69 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct ShadowSamplingMode_t8BE740C4258CFEDDBAC01FDC0438D8EE3F776BA8 
{
	int32_t ___value__;
};
struct TextureDimension_t8D7148B9168256EE1E9AF91378ABA148888CE642 
{
	int32_t ___value__;
};
struct TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D 
{
	int32_t ___value__;
};
struct TextureWrapMode_tF9851343029052ED45668D1C99BAE09B2CCC13AD 
{
	int32_t ___value__;
};
struct VRTextureUsage_t57FAA0077810142A461D74EDC5E33FC3D78BD2E8 
{
	int32_t ___value__;
};
struct Antialiasing_tB22ED24BC70A5C2A185CC71C43AA2C5DA4D85E72 
{
	int32_t ___value__;
};
struct StereoRenderingMode_t2F51C8E4E7B9A3D74D89A9D0EA83182D8B6C5424 
{
	int32_t ___value__;
};
struct ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B 
{
	int32_t ___m_Type;
	int32_t ___m_NameID;
	int32_t ___m_InstanceID;
	intptr_t ___m_BufferPointer;
	int32_t ___m_MipLevel;
	int32_t ___m_CubeFace;
	int32_t ___m_DepthSlice;
};
struct RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 
{
	int32_t ___U3CwidthU3Ek__BackingField;
	int32_t ___U3CheightU3Ek__BackingField;
	int32_t ___U3CmsaaSamplesU3Ek__BackingField;
	int32_t ___U3CvolumeDepthU3Ek__BackingField;
	int32_t ___U3CmipCountU3Ek__BackingField;
	int32_t ____graphicsFormat;
	int32_t ___U3CstencilFormatU3Ek__BackingField;
	int32_t ___U3CdepthStencilFormatU3Ek__BackingField;
	int32_t ___U3CdimensionU3Ek__BackingField;
	int32_t ___U3CshadowSamplingModeU3Ek__BackingField;
	int32_t ___U3CvrUsageU3Ek__BackingField;
	int32_t ____flags;
	int32_t ___U3CmemorylessU3Ek__BackingField;
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};
struct Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295  : public Exception_t
{
};
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A  : public SystemException_tCC48D868298F4C0705279823E34B00F4FBDB7295
{
};
struct PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7  : public RuntimeObject
{
	Camera_tA92CC927D7439999BC82DBEDC0AA45B470F9E184* ___m_Camera;
	CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___U3CcommandU3Ek__BackingField;
	RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B ___U3CsourceU3Ek__BackingField;
	RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B ___U3CdestinationU3Ek__BackingField;
	int32_t ___U3CsourceFormatU3Ek__BackingField;
	bool ___U3CflipU3Ek__BackingField;
	PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* ___U3CresourcesU3Ek__BackingField;
	PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* ___U3CpropertySheetsU3Ek__BackingField;
	Dictionary_2_tA348003A3C1CEFB3096E9D2A0BC7F1AC8EC4F710* ___U3CuserDataU3Ek__BackingField;
	PostProcessDebugLayer_tD1025B624D67FB5F369C673972D2219EFF179D02* ___U3CdebugLayerU3Ek__BackingField;
	int32_t ___U3CwidthU3Ek__BackingField;
	int32_t ___U3CheightU3Ek__BackingField;
	bool ___U3CstereoActiveU3Ek__BackingField;
	int32_t ___U3CxrActiveEyeU3Ek__BackingField;
	int32_t ___U3CnumberOfEyesU3Ek__BackingField;
	int32_t ___U3CstereoRenderingModeU3Ek__BackingField;
	int32_t ___U3CscreenWidthU3Ek__BackingField;
	int32_t ___U3CscreenHeightU3Ek__BackingField;
	bool ___U3CisSceneViewU3Ek__BackingField;
	int32_t ___U3CantialiasingU3Ek__BackingField;
	TemporalAntialiasing_t66F588EBDAB51F3C7094E5495132B8256CB0C644* ___U3CtemporalAntialiasingU3Ek__BackingField;
	PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* ___uberSheet;
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___autoExposureTexture;
	LogHistogram_tFD0177A61EF64A4720A90B188FB18B1AEAC02A24* ___logHistogram;
	Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___logLut;
	AutoExposure_t3DC0981C2B844D7B8E12CE9C8C731F4387846F5C* ___autoExposure;
	int32_t ___bloomBufferNameID;
	bool ___physicalCamera;
	RenderTextureDescriptor_t69845881CE6437E4E61F92074F2F84079F23FA46 ___m_sourceDescriptor;
};
struct PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	Texture2DU5BU5D_t05332F1E3F7D4493E304C702201F9BE4F9236191* ___blueNoise64;
	Texture2DU5BU5D_t05332F1E3F7D4493E304C702201F9BE4F9236191* ___blueNoise256;
	SMAALuts_t62105E31B4A58EC4A668D9C79B9720F7CD5C3CB2* ___smaaLuts;
	Shaders_t2934A1A9726776BE88E31A97A67A9BD9ACEED86B* ___shaders;
	ComputeShaders_t8F1C8B34C544DEC3B4D302509211C37989AAC39F* ___computeShaders;
};
struct RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27  : public Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700
{
};
struct Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4  : public Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700
{
};
struct Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1  : public Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700
{
};
struct List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_StaticFields
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ___s_emptyArray;
};
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray;
};
struct List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B_StaticFields
{
	RenderTextureU5BU5D_t9C963C4B9AAD862BBE402147E82F7BEBF699F6A6* ___s_emptyArray;
};
struct U3CPrivateImplementationDetailsU3E_t60D390955C994A4CAC0539E533ED349C64C34EF4_StaticFields
{
	__StaticArrayInitTypeSizeU3D5941_tB91938418F882A3AF5F00ADFE1F2392D38DA543F ___250F72F5422D05154E53F836C8F4BD80892D01BC28F3D870BD57D854D08E80B4;
	__StaticArrayInitTypeSizeU3D20_tE536C13103641FC6425E75BF920C347C0C2F39A0 ___BEE794DBCD4CBBBF1C7DAF41EF192F0C8543F2C04DCD5D88F445D71BB75FC08A;
	__StaticArrayInitTypeSizeU3D6082_t47F7F4CFED0C129AFC46BFEE7C2DBE26A32643E6 ___E8E20156808282C9A98EDCADFB5DF558E04C185FC4287D1F9D3963B775FAC031;
};
struct ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_StaticFields
{
	int32_t ___MainTex;
	int32_t ___Jitter;
	int32_t ___Sharpness;
	int32_t ___FinalBlendParameters;
	int32_t ___HistoryTex;
	int32_t ___SMAA_Flip;
	int32_t ___SMAA_Flop;
	int32_t ___AOParams;
	int32_t ___AOColor;
	int32_t ___OcclusionTexture1;
	int32_t ___OcclusionTexture2;
	int32_t ___SAOcclusionTexture;
	int32_t ___MSVOcclusionTexture;
	int32_t ___DepthCopy;
	int32_t ___LinearDepth;
	int32_t ___LowDepth1;
	int32_t ___LowDepth2;
	int32_t ___LowDepth3;
	int32_t ___LowDepth4;
	int32_t ___TiledDepth1;
	int32_t ___TiledDepth2;
	int32_t ___TiledDepth3;
	int32_t ___TiledDepth4;
	int32_t ___Occlusion1;
	int32_t ___Occlusion2;
	int32_t ___Occlusion3;
	int32_t ___Occlusion4;
	int32_t ___Combined1;
	int32_t ___Combined2;
	int32_t ___Combined3;
	int32_t ___SSRResolveTemp;
	int32_t ___Noise;
	int32_t ___Test;
	int32_t ___Resolve;
	int32_t ___History;
	int32_t ___ViewMatrix;
	int32_t ___InverseViewMatrix;
	int32_t ___ScreenSpaceProjectionMatrix;
	int32_t ___Params2;
	int32_t ___FogColor;
	int32_t ___FogParams;
	int32_t ___VelocityScale;
	int32_t ___MaxBlurRadius;
	int32_t ___RcpMaxBlurRadius;
	int32_t ___VelocityTex;
	int32_t ___Tile2RT;
	int32_t ___Tile4RT;
	int32_t ___Tile8RT;
	int32_t ___TileMaxOffs;
	int32_t ___TileMaxLoop;
	int32_t ___TileVRT;
	int32_t ___NeighborMaxTex;
	int32_t ___LoopCount;
	int32_t ___DepthOfFieldTemp;
	int32_t ___DepthOfFieldTex;
	int32_t ___Distance;
	int32_t ___LensCoeff;
	int32_t ___MaxCoC;
	int32_t ___RcpMaxCoC;
	int32_t ___RcpAspect;
	int32_t ___CoCTex;
	int32_t ___TaaParams;
	int32_t ___AutoExposureTex;
	int32_t ___HistogramBuffer;
	int32_t ___Params;
	int32_t ___ScaleOffsetRes;
	int32_t ___BloomTex;
	int32_t ___SampleScale;
	int32_t ___Threshold;
	int32_t ___ColorIntensity;
	int32_t ___Bloom_DirtTex;
	int32_t ___Bloom_Settings;
	int32_t ___Bloom_Color;
	int32_t ___Bloom_DirtTileOffset;
	int32_t ___ChromaticAberration_Amount;
	int32_t ___ChromaticAberration_SpectralLut;
	int32_t ___Distortion_CenterScale;
	int32_t ___Distortion_Amount;
	int32_t ___Lut2D;
	int32_t ___Lut3D;
	int32_t ___Lut3D_Params;
	int32_t ___Lut2D_Params;
	int32_t ___UserLut2D_Params;
	int32_t ___PostExposure;
	int32_t ___ColorBalance;
	int32_t ___ColorFilter;
	int32_t ___HueSatCon;
	int32_t ___Brightness;
	int32_t ___ChannelMixerRed;
	int32_t ___ChannelMixerGreen;
	int32_t ___ChannelMixerBlue;
	int32_t ___Lift;
	int32_t ___InvGamma;
	int32_t ___Gain;
	int32_t ___Curves;
	int32_t ___CustomToneCurve;
	int32_t ___ToeSegmentA;
	int32_t ___ToeSegmentB;
	int32_t ___MidSegmentA;
	int32_t ___MidSegmentB;
	int32_t ___ShoSegmentA;
	int32_t ___ShoSegmentB;
	int32_t ___Vignette_Color;
	int32_t ___Vignette_Center;
	int32_t ___Vignette_Settings;
	int32_t ___Vignette_Mask;
	int32_t ___Vignette_Opacity;
	int32_t ___Vignette_Mode;
	int32_t ___Grain_Params1;
	int32_t ___Grain_Params2;
	int32_t ___GrainTex;
	int32_t ___Phase;
	int32_t ___GrainNoiseParameters;
	int32_t ___LumaInAlpha;
	int32_t ___DitheringTex;
	int32_t ___Dithering_Coords;
	int32_t ___From;
	int32_t ___To;
	int32_t ___Interp;
	int32_t ___TargetColor;
	int32_t ___HalfResFinalCopy;
	int32_t ___WaveformSource;
	int32_t ___WaveformBuffer;
	int32_t ___VectorscopeBuffer;
	int32_t ___RenderViewportScaleFactor;
	int32_t ___UVTransform;
	int32_t ___DepthSlice;
	int32_t ___UVScaleOffset;
	int32_t ___PosScaleOffset;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields
{
	Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* ___s_FormatAliasMap;
	Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* ___s_SupportedRenderTextureFormats;
	Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* ___s_SupportedTextureFormats;
};
struct TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_StaticFields
{
	TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* ___m_Instance;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7_StaticFields
{
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___zeroVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___oneVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___upVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___downVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___leftVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___rightVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___positiveInfinityVector;
	Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___negativeInfinityVector;
};
struct Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3_StaticFields
{
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___zeroVector;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___oneVector;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___positiveInfinityVector;
	Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___negativeInfinityVector;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700_StaticFields
{
	int32_t ___GenerateAllMips;
};
struct Type_t_StaticFields
{
	Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235* ___s_defaultBinder;
	Il2CppChar ___Delimiter;
	TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB* ___EmptyTypes;
	RuntimeObject* ___Missing;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterAttribute;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterName;
	MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553* ___FilterNameIgnoreCase;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C  : public RuntimeArray
{
	ALIGN_FIELD (8) float m_Items[1];

	inline float GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline float* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, float value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline float GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline float* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, float value)
	{
		m_Items[index] = value;
	}
};
struct KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3  : public RuntimeArray
{
	ALIGN_FIELD (8) Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 m_Items[1];

	inline Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 value)
	{
		m_Items[index] = value;
	}
};
struct ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6  : public RuntimeArray
{
	ALIGN_FIELD (8) ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A* m_Items[1];

	inline ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C  : public RuntimeArray
{
	ALIGN_FIELD (8) int32_t m_Items[1];

	inline int32_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline int32_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, int32_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline int32_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline int32_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, int32_t value)
	{
		m_Items[index] = value;
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8_gshared (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_gshared (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_mC9C175D184F142682355D54F32FC231BBDCE6FD0_gshared (Dictionary_2_t8692D65464844EB649ABD241057B7447F5B945B6* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_m95641EE4325D83C122BF87ECF2A0F729A1CF14D3_gshared (Dictionary_2_t8692D65464844EB649ABD241057B7447F5B945B6* __this, int32_t ___0_key, int32_t ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_gshared (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_gshared (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, int32_t ___0_key, bool ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_TryGetValue_m9C7A828C0B5419633EB1A5AA73FC2E06284E639E_gshared (Dictionary_2_t8692D65464844EB649ABD241057B7447F5B945B6* __this, int32_t ___0_key, int32_t* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_gshared (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, int32_t ___0_key, bool* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_AddRange_m1F76B300133150E6046C5FED00E88B5DE0A02E17_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1_RemoveAt_m54F62297ADEE4D4FDA697F49ED807BF901201B54_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m378B392086AAB6F400944FA9839516326B3F7BB8_gshared (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_item, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_NO_INLINE IL2CPP_METHOD_ATTR void List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AnimationCurve_get_length_m259A67BB0870D3A153F6FEDBB06CB0D24089CD81 (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnimationCurve__ctor_m0D976567166F92383307DC8EB8D7082CD34E226F (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 AnimationCurve_get_Item_mD4E73EE674F7A82673F1A9CEB8E5EF86BA47E64B (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, int32_t ___0_index, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Keyframe_get_time_mB8886F64CBB373936C0C25C4C68397C05779F661 (Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Keyframe_set_time_m98F165193486C0DF1611B562016595B18052A2D6 (Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* __this, float ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3* AnimationCurve_get_keys_m34452C69464AB459C04BFFEA4F541F06B419AC4E (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AnimationCurve_set_keys_mBE1284B44CDBB1D8381177A3D581A6E71467F95C (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t AnimationCurve_AddKey_mBB48B2229D76098019DDAA1F2BAAEFE59F47CCCC (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 ___0_key, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Spline_Evaluate_m908B44D36171764274B429D923E47F36BF972516 (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, float ___0_t, int32_t ___1_length, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Time_get_renderedFrameCount_m65102648E50AC7B110E619C91D1B67DEA53D7DBE (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AnimationCurve_Evaluate_m50B857043DE251A186032ADBCBB4CEF817F4EE3C (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* __this, float ___0_time, const RuntimeMethod* method) ;
inline void List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8 (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, const RuntimeMethod*))List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TargetPool_Get_m0547F5C89835B0586BE4B46593AD5A388FF434B0 (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TargetPool_Get_m3E7EF2A9C513F78863718A8237F3A0277A33DC1A (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, int32_t ___0_i, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, const RuntimeMethod*))List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline)(__this, method);
}
inline int32_t List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_gshared)(__this, ___0_index, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5 (int32_t* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Shader_PropertyToID_mE98523D50F5656CAE89B30695C458253EB8956CA (String_t* ___0_name, const RuntimeMethod* method) ;
inline void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline)(__this, ___0_item, method);
}
inline void Dictionary_2__ctor_mAEC79FC89BB27663084F0D041157806C2B4858B4 (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232*, const RuntimeMethod*))Dictionary_2__ctor_mC9C175D184F142682355D54F32FC231BBDCE6FD0_gshared)(__this, method);
}
inline void Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* __this, int32_t ___0_key, int32_t ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232*, int32_t, int32_t, const RuntimeMethod*))Dictionary_2_Add_m95641EE4325D83C122BF87ECF2A0F729A1CF14D3_gshared)(__this, ___0_key, ___1_value, method);
}
inline void Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024 (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682*, const RuntimeMethod*))Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57 (RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ___0_handle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeArray* Enum_GetValues_m803B9D68C367FAABC5AFB6B5B52775C8A573CEF9 (Type_t* ___0_enumType, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Array_GetEnumerator_mDB7E2AF23F2BDC715D429C71CA3B8D0151F0DC1E (RuntimeArray* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TextureFormatUtilities_IsObsolete_m6196F51252D89FDE496B58689F62279DD86D23BC (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SystemInfo_SupportsRenderTextureFormat_mCCC3C69578A2C5B7367F73999E6938C315A98201 (int32_t ___0_format, const RuntimeMethod* method) ;
inline void Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48 (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, int32_t ___0_key, bool ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682*, int32_t, bool, const RuntimeMethod*))Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_gshared)(__this, ___0_key, ___1_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool SystemInfo_SupportsTextureFormat_m833B0ABED13B5B8D0D4BCF082F3EFA51A3B5C860 (int32_t ___0_format, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Type_t* Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FieldInfo_t* Type_GetField_m0BF55B1A27A1B6AB6D3477E7F9E1CF2A3451E1E0 (Type_t* __this, String_t* ___0_name, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RenderTexture_get_format_m58556ABB91A1FADA8044BEEA2E8C55280768CF35 (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Texture2D_get_format_mE39DD922F83CA1097383309278BB6F20636A7D9D (Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4* __this, const RuntimeMethod* method) ;
inline bool Dictionary_2_TryGetValue_m7D68C17F2EFEADA12A803841CFD7C5A8D3ED159B (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* __this, int32_t ___0_key, int32_t* ___1_value, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232*, int32_t, int32_t*, const RuntimeMethod*))Dictionary_2_TryGetValue_m9C7A828C0B5419633EB1A5AA73FC2E06284E639E_gshared)(__this, ___0_key, ___1_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* __this, String_t* ___0_message, const RuntimeMethod* method) ;
inline bool Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* __this, int32_t ___0_key, bool* ___1_value, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682*, int32_t, bool*, const RuntimeMethod*))Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_gshared)(__this, ___0_key, ___1_value, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureLerper__ctor_m326A48946F64C5733E75607AE62A9D247FC7E67D (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, const RuntimeMethod* method) ;
inline void List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49 (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* PostProcessRenderContext_get_command_m028BE33B6194640A1DE901A6F935658034A3E2CD_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* PostProcessRenderContext_get_propertySheets_m60E7825143611FEC183803150D8F7C2785514D79_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* PostProcessRenderContext_get_resources_m89879DF69E4B910F9EE3008AB8DC60B732ABF02A_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) ;
inline int32_t List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_inline (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
inline Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, const RuntimeMethod* method)
{
	return ((  Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, const RuntimeMethod*))List_1_GetEnumerator_mD8294A7FA2BEB1929487127D476F8EC1CDC23BFC_gshared)(__this, method);
}
inline void Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4 (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693* __this, const RuntimeMethod* method)
{
	((  void (*) (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693*, const RuntimeMethod*))Enumerator_Dispose_mD9DC3E3C3697830A4823047AB29A77DBBB5ED419_gshared)(__this, method);
}
inline RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_inline (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693* __this, const RuntimeMethod* method)
{
	return ((  RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* (*) (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693*, const RuntimeMethod*))Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeUtilities_Destroy_m88860DDA45529FA1193643863F052D709087B493 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_obj, const RuntimeMethod* method) ;
inline bool Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5 (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693*, const RuntimeMethod*))Enumerator_MoveNext_mE921CC8F29FBBDE7CC3209A0ED0D921D58D00BCB_gshared)(__this, method);
}
inline void List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_inline (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, const RuntimeMethod*))List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline)(__this, method);
}
inline void List_1_AddRange_mEC23918422A46D768ED6755FC601960B173E6D8B (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, RuntimeObject* ___0_collection, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, RuntimeObject*, const RuntimeMethod*))List_1_AddRange_m1F76B300133150E6046C5FED00E88B5DE0A02E17_gshared)(__this, ___0_collection, method);
}
inline RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* List_1_get_Item_mFD8C82612D4654B884F2F68C8C1D93B56A1E0081 (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___0_index, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool RenderTexture_get_enableRandomWrite_m64B49A97B19ACC25765E27F8F8A39F625D4D20CD (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RenderTexture__ctor_m53215A8EDDE262932758186108347685F6A512C4 (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, int32_t ___0_width, int32_t ___1_height, int32_t ___2_depth, int32_t ___3_format, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Texture_set_filterMode_mE423E58C0C16D059EA62BA87AD70F44AEA50CCC9 (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Texture_set_wrapMode_m1F74A690E3883EC9C5C371D502D09642F15D0F7E (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Texture_set_anisoLevel_m768759DE9D6BE15FCE995F5FC468980B904B9D1F (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RenderTexture_set_volumeDepth_mD9B1E6BA4BE6B1741427B34A23B9D48BA9493633 (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RenderTexture_set_enableRandomWrite_m1F1B5E033802C193449803411560DB4D5D4AEEAB (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, bool ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool RenderTexture_Create_mA6E4D3CCC84AC3F68E85AA0D6609E1692C672AD2 (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* __this, const RuntimeMethod* method) ;
inline void List_1_RemoveAt_m164E41C28FA43F25F51C417135CF61976ECF3B69 (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, int32_t, const RuntimeMethod*))List_1_RemoveAt_m54F62297ADEE4D4FDA697F49ED807BF901201B54_gshared)(__this, ___0_index, method);
}
inline void List_1_Add_m9F1BA8EEA06819CC244EC23459573BB8E8D32C78_inline (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* __this, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___0_item, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Texture3D_get_depth_m08A40112C90FB3346068195C2B83FEB544313169 (Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline (int32_t ___0_a, int32_t ___1_b, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, int32_t ___0_format, int32_t ___1_w, int32_t ___2_h, int32_t ___3_d, bool ___4_enableRandomWrite, bool ___5_force3D, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ComputeShader_FindKernel_m3BA5C50794FA6AF4C432E55FBBD7CB266532F659 (ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* __this, String_t* ___0_name, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline (Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3* __this, float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CommandBuffer_SetComputeVectorParam_mCB04E8C59D63D6CDCA0E8EDA362BE1CB7BF49709 (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* __this, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_computeShader, String_t* ___1_name, Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___2_val, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___0_tex, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174 (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* __this, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_computeShader, int32_t ___1_kernelIndex, String_t* ___2_name, RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B ___3_rt, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ComputeShader_GetKernelThreadGroupSizes_m693428494DB1FD3CFC69FCE4E0093A2C4AAE1CBE (ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* __this, int32_t ___0_kernelIndex, uint32_t* ___1_x, uint32_t* ___2_y, uint32_t* ___3_z, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Mathf_CeilToInt_mF2BF9F4261B3431DC20E10A46CFEEED103C48963_inline (float ___0_f, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CommandBuffer_DispatchCompute_mF9F5605B77F0480FD4B8C3BCAEC2FC59A24E31A2 (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* __this, ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* ___0_computeShader, int32_t ___1_kernelIndex, int32_t ___2_threadGroupsX, int32_t ___3_threadGroupsY, int32_t ___4_threadGroupsZ, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___0_texture, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* PropertySheetFactory_Get_mCFDB007DD001F66FCC0EAD7549B63C74857569FC (PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* __this, Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* ___0_shader, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline (PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MaterialPropertyBlock_SetTexture_m39F531D3F35D6C5B661A7B4F07DD7B8ACC22627F (MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* __this, int32_t ___0_nameID, Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MaterialPropertyBlock_SetFloat_m6BA8DA03FAD1ABA0BD339E0E5157C4DF3C987267 (MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* __this, int32_t ___0_nameID, float ___1_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeUtilities_BlitFullscreenTriangle_m5E84F777CA552E3540C83CDAEB6C2075F8406E16 (CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* ___0_cmd, RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B ___1_source, RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B ___2_destination, PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* ___3_propertySheet, int32_t ___4_pass, bool ___5_clear, Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 ___6_viewport, bool ___7_preserveDepth, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MaterialPropertyBlock_SetVector_m22B010D99231EF5684063F4A07F5948854D590B3 (MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* __this, int32_t ___0_nameID, Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 ___1_value, const RuntimeMethod* method) ;
inline void List_1_AddWithResize_m378B392086AAB6F400944FA9839516326B3F7BB8 (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*, int32_t, const RuntimeMethod*))List_1_AddWithResize_m378B392086AAB6F400944FA9839516326B3F7BB8_gshared)(__this, ___0_item, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB (RuntimeArray* ___0_array, int32_t ___1_index, int32_t ___2_length, const RuntimeMethod* method) ;
inline void List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4 (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4_gshared)(__this, ___0_item, method);
}
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 56373
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Spline__ctor_mF4CB5FE207BD36B6A8B1425A3CEC164DD61B74B3 (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* ___0_curve, float ___1_zeroValue, bool ___2_loop, Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7 ___3_bounds, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:40>
		__this->___frameCount = (-1);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:54>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:57>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_0 = ___0_curve;
		__this->___curve = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___curve), (void*)L_0);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:58>
		float L_1 = ___1_zeroValue;
		__this->___m_ZeroValue = L_1;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:59>
		bool L_2 = ___2_loop;
		__this->___m_Loop = L_2;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:60>
		float L_3;
		L_3 = Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline((&___3_bounds), NULL);
		__this->___m_Range = L_3;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:61>
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_4 = (SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C*)SZArrayNew(SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C_il2cpp_TypeInfo_var, (uint32_t)((int32_t)128));
		__this->___cachedData = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___cachedData), (void*)L_4);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:62>
		return;
	}
}
// Method Definition Index: 56374
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Spline_Cache_mF98EF50CC2A26EC6716EB5452FD4B88641A7F833 (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, int32_t ___0_frame, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 V_1;
	memset((&V_1), 0, sizeof(V_1));
	Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 V_2;
	memset((&V_2), 0, sizeof(V_2));
	int32_t V_3 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:75>
		int32_t L_0 = ___0_frame;
		int32_t L_1 = __this->___frameCount;
		if ((!(((uint32_t)L_0) == ((uint32_t)L_1))))
		{
			goto IL_000a;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:76>
		return;
	}

IL_000a:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:78>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_2 = __this->___curve;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = AnimationCurve_get_length_m259A67BB0870D3A153F6FEDBB06CB0D24089CD81(L_2, NULL);
		V_0 = L_3;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:80>
		bool L_4 = __this->___m_Loop;
		if (!L_4)
		{
			goto IL_00af;
		}
	}
	{
		int32_t L_5 = V_0;
		if ((((int32_t)L_5) <= ((int32_t)1)))
		{
			goto IL_00af;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:82>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_6 = __this->___m_InternalLoopingCurve;
		if (L_6)
		{
			goto IL_003b;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:83>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_7 = (AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354*)il2cpp_codegen_object_new(AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354_il2cpp_TypeInfo_var);
		AnimationCurve__ctor_m0D976567166F92383307DC8EB8D7082CD34E226F(L_7, NULL);
		__this->___m_InternalLoopingCurve = L_7;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_InternalLoopingCurve), (void*)L_7);
	}

IL_003b:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:85>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_8 = __this->___curve;
		int32_t L_9 = V_0;
		NullCheck(L_8);
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 L_10;
		L_10 = AnimationCurve_get_Item_mD4E73EE674F7A82673F1A9CEB8E5EF86BA47E64B(L_8, ((int32_t)il2cpp_codegen_subtract(L_9, 1)), NULL);
		V_1 = L_10;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:86>
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* L_11 = (&V_1);
		float L_12;
		L_12 = Keyframe_get_time_mB8886F64CBB373936C0C25C4C68397C05779F661(L_11, NULL);
		float L_13 = __this->___m_Range;
		Keyframe_set_time_m98F165193486C0DF1611B562016595B18052A2D6(L_11, ((float)il2cpp_codegen_subtract(L_12, L_13)), NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:87>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_14 = __this->___curve;
		NullCheck(L_14);
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 L_15;
		L_15 = AnimationCurve_get_Item_mD4E73EE674F7A82673F1A9CEB8E5EF86BA47E64B(L_14, 0, NULL);
		V_2 = L_15;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:88>
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0* L_16 = (&V_2);
		float L_17;
		L_17 = Keyframe_get_time_mB8886F64CBB373936C0C25C4C68397C05779F661(L_16, NULL);
		float L_18 = __this->___m_Range;
		Keyframe_set_time_m98F165193486C0DF1611B562016595B18052A2D6(L_16, ((float)il2cpp_codegen_add(L_17, L_18)), NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:89>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_19 = __this->___m_InternalLoopingCurve;
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_20 = __this->___curve;
		NullCheck(L_20);
		KeyframeU5BU5D_t63250A46914A6A07B2A6689850D47D7D19D80BA3* L_21;
		L_21 = AnimationCurve_get_keys_m34452C69464AB459C04BFFEA4F541F06B419AC4E(L_20, NULL);
		NullCheck(L_19);
		AnimationCurve_set_keys_mBE1284B44CDBB1D8381177A3D581A6E71467F95C(L_19, L_21, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:90>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_22 = __this->___m_InternalLoopingCurve;
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 L_23 = V_1;
		NullCheck(L_22);
		int32_t L_24;
		L_24 = AnimationCurve_AddKey_mBB48B2229D76098019DDAA1F2BAAEFE59F47CCCC(L_22, L_23, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:91>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_25 = __this->___m_InternalLoopingCurve;
		Keyframe_tB9C67DCBFE10C0AE9C52CB5C66E944255C9254F0 L_26 = V_2;
		NullCheck(L_25);
		int32_t L_27;
		L_27 = AnimationCurve_AddKey_mBB48B2229D76098019DDAA1F2BAAEFE59F47CCCC(L_25, L_26, NULL);
	}

IL_00af:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:94>
		V_3 = 0;
		goto IL_00ce;
	}

IL_00b3:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:95>
		SingleU5BU5D_t89DEFE97BCEDB5857010E79ECE0F52CF6E93B87C* L_28 = __this->___cachedData;
		int32_t L_29 = V_3;
		int32_t L_30 = V_3;
		int32_t L_31 = V_0;
		float L_32;
		L_32 = Spline_Evaluate_m908B44D36171764274B429D923E47F36BF972516(__this, ((float)il2cpp_codegen_multiply(((float)L_30), (0.0078125f))), L_31, NULL);
		NullCheck(L_28);
		(L_28)->SetAt(static_cast<il2cpp_array_size_t>(L_29), (float)L_32);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:94>
		int32_t L_33 = V_3;
		V_3 = ((int32_t)il2cpp_codegen_add(L_33, 1));
	}

IL_00ce:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:94>
		int32_t L_34 = V_3;
		if ((((int32_t)L_34) < ((int32_t)((int32_t)128))))
		{
			goto IL_00b3;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:97>
		int32_t L_35;
		L_35 = Time_get_renderedFrameCount_m65102648E50AC7B110E619C91D1B67DEA53D7DBE(NULL);
		__this->___frameCount = L_35;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:98>
		return;
	}
}
// Method Definition Index: 56375
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Spline_Evaluate_m908B44D36171764274B429D923E47F36BF972516 (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, float ___0_t, int32_t ___1_length, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:108>
		int32_t L_0 = ___1_length;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:109>
		float L_1 = __this->___m_ZeroValue;
		return L_1;
	}

IL_000a:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:111>
		bool L_2 = __this->___m_Loop;
		if (!L_2)
		{
			goto IL_0016;
		}
	}
	{
		int32_t L_3 = ___1_length;
		if ((!(((uint32_t)L_3) == ((uint32_t)1))))
		{
			goto IL_0023;
		}
	}

IL_0016:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:112>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_4 = __this->___curve;
		float L_5 = ___0_t;
		NullCheck(L_4);
		float L_6;
		L_6 = AnimationCurve_Evaluate_m50B857043DE251A186032ADBCBB4CEF817F4EE3C(L_4, L_5, NULL);
		return L_6;
	}

IL_0023:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:114>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_7 = __this->___m_InternalLoopingCurve;
		float L_8 = ___0_t;
		NullCheck(L_7);
		float L_9;
		L_9 = AnimationCurve_Evaluate_m50B857043DE251A186032ADBCBB4CEF817F4EE3C(L_7, L_8, NULL);
		return L_9;
	}
}
// Method Definition Index: 56376
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Spline_Evaluate_m782264B22BBC1BADE12ED2E0A0C4CD1DE9A79BBC (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, float ___0_t, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:131>
		float L_0 = ___0_t;
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_1 = __this->___curve;
		NullCheck(L_1);
		int32_t L_2;
		L_2 = AnimationCurve_get_length_m259A67BB0870D3A153F6FEDBB06CB0D24089CD81(L_1, NULL);
		float L_3;
		L_3 = Spline_Evaluate_m908B44D36171764274B429D923E47F36BF972516(__this, L_0, L_2, NULL);
		return L_3;
	}
}
// Method Definition Index: 56377
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Spline_GetHashCode_mF43C4207D64C2A5725F5341460012D79371FBFD7 (Spline_tD8C641273FEAA3A7958A261C41D694636049CB21* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:142>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:143>
		AnimationCurve_tCBFFAAD05CEBB35EF8D8631BD99914BE1A6BB354* L_0 = __this->___curve;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = VirtualFuncInvoker0< int32_t >::Invoke(2, L_0);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/Spline.cs:144>
		return ((int32_t)il2cpp_codegen_add(((int32_t)il2cpp_codegen_multiply(((int32_t)17), ((int32_t)23))), L_1));
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 56378
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TargetPool__ctor_m00A7A8FD2FB5F0FD7BF02F424D32A9FB6B365199 (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:10>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:12>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_0 = (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73*)il2cpp_codegen_object_new(List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73_il2cpp_TypeInfo_var);
		List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8(L_0, List_1__ctor_m17F501B5A5C289ECE1B4F3D6EBF05DFA421433F8_RuntimeMethod_var);
		__this->___m_Pool = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Pool), (void*)L_0);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:13>
		int32_t L_1;
		L_1 = TargetPool_Get_m0547F5C89835B0586BE4B46593AD5A388FF434B0(__this, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:14>
		return;
	}
}
// Method Definition Index: 56379
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TargetPool_Get_m0547F5C89835B0586BE4B46593AD5A388FF434B0 (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:18>
		int32_t L_0 = __this->___m_Current;
		int32_t L_1;
		L_1 = TargetPool_Get_m3E7EF2A9C513F78863718A8237F3A0277A33DC1A(__this, L_0, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:19>
		int32_t L_2 = __this->___m_Current;
		__this->___m_Current = ((int32_t)il2cpp_codegen_add(L_2, 1));
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:20>
		return L_1;
	}
}
// Method Definition Index: 56380
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TargetPool_Get_m3E7EF2A9C513F78863718A8237F3A0277A33DC1A (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, int32_t ___0_i, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralADD218EEEC60165FC319ED66CAD59FFB5160614D);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:27>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_0 = __this->___m_Pool;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_inline(L_0, List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var);
		int32_t L_2 = ___0_i;
		if ((((int32_t)L_1) <= ((int32_t)L_2)))
		{
			goto IL_003e;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:29>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_3 = __this->___m_Pool;
		int32_t L_4 = ___0_i;
		NullCheck(L_3);
		int32_t L_5;
		L_5 = List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D(L_3, L_4, List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_RuntimeMethod_var);
		V_0 = L_5;
		goto IL_0059;
	}

IL_001d:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:35>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_6 = __this->___m_Pool;
		String_t* L_7;
		L_7 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&___0_i), NULL);
		String_t* L_8;
		L_8 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralADD218EEEC60165FC319ED66CAD59FFB5160614D, L_7, NULL);
		int32_t L_9;
		L_9 = Shader_PropertyToID_mE98523D50F5656CAE89B30695C458253EB8956CA(L_8, NULL);
		NullCheck(L_6);
		List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_inline(L_6, L_9, List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_RuntimeMethod_var);
	}

IL_003e:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:34>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_10 = __this->___m_Pool;
		NullCheck(L_10);
		int32_t L_11;
		L_11 = List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_inline(L_10, List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_RuntimeMethod_var);
		int32_t L_12 = ___0_i;
		if ((((int32_t)L_11) <= ((int32_t)L_12)))
		{
			goto IL_001d;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:37>
		List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* L_13 = __this->___m_Pool;
		int32_t L_14 = ___0_i;
		NullCheck(L_13);
		int32_t L_15;
		L_15 = List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D(L_13, L_14, List_1_get_Item_mD99081BEFA1AB3526715F489192B0F7F596C183D_RuntimeMethod_var);
		V_0 = L_15;
	}

IL_0059:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:40>
		int32_t L_16 = V_0;
		return L_16;
	}
}
// Method Definition Index: 56381
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TargetPool_Reset_m2D79A232CECE4951254E2EDA0529F0C877B97BB3 (TargetPool_t88FB94A9290BBADD9DDE613ED91F1FE23203C06C* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:45>
		__this->___m_Current = 0;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TargetPool.cs:46>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 56382
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureFormatUtilities__cctor_m38CEE5DD353AAD05D0AC95A00B923120A2706EC8 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_mAEC79FC89BB27663084F0D041157806C2B4858B4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	bool V_2 = false;
	RuntimeObject* V_3 = NULL;
	RuntimeObject* V_4 = NULL;
	bool V_5 = false;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:20>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:21>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:22>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:23>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:24>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:25>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:26>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:27>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:28>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:29>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:30>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:31>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:32>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:33>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:34>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:35>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:36>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:37>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:38>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:39>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:40>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:41>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:42>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:43>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:44>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:45>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:46>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:47>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:48>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:49>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:50>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:51>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:52>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:53>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:54>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:55>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:56>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:57>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:58>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:59>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:60>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:61>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:62>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:63>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:64>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:65>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:66>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:67>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:68>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:69>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:70>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:71>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:72>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:73>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:74>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:75>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:76>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:77>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:78>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:79>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:80>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:81>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:82>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:83>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:84>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:85>
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_0 = (Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232*)il2cpp_codegen_object_new(Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_mAEC79FC89BB27663084F0D041157806C2B4858B4(L_0, Dictionary_2__ctor_mAEC79FC89BB27663084F0D041157806C2B4858B4_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_1 = L_0;
		NullCheck(L_1);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_1, 1, 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_2 = L_1;
		NullCheck(L_2);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_2, 2, 5, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_3 = L_2;
		NullCheck(L_3);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_3, 3, 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_4 = L_3;
		NullCheck(L_4);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_4, 4, 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_5 = L_4;
		NullCheck(L_5);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_5, 5, 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_6 = L_5;
		NullCheck(L_6);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_6, 7, 4, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_7 = L_6;
		NullCheck(L_7);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_7, ((int32_t)9), ((int32_t)15), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_8 = L_7;
		NullCheck(L_8);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_8, ((int32_t)10), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_9 = L_8;
		NullCheck(L_9);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_9, ((int32_t)12), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_10 = L_9;
		NullCheck(L_10);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_10, ((int32_t)13), 5, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_11 = L_10;
		NullCheck(L_11);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_11, ((int32_t)14), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_12 = L_11;
		NullCheck(L_12);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_12, ((int32_t)15), ((int32_t)15), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_13 = L_12;
		NullCheck(L_13);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_13, ((int32_t)16), ((int32_t)13), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_14 = L_13;
		NullCheck(L_14);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_14, ((int32_t)17), 2, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_15 = L_14;
		NullCheck(L_15);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_15, ((int32_t)18), ((int32_t)14), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_16 = L_15;
		NullCheck(L_16);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_16, ((int32_t)19), ((int32_t)12), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_17 = L_16;
		NullCheck(L_17);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_17, ((int32_t)20), ((int32_t)11), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_18 = L_17;
		NullCheck(L_18);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_18, ((int32_t)22), 2, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_19 = L_18;
		NullCheck(L_19);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_19, ((int32_t)26), ((int32_t)16), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_20 = L_19;
		NullCheck(L_20);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_20, ((int32_t)27), ((int32_t)13), Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_21 = L_20;
		NullCheck(L_21);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_21, ((int32_t)24), 2, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_22 = L_21;
		NullCheck(L_22);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_22, ((int32_t)25), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_23 = L_22;
		NullCheck(L_23);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_23, ((int32_t)28), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_24 = L_23;
		NullCheck(L_24);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_24, ((int32_t)29), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_25 = L_24;
		NullCheck(L_25);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_25, ((int32_t)30), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_26 = L_25;
		NullCheck(L_26);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_26, ((int32_t)31), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_27 = L_26;
		NullCheck(L_27);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_27, ((int32_t)32), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_28 = L_27;
		NullCheck(L_28);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_28, ((int32_t)33), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_29 = L_28;
		NullCheck(L_29);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_29, ((int32_t)34), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_30 = L_29;
		NullCheck(L_30);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_30, ((int32_t)45), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_31 = L_30;
		NullCheck(L_31);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_31, ((int32_t)46), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_32 = L_31;
		NullCheck(L_32);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_32, ((int32_t)47), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_33 = L_32;
		NullCheck(L_33);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_33, ((int32_t)48), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_34 = L_33;
		NullCheck(L_34);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_34, ((int32_t)49), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_35 = L_34;
		NullCheck(L_35);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_35, ((int32_t)50), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_36 = L_35;
		NullCheck(L_36);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_36, ((int32_t)51), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_37 = L_36;
		NullCheck(L_37);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_37, ((int32_t)52), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_38 = L_37;
		NullCheck(L_38);
		Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B(L_38, ((int32_t)53), 0, Dictionary_2_Add_mB42BBFF37C46EBCEAD373E5A37B9B75AB942084B_RuntimeMethod_var);
		((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_FormatAliasMap = L_38;
		Il2CppCodeGenWriteBarrier((void**)(&((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_FormatAliasMap), (void*)L_38);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:91>
		Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_39 = (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682*)il2cpp_codegen_object_new(Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024(L_39, Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_RuntimeMethod_var);
		((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedRenderTextureFormats = L_39;
		Il2CppCodeGenWriteBarrier((void**)(&((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedRenderTextureFormats), (void*)L_39);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:92>
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_40 = { reinterpret_cast<intptr_t> (RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_41;
		L_41 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_40, NULL);
		il2cpp_codegen_runtime_class_init_inline(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var);
		RuntimeArray* L_42;
		L_42 = Enum_GetValues_m803B9D68C367FAABC5AFB6B5B52775C8A573CEF9(L_41, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:94>
		NullCheck(L_42);
		RuntimeObject* L_43;
		L_43 = Array_GetEnumerator_mDB7E2AF23F2BDC715D429C71CA3B8D0151F0DC1E(L_42, NULL);
		V_0 = L_43;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_01c2:
			{
				{
					RuntimeObject* L_44 = V_0;
					V_3 = ((RuntimeObject*)IsInst((RuntimeObject*)L_44, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var));
					RuntimeObject* L_45 = V_3;
					if (!L_45)
					{
						goto IL_01d2;
					}
				}
				{
					RuntimeObject* L_46 = V_3;
					NullCheck(L_46);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_46);
				}

IL_01d2:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_01b8_1;
			}

IL_0183_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:94>
				RuntimeObject* L_47 = V_0;
				NullCheck(L_47);
				RuntimeObject* L_48;
				L_48 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_47);
				V_1 = L_48;
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:96>
				RuntimeObject* L_49 = V_1;
				if ((((int32_t)((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_49, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var))))) < ((int32_t)0)))
				{
					goto IL_01b8_1;
				}
			}
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:99>
				RuntimeObject* L_50 = V_1;
				bool L_51;
				L_51 = TextureFormatUtilities_IsObsolete_m6196F51252D89FDE496B58689F62279DD86D23BC(L_50, NULL);
				if (L_51)
				{
					goto IL_01b8_1;
				}
			}
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:102>
				RuntimeObject* L_52 = V_1;
				bool L_53;
				L_53 = SystemInfo_SupportsRenderTextureFormat_mCCC3C69578A2C5B7367F73999E6938C315A98201(((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_52, RenderTextureFormat_tB6F1ED5040395B46880CE00312D2FDDBF9EEB40F_il2cpp_TypeInfo_var)))), NULL);
				V_2 = L_53;
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:103>
				Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_54 = ((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedRenderTextureFormats;
				RuntimeObject* L_55 = V_1;
				bool L_56 = V_2;
				NullCheck(L_54);
				Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48(L_54, ((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_55, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)))), L_56, Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_RuntimeMethod_var);
			}

IL_01b8_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:94>
				RuntimeObject* L_57 = V_0;
				NullCheck(L_57);
				bool L_58;
				L_58 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_57);
				if (L_58)
				{
					goto IL_0183_1;
				}
			}
			{
				goto IL_01d3;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_01d3:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:109>
		Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_59 = (Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682*)il2cpp_codegen_object_new(Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024(L_59, Dictionary_2__ctor_m1AE7A14FE6CF65276434BAC094AE2F7269D06024_RuntimeMethod_var);
		((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedTextureFormats = L_59;
		Il2CppCodeGenWriteBarrier((void**)(&((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedTextureFormats), (void*)L_59);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:110>
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_60 = { reinterpret_cast<intptr_t> (TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_61;
		L_61 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_60, NULL);
		il2cpp_codegen_runtime_class_init_inline(Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_il2cpp_TypeInfo_var);
		RuntimeArray* L_62;
		L_62 = Enum_GetValues_m803B9D68C367FAABC5AFB6B5B52775C8A573CEF9(L_61, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:112>
		NullCheck(L_62);
		RuntimeObject* L_63;
		L_63 = Array_GetEnumerator_mDB7E2AF23F2BDC715D429C71CA3B8D0151F0DC1E(L_62, NULL);
		V_0 = L_63;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_023a:
			{
				{
					RuntimeObject* L_64 = V_0;
					V_3 = ((RuntimeObject*)IsInst((RuntimeObject*)L_64, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var));
					RuntimeObject* L_65 = V_3;
					if (!L_65)
					{
						goto IL_024a;
					}
				}
				{
					RuntimeObject* L_66 = V_3;
					NullCheck(L_66);
					InterfaceActionInvoker0::Invoke(0, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_66);
				}

IL_024a:
				{
					return;
				}
			}
		});
		try
		{
			{
				goto IL_0230_1;
			}

IL_01f4_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:112>
				RuntimeObject* L_67 = V_0;
				NullCheck(L_67);
				RuntimeObject* L_68;
				L_68 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_67);
				V_4 = L_68;
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:114>
				RuntimeObject* L_69 = V_4;
				if ((((int32_t)((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_69, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var))))) < ((int32_t)0)))
				{
					goto IL_0230_1;
				}
			}
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:117>
				RuntimeObject* L_70 = V_4;
				bool L_71;
				L_71 = TextureFormatUtilities_IsObsolete_m6196F51252D89FDE496B58689F62279DD86D23BC(L_70, NULL);
				if (L_71)
				{
					goto IL_0230_1;
				}
			}
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:120>
				RuntimeObject* L_72 = V_4;
				bool L_73;
				L_73 = SystemInfo_SupportsTextureFormat_m833B0ABED13B5B8D0D4BCF082F3EFA51A3B5C860(((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_72, TextureFormat_t87A73E4A3850D3410DC211676FC14B94226C1C1D_il2cpp_TypeInfo_var)))), NULL);
				V_5 = L_73;
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:121>
				Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_74 = ((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedTextureFormats;
				RuntimeObject* L_75 = V_4;
				bool L_76 = V_5;
				NullCheck(L_74);
				Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48(L_74, ((*(int32_t*)((int32_t*)(int32_t*)UnBox(L_75, Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C_il2cpp_TypeInfo_var)))), L_76, Dictionary_2_set_Item_m266E81996A1E6BFCB7C91E78EC0DB1253655AD48_RuntimeMethod_var);
			}

IL_0230_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:112>
				RuntimeObject* L_77 = V_0;
				NullCheck(L_77);
				bool L_78;
				L_78 = InterfaceFuncInvoker0< bool >::Invoke(0, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_77);
				if (L_78)
				{
					goto IL_01f4_1;
				}
			}
			{
				goto IL_024b;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_024b:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:124>
		return;
	}
}
// Method Definition Index: 56383
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TextureFormatUtilities_IsObsolete_m6196F51252D89FDE496B58689F62279DD86D23BC (RuntimeObject* ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A_0_0_0_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6* V_0 = NULL;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:128>
		RuntimeObject* L_0 = ___0_value;
		NullCheck(L_0);
		Type_t* L_1;
		L_1 = Object_GetType_mE10A8FC1E57F3DF29972CCBC026C2DC3942263B3(L_0, NULL);
		RuntimeObject* L_2 = ___0_value;
		NullCheck(L_2);
		String_t* L_3;
		L_3 = VirtualFuncInvoker0< String_t* >::Invoke(3, L_2);
		NullCheck(L_1);
		FieldInfo_t* L_4;
		L_4 = Type_GetField_m0BF55B1A27A1B6AB6D3477E7F9E1CF2A3451E1E0(L_1, L_3, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:129>
		RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B L_5 = { reinterpret_cast<intptr_t> (ObsoleteAttribute_tF4885B281E932B8B87A5B9AA1C24D46DEEA8FD8A_0_0_0_var) };
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_6;
		L_6 = Type_GetTypeFromHandle_m6062B81682F79A4D6DF2640692EE6D9987858C57(L_5, NULL);
		NullCheck(L_4);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_7;
		L_7 = VirtualFuncInvoker2< ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*, Type_t*, bool >::Invoke(14, L_4, L_6, (bool)0);
		V_0 = ((ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6*)Castclass((RuntimeObject*)L_7, ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6_il2cpp_TypeInfo_var));
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:130>
		ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6* L_8 = V_0;
		if (!L_8)
		{
			goto IL_0030;
		}
	}
	{
		ObsoleteAttributeU5BU5D_tCEEAC50D326AEA0B2122C2F48969D6B97C6ABEA6* L_9 = V_0;
		NullCheck(L_9);
		return (bool)((!(((uint32_t)(((RuntimeArray*)L_9)->max_length)) <= ((uint32_t)0)))? 1 : 0);
	}

IL_0030:
	{
		return (bool)0;
	}
}
// Method Definition Index: 56384
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A (Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___0_texture, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_m7D68C17F2EFEADA12A803841CFD7C5A8D3ED159B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	int32_t V_1 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:142>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_0 = ___0_texture;
		if (!((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)IsInstClass((RuntimeObject*)L_0, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)))
		{
			goto IL_0014;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:143>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_1 = ___0_texture;
		NullCheck(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)IsInstClass((RuntimeObject*)L_1, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)));
		int32_t L_2;
		L_2 = RenderTexture_get_format_m58556ABB91A1FADA8044BEEA2E8C55280768CF35(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)IsInstClass((RuntimeObject*)L_1, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)), NULL);
		return L_2;
	}

IL_0014:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:145>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_3 = ___0_texture;
		if (!((Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)IsInstSealed((RuntimeObject*)L_3, Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var)))
		{
			goto IL_0044;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:147>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_4 = ___0_texture;
		NullCheck(((Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)CastclassSealed((RuntimeObject*)L_4, Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var)));
		int32_t L_5;
		L_5 = Texture2D_get_format_mE39DD922F83CA1097383309278BB6F20636A7D9D(((Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4*)CastclassSealed((RuntimeObject*)L_4, Texture2D_tE6505BC111DD8A424A9DBE8E05D7D09E11FFFCF4_il2cpp_TypeInfo_var)), NULL);
		V_0 = L_5;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:150>
		il2cpp_codegen_runtime_class_init_inline(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		Dictionary_2_tB5BCF24B77495C576D9ECE31C959C276F5128232* L_6 = ((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_FormatAliasMap;
		int32_t L_7 = V_0;
		NullCheck(L_6);
		bool L_8;
		L_8 = Dictionary_2_TryGetValue_m7D68C17F2EFEADA12A803841CFD7C5A8D3ED159B(L_6, L_7, (&V_1), Dictionary_2_TryGetValue_m7D68C17F2EFEADA12A803841CFD7C5A8D3ED159B_RuntimeMethod_var);
		if (L_8)
		{
			goto IL_0042;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:151>
		NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A* L_9 = (NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A*)il2cpp_codegen_object_new(((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&NotSupportedException_t1429765983D409BD2986508963C98D214E4EBF4A_il2cpp_TypeInfo_var)));
		NotSupportedException__ctor_mE174750CF0247BBB47544FFD71D66BB89630945B(L_9, ((String_t*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&_stringLiteral96AFB5CB3B476CE64056EF8716AEA14B385714ED)), NULL);
		IL2CPP_RAISE_MANAGED_EXCEPTION(L_9, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A_RuntimeMethod_var)));
	}

IL_0042:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:153>
		int32_t L_10 = V_1;
		return L_10;
	}

IL_0044:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:156>
		return (int32_t)(7);
	}
}
// Method Definition Index: 56385
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TextureFormatUtilities_IsSupported_mB19AE8F3F583E6286877F54F90E0A90676EBA173 (int32_t ___0_format, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:162>
		il2cpp_codegen_runtime_class_init_inline(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_0 = ((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedRenderTextureFormats;
		int32_t L_1 = ___0_format;
		NullCheck(L_0);
		bool L_2;
		L_2 = Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF(L_0, L_1, (&V_0), Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:163>
		bool L_3 = V_0;
		return L_3;
	}
}
// Method Definition Index: 56386
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TextureFormatUtilities_IsSupported_m36A432AF0FF57C130ACB43EC4FDE96452003CE5B (int32_t ___0_format, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	bool V_0 = false;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:169>
		il2cpp_codegen_runtime_class_init_inline(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		Dictionary_2_t01224C8DBCCFE276E97D2BF52F4D7B10D3642682* L_0 = ((TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_StaticFields*)il2cpp_codegen_static_fields_for(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var))->___s_SupportedTextureFormats;
		int32_t L_1 = ___0_format;
		NullCheck(L_0);
		bool L_2;
		L_2 = Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF(L_0, L_1, (&V_0), Dictionary_2_TryGetValue_mB76129E352C910CA78BB17FF72AA4864B1A385CF_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureFormatUtilities.cs:170>
		bool L_3 = V_0;
		return L_3;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// Method Definition Index: 56387
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* TextureLerper_get_instance_m9FBD47F5E67E2E692847BCD402084526D6AF8F37 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:13>
		TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* L_0 = ((TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_StaticFields*)il2cpp_codegen_static_fields_for(TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var))->___m_Instance;
		if (L_0)
		{
			goto IL_0011;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:14>
		TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* L_1 = (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424*)il2cpp_codegen_object_new(TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var);
		TextureLerper__ctor_m326A48946F64C5733E75607AE62A9D247FC7E67D(L_1, NULL);
		((TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_StaticFields*)il2cpp_codegen_static_fields_for(TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var))->___m_Instance = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_StaticFields*)il2cpp_codegen_static_fields_for(TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var))->___m_Instance), (void*)L_1);
	}

IL_0011:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:16>
		TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* L_2 = ((TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_StaticFields*)il2cpp_codegen_static_fields_for(TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424_il2cpp_TypeInfo_var))->___m_Instance;
		return L_2;
	}
}
// Method Definition Index: 56388
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureLerper__ctor_m326A48946F64C5733E75607AE62A9D247FC7E67D (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:27>
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:29>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_0 = (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*)il2cpp_codegen_object_new(List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B_il2cpp_TypeInfo_var);
		List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49(L_0, List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49_RuntimeMethod_var);
		__this->___m_Recycled = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Recycled), (void*)L_0);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:30>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_1 = (List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B*)il2cpp_codegen_object_new(List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B_il2cpp_TypeInfo_var);
		List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49(L_1, List_1__ctor_mD0891E1C91B860AD22385685DAAFF9988CA4CB49_RuntimeMethod_var);
		__this->___m_Actives = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Actives), (void*)L_1);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:31>
		return;
	}
}
// Method Definition Index: 56389
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureLerper_BeginFrame_mF2957CC10D96B1A568D284E0A7DA7F4E0CFE0558 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* ___0_context, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:35>
		PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* L_0 = ___0_context;
		NullCheck(L_0);
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_1;
		L_1 = PostProcessRenderContext_get_command_m028BE33B6194640A1DE901A6F935658034A3E2CD_inline(L_0, NULL);
		__this->___m_Command = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Command), (void*)L_1);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:36>
		PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* L_2 = ___0_context;
		NullCheck(L_2);
		PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* L_3;
		L_3 = PostProcessRenderContext_get_propertySheets_m60E7825143611FEC183803150D8F7C2785514D79_inline(L_2, NULL);
		__this->___m_PropertySheets = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_PropertySheets), (void*)L_3);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:37>
		PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* L_4 = ___0_context;
		NullCheck(L_4);
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_5;
		L_5 = PostProcessRenderContext_get_resources_m89879DF69E4B910F9EE3008AB8DC60B732ABF02A_inline(L_4, NULL);
		__this->___m_Resources = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___m_Resources), (void*)L_5);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:38>
		return;
	}
}
// Method Definition Index: 56390
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureLerper_EndFrame_m827E01313363D563B9D08D245272A162D1872745 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_AddRange_mEC23918422A46D768ED6755FC601960B173E6D8B_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:43>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_0 = __this->___m_Recycled;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_inline(L_0, List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var);
		if ((((int32_t)L_1) <= ((int32_t)0)))
		{
			goto IL_004c;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:45>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_2 = __this->___m_Recycled;
		NullCheck(L_2);
		Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 L_3;
		L_3 = List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A(L_2, List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var);
		V_0 = L_3;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0033:
			{
				Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4((&V_0), Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_0028_1;
			}

IL_001c_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:45>
				RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_4;
				L_4 = Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_inline((&V_0), Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var);
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:46>
				il2cpp_codegen_runtime_class_init_inline(RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
				RuntimeUtilities_Destroy_m88860DDA45529FA1193643863F052D709087B493(L_4, NULL);
			}

IL_0028_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:45>
				bool L_5;
				L_5 = Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5((&V_0), Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var);
				if (L_5)
				{
					goto IL_001c_1;
				}
			}
			{
				goto IL_0041;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0041:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:48>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_6 = __this->___m_Recycled;
		NullCheck(L_6);
		List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_inline(L_6, List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
	}

IL_004c:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:53>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_7 = __this->___m_Actives;
		NullCheck(L_7);
		int32_t L_8;
		L_8 = List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_inline(L_7, List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var);
		if ((((int32_t)L_8) <= ((int32_t)0)))
		{
			goto IL_0076;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:55>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_9 = __this->___m_Recycled;
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_10 = __this->___m_Actives;
		NullCheck(L_9);
		List_1_AddRange_mEC23918422A46D768ED6755FC601960B173E6D8B(L_9, L_10, List_1_AddRange_mEC23918422A46D768ED6755FC601960B173E6D8B_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:57>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_11 = __this->___m_Actives;
		NullCheck(L_11);
		List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_inline(L_11, List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
	}

IL_0076:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:59>
		return;
	}
}
// Method Definition Index: 56391
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, int32_t ___0_format, int32_t ___1_w, int32_t ___2_h, int32_t ___3_d, bool ___4_enableRandomWrite, bool ___5_force3D, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m9F1BA8EEA06819CC244EC23459573BB8E8D32C78_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_RemoveAt_m164E41C28FA43F25F51C417135CF61976ECF3B69_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_mFD8C82612D4654B884F2F68C8C1D93B56A1E0081_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* V_0 = NULL;
	int32_t V_1 = 0;
	int32_t V_2 = 0;
	RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* V_3 = NULL;
	int32_t V_4 = 0;
	int32_t G_B15_0 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:63>
		V_0 = (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)NULL;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:64>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_0 = __this->___m_Recycled;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_inline(L_0, List_1_get_Count_mDF53F5D42E96D7FDE797826D3902F54BB8B80221_RuntimeMethod_var);
		V_2 = L_1;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:66>
		V_1 = 0;
		goto IL_0063;
	}

IL_0012:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:68>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_2 = __this->___m_Recycled;
		int32_t L_3 = V_1;
		NullCheck(L_2);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_4;
		L_4 = List_1_get_Item_mFD8C82612D4654B884F2F68C8C1D93B56A1E0081(L_2, L_3, List_1_get_Item_mFD8C82612D4654B884F2F68C8C1D93B56A1E0081_RuntimeMethod_var);
		V_3 = L_4;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:69>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_5 = V_3;
		NullCheck(L_5);
		int32_t L_6;
		L_6 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_5);
		int32_t L_7 = ___1_w;
		if ((!(((uint32_t)L_6) == ((uint32_t)L_7))))
		{
			goto IL_005f;
		}
	}
	{
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_8 = V_3;
		NullCheck(L_8);
		int32_t L_9;
		L_9 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_8);
		int32_t L_10 = ___2_h;
		if ((!(((uint32_t)L_9) == ((uint32_t)L_10))))
		{
			goto IL_005f;
		}
	}
	{
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_11 = V_3;
		NullCheck(L_11);
		int32_t L_12;
		L_12 = RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF(L_11, NULL);
		int32_t L_13 = ___3_d;
		if ((!(((uint32_t)L_12) == ((uint32_t)L_13))))
		{
			goto IL_005f;
		}
	}
	{
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_14 = V_3;
		NullCheck(L_14);
		int32_t L_15;
		L_15 = RenderTexture_get_format_m58556ABB91A1FADA8044BEEA2E8C55280768CF35(L_14, NULL);
		int32_t L_16 = ___0_format;
		if ((!(((uint32_t)L_15) == ((uint32_t)L_16))))
		{
			goto IL_005f;
		}
	}
	{
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_17 = V_3;
		NullCheck(L_17);
		bool L_18;
		L_18 = RenderTexture_get_enableRandomWrite_m64B49A97B19ACC25765E27F8F8A39F625D4D20CD(L_17, NULL);
		bool L_19 = ___4_enableRandomWrite;
		if ((!(((uint32_t)L_18) == ((uint32_t)L_19))))
		{
			goto IL_005f;
		}
	}
	{
		bool L_20 = ___5_force3D;
		if (!L_20)
		{
			goto IL_005b;
		}
	}
	{
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_21 = V_3;
		NullCheck(L_21);
		int32_t L_22;
		L_22 = VirtualFuncInvoker0< int32_t >::Invoke(9, L_21);
		if ((!(((uint32_t)L_22) == ((uint32_t)3))))
		{
			goto IL_005f;
		}
	}

IL_005b:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:71>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_23 = V_3;
		V_0 = L_23;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:72>
		goto IL_0067;
	}

IL_005f:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:66>
		int32_t L_24 = V_1;
		V_1 = ((int32_t)il2cpp_codegen_add(L_24, 1));
	}

IL_0063:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:66>
		int32_t L_25 = V_1;
		int32_t L_26 = V_2;
		if ((((int32_t)L_25) < ((int32_t)L_26)))
		{
			goto IL_0012;
		}
	}

IL_0067:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:76>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_27 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_28;
		L_28 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_27, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_28)
		{
			goto IL_00c0;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:78>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:79>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:80>
		int32_t L_29 = ___3_d;
		bool L_30 = ___5_force3D;
		if (((int32_t)(((((int32_t)L_29) > ((int32_t)1))? 1 : 0)|(int32_t)L_30)))
		{
			goto IL_007d;
		}
	}
	{
		G_B15_0 = 2;
		goto IL_007e;
	}

IL_007d:
	{
		G_B15_0 = 3;
	}

IL_007e:
	{
		V_4 = G_B15_0;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:82>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:83>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:84>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:85>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:86>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:87>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:88>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:89>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:90>
		int32_t L_31 = ___1_w;
		int32_t L_32 = ___2_h;
		int32_t L_33 = ___0_format;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_34 = (RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)il2cpp_codegen_object_new(RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var);
		RenderTexture__ctor_m53215A8EDDE262932758186108347685F6A512C4(L_34, L_31, L_32, 0, L_33, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_35 = L_34;
		int32_t L_36 = V_4;
		NullCheck(L_35);
		VirtualActionInvoker1< int32_t >::Invoke(10, L_35, L_36);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_37 = L_35;
		NullCheck(L_37);
		Texture_set_filterMode_mE423E58C0C16D059EA62BA87AD70F44AEA50CCC9(L_37, 1, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_38 = L_37;
		NullCheck(L_38);
		Texture_set_wrapMode_m1F74A690E3883EC9C5C371D502D09642F15D0F7E(L_38, 1, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_39 = L_38;
		NullCheck(L_39);
		Texture_set_anisoLevel_m768759DE9D6BE15FCE995F5FC468980B904B9D1F(L_39, 0, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_40 = L_39;
		int32_t L_41 = ___3_d;
		NullCheck(L_40);
		RenderTexture_set_volumeDepth_mD9B1E6BA4BE6B1741427B34A23B9D48BA9493633(L_40, L_41, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_42 = L_40;
		bool L_43 = ___4_enableRandomWrite;
		NullCheck(L_42);
		RenderTexture_set_enableRandomWrite_m1F1B5E033802C193449803411560DB4D5D4AEEAB(L_42, L_43, NULL);
		V_0 = L_42;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:91>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_44 = V_0;
		NullCheck(L_44);
		bool L_45;
		L_45 = RenderTexture_Create_mA6E4D3CCC84AC3F68E85AA0D6609E1692C672AD2(L_44, NULL);
		goto IL_00cc;
	}

IL_00c0:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:93>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_46 = __this->___m_Recycled;
		int32_t L_47 = V_1;
		NullCheck(L_46);
		List_1_RemoveAt_m164E41C28FA43F25F51C417135CF61976ECF3B69(L_46, L_47, List_1_RemoveAt_m164E41C28FA43F25F51C417135CF61976ECF3B69_RuntimeMethod_var);
	}

IL_00cc:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:95>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_48 = __this->___m_Actives;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_49 = V_0;
		NullCheck(L_48);
		List_1_Add_m9F1BA8EEA06819CC244EC23459573BB8E8D32C78_inline(L_48, L_49, List_1_Add_m9F1BA8EEA06819CC244EC23459573BB8E8D32C78_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:96>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_50 = V_0;
		return L_50;
	}
}
// Method Definition Index: 56392
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* TextureLerper_Lerp_m675AC3EF5BB02D904E0988F874D1F6F9AF9FB257 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___0_from, Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___1_to, float ___2_t, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4F889A7069FB348E59778EC4C275B6A09FE34752);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral5ED45B85ADA1AFA9C8962A3063BC0DB7FA04521E);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6067E93B7ED6BC9634C2207045961FBB1126B92A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD754031EB09A4061A82F00A1E77A9972C831A1FB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF51190EEE90545F9CD168B86B0A73EF3C85E3A2C);
		s_Il2CppMethodInitialized = true;
	}
	RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* V_0 = NULL;
	int32_t V_1 = 0;
	PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* V_2 = NULL;
	int32_t V_3 = 0;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* V_4 = NULL;
	int32_t V_5 = 0;
	uint32_t V_6 = 0;
	uint32_t V_7 = 0;
	uint32_t V_8 = 0;
	int32_t V_9 = 0;
	int32_t V_10 = 0;
	Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 V_11;
	memset((&V_11), 0, sizeof(V_11));
	int32_t G_B11_0 = 0;
	int32_t G_B15_0 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:107>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_0 = ___0_from;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_1 = ___1_to;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_2;
		L_2 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, L_1, NULL);
		if (!L_2)
		{
			goto IL_000b;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:108>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_3 = ___0_from;
		return L_3;
	}

IL_000b:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:111>
		float L_4 = ___2_t;
		if ((!(((float)L_4) <= ((float)(0.0f)))))
		{
			goto IL_0015;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:111>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_5 = ___0_from;
		return L_5;
	}

IL_0015:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:112>
		float L_6 = ___2_t;
		if ((!(((float)L_6) >= ((float)(1.0f)))))
		{
			goto IL_001f;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:112>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_7 = ___1_to;
		return L_7;
	}

IL_001f:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:114>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:115>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_8 = ___0_from;
		if (((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)IsInstSealed((RuntimeObject*)L_8, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)))
		{
			goto IL_0042;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_9 = ___0_from;
		if (!((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)IsInstClass((RuntimeObject*)L_9, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)))
		{
			goto IL_003f;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_10 = ___0_from;
		NullCheck(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_10, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)));
		int32_t L_11;
		L_11 = RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_10, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)), NULL);
		G_B11_0 = ((((int32_t)L_11) > ((int32_t)1))? 1 : 0);
		goto IL_0043;
	}

IL_003f:
	{
		G_B11_0 = 0;
		goto IL_0043;
	}

IL_0042:
	{
		G_B11_0 = 1;
	}

IL_0043:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:120>
		if (!G_B11_0)
		{
			goto IL_016e;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:122>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_12 = ___0_from;
		if (((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)IsInstSealed((RuntimeObject*)L_12, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)))
		{
			goto IL_005d;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_13 = ___0_from;
		NullCheck(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_13, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)));
		int32_t L_14;
		L_14 = RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_13, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)), NULL);
		G_B15_0 = L_14;
		goto IL_0068;
	}

IL_005d:
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_15 = ___0_from;
		NullCheck(((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)CastclassSealed((RuntimeObject*)L_15, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)));
		int32_t L_16;
		L_16 = Texture3D_get_depth_m08A40112C90FB3346068195C2B83FEB544313169(((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)CastclassSealed((RuntimeObject*)L_15, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)), NULL);
		G_B15_0 = L_16;
	}

IL_0068:
	{
		V_3 = G_B15_0;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:123>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_17 = ___0_from;
		NullCheck(L_17);
		int32_t L_18;
		L_18 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_17);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_19 = ___0_from;
		NullCheck(L_19);
		int32_t L_20;
		L_20 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_19);
		int32_t L_21;
		L_21 = Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline(L_18, L_20, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:124>
		int32_t L_22 = V_3;
		int32_t L_23;
		L_23 = Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline(L_21, L_22, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:126>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_24 = ___0_from;
		NullCheck(L_24);
		int32_t L_25;
		L_25 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_24);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_26 = ___0_from;
		NullCheck(L_26);
		int32_t L_27;
		L_27 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_26);
		int32_t L_28 = V_3;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_29;
		L_29 = TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120(__this, 2, L_25, L_27, L_28, (bool)1, (bool)1, NULL);
		V_0 = L_29;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:128>
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_30 = __this->___m_Resources;
		NullCheck(L_30);
		ComputeShaders_t8F1C8B34C544DEC3B4D302509211C37989AAC39F* L_31 = L_30->___computeShaders;
		NullCheck(L_31);
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_32 = L_31->___texture3dLerp;
		V_4 = L_32;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:129>
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_33 = V_4;
		NullCheck(L_33);
		int32_t L_34;
		L_34 = ComputeShader_FindKernel_m3BA5C50794FA6AF4C432E55FBBD7CB266532F659(L_33, _stringLiteral4F889A7069FB348E59778EC4C275B6A09FE34752, NULL);
		V_5 = L_34;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:130>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_35 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_36 = V_4;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_37 = ___0_from;
		NullCheck(L_37);
		int32_t L_38;
		L_38 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_37);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_39 = ___0_from;
		NullCheck(L_39);
		int32_t L_40;
		L_40 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_39);
		int32_t L_41 = V_3;
		float L_42 = ___2_t;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_43;
		memset((&L_43), 0, sizeof(L_43));
		Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline((&L_43), ((float)L_38), ((float)L_40), ((float)L_41), L_42, NULL);
		NullCheck(L_35);
		CommandBuffer_SetComputeVectorParam_mCB04E8C59D63D6CDCA0E8EDA362BE1CB7BF49709(L_35, L_36, _stringLiteralD754031EB09A4061A82F00A1E77A9972C831A1FB, L_43, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:131>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_44 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_45 = V_4;
		int32_t L_46 = V_5;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_47 = V_0;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_48;
		L_48 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_47, NULL);
		NullCheck(L_44);
		CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174(L_44, L_45, L_46, _stringLiteral6067E93B7ED6BC9634C2207045961FBB1126B92A, L_48, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:132>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_49 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_50 = V_4;
		int32_t L_51 = V_5;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_52 = ___0_from;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_53;
		L_53 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_52, NULL);
		NullCheck(L_49);
		CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174(L_49, L_50, L_51, _stringLiteralF51190EEE90545F9CD168B86B0A73EF3C85E3A2C, L_53, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:133>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_54 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_55 = V_4;
		int32_t L_56 = V_5;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_57 = ___1_to;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_58;
		L_58 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_57, NULL);
		NullCheck(L_54);
		CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174(L_54, L_55, L_56, _stringLiteral5ED45B85ADA1AFA9C8962A3063BC0DB7FA04521E, L_58, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:136>
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_59 = V_4;
		int32_t L_60 = V_5;
		NullCheck(L_59);
		ComputeShader_GetKernelThreadGroupSizes_m693428494DB1FD3CFC69FCE4E0093A2C4AAE1CBE(L_59, L_60, (&V_6), (&V_7), (&V_8), NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:138>
		int32_t L_61 = L_23;
		uint32_t L_62 = V_6;
		int32_t L_63;
		L_63 = Mathf_CeilToInt_mF2BF9F4261B3431DC20E10A46CFEEED103C48963_inline(((float)(((float)L_61)/((float)((double)(uint32_t)L_62)))), NULL);
		V_9 = L_63;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:139>
		uint32_t L_64 = V_8;
		int32_t L_65;
		L_65 = Mathf_CeilToInt_mF2BF9F4261B3431DC20E10A46CFEEED103C48963_inline(((float)(((float)L_61)/((float)((double)(uint32_t)L_64)))), NULL);
		V_10 = L_65;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:141>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_66 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_67 = V_4;
		int32_t L_68 = V_5;
		int32_t L_69 = V_9;
		int32_t L_70 = V_9;
		int32_t L_71 = V_10;
		NullCheck(L_66);
		CommandBuffer_DispatchCompute_mF9F5605B77F0480FD4B8C3BCAEC2FC59A24E31A2(L_66, L_67, L_68, L_69, L_70, L_71, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:142>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_72 = V_0;
		return L_72;
	}

IL_016e:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:152>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_73 = ___1_to;
		il2cpp_codegen_runtime_class_init_inline(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		int32_t L_74;
		L_74 = TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A(L_73, NULL);
		V_1 = L_74;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:153>
		int32_t L_75 = V_1;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_76 = ___1_to;
		NullCheck(L_76);
		int32_t L_77;
		L_77 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_76);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_78 = ___1_to;
		NullCheck(L_78);
		int32_t L_79;
		L_79 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_78);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_80;
		L_80 = TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120(__this, L_75, L_77, L_79, 1, (bool)0, (bool)0, NULL);
		V_0 = L_80;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:155>
		PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* L_81 = __this->___m_PropertySheets;
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_82 = __this->___m_Resources;
		NullCheck(L_82);
		Shaders_t2934A1A9726776BE88E31A97A67A9BD9ACEED86B* L_83 = L_82->___shaders;
		NullCheck(L_83);
		Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* L_84 = L_83->___texture2dLerp;
		NullCheck(L_81);
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_85;
		L_85 = PropertySheetFactory_Get_mCFDB007DD001F66FCC0EAD7549B63C74857569FC(L_81, L_84, NULL);
		V_2 = L_85;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:156>
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_86 = V_2;
		NullCheck(L_86);
		MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* L_87;
		L_87 = PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline(L_86, NULL);
		il2cpp_codegen_runtime_class_init_inline(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var);
		int32_t L_88 = ((ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_StaticFields*)il2cpp_codegen_static_fields_for(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var))->___To;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_89 = ___1_to;
		NullCheck(L_87);
		MaterialPropertyBlock_SetTexture_m39F531D3F35D6C5B661A7B4F07DD7B8ACC22627F(L_87, L_88, L_89, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:157>
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_90 = V_2;
		NullCheck(L_90);
		MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* L_91;
		L_91 = PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline(L_90, NULL);
		int32_t L_92 = ((ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_StaticFields*)il2cpp_codegen_static_fields_for(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var))->___Interp;
		float L_93 = ___2_t;
		NullCheck(L_91);
		MaterialPropertyBlock_SetFloat_m6BA8DA03FAD1ABA0BD339E0E5157C4DF3C987267(L_91, L_92, L_93, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:159>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_94 = __this->___m_Command;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_95 = ___0_from;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_96;
		L_96 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_95, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_97 = V_0;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_98;
		L_98 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_97, NULL);
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_99 = V_2;
		il2cpp_codegen_initobj((&V_11), sizeof(Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440));
		Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 L_100 = V_11;
		il2cpp_codegen_runtime_class_init_inline(RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		RuntimeUtilities_BlitFullscreenTriangle_m5E84F777CA552E3540C83CDAEB6C2075F8406E16(L_94, L_96, L_98, L_99, 0, (bool)0, L_100, (bool)0, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:161>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_101 = V_0;
		return L_101;
	}
}
// Method Definition Index: 56393
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* TextureLerper_Lerp_mF45DC5D430B5C7B0C0067B55F390910DE27232DA (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* ___0_from, Color_tD001788D726C3A7F1379BEED0260B9591F440C1F ___1_to, float ___2_t, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral3E63EA4D6F8144DD6406580EE9A7B6F874A529E4);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral6067E93B7ED6BC9634C2207045961FBB1126B92A);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9C7F6CFACBA60E7454344DA7A7EB629EB0C4A8F2);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralD754031EB09A4061A82F00A1E77A9972C831A1FB);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF51190EEE90545F9CD168B86B0A73EF3C85E3A2C);
		s_Il2CppMethodInitialized = true;
	}
	RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* V_0 = NULL;
	int32_t V_1 = 0;
	PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* V_2 = NULL;
	int32_t V_3 = 0;
	ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* V_4 = NULL;
	int32_t V_5 = 0;
	int32_t V_6 = 0;
	Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 V_7;
	memset((&V_7), 0, sizeof(V_7));
	int32_t G_B7_0 = 0;
	int32_t G_B11_0 = 0;
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:168>
		float L_0 = ___2_t;
		if ((!(((double)((double)L_0)) < ((double)(1.0000000000000001E-05)))))
		{
			goto IL_000f;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:169>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_1 = ___0_from;
		return L_1;
	}

IL_000f:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:171>
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:172>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_2 = ___0_from;
		if (((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)IsInstSealed((RuntimeObject*)L_2, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)))
		{
			goto IL_0032;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_3 = ___0_from;
		if (!((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)IsInstClass((RuntimeObject*)L_3, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)))
		{
			goto IL_002f;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_4 = ___0_from;
		NullCheck(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_4, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)));
		int32_t L_5;
		L_5 = RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_4, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)), NULL);
		G_B7_0 = ((((int32_t)L_5) > ((int32_t)1))? 1 : 0);
		goto IL_0033;
	}

IL_002f:
	{
		G_B7_0 = 0;
		goto IL_0033;
	}

IL_0032:
	{
		G_B7_0 = 1;
	}

IL_0033:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:177>
		if (!G_B7_0)
		{
			goto IL_0157;
		}
	}
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:179>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_6 = ___0_from;
		if (((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)IsInstSealed((RuntimeObject*)L_6, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)))
		{
			goto IL_004d;
		}
	}
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_7 = ___0_from;
		NullCheck(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_7, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)));
		int32_t L_8;
		L_8 = RenderTexture_get_volumeDepth_m049340EE670E9632FC824B640A5570B5D3FCFEBF(((RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27*)CastclassClass((RuntimeObject*)L_7, RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27_il2cpp_TypeInfo_var)), NULL);
		G_B11_0 = L_8;
		goto IL_0058;
	}

IL_004d:
	{
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_9 = ___0_from;
		NullCheck(((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)CastclassSealed((RuntimeObject*)L_9, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)));
		int32_t L_10;
		L_10 = Texture3D_get_depth_m08A40112C90FB3346068195C2B83FEB544313169(((Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1*)CastclassSealed((RuntimeObject*)L_9, Texture3D_tDC30A0F19B6055086859D1ABC098D6E6762000E1_il2cpp_TypeInfo_var)), NULL);
		G_B11_0 = L_10;
	}

IL_0058:
	{
		V_3 = G_B11_0;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:180>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_11 = ___0_from;
		NullCheck(L_11);
		int32_t L_12;
		L_12 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_11);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_13 = ___0_from;
		NullCheck(L_13);
		int32_t L_14;
		L_14 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_13);
		int32_t L_15;
		L_15 = Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline(L_12, L_14, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:181>
		int32_t L_16 = V_3;
		int32_t L_17;
		L_17 = Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline(L_15, L_16, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:183>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_18 = ___0_from;
		NullCheck(L_18);
		int32_t L_19;
		L_19 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_18);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_20 = ___0_from;
		NullCheck(L_20);
		int32_t L_21;
		L_21 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_20);
		int32_t L_22 = V_3;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_23;
		L_23 = TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120(__this, 2, L_19, L_21, L_22, (bool)1, (bool)1, NULL);
		V_0 = L_23;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:185>
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_24 = __this->___m_Resources;
		NullCheck(L_24);
		ComputeShaders_t8F1C8B34C544DEC3B4D302509211C37989AAC39F* L_25 = L_24->___computeShaders;
		NullCheck(L_25);
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_26 = L_25->___texture3dLerp;
		V_4 = L_26;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:186>
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_27 = V_4;
		NullCheck(L_27);
		int32_t L_28;
		L_28 = ComputeShader_FindKernel_m3BA5C50794FA6AF4C432E55FBBD7CB266532F659(L_27, _stringLiteral9C7F6CFACBA60E7454344DA7A7EB629EB0C4A8F2, NULL);
		V_5 = L_28;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:187>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_29 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_30 = V_4;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_31 = ___0_from;
		NullCheck(L_31);
		int32_t L_32;
		L_32 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_31);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_33 = ___0_from;
		NullCheck(L_33);
		int32_t L_34;
		L_34 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_33);
		int32_t L_35 = V_3;
		float L_36 = ___2_t;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_37;
		memset((&L_37), 0, sizeof(L_37));
		Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline((&L_37), ((float)L_32), ((float)L_34), ((float)L_35), L_36, NULL);
		NullCheck(L_29);
		CommandBuffer_SetComputeVectorParam_mCB04E8C59D63D6CDCA0E8EDA362BE1CB7BF49709(L_29, L_30, _stringLiteralD754031EB09A4061A82F00A1E77A9972C831A1FB, L_37, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:188>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_38 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_39 = V_4;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_40 = ___1_to;
		float L_41 = L_40.___r;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_42 = ___1_to;
		float L_43 = L_42.___g;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_44 = ___1_to;
		float L_45 = L_44.___b;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_46 = ___1_to;
		float L_47 = L_46.___a;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_48;
		memset((&L_48), 0, sizeof(L_48));
		Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline((&L_48), L_41, L_43, L_45, L_47, NULL);
		NullCheck(L_38);
		CommandBuffer_SetComputeVectorParam_mCB04E8C59D63D6CDCA0E8EDA362BE1CB7BF49709(L_38, L_39, _stringLiteral3E63EA4D6F8144DD6406580EE9A7B6F874A529E4, L_48, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:189>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_49 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_50 = V_4;
		int32_t L_51 = V_5;
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_52 = V_0;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_53;
		L_53 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_52, NULL);
		NullCheck(L_49);
		CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174(L_49, L_50, L_51, _stringLiteral6067E93B7ED6BC9634C2207045961FBB1126B92A, L_53, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:190>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_54 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_55 = V_4;
		int32_t L_56 = V_5;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_57 = ___0_from;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_58;
		L_58 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_57, NULL);
		NullCheck(L_54);
		CommandBuffer_SetComputeTextureParam_m4EE2EFCF46096652EA2D3D14C0DE3D1252CD2174(L_54, L_55, L_56, _stringLiteralF51190EEE90545F9CD168B86B0A73EF3C85E3A2C, L_58, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:192>
		int32_t L_59;
		L_59 = Mathf_CeilToInt_mF2BF9F4261B3431DC20E10A46CFEEED103C48963_inline(((float)(((float)L_17)/(4.0f))), NULL);
		V_6 = L_59;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:193>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_60 = __this->___m_Command;
		ComputeShader_tA7BDD0F6EE879D149480F5890BA2E665C50CFBF8* L_61 = V_4;
		int32_t L_62 = V_5;
		int32_t L_63 = V_6;
		int32_t L_64 = V_6;
		int32_t L_65 = V_6;
		NullCheck(L_60);
		CommandBuffer_DispatchCompute_mF9F5605B77F0480FD4B8C3BCAEC2FC59A24E31A2(L_60, L_61, L_62, L_63, L_64, L_65, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:194>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_66 = V_0;
		return L_66;
	}

IL_0157:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:204>
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_67 = ___0_from;
		il2cpp_codegen_runtime_class_init_inline(TextureFormatUtilities_t68E492E14F6DE04D603579FC02F042ED0174572D_il2cpp_TypeInfo_var);
		int32_t L_68;
		L_68 = TextureFormatUtilities_GetUncompressedRenderTextureFormat_mCA81A8A50F7AF487D8D4E2AC8363A5FA6D1ECA1A(L_67, NULL);
		V_1 = L_68;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:205>
		int32_t L_69 = V_1;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_70 = ___0_from;
		NullCheck(L_70);
		int32_t L_71;
		L_71 = VirtualFuncInvoker0< int32_t >::Invoke(5, L_70);
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_72 = ___0_from;
		NullCheck(L_72);
		int32_t L_73;
		L_73 = VirtualFuncInvoker0< int32_t >::Invoke(7, L_72);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_74;
		L_74 = TextureLerper_Get_mD700555241B9F131B78A0302883C35AD0936F120(__this, L_69, L_71, L_73, 1, (bool)0, (bool)0, NULL);
		V_0 = L_74;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:207>
		PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* L_75 = __this->___m_PropertySheets;
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_76 = __this->___m_Resources;
		NullCheck(L_76);
		Shaders_t2934A1A9726776BE88E31A97A67A9BD9ACEED86B* L_77 = L_76->___shaders;
		NullCheck(L_77);
		Shader_tADC867D36B7876EE22427FAA2CE485105F4EE692* L_78 = L_77->___texture2dLerp;
		NullCheck(L_75);
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_79;
		L_79 = PropertySheetFactory_Get_mCFDB007DD001F66FCC0EAD7549B63C74857569FC(L_75, L_78, NULL);
		V_2 = L_79;
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:208>
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_80 = V_2;
		NullCheck(L_80);
		MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* L_81;
		L_81 = PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline(L_80, NULL);
		il2cpp_codegen_runtime_class_init_inline(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var);
		int32_t L_82 = ((ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_StaticFields*)il2cpp_codegen_static_fields_for(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var))->___TargetColor;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_83 = ___1_to;
		float L_84 = L_83.___r;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_85 = ___1_to;
		float L_86 = L_85.___g;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_87 = ___1_to;
		float L_88 = L_87.___b;
		Color_tD001788D726C3A7F1379BEED0260B9591F440C1F L_89 = ___1_to;
		float L_90 = L_89.___a;
		Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3 L_91;
		memset((&L_91), 0, sizeof(L_91));
		Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline((&L_91), L_84, L_86, L_88, L_90, NULL);
		NullCheck(L_81);
		MaterialPropertyBlock_SetVector_m22B010D99231EF5684063F4A07F5948854D590B3(L_81, L_82, L_91, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:209>
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_92 = V_2;
		NullCheck(L_92);
		MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* L_93;
		L_93 = PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline(L_92, NULL);
		int32_t L_94 = ((ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_StaticFields*)il2cpp_codegen_static_fields_for(ShaderIDs_t5ECE3A5347F3C559BCA977BD590CC3CB8228BD99_il2cpp_TypeInfo_var))->___Interp;
		float L_95 = ___2_t;
		NullCheck(L_93);
		MaterialPropertyBlock_SetFloat_m6BA8DA03FAD1ABA0BD339E0E5157C4DF3C987267(L_93, L_94, L_95, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:211>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_96 = __this->___m_Command;
		Texture_t791CBB51219779964E0E8A2ED7C1AA5F92A4A700* L_97 = ___0_from;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_98;
		L_98 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_97, NULL);
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_99 = V_0;
		RenderTargetIdentifier_tA528663AC6EB3911D8E91AA40F7070FA5455442B L_100;
		L_100 = RenderTargetIdentifier_op_Implicit_mBF13C6AE62DCEDDEFDC1C7305BE646FE99D2F24C(L_99, NULL);
		PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* L_101 = V_2;
		il2cpp_codegen_initobj((&V_7), sizeof(Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440));
		Nullable_1_t13F9968C978BAF968F02BA5B41ABB481321A5440 L_102 = V_7;
		il2cpp_codegen_runtime_class_init_inline(RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		RuntimeUtilities_BlitFullscreenTriangle_m5E84F777CA552E3540C83CDAEB6C2075F8406E16(L_96, L_98, L_100, L_101, 1, (bool)0, L_102, (bool)0, NULL);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:213>
		RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_103 = V_0;
		return L_103;
	}
}
// Method Definition Index: 56394
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TextureLerper_Clear_mF79AC272E135A60DFE6AB2012B81905934090214 (TextureLerper_t3EAB762818E71EC9F4627D4BF1C4428DF9570424* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:218>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_0 = __this->___m_Actives;
		NullCheck(L_0);
		Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 L_1;
		L_1 = List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A(L_0, List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var);
		V_0 = L_1;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0025:
			{
				Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4((&V_0), Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_001a_1;
			}

IL_000e_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:218>
				RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_2;
				L_2 = Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_inline((&V_0), Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var);
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:219>
				il2cpp_codegen_runtime_class_init_inline(RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
				RuntimeUtilities_Destroy_m88860DDA45529FA1193643863F052D709087B493(L_2, NULL);
			}

IL_001a_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:218>
				bool L_3;
				L_3 = Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5((&V_0), Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var);
				if (L_3)
				{
					goto IL_000e_1;
				}
			}
			{
				goto IL_0033;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0033:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:221>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_4 = __this->___m_Recycled;
		NullCheck(L_4);
		Enumerator_tF357B3332520AE13BF09CAC75EB1CF64734C9693 L_5;
		L_5 = List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A(L_4, List_1_GetEnumerator_m267422B31574DBAB05F061FC47A56C46671B278A_RuntimeMethod_var);
		V_0 = L_5;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0058:
			{
				Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4((&V_0), Enumerator_Dispose_m0BD79377444E12888C74A351C865A99A54DA23A4_RuntimeMethod_var);
				return;
			}
		});
		try
		{
			{
				goto IL_004d_1;
			}

IL_0041_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:221>
				RenderTexture_tBA90C4C3AD9EECCFDDCC632D97C29FAB80D60D27* L_6;
				L_6 = Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_inline((&V_0), Enumerator_get_Current_m62EDA921433856AC26651F6AFD54E11642E37555_RuntimeMethod_var);
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:222>
				il2cpp_codegen_runtime_class_init_inline(RuntimeUtilities_t275B86E9EF6FD2FCA0688D1C65BCE9BEC09BCBB6_il2cpp_TypeInfo_var);
				RuntimeUtilities_Destroy_m88860DDA45529FA1193643863F052D709087B493(L_6, NULL);
			}

IL_004d_1:
			{
				//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:221>
				bool L_7;
				L_7 = Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5((&V_0), Enumerator_MoveNext_mAE1E268534DD3049C788ABE6BB7512784F3EE2A5_RuntimeMethod_var);
				if (L_7)
				{
					goto IL_0041_1;
				}
			}
			{
				goto IL_0066;
			}
		}
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0066:
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:224>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_8 = __this->___m_Actives;
		NullCheck(L_8);
		List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_inline(L_8, List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:225>
		List_1_tF20988DD2863E9321EB7427D9B83C337053F1C8B* L_9 = __this->___m_Recycled;
		NullCheck(L_9);
		List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_inline(L_9, List_1_Clear_m251977459109D48D068772EA3624051434EB6DB0_RuntimeMethod_var);
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/TextureLerper.cs:226>
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 44749
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Vector2_get_magnitude_m5C59B4056420AEFDB291AD0914A3F675330A75CE_inline (Vector2_t1FD6F485C871E832B347AB2DC8CBA08B739D8DF7* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	float V_0 = 0.0f;
	{
		float L_0 = __this->___x;
		float L_1 = __this->___x;
		float L_2 = __this->___y;
		float L_3 = __this->___y;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_4;
		L_4 = sqrt(((double)((float)il2cpp_codegen_add(((float)il2cpp_codegen_multiply(L_0, L_1)), ((float)il2cpp_codegen_multiply(L_2, L_3))))));
		V_0 = ((float)L_4);
		goto IL_0026;
	}

IL_0026:
	{
		float L_5 = V_0;
		return L_5;
	}
}
// Method Definition Index: 56171
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* PostProcessRenderContext_get_command_m028BE33B6194640A1DE901A6F935658034A3E2CD_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/PostProcessRenderContext.cs:78>
		CommandBuffer_tB56007DC84EF56296C325EC32DD12AC1E3DC91F7* L_0 = __this->___U3CcommandU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 56183
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* PostProcessRenderContext_get_propertySheets_m60E7825143611FEC183803150D8F7C2785514D79_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/PostProcessRenderContext.cs:114>
		PropertySheetFactory_t5ABFD70669DCB136C812072371B67AD83FCDD19D* L_0 = __this->___U3CpropertySheetsU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 56181
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* PostProcessRenderContext_get_resources_m89879DF69E4B910F9EE3008AB8DC60B732ABF02A_inline (PostProcessRenderContext_t7A95408B72564734295D248DE20A301815141FD7* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/PostProcessRenderContext.cs:109>
		PostProcessResources_t300E967CDD9643AE04457DA4FFC5B1100885048D* L_0 = __this->___U3CresourcesU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 44702
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Mathf_Max_m7FA442918DE37E3A00106D1F2E789D65829792B8_inline (int32_t ___0_a, int32_t ___1_b, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	int32_t G_B3_0 = 0;
	{
		int32_t L_0 = ___0_a;
		int32_t L_1 = ___1_b;
		if ((((int32_t)L_0) > ((int32_t)L_1)))
		{
			goto IL_0008;
		}
	}
	{
		int32_t L_2 = ___1_b;
		G_B3_0 = L_2;
		goto IL_0009;
	}

IL_0008:
	{
		int32_t L_3 = ___0_a;
		G_B3_0 = L_3;
	}

IL_0009:
	{
		V_0 = G_B3_0;
		goto IL_000c;
	}

IL_000c:
	{
		int32_t L_4 = V_0;
		return L_4;
	}
}
// Method Definition Index: 44805
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Vector4__ctor_m96B2CD8B862B271F513AF0BDC2EABD58E4DBC813_inline (Vector4_t58B63D32F48C0DBF50DE2C60794C4676C80EDBE3* __this, float ___0_x, float ___1_y, float ___2_z, float ___3_w, const RuntimeMethod* method) 
{
	{
		float L_0 = ___0_x;
		__this->___x = L_0;
		float L_1 = ___1_y;
		__this->___y = L_1;
		float L_2 = ___2_z;
		__this->___z = L_2;
		float L_3 = ___3_w;
		__this->___w = L_3;
		return;
	}
}
// Method Definition Index: 44711
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Mathf_CeilToInt_mF2BF9F4261B3431DC20E10A46CFEEED103C48963_inline (float ___0_f, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		float L_0 = ___0_f;
		il2cpp_codegen_runtime_class_init_inline(Math_tEB65DE7CA8B083C412C969C92981C030865486CE_il2cpp_TypeInfo_var);
		double L_1;
		L_1 = ceil(((double)L_0));
		V_0 = il2cpp_codegen_cast_double_to_int<int32_t>(L_1);
		goto IL_000c;
	}

IL_000c:
	{
		int32_t L_2 = V_0;
		return L_2;
	}
}
// Method Definition Index: 56283
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* PropertySheet_get_properties_m3F54B6A690186CF8AE8CCD585068A4DB80AA50F5_inline (PropertySheet_tBA80DF63DC9A09D98B5F30A781448F77410D4397* __this, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.postprocessing@3.4.0/PostProcessing/Runtime/Utils/PropertySheet.cs:16>
		MaterialPropertyBlock_t2308669579033A857EFE6E4831909F638B27411D* L_0 = __this->___U3CpropertiesU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 11909
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_mF590592E32D421DE2C6E2F0D5C2F62FB14CCEFDF_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
// Method Definition Index: 11920
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_m0248A96C5334E9A93E6994B7780478BCD994EA3D_gshared_inline (List_1_t05915E9237850A58106982B7FE4BC5DA4E872E73* __this, int32_t ___0_item, const RuntimeMethod* method) 
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_5, 1));
		Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* L_6 = V_0;
		int32_t L_7 = V_1;
		int32_t L_8 = ___0_item;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (int32_t)L_8);
		return;
	}

IL_0034:
	{
		int32_t L_9 = ___0_item;
		List_1_AddWithResize_m378B392086AAB6F400944FA9839516326B3F7BB8(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		return;
	}
}
// Method Definition Index: 11909
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size;
		return L_0;
	}
}
// Method Definition Index: 11971
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Enumerator_get_Current_m6330F15D18EE4F547C05DF9BF83C5EB710376027_gshared_inline (Enumerator_t9473BAB568A27E2339D48C1F91319E0F6D244D7A* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->____current;
		return L_0;
	}
}
// Method Definition Index: 11928
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Clear_m16C1F2C61FED5955F10EB36BC1CB2DF34B128994_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
	}
	{
		int32_t L_1 = __this->____size;
		V_0 = L_1;
		__this->____size = 0;
		int32_t L_2 = V_0;
		if ((((int32_t)L_2) <= ((int32_t)0)))
		{
			goto IL_003c;
		}
	}
	{
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_3 = __this->____items;
		int32_t L_4 = V_0;
		Array_Clear_m50BAA3751899858B097D3FF2ED31F284703FE5CB((RuntimeArray*)L_3, 0, L_4, NULL);
		return;
	}

IL_003c:
	{
		return;
	}
}
// Method Definition Index: 11920
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version;
		__this->____version = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = __this->____items;
		V_0 = L_1;
		int32_t L_2 = __this->____size;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___0_item;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___0_item;
		List_1_AddWithResize_m79A9BF770BEF9C06BE40D5401E55E375F2726CC4(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 14));
		return;
	}
}
