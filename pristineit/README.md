# PristineIt - Personal Task Manager

> **A clean, simple, and friendly way to learn Clean Architecture, DDD basics, and EF Core with Postgres!**

---

## Why This Repo Exists

Hey there, junior dev!  
If you’ve ever felt lost in big codebases or wondered **“How should I structure my app?”**, this repo is **made for you**.

`PristineIt` is a **tiny todo app** — but under the hood, it shows **real-world best practices** in a way that’s **easy to read, understand, and copy**.

---

## What You’ll Learn (Step by Step)

| Topic                            | What You See                                                          | Why It Matters                                       |
| -------------------------------- | --------------------------------------------------------------------- | ---------------------------------------------------- |
| **Clean Architecture**           | 4 clear layers: `Domain`, `Application`, `Infrastructure`, `API`      | Keeps business logic **independent** from frameworks |
| **Domain-Driven Design (Light)** | `Task` as **Aggregate Root**, `Priority` & `Tag` as **Value Objects** | Learn **rich domain models** without confusion       |
| **Value Objects**                | `Priority`, `Tag` with factory methods                                | Prevent invalid data, make code **safer**            |
| **Factory Methods**              | `Task.Create(...)`                                                    | Objects are **always valid** when created            |
| **Custom Exceptions**            | `TaskAlreadyCompletedException`                                       | Handle **business rules** clearly                    |
| **EF Core + Postgres**           | `TaskDbContext`, owned types, change tracking                         | Real database with **no magic**                      |
| **Repository Pattern**           | `ITaskRepository` + `TaskRepository`                                  | Decouple domain from data access                     |
| **Application Services**         | `TaskService`                                                         | Orchestrate use cases cleanly                        |
| **DTOs**                         | `TaskDto`                                                             | Keep domain pure, expose only what’s needed          |

---

## Features (Simple but Powerful)

- Create tasks with title, description, priority
- Add tags
- Mark as completed (with rule check!)
- Snooze to a future date
- Filter by tag or priority
- List **today’s tasks**

---

## How to Run (5 Minutes!)

1. **Clone the repo**

   ```bash
   git clone https://github.com/yourname/PristineIt.git
   cd PristineIt
   ```

2. **Set up Postgres**

   - Create a database: `pristineit_db`
   - Update connection string in `appsettings.json`

3. **Run migrations**

   ```bash
   dotnet ef database update --project Infrastructure
   ```

4. **Run the app**

   ```bash
   dotnet run --project API
   ```

5. **Try it out!**  
   Open `http://localhost:5000/swagger` and play with tasks

---

## Perfect For You If You Want To...

- Understand **why** we separate layers
- See **Value Objects** in action
- Learn **EF Core owned types** with collections
- Practice **Clean Code** with comments in simple English
- Build a **portfolio project** that impresses seniors

---

## Made With Love For Junior Devs

> **No over-engineering. No jargon. Just clean, working code with comments that explain _why_.**

---

## Want to Contribute?

1. Fork it
2. Create a feature branch (`feat/add-due-date-reminders`)
3. Commit your changes
4. Open a Pull Request

We **welcome simple improvements** — especially if they help juniors learn!
