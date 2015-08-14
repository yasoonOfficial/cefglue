namespace CefGlue.UnitTests
{
    using System;
    using CefGlue;
    using NUnit.Framework;

    [TestFixture]
    public class CefStringMapTests
    {
        [Test]
        public void Test1()
        {
            var map = new CefStringMap();

            map.Append("Key 1", "String 1");
            map.Append("Key 2", "String 2");
            map.Append("Key 3", "String 3");

            Assert.That(map.Count, Is.EqualTo(3));

            // TODO:
            /*
            Assert.That(map.GetKey(0), Is.EqualTo("Key 1"));
            Assert.That(map.GetValue(0), Is.EqualTo("String 1"));

            Assert.That(map.GetKey(1), Is.EqualTo("Key 2"));
            Assert.That(map.GetValue(1), Is.EqualTo("String 2"));

            Assert.That(map.GetKey(2), Is.EqualTo("Key 3"));
            Assert.That(map.GetValue(2), Is.EqualTo("String 3"));
            */

            Assert.That(map.GetValue("Key 2"), Is.EqualTo("String 2"));

            map.Clear();
            Assert.That(map.Count, Is.EqualTo(0));

            // TODO: map.Dispose();
        }
    }
}
