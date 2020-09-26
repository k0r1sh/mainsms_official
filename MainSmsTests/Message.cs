using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class MessageTests
    {
        private readonly Sms mainSms = new Sms("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;
            
            Settings.apiPaths["message_balance"] = "/56c01e8d-f652-4cc8-954f-8fdf8faa1f43";
            Settings.apiPaths["message_price"] = "/9bffb66f-ecc0-4b99-b86f-5d1c0df07d2a";
            Settings.apiPaths["message_send"] = "/f7fd70c6-bc76-4645-a900-8b365bb68df9";
            Settings.apiPaths["message_status"] = "/f7c6c742-7ead-47da-bb07-fd8d7cd0d263";
            Settings.apiPaths["message_cancel"] = "/6e784282-ff0b-4df5-9e6b-52588053837d";
            Settings.apiPaths["message_info"] = "/6203bd46-2ac3-4967-b403-507a6e268d35";
        }

        [Test]
        public void GetBalanceTest()
        {
            ResponseBalance responseBalance = mainSms.getBalance();

            Assert.AreEqual("success", responseBalance.status);
            Assert.AreEqual("13008,92", responseBalance.balance);
        }

        [Test]
        public void GetMessagePriceTest()
        {
            ResponsePrice responsePrice = mainSms.getMessagesPrice("mainsms", "79609708097, 79545551232", "run at 23:00 olo+ + lo");

            Assert.AreEqual("success", responsePrice.status);
            Assert.AreEqual("13008,9", responsePrice.balance);
            Assert.AreEqual("0,02", responsePrice.price);
            Assert.AreEqual("1", responsePrice.parts);
            Assert.AreEqual("2", responsePrice.count);
            CollectionAssert.Contains(responsePrice.recipients, "79609708097");
            CollectionAssert.Contains(responsePrice.recipients, "79545551232");
        }

        public void SendSmsTest()
        {
            ResponseSend responseSend = mainSms.sendSms("mainsms", "79609708491", "test2 message +", DateTime.Parse("25.09.2020 23:00"));

            Assert.AreEqual("success", responseSend.status);
            Assert.AreEqual("13008,91", responseSend.balance);
            Assert.AreEqual("0,01", responseSend.price);
            Assert.AreEqual("1", responseSend.parts);
            Assert.AreEqual("1", responseSend.count);
            CollectionAssert.Contains(responseSend.recipients, "79609708491");
            CollectionAssert.Contains(responseSend.message_ids, "1415");
        }

        public void GetMessageStatusTest()
        {
            ResponseStatus responseStatus = mainSms.getMessagesStatus("1415");

            Assert.AreEqual("success", responseStatus.status);
            CollectionAssert.Contains(responseStatus.messages["1415"], "delivered");
            CollectionAssert.Contains(responseStatus.channels["1415"], "sms");
        }

        public void CancelMessagesTest()
        {
            ResponseCancel responseCancel = mainSms.cancelMessages("1515");

            Assert.AreEqual("success", responseCancel.status);
            CollectionAssert.Contains(responseCancel.messages["1515"], "canceled");
        }

        public void GerPhoneInfo()
        {
            ResponseInfo responseInfo = mainSms.getPhonesInfo("79138857567");
            Assert.AreEqual("success", responseInfo.status);
            CollectionAssert.Contains(responseInfo.info[0].phone, "79138857567");
            CollectionAssert.Contains(responseInfo.info[0].code, "Mobile TeleSystems");
            CollectionAssert.Contains(responseInfo.info[0].region, "Томская обл.");
            CollectionAssert.Contains(responseInfo.info[0].name, "Мобильные ТелеСистемы");
        }
    }
}