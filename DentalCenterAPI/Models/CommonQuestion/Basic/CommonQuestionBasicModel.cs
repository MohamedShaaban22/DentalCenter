using System;

namespace DentalCenterAPI.Models.CommonQuestion.Basic
{
    public class CommonQuestionBasicModel
    {
        public Guid CommonQuestionID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Type { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}