using CourseStore.Core.Domain;
using System;

namespace CourseStore.Services.ApplicationServices
{
    public class CourseService
    {
        public DateTime? GetExpirationDate(LicensingModel licensingModel)
        {
            DateTime? result;
            switch (licensingModel)
            {
                case LicensingModel.TowDays:
                    result = DateTime.UtcNow.AddDays(2);
                    break;
                case LicensingModel.LifeLong:
                    result = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }


    }
}
