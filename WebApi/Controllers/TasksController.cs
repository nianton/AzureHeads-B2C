﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    public class TasksController : ApiController
    {
        private static readonly ITodoRepository repository = new TodoInMemoryRepository();

        // API Scopes
        public static string ReadPermission = ConfigurationManager.AppSettings["api:ReadScope"];
        public static string WritePermission = ConfigurationManager.AppSettings["api:WriteScope"];

        public IEnumerable<TodoItem> Get()
        {
            HasRequiredScopes(ReadPermission);
            string owner = User.GetId();
            IEnumerable<TodoItem> userTasks = repository.ListByUser(owner);
            return userTasks;
        }

        public void Post(TodoItem task)
        {
            HasRequiredScopes(WritePermission);

            if (String.IsNullOrEmpty(task.Text))
                throw new WebException("Please provide a task description");

            task.Owner = User.GetId();
            task.Completed = false;
            task.DateModified = DateTime.UtcNow;
            repository.Add(task);
        }

        public void Delete(int id)
        {
            HasRequiredScopes(WritePermission);
            repository.Delete(id);
        }

        // Validate to ensure the necessary scopes are present.
        private void HasRequiredScopes(String permission)
        {
            if (!User.HasPermission(permission))
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ReasonPhrase = $"The Scope claim does not contain the {permission} permission."
                });
            }
        }
    }
}
