# 📝 HappyBlog — ASP.NET Core MVC Blog Application

HappyBlog is a full-stack blogging web application built with **ASP.NET Core MVC**, **Entity Framework Core (Code First)**, and **MS SQL Server**. It lets visitors browse and comment on posts, and lets administrators create, edit, and manage blog posts, categories, and images — all secured with ASP.NET Identity.

This project was built as part of a **Full Stack Developer Internship at Qralink Technologies Pvt. Ltd.**

---

## ✨ Features

- 🔐 **User Authentication** — Register, log in, and log out using ASP.NET Identity
- 📰 **Blog Post Management** — Full CRUD operations for posts (create, read, update, delete)
- 🗂️ **Categories** — Organize posts under categories (Technology, Health, Lifestyle, Education, Sports, etc.)
- 💬 **Comments** — Visitors can comment on blog posts
- 🖼️ **Image Uploads** — Upload and manage featured images for posts
- 📱 **Responsive UI** — Built with Bootstrap for cross-device compatibility
- 🏗️ **Clean MVC Architecture** — Clear separation of Controllers, Models, and Views

---

## 🏛️ Architecture

```mermaid
flowchart TD

subgraph group_web["Web experience"]
  node_program["App startup<br/>[Program.cs]"]
  node_home["Home routes<br/>[HomeController.cs]"]
  node_homeviews["Home views"]
  node_layout["Shared layout"]
end

subgraph group_content["Blog content"]
  node_post["Post routes<br/>[PostController.cs]"]
  node_postviews["Post views"]
  node_postmodel["Post model<br/>[Post.cs]"]
  node_category["Category model<br/>[Category.cs]"]
  node_comment["Comment model<br/>[Comment.cs]"]
end

subgraph group_identity["Identity"]
  node_auth["Auth routes<br/>[AuthController.cs]"]
  node_authviews["Auth views"]
  node_identityservice["ASP.NET Identity"]
end

subgraph group_storage["Persistence"]
  node_context["EF context<br/>[AppDbContext.cs]"]
  node_sql[("SQL Server")]
  node_images["Image files"]
end

node_visitor(("Visitor"))
node_admin(("Administrator"))

node_visitor -->|"requests"| node_home
node_visitor -->|"browses, comments"| node_post
node_admin -->|"manages posts"| node_post
node_visitor -->|"registers, signs in"| node_auth
node_program -->|"routes requests"| node_home
node_program -->|"routes requests"| node_post
node_program -->|"routes requests"| node_auth
node_program -->|"configures"| node_context
node_program -->|"configures"| node_identityservice
node_home -->|"loads posts"| node_context
node_home -->|"returns view"| node_homeviews
node_post -->|"reads, writes"| node_context
node_post -->|"uploads, deletes"| node_images
node_post -->|"returns view"| node_postviews
node_post -->|"creates comment"| node_comment
node_auth -->|"registers, authenticates"| node_identityservice
node_auth -->|"returns view"| node_authviews
node_context -->|"reads, writes"| node_sql
node_post -->|"uses"| node_postmodel
node_post -->|"loads categories"| node_category
node_home -->|"loads posts"| node_postmodel
node_homeviews -->|"uses layout"| node_layout
node_postviews -->|"uses layout"| node_layout
node_authviews -->|"uses layout"| node_layout
```

---

## 🧰 Tech Stack

| Layer          | Technology                          |
|----------------|--------------------------------------|
| Framework      | ASP.NET Core MVC (.NET)              |
| ORM            | Entity Framework Core (Code First)   |
| Database       | Microsoft SQL Server                 |
| Authentication | ASP.NET Core Identity                |
| Frontend       | Razor Views, Bootstrap, CSS, JS      |
| Language       | C#                                   |

---

## 📁 Project Structure

```
HappyBlog/
├── Controllers/
│   ├── HomeController.cs
│   ├── PostController.cs
│   └── AuthController.cs
├── Models/
│   ├── Post.cs
│   ├── Category.cs
│   └── Comment.cs
├── Data/
│   └── AppDbContext.cs
├── Views/
│   ├── Home/
│   ├── Post/
│   ├── Auth/
│   └── Shared/
├── wwwroot/
│   └── images/
└── Program.cs
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
- [SQL Server](https://www.microsoft.com/sql-server) (Express or full edition)
- Visual Studio 2022 / Visual Studio Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/subhranshusahu05/happyblog_mvc_project.git
   cd happyblog_mvc_project/HappyBlog
   ```

2. **Configure the database connection**

   Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=HappyBlogDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` (or the port shown in the console).

---

## 👤 User Roles

| Role          | Capabilities                                              |
|---------------|-------------------------------------------------------------|
| Visitor       | Browse posts, view categories, add comments                |
| Administrator | Create, edit, delete posts; manage categories and images    |

---

## 🖼️ Video  https://youtu.be/sqYiL-EYN6E

<img width="5585" height="5729" alt="diagram" src="https://github.com/user-attachments/assets/b6d04bb0-e63d-49de-b7aa-71d00def600b" />
<img width="1920" height="1025" alt="01" src="https://github.com/user-attachments/assets/c93098cd-a446-42e6-a148-64874392a6ba" />
<img width="1920" height="972" alt="02" src="https://github.com/user-attachments/assets/b5dccb2e-3332-4eae-aac2-1eec31688351" />
<img width="1920" height="972" alt="03" src="https://github.com/user-attachments/assets/b48e98e0-0332-45b8-b745-8e0a7aa79a22" />
<img width="1920" height="972" alt="04" src="https://github.com/user-attachments/assets/87791adc-a2d3-4f38-bcd7-ced4c8c65adc" />
<img width="1920" height="972" alt="05" src="https://github.com/user-attachments/assets/1ce3e2c7-02e7-4aa9-9864-8cfe52ae1c16" />
<img width="1920" height="972" alt="06" src="https://github.com/user-attachments/assets/e0e38362-ca6e-4b8d-95da-5eeacf2e36f9" />
<img width="1920" height="972" alt="07" src="https://github.com/user-attachments/assets/2dbecc21-cdd8-4642-8640-5b1bc0b7f25d" />





---

## 🙏 Acknowledgment

This project was developed during a **Full Stack Developer Internship** at **Qralink Technologies Pvt. Ltd.**, where I gained hands-on experience with C#, ASP.NET Core MVC, ASP.NET Core Web API, Bootstrap, and MS SQL Server.

---

## 📄 License

This project is open source and available for learning purposes. Feel free to fork and build upon it.

---

## 📬 Contact

**Subhranshu Sekhar Sahu**
Electronics and Telecommunication Engineering, Parala Maharaja Engineering College (PMEC)

