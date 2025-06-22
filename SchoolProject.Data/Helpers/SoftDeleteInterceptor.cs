using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SchoolProject.Data.Helpers
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            Console.WriteLine("🔥 Interceptor Triggered");

            if (eventData.Context is null)
                return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                Console.WriteLine($"➡️ Entry: {entry.Entity.GetType().Name}, State: {entry.State}");

                if (entry.State == EntityState.Deleted && entry.Entity is ISoftDeleteable entity)
                {
                    Console.WriteLine("🛑 Converting to soft delete");
                    entity.Delete();
                    entry.State = EntityState.Modified;
                }
            }

            return result;
        }
    }
}