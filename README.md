## ����������� ���������� API MainSMS.ru ��� C#
[![NuGet](https://img.shields.io/nuget/v/mainsms_official)](https://www.nuget.org/packages/mainsms_official/)

[MainSMS.ru](http://MainSMS.ru) ��� ��������� ��� �������������� SMS ����������� � SMS ��������.

### ���������

> ������ ������ ��� .NET 45 � .NET Platform Standard 1.4.

��������� �� ��������� ������� Nuget ��� �� ��������� ������

`PM> Install-Package MainSMS`

### �������������
#### ����������� ������������ ����
````c#
using MainSms;
````
#### �������������� ������ ������ Sms
� �������� ��������� �������� project_id, api_key � ���� ���������� is_test � use_ssl.
project_id, api_key ����� �������� �� �������� https://mainsms.ru/office/api_accounts
is_test - ���� true �� ��� �� ����� ������������.
use_ssl - ���� true �� ����� �������������� �������� https.
````c#
Sms sms = new Sms(project_id, api_key);
````
#### ������ �� �������
����� ������ � API, ���������� ������ ������ ������� �������� ���� 
| ���� | �������� |��������|
| ------ | ------ | ------ |
| status | ������ ���������� ������� |success ��� error |
| erorr | ��� ������ ���� ������ error | [�������� ���� ������ ��� ����������� ������](https://mainsms.ru/home/api/main#send) |
| message | �������� ������ ���� ������ error | [�������� �������� ������ ��� ����������� ������](https://mainsms.ru/home/api/main#send) |
| response | �������� XML ������ �� API ����� | |

� ��� �� ���� ��������� ������ ��� ����������� ������ �� ������, �������� ��� ResponseBalance �������� ���� balance. 
#### ������ �������
````c#
ResponseBalance responseBalance = sms.getBalance();
if (responseBalance.status == "success") Console.WriteLine(responseBalance.balance);
else Console.WriteLine("Error - " + responseBalance.message);
````
##### ���� ������ ResponseBalance
| ���� | �������� |
| ------ | ------ |
| balance | ������� ������� �� ����� |
#### ������ ��������� ��������
````c#
ResponsePrice responsePrice = sms.getMessagesPrice("sender_name", "89121231234,9121231235", "test text");
if (responsePrice.status == "success") Console.WriteLine($"������ � ����� ��� {responsePrice.parts}, ����� ������ {responsePrice.count}, ��������� �������� {responsePrice.price}");
else Console.WriteLine("Error - " + responsePrice.message);
````
##### ���� ������ ResponsePrice
| ���� | �������� |
| ------ | ------ |
| recipients | ������ ������� ����������� |
| balance | ������� ������� �� ����� |
| parts | ���������� ������ � ����� ��� |
| count | ����� ������ ��� |
| price | ��������� �������� |
#### �������� SMS ���������
````c#
ResponseSend responseSend = sms.sendSms("sender_name", "79609701234", "test SMS message");
if (responseSend.status == "success") Console.WriteLine($"������ � ����� ��� {responseSend.parts}, ����� ������ {responseSend.count}, ��������� �������� {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> ����� sendSms, ����� ��������� � �������� ���������� ��������� �������� ���� DateTime, �� �������� �� ������� ���������� ������������� �������� ���������. ����� ����������� � [������� ����� �������� mainsms.ru](https://mainsms.ru/office/user/zone)
##### ���� ������ ResponseSend
| ���� | �������� |
| ------ | ------ |
| recipients | ������ ������� ����������� |
| messages_id | ������ ��������������� ��������� |
| balance | ������� �� ������� ����� |
| parts | ���������� ������ � ����� ��� |
| count | ����� ������ ��� |
| price | ��������� �������� |
| test | 1 ���� ����� ������������ � 0 ���� ��������� ������������ |
#### �������� Viber ���������
````c#
ResponseSend responseSend = sms.sendViber("mainsms", "79609701234", "test Viber message", "http://autodrive.org.ua/wp-content/uploads/2018/11/final-logo-example.png", "Buy now", "https://mainsms.ru");
if (responseSend.status == "success") Console.WriteLine($"������ � ����� ��������� {responseSend.parts}, ����� ������ {responseSend.count}, ��������� �������� {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> ����� sendViber, ����� ��������� � �������� ��������������� ��������� �������� ���� DateTime, �� �������� �� ������� ���������� ������������� �������� ���������. ����� ����������� � [������� ����� �������� mainsms.ru](https://mainsms.ru/office/user/zone)
#### �������� Viber ��� SMS ���������
� ������ ���� Viber ��������� ��������� �� ���������, ���������� SMS ���������.
````c#
ResponseSend responseSend = sms.sendViberOrSms("mainsms", "79609701234", "test SMS message", "test Viber message", "http://autodrive.org.ua/wp-content/uploads/2018/11/final-logo-example.png", "Buy now", "https://mainsms.ru");
if (responseSend.status == "success") Console.WriteLine($"������ � ����� ��������� {responseSend.parts}, ����� ������ {responseSend.count}, ��������� �������� {responseSend.price}");
else Console.WriteLine("Error - " + responseSend.message);
````
> ����� sendViberOrSms, ����� ��������� � �������� ��������������� ��������� �������� ���� DateTime, �� �������� �� ������� ���������� ������������� �������� ���������. ����� ����������� � [������� ����� �������� mainsms.ru](https://mainsms.ru/office/user/zone)
#### ������ ������� ���������
````c#
ResponseStatus responseStatus = sms.getMessagesStatus("1234567");
if (responseStatus.status == "success") Console.WriteLine($"������ ��������� 1234567 - {responseStatus.messages["1234567"]}, ����� �������� - {responseStatus.channels["1234567"]}");
else Console.WriteLine("Error - " + responseStatus.message);
````
##### ���� ������ ResponseStatus
| ���� | �������� |
| ------ | ------ |
| messages | ��������� ���� ��������, ��� ���� ��� ID ���������, � �������� ��� ������ |
| channels | ��������� ���� ��������, ��� ���� ��� ID ���������, � �������� ����� �������� |
#### ������ ���������������� ���������
````c#
ResponseCancel responseCancel = sms.cancelMessages("1234567");
if (responseCancel.status == "success") Console.WriteLine($"��������� 1234567 - {responseCancel.messages["1234567"]}");
else Console.WriteLine("Error - " + responseCancel.message);
````
##### ���� ������ ResponseCancel
| ���� | �������� |
| ------ | ------ |
| messages | ��������� ���� ��������, ��� ���� ��� ID ���������, � �������� ��� ������ (canceled) |
#### ������ ���������� � �������
````c#
ResponseInfo responseInfo = sms.getPhonesInfo("9138857567");
if (responseInfo.status == "success") Console.WriteLine($"����� 9138857567 ����������� ��������� - {responseInfo.info[0].name}");
else Console.WriteLine("Error - " + responseInfo.message);
````
##### ���� ������ ResponseInfo
| ���� | �������� |
| ------ | ------ |
| info | ������ �������� ���� PhoneInfo |
##### ���� ������ PhoneInfo
| ���� | �������� |
| ------ | ------ |
| phone | ����� �������� |
| code | ��� ��������� |
| region | ������ |
| name | �������� ��������� |

