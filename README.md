# GraphQL_API_Practice
This project is practice to build a simple GraphQL web API with C# ( .NET Core 6 )  
<br>

## 說明

這是一個先參考 : https://igouist.github.io/series/%E8%8F%9C%E9%9B%9E%E6%96%B0%E8%A8%93%E8%A8%98/

所撰寫完成的簡易 Restful Web API 之後，嘗試抽換 Presentation Layer 成 GraphQL Web API 的專案

主要使用套件為 Hotchocolate，程式主要範例功能有

1. 紀錄 Log (Serilog) 到 SQL Server
2. 簡易 Card 的 CRUD web API ( 需連線到 SQL server )
3. 基於 Hot chocolate 下的使用者建立、登入和認證功能 的 GraphQL Web API ( JWT )
4. Request Data validation

其餘程式設定可以參考 appsetting.json  
<br>  


## SQL Server Table

dbo.Card

|資料行名稱    |資料類型            | 允許 Null |
|-------------|-------------------|-----------|
|Id (*)       | int               |  false    |
|Name         | nvarchar(50)      |  true     |
|Description  | nvarchar(50)      |  true     |
|Attack       | int               |  false    |
|Health       | int               |  false    |
|Cost         | int               |  false    |

dbo.PraticeUser

|資料行名稱    |資料類型            | 允許 Null |
|-------------|-------------------|-----------|
|Id (*)       | int               |  false    |
|email        | nvarchar(64)      |  false    |
|password     | nvarchar(MAX)     |  false    |
|name         | nvarchar(50)      |  false    |
|role         | int               |  false    |

<br>
* : 表示 primary key，且自動增長 1
