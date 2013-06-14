using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SCv20_Tools.Core.Services;

namespace SCv20_Tools.Core.Tests {
    [TestFixture]
    public class DataServiceSceneTests {
        private DataService _ds;

        [SetUp]
        public void init() {
            this._ds = DataService.GetInstance();
        }

        [Test]
        public void change_objetive_order() {
            var O2 = _ds.SaveSceneObjectiveOrder(1, 2, +1);
            var O3 = _ds.GetSceneObjective(3);

            Assert.AreEqual(3, O2.Order);
            Assert.AreEqual(2, O3.Order);
        }
    }
}
