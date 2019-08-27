using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Curnow.Biz.What3WordsV3Net;
using Curnow.Biz.What3WordsV3Net.Enums;
using Curnow.Biz.What3WordsV3Net.Models;

namespace What3WordsV3Net.Tests
{
    [TestClass]
    public class What3WordsV3NetTests
    {
        [TestMethod]
        public void TestConvertTo3WA()
        {
            W3WClient w3w = new W3WClient("<<your api key>>");
            AddressResponse response = w3w.ConvertTo3WA(51.520847, -0.19552);
            Assert.AreEqual(response.words, "filled.count.soap");
        }

        /// <summary>
        /// test converting 3 word address to lat,lng
        /// </summary>
        [TestMethod]
        public void TestConvertCoordinates()
        {
            W3WClient w3w = new W3WClient("<<your api key>>");
            AddressResponse response = w3w.ConvertToCoordinates("pinch", "veal", "sector");
            Assert.AreEqual(response.coordinates.lat, 50.143071);
            Assert.AreEqual(response.coordinates.lng, -5.091856);
        }
    }
}
