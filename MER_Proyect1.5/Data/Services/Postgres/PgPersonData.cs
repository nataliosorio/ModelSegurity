//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Data.Interfaces;
//using Entity.Context;
//using Entity.Model;
//using Microsoft.Extensions.Logging;

//namespace Data.Services.Postgres
//{
//    ///<summary>
//    /// Clase encargada de la gestión de la entidad Person en la base de datos Postgres, haciendo uso de una interfaz.
//    /// </summary>
//    public class PgPersonData :  IRepository<Person>
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly ILogger<PgPersonData> _logger;
//        public PgPersonData(ApplicationDbContext context, ILogger<PgPersonData> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        public async Task<IEnumerable<Person>> GetAllAsync()
//        {
//            try
//            {
//                string query = @"SELECT 
//                                    id, 
//                                    firstname, 
//                                    lastname, 
//                                    phonenumber, 
//                                    active, 
//                                    isdeleted 
//                                FROM person
//                                WHERE active = true";


//                return await _context.QueryAsync<Person>(query);
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
//                                    phonenumber,
//                                    Active
//                                FROM Person 
//                                WHERE Id = @Id AND Active = true";

//                return await _context.QueryFirstOrDefaultAsync<Person>(Query, new { Id = id });
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al obtener Persona con el ID {PersonaId}", id);
//                throw;
//            }
//        }
//        public async Task<Person> CreateAsync(Person person)
//        {
//            try
//            {              
//                string query = @"
//                                INSERT INTO Person (FirstName, LastName, phonenumber, Active, IsDeleted ) 
//                                VALUES (@FirstName, @LastName, @PhoneNumber, true, false)
//                                RETURNING Id;";

//                person.Id = await _context.QueryFirstOrDefaultAsync<int>(query, new
//                {
//                    person.FirstName,
//                    person.LastName,
//                    person.PhoneNumber,
//                });

//                return person;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al crear la persona.");
//                throw;
//            }
//        }
//        public async Task<bool> UpdateAsync(Person person)
//        {
//            try
//            {             

//                string query = @"
//                                UPDATE Person 
//                                SET FirstName = @FirstName, 
//                                    LastName = @LastName,
//                                    phonenumber = @PhoneNumber
//                                WHERE Id = @Id;";

//                int rowsAffected = await _context.ExecuteAsync(query, new
//                {
//                    person.Id,
//                    person.FirstName,
//                    person.LastName,
//                    person.PhoneNumber
//                });

//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al actualizar la persona con ID {PersonId}", person.Id);
//                throw;
//            }
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            try
//            {
//                string query = @"UPDATE Person
//                               SET IsDeleted = true
//                               WHERE Id=@Id";

//                int rowsAffected = await _context.ExecuteAsync(query, new { id });
//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al eliminar lógicamente la persona con ID {PersonId}", id);
//                return false;
//            }
//        }

//        public async Task<bool> DeleteLogicalAsync(int id)
//        {
//            try
//            {
//                string query = @"
//                                DELETE FROM Person
//                                WHERE Id = @Id";

//                int rowsAffected = await _context.ExecuteAsync(query, new { Id = id });


//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error al crear la persona.");
//                return false;
//            }
//        }



//    }
//}
