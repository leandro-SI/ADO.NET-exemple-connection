using Library.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Library.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertUsuario();

            var dt = new DataTable();

            var cmd = "sp_ListaUsuario";

            dt = (DataTable)GenericDataBase.ExecuteCommand(cmd, CommandType.StoredProcedure, null, TypeCommand.ExecuteDataTable);

            foreach (DataRow item in dt.Rows)
            {
                System.Console.WriteLine("O Usuário é o {0} que mora na Rua {1}", item["Nome"], item["Endereco"]);
            }


            System.Console.ReadKey();
        }

        static void InsertUsuario()
        {
            var query = string.Format("INSERT INTO Usuario(Nome, Endereco) VALUES('{0}', '{1}')", "Leandro", "Av. castro Alves");

            GenericDataBase.ExecuteCommand(query, CommandType.Text, null, TypeCommand.ExecuteNonQuery);

        }

    }
}
