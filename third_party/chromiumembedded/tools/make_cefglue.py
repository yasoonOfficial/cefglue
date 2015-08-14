# Copyright (c) 2011 The Chromium Embedded Framework Authors. All rights
# reserved. Use of this source code is governed by a BSD-style license that
# can be found in the LICENSE file.

from cef_parser import *

def is_handler_class(cls):
    name = cls.get_capi_name();

    if name == "cef_scheme_handler_callback_t":
    	return False

    return (re.match(".*handler_.*", name) != None 
            or re.match(".*_listener_.*", name) != None 
            or re.match(".*_filter_.*", name) != None 
            or name == "cef_app_t"
            or name == "cef_client_t"
            or name == "cef_v8accessor_t"
            or name == "cef_task_t"
            or name == "cef_web_urlrequest_client_t"
            or re.match(".*visitor_t", name) != None
            );

def make_cefglue_handlerschema(classes, cefgluedir):
    dir = cefgluedir;
    filename = 'HandlerSchema.g.ttinclude';

    result = """<#
//
// DO NOT MODIFY! THIS IS AUTO-GENERATED FILE!
//

this.HandlerSchema = CreateHandlerSchema();

#><#+

private Dictionary<string, HandlerDef> HandlerSchema { get; set; }

Dictionary<string, HandlerDef> CreateHandlerSchema()
{
    var schema = new Dictionary<string, HandlerDef>();
    HandlerDef def;
""";

    for cls in classes:
        if not is_handler_class(cls):
            continue
        make_cefglue_handlerimpl(cls, cefgluedir);
        result += """
    // """ + cls.get_cefglue_name() + """
    def = new HandlerDef()
    {
        ClassName = """ + '"' + cls.get_cefglue_name() + '"' + """,
        StructName = """ + '"' + cls.get_capi_name() + '"' + """,
    };
""";
        funcs = cls.get_virtual_funcs();
        for func in funcs:
            result += """    def.AddCallback(""" + '"' + func.get_capi_name() + '"' + """);\n""";
        result += """    schema.Add(def.ClassName, def);\n""";

    result += """
    return schema;
}

#>""";

    write_cefglue_file(dir, filename, result);
    return

def make_cefglue_handlerimpl(cls, cefgluedir):
    dir = cefgluedir + '/HandlerImpl/';
    filename = cls.get_cefglue_name() + ".Impl.cs";
    result = """namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class """ + cls.get_cefglue_name() + """
    {
""";

    indent = "        ";


    # static functions (actual handlers doesn't have static functions)
    funcs = cls.get_static_funcs()
    for func in funcs:
        parts = func.get_cefglue_parts();
        result += format_comment_cefglue(func.get_comment(), indent)
        result += indent + '/* FIXME: ' + cls.get_cefglue_name() + '.' + func.get_name() + ' public */\n'
        result += indent + 'static ' + parts['retval'] + ' ' + func.get_name() + '(' + string.join(parts['args'], ', ') + ')\n';
        result += indent + '{\n';
        result += indent + '    // TODO: ' + cls.get_cefglue_name() + '.' + func.get_name() + '\n';
        result += indent + '    ' + 'throw new NotImplementedException();\n';
        result += indent + '}\n';
        result += '\n'


    funcs = cls.get_virtual_funcs();
    for func in funcs:
        parts = func.get_cefglue_parts();
        result += format_comment_cefglue(func.get_comment(), indent)
        result += indent + 'private ' + parts['retval'] + ' ' + parts['name'] + '(' + string.join(parts['args'], ', ') + ')\n';
        result += indent + '{\n';
        result += indent + '    ThrowIfObjectDisposed();\n';
        result += indent + '    // TODO: ' + cls.get_cefglue_name() + '.' + parts['name'] + '\n';
        result += indent + '    ' + 'throw new NotImplementedException();\n';
        result += indent + '}\n';
        result += '\n'

    result += """
    }
}
""";

    write_cefglue_file(dir, filename, result);
    return

