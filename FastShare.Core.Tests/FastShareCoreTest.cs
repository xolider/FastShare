using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastShare.Core.Tests
{
    [TestClass]
    public class FastShareCoreTest
    {

        [TestMethod]
        public async Task TestReceiveWithoutCodeThrowsException()
        {
            await Assert.ThrowsExceptionAsync<Exception>(() =>
            {
                return FastShareCore.Instance.ReceiveFile();
            });
        }
    }
}
