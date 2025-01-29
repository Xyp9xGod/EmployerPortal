# Employer Portal API 🔒
Modern implementation of an employer portal with enhanced security measures and best practices.

## 🚨 Previous Vulnerabilities (Legacy System)
1. **SQL Injection**  
   Caused by raw SQL query concatenation with user input
   
2. **Information Disclosure**  
   Detailed error messages exposed system internals
   
3. **Missing Input Validation**  
   No sanitization of user-provided parameters

## 🛡️ Security Improvements
- ✅ Parameterized queries using Entity Framework Core
- ✅ Centralized error handling with generic messages
- ✅ Input validation pipeline
- ✅ HTTPS enforcement
- ✅ Secure headers configuration
- ✅ Audit logging for sensitive operations

## 🚀 Quick Start

### Prerequisites
- .NET 9 SDK
- SQL Server 2019+
- Visual Studio 2022 (ou VS Code)

```bash
# Clone o repositório
git clone https://github.com/Xyp9xGod/EmployerPortal.git

# Restaure as dependências
dotnet restore

# Execute o projeto
dotnet run --project EmployerPortal

# Execute todos os testes
dotnet test
