using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.Attendances.Dto;

namespace CoachApp.Attendances
{
    public interface IAttendanceAppService : IAsyncCrudAppService<
        AttendanceDto,
        long,
        PagedAttendanceResultRequestDto,
        CreateAttendanceDto,
        AttendanceDto>
    {
    }
}
