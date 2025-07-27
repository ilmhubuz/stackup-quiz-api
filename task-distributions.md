# 📋 Quiz API – Task Assignment

Each task is designed to be worked on **in parallel** with **no overlap or blocking**.  
All collaboration must happen through **GitHub branches and pull requests**.

---

## ✅ Task 1 – Project Setup  
👤 **Azizbek**

**Responsibilities**
- [x] Create GitHub repository (`quiz-api`)
- [ ] Scaffold .NET Web API project:
  ```bash
  dotnet new webapi -n Stackup.Quiz.Api
  ```
- [ ] Set up folder structure:
  - `Models/`, `Controllers/`, `Dtos/`, `Repositories/`, `Services/`
- [ ] Add `.gitignore`, README, and commit base template
- [ ] Clean `Program.cs` and minimal startup logic
- [ ] Example controller stub
- [ ] Working API template with build/run verified
- [ ] Push to GitHub so others can clone and start

---

## ✅ Task 2 – Models & Inheritance  
👤 **Elyorbek**

**Responsibilities**
- Implement quiz data models:
  - `Quiz.cs`
  - `Question.cs` (abstract base)
  - `McqQuestion.cs`
  - `TrueFalseQuestion.cs`
  - `ShortAnswerQuestion.cs`
- (Optional) Add `QuestionType` enum
- [ ] Push to GitHub so others can clone and start

---

## ✅ Task 3 – DTO Layer  
👤 **Mirsalim**

**Responsibilities**
- Create clean API DTOs:
  - `QuizCreateDto.cs`
  - `QuestionDto.cs` (with support for MCQ/TF/Short)
  - `AnswerSubmissionDto.cs`
  - `QuizResultDto.cs`
- Use validation attributes like `[Required]`, `[Range]`
- Keep DTOs free of domain model logic

---

## ✅ Task 4 – Repository Layer  
👤 **Akbarjon**

**Responsibilities**
- Build `QuizRepository.cs` for in-memory storage
  - Store: `List<Quiz>`
  - Methods: `AddQuiz`, `GetAll`, `GetById`, `Delete`, `ToggleStatus`, `AddQuestion`
- This layer acts as the app’s database
- No database or EF Core – just C# lists

---

## ✅ Task 5 – Teacher API Controller  
👤 **Abduvali**

**Responsibilities**
- Implement `TeacherController.cs`:
  - `POST /api/quizzes` → Create quiz
  - `POST /api/quizzes/{id}/questions` → Add question
  - `PUT /api/quizzes/{id}/status` → Enable/Disable
  - `GET /api/quizzes` → List all
  - `DELETE /api/quizzes/{id}` → Delete quiz
- Use DTOs from Task 3 and repository from Task 4

---

## ✅ Task 6 – Student API Controller  
👤 **Sardor**

**Responsibilities**
- Implement `StudentController.cs`:
  - `GET /api/student/quizzes` → List active quizzes
  - `GET /api/student/quizzes/{id}` → Load quiz
  - `POST /api/student/quizzes/{id}/submit` → Submit answers
- Use repository from Task 4 and grader from Task 7

---

## ✅ Task 7 – Quiz Grading Logic  
👤 **Sirojiddin**

**Responsibilities**
- Build `QuizGrader.cs` service:
  - `Evaluate(Quiz, List<AnswerSubmissionDto>)`
  - Apply time-limit logic:
    - Full credit if on time
    - Half credit if late
    - Zero if wrong/missing
  - Return score, percent, pass/fail, and per-question feedback

---

# 🔀 GitHub Workflow Rules

1. **Clone repo from** https://github.com/ilmhubuz/stackup-quiz-api and start working Task 1.
2. **Create a branch** for your task:
   ```bash
   git checkout -b feature/task-3-dtos
   ```
3. Work only within your assigned files.
4. Open a **Pull Request (PR)** to `main` branch.
5. Request **at last 1 team member for review**.
6. After approval, **merge into `main`**.

---
