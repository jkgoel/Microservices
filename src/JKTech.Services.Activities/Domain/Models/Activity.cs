using System;
using JKTech.Common.Exceptions;

namespace JKTech.Services.Activities.Domain.Models
{
    public class Activity
    {
        protected Activity()
        {
        }

        public Activity(Guid id, Guid userId, string name, string category, string description, DateTime createdAt)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new JKTechException("empty_activity_name", $"Activity name can not be empty");
            }

            Id = id;
            UserId = userId;
            Name = name;
            Category = category;
            Description = description;
            CreatedAt = createdAt;
        }

        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public DateTime CreatedAt{ get; protected set; }


        
    }
}