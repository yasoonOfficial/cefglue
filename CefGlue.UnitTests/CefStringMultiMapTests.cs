namespace CefGlue.UnitTests
{
    using System;
    using CefGlue;
    using NUnit.Framework;

    [TestFixture]
    public class CefStringMultiMapTests
    {
        [Test]
        public void Test1()
        {
            var map = new CefStringMultiMap();

            map.Append("Key 1", "String 1");
            map.Append("Key 2", "String 2");
            map.Append("Key 2", "String 2.1");
            map.Append("Key 3", "String 3");

            Assert.That(map.Count, Is.EqualTo(4));

            // TODO:

            map.Clear();
            Assert.That(map.Count, Is.EqualTo(0));

            // TODO: map.Dispose();
        }
    }
}
