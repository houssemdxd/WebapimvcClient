using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WebAppTemplate.Models
{
    public partial class Article
    {
        public class Access
        {
            #region Default Methods
            public static Article Get(long Id)
            {
                try
                {
                    MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                    DataTable dt = new DataTable();
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "SELECT * FROM Article WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Id", Id);

                        SelectAdapter = new MySqlDataAdapter(cmd);
                        SelectAdapter.Fill(dt);

                    }

                    if (dt.Rows.Count > 0)
                    {
                        return new Article(dt.Rows[0]);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static List<Article> Get()
            {
                try
                {
                    MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                    DataTable dt = new DataTable();
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "SELECT * FROM Article";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);

                        SelectAdapter = new MySqlDataAdapter(cmd);
                        SelectAdapter.Fill(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        return Helpers.GetListFromDataTable(dt);
                    }
                    else
                    {
                        return new List<Article>();
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static List<Article> Get(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int MAX_QUERY_SIZE = Config.MAX_BDMS_PARAMS_NUM;
                        List<Article> Results = null;
                        if (LIds.Count <= MAX_QUERY_SIZE)
                        {
                            Results = get(LIds);
                        }
                        else
                        {
                            int batchSize = LIds.Count / MAX_QUERY_SIZE;
                            Results = new List<Article>();
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results.AddRange(get(LIds.GetRange(i * MAX_QUERY_SIZE, MAX_QUERY_SIZE)));
                            }
                            Results.AddRange(get(LIds.GetRange(batchSize * MAX_QUERY_SIZE, LIds.Count - batchSize * MAX_QUERY_SIZE)));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return new List<Article>();
            }
            private static List<Article> get(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                        DataTable dt = new DataTable();
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = cnn;

                            string queryIds = string.Empty;
                            for (int i = 0; i < LIds.Count; i++)
                            {
                                queryIds += "@Id" + i + ",";
                                cmd.Parameters.AddWithValue("Id" + i, LIds[i]);
                            }
                            queryIds = queryIds.TrimEnd(',');

                            cmd.CommandText = "SELECT * FROM Article WHERE Id IN (" + queryIds + ")";
                            SelectAdapter = new MySqlDataAdapter(cmd);
                            SelectAdapter.Fill(dt);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            return Helpers.GetListFromDataTable(dt);
                        }
                        else
                        {
                            return new List<Article>();
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return new List<Article>();
            }
            public static async Task<Article> GetAsync(long Id)
            {
                try
                {
                    MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                    DataTable dt = new DataTable();
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "SELECT * FROM Article WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Id", Id);

                        SelectAdapter = new MySqlDataAdapter(cmd);
                        await SelectAdapter.FillAsync(dt);

                    }

                    if (dt.Rows.Count > 0)
                    {
                        return new Article(dt.Rows[0]);
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static async Task<List<Article>> GetAsync()
            {
                try
                {
                    MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                    DataTable dt = new DataTable();
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "SELECT * FROM Article";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);

                        SelectAdapter = new MySqlDataAdapter(cmd);
                        await SelectAdapter.FillAsync(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        return Helpers.GetListFromDataTable(dt);
                    }
                    else
                    {
                        return new List<Article>();
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static async Task<List<Article>> GetAsync(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int MAX_QUERY_SIZE = Config.MAX_BDMS_PARAMS_NUM;
                        List<Article> Results = null;
                        if (LIds.Count <= MAX_QUERY_SIZE)
                        {
                            Results = await getAsync(LIds);
                        }
                        else
                        {
                            int batchSize = LIds.Count / MAX_QUERY_SIZE;
                            Results = new List<Article>();
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results.AddRange(await getAsync(LIds.GetRange(i * MAX_QUERY_SIZE, MAX_QUERY_SIZE)));
                            }
                            Results.AddRange(await getAsync(LIds.GetRange(batchSize * MAX_QUERY_SIZE, LIds.Count - batchSize * MAX_QUERY_SIZE)));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return new List<Article>();
            }
            private static async Task<List<Article>> getAsync(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        MySqlDataAdapter SelectAdapter = new MySqlDataAdapter();
                        DataTable dt = new DataTable();
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = cnn;

                            string queryIds = string.Empty;
                            for (int i = 0; i < LIds.Count; i++)
                            {
                                queryIds += "@Id" + i + ",";
                                cmd.Parameters.AddWithValue("Id" + i, LIds[i]);
                            }
                            queryIds = queryIds.TrimEnd(',');

                            cmd.CommandText = "SELECT * FROM Article WHERE Id IN (" + queryIds + ")";
                            SelectAdapter = new MySqlDataAdapter(cmd);
                            await SelectAdapter.FillAsync(dt);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            return Helpers.GetListFromDataTable(dt);
                        }
                        else
                        {
                            return new List<Article>();
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return new List<Article>();
            }

            public static int Add(Article T)
            {
                try
                {
                    int InsertedID = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "INSERT INTO Article(Code,Description,Quantity)  VALUES (@Code,@Description,@Quantity)";

                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Code", T.Code);
                        cmd.Parameters.AddWithValue("Description", T.Description);
                        cmd.Parameters.AddWithValue("Quantity", T.Quantity == null ? (object)DBNull.Value : T.Quantity);

                        InsertedID = cmd.ExecuteNonQuery();

                    }

                    return InsertedID;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static int Add(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM / 4; // Nb params per query
                        int Results = 0;
                        if (Lt.Count <= MAX_Params_Number)
                        {
                            Results = add(Lt);
                        }
                        else
                        {
                            int batchSize = Lt.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += add(Lt.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += add(Lt.GetRange(batchSize * MAX_Params_Number, Lt.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            private static int add(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            string query = "";
                            MySqlCommand cmd = new MySqlCommand(query, cnn);

                            int i = 0;
                            foreach (Article t in Lt)
                            {
                                i++;
                                query += " INSERT INTO Article(Code,Description,Quantity) VALUES( "

                                    + "@Code" + i + ","
                                    + "@Description" + i + ","
                                    + "@Quantity" + i
                                     + "); ";


                                cmd.Parameters.AddWithValue("Code" + i, t.Code);
                                cmd.Parameters.AddWithValue("Description" + i, t.Description);
                                cmd.Parameters.AddWithValue("Quantity" + i, t.Quantity == null ? (object)DBNull.Value : t.Quantity);
                            }

                            cmd.CommandText = query;

                            r = cmd.ExecuteNonQuery();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            public static async Task<int> AddAsync(Article T)
            {
                try
                {
                    int InsertedID = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "INSERT INTO Article(Code,Description,Quantity)  VALUES (@Code,@Description,@Quantity)";

                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Code", T.Code);
                        cmd.Parameters.AddWithValue("Description", T.Description);
                        cmd.Parameters.AddWithValue("Quantity", T.Quantity == null ? (object)DBNull.Value : T.Quantity);

                        InsertedID = await cmd.ExecuteNonQueryAsync();

                    }

                    return InsertedID;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static async Task<int> AddAsync(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM / 4; // Nb params per query
                        int Results = 0;
                        if (Lt.Count <= MAX_Params_Number)
                        {
                            Results = await addAsync(Lt);
                        }
                        else
                        {
                            int batchSize = Lt.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += await addAsync(Lt.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += await addAsync(Lt.GetRange(batchSize * MAX_Params_Number, Lt.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            private static async Task<int> addAsync(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            string query = "";
                            MySqlCommand cmd = new MySqlCommand(query, cnn);

                            int i = 0;
                            foreach (Article t in Lt)
                            {
                                i++;
                                query += " INSERT INTO Article(Code,Description,Quantity) VALUES( "

                                    + "@Code" + i + ","
                                    + "@Description" + i + ","
                                    + "@Quantity" + i
                                     + "); ";


                                cmd.Parameters.AddWithValue("Code" + i, t.Code);
                                cmd.Parameters.AddWithValue("Description" + i, t.Description);
                                cmd.Parameters.AddWithValue("Quantity" + i, t.Quantity == null ? (object)DBNull.Value : t.Quantity);
                            }

                            cmd.CommandText = query;

                            r = await cmd.ExecuteNonQueryAsync();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }

            public static int Edit(Article T)
            {
                try
                {
                    int r = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "UPDATE Article SET Code=@Code,Description=@Description,Quantity=@Quantity WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);

                        cmd.Parameters.AddWithValue("Id", T.Id);
                        cmd.Parameters.AddWithValue("Code", T.Code);
                        cmd.Parameters.AddWithValue("Description", T.Description);
                        cmd.Parameters.AddWithValue("Quantity", T.Quantity == null ? (object)DBNull.Value : T.Quantity);

                        r = cmd.ExecuteNonQuery();
                    }

                    return r;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static int Edit(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM / 4; // Nb params per query
                        int Results = 0;
                        if (Lt.Count <= MAX_Params_Number)
                        {
                            Results = edit(Lt);
                        }
                        else
                        {
                            int batchSize = Lt.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += edit(Lt.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += edit(Lt.GetRange(batchSize * MAX_Params_Number, Lt.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            private static int edit(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            string query = "";
                            MySqlCommand cmd = new MySqlCommand(query, cnn);

                            int i = 0;
                            foreach (Article t in Lt)
                            {
                                i++;
                                query += " UPDATE Article SET "

                                    + "Code=@Code" + i + ","
                                    + "Description=@Description" + i + ","
                                    + "Quantity=@Quantity" + i + " WHERE Id=@Id" + i
                                     + "; ";

                                cmd.Parameters.AddWithValue("Id" + i, t.Id);
                                cmd.Parameters.AddWithValue("Code" + i, t.Code);
                                cmd.Parameters.AddWithValue("Description" + i, t.Description);
                                cmd.Parameters.AddWithValue("Quantity" + i, t.Quantity == null ? (object)DBNull.Value : t.Quantity);
                            }

                            cmd.CommandText = query;

                            r = cmd.ExecuteNonQuery();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            public static async Task<int> EditAsync(Article T)
            {
                try
                {
                    int r = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "UPDATE Article SET Code=@Code,Description=@Description,Quantity=@Quantity WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);

                        cmd.Parameters.AddWithValue("Id", T.Id);
                        cmd.Parameters.AddWithValue("Code", T.Code);
                        cmd.Parameters.AddWithValue("Description", T.Description);
                        cmd.Parameters.AddWithValue("Quantity", T.Quantity == null ? (object)DBNull.Value : T.Quantity);

                        r = await cmd.ExecuteNonQueryAsync();
                    }

                    return r;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static async Task<int> EditAsync(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM / 4; // Nb params per query
                        int Results = 0;
                        if (Lt.Count <= MAX_Params_Number)
                        {
                            Results = await editAsync(Lt);
                        }
                        else
                        {
                            int batchSize = Lt.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += await editAsync(Lt.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += await editAsync(Lt.GetRange(batchSize * MAX_Params_Number, Lt.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }
            private static async Task<int> editAsync(List<Article> Lt)
            {
                if (Lt != null && Lt.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            string query = "";
                            MySqlCommand cmd = new MySqlCommand(query, cnn);

                            int i = 0;
                            foreach (Article t in Lt)
                            {
                                i++;
                                query += " UPDATE Article SET "

                                    + "Code=@Code" + i + ","
                                    + "Description=@Description" + i + ","
                                    + "Quantity=@Quantity" + i + " WHERE Id=@Id" + i
                                     + "; ";

                                cmd.Parameters.AddWithValue("Id" + i, t.Id);
                                cmd.Parameters.AddWithValue("Code" + i, t.Code);
                                cmd.Parameters.AddWithValue("Description" + i, t.Description);
                                cmd.Parameters.AddWithValue("Quantity" + i, t.Quantity == null ? (object)DBNull.Value : t.Quantity);
                            }

                            cmd.CommandText = query;

                            r = await cmd.ExecuteNonQueryAsync();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }

                return -1;
            }

            public static int Delete(long Id)
            {
                try
                {
                    int r = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "DELETE FROM Article WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Id", Id);

                        r = cmd.ExecuteNonQuery();
                    }

                    return r;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static int Delete(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM;
                        int Results = 0;
                        if (LIds.Count <= MAX_Params_Number)
                        {
                            Results = delete(LIds);
                        }
                        else
                        {
                            int batchSize = LIds.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += delete(LIds.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += delete(LIds.GetRange(batchSize * MAX_Params_Number, LIds.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return -1;
            }
            private static int delete(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = cnn;

                            string queryIds = string.Empty;
                            for (int i = 0; i < LIds.Count; i++)
                            {
                                queryIds += "@Id" + i + ",";
                                cmd.Parameters.AddWithValue("Id" + i, LIds[i]);
                            }
                            queryIds = queryIds.TrimEnd(',');

                            string query = "DELETE FROM Article WHERE Id IN (" + queryIds + ")";
                            cmd.CommandText = query;

                            r = cmd.ExecuteNonQuery();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return -1;
            }
            public static async Task<int> DeleteAsync(long Id)
            {
                try
                {
                    int r = -1;
                    using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                    {
                        cnn.Open();
                        string query = "DELETE FROM Article WHERE Id=@Id";
                        MySqlCommand cmd = new MySqlCommand(query, cnn);
                        cmd.Parameters.AddWithValue("Id", Id);

                        r = await cmd.ExecuteNonQueryAsync();
                    }

                    return r;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            public static async Task<int> DeleteAsync(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int MAX_Params_Number = Config.MAX_BDMS_PARAMS_NUM;
                        int Results = 0;
                        if (LIds.Count <= MAX_Params_Number)
                        {
                            Results = await deleteAsync(LIds);
                        }
                        else
                        {
                            int batchSize = LIds.Count / MAX_Params_Number;
                            for (int i = 0; i < batchSize; i++)
                            {
                                Results += await deleteAsync(LIds.GetRange(i * MAX_Params_Number, MAX_Params_Number));
                            }
                            Results += await deleteAsync(LIds.GetRange(batchSize * MAX_Params_Number, LIds.Count - batchSize * MAX_Params_Number));
                        }
                        return Results;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return -1;
            }
            private static async Task<int> deleteAsync(List<long> LIds)
            {
                if (LIds != null && LIds.Count > 0)
                {
                    try
                    {
                        int r = -1;
                        using (MySqlConnection cnn = new MySqlConnection(Config.GetConnectionString()))
                        {
                            cnn.Open();
                            MySqlCommand cmd = new MySqlCommand();
                            cmd.Connection = cnn;

                            string queryIds = string.Empty;
                            for (int i = 0; i < LIds.Count; i++)
                            {
                                queryIds += "@Id" + i + ",";
                                cmd.Parameters.AddWithValue("Id" + i, LIds[i]);
                            }
                            queryIds = queryIds.TrimEnd(',');

                            string query = "DELETE FROM Article WHERE Id IN (" + queryIds + ")";
                            cmd.CommandText = query;

                            r = await cmd.ExecuteNonQueryAsync();
                        }

                        return r;
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }
                }
                return -1;
            }
            #endregion

            #region Custom Methods


            #endregion

            #region Helpers
            public class Helpers
            {
                public static List<Article> GetListFromDataTable(DataTable dt)
                {
                    List<Article> L = new List<Article>(dt.Rows.Count);
                    foreach (DataRow dr in dt.Rows)
                    { L.Add(new Article(dr)); }
                    return L;
                }
            }
            #endregion
        }
    }
}