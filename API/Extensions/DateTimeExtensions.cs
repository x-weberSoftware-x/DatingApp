using System;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - dob.Year;

        //if this is true they have not had their bday yet this year so minus one from age
        if (dob > today.AddYears(-age)) age--;
        //2010 - 2000 = 10
        //08/13/2000 > (06/12/2010 - 10) so 08/13/2010 > 06/13/2010 = true, so minus one from age since the birthday has not passed yet
        return age;
    }
}
