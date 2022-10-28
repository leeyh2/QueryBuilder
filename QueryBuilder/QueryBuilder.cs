using System.Reflection;
using System.Text;
using Microsoft.Data.Sqlite;

namespace QueryBuilder
{
    public class QueryBuilder : IDisposable
    {
        // db connection referenced by the 'connection' field
        private SqliteConnection connection;

        /// <summary>
        /// Constructor will set up our connection to a given SQLite database file and open it.
        /// </summary>
        /// <param name="databaseLocation">File path to a .db file</param>
        public QueryBuilder(string databaseLocation)
        {
            connection = new SqliteConnection("Data Source=" + databaseLocation);
            connection.Open();
        }

        /// <summary>
        /// By implementing IDisposable, we have the capability to 
        /// use a QueryBuilder object in a 'using' statement in our
        /// driver; when that using statement is complete, our Sqlite
        /// connection will be closed automatically
        /// </summary>
        public void Dispose()
        {
            connection.Dispose();
        }
       
        public T Read<T>(int id) where T : IClassModel, new()
        {
            var command = connection.CreateCommand();

            // add text to command
            command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id = {id}";
            var reader = command.ExecuteReader();
            T data;
            data = new T();
          
            while (reader.Read())
            {
                
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                
            }
            return data;

            
        }
        //not finished
        public void Create<T>(T obj)
        {
            //I still don't understand the Create Method. What is it doing?
            //Is it creating a table?
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> values = new List<string>();
            List<string> names = new List<string>();
         

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());
                names.Add(property.Name);
            }

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            for(int i = 0; i < values.Count; i++)
            {
                if(i == values.Count -1)
                {
                    sbValues.Append($"{ values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}, ");
                }
            }

            var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";

            var insert = command.ExecuteNonQuery();

        }
        public void Update<T>(T obj, int id)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> values = new List<string>();
            List<string> names = new List<string>();


            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    values.Add("\"" + property.GetValue(obj) + "\"");
                }
                else
                    values.Add(property.GetValue(obj).ToString());
                names.Add(property.Name);
            }

            StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

            for (int i = 0; i < values.Count; i++)
            {
                if (i == values.Count - 1)
                {
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
                }
                else
                {
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}, ");
                }
            }

            var command = connection.CreateCommand();
            command.CommandText = $"UPDATE {typeof(T).Name} SET ({sbNames}) = ({sbValues}) WHERE Id = {id}";

            var update = command.ExecuteNonQuery();
        }
        public void Delete<T>(T obj) where T : IClassModel, new ()
        {
            var command = connection.CreateCommand();

           //ask what is the parameter used for
           //Ask am i just deleting everything from the table?
            command.CommandText = $"DELETE  FROM {typeof(T).Name} ";
         
            var execute = command.ExecuteNonQuery();
        }

        public List<T> ReadAll<T>() where T : IClassModel, new()
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT * FROM {typeof(T).Name}";
            var reader = command.ExecuteReader();
            T data;
            var datas = new List<T>();
            while (reader.Read())
            {
                data = new T();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (typeof(T).GetProperty(reader.GetName(i)).PropertyType == typeof(int))
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    else
                        typeof(T).GetProperty(reader.GetName(i)).SetValue(data, reader.GetValue(i));
                }
                datas.Add(data);
            }
            return datas;
        }
    }
}
