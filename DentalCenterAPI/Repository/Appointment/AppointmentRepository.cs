using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Appointment.Basic;
using DentalCenterAPI.Models.Appointment.Business;
using DentalCenterAPI.Models.Patient.Basic;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.Appointment
{
    public class AppointmentRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public AppointmentRepository()
        {
            _db = new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<AppointmentBusinessModel>> GetAllByTypeAsync(string type)
        {
            string query = @"SELECT * FROM   appointment
                            INNER JOIN patient ON appointment.patientid = patient.patientid 
                            Where LOWER(TYPE) =@type ";
            var result = await _db.QueryAsync<AppointmentBusinessModel, PatientBasicModel, AppointmentBusinessModel>(query,
            (appointment, patient) =>
            {
                appointment.Patient = patient;
                return appointment;
            },
            new { type },
            splitOn: "patientid"
            );
            return result;
        }

        public async Task<AppointmentBusinessModel> GetByIDAsync(Guid appointmentID)
        {
            string query = @"SELECT * FROM   appointment
                            INNER JOIN patient ON appointment.patientid = patient.patientid 
                            Where appointmentID = @appointmentID";
            var result = await _db.QueryAsync<AppointmentBusinessModel, PatientBasicModel, AppointmentBusinessModel>(query,
            (appointment, patient) =>
            {
                appointment.Patient = patient;
                return appointment;
            },
            new { appointmentID },
            splitOn: "patientid"
            );
            return result.FirstOrDefault();
        }

        public async Task<Guid?> AddAsync(AppointmentAddBusinessModel model)
        {
            string query = @"
            DECLARE @TBL TABLE(col1 UNIQUEIDENTIFIER)
            DECLARE @pateintID UNIQUEIDENTIFIER

            --Insert Patient
            INSERT INTO [dbo].[patient] ([name] ,[email] ,[phonenumber] ,[address] ,[contactmethod] ,[birthdate] ,[firstknowusfrom] ,
                                    [secondknowusfrom] ,[patientstatus] ,[country] ,[nationality] ,[isresident] ,[insertdate]) output inserted.patientid INTO @tbl 
            VALUES (@name ,@email ,@phonenumber,@address ,@contactmethod ,@birthdate ,@firstknowusfrom ,@secondknowusfrom ,@patientstatus ,@country ,@nationality ,
                                    @isresident ,@insertdate)

            SELECT @pateintID = col1  FROM   @tbl
            IF(@pateintID IS NOT NULL )
            BEGIN
            INSERT INTO [dbo].[appointment] ( [servicename] ,[branchname] ,[reservationdate] ,[reservationtime] ,[patientid] ,[type] ,
                                    [numberofpatients] ,[flighttickets] ,[domesctictransportation] ,[tour], [lastservice] ,[insertdate]) output inserted.appointmentid 
                VALUES(@servicename ,@branchname ,@reservationdate ,@reservationtime ,@pateintID ,@type ,@numberofpatients ,
                                    @flighttickets ,@domesctictransportation ,@tour , @lastservice, @insertdate)
            END";
            var result = await _db.QueryFirstOrDefaultAsync<Guid>(query, model);
            return result;
        }

        public async Task<int> DeleteAsync(Guid appointmentID)
        {
            string query = @"Delete from Appointment Where appointmentID = @appointmentID";
            var result = await _db.ExecuteAsync(query, new { appointmentID });
            return result;
        }
    }
}