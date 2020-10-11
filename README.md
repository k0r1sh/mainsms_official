## Официальная реализация API MainSMS.ru для C#
[![NuGet](https://img.shields.io/nuget/v/mainsms_official)](https://www.nuget.org/packages/mainsms_official/)

[MainSMS.ru](http://MainSMS.ru) это платформа для транзакционных SMS уведомлений и SMS рассылок.

### Установка

> Проект собран для .NET 45 и .NET Platform Standard 1.4.

Установка из менеджера пакетов Nuget или из командной строки

`PM> Install-Package MainSMS`

### Использование
#### Подключение пространства имен
````c#
using MainSms;
````

#### Ответы на запросы
Любой запрос к API, возвращает объект класса который содержит поля:
| Поле | Описание |Значения|
| ------ | ------ | ------ |
| status | Статус выполнения запроса |success или error |
| erorr | Код ошибки если статус error | [Смотрите коды ошибок для вызываемого метода](https://mainsms.ru/home/api/main#send) |
| message | Описание ошибки если статус error | [Смотрите описание ошибок для вызываемого метода](https://mainsms.ru/home/api/main#send) |
| response | Исходный XML ответа на API вызов | |

А так же поля доступные только для конкретного ответа на запрос, например при запросе баланса доступно поле balance. 

# Работа с сообщениями
````c#
SmsMessage sms = new SmsMessage(project_id, api_key);
````
В качестве параметра передаем project_id, api_key и если необходимо is_test и use_ssl.
project_id, api_key можно получить на странице https://mainsms.ru/office/api_accounts
is_test - если true то смс не будут отправляться.
use_ssl - если true то будет использоваться протокол https.

#### Запрос баланса
````c#
ResponseBalance responseBalance = sms.getBalance();
if (responseBalance.status == "success") Console.WriteLine(responseBalance.balance);
else Console.WriteLine("Error - " + responseBalance.message);
````
##### Поля класса ResponseBalance
| Поле | Описание |
| ------ | ------ |
| balance | Остаток средств на счете |
#### Расчет стоимости отправки
````c#
ResponsePrice responsePrice = sms.getMessagesPrice("sender_name", "89121231234,9121231235", "test text");
if (responsePrice.status == "success") Console.WriteLine($"Частей в одной смс {responsePrice.parts}, всего частей {responsePrice.count}, стоимость отправки {responsePrice.price}");
else Console.WriteLine("Error - " + responsePrice.message);
````
##### Поля класса ResponsePrice
| Поле | Описание |
| ------ | ------ |
| recipients | Массив номеров получателей |
| balance | Остаток средств на счете |
| parts | Количество частей в одной смс |
| count | Всего частей смс |
| price | Стоимость отправки |
#### Отправка SMS сообщения
````c#
ResponseSend responseSend = sms.sendSms("sender_name", "79609701234", "test SMS message");
if (responseSend.status == "success") Console.WriteLine($"Частей в одной смс {responseSend.parts}, всего частей {responseSend.count}, стоимость отправки {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> Меток sendSms, может принимать в качестве четвертого параметра параметр типа DateTime, со временем на которое необходимо запланировать отправку сообщения. Время указывается в [часовом поясе кабинета mainsms.ru](https://mainsms.ru/office/user/zone)
##### Поля класса ResponseSend
| Поле | Описание |
| ------ | ------ |
| recipients | Массив номеров получателей |
| messages_id | Массив идентификаторов сообщений |
| balance | Остаток на средств счете |
| parts | Количество частей в одной смс |
| count | Всего частей смс |
| price | Стоимость отправки |
| test | 1 если режим тестирования и 0 если сообщения отправляются |
#### Отправка Viber сообщения
````c#
ResponseSend responseSend = sms.sendViber("mainsms", "79609701234", "test Viber message", "http://autodrive.org.ua/wp-content/uploads/2018/11/final-logo-example.png", "Buy now", "https://mainsms.ru");
if (responseSend.status == "success") Console.WriteLine($"Частей в одном сообщении {responseSend.parts}, всего частей {responseSend.count}, стоимость отправки {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> Меток sendViber, может принимать в качестве дополнительного параметра параметр типа DateTime, со временем на которое необходимо запланировать отправку сообщения. Время указывается в [часовом поясе кабинета mainsms.ru](https://mainsms.ru/office/user/zone)
#### Отправка Viber или SMS сообщения
В случае если Viber сообщение доставить не получится, отправится SMS сообщение.
````c#
ResponseSend responseSend = sms.sendViberOrSms("mainsms", "79609701234", "test SMS message", "test Viber message", "http://autodrive.org.ua/wp-content/uploads/2018/11/final-logo-example.png", "Buy now", "https://mainsms.ru");
if (responseSend.status == "success") Console.WriteLine($"Частей в одном сообщении {responseSend.parts}, всего частей {responseSend.count}, стоимость отправки {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> Меток sendViberOrSms, может принимать в качестве дополнительного параметра параметр типа DateTime, со временем на которое необходимо запланировать отправку сообщения. Время указывается в [часовом поясе кабинета mainsms.ru](https://mainsms.ru/office/user/zone)
#### Запрос статуса сообщения
````c#
ResponseStatus responseStatus = sms.getMessagesStatus("1234567");
if (responseStatus.status == "success") Console.WriteLine($"Статус сообщения 1234567 - {responseStatus.messages["1234567"]}, канал доставки - {responseStatus.channels["1234567"]}");
else Console.WriteLine("Error - " + responseStatus.message);
````
##### Поля класса ResponseStatus
| Поле | Описание |
| ------ | ------ |
| messages | Хранилище ключ значение, где ключ это ID сообщения, а значение его статус |
| channels | Хранилище ключ значение, где ключ это ID сообщения, а значение канал отправки |
#### Отмена запланированного сообщения
````c#
ResponseCancel responseCancel = sms.cancelMessages("1234567");
if (responseCancel.status == "success") Console.WriteLine($"Сообщение 1234567 - {responseCancel.messages["1234567"]}");
else Console.WriteLine("Error - " + responseCancel.message);
````
##### Поля класса ResponseCancel
| Поле | Описание |
| ------ | ------ |
| messages | Хранилище ключ значение, где ключ это ID сообщения, а значение его статус (canceled) |
#### Запрос информации о номерах
````c#
ResponseInfo responseInfo = sms.getPhonesInfo("9138857567");
if (responseInfo.status == "success") Console.WriteLine($"Номер 9138857567 принадлежит оператору - {responseInfo.info[0].name}");
else Console.WriteLine("Error - " + responseInfo.message);
````
##### Поля класса ResponseInfo
| Поле | Описание |
| ------ | ------ |
| info | Массив объектов типа PhoneInfo |
##### Поля класса PhoneInfo
| Поле | Описание |
| ------ | ------ |
| phone | Номер телефона |
| code | Код оператора |
| region | Регион |
| name | Название оператора |

# Работа с группами получателей
````c#
SmsContactsGroup smsGroup = new SmsContactsGroup(project_id, api_key);
````
В качестве параметра передаем project_id, api_key и если необходимо use_ssl.
project_id, api_key можно получить на странице https://mainsms.ru/office/api_accounts
use_ssl - если true то будет использоваться протокол https.
#### Запрос списка групп
````c#
ResponseGroupList responseGroupList = smsGroup.getGroupList(GroupType.User);
if (responseGroupList.status == "success") Console.WriteLine($"Всего групп - {responseGroupList.recipientsGroups.Count}");
else Console.WriteLine("Error - " + responseGroupList.message);
````
##### Поля класса responseGroupList
| Поле | Описание |
| ------ | ------ |
| recipientsGroups | Массив объектов типа RecipientsGroup |
##### Поля класса RecipientsGroup
| Поле | Описание |
| ------ | ------ |
| id | Уникальный идентификатор группы |
| name | Название группы получателей |
| contacts | Количество контактов в группе |
| type | Тип группы |
#### Создание группы
````c#
ResponseGroupCreate responseGroupCreate = smsGroup.createGroup("group name");
if (responseGroupCreate.status == "success") Console.WriteLine($"Группа {responseGroupCreate.name} создана, id - {responseGroupCreate.id}");
else Console.WriteLine("Error - " + responseGroupCreate.message);
````
##### Поля класса ResponseGroupCreate
| Поле | Описание |
| ------ | ------ |
| id | Уникальный идентификатор группы |
| name | Название группы получателей |
#### Удаление группы
````c#
ResponseGroupRemove responseGroupRemove = smsGroup.removeGroup("12345");
if (responseGroupRemove.status == "success") Console.WriteLine("Группа удалена");
else Console.WriteLine("Error - " + responseGroupRemove.message);
````
##### Поля класса ResponseGroupRemove
| Поле | Описание |
| ------ | ------ |
| result | Ответ сеовера "ok" |

# Работа с получателями
````c#
SmsContact smsContact = new SmsContact(project_id, api_key);
````
В качестве параметра передаем project_id, api_key и если необходимо use_ssl.
project_id, api_key можно получить на странице https://mainsms.ru/office/api_accounts
use_ssl - если true то будет использоваться протокол https.
#### Создание контакта
````c#
ContactInfo contactInfo = new ContactInfo();
contactInfo.phone = "79609709097";
contactInfo.group = "141515";
contactInfo.lastname = "Иванов";
contactInfo.firstname = "Николай";
contactInfo.patronymic = "Александрович";
contactInfo.birthday = "24.12.1987";
contactInfo.param1 = "Параметр 1";
contactInfo.param2 = "Параметр 2";
ResponseContactCreate responseContactCreate = smsContact.createContact(contactInfo);
if (responseContactCreate.status == "success")  Console.WriteLine("Получатель создан");
else Console.WriteLine("Error - " + responseContactCreate.message);
````
##### Поля класса ResponseContactCreate
| Поле | Описание |
| ------ | ------ |
| phones | Массив из номеров которые удалось добавить |
| group | Массив ID групп в которых встречается номер |
#### Запрос контакта
````c#
ResponseContactExists responseContactExists = smsContact.existsContact("79609709097");
if (responseContactExists.status == "success")  {
    Console.WriteLine($"Получатель найден, в группах {responseContactExists.contactInfo[0].group}");
}
else Console.WriteLine("Error - " + responseContactExists.message);
````
##### Поля класса responseContactExists
| Поле | Описание |
| ------ | ------ |
| contactInfo | Массив объектов типа ContactInfo |
##### Поля класса ContactInfo
| Поле | Описание |
| ------ | ------ |
| phone | Номер телефона |
| group | id групп через строкой через запятую |
| lastname | Фамилия |
| patronymic | Отчество |
| birthday | День рождения в фотмате 29.01.1987 |
| param1 | Параметр 1 |
| param2 | Параметр 2 |
#### Удаление контакта
````c#
ResponseContactRemove responseContactRemove = smsContact.removeContact("79609709097, 79609709098");
if (responseContactRemove.status == "success")  {
    Console.WriteLine($"Удалено {responseContactRemove.phones.Count} контактов");
}
else Console.WriteLine("Error - " + responseContactRemove.message);
````
##### Поля класса ResponseContactRemove
| Поле | Описание |
| ------ | ------ |
| phones | Массив удаленный номеров |
