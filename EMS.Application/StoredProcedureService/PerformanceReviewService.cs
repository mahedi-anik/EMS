using EMS.Application.DTOs;
using EMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EMS.Application.StoredProcedureService
{
    public class PerformanceReviewService
    {
        private readonly ApplicationDbContext _dbContext;

        public PerformanceReviewService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeePerformanceReviewDto>> GetEmployeePerformanceHistoryAsync(string employeeId)
        {
            var performanceHistory = new List<EmployeePerformanceReviewDto>();

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EmployeeWisePerformanceHistory"; // Stored Procedure Name
                    command.CommandType = CommandType.StoredProcedure;

                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@EmployeeId";
                    parameter.Value = employeeId;
                    command.Parameters.Add(parameter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            performanceHistory.Add(new EmployeePerformanceReviewDto
                            {
                                Department = reader["Department"].ToString(),
                                Manager = reader["Manager"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                DOJ = reader["DOJ"] == DBNull.Value
    ? DateTime.MinValue
    : DateTime.ParseExact(reader["DOJ"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                                Status = reader["Status"].ToString(),
                                ReviewDate = reader["ReviewDate"] == DBNull.Value
    ? DateTime.MinValue
    : DateTime.ParseExact(reader["ReviewDate"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),

                                ReviewScore = reader["ReviewScore"].ToString(),
                                ReviewNote = reader["ReviewNote"].ToString(),

                            });
                        }
                    }
                }
            }

            return performanceHistory;
        }
    }
}
