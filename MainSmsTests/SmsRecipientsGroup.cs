using NUnit.Framework;
using MainSms;
using System;

namespace MainSmsTests
{
    public class RecipientsGroupTests
    {
        private readonly SmsRecipientsGroup mainSms = new SmsRecipientsGroup("test_project", "test_key");
        [SetUp]
        public void Setup()
        {
            Settings.host = "run.mocky.io/v3";
            Settings.use_ssl = true;

            Settings.apiPaths["group_list"] = "/f9c581cb-f6ba-49e2-bd74-4648a1c56059";
            Settings.apiPaths["group_create"] = "/0655357f-437a-4bdb-9693-ce97143882d6";
            Settings.apiPaths["group_remove"] = "/cefecd67-761c-44c6-827d-944da63877b6";
        }

        [Test]
        public void GetGroupListTest()
        {
            ResponseGroupList responseGroupList = mainSms.getGroupList(GroupType.All);

            Assert.AreEqual("success", responseGroupList.status);
            Assert.AreEqual(2, responseGroupList.recipientsGroups.Count);
        }

        [Test]
        public void createGroupTest()
        {
            ResponseGroupCreate responseGroupCreate = mainSms.createGroup("api test");

            Assert.AreEqual("success", responseGroupCreate.status);
            Assert.AreEqual("141440", responseGroupCreate.id);
            Assert.AreEqual("api test", responseGroupCreate.name);
        }

        [Test]
        public void removeGroupTest()
        {
            ResponseGroupRemove responseGroupRemove = mainSms.removeGroup("141440");

            Assert.AreEqual("success", responseGroupRemove.status);
            Assert.AreEqual("ok", responseGroupRemove.result);
        }
    }
}