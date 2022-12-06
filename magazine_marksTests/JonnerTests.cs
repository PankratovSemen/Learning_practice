using Microsoft.VisualStudio.TestTools.UnitTesting;
using magazine_marks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magazine_marks.Tests
{
    [TestClass()]
    public class JonnerTests
    {
        Jonner jonner = new Jonner();
        [TestMethod()]
        public void loginingTest()
        {
            
            string log = "admin";
            string pass = "12345";
            bool res;
            if (jonner.logining(log, pass) == "OK")
            {
                res = true;
                Assert.IsTrue(res);
            }
        }

        [TestMethod()]
        public void rolesTest()
        {
            if(jonner.roles() != null)
            {
                Assert.IsNotNull(jonner.roles());
            }
        }

        [TestMethod()]
        public void subjectsTest()
        {
            if (jonner.subjects() != null)
            {
                Assert.IsNotNull(jonner.subjects());
            }
        }

        [TestMethod()]
        public void usersTest()
        {
            if (jonner.users() != null)
            {
                Assert.IsNotNull(jonner.users());
            }
        }

       
    }
}