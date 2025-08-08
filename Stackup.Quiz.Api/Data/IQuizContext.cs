using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace Stackup.Quiz.Api.Data;

using Stackup.Quiz.Api.Entities;

public interface IQuizContext
{
    DbSet<Quiz> Quizzes { get; set; }

    DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}