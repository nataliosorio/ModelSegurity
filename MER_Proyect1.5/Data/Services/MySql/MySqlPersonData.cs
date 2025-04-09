//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Text;
//using System.Threading.Tasks;
//using Data.Interfaces;
//using Entity.Context;
//using Entity.Model;
//using Microsoft.Extensions.Logging;

//namespace Data.Services.MySql
//{
//    ///<summary>
//    /// Clase encargada de la gestión de la entidad Person en la base de datos MySql, haciendo uso de una interfaz.
//    /// </summary>
//    public class MySqlPersonData : IRepository<Person>
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly ILogger<MySqlPersonData> _logger;

//        public MySqlPersonData(ApplicationDbContext context, ILogger<MySqlPersonData> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }
//        public async Task<IEnumerable<Person>> GetAllAsync()
//        {
//            try
//            {
//                string Query = @"SELECT 
//                                    Id, 
//                                    FirstName, 
//                                    LastName, 
//                                    PhoneNumber,
//                                    Active
//                                FROM Person 
//                                WHERE Active = 1";

//                return await _context.QueryAsync<Person>(Query);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al obtener Los Person");
//                throw; // Relanza la excepcion  para q sea manejada por las capas superiores
//            }
//        }

//        public async Task<Person?> GetByIdAsync(int id)
//        {
//            try
//            {
//                string Query = @"SELECT 
//                                    Id,
//                                    FirstName, 
//                                    LastName, 
//                                    CONCAT(FirstName, ' ', LastName) AS NameCompleted, 
//                                    PhoneNumber,
//                                    Active
//                                FROM Person 
//                                WHERE Id = @Id AND Active = 1";

//                return await _context.QueryFirstOrDefaultAsync<Person>(Query, new { Id = id });
//            }
//            catch(Exception ex)
//            {
//                _logger.LogError(ex, "Error al obtener la persona con ID: {PersonId}", id);
//                throw; // Relanza la excepcion  para q sea manejada por las capas superiores
//            }
//        }
//        public async Task<Person> CreateAsync(Person person)
//        {
//            try
//            {
//                string insertQuery = @"INSERT INTO Person (firstname, LastName, PhoneNumber, Active, IsDeleted) 
//                               VALUES (@FirstName, @LastName, @PhoneNumber, 1, 0);
//                               SELECT LAST_INSERT_ID();";

//                var parameters = new
//                {
//                    person.firstname,
//                    person.lastname,
//                    person.phonenumber,
//                };

//                var id = await _context.QueryFirstOrDefaultAsync<int>(insertQuery, parameters);

//                person.id = id;
//                return person;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al crear la persona");
//                throw;
//            }
//        }



//        public async Task<bool> UpdateAsync(Person person)
//        {
//            try
//            {
//                string Query = @"UPDATE Person 
//                                SET FirstName = @FirstName, 
//                                    LastName = @LastName, 
//                                    PhoneNumber = @PhoneNumber, 
//                                    Active = @Active
//                                WHERE Id = @Id";
                
//                object Parameter = new
//                {
//                    person.irstName,
//                    person.LastName,
//                    person.PhoneNumber,
//                    person.Active,
//                    person.Id
//                };
//                int rowAffected = await _context.ExecuteAsync(Query, Parameter);
//                return rowAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al actualizar la persona con ID: {PersonId}", person.Id);
//                throw; // Relanza la excepcion  para q sea manejada por las capas superiores
//            }
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            try
//            {
//                string Query = @"DELETE FROM Person 
//                                WHERE Id = @Id";
//                int rowsAffected = await _context.ExecuteAsync(Query, new { Id = id });
//                return rowsAffected > 0;

//            }
//            catch(Exception ex)
//            {
//                _logger.LogError(ex, "Error al eliminar la persona con ID: {PersonId}", id);
//                throw; // Relanza la excepcion  para q sea manejada por las capas superiores
//            }
//        }

//        public async Task<bool> DeleteLogicalAsync(int id)
//        {
//            try
//            {
//                string Query = @"UPDATE Person 
//                                SET IsDeleted = 0
//                                WHERE Id = @Id";

//                int rowsAffected = await _context.ExecuteAsync(Query, new { Id = id });
//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al eliminar la persona con ID: {PersonId}", id);
//                throw; // Relanza la excepcion  para q sea manejada por las capas superiores
//            }
//        }

//    }
//}
