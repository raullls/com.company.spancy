# com.company.spancy

Template web project built using an **N-tier architecture**, combining a .NET backend and a modern JavaScript frontend.

The solution demonstrates how to structure a web application using:
- **Nancy Framework** for the web layer
- **Spring.NET** for dependency injection and configuration
- **NHibernate** for data access
- **React** and **Bootstrap** for the frontend UI

This repository is intended as a **template / starting point** for new projects following this architecture.

---

## Solution Structure

The solution contains three main projects:

### com.company.spancy
**Console Application (Backend / Web Host)**

- Hosts the core web application using a **built-in web server**
- Contains the main backend logic
- Uses:
  - Nancy Framework
  - Spring.NET
  - NHibernate
- Acts as the entry point for the backend application

---

### com.company.spancy.front
**Node.js Frontend Application**

- Frontend implemented using:
  - React
  - Bootstrap
- Responsible for the user interface
- Communicates with the backend hosted by `com.company.spancy`

---

### com.company.spancy.test
**Backend Test Project**

- Automated backend tests
- Built using:
  - MSTest
  - Spring Test Framework
- Focused on testing backend logic and integration

---

## Prerequisites

### Backend
- Visual Studio 2019 (or newer)
- .NET Framework / .NET SDK (as required by the solution)
- NuGet (package restore enabled)

### Frontend
- Node.js (LTS recommended)
- npm (comes with Node.js)

---

## How to Run

### Backend (Console Application)

1. Open `com.company.spancy.sln` in Visual Studio
2. Restore NuGet packages (automatic or via `nuget restore`)
3. Set `com.company.spancy` as the startup project
4. Run using **F5** or **Ctrl+F5**

The backend will start hosting the web application.

---

### Frontend (React Application)

From the `com.company.spancy.front` folder:

```bash
npm install
npm start