def make_cefglue_proxyschema(classes, cefgluedir):
    dir = cefgluedir;
    filename = 'ProxySchema.g.ttinclude';


    result = """<#
//
// DO NOT MODIFY! THIS IS AUTO-GENERATED FILE!
//

this.ProxySchema = CreateProxySchema();

#><#+

private Dictionary<string, ProxyDef> ProxySchema { get; set; }

Dictionary<string, ProxyDef> CreateProxySchema()
{
    var schema = new Dictionary<string, ProxyDef>();
    ProxyDef def;
""";

    for cls in classes:
        if is_handler_class(cls):
            continue
        make_cefglue_proxyimpl(cls, cefgluedir);
        result += """
    // """ + cls.get_cefglue_name() + """
    def = new ProxyDef()
    {
        ClassName = """ + '"' + cls.get_cefglue_name() + '"' + """,
        StructName = """ + '"' + cls.get_capi_name() + '"' + """,
    };
""";
        funcs = cls.get_virtual_funcs();
        for func in funcs:
            result += """    def.AddMethod(""" + '"' + func.get_capi_name() + '"' + """);\n""";
        result += """    schema.Add(def.ClassName, def);\n""";

    result += """
    return schema;
}

#>""";

    write_cefglue_file(dir, filename, result);
    return

def make_cefglue_proxyimpl(cls, cefgluedir):
    dir = cefgluedir + '/ProxyImpl/';
    filename = cls.get_cefglue_name() + ".Impl.cs";
    result = """namespace CefGlue
{
    using System;
    using CefGlue.Interop;

    unsafe partial class """ + cls.get_cefglue_name() + """
    {
""";

    indent = "        ";

    # static functions
    funcs = cls.get_static_funcs()
    for func in funcs:
        parts = func.get_cefglue_parts();
        result += format_comment_cefglue(func.get_comment(), indent)
        result += indent + '/* FIXME: ' + cls.get_cefglue_name() + '.' + func.get_name() + ' public */\n'
        result += indent + 'static ' + parts['retval'] + ' ' + func.get_name() + '(' + string.join(parts['args'], ', ') + ')\n';
        result += indent + '{\n';
        result += indent + '    // TODO: ' + cls.get_cefglue_name() + '.' + func.get_name() + '\n';
        result += indent + '    ' + 'throw new NotImplementedException();\n';
        result += indent + '}\n';
        result += '\n'


    funcs = cls.get_virtual_funcs();
    for func in funcs:
        parts = func.get_cefglue_parts();
        result += format_comment_cefglue(func.get_comment(), indent)

        #sys.stdout.write( format_comment_cefglue(func.get_comment(), indent) )
        #if 'CefV8Value.HasValue' == (cls.get_cefglue_name() + '.' + func.get_name()):
        #    sys.stdout.write( format_comment_cefglue(func.get_comment(), indent) )

        result += indent + '/* FIXME: ' + cls.get_cefglue_name() + '.' + func.get_name() + ' public */\n'
        result += indent + parts['retval'] + ' ' + func.get_name() + '(' + string.join(parts['args'][1:], ', ') + ')\n';
        result += indent + '{\n';
        result += indent + '    // TODO: ' + cls.get_cefglue_name() + '.' + func.get_name() + '\n';
        result += indent + '    ' + 'throw new NotImplementedException();\n';
        result += indent + '}\n';
        result += '\n'

    result += """
    }
}
""";

    write_cefglue_file(dir, filename, result);

    return

def make_cefglue_structlayout():
    result = '[StructLayout(LayoutKind.Sequential, Pack = NativeMethods.CefStructPack)]'
    return result

def make_cefglue_struct_suppressmessage():
    result = '[SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]';
    return result

def make_cefglue_unmanaged_function_pointer():
	result = '[UnmanagedFunctionPointer(NativeMethods.CefCallback), SuppressUnmanagedCodeSecurity]'
	return result

def make_struct_decl():
	result = 'internal unsafe partial struct'
	return result

def make_nativemethods_decl():
	result = 'internal static unsafe partial class NativeMethods'
	return result

def format_comment_cefglue(comment, indent, translate_map = None, maxchars = 80):
	result = format_comment(comment, indent, translate_map, maxchars)
	result = string.replace(result, "&", "&amp;");
	result = string.replace(result, "<", "&lt;");
	result = string.replace(result, ">", "&gt;");
	result = re.compile("^" + indent + "\/\/(?!\/)", re.MULTILINE).sub("{=__cefglue_placeholder_1}", result)
	result = string.replace(result, "///\n", "/// <summary>\n", 1)
	result = string.replace(result, "///\n", "/// </summary>\n", 1)
	result = string.replace(result, "{=__cefglue_placeholder_1}", indent + "///")
	return result

