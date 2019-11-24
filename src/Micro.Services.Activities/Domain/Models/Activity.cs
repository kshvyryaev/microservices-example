using System;
using Micro.Common.Exceptions;

namespace Micro.Services.Activities.Domain.Models
{
    public class Activity
    {
        protected Activity()
        {
        }

        public Activity(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("empty_activity_name", "Activity name can not be empty");
            }

            Id = id;
            UserId = userId;
            Category = category;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public Guid Id { get; protected set; }

        public Guid UserId { get; protected set; }

        public string Category { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public DateTime CreatedAt { get; protected set; }
    }
}
