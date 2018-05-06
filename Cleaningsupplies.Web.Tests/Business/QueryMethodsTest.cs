using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cleaningsupplies.Business.Classes;
using CleaningSupplies.Database.Models;
using System.Linq;

namespace Cleaningsupplies.Web.Tests.Business
{
    [TestClass]
    public class QueryMethodsTest
    {

        [TestMethod]
        public void GetProdInvSum()
        {
            //Arrange
            ApplicationDbContext db = new ApplicationDbContext();
            int expected = (from p in db.Master
                            where (p.ID == 2)
                            join pinv in db.Usage on p.ID equals pinv.GetMasterT.ID
                            orderby p.Description
                            select pinv).ToList().Select(pinv => pinv.Quantity_modified).Sum();

            //Act
            int actual = QueryMethods.GetProdInvSum(2);

            //Assert
            Assert.AreEqual(expected, expected);
        }

        [TestMethod]
        public void GetMaster() 
        {
            //Arrange
            ApplicationDbContext db = new ApplicationDbContext();
            Master expected = db.Master.Where(x => x.ID == 2).FirstOrDefault(); ;

            //Act
            Master actual = QueryMethods.GetMaster(2);

            //Assert
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void GetAllMasters()
        {
            //Arrange
            ApplicationDbContext db = new ApplicationDbContext();
            List<Master> expected = db.Master.ToList();

            //Act
            List<Master> actual = QueryMethods.GetAllMasters();

            //Assert
            Assert.ReferenceEquals(expected, actual);
        }
    }
}