def make_cefglue_global_funcs(funcs, defined_names, translate_map, indent):
    result = ''
    first = True
    for func in funcs:
        comment = func.get_comment()
        if first or len(comment) > 0:
            result += '\n'+format_comment_cefglue(comment, indent, translate_map);
        if func.get_retval().get_type().is_result_string():
            result += indent + '/// <remarks>\n' + indent + '/// The resulting string must be freed by calling cef_string_userfree_free().\n' + indent + '/// </remarks>\n'

        parts = func.get_cefglue_parts()
        # result += indent + "[DllImport(CefDllName, EntryPoint = \"" + parts['name'] + "\", CallingConvention = CefCall)]\n";
        result += indent + "[DllImport(CefDllName, CallingConvention = CefCall)]\n";
        result += indent + "public static extern " + func.get_cefglue_proto(defined_names) + ";\n"

        #result += wrap_code(indent+'CEF_EXPORT '+
        #                    func.get_capi_proto(defined_names)+';')

        if first:
            first = False
    return result

def make_cefglue_member_funcs(funcs, defined_names, translate_map, make_method_invoke, indent):
    result = ''

    # cef_base_t functions
    if make_method_invoke:
        result += make_cefglue_method_invoke(0, 'add_ref',   'int', 'cef_base_t* self', 'self', 'cef_base_t.add_ref_delegate',   '((cef_base_t*)self)->add_ref',   indent);
        result += make_cefglue_method_invoke(1, 'release',   'int', 'cef_base_t* self', 'self', 'cef_base_t.release_delegate',   '((cef_base_t*)self)->release',   indent);
        result += make_cefglue_method_invoke(2, 'get_refct', 'int', 'cef_base_t* self', 'self', 'cef_base_t.get_refct_delegate', '((cef_base_t*)self)->get_refct', indent);

    first = True
    method_index = 3;
    for func in funcs:
        comment = func.get_comment()

        if first or len(comment) > 0:
            result += '\n' + format_comment_cefglue(comment, indent, translate_map)

        if func.get_retval().get_type().is_result_string():
            result += indent + '/// <remarks>\n' + indent + '/// The resulting string must be freed by calling cef_string_userfree_free().\n' + indent + '/// </remarks>\n'

        parts = func.get_cefglue_parts()
        result += indent + 'public IntPtr ' + parts['name'] + ';\n'

        method_delegate_type = parts['name'] + '_delegate';

        result += indent + make_cefglue_unmanaged_function_pointer() + '\n'
        result += indent + 'public delegate ' + parts['retval'] + ' ' + method_delegate_type + \
                  '(' + string.join(parts['args'], ', ') + ');\n'

        if make_method_invoke:
            method_name       = parts['name'];                                 # method name
            method_ret_type   = parts['retval'];                               # return type
            method_args_decl  = string.join(parts['args'], ', ');              # arguments
            ptr_expr = 'self->' + method_name;

            method_args_ref_list = [];
            for arg in parts['args']:
                argparts = arg.split(' ');
                argparts.reverse();
                method_args_ref_list.append( argparts[0] );

            method_args_ref   = string.join( method_args_ref_list, ', ');  # arguments

            result += make_cefglue_method_invoke(method_index, method_name, method_ret_type, method_args_decl, method_args_ref, method_delegate_type, ptr_expr, indent);


        #result += wrap_code(indent+parts['retval']+' (CEF_CALLBACK *'+
        #                    parts['name']+')('+
        #                    string.join(parts['args'], ', ')+');')
        
        method_index = method_index + 1;

        if first:
            first = False

    return result

def make_cefglue_method_invoke(method_index, method_name, method_ret_type, method_args_decl, method_args_ref, method_delegate_type, ptr_expr, indent):
    method_slot = format(method_index, "X");
    bs_ptr_0 = 's_bp' + method_slot;
    bs_delegate_0 = 's_bd' + method_slot;

    result = '';
    result += '\n';

    result += indent + 'private static IntPtr ' + bs_ptr_0 + ';\n';
    result += indent + 'private static ' + method_delegate_type + ' ' + bs_delegate_0 + ';\n'

    result += '\n';
    result += indent + '// method slot: ' + method_slot + '\n';
    result += indent + 'public static ' + method_ret_type + ' invoke_' + method_name + '(' + method_args_decl + ')\n';
    result += indent + '{\n';
    result += indent + '    ' + method_delegate_type + ' mdelegate;\n';
    result += '\n';
    result += indent + '    var mptr = ' + ptr_expr + ';\n';
    result += indent + '    if (mptr == ' + bs_ptr_0 + ')\n';
    result += indent + '    {\n';
    result += indent + '        mdelegate = ' + bs_delegate_0 + ';\n';
    result += indent + '    }\n';
    result += indent + '    else\n';
    result += indent + '    {\n';
    result += indent + '        mdelegate = (' + method_delegate_type + ')Marshal.GetDelegateForFunctionPointer(mptr, typeof(' + method_delegate_type + '));\n';
    result += indent + '        if (' + bs_ptr_0 + ' == IntPtr.Zero)\n'
    result += indent + '        {\n'
    result += indent + '            ' + bs_delegate_0 + ' = mdelegate;\n'
    result += indent + '            ' + bs_ptr_0 + ' = mptr;\n'
    result += indent + '        }\n'
    result += indent + '    }\n'
    result += '\n';

    result += indent + '    ';
    if method_ret_type != 'void':
        result += 'return ';
        
    result += 'mdelegate(' + method_args_ref + ');\n';

    result += indent + '}\n';
    result += '\n';
    return result

