﻿using FluentValidation;

namespace uBlogger.Api.Features.Users.Timeline
{
    public class TimelineViewModel
    {
        public string Username { get; set; }
    }

    public class TimelineViewModelValidator : AbstractValidator<TimelineViewModel>
    {
        public TimelineViewModelValidator()
        {
            RuleFor(m => m.Username).NotEmpty();
        }
    }
}