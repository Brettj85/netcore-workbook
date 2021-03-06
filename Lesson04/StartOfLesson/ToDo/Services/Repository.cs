﻿using System;
using System.Collections.Generic;
using System.Threading;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class Repository : IRepository
    {
        private int statusKeyCounter = 3;
        private int toDoKeyCounter = 4;

        private readonly List<Status> _statuses;

        private readonly List<ToDo> _toDos;

        public Repository()
        {
            _statuses = new List<Status>
            {
                new Status { Id = 1, Value = "Not Started" },
                new Status { Id = 2, Value = "In Progress" },
                new Status { Id = 3, Value = "Done" }
            };
            _toDos = new List<ToDo>
            {
                new ToDo
                {
                    Id = 1,
                    Title = "My First ToDo",
                    Description = "Get the app working",
                    Status = _statuses[2],
                    Created = DateTime.Today
                },
                new ToDo
                {
                    Id = 2,
                    Title = "Add DateTime",
                    Description = "Should track when the ToDo was created",
                    Status = _statuses[1],
                    Created = DateTime.Today.AddDays(4)
                },
                new ToDo
                {
                    Id = 3,
                    Title = "Add day-of-the-week TagHelper",
                    Description = "Need an attribute we can use in our view that will pretty format the DateTime as a weekday when possible",
                    Status = _statuses[1],
                    Created = DateTime.Today.AddDays(-4)
                },
                new ToDo
                {
                    Id = 4,
                    Title = "Add ViewComponent",
                    Description = "Should track when the ToDo was created",
                    Status = _statuses[1],
                    Created = DateTime.Today.AddDays(-60)
                },
                new ToDo
                {
                    Id = 5,
                    Title = "Make Tests Pass",
                    Description = "Should track when the ToDo was created",
                    Status = _statuses[1],
                    Created = DateTime.Today.AddDays(60)
                }
            };
        }

        public IReadOnlyList<ToDo> ToDos => _toDos;

        public IReadOnlyList<Status> Statuses => _statuses;

        public void Add(ToDo toDo)
        {
            toDo.Id = Interlocked.Increment(ref toDoKeyCounter);
            toDo.Status = _statuses.Find(x => x.Id == toDo.Status?.Id);
            _toDos.Add(toDo);
        }

        public void Update(int id, ToDo toDo)
        {
            var index = _toDos.FindIndex(x => x.Id == id);
            _toDos.RemoveAt(index);
            toDo.Id = id;
            toDo.Status = _statuses.Find(x => x.Id == toDo.Status?.Id);
            _toDos.Insert(index, toDo);
        }

        public void DeleteToDo(int id)
        {
            var index = _toDos.FindIndex(x => x.Id == id);
            _toDos.RemoveAt(index);
        }

        public ToDo GetToDo(int id)
        {
            return _toDos.Find(x => x.Id == id);
        }

        public void Add(Status status)
        {
            status.Id = Interlocked.Increment(ref statusKeyCounter);
            _statuses.Add(status);
        }

        public void Update(int id, Status toDo)
        {
            var index = _statuses.FindIndex(x => x.Id == id);
            _statuses.RemoveAt(index);
            toDo.Id = id;
            _statuses.Insert(index, toDo);
        }

        public void DeleteStatus(int id)
        {
            var index = _statuses.FindIndex(x => x.Id == id);
            _statuses.RemoveAt(index);
        }

        public Status GetStatus(int id)
        {
            return _statuses.Find(x => x.Id == id);
        }
    }
}
