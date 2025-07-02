# 📦 Angular 19 + .NET 8 Fullstack Template

Dieses Repository enthält ein sofort einsetzbares Fullstack-Projekt-Template für **Angular 19 (inkl. NgRx Router Store)** und **ASP.NET Core 8 Web API** mit einer **PostgreSQL-Datenbank via Docker Compose**.

Es wurde als Basis für **Coding Challenges** oder den schnellen Start neuer Projekte konzipiert und kombiniert moderne Best Practices im Frontend und Backend.

---

## 🔧 Tech Stack

### Frontend
- [Angular 19](https://angular.io/)
- [NgRx](https://ngrx.io/) (inkl. Router Store)
- [Angular Material](https://material.angular.io/)
- RxJS, TypeScript

### Backend
- [.NET 8 Web API](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [MediatR](https://github.com/jbogard/MediatR)
- [AutoMapper](https://automapper.org/)
- Clean Architecture-Ansatz

### Datenbank
- PostgreSQL (über Docker Compose)
- Optional: SQLite (lokal ohne Container)

---

## 🚀 Ziele des Templates
- Schneller Start für Fullstack-Projekte mit Angular + .NET
- Klar strukturierte Projekt-Architektur
- Bereit für Coding-Challenges oder Prototypen
- Modulare Erweiterbarkeit durch Clean-Code-Prinzipien

---

## 📁 Projektstruktur (Kurzüberblick)
```
root/
├── backend/       // .NET API mit Clean Architecture-Struktur
├── frontend/      // Angular 19 App mit NgRx & Material
└── docker/        // Docker Compose Setup für PostgreSQL
```

---

## 📌 Hinweise
- Du kannst sowohl Visual Studio / Rider (Backend) als auch VS Code (Frontend) verwenden.
- Die Angular-App nutzt den Router Store und Angular Material für UI-Komponenten.
- Das Backend ist auf .NET 8 festgelegt, mit Entity Framework Core für PostgreSQL-Zugriff.
