#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>



struct Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA;
struct Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83;
struct IDictionary_2_t51DBA2F8AFDC8E5CC588729B12034B8C4D30B0AF;
struct IEqualityComparer_1_tAE94C8F24AD5B94D4EE85CA9FC59E3409D41CAF7;
struct IReadOnlyDictionary_2_t8FD5C7F0C22A371C71196C9A42D80E0E47EAC1C8;
struct KeyCollection_t2EDD317F5771E575ACB63527B5AFB71291040342;
struct ValueCollection_t238D0D2427C6B841A01F522A41540165A2C4AE76;
struct EntryU5BU5D_t1AF33AD0B7330843448956EC4277517081658AE7;
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
struct Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012;
struct DiagnosticsFactory_tD2C64CDF0CC356965DB1993359515DC82D715CF2;
struct IDiagnostics_tC5C252A201DC608FD4FA8B95CB546ACE5400345E;
struct IMetrics_tA68E45B18912AD74792533F57895E50599D4A7D5;
struct Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1;
struct MetricsFactory_tFED08C34B8CB569B801796787E82F2818606FA05;
struct String_t;

IL2CPP_EXTERN_C RuntimeClass* Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_tA8E35B180310D6046FB6F4D10E1B4A172B9C0215 
{
};
struct Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83  : public RuntimeObject
{
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets;
	EntryU5BU5D_t1AF33AD0B7330843448956EC4277517081658AE7* ____entries;
	int32_t ____count;
	int32_t ____freeList;
	int32_t ____freeCount;
	int32_t ____version;
	RuntimeObject* ____comparer;
	KeyCollection_t2EDD317F5771E575ACB63527B5AFB71291040342* ____keys;
	ValueCollection_t238D0D2427C6B841A01F522A41540165A2C4AE76* ____values;
	RuntimeObject* ____syncRoot;
};
struct Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012  : public RuntimeObject
{
	RuntimeObject* ___U3CPackageTagsU3Ek__BackingField;
};
struct DiagnosticsFactory_tD2C64CDF0CC356965DB1993359515DC82D715CF2  : public RuntimeObject
{
	RuntimeObject* ___U3CCommonTagsU3Ek__BackingField;
};
struct Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1  : public RuntimeObject
{
	RuntimeObject* ___U3CPackageTagsU3Ek__BackingField;
};
struct MetricsFactory_tFED08C34B8CB569B801796787E82F2818606FA05  : public RuntimeObject
{
	RuntimeObject* ___U3CCommonTagsU3Ek__BackingField;
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
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
struct Double_tE150EF3D1D43DEE85D533810AB4C742307EEDE5F 
{
	double ___m_value;
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
struct String_t_StaticFields
{
	String_t* ___Empty;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, const RuntimeMethod* method) ;

inline void Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052 (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83*, const RuntimeMethod*))Dictionary_2__ctor_m5B32FBC624618211EB461D59CFBB10E987FD1329_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Diagnostics__ctor_m2CF108329965DFBA2D13399A65E9D5D47BF63BD8 (Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Metrics__ctor_m6318435D0C0C0AD81ABC2B7D542FE92D7B5C7D79 (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* __this, const RuntimeMethod* method) ;
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
// Method Definition Index: 65197
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Diagnostics_SendDiagnostic_mF569E18F0662E882CD37D4BC894BC085CB222D08 (Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012* __this, String_t* ___0_name, String_t* ___1_message, RuntimeObject* ___2_tags, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Diagnotstics/Diagnostics.cs:13>
		return;
	}
}
// Method Definition Index: 65198
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Diagnostics__ctor_m2CF108329965DFBA2D13399A65E9D5D47BF63BD8 (Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Diagnotstics/Diagnostics.cs:8>
		Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83* L_0 = (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83*)il2cpp_codegen_object_new(Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052(L_0, Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		__this->___U3CPackageTagsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CPackageTagsU3Ek__BackingField), (void*)L_0);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
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
// Method Definition Index: 65199
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* DiagnosticsFactory_Create_mE152323E55C07B54470CB90071252FF5F24A138B (DiagnosticsFactory_tD2C64CDF0CC356965DB1993359515DC82D715CF2* __this, String_t* ___0_packageName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Diagnotstics/DiagnosticsFactory.cs:12>
		Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012* L_0 = (Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012*)il2cpp_codegen_object_new(Diagnostics_t24C9DED6F621BBF7D712D62FD3776D29B3CE5012_il2cpp_TypeInfo_var);
		Diagnostics__ctor_m2CF108329965DFBA2D13399A65E9D5D47BF63BD8(L_0, NULL);
		return L_0;
	}
}
// Method Definition Index: 65200
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DiagnosticsFactory__ctor_m6AFD4725FFA4F4054B13917A376B8BE059AF4D27 (DiagnosticsFactory_tD2C64CDF0CC356965DB1993359515DC82D715CF2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Diagnotstics/DiagnosticsFactory.cs:8>
		Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83* L_0 = (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83*)il2cpp_codegen_object_new(Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052(L_0, Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		__this->___U3CCommonTagsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCommonTagsU3Ek__BackingField), (void*)L_0);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
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
// Method Definition Index: 65201
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Metrics_Unity_Services_Core_Telemetry_Internal_IMetrics_SendGaugeMetric_m44A74F915A2C4302A6BF07BE010D16D9793D7BE6 (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* __this, String_t* ___0_name, double ___1_value, RuntimeObject* ___2_tags, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/Metrics.cs:12>
		return;
	}
}
// Method Definition Index: 65202
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Metrics_Unity_Services_Core_Telemetry_Internal_IMetrics_SendHistogramMetric_mA7EB1B96D0F7E39A951A68AF58BF6A455E27C9ED (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* __this, String_t* ___0_name, double ___1_time, RuntimeObject* ___2_tags, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/Metrics.cs:17>
		return;
	}
}
// Method Definition Index: 65203
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Metrics_Unity_Services_Core_Telemetry_Internal_IMetrics_SendSumMetric_m3698244EA67D8736E51D84935E05E2DEB290C6A8 (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* __this, String_t* ___0_name, double ___1_value, RuntimeObject* ___2_tags, const RuntimeMethod* method) 
{
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/Metrics.cs:22>
		return;
	}
}
// Method Definition Index: 65204
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Metrics__ctor_m6318435D0C0C0AD81ABC2B7D542FE92D7B5C7D79 (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/Metrics.cs:7>
		Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83* L_0 = (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83*)il2cpp_codegen_object_new(Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052(L_0, Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		__this->___U3CPackageTagsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CPackageTagsU3Ek__BackingField), (void*)L_0);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
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
// Method Definition Index: 65205
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MetricsFactory_Create_m6DCE478FE70DE3D0533A150550812CE038D8CD46 (MetricsFactory_tFED08C34B8CB569B801796787E82F2818606FA05* __this, String_t* ___0_packageName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/MetricsFactory.cs:12>
		Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1* L_0 = (Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1*)il2cpp_codegen_object_new(Metrics_t64BEB1BA35E4B6D2D709508D665841289875F1D1_il2cpp_TypeInfo_var);
		Metrics__ctor_m6318435D0C0C0AD81ABC2B7D542FE92D7B5C7D79(L_0, NULL);
		return L_0;
	}
}
// Method Definition Index: 65206
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MetricsFactory__ctor_mD15F90B2C781DE2B48F41556FECEFAB2347AFC0C (MetricsFactory_tFED08C34B8CB569B801796787E82F2818606FA05* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		//<source_info:./Library/PackageCache/com.unity.services.core@1.12.5/Runtime/Telemetry/Metrics/MetricsFactory.cs:8>
		Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83* L_0 = (Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83*)il2cpp_codegen_object_new(Dictionary_2_t46B2DB028096FA2B828359E52F37F3105A83AD83_il2cpp_TypeInfo_var);
		Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052(L_0, Dictionary_2__ctor_m768E076F1E804CE4959F4E71D3E6A9ADE2F55052_RuntimeMethod_var);
		__this->___U3CCommonTagsU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCommonTagsU3Ek__BackingField), (void*)L_0);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
