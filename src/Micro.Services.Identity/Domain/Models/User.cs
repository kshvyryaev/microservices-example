﻿using System;
using Micro.Common.Exceptions;
using Micro.Services.Identity.Domain.Services;

namespace Micro.Services.Identity.Domain.Models
{
    public class User
    {
        protected User()
        {
        }

        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("empty_user_email", "User email can not be empty");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("empty_user_name", "User name can not be empty");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }

        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Salt { get; protected set; }

        public string Name { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidationException("empty_user_password", "User password can not be empty");
            }

            Salt = encrypter.GetSalt(password);
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }
}
