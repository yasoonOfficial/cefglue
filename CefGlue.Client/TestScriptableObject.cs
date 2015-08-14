namespace CefGlue.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using CefGlue.JSBinding;

    public interface IJSObject
    {
        bool EchoBoolean(bool value);
        string ReadWriteProperty { get; set; }
    }


    [CLSCompliant(false)]
    public sealed class TestScriptableObject : IJSObject
    {
        public void EchoVoid() { }

        public bool EchoBoolean(bool value) { return value; }
        public int EchoInt32(int value) { return value; }
        public double EchoDouble(double value) { return value; }
        public string EchoString(string value) { return value; }
        public DateTime EchoDateTime(DateTime value) { return value; }

        public byte EchoByte(byte value) { return value; }
        public sbyte EchoSByte(sbyte value) { return value; }
        public short EchoInt16(short value) { return value; }
        public ushort EchoUInt16(ushort value) { return value; }
        public char EchoChar(char value) { return value; }
        public float EchoSingle(float value) { return value; }

        public bool? EchoNullableBoolean(bool? value) { return value; }
        public int? EchoNullableInt32(int? value) { return value; }
        public double? EchoNullableDouble(double? value) { return value; }
        public DateTime? EchoNullableDateTime(DateTime? value) { return value; }

        public byte? EchoNullableByte(byte? value) { return value; }
        public sbyte? EchoNullableSByte(sbyte? value) { return value; }
        public short? EchoNullableInt16(short? value) { return value; }
        public ushort? EchoNullableUInt16(ushort? value) { return value; }
        public char? EchoNullableChar(char? value) { return value; }
        public float? EchoNullableSingle(float? value) { return value; }

        public object EchoObject(object value) { return value; }


        public void ArgumentCount0() { }
        public void ArgumentCount1(int arg1) { }
        public void ArgumentCount2(int arg1, int arg2) { }

        public string ReadOnlyProperty { get { return "value"; } }
        public string ReadWriteProperty { get; set; }
        public string ThrowingProperty
        {
            get { throw new InvalidOperationException("I'm throwing getter."); }
            set { throw new InvalidOperationException("I'm throwing setter."); }
        }


        public bool EchoOptBoolean(bool value = true) { return value; }
        public char EchoOptChar(char value = 'a') { return value; }
        public sbyte EchoOptSByte(sbyte value = -128) { return value; }
        public byte EchoOptByte(byte value = 255) { return value; }
        public short EchoOptInt16(short value = -32768) { return value; }
        public ushort EchoOptUInt16(ushort value = 65535) { return value; }
        public int EchoOptInt32(int value = -2147483648) { return value; }
        public string EchoOptString(string value = "value") { return value; }


        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public string Overload()
        {
            return "#1";
        }

        public string Overload(int arg1)
        {
            return "#2";
        }

        public string Overload(int arg1, int arg2)
        {
            return "#3";
        }

        // TODO: this must be hidden automatically, 'cause we have more priority signature (int arg1)
        [JSBind(false)]
        public string Overload(short arg1)
        {
            return "#4";
        }

        // TODO: overload by type variance
        [JSBind(false)]
        public string Overload(int arg1, string arg2)
        {
            return "#4";
        }

    }
}
