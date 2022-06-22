using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmokeDTOs;
using SmokeInterfaces;
using SmokeLogic;
using SmokeTest.MockDAL;
using System.Collections.Generic;

namespace SmokeTest
{
    [TestClass]
    public class PropertyTest
    {

        private IPropertyDAL _propMockDAL;
        private PropertyCollection propColl = new PropertyCollection();
        private PropertyHandler propHandler = new PropertyHandler();
        private List<Property> propList;
        private PropertyMockDAL propMockDAL = new PropertyMockDAL();

        PropertyDTO propDTO;
        Property prop;


        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IPropertyDAL, PropertyMockDAL>();

            var serviceProvider = services.BuildServiceProvider();

            _propMockDAL = serviceProvider.GetService<IPropertyDAL>();

            propDTO = new PropertyDTO { gameId = 1, userId = 1, parentId = null, name = "Name", value = "Name", type = null };
            prop = new Property(propDTO);

            propMockDAL = new PropertyMockDAL();
            propHandler = new PropertyHandler(_propMockDAL);
            propColl = new PropertyCollection(_propMockDAL);
            propList = new List<Property>();
        }

        [TestMethod]
        public void WhileEmptyDAL()
        {
            //Arrange
            for (int i = 0; i <= propMockDAL.GetAll().Count; i++)
            {
                propMockDAL.Delete(i);
            }

            //Act

            //Assert
            Assert.AreEqual(0, propMockDAL.GetAll().Count);
            propColl.Add(new Property(propDTO));
            Assert.AreEqual(1, propMockDAL.GetAll().Count);

        }
        [TestMethod]
        public void GetPropertiesTest()
        {
            //Arrange
            for (int i = 0; i <= propMockDAL.GetAll().Count; i++)
            {
                propMockDAL.Delete(i);
            }

            //Act
            propColl.Add(new Property(propDTO));

            //Assert
            Assert.AreEqual(1, propMockDAL.GetAll().Count);
        }
        [TestMethod]
        public void UpdateProperty()
        {
            //Arrange
            for (int i = 0; i <= propMockDAL.GetAll().Count; i++)
            {
                propMockDAL.Delete(i);
            }
            propColl.Add(new Property(propDTO));


            //Act

            //Assert
            Assert.IsTrue(propMockDAL.GetAll()[0].name == "Name");
            propHandler.Update(1, 1, "new", "Name", null);
            Assert.IsTrue(propMockDAL.GetAll()[0].name == "new");
        }
        [TestMethod]
        public void CheckSingleDetails()
        {
            //Arrange
            for (int i = 0; i <= propMockDAL.GetAll().Count; i++)
            {
                propMockDAL.Delete(i);
            }
            
            //Act
            propColl.Add(new Property(propDTO));
            List<Property> props = propHandler.GetDetails(1, null);

            //Assert
            foreach(Property prop in props)
            {
                Assert.IsTrue(prop.name == propHandler.GetDetails(1, null)[0].name);
            }
        }
        [TestMethod]
        public void CheckChildDetails()
        {
            //Arrange
            for (int i = 0; i <= propMockDAL.GetAll().Count; i++)
            {
                propMockDAL.Delete(i);
            }
            int count = 0;
            PropertyDTO p1 = propDTO;
            PropertyDTO p2 = new PropertyDTO { Id = 2, parentId = 1 , name = "child"};

            //Act
            propColl.Add(new Property(p1));
            propColl.Add(new Property(p2));
            List<Property> props = propHandler.GetDetails(1, 1);

            //Assert
            foreach (Property prop in props)
            {
                Assert.IsTrue(prop.name == propHandler.GetDetails(1, 1)[count].name);
                count++;
            }
        }
    }
}
