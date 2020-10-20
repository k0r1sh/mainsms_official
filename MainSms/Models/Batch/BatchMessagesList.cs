using System;
using System.Collections.Generic;
using System.Text;

namespace MainSms
{
    public class BatchMessagesList
    {
        private int currentId = 0;
        private Dictionary<string, BatchMessage> _messagesList = new Dictionary<string, BatchMessage>();
        public string addMessage(string phone, string text, string messageId)
        {
            BatchMessage batchMessage = new BatchMessage(messageId, phone, text);
            _messagesList.Remove(messageId);
            _messagesList.Add(messageId, batchMessage);
            return messageId;
        }

        public string addMessage(string phone, string text)
        {
            currentId++;
            string messageId = currentId.ToString();
            BatchMessage batchMessage = new BatchMessage(messageId, phone, text);
            _messagesList.Add(messageId, batchMessage);
            return messageId;
        }

        public bool removeMessage(string messageId)
        {
            return _messagesList.Remove(messageId);
        }

        public Dictionary<string, string> toDictionary()
        {
            Dictionary<string, string> resultDctionary = new Dictionary<string, string>();
            int index = 0;
            foreach(var batchMessage in _messagesList)
            {
                resultDctionary.Add($"messages[{index}][id]", batchMessage.Value.id);
                resultDctionary.Add($"messages[{index}][phone]", batchMessage.Value.phone);
                resultDctionary.Add($"messages[{index}][text]", batchMessage.Value.text);
                index++;
            }
            return resultDctionary;
        }

        public Dictionary<string, string> toSignDictionary()
        {
            Dictionary<string, string> resultDctionary = new Dictionary<string, string>();
            List<string> messagesStringList = new List<string>();
            foreach (var batchMessage in _messagesList)
            {
                messagesStringList.Add($"{batchMessage.Value.id},{batchMessage.Value.phone},{batchMessage.Value.text}");
            }
            resultDctionary.Add("messages", String.Join(",", messagesStringList));
            return resultDctionary;
        }
    }
}
