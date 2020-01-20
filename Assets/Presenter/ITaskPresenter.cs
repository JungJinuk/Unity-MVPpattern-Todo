using System.Collections.Generic;

public interface ITaskPresenter {
    void LoadTasks();
    void AddNewTask(Task newTask);
    void CompleteTask(Task completedTask);
    void ActiveTask(Task activeTask);
    void SetFiltering(TasksFilterType requestType);
    void DeleteTask(string taskId);
}
