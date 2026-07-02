using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CoachApp.Lessons.Dto;

namespace CoachApp.Lessons
{
    public interface ILessonAppService : IAsyncCrudAppService<
        LessonDto,
        long,
        PagedLessonResultRequestDto,
        CreateLessonDto,
        LessonDto>
    {
        Task<LessonDto> ChangeStatusAsync(long id, ChangeStatusInput input);
    }
}
