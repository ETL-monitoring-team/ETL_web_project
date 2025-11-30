# 🧊 Retail Data Warehouse & ETL Monitoring Dashboard

<div align="center">

![Dashboard](https://cdn-icons-png.flaticon.com/512/2930/2930685.png)

*Monitor ETL pipelines, track data freshness, and analyze retail sales with a modern ASP.NET MVC dashboard.*

[![ASP.NET](https://img.shields.io/badge/ASP.NET%20MVC-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)]()
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)]()
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=.NET&logoColor=white)]()
[![Chart.js](https://img.shields.io/badge/Chart.js-F5788D?style=for-the-badge&logo=chartdotjs&logoColor=white)]()

</div>

---

## 🌟 Features

### 📊 **Sales Analytics Dashboard**
- Total Sales  
- Total Quantity Sold  
- Distinct Stores / Products / Customers  
- Average Order Value  
- Daily Sales Trend (Chart.js)  
- Top Stores / Top Products / Top Customers  
- Latest 200 Fact Records  

### ⚙️ **ETL Jobs**
- View all job types (Load DimStore, Load DimProduct, Load SalesRaw, Transform_Sales, Archive, KPI Daily, etc.)  
- Manual Run button  
- “View Runs” history  
- Job status (Enabled / Disabled)

### 🧾 **ETL Logs & Run History**
- Info / Warning / Error logs  
- Execution time & duration  
- Detailed ETL messages  
- Error statistics (last 24 hours)

### ⏱️ **ETL Schedule**
- Hourly / Daily / Custom schedules  
- Next run time  
- Last run status  
- Pause / Resume / Edit actions  

### 📥 **Staging Table Explorer**
- View raw records from `stg.SalesRaw`  
- Freshness metric  
- Data quality checks (missing or invalid fields)

### 🗄️ **Data Warehouse Explorer**
Explore all DW tables:
- `DimStore`  
- `DimProduct`  
- `DimCustomer`  
- `DimDate`  
- `FactSales`  

### 🔐 **Role Management (RBAC)**
- Admin  
- Analyst  
- User  
Permissions defined with:
```csharp
[Authorize(Roles = "Admin")]
```

### 👤 **User Management & Settings**
- Change role  
- Activate / deactivate  
- Edit profile  
- Change password  
- Send requests to admin (Web3Forms)

---

## 🧱 Technology Stack

### Backend (ASP.NET MVC)
```txt
Framework: ASP.NET MVC 7
Language: C#
Database ORM: Entity Framework Core
Scheduling: Cron + Hosted Services
Authentication: ASP.NET Identity
Charts: Chart.js
```

### Database (SQL Server)
```txt
SQL Server 2022
Staging → Data Warehouse → Fact Architecture
```

### UI / Frontend
```txt
Razor Pages
CSS3 + Custom Dark Theme
Chart.js Visualization
JavaScript Components
```

---

## 🗄 Database Schema

### ⭐ Dimension Tables
```sql
DimStore(StoreKey, StoreCode, StoreName, City, Country, IsActive)
DimProduct(ProductKey, ProductCode, ProductName, Category, UnitPrice, IsActive)
DimCustomer(CustomerKey, CustomerCode, FullName, Gender, BirthDate, City)
DimDate(DateKey, FullDate, Day, Month, Year, Quarter, Week)
```

### ⭐ Fact Table
```sql
FactSales(
  FactKey,
  DateKey,
  StoreKey,
  ProductKey,
  CustomerKey,
  Quantity,
  TotalAmount
)
```

### ⭐ Staging
```sql
stg.SalesRaw
```

### ⭐ ETL Control Tables
```sql
etl.EtlJob
etl.EtlRun
etl.EtlLog
etl.EtlSchedule
```

---

## 📺 Demo Video

[![Watch the demo](https://img.youtube.com/vi/Jayw-OYoraI/0.jpg)](https://youtu.be/Jayw-OYoraI)

> Click the thumbnail to watch the full demo.


---

## 🚀 Installation & Setup

### 📌 Prerequisites
- .NET 7 SDK  
- SQL Server  
- Visual Studio 2022  
- SSMS (optional)

---

### 1️⃣ Clone the repository
```bash
git clone https://github.com/OykuEyboglu/ETL_web_project.git
cd ETL_web_project
```

---

### 2️⃣ Database Setup

**Create database:**
```sql
CREATE DATABASE EtlDb;
```

**Apply migrations:**
```powershell
Update-Database
```

Insert sample data into:
- `DimStore`
- `DimProduct`
- `DimCustomer`
- `stg.SalesRaw`

---

### 3️⃣ Run the Project
```bash
dotnet run
```

Runs at:

> https://localhost:7180/

---

## 🔧 Configuration

### Modify Connection String
`appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EtlDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

## 🤝 Contributing

1. Fork the repository  
2. Create a feature branch  
3. Commit your changes  
4. Push your branch  
5. Open a pull request  

---

## 👩‍💻 Authors

**Azra Culhacı**  
**Dila Öykü Eyüboğlu**

📧 azraculhaci@gmail.com  
📧 oykueyuboglu44@gmail.com  

---

<div align="center">

⭐ **If you like this project, don’t forget to star the repository!**  
Made with ❤️ and ☕ by Azra & Öykü

</div>
