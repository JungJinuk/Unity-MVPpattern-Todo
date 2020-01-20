using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TaskView : MonoBehaviour, ITaskView {
    private GameObject _baseTaskItem;

    private Dropdown _Dropdown_TaskFilter;
    private Transform _Transform_TasksParent;
    private InputField _InputField_Task;
    private Button _Button_AddTask;

    private TaskPresenter _taskPresenter;

    private void Awake() {
        _baseTaskItem = transform.Find("ScrollRect_Tasks/Viewport/Content/BaseTaskItem").gameObject;
        _Dropdown_TaskFilter = transform.Find("Dropdown_TaskFilter").GetComponent<Dropdown>();
        _Transform_TasksParent = transform.Find("ScrollRect_Tasks/Viewport/Content");
        _InputField_Task = transform.Find("InputField_Task").GetComponent<InputField>();
        _Button_AddTask = transform.Find("Button_Add").GetComponent<Button>();

        _Dropdown_TaskFilter.onValueChanged.AddListener(OnTaskFilterSelected);
        _Button_AddTask.onClick.AddListener(OnClickAddTask);

        _taskPresenter = new TaskPresenter(this);
        _taskPresenter.LoadTasks();
        _taskPresenter.SetFiltering(TasksFilterType.ALL_TASKS);
        _baseTaskItem.SetActive(false);
    }

    private void OnTaskFilterSelected(int itemId) {
        switch (itemId) {
            case 0:
                // All
                _taskPresenter.SetFiltering(TasksFilterType.ALL_TASKS);
                break;
            case 1:
                // Active
                _taskPresenter.SetFiltering(TasksFilterType.ACTIVE_TASKS);
                break;
            case 2:
                // Completed
                _taskPresenter.SetFiltering(TasksFilterType.COMPLETED_TASKS);
                break;
            default:
                _taskPresenter.SetFiltering(TasksFilterType.ALL_TASKS);
                break;
        }
        _taskPresenter.LoadTasks();
    }

    private void OnClickAddTask() {
        string id = DateTime.Now.Ticks.ToString();
        string title = _InputField_Task.text;
        Task newTask = new Task(id, title, false);
        _taskPresenter.AddNewTask(newTask);
        _InputField_Task.text = "";
    }

    public void ShowTasks(List<Task> tasks) {
        for (int i = 1; i < _Transform_TasksParent.childCount; i++) {
            Destroy(_Transform_TasksParent.GetChild(i).gameObject);
        }

        foreach(Task task in tasks) {
            CreateTaskItem(task);
        }
    }

    private void CreateTaskItem(Task task) {
        GameObject todoItemObject = Instantiate(_baseTaskItem, _Transform_TasksParent);
        todoItemObject.SetActive(true);
        SetTaskItemEvent(todoItemObject, task);
    }

    private void SetTaskItemEvent(GameObject taskItem, Task task) {
        Toggle Toggle_Completed = taskItem.transform.Find("Toggle_Completed").GetComponent<Toggle>();
        Button Button_Delete = taskItem.transform.Find("Button_Delete").GetComponent<Button>();
        Text Text_title = taskItem.transform.Find("Text_TodoContent").GetComponent<Text>();
        Toggle_Completed.isOn = task.IsCompleted;
        Toggle_Completed.onValueChanged.AddListener((isCompleted) => {
            if (isCompleted) {
                _taskPresenter.CompleteTask(task);
            } else {
                _taskPresenter.ActiveTask(task);
            }
        });
        Button_Delete.onClick.AddListener(() => {
            _taskPresenter.DeleteTask(task.Id);
        });
        Text_title.text = task.Title;
    }
}
