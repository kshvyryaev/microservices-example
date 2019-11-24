using System;

namespace Micro.Services.Activities.Domain.Models
{
    public class Category
    {
        protected Category()
        {
        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; protected set; }

        public string Name { get; protected set; }
    }
}
