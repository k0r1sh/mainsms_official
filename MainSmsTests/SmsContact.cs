using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class RecipientsContactTests
    {
        private readonly SmsContact mainSms = new SmsContact("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;

            Settings.apiPaths["contact_create"] = "/30bda619-617d-4011-a884-ac4226da2b98";
            Settings.apiPaths["contact_remove"] = "/858a3bc0-d210-4223-9554-aad9a4945d7d";
            Settings.apiPaths["contact_exists"] = "/9aef47c7-b718-4cf1-bcd9-ab1331272eb7";
        }

        [Test]
        public void createContactTest()
        {
            ContactInfo contactInfo = new ContactInfo();
            contactInfo.phone = "79609709097";
            contactInfo.group = "141515";
            contactInfo.lastname = "Иванов";
            contactInfo.firstname = "Николай";
            contactInfo.patronymic = "Александрович";
            contactInfo.birthday = "24.12.1987";
            contactInfo.param1 = "Параметр 1";
            contactInfo.param2 = "Параметр 2";

            ResponseContactCreate responseContactCreate = mainSms.createContact(contactInfo);
            Assert.AreEqual("success", responseContactCreate.status);
            CollectionAssert.Contains(responseContactCreate.phones, "79609709097");
            CollectionAssert.Contains(responseContactCreate.groups, "141515");
            CollectionAssert.Contains(responseContactCreate.groups, "2");
        }

        [Test]
        public void removeContactTest()
        {
            ResponseContactRemove contactRemove = mainSms.removeContact("79609709097, 79609709098");

            Assert.AreEqual("success", contactRemove.status);
            CollectionAssert.Contains(contactRemove.phones, "79609709097");
            CollectionAssert.Contains(contactRemove.phones, "79609709098");
        }

        [Test]
        public void existsContactTest()
        {
            ResponseContactExists responseContactExists= mainSms.existsContact("79609709097");

            Assert.AreEqual("success", responseContactExists.status);
            Assert.AreEqual("79609709097", responseContactExists.contactInfo[0].phone);
            Assert.AreEqual("Николай", responseContactExists.contactInfo[0].firstname);
            Assert.AreEqual("Иванов", responseContactExists.contactInfo[0].lastname);
            Assert.AreEqual("Александрович", responseContactExists.contactInfo[0].patronymic);
            Assert.AreEqual("1987-12-24", responseContactExists.contactInfo[0].birthday);
            Assert.AreEqual("Параметр 1", responseContactExists.contactInfo[0].param1);
            Assert.AreEqual("Параметр 2", responseContactExists.contactInfo[0].param2);
            CollectionAssert.Contains(responseContactExists.contactInfo[0].group.Split(','), "2");
            CollectionAssert.Contains(responseContactExists.contactInfo[0].group.Split(','), "141515");

        }
    }
}