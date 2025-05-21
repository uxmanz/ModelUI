using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using System;

namespace ModelUI.Views
{
    public class DatabaseHelper
    {

        public DatabaseHelper()
        {
            CreateGeneralDataTable("GeneralSettings");
        }
        // Absolute path to avoid issues with relative paths
        private readonly string connectionString = Path.Combine(AppContext.BaseDirectory, "Data.db");

        /// <summary>
        /// Creates a table with the specified name, if it does not already exist.
        /// </summary>
        public void CreateGeneralDataTable(string name)
        {
            using var connection = new SQLiteConnection(connectionString);
            string sql = $@"CREATE TABLE IF NOT EXISTS GEN{name} (
                            ID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT,
                            JSON TEXT,
                            AnyData TEXT)";
            var command = new SQLiteCommand(connection)
            {
                CommandText = sql
            };
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Retrieves a key-value pair by key. If not found, inserts the default.
        /// </summary>
        public generalDataClass GetKeyValue(string key, generalDataClass defaultValue)
        {
            var result = GetDataByName("GeneralSettings", key);
            if (result == null)
            {
                InsertUpdateInTable(defaultValue, "GeneralSettings");
                result = GetDataByName("GeneralSettings", key);
            }
            return result!;
        }

        /// <summary>
        /// Gets all records from a given table.
        /// </summary>
        public List<generalDataClass> GetAllDataFromTable(string tableName)
        {
            using var connection = new SQLiteConnection(connectionString);
            var command = new SQLiteCommand(connection)
            {
                CommandText = $"SELECT * FROM GEN{tableName}"
            };
            return command.ExecuteQuery<generalDataClass>();
        }

        /// <summary>
        /// Returns a single entry by name. If not present, inserts the default.
        /// </summary>
        public generalDataClass GetSingleEntryFromTable(string tableName, string name, generalDataClass defaultValue)
        {
            var value = GetDataByName(tableName, name);
            if (value == null)
            {
                InsertUpdateInTable(defaultValue, tableName);
                value = GetDataByName(tableName, name);
            }
            return value!;
        }

        /// <summary>
        /// Inserts or updates a record into a table.
        /// </summary>
        public void InsertUpdateInTable(generalDataClass newData, string tableName)
        {
            var existing = GetDataByName(tableName, newData.Name);
            if (existing != null)
            {
                UpdateInTable(newData, tableName);
            }
            else
            {
                InsertInTable(newData, tableName);
            }
        }

        /// <summary>
        /// Deletes a record by ID.
        /// </summary>
        public void DeleteIDFromTable(int id, string tableName)
        {
            using var connection = new SQLiteConnection(connectionString);
            var command = new SQLiteCommand(connection)
            {
                CommandText = $"DELETE FROM GEN{tableName} WHERE ID=@ID"
            };
            command.Bind("@ID", id);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Inserts a new record.
        /// </summary>
        public void InsertInTable(generalDataClass newData, string tableName)
        {
            using var connection = new SQLiteConnection(connectionString);
            var command = new SQLiteCommand(connection)
            {
                CommandText = $"INSERT INTO GEN{tableName} (Name, JSON, AnyData) VALUES (@Name, @JSON, @AnyData)"
            };
            command.Bind("@Name", newData.Name);
            command.Bind("@JSON", newData.JSON);
            command.Bind("@AnyData", newData.AnyData);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Updates an existing record.
        /// </summary>
        public void UpdateInTable(generalDataClass newData, string tableName)
        {
            using var connection = new SQLiteConnection(connectionString);
            var command = new SQLiteCommand(connection)
            {
                CommandText = $"UPDATE GEN{tableName} SET JSON=@JSON, AnyData=@AnyData WHERE Name=@Name"
            };
            command.Bind("@JSON", newData.JSON);
            command.Bind("@AnyData", newData.AnyData);
            command.Bind("@Name", newData.Name);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Returns a single record by name or null.
        /// </summary>
        private generalDataClass? GetDataByName(string tableName, string name)
        {
            using var connection = new SQLiteConnection(connectionString);
            var command = new SQLiteCommand(connection)
            {
                CommandText = $"SELECT * FROM GEN{tableName} WHERE Name=@Name"
            };
            command.Bind("@Name", name);
            var results = command.ExecuteQuery<generalDataClass>();
            return results.FirstOrDefault();
        }
    }
}
