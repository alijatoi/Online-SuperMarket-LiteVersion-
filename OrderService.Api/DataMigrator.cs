using Microsoft.Data.Sqlite;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;

namespace OrderService.Api
{
    public class DataMigrator
    {
        public static void Migrate()
        {
            // 1️⃣ SQLite path
            var sqlitePath = @"D:\C#\Apps\SupermarketOrderServices\OrderService.Api\orders.db";

            if (!File.Exists(sqlitePath))
            {
                Console.WriteLine($"❌ SQLite file not found: {sqlitePath}");
                return;
            }

            Console.WriteLine($"✅ Found SQLite DB: {sqlitePath}");

            var sqliteConnString = $"Data Source={sqlitePath};";
            using var sqliteConn = new SqliteConnection(sqliteConnString);
            sqliteConn.Open();

            // 2️⃣ Load Orders
            var ordersTable = new DataTable();
            using (var cmd = sqliteConn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, CustomerId, TotalPrice, Status, CreatedAt FROM Orders";
                using var reader = cmd.ExecuteReader();
                ordersTable.Load(reader);
            }

            Console.WriteLine($"📦 Orders rows loaded: {ordersTable.Rows.Count}");

            // 3️⃣ Load OrderItems
            var itemsTable = new DataTable();
            using (var cmd = sqliteConn.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, OrderId, ProductId, Name, Price, Quantity FROM OrderItem";
                using var reader = cmd.ExecuteReader();
                itemsTable.Load(reader);
            }

            Console.WriteLine($"📦 OrderItem rows loaded: {itemsTable.Rows.Count}");

            if (ordersTable.Rows.Count == 0 && itemsTable.Rows.Count == 0)
            {
                Console.WriteLine("⚠️ No data found in SQLite database.");
                return;
            }

            // 4️⃣ Convert DataTypes for SQL Server compatibility
            ordersTable = ConvertTableForSqlServer(ordersTable);
            itemsTable = ConvertTableForSqlServer(itemsTable);

            // 5️⃣ SQL Server connection
            var sqlServerConnString = @"Server=(LocalDB)\MSSQLLocalDB;Database=SupermarketOrders;Trusted_Connection=True;";
            using var sqlConn = new SqlConnection(sqlServerConnString);
            sqlConn.Open();

            using var transaction = sqlConn.BeginTransaction();
            try
            {
                if (ordersTable.Rows.Count > 0)
                {
                    using var bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, transaction)
                    {
                        DestinationTableName = "Orders"
                    };
                    bulkCopy.WriteToServer(ordersTable);
                    Console.WriteLine("✅ Orders migrated successfully.");
                }

                if (itemsTable.Rows.Count > 0)
                {
                    using var bulkCopy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.KeepIdentity, transaction)
                    {
                        DestinationTableName = "OrderItem"
                    };
                    bulkCopy.WriteToServer(itemsTable);
                    Console.WriteLine("✅ OrderItem migrated successfully.");
                }

                transaction.Commit();
                Console.WriteLine("🎉 Data migration completed successfully!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"❌ Migration failed: {ex.Message}");
            }
        }

        // 6️⃣ Helper method: Convert SQLite string columns into proper SQL Server types
        private static DataTable ConvertTableForSqlServer(DataTable source)
        {
            var dest = new DataTable();

            // Create destination columns with correct types
            foreach (DataColumn col in source.Columns)
            {
                Type newType = col.DataType;

                // Only these are true GUIDs
                if (col.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                    col.ColumnName.Equals("OrderId", StringComparison.OrdinalIgnoreCase) ||
                    col.ColumnName.Equals("ProductId", StringComparison.OrdinalIgnoreCase))
                {
                    newType = typeof(Guid);
                }

                // Decimal columns
                if (col.ColumnName.Contains("Price", StringComparison.OrdinalIgnoreCase))
                    newType = typeof(decimal);

                dest.Columns.Add(new DataColumn(col.ColumnName, newType));
            }

            // Copy and convert rows
            foreach (DataRow srcRow in source.Rows)
            {
                var destRow = dest.NewRow();
                foreach (DataColumn col in source.Columns)
                {
                    var val = srcRow[col];
                    if (val == DBNull.Value)
                    {
                        destRow[col.ColumnName] = DBNull.Value;
                        continue;
                    }

                    var destType = dest.Columns[col.ColumnName].DataType;
                    if (destType == typeof(Guid))
                    {
                        if (Guid.TryParse(val.ToString(), out Guid g))
                            destRow[col.ColumnName] = g;
                        else
                            destRow[col.ColumnName] = DBNull.Value;
                    }
                    else if (destType == typeof(decimal))
                    {
                        if (decimal.TryParse(val.ToString(), out decimal d))
                            destRow[col.ColumnName] = d;
                        else
                            destRow[col.ColumnName] = DBNull.Value;
                    }
                    else
                    {
                        destRow[col.ColumnName] = val;
                    }
                }
                dest.Rows.Add(destRow);
            }

            return dest;
        }
    }
}
