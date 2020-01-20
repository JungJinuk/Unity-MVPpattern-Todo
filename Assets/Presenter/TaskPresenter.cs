using System.Collections;
using System.Collections.Generic;

public class TaskPresenter : ITaskPresenter {
    private TaskView _taskView;
    private TaskModel _taskModel;

    private TasksFilterType _currentFiltering = TasksFilterType.ALL_TASKS;

    public TaskPresenter(TaskView taskView) {
        _taskView = taskView;
        _taskModel = new TaskModel();
    }

    public void ActiveTask(Task activeTask) {
        _taskModel.UpdateCompleted(activeTask.Id, false);
        LoadTasks();
    }

    public void AddNewTask(Task newTask) {
        _taskModel.InsertTask(newTask);
        LoadTasks();
    }

    public void CompleteTask(Task completedTask) {
        _taskModel.UpdateCompleted(completedTask.Id, true);
        LoadTasks();
    }

    public void DeleteTask(string taskId) {
        _taskModel.DeleteTaskById(taskId);
        LoadTasks();
    }

    public void LoadTasks() {
        var tasks = _taskModel.GetTasks();
        List<Task> tasksToShow = new List<Task>();
        foreach(Task task in tasks) {
            switch (_currentFiltering) {
                case TasksFilterType.ALL_TASKS:
                    tasksToShow.Add(task);
                    break;
                case TasksFilterType.ACTIVE_TASKS:
                    if (!task.IsCompleted) {
                        tasksToShow.Add(task);
                    }
                    break;
                case TasksFilterType.COMPLETED_TASKS:
                    if (task.IsCompleted) {
                        tasksToShow.Add(task);
                    }
                    break;
                default:
                    tasksToShow.Add(task);
                    break;
            }
        }

        _taskView.ShowTasks(tasksToShow);
    }

    public void SetFiltering(TasksFilterType requestType) {
        _currentFiltering = requestType;
    }
}
