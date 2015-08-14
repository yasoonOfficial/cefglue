namespace CefGlue.UnitTests
{
    using System;
    using CefGlue;
    using NUnit.Framework;

    [TestFixture]
    public class CefStringListTests
    {
        [Test]
        public void Test1()
        {
            var list = new CefStringList();

            list.Add("String 1");
            list.Add("String 2");
            list.Add("String 3");

            Assert.That(list.Count, Is.EqualTo(3));

            Assert.That(list[0], Is.EqualTo("String 1"));
            Assert.That(list[1], Is.EqualTo("String 2"));
            Assert.That(list[2], Is.EqualTo("String 3"));

            var list2 = list.Clone();
            list.Clear();

            Assert.That(list.Count, Is.EqualTo(0));
            // TODO: list.Dispose();


            Assert.That(list2.Count, Is.EqualTo(3));
            Assert.That(list2[0], Is.EqualTo("String 1"));
            Assert.That(list2[1], Is.EqualTo("String 2"));
            Assert.That(list2[2], Is.EqualTo("String 3"));

            // TODO: list2.Dispose();
        }
    }
}

