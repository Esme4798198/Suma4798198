using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suma4798198
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            //Le indica al sistema qe cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Resultado>();
        }
        //Método para listar los registros de nuestra tabla

        public async Task<List<Resultado>> GetResultado()
        {
            return await _connection.Table<Resultado>().ToListAsync();
        }

        //Método para crear registros 
        public async Task Create(Resultado resultado)
        {
            await _connection.InsertAsync(resultado);
        }
        //Método para actualizar
        public async Task Update(Resultado resultado)
        {
            await  _connection.UpdateAsync(resultado);
        }
        //Método para eliminar
        public async Task Delete(Resultado resultado)
        {
            await _connection.DeleteAsync(resultado);
        }
    }
}
