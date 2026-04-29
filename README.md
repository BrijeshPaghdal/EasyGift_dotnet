# 🎁 EasyGift

> A full-stack gift management web application with a PHP-powered frontend and a .NET Core REST API backend — backed by MySQL.

---

## 📌 About the Project

EasyGift is a web application designed to simplify the process of managing and sending gifts. The UI is built with pure **HTML, CSS, and JavaScript**, the frontend-backend integration is handled via **PHP**, and all core business logic and data operations are exposed through a **RESTful API built with ASP.NET Core** connected to a **MySQL** database.

> ⚠️ **Note on language stats:** The repository shows a high percentage of JavaScript due to included third-party library files (e.g., jQuery, Bootstrap JS, etc.). The actual custom application code is primarily PHP, C#, HTML, and CSS.

---

## 🏗️ Project Structure

```
EasyGift_dotnet/
├── EasyGift/               # Frontend — HTML, CSS, JS + PHP integration layer
│   ├── assets/             # Static assets (CSS, JS libraries, images)
│   ├── pages/              # HTML pages
│   └── api/                # PHP files handling API calls to the .NET backend
│
├── EasyGift_API/           # Backend — ASP.NET Core Web API
│   ├── Controllers/        # API controllers (REST endpoints)
│   ├── Models/             # Data models / entities
│   ├── Data/               # Database context / EF configurations
│   └── EasyGift_API.csproj
│
├── EasyGift.bacpac         # Database export file (MySQL schema + seed data)
├── EasyGift.sln            # Visual Studio solution file
└── README.md
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Frontend UI** | HTML5, CSS3, Vanilla JavaScript |
| **Frontend-Backend Bridge** | PHP |
| **Backend API** | ASP.NET Core (C#) |
| **Database** | MySQL |
| **Database Export** | `.bacpac` file included |

---

## ✨ Features

- 🎁 Browse and manage gift listings
- 👤 User registration and login
- 🛒 Add gifts to cart / wishlist
- 📦 Order management
- 🔌 RESTful API for all core operations
- 🗄️ Full database schema included for easy setup

---

## 🚀 Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/download) or later
- [PHP 7.4+](https://www.php.net/downloads) with a local server (XAMPP / WAMP / Laragon)
- [MySQL 8.0+](https://dev.mysql.com/downloads/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

---

### 1. Clone the Repository

```bash
git clone https://github.com/BrijeshPaghdal/EasyGift_dotnet.git
cd EasyGift_dotnet
```

---

### 2. Set Up the Database

Import the included `.bacpac` database file into your MySQL instance.

You can use **MySQL Workbench** or run via CLI:

```bash
# Import using MySQL Workbench > Server > Data Import > Import from Self-Contained File
# Select EasyGift.bacpac
```

> Alternatively, if you have the SQL dump, run:
> ```bash
> mysql -u root -p easygift < easygift_dump.sql
> ```

---

### 3. Configure and Run the .NET API

```bash
cd EasyGift_API
```

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=EasyGift;user=root;password=yourpassword;"
}
```

Then run the API:

```bash
dotnet restore
dotnet run
```

The API will start at: `http://localhost:5000` (or as configured)

---

### 4. Run the Frontend (PHP)

Copy the `EasyGift/` folder into your local PHP server's root directory (e.g., `htdocs` for XAMPP):

```
C:/xampp/htdocs/EasyGift/
```

Update the API base URL in the PHP config/constants file to point to your running .NET API:

```php
define('API_BASE_URL', 'http://localhost:5000/api');
```

Then open in your browser:

```
http://localhost/EasyGift/
```

---

## 📡 API Endpoints (Sample)

All endpoints follow the base route pattern: `/api/EasyGift/{Controller}`

The API uses a **Generic Controller** pattern — every resource (e.g., `Order`, `Product`, `User`) inherits the same base CRUD endpoints. The `Order` controller also adds a custom endpoint on top.

### Generic Endpoints (available on all controllers)

| Method | Endpoint | Query Params | Description |
|---|---|---|---|
| `GET` | `/api/EasyGift/{Controller}` | `columns`, `filter`, `limit` | Get all records (supports filtering & column selection) |
| `GET` | `/api/EasyGift/{Controller}/{id}` | `columns`, `filter` | Get a single record by ID |
| `POST` | `/api/EasyGift/{Controller}` | — | Create a new record |
| `PATCH` | `/api/EasyGift/{Controller}/{id}` | — | Partially update a record by ID (key-value dictionary body) |
| `DELETE` | `/api/EasyGift/{Controller}/{id}` | — | Delete a record by ID |
| `DELETE` | `/api/EasyGift/{Controller}` | `filter` | Delete records matching a filter expression |

### Order — Custom Endpoint

| Method | Endpoint | Query Params | Description |
|---|---|---|---|
| `GET` | `/api/EasyGift/Order/GetPastOrder` | `ShopId`, `limit` (default: 7) | Get past orders for a specific shop |

### Example Requests

```http
# Get all orders for ShopId 5, limited to last 7
GET /api/EasyGift/Order/GetPastOrder?ShopId=5&limit=7

# Get specific columns only
GET /api/EasyGift/Order?columns=Id&columns=Status&columns=TotalAmount

# Create a new order
POST /api/EasyGift/Order
Content-Type: application/json
{ "shopId": 5, "totalAmount": 1500, "status": "Pending" }

# Partial update — change order status
PATCH /api/EasyGift/Order/12
Content-Type: application/json
{ "Status": "Delivered" }
```

---

## 🗃️ Database

The repository includes `EasyGift.bacpac` — a full database export containing the schema and sample data. Import it to get the database up and running immediately.

---

## 👨‍💻 Author

**Brijesh Paghdal**

- GitHub: [@BrijeshPaghdal](https://github.com/BrijeshPaghdal)
- LinkedIn: [brijesh-paghdal](https://linkedin.com/in/brijesh-paghdal)
- Email: brijeshpaghdal13@gmail.com
