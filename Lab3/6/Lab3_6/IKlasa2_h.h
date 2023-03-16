

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 04:14:07 2038
 */
/* Compiler settings for IKlasa2.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */


#ifndef __IKlasa2_h_h__
#define __IKlasa2_h_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IKlasa2_FWD_DEFINED__
#define __IKlasa2_FWD_DEFINED__
typedef interface IKlasa2 IKlasa2;

#endif 	/* __IKlasa2_FWD_DEFINED__ */


#ifndef __Klasa2_FWD_DEFINED__
#define __Klasa2_FWD_DEFINED__

#ifdef __cplusplus
typedef class Klasa2 Klasa2;
#else
typedef struct Klasa2 Klasa2;
#endif /* __cplusplus */

#endif 	/* __Klasa2_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __KlasaLib_LIBRARY_DEFINED__
#define __KlasaLib_LIBRARY_DEFINED__

/* library KlasaLib */
/* [version][lcid][helpstring][uuid] */ 


EXTERN_C const IID LIBID_KlasaLib;

#ifndef __IKlasa2_INTERFACE_DEFINED__
#define __IKlasa2_INTERFACE_DEFINED__

/* interface IKlasa2 */
/* [uuid][object] */ 


EXTERN_C const IID IID_IKlasa2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("442A7716-E106-4279-9BC0-9C1B4ED1779B")
    IKlasa2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Test( 
            /* [in] */ BSTR i) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IKlasa2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IKlasa2 * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IKlasa2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IKlasa2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *Test )( 
            IKlasa2 * This,
            /* [in] */ BSTR i);
        
        END_INTERFACE
    } IKlasa2Vtbl;

    interface IKlasa2
    {
        CONST_VTBL struct IKlasa2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IKlasa2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IKlasa2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IKlasa2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IKlasa2_Test(This,i)	\
    ( (This)->lpVtbl -> Test(This,i) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IKlasa2_INTERFACE_DEFINED__ */


EXTERN_C const CLSID CLSID_Klasa2;

#ifdef __cplusplus

class DECLSPEC_UUID("3F7A6746-5F7C-4A1C-BA61-E2C5C79CC096")
Klasa2;
#endif
#endif /* __KlasaLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


