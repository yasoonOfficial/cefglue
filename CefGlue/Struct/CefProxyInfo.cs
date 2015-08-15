using CefGlue.Enum;
using CefGlue.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefGlue.Struct
{
	public sealed unsafe class CefProxyInfo: IDisposable
	{
		internal static CefProxyInfo From(cef_proxy_info_t* ptr)
        {
            return new CefProxyInfo(ptr);
        }

        private cef_proxy_info_t* ptr;

        private CefProxyInfo(cef_proxy_info_t* ptr)
        {
            this.ptr = ptr;
        }

        ~CefProxyInfo()
        {
            this.ptr = null;
        }

        public void Dispose()
        {
            this.ptr = null;
        }

		public string ProxyList
		{
			get
			{
				return cef_string_t.ToString(&this.ptr->proxyList);
			}
			set
			{
				cef_string_t.Set(value, &this.ptr->proxyList, true);
			}
		}

		public CefProxyType ProxyType
		{
			get
			{
				return (CefProxyType)this.ptr->proxyType;
			}
			set
			{
				this.ptr->proxyType = (cef_proxy_type_t)value;
			}
		}
	}
}
