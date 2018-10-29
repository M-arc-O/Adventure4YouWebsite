﻿using FluentValidation.Attributes;

using Adventure4You.ViewModels.Identity.Validations;

namespace Adventure4You.ViewModels.Identity
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
