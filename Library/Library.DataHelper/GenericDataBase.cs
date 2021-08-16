using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace Library.DataHelper
{
    public class GenericDataBase
    {

        public static DbCommand CreateCommand(string cmdText, CommandType cmnType, List<DbParameter> listParameters)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);

            var conn = factory.CreateConnection();

            conn.ConnectionString = ConnectionDB.ConnectionString;

            var comn = conn.CreateCommand();

            comn.CommandText = cmdText;

            comn.CommandType = cmnType;

            if (listParameters != null)
            {
                foreach(var param in listParameters)
                {
                    comn.Parameters.Add(param);
                }
            }

            return comn;


        }

        public static DbParameter CreateParameter(string nameParameter, DbType typeParameter, Object valueParameter)
        {
            var factory = DbProviderFactories.GetFactory(ConnectionDB.ProviderName);

            var param = factory.CreateParameter();

            if (param != null)
            {
                param.ParameterName = nameParameter;
                param.DbType = typeParameter;
                param.Value = valueParameter;
            }

            return param;
        }

        public static Object ExecuteCommand(string cmdText, CommandType cmdType, List<DbParameter> listParameter, TypeCommand typeCmd)
        {

            var command = CreateCommand(cmdText, cmdType, listParameter);
            Object Objretorno = null;

            try
            {
                command.Connection.Open();

                switch (typeCmd)
                {
                    case TypeCommand.ExecuteNonQuery:
                        Objretorno = command.ExecuteNonQuery();
                        break;
                    case TypeCommand.ExecuteReader:
                        Objretorno = command.ExecuteReader();
                        break;
                    case TypeCommand.ExecuteScalar:
                        Objretorno = command.ExecuteScalar();
                        break;
                    case TypeCommand.ExecuteDataTable:
                        var table = new DataTable();
                        var reader = command.ExecuteReader();

                        table.Load(reader);

                        reader.Close();
                        Objretorno = table;
                        break;

                    default:
                        break;
                }

            }
            catch (Exception)
            {

                throw new Exception("Erro ao executar Execute Comando");
            }
            finally
            {
                if (typeCmd != TypeCommand.ExecuteReader)
                {
                    if (command.Connection.State == ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }
                    command.Connection.Dispose();
                }
            }

            return Objretorno;

        }

    }

    public enum TypeCommand
    {
        ExecuteNonQuery,
        ExecuteReader,
        ExecuteScalar,
        ExecuteDataTable
    }
}