def make_cefglue(header, cefgluedir):
    # structure names that have already been defined
    defined_names = header.get_defined_structs()
    
    # map of strings that will be changed in C++ comments
    translate_map = header.get_capi_translations()
    
    # header string
    result = \
"""//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace CefGlue.Interop
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

#if !DEBUG
    [SuppressUnmanagedCodeSecurity]
#endif
    """ + make_nativemethods_decl() + """
    {
        internal const int CefStructPack = 0;

        internal const CallingConvention CefCall = CallingConvention.Cdecl;

        internal const CallingConvention CefCallback
#if WINDOWS
            = CallingConvention.StdCall;
#else
            = CallingConvention.Cdecl;
#endif

        internal const string CefDllName = "libcef.dll";

"""

    # output global functions
    result += make_cefglue_global_funcs(header.get_funcs(), defined_names,
                                     translate_map, '        ')

    result += \
"""
    }

"""

    # output classes
    classes = header.get_classes()
    for cls in classes:
        # virtual functions are inside the structure
        classname = cls.get_capi_name()
        result += '\n' + format_comment_cefglue(cls.get_comment(), '    ', translate_map);
        result += '    ' + make_cefglue_structlayout() + '\n'
        result += '    ' + make_cefglue_struct_suppressmessage() + '\n'
        result += '    ' + make_struct_decl() + ' ' + classname + \
                  '\n    {\n        /// <summary>\n        /// Base structure.\n        /// </summary>\n        public cef_base_t @base;\n'
        funcs = cls.get_virtual_funcs()
        result += make_cefglue_member_funcs(funcs, defined_names, translate_map,
                                         not is_handler_class(cls), # make method invoke only to proxies
                                         '        ')
        result += '\n    };\n\n'

        defined_names.append(cls.get_capi_name())

        # static functions become global
        funcs = cls.get_static_funcs()
        if len(funcs) > 0:
            result += '    ' + make_nativemethods_decl() + '\n'
            result += '    ' + '{\n'
            result += make_cefglue_global_funcs(funcs, defined_names,
                                             translate_map, '        ')+'\n'
            result += '    ' + '}\n'

    # footer string
    result += \
"""
}

"""

    make_cefglue_handlerschema(classes, cefgluedir)
    make_cefglue_proxyschema(classes, cefgluedir)

    return result


def write_cefglue(header, filename, cefgluedir, backup):
    filedir = cefgluedir;
    file = filedir + '/NativeMethods.g.cs'
    #file = filedir + '/' + os.path.basename(filename) + '.g.cs'

    if not os.path.isdir(filedir):
        os.makedirs(filedir);

    if path_exists(file):
        oldcontents = read_file(file)
    else:
        oldcontents = ''

    newcontents = make_cefglue(header, cefgluedir)
    if newcontents != oldcontents:
        if backup and oldcontents != '':
            backup_file(file)
        write_file(file, newcontents)
        return True

    return False

def write_cefglue_file(dir, file, contents):
    if not os.path.isdir(dir):
        os.makedirs(dir);

    #sys.stdout.write(file + "... ");
    file = dir + "/" + file;

    if path_exists(file):
        oldcontents = read_file(file)
    else:
        oldcontents = ''

    if contents != oldcontents:
        write_file(file, contents)
        #sys.stdout.write("updated.\n");
        return True

    #sys.stdout.write("up-to-date.\n");
    return False


# test the module
if __name__ == "__main__":
    import sys

    # verify that the correct number of command-line arguments are provided
    if len(sys.argv) < 2:
        sys.stderr.write('Usage: '+sys.argv[0]+' <infile>')
        sys.exit()

    # create the header object
    header = obj_header(sys.argv[1])

    # dump the result to stdout
    sys.stdout.write(make_cefglue_header(header))
