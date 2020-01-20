using System.Collections;
using System.Collections.Generic;
using System;

public interface ITaskModel {
    List<Task> GetTasks();
    Task GetTaskById(string taskId);
    void InsertTask(Task task);
    void UpdateCompleted(string taskId, bool isCompleted);
    void DeleteTaskById(string taskId);
    void DeleteTasks();
}
