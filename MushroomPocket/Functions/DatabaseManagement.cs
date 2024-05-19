using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MushroomPocket.Functions {
    public static class DatabaseManagementFunctions {
        public static void UpdateDB(DatabaseContext context) {
            context.SaveChanges();
            context.Database.ExecuteSqlInterpolated($"PRAGMA wal_checkpoint(FULL)");
            context.Dispose();
        }

        public static void RemoveTempFiles() {
            if (File.Exists("database.db-shm")) {  // remove to always ensure a clean database state
                File.Delete("database.db-shm");
            }
            if (File.Exists("database.db-wal")) { // also remove this to always ensure a clean database state
                File.Delete("database.db-wal");
            }
        }
    }
}