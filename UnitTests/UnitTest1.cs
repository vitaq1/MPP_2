using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        Faker.Faker faker = new Faker.Faker();
        static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        [TestInitialize]
        public void Initialize()
        {
            var logConfig = new NLog.Config.LoggingConfiguration();
            var logFile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
            logConfig.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logFile);
            NLog.LogManager.Configuration = logConfig;

        }
        class SimpleFieldClass
        {
            public int intValue;
            public double doubleValue;
            public bool boolValue;
        }

        class SimplePropertyClass
        {
            public int intValue { get; set; }
            public long longValue { get; set; }
            public float floatValue { get; set; }
        }
        class ListClass
        {
            public int intValue;
            public List<int> list;
            public bool boolValue { get; set; }
        }
        class ClassWithPrivateFields
        {
            public int intValue;
            private double doubleValue;
            public int anotherInt { get; set; }
            public double anotherDouble { get;  private set; }
        }
        class ClassWithConstructor
        {
            public int intValue;
            public long longValue { get; set; }
            public ClassWithConstructor(int i)
            {
                intValue = i;
                longValue = 15;
            }
        }
        class ClassWithManyConstructors
        {
            public int intValue;
            public bool boolValue;
            public long longValue { get; set; }
            public ClassWithManyConstructors(int i)
            {
                intValue = i;
            }
            public ClassWithManyConstructors(long l, bool b)
            {
                longValue = l;
                boolValue = b;
                intValue = 17;
            }
        }
        class ClassWithString
        {
            public string stringValue;
            public string anotherStringValue { get; set; }
        }
        class ClassWithChar
        {
            public char charValue;
            public char anotherCharValue { get; set; }
        }
        class DTO1
        {
            public int intValue;
            public DTO2 dto2;
        }
        class DTO2
        {
            public string stringValue;
            public char charValue { get; set; }
        }
        class FirstClass
        {
            public SecondClass obj;
        }
        class SecondClass
        {
            public FirstClass obj;
        }
        [TestMethod]
        public void PublicFieldTest()
        {
            SimpleFieldClass obj = faker.Create<SimpleFieldClass>();
            try
            {
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.doubleValue);
                Assert.AreEqual(obj.doubleValue.GetType(), typeof(double));
                Assert.IsNotNull(obj.boolValue);
                Assert.AreEqual(obj.boolValue.GetType(), typeof(bool));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(PublicFieldTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(PublicFieldTest) }  test passed.");
        }
        [TestMethod]
        public void PublicPropertyTest()
        {
            SimplePropertyClass obj = faker.Create<SimplePropertyClass>();
            try
            {
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.longValue);
                Assert.AreEqual(obj.longValue.GetType(), typeof(long));
                Assert.IsNotNull(obj.floatValue);
                Assert.AreEqual(obj.floatValue.GetType(), typeof(float));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(PublicPropertyTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(PublicPropertyTest) }  test passed.");
        }
        [TestMethod]
        public void ListTest()
        {
            ListClass obj = faker.Create<ListClass>();
            try
            {
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.boolValue);
                Assert.AreEqual(obj.boolValue.GetType(), typeof(bool));
                Assert.IsNotNull(obj.list);
                Assert.AreEqual(obj.list.GetType(), typeof(List<int>));

                foreach (var elem in obj.list)
                {
                    Assert.IsNotNull(elem);
                    Assert.AreEqual(elem.GetType(), typeof(int));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(ListTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(ListTest) }  test passed.");
        }
        [TestMethod]
        public void PrivateFieldsAndPropertiesTest()
        {
            try
            {
                ClassWithPrivateFields obj = faker.Create<ClassWithPrivateFields>();
                Assert.IsNotNull(obj);
                int counter = 0;
                foreach (var prop in obj.GetType().GetProperties())
                {
                    if (prop != null)
                    {
                        counter++;
                    }
                }
                Assert.AreEqual(2, counter);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(PrivateFieldsAndPropertiesTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(PrivateFieldsAndPropertiesTest) }  test passed.");
        }
        [TestMethod]
        public void StringPluginTest()
        {
            ClassWithString obj = faker.Create<ClassWithString>();
            try
            {
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.stringValue);
                Assert.AreEqual(obj.stringValue.GetType(), typeof(string));
                Assert.IsNotNull(obj.anotherStringValue);
                Assert.AreEqual(obj.anotherStringValue.GetType(), typeof(string));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(StringPluginTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(StringPluginTest) }  test passed.");
        }
        [TestMethod]
        public void CharPluginTest()
        {
            ClassWithChar obj = faker.Create<ClassWithChar>();
            try
            {
                Assert.IsNotNull(obj);
                Assert.IsNotNull(obj.charValue);
                Assert.AreEqual(obj.charValue.GetType(), typeof(char));
                Assert.IsNotNull(obj.anotherCharValue);
                Assert.AreEqual(obj.anotherCharValue.GetType(), typeof(char));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(CharPluginTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(CharPluginTest) }  test passed.");
        }
        [TestMethod]
        public void ConstructorTest()
        {
            try
            {
                ClassWithConstructor obj = faker.Create<ClassWithConstructor>();
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.longValue);
                Assert.AreEqual(obj.longValue.GetType(), typeof(long));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(ConstructorTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(ConstructorTest) }  test passed.");
        }
        [TestMethod]
        public void ManyConstructorTest()
        {
            try
            {
                ClassWithManyConstructors obj = faker.Create<ClassWithManyConstructors>();
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.longValue);
                Assert.AreEqual(obj.longValue.GetType(), typeof(long));
                Assert.IsNotNull(obj.boolValue);
                Assert.AreEqual(obj.boolValue.GetType(), typeof(bool));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(ManyConstructorTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(ManyConstructorTest) }  test passed.");
        }
        [TestMethod]
        public void DependencyTest()
        {
            DTO1 obj = faker.Create<DTO1>();
            try
            {
                Assert.IsNotNull(obj.intValue);
                Assert.AreEqual(obj.intValue.GetType(), typeof(int));
                Assert.IsNotNull(obj.dto2);
                Assert.AreEqual(obj.dto2.GetType(), typeof(DTO2));
                Assert.IsNotNull(obj.dto2.charValue);
                Assert.AreEqual(obj.dto2.charValue.GetType(), typeof(char));
                Assert.IsNotNull(obj.dto2.stringValue);
                Assert.AreEqual(obj.dto2.stringValue.GetType(), typeof(string));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{ nameof(DependencyTest) }  test failed.");
                Assert.Fail();
            }
            logger.Info($"{ nameof(DependencyTest) }  test passed.");
        }

        [TestMethod]
        public void ExceptionTest()
        {
            try
            {
                FirstClass obj = faker.Create<FirstClass>();
                Assert.Fail();
            }
            catch(Exception ex)
            {
                logger.Info($"{ nameof(ExceptionTest) }  test passed with exception ${ex.Message}.");
            }
        }
    }
}
