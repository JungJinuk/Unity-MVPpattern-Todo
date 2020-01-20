using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TaskModel : ITaskModel {
    private Dictionary<string, Task> _tasks = new Dictionary<string, Task>();

    public void DeleteTaskById(string taskId) {
        _tasks.Remove(taskId);
    }

    public void DeleteTasks() {
        _tasks.Clear();
    }

    public Task GetTaskById(string taskId) {
        return _tasks[taskId];
    }

    public List<Task> GetTasks() {
        return _tasks.Values.ToList();
    }

    public void InsertTask(Task task) {
        _tasks.Add(task.Id, task);
    }

    public void UpdateCompleted(string taskId, bool isCompleted) {
        _tasks[taskId].IsCompleted = isCompleted;
    }
}
