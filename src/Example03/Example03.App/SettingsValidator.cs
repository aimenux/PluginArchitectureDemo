﻿using FluentValidation;

namespace Example03.App;

public class SettingsValidator : AbstractValidator<Settings>
{
    public SettingsValidator()
    {
        RuleFor(x => x.PluginsPath)
            .NotEmpty()
            .DirectoryExists();

        RuleFor(x => x.PluginsPattern)
            .NotEmpty();
    }
}