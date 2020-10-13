using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class SmsSendingTests
    {
        private readonly SmsSending mainSms = new SmsSending("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;

            Settings.apiPaths["sending_create"] = "/6c6f4396-ace0-4866-9b8c-ca11daea4eef";
            Settings.apiPaths["sending_status"] = "/f34d8276-8eeb-4c10-961f-e87baadad895";
        }

        [Test]
        public void createSendingTest()
        {
            SendingInfo sendingInfo = new SendingInfo();
            sendingInfo.include = "141606";
            sendingInfo.exclude = "141607";
            sendingInfo.sender = "mainsms";
            sendingInfo.message = "test_api_sending";

            ResponseSendingCreate responseSendingCreate = mainSms.createSending(sendingInfo);

            Assert.AreEqual("success", responseSendingCreate.status);
            Assert.AreEqual("166937", responseSendingCreate.id);
            Assert.AreEqual("2.64", responseSendingCreate.cost);
            Assert.AreEqual("1", responseSendingCreate.parts);
            Assert.AreEqual("1", responseSendingCreate.contacts);
            Assert.AreEqual("141606", responseSendingCreate.include);
            Assert.AreEqual("141607,13555", responseSendingCreate.exclude);
            Assert.AreEqual("Api Рассылка 2020-10-13", responseSendingCreate.name);
        }

        [Test]
        public void SendingStatusTest()
        {
            ResponseSendingStatus responseSendingStatus = mainSms.sendingStatus("166937");

            Assert.AreEqual("success", responseSendingStatus.status);
            Assert.AreEqual("166937", responseSendingStatus.id);
            Assert.AreEqual("1", responseSendingStatus.total);
            Assert.AreEqual("1", responseSendingStatus.delivered);
            Assert.AreEqual("0", responseSendingStatus.undelivered);
            Assert.AreEqual("0", responseSendingStatus.indelivered);
        }
    }
}