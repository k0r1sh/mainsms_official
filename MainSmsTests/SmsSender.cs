using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class SmsSenderTests
    {
        private readonly SmsSender mainSms = new SmsSender("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;

            Settings.apiPaths["sender_create"] = "/228d0227-afb1-4a7a-9c27-7b8d3c5fe761";
            Settings.apiPaths["sender_remove"] = "/c628e552-e29e-4624-a719-7e6b937bf1e6";
            Settings.apiPaths["sender_list"] = "/d07804dd-6cbd-4f64-ac49-315eca116d90";
            Settings.apiPaths["sender_default"] = "/50cb2aa8-b872-4c49-be4b-d2e296ba0c09";
            Settings.apiPaths["sender_set"] = "/6d37bb9f-2f8c-422f-9ea0-035cfe3d7035";
        }

        [Test]
        public void createSenderTest()
        {
            ResponseSenderCreate responseSenderCreate = mainSms.createSender("api_test");

            Assert.AreEqual("success", responseSenderCreate.status);
            Assert.AreEqual("api_test", responseSenderCreate.sender);
        }

        [Test]
        public void removeSenderTest()
        {
            ResponseSenderRemove responseSenderRemove = mainSms.removeSender("api_test");

            Assert.AreEqual("success", responseSenderRemove.status);
            Assert.AreEqual("api_test", responseSenderRemove.sender);
        }

        [Test]
        public void listSenderTest()
        {
            ResponseSenderList responseSenderList = mainSms.listSender();

            Assert.AreEqual("success", responseSenderList.status);
            Assert.AreEqual(3, responseSenderList.senders.Count);
            CollectionAssert.Contains(responseSenderList.senders, "apitest");
        }

        [Test]
        public void defaultSenderTest()
        {
            ResponseSenderDefault responseSenderDefault = mainSms.defaultSender();
            Assert.AreEqual("success", responseSenderDefault.status);
            Assert.AreEqual("maintest", responseSenderDefault.sender);
        }

        [Test]
        public void setSenderTest()
        {
            ResponseSenderSet responseSenderSet = mainSms.setSender("maintest");
            Assert.AreEqual("success", responseSenderSet.status);
            Assert.AreEqual("maintest", responseSenderSet.sender);
        }

    }
}