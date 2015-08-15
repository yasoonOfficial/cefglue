using CefGlue.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefGlue.Enum
{
	public enum CefProxyType
	{
		Direct = cef_proxy_type_t.PROXY_TYPE_DIRECT,
		Named = cef_proxy_type_t.PROXY_TYPE_NAMED,
		PacString = cef_proxy_type_t.PROXY_TYPE_PAC_STRING
	}
}
