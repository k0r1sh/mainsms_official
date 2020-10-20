using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class SmsBatchTests
    {
        private readonly SmsBatch mainSms = new SmsBatch("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;

            Settings.apiPaths["batch_send"] = "/25307cbc-6e04-4c00-a179-89ec78dea3c8";
        }

        [Test]
        public void sendBatchTest()
        {
            BatchMessagesList batchMessagesList = new BatchMessagesList();
            batchMessagesList.addMessage("+79609701234", "test message1");
            batchMessagesList.addMessage("+041235", "asd");

            ResponseBatchSend responseBatchSend = mainSms.sendBatch(batchMessagesList);

            Assert.AreEqual("success", responseBatchSend.status);
            Assert.AreEqual("4208", responseBatchSend.id);
            Assert.AreEqual("2.57", responseBatchSend.cost);
            Assert.AreEqual("1", responseBatchSend.phones);
            Assert.AreEqual("1", responseBatchSend.parts);

            Assert.AreEqual("Номер получателя не задан", responseBatchSend.errors["2"]);
            Assert.AreEqual("Номер получателя не задан", responseBatchSend.errors["3"]);
        }
    }
}