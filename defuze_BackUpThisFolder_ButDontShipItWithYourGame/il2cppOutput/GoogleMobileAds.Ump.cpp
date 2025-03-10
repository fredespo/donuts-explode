#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1, typename T2>
struct InterfaceActionInvoker2
{
	typedef void (*Action)(void*, T1, T2, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
	}
};
template <typename T1, typename T2, typename T3>
struct InterfaceActionInvoker3
{
	typedef void (*Action)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
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

struct Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4;
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
struct Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF;
struct Action_2_t156C43F079E7E68155FCDCD12DC77DD11AEF7E3C;
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct TypeU5BU5D_t97234E1129B564EB38B8D85CAC2AD8B5B9522FFB;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct Binder_t91BFCE95A7057FADF4D8A1A342AFE52872246235;
struct ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6;
struct ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF;
struct ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026;
struct IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541;
struct IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E;
struct IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB;
struct MemberFilter_tF644F1AE82F611B677CE1964D5A3277DDA21D553;
struct MethodInfo_t;
struct String_t;
struct Type_t;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012;
struct U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927;
struct U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D;
struct U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3;
struct U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093;
struct U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7;

IL2CPP_EXTERN_C RuntimeClass* Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Type_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral87D3D808CD24C33F1425FFDE4A0710FC4BBC9C19;
IL2CPP_EXTERN_C String_t* _stringLiteralE4186B0B875F8115D04109741D50C304CE9A671F;
IL2CPP_EXTERN_C String_t* _stringLiteralE707B37FA85D7F3FB6BF6DA486F7250D62EBC726;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CLoadU3Ec__AnonStorey0_U3CU3Em__0_mCFE7B081E5FED930F60A770B0E64AC16CEA21861_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CLoadU3Ec__AnonStorey0_U3CU3Em__1_m5E93B0194710C897FF8CDEE171D209D8E67CF953_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CLoadU3Ec__AnonStorey0_U3CU3Em__2_m200EBAF519788BED08C1EA99141B2BF31ED3F766_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CLoadU3Ec__AnonStorey1_U3CU3Em__0_m5179274E91CFEEC3DC12EB844759CACD2FC1CEB5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CShowU3Ec__AnonStorey2_U3CU3Em__0_mBE702451A531B33A0866E66FC694432CE1E07B85_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CShowU3Ec__AnonStorey3_U3CU3Em__0_mF9E3A0B64325161099D1EC708C4E6AB01716EA11_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CUpdateU3Ec__AnonStorey0_U3CU3Em__0_mFDFF30F7D0A0146CFE54AE0C9028996BE6132B57_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CUpdateU3Ec__AnonStorey0_U3CU3Em__1_mCFBF3F6B526674C5E2093DDEE1F4C5A7DDDF5DBC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CUpdateU3Ec__AnonStorey0_U3CU3Em__2_m09B6D5C6B963E2B42A5559B3755F25C47324EFFD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CUpdateU3Ec__AnonStorey1_U3CU3Em__0_m6D326C370DDADE82DB806657F000BAD5C5B77CCF_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;


IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t742906D1ECE8A5FA78682FD3E1F8204B25D019FA 
{
};
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD  : public RuntimeObject
{
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF  : public RuntimeObject
{
	RuntimeObject* ____client;
};
struct ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3  : public RuntimeObject
{
};
struct ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516  : public RuntimeObject
{
	bool ___TagForUnderAgeOfConsent;
	ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6* ___ConsentDebugSettings;
};
struct FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026  : public RuntimeObject
{
	int32_t ___U3CErrorCodeU3Ek__BackingField;
	String_t* ___U3CMessageU3Ek__BackingField;
};
struct MemberInfo_t  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct Utils_t2CF23FFC5ED202531D5A300162B6AD7CCF89B94D  : public RuntimeObject
{
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
struct U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012  : public RuntimeObject
{
	Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* ___formLoadCallback;
	RuntimeObject* ___client;
};
struct U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927  : public RuntimeObject
{
	Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* ___onDismissed;
};
struct U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D  : public RuntimeObject
{
	Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* ___consentInfoUpdateCallback;
};
struct U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3  : public RuntimeObject
{
	FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___error;
	U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* ___U3CU3Ef__refU240;
};
struct U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093  : public RuntimeObject
{
	FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___error;
	U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* ___U3CU3Ef__refU242;
};
struct U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7  : public RuntimeObject
{
	FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___error;
	U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* ___U3CU3Ef__refU240;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
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
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
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
struct ConsentStatus_t17E6578CFF6D27E574D6437D2834B200E08B4777 
{
	int32_t ___value__;
};
struct DebugGeography_tE764B93413E15CC10191FEAFB27703EB137D4722 
{
	int32_t ___value__;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct RuntimePlatform_t9A8AAF204603076FCAAECCCC05DA386AEE7BF66E 
{
	int32_t ___value__;
};
struct RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B 
{
	intptr_t ___value;
};
struct ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6  : public RuntimeObject
{
	int32_t ___DebugGeography;
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* ___TestDeviceHashedIds;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct Type_t  : public MemberInfo_t
{
	RuntimeTypeHandle_t332A452B8B6179E4469B69525D0FE82A88030F7B ____impl;
};
struct Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4  : public MulticastDelegate_t
{
};
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};
struct Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF  : public MulticastDelegate_t
{
};
struct Action_2_t156C43F079E7E68155FCDCD12DC77DD11AEF7E3C  : public MulticastDelegate_t
{
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_StaticFields
{
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___s_emptyArray;
};
struct ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_StaticFields
{
	RuntimeObject* ____clientFactory;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct IntPtr_t_StaticFields
{
	intptr_t ___Zero;
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


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_2_Invoke_m7BFCE0BBCF67689D263059B56A8D79161B698587_gshared_inline (Action_2_t156C43F079E7E68155FCDCD12DC77DD11AEF7E3C* __this, RuntimeObject* ___0_arg1, RuntimeObject* ___1_arg2, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey0__ctor_m5D157B75B884DF130C17084209BA0045F3BB4B3F (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
inline void Action_1__ctor_m021285912E458C3D54D9EAD926A8BB4CBC77908A (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey2__ctor_mBF5A9EEF097D451C3E43D1DD81954354DB60A7DC (U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2 (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_action, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey1__ctor_m4230348DB164723CA9736F7188B2824F682C03E6 (U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentForm__ctor_m7E621E44264000847F539870184EB3230EDFEF41 (ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF* __this, RuntimeObject* ___0_client, const RuntimeMethod* method) ;
inline void Action_2_Invoke_mD12E1AE2987FA9B92D6566FA260B000A58911A93_inline (Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* __this, ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF* ___0_arg1, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___1_arg2, const RuntimeMethod* method)
{
	((  void (*) (Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF*, ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF*, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026*, const RuntimeMethod*))Action_2_Invoke_m7BFCE0BBCF67689D263059B56A8D79161B698587_gshared_inline)(__this, ___0_arg1, ___1_arg2, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey3__ctor_m5DFC2C2D5962E76339884182F9CB77D9FADAF06F (U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* __this, const RuntimeMethod* method) ;
inline void Action_1_Invoke_m3DEF207D7DDE949DDC2EC78FF96E9789029BAC51_inline (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* __this, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4*, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___0_obj, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey0__ctor_m3525CFA3A371A63BCAF453F55D8BCC95E94D4C3C (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey1__ctor_mCE49B6C8A8E97FDC7FDA2642E82FEAE6BD1BDF68 (U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* __this, const RuntimeMethod* method) ;
inline void List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentDebugSettings__ctor_m55A234F30D3B678E063DFBB6B9D838DC5768744D (ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FormError_set_ErrorCode_mCC38EFF4FB071A3DA5B2E0D0F77A527E1D5B5426_inline (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, int32_t ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FormError_set_Message_mA6F54ADCA0D8A37C7CBD0A68B213AAE392BA8F1F_inline (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, String_t* ___0_value, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Activator_CreateInstance_mFF030428C64FDDFACC74DFAC97388A1C628BFBCF (Type_t* ___0_type, const RuntimeMethod* method) ;
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
// Method Definition Index: 65013
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentForm__ctor_m7E621E44264000847F539870184EB3230EDFEF41 (ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF* __this, RuntimeObject* ___0_client, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		RuntimeObject* L_0 = ___0_client;
		__this->____client = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->____client), (void*)L_0);
		return;
	}
}
// Method Definition Index: 65014
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentForm_Load_mAB879496A1456226F52B4CE47B53975BB4ED5656 (Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* ___0_formLoadCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey0_U3CU3Em__0_mCFE7B081E5FED930F60A770B0E64AC16CEA21861_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey0_U3CU3Em__1_m5E93B0194710C897FF8CDEE171D209D8E67CF953_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* V_0 = NULL;
	{
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_0 = (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012*)il2cpp_codegen_object_new(U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012_il2cpp_TypeInfo_var);
		U3CLoadU3Ec__AnonStorey0__ctor_m5D157B75B884DF130C17084209BA0045F3BB4B3F(L_0, NULL);
		V_0 = L_0;
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_1 = V_0;
		Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* L_2 = ___0_formLoadCallback;
		NullCheck(L_1);
		L_1->___formLoadCallback = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___formLoadCallback), (void*)L_2);
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_3 = V_0;
		RuntimeObject* L_4;
		L_4 = ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6(NULL);
		NullCheck(L_4);
		RuntimeObject* L_5;
		L_5 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var, L_4);
		NullCheck(L_3);
		L_3->___client = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___client), (void*)L_5);
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_6 = V_0;
		NullCheck(L_6);
		RuntimeObject* L_7 = L_6->___client;
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_8 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_9 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_9, L_8, (intptr_t)((void*)U3CLoadU3Ec__AnonStorey0_U3CU3Em__0_mCFE7B081E5FED930F60A770B0E64AC16CEA21861_RuntimeMethod_var), NULL);
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_10 = V_0;
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_11 = (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4*)il2cpp_codegen_object_new(Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		Action_1__ctor_m021285912E458C3D54D9EAD926A8BB4CBC77908A(L_11, L_10, (intptr_t)((void*)U3CLoadU3Ec__AnonStorey0_U3CU3Em__1_m5E93B0194710C897FF8CDEE171D209D8E67CF953_RuntimeMethod_var), NULL);
		NullCheck(L_7);
		InterfaceActionInvoker2< Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*, Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* >::Invoke(0, IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541_il2cpp_TypeInfo_var, L_7, L_9, L_11);
		return;
	}
}
// Method Definition Index: 65015
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentForm_Show_m4B7187A682576C0A936E602958AFEDB712583C24 (ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF* __this, Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* ___0_onDismissed, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CShowU3Ec__AnonStorey2_U3CU3Em__0_mBE702451A531B33A0866E66FC694432CE1E07B85_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* V_0 = NULL;
	{
		U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* L_0 = (U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927*)il2cpp_codegen_object_new(U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927_il2cpp_TypeInfo_var);
		U3CShowU3Ec__AnonStorey2__ctor_mBF5A9EEF097D451C3E43D1DD81954354DB60A7DC(L_0, NULL);
		V_0 = L_0;
		U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* L_1 = V_0;
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_2 = ___0_onDismissed;
		NullCheck(L_1);
		L_1->___onDismissed = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___onDismissed), (void*)L_2);
		RuntimeObject* L_3 = __this->____client;
		U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* L_4 = V_0;
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_5 = (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4*)il2cpp_codegen_object_new(Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		Action_1__ctor_m021285912E458C3D54D9EAD926A8BB4CBC77908A(L_5, L_4, (intptr_t)((void*)U3CShowU3Ec__AnonStorey2_U3CU3Em__0_mBE702451A531B33A0866E66FC694432CE1E07B85_RuntimeMethod_var), NULL);
		NullCheck(L_3);
		InterfaceActionInvoker1< Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* >::Invoke(1, IConsentFormClient_t74E4CFA27BAAA6057C6C3F92D010640FFF44B541_il2cpp_TypeInfo_var, L_3, L_5);
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
// Method Definition Index: 65016
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey0__ctor_m5D157B75B884DF130C17084209BA0045F3BB4B3F (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65017
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey0_U3CU3Em__0_mCFE7B081E5FED930F60A770B0E64AC16CEA21861 (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey0_U3CU3Em__2_m200EBAF519788BED08C1EA99141B2BF31ED3F766_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* L_0 = __this->___formLoadCallback;
		if (!L_0)
		{
			goto IL_001c;
		}
	}
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_1, __this, (intptr_t)((void*)U3CLoadU3Ec__AnonStorey0_U3CU3Em__2_m200EBAF519788BED08C1EA99141B2BF31ED3F766_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2(L_1, NULL);
	}

IL_001c:
	{
		return;
	}
}
// Method Definition Index: 65018
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey0_U3CU3Em__1_m5E93B0194710C897FF8CDEE171D209D8E67CF953 (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* __this, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___0_error, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey1_U3CU3Em__0_m5179274E91CFEEC3DC12EB844759CACD2FC1CEB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* V_0 = NULL;
	{
		U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* L_0 = (U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3*)il2cpp_codegen_object_new(U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3_il2cpp_TypeInfo_var);
		U3CLoadU3Ec__AnonStorey1__ctor_m4230348DB164723CA9736F7188B2824F682C03E6(L_0, NULL);
		V_0 = L_0;
		U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3Ef__refU240 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3Ef__refU240), (void*)__this);
		U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* L_2 = V_0;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_3 = ___0_error;
		NullCheck(L_2);
		L_2->___error = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___error), (void*)L_3);
		Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* L_4 = __this->___formLoadCallback;
		if (!L_4)
		{
			goto IL_0030;
		}
	}
	{
		U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* L_5 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_6, L_5, (intptr_t)((void*)U3CLoadU3Ec__AnonStorey1_U3CU3Em__0_m5179274E91CFEEC3DC12EB844759CACD2FC1CEB5_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2(L_6, NULL);
	}

IL_0030:
	{
		return;
	}
}
// Method Definition Index: 65019
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey0_U3CU3Em__2_m200EBAF519788BED08C1EA99141B2BF31ED3F766 (U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* L_0 = __this->___formLoadCallback;
		RuntimeObject* L_1 = __this->___client;
		ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF* L_2 = (ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF*)il2cpp_codegen_object_new(ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF_il2cpp_TypeInfo_var);
		ConsentForm__ctor_m7E621E44264000847F539870184EB3230EDFEF41(L_2, L_1, NULL);
		NullCheck(L_0);
		Action_2_Invoke_mD12E1AE2987FA9B92D6566FA260B000A58911A93_inline(L_0, L_2, (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026*)NULL, NULL);
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
// Method Definition Index: 65020
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey1__ctor_m4230348DB164723CA9736F7188B2824F682C03E6 (U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65021
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CLoadU3Ec__AnonStorey1_U3CU3Em__0_m5179274E91CFEEC3DC12EB844759CACD2FC1CEB5 (U3CLoadU3Ec__AnonStorey1_tAED083EC5BC483A9A7E6A51E812AF8A0887B62F3* __this, const RuntimeMethod* method) 
{
	{
		U3CLoadU3Ec__AnonStorey0_t4CE6008F4737411514DA60A638B984ACF49F3012* L_0 = __this->___U3CU3Ef__refU240;
		NullCheck(L_0);
		Action_2_t635A5B9FAB4E7C090556DCA3F0CD60AD640D41CF* L_1 = L_0->___formLoadCallback;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_2 = __this->___error;
		NullCheck(L_1);
		Action_2_Invoke_mD12E1AE2987FA9B92D6566FA260B000A58911A93_inline(L_1, (ConsentForm_t5610A8E6FEE812A11649F04979CB8679F9B0E1EF*)NULL, L_2, NULL);
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
// Method Definition Index: 65022
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey2__ctor_mBF5A9EEF097D451C3E43D1DD81954354DB60A7DC (U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65023
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey2_U3CU3Em__0_mBE702451A531B33A0866E66FC694432CE1E07B85 (U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* __this, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___0_error, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CShowU3Ec__AnonStorey3_U3CU3Em__0_mF9E3A0B64325161099D1EC708C4E6AB01716EA11_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* V_0 = NULL;
	{
		U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* L_0 = (U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093*)il2cpp_codegen_object_new(U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093_il2cpp_TypeInfo_var);
		U3CShowU3Ec__AnonStorey3__ctor_m5DFC2C2D5962E76339884182F9CB77D9FADAF06F(L_0, NULL);
		V_0 = L_0;
		U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3Ef__refU242 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3Ef__refU242), (void*)__this);
		U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* L_2 = V_0;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_3 = ___0_error;
		NullCheck(L_2);
		L_2->___error = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___error), (void*)L_3);
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_4 = __this->___onDismissed;
		if (!L_4)
		{
			goto IL_0030;
		}
	}
	{
		U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* L_5 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_6, L_5, (intptr_t)((void*)U3CShowU3Ec__AnonStorey3_U3CU3Em__0_mF9E3A0B64325161099D1EC708C4E6AB01716EA11_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2(L_6, NULL);
	}

IL_0030:
	{
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
// Method Definition Index: 65024
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey3__ctor_m5DFC2C2D5962E76339884182F9CB77D9FADAF06F (U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65025
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CShowU3Ec__AnonStorey3_U3CU3Em__0_mF9E3A0B64325161099D1EC708C4E6AB01716EA11 (U3CShowU3Ec__AnonStorey3_t7F1297BA08B3793FB83B545F900AA45B0EA5A093* __this, const RuntimeMethod* method) 
{
	{
		U3CShowU3Ec__AnonStorey2_tF77FC8FCEBA17C61D850A0FBCA5355ED0C7D4927* L_0 = __this->___U3CU3Ef__refU242;
		NullCheck(L_0);
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_1 = L_0->___onDismissed;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_2 = __this->___error;
		NullCheck(L_1);
		Action_1_Invoke_m3DEF207D7DDE949DDC2EC78FF96E9789029BAC51_inline(L_1, L_2, NULL);
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
// Method Definition Index: 65026
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = ((ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_StaticFields*)il2cpp_codegen_static_fields_for(ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var))->____clientFactory;
		if (L_0)
		{
			goto IL_0014;
		}
	}
	{
		RuntimeObject* L_1;
		L_1 = Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409(NULL);
		((ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_StaticFields*)il2cpp_codegen_static_fields_for(ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var))->____clientFactory = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_StaticFields*)il2cpp_codegen_static_fields_for(ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var))->____clientFactory), (void*)L_1);
	}

IL_0014:
	{
		RuntimeObject* L_2 = ((ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_StaticFields*)il2cpp_codegen_static_fields_for(ConsentInformation_t7D85F62D07F64B8B78347A408923F50AF7C9DDC3_il2cpp_TypeInfo_var))->____clientFactory;
		return L_2;
	}
}
// Method Definition Index: 65027
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t ConsentInformation_get_ConsentStatus_m0FD7B16BCB11F667C11CA56AB894BFFFDB12656D (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0;
		L_0 = ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6(NULL);
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var, L_0);
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		NullCheck(L_2);
		int32_t L_3;
		L_3 = InterfaceFuncInvoker0< int32_t >::Invoke(1, IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var, L_2);
		return (int32_t)(L_3);
	}
}
// Method Definition Index: 65028
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentInformation_Update_m77C30ED8853B66168D77E0F02666360C78671046 (ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516* ___0_request, Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* ___1_consentInfoUpdateCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey0_U3CU3Em__0_mFDFF30F7D0A0146CFE54AE0C9028996BE6132B57_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey0_U3CU3Em__1_mCFBF3F6B526674C5E2093DDEE1F4C5A7DDDF5DBC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* V_0 = NULL;
	RuntimeObject* V_1 = NULL;
	{
		U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* L_0 = (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D*)il2cpp_codegen_object_new(U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D_il2cpp_TypeInfo_var);
		U3CUpdateU3Ec__AnonStorey0__ctor_m3525CFA3A371A63BCAF453F55D8BCC95E94D4C3C(L_0, NULL);
		V_0 = L_0;
		U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* L_1 = V_0;
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_2 = ___1_consentInfoUpdateCallback;
		NullCheck(L_1);
		L_1->___consentInfoUpdateCallback = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___consentInfoUpdateCallback), (void*)L_2);
		RuntimeObject* L_3;
		L_3 = ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6(NULL);
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var, L_3);
		V_1 = L_4;
		RuntimeObject* L_5 = V_1;
		ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516* L_6 = ___0_request;
		U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* L_7 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_8 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_8, L_7, (intptr_t)((void*)U3CUpdateU3Ec__AnonStorey0_U3CU3Em__0_mFDFF30F7D0A0146CFE54AE0C9028996BE6132B57_RuntimeMethod_var), NULL);
		U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* L_9 = V_0;
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_10 = (Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4*)il2cpp_codegen_object_new(Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4_il2cpp_TypeInfo_var);
		Action_1__ctor_m021285912E458C3D54D9EAD926A8BB4CBC77908A(L_10, L_9, (intptr_t)((void*)U3CUpdateU3Ec__AnonStorey0_U3CU3Em__1_mCFBF3F6B526674C5E2093DDEE1F4C5A7DDDF5DBC_RuntimeMethod_var), NULL);
		NullCheck(L_5);
		InterfaceActionInvoker3< ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516*, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*, Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* >::Invoke(0, IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var, L_5, L_6, L_8, L_10);
		return;
	}
}
// Method Definition Index: 65029
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConsentInformation_CanRequestAds_m7417C9A869670EBB8E9EA1B055FA959D5630C1E9 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0;
		L_0 = ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6(NULL);
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var, L_0);
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		NullCheck(L_2);
		bool L_3;
		L_3 = InterfaceFuncInvoker0< bool >::Invoke(2, IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var, L_2);
		return L_3;
	}
}
// Method Definition Index: 65030
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool ConsentInformation_IsConsentFormAvailable_m55FB09EEC81648619F52F648D75AA6DBAC5B6ECE (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		RuntimeObject* L_0;
		L_0 = ConsentInformation_get_ClientFactory_mD99BFD395187E5C21F07E369A6DF612D300060A6(NULL);
		NullCheck(L_0);
		RuntimeObject* L_1;
		L_1 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(1, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var, L_0);
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		NullCheck(L_2);
		bool L_3;
		L_3 = InterfaceFuncInvoker0< bool >::Invoke(3, IConsentInformationClient_tF58668605A3AC2F36DB94BA4A3A2621D3059259E_il2cpp_TypeInfo_var, L_2);
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
// Method Definition Index: 65031
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey0__ctor_m3525CFA3A371A63BCAF453F55D8BCC95E94D4C3C (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65032
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey0_U3CU3Em__0_mFDFF30F7D0A0146CFE54AE0C9028996BE6132B57 (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey0_U3CU3Em__2_m09B6D5C6B963E2B42A5559B3755F25C47324EFFD_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_0 = __this->___consentInfoUpdateCallback;
		if (!L_0)
		{
			goto IL_001c;
		}
	}
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_1, __this, (intptr_t)((void*)U3CUpdateU3Ec__AnonStorey0_U3CU3Em__2_m09B6D5C6B963E2B42A5559B3755F25C47324EFFD_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2(L_1, NULL);
	}

IL_001c:
	{
		return;
	}
}
// Method Definition Index: 65033
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey0_U3CU3Em__1_mCFBF3F6B526674C5E2093DDEE1F4C5A7DDDF5DBC (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* __this, FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* ___0_error, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey1_U3CU3Em__0_m6D326C370DDADE82DB806657F000BAD5C5B77CCF_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* V_0 = NULL;
	{
		U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* L_0 = (U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7*)il2cpp_codegen_object_new(U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7_il2cpp_TypeInfo_var);
		U3CUpdateU3Ec__AnonStorey1__ctor_mCE49B6C8A8E97FDC7FDA2642E82FEAE6BD1BDF68(L_0, NULL);
		V_0 = L_0;
		U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* L_1 = V_0;
		NullCheck(L_1);
		L_1->___U3CU3Ef__refU240 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___U3CU3Ef__refU240), (void*)__this);
		U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* L_2 = V_0;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_3 = ___0_error;
		NullCheck(L_2);
		L_2->___error = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___error), (void*)L_3);
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_4 = __this->___consentInfoUpdateCallback;
		if (!L_4)
		{
			goto IL_0030;
		}
	}
	{
		U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* L_5 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_6, L_5, (intptr_t)((void*)U3CUpdateU3Ec__AnonStorey1_U3CU3Em__0_m6D326C370DDADE82DB806657F000BAD5C5B77CCF_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(MobileAds_tE6A198EF1FBB6C42E5C1062E4B6CD5FB5B5494D5_il2cpp_TypeInfo_var);
		MobileAds_RaiseAction_mE70DFA931C5DC14845C02D5B1D0A994FE29370B2(L_6, NULL);
	}

IL_0030:
	{
		return;
	}
}
// Method Definition Index: 65034
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey0_U3CU3Em__2_m09B6D5C6B963E2B42A5559B3755F25C47324EFFD (U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* __this, const RuntimeMethod* method) 
{
	{
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_0 = __this->___consentInfoUpdateCallback;
		NullCheck(L_0);
		Action_1_Invoke_m3DEF207D7DDE949DDC2EC78FF96E9789029BAC51_inline(L_0, (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026*)NULL, NULL);
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
// Method Definition Index: 65035
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey1__ctor_mCE49B6C8A8E97FDC7FDA2642E82FEAE6BD1BDF68 (U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// Method Definition Index: 65036
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CUpdateU3Ec__AnonStorey1_U3CU3Em__0_m6D326C370DDADE82DB806657F000BAD5C5B77CCF (U3CUpdateU3Ec__AnonStorey1_t4DCCA3AA69929F3A51FC97B23F0DDAE47AC18FB7* __this, const RuntimeMethod* method) 
{
	{
		U3CUpdateU3Ec__AnonStorey0_t0206FA681FB9288FC1AD58C3A4E25EEFF274393D* L_0 = __this->___U3CU3Ef__refU240;
		NullCheck(L_0);
		Action_1_tB03D82616088D202ABD23F934CC2976A2ED530B4* L_1 = L_0->___consentInfoUpdateCallback;
		FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* L_2 = __this->___error;
		NullCheck(L_1);
		Action_1_Invoke_m3DEF207D7DDE949DDC2EC78FF96E9789029BAC51_inline(L_1, L_2, NULL);
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
// Method Definition Index: 65037
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentDebugSettings__ctor_m55A234F30D3B678E063DFBB6B9D838DC5768744D (ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_0 = (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*)il2cpp_codegen_object_new(List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E(L_0, List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		__this->___TestDeviceHashedIds = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___TestDeviceHashedIds), (void*)L_0);
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
// Method Definition Index: 65038
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ConsentRequestParameters__ctor_mB689D636424571F47A023696E0916A769EE1EC5C (ConsentRequestParameters_t34C1E8C04ED21B543DFE57708C303AABEA447516* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6* L_0 = (ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6*)il2cpp_codegen_object_new(ConsentDebugSettings_t21BCD70B1E4DB762E04807E88E78285CC51370C6_il2cpp_TypeInfo_var);
		ConsentDebugSettings__ctor_m55A234F30D3B678E063DFBB6B9D838DC5768744D(L_0, NULL);
		__this->___ConsentDebugSettings = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___ConsentDebugSettings), (void*)L_0);
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
// Method Definition Index: 65039
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FormError__ctor_m74D2F9BD01E242B45657155A11219192DF02A8A7 (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, int32_t ___0_errorCode, String_t* ___1_message, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		int32_t L_0 = ___0_errorCode;
		FormError_set_ErrorCode_mCC38EFF4FB071A3DA5B2E0D0F77A527E1D5B5426_inline(__this, L_0, NULL);
		String_t* L_1 = ___1_message;
		FormError_set_Message_mA6F54ADCA0D8A37C7CBD0A68B213AAE392BA8F1F_inline(__this, L_1, NULL);
		return;
	}
}
// Method Definition Index: 65040
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t FormError_get_ErrorCode_m56DA072ADC0AC19C1684477D7262DD62E532A946 (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->___U3CErrorCodeU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 65041
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FormError_set_ErrorCode_mCC38EFF4FB071A3DA5B2E0D0F77A527E1D5B5426 (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_value;
		__this->___U3CErrorCodeU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 65042
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* FormError_get_Message_mF77BE1DEF78A279C9B197AFF203EA2E014E952F7 (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = __this->___U3CMessageU3Ek__BackingField;
		return L_0;
	}
}
// Method Definition Index: 65043
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FormError_set_Message_mA6F54ADCA0D8A37C7CBD0A68B213AAE392BA8F1F (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_value;
		__this->___U3CMessageU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMessageU3Ek__BackingField), (void*)L_0);
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
// Method Definition Index: 65044
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Type_t_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral87D3D808CD24C33F1425FFDE4A0710FC4BBC9C19);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE4186B0B875F8115D04109741D50C304CE9A671F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE707B37FA85D7F3FB6BF6DA486F7250D62EBC726);
		s_Il2CppMethodInitialized = true;
	}
	String_t* V_0 = NULL;
	Type_t* V_1 = NULL;
	{
		V_0 = (String_t*)NULL;
		il2cpp_codegen_runtime_class_init_inline(Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		int32_t L_0;
		L_0 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
		if ((!(((uint32_t)L_0) == ((uint32_t)8))))
		{
			goto IL_0018;
		}
	}
	{
		V_0 = _stringLiteral87D3D808CD24C33F1425FFDE4A0710FC4BBC9C19;
		goto IL_0035;
	}

IL_0018:
	{
		il2cpp_codegen_runtime_class_init_inline(Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		int32_t L_1;
		L_1 = Application_get_platform_m59EF7D6155D18891B24767F83F388160B1FF2138(NULL);
		if ((!(((uint32_t)L_1) == ((uint32_t)((int32_t)11)))))
		{
			goto IL_002f;
		}
	}
	{
		V_0 = _stringLiteralE4186B0B875F8115D04109741D50C304CE9A671F;
		goto IL_0035;
	}

IL_002f:
	{
		V_0 = _stringLiteralE707B37FA85D7F3FB6BF6DA486F7250D62EBC726;
	}

IL_0035:
	{
		String_t* L_2 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Type_t_il2cpp_TypeInfo_var);
		Type_t* L_3;
		L_3 = il2cpp_codegen_get_type(L_2, Type_GetType_m71A077E0B5DA3BD1DC0AB9AE387056CFCF56F93F_RuntimeMethod_var, Utils_GetClientFactory_m94A1DAB83A77761C867D03DD43103387F9ABF409_RuntimeMethod_var);
		V_1 = L_3;
		Type_t* L_4 = V_1;
		RuntimeObject* L_5;
		L_5 = Activator_CreateInstance_mFF030428C64FDDFACC74DFAC97388A1C628BFBCF(L_4, NULL);
		return ((RuntimeObject*)Castclass((RuntimeObject*)L_5, IUmpClientFactory_tCB177A94EFFB99686E08D634BBF3D8502D57E6DB_il2cpp_TypeInfo_var));
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// Method Definition Index: 65041
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FormError_set_ErrorCode_mCC38EFF4FB071A3DA5B2E0D0F77A527E1D5B5426_inline (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_value;
		__this->___U3CErrorCodeU3Ek__BackingField = L_0;
		return;
	}
}
// Method Definition Index: 65043
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void FormError_set_Message_mA6F54ADCA0D8A37C7CBD0A68B213AAE392BA8F1F_inline (FormError_t925BBA051FDAC8CC3DECB9E5511864E8ED383026* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		String_t* L_0 = ___0_value;
		__this->___U3CMessageU3Ek__BackingField = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMessageU3Ek__BackingField), (void*)L_0);
		return;
	}
}
// Method Definition Index: 882
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_2_Invoke_m7BFCE0BBCF67689D263059B56A8D79161B698587_gshared_inline (Action_2_t156C43F079E7E68155FCDCD12DC77DD11AEF7E3C* __this, RuntimeObject* ___0_arg1, RuntimeObject* ___1_arg2, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_arg1, ___1_arg2, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
// Method Definition Index: 880
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
